using System.Collections.Concurrent;
using System.Text;
using System.Windows.Forms;
using Force.Crc32;
using System;

namespace AudioMonitor;

public partial class AccelerometerForm : Form
{
    private double[] _fftValuesX;
    private double[] _fftValuesY;
    private FtdiSerialPortDriver _ftdiPort;
    private ConcurrentBag<Iis2IclxRecord> _iis2IclxLogList;
    private ConcurrentBag<Iis2IclxRecord> _accelLogList;
    private static int _accEntryCount;
    private static uint _prevAccCount;

    private int _bufferIndex;
    private byte[] _buffer;
    private int _samplesNeeded;
    private int _sampleRate;
    private string _portName;
    public AccelerometerForm(int sampleRate, int ftdiIndex)
    {
        InitializeComponent();
        _buffer = new byte[21];
        _sampleRate = sampleRate;
        var paddedAudio = FftSharp.Pad.ZeroPad(new double[sampleRate]);
        var fftMag = FftSharp.Transform.FFTpower(paddedAudio);
        _samplesNeeded = paddedAudio.Length;
        _fftValuesX = new double[fftMag.Length];
        _fftValuesY = new double[fftMag.Length];
        var fftPeriod = FftSharp.Transform.FFTfreqPeriod(sampleRate, fftMag.Length);
        formsPlotX.Plot.AddSignal(_fftValuesX, 1.0 / fftPeriod);
        formsPlotX.Plot.YLabel("Spectral Power");
        formsPlotX.Plot.XLabel("Frequency (Hz)");
        formsPlotX.Plot.Title($"X-Axis {sampleRate} Hz");
        formsPlotX.Plot.SetAxisLimits(0, sampleRate / 2, 0, 2000);
        formsPlotX.Refresh();
        formsPlotY.Plot.AddSignal(_fftValuesY, 1.0 / fftPeriod);
        formsPlotY.Plot.YLabel("Spectral Power");
        formsPlotY.Plot.XLabel("Frequency (Hz)");
        formsPlotY.Plot.Title($"Y-Axis {sampleRate} Hz");
        formsPlotY.Plot.SetAxisLimits(0, sampleRate / 2, 0, 2000);
        formsPlotY.Refresh();

        _ftdiPort = new FtdiSerialPortDriver((uint) (ftdiIndex), 115200);
        _iis2IclxLogList = new ConcurrentBag<Iis2IclxRecord>(); // this is for the fft
        _accelLogList = new ConcurrentBag<Iis2IclxRecord>(); // this is for logging to files
        _ftdiPort.DataReceived += Sp_DataReceived;
        FormClosed += AccelerometerForm_FormClosed;
        _portName = _ftdiPort.PortName;
    }

    private static async Task SaveLogs(List<Iis2IclxRecord> accRecs, string filePath)
    {
        

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        var sb = new StringBuilder();
        foreach (var uartLog in accRecs)
        {
            if (uartLog.RecordType != 0) continue;
            var record =
                uartLog.SequenceNum + "," +
                uartLog.AccelX + "," + uartLog.AccelY + "," + uartLog.SampleRate + "," + uartLog.Scale;
            sb.AppendLine(record);
        }

        var encodedText = Encoding.ASCII.GetBytes(sb.ToString());
        await using var sourceStream = new FileStream(filePath + ".csv",
            FileMode.Create, FileAccess.Write, FileShare.None,
            bufferSize: 4096, useAsync: true);
        await sourceStream.WriteAsync(encodedText);
    }

    private void Sp_DataReceived(object? sender, FtdiSerialPortDriver.DataReceivedArgs e)
    {

        var data = e.Data.ToArray();
        foreach (var b in data)
        {
            if (_bufferIndex == 0 && b != 0x5A)
            {
                System.Diagnostics.Debug.WriteLine("Syncing");
            }
            else
            {
                _buffer[_bufferIndex] = b;
                _bufferIndex++;
            }

            if (_buffer.Length != _bufferIndex) continue;
            using (var cs = new MemoryStream(_buffer))
            {
                using (var br = new BinaryReader(cs))
                {
                    var startByte = br.ReadByte();
                    if (startByte == 0x5A)
                    {
                        // start char received
                        var ts = DateTime.UtcNow;
                        var count = br.ReadUInt32();
                        var x = br.ReadSingle();
                        var y = br.ReadSingle();
                        var sampleRateRaw = br.ReadByte();
                        var scaleRaw = br.ReadByte();
                        var pad = br.ReadBytes(2);
                        var sampleRate = sampleRateRaw switch
                        {
                            0 => 0.0F,
                            1 => 12.5F,
                            2 => 26.0F,
                            3 => 52.0F,
                            4 => 104.0F,
                            5 => 208.0F,
                            6 => 416.0F,
                            7 => 833.0F,
                            11 => 1.6F,
                            _ => 0
                        };
                        var scale = scaleRaw switch
                        {
                            0 => 0.5F,
                            1 => 3.0F,
                            2 => 1.0F,
                            3 => 2.0F,
                            _ => 0
                        };

                        var crc = br.ReadUInt32();
                        var chksum = 0;
                        for (var i = 1; i < _buffer.Length - 4; i++)
                        {
                            chksum += _buffer[i];
                        }

                        if (crc == chksum)
                        {
                            var iis2IclxRecord = new Iis2IclxRecord
                            {
                                TimeStamp = ts,
                                RecordType = 0,
                                SequenceNum = (int) count,
                                AccelX = x,
                                AccelY = y,
                                SampleRate = sampleRate,
                                Scale = scale
                            };
                            _iis2IclxLogList.Add(iis2IclxRecord);
                            _accelLogList.Add(iis2IclxRecord);

                            _prevAccCount++;
                            if (_prevAccCount != count && _accEntryCount != 0)
                            {
                                System.Diagnostics.Debug.WriteLine(_portName + " " + DateTime.Now +
                                                                   " Accel sequence error " + count + " != " +
                                                                   _prevAccCount);
                            }

                            _prevAccCount = count;
                            _accEntryCount++;
                            if (_accEntryCount % (12000) == 0)
                            {
                                System.Diagnostics.Debug.WriteLine("Recevied " + _accEntryCount + " packets");
                            }

                            if (_accEntryCount % (_sampleRate * 60) == 0)
                            {
                                var outFileName = AppContext.BaseDirectory + _portName + "-" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
                                var tempAccList = _accelLogList.ToList();
                                _accelLogList.Clear();
                                Task.Run(() => SaveLogs(tempAccList, outFileName));
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(_portName + " " + DateTime.Now + " Accel CRC error");
                        }
                    }
                    else
                    {
                        _bufferIndex = 0;
                    }
                }
            }

            _bufferIndex = 0;
        }
    }

    private string GetFft(double[] accel, ref double[] fftValues, int sampleRate)
    {
        var paddedAcceleration = FftSharp.Pad.ZeroPad(accel);
        var fftMag = FftSharp.Transform.FFTmagnitude(paddedAcceleration);
        //fftValues = new double[fftMag.Length];
        Array.Copy(fftMag, fftValues, fftMag.Length);
        // find the frequency peak
        var peakIndexX = 0;
        for (var i = 0; i < fftMag.Length; i++)
        {
            if (fftMag[i] > fftMag[peakIndexX])
                peakIndexX = i;
        }

        var fftPeriod = FftSharp.Transform.FFTfreqPeriod(sampleRate, fftMag.Length);
        var peakFrequency = fftPeriod * peakIndexX;
        return $"Peak Frequency: {peakFrequency:N0} Hz";
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        if (null == _iis2IclxLogList || _iis2IclxLogList.Count < _samplesNeeded)
            return;

        var accelerationY = new double[_samplesNeeded];
        var accelerationX = new double[_samplesNeeded];
        var sampleRate = _iis2IclxLogList.First().SampleRate;
        for (var ix = 0; ix < _samplesNeeded; ix++)
        {
            if (!_iis2IclxLogList.TryTake(out var item)) continue;
            accelerationX[ix] = item.AccelX;
            accelerationY[ix] = item.AccelY;
        }

        labelPeakX.Text = GetFft(accelerationX, ref _fftValuesX, (int) sampleRate);
        labelPeakY.Text = GetFft(accelerationY, ref _fftValuesY, (int) sampleRate);

        // request a redraw using a non-blocking render queue
        formsPlotX.RefreshRequest();
        formsPlotY.RefreshRequest();
    }

    private void AccelerometerForm_FormClosed(object? sender, FormClosedEventArgs e)
    {
        _ftdiPort.Close();
        var outFileName = AppContext.BaseDirectory + _portName + "-" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".csv";
        var tempAccList = _accelLogList.ToList();
        _accelLogList.Clear();
        Task.Run(() => SaveLogs(tempAccList, outFileName));
    }

    private void labelPeakY_Click(object sender, EventArgs e)
    {
    }
}