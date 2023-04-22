using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using FTD2XX_NET;

namespace AudioMonitor
{
    class FtdiSerialPortDriver
    {
        public class DataReceivedArgs : EventArgs
        {
            public byte[] Data { get; set; }
            public uint Index { get; set; }
        }
        public static uint DeviceCount { get; set; }
        private AutoResetEvent receivedDataEvent;
        private BackgroundWorker dataReceivedHandler;
        private FTDI _portFtdi;
        private uint _index;
        public static List<FTDI.FT_DEVICE_INFO_NODE> DeviceList { get; set; }
        public string PortName { get; set; }
        public static string GetPortList()
        {
            var sb = new StringBuilder();
            var ftdiDeviceCount = 0U;
            var ftStatus = FTDI.FT_STATUS.FT_OK;

            // Create new instance of the FTDI device class
            var myFtdiDevice = new FTDI();

            // Determine the number of FTDI devices connected to the machine
            ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);
            // Check status
            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                sb.AppendLine("Number of FTDI devices: " + ftdiDeviceCount);
                sb.AppendLine("");
            }
            else
            {
                // Wait for a key press
                sb.AppendLine("Failed to get number of devices (error " + ftStatus + ")");
                
                return sb.ToString();
            }

            // If no devices available, return
            if (ftdiDeviceCount == 0)
            {
                // Wait for a key press
                sb.AppendLine("Failed to get number of devices (error " + ftStatus + ")");
                return sb.ToString();
            }

            // Allocate storage for device info list
            DeviceList = new List<FTDI.FT_DEVICE_INFO_NODE>();

            // Populate our device list
            ftStatus = myFtdiDevice.GetDeviceList(DeviceList);

            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                var i = 0;
                foreach (var node in DeviceList)
                {
                    sb.AppendLine($"Device Index: {i}");
                    sb.AppendLine("Flags: " + $"{node.Flags:x}");
                    sb.AppendLine("Type: " + node.Type);
                    sb.AppendLine("ID: " + $"{node.ID:x}");
                    sb.AppendLine("Location ID: " + $"{node.LocId:x}");
                    sb.AppendLine("Serial Number: " + node.SerialNumber);
                    sb.AppendLine("Description: " + node.Description);
                    
                    sb.AppendLine("");
                    i++;
                }
            }

            DeviceCount = ftdiDeviceCount;
            return sb.ToString();

        }
        
        public FtdiSerialPortDriver(uint index, uint baudRate)
        {
            _portFtdi = new FTDI();
            var ftStatus = _portFtdi.OpenByIndex(index);
            // Set up device data parameters
            // Set Baud rate to 9600
            ftStatus = _portFtdi.SetBaudRate(baudRate);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to set Baud rate (error " + ftStatus + ")");
                Console.ReadKey();
                return;
            }

            // Set data characteristics - Data bits, Stop bits, Parity
            ftStatus = _portFtdi.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_1, FTDI.FT_PARITY.FT_PARITY_NONE);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to set data characteristics (error " + ftStatus + ")");
                Console.ReadKey();
                return;
            }

            // Set flow control - set RTS/CTS flow control
            ftStatus = _portFtdi.SetFlowControl(FTDI.FT_FLOW_CONTROL.FT_FLOW_NONE, 0x11, 0x13);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to set flow control (error " + ftStatus + ")");
                Console.ReadKey();
                return;
            }

            _portFtdi.GetSerialNumber(out var s);
            PortName = s;
            receivedDataEvent = new AutoResetEvent(false);
            ftStatus = _portFtdi.SetEventNotification(FTDI.FT_EVENTS.FT_EVENT_RXCHAR, receivedDataEvent);
            dataReceivedHandler = new BackgroundWorker();
            dataReceivedHandler.DoWork += ReadData;
            _index = index;
            if (!dataReceivedHandler.IsBusy)
            {
                dataReceivedHandler.RunWorkerAsync();
            }
            Console.WriteLine("Opened Device " + PortName + " at Index " + _index);
        }

        public void Close()
        {
            _portFtdi.Close();
        }
        private void ReadData(object pSender, DoWorkEventArgs pEventArgs)
        {
            var nrOfBytesAvailable = 0U;
            while (true)
            {
                // wait until event is fired
                receivedDataEvent.WaitOne();

                // try to recieve data now
                var status = _portFtdi.GetRxBytesAvailable(ref nrOfBytesAvailable);
                if (status != FTDI.FT_STATUS.FT_OK)
                {
                    break;
                }
                if (nrOfBytesAvailable > 0)
                {
                    var readData = new byte[nrOfBytesAvailable];
                    var numBytesRead = 0U;
                    status = _portFtdi.Read(readData, nrOfBytesAvailable, ref numBytesRead);

                    // invoke your own event handler for data received...
                    //InvokeCharacterReceivedEvent(fParsedData);
                    DataReceived?.Invoke(this, new DataReceivedArgs { Data = readData, Index = _index});
                }
            }
        }

        public bool Write(string data)
        {
            var enconding = new ASCIIEncoding();
            var bytes = enconding.GetBytes(data);
            return Write(bytes);
        }

        public bool Write(byte[] bytes)
        {
            var numBytesWritten = 0U;
            var status = _portFtdi.Write(bytes, bytes.Length, ref numBytesWritten);
            if (status != FTDI.FT_STATUS.FT_OK)
            {
                Console.WriteLine("FTDI Write Status ERROR: " + status);
                return false;
            }
            if (numBytesWritten < bytes.Length)
            {
                Console.WriteLine("FTDI Write Length ERROR: " + status + " length " + bytes.Length +
                                  " written " + numBytesWritten);
                return false;
            }
            return true;
        }

        public event EventHandler<DataReceivedArgs> DataReceived;
        public virtual void OnDataReceived(byte[] data, uint index)
        {
            DataReceived?.Invoke(this, new DataReceivedArgs { Data = data, Index = index });
        }
    }

}

