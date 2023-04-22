using FftSharp;
using Microsoft.VisualBasic;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTD2XX_NET;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AudioMonitor
{
    public partial class MenuForm : Form
    {
        public readonly MMDevice[] AudioDevices = new MMDeviceEnumerator()
            .EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active)
            .ToArray();

        private List<FTDI.FT_DEVICE_INFO_NODE> _ftdiPorts;
        private BindingList<string> _ftdiPortsSerialNumbers;
        public MenuForm()
        {
            InitializeComponent();
            
            _ftdiPorts = new List<FTDI.FT_DEVICE_INFO_NODE>();
            _ftdiPortsSerialNumbers = new BindingList<string>();
            System.Diagnostics.Debug.WriteLine(FtdiSerialPortDriver.GetPortList());
            FtdiSerialPortDriver.GetPortList();
            _ftdiPorts = FtdiSerialPortDriver.DeviceList;
            foreach (var ftdiPort in _ftdiPorts)
            {
                _ftdiPortsSerialNumbers.Add(ftdiPort.ID.ToString());
                lbDevice.Items.Add(ftdiPort.SerialNumber);
            }
            lbDevice.SelectedIndex = 0;
        }

        private WasapiCapture GetSelectedDevice()
        {
            var selectedDevice = AudioDevices[lbDevice.SelectedIndex];
            return selectedDevice.DataFlow == DataFlow.Render
                ? new WasapiLoopbackCapture(selectedDevice)
                : new WasapiCapture(selectedDevice, true, 10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new AccelerometerForm(416, lbDevice.SelectedIndex).ShowDialog();
        }
    }
}
