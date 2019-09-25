using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace RemoteReceiver
{
    public static class RemoteSerialListener
    {
        static SerialPort _devicePort;
        static bool _connectedOnce;
        const string responseString = "qkjweklqjwe87dahgdq9qwd";
        const string askingString = "who are you bitch";

        public static string CurrentlyConnectedPort => _devicePort?.PortName;

        public static void StartListening()
        {
            SerialPortService.PortsChanged += PortsChanged;
            RefreshAutoDetectPreference();
        }

        public static void RefreshAutoDetectPreference()
        {
            if (PreferencesManager.IsAutoDetectEnabled && _devicePort == null)
                FindDevice(SerialPortService.GetAvailableSerialPorts());
        }

        public static void ClearCurrentPort()
        {
            _devicePort?.Close();
            _devicePort = null;
            SysTray.SelectPortName(null);
        }

        private static async void PortsChanged(object sender, PortsChangedArgs e)
        {
            SysTray.UpdateAvailablePorts();
            if (_devicePort != null && 
                e.EventType == EventType.Removal && 
                !e.SerialPorts.Contains(_devicePort.PortName))
            {
                ClearCurrentPort();
                Debug.WriteLine("removed ir");
                _connectedOnce = false;
                return;
            }
            else if (e.EventType == EventType.Insertion && _devicePort == null && PreferencesManager.IsAutoDetectEnabled)
            {
                Debug.WriteLine("added something");
                await Task.Delay(6000);
                FindDevice(e.ChangedPorts);
            }
        }

        public static void FindDevice(params string[] comsToTest)
        {
            List<Task> tasks = new List<Task>();
            foreach (string comPort in comsToTest)
            {
                tasks.Add(Task.Run(async () => 
                {
                    try
                    {
                        await TestPort(comPort);
                    }
                    catch { }
                }));
            }
        }

        public static async Task<bool> TestPort(string comPort)
        {
            string result = null;
            Debug.WriteLine($"Testing port {comPort}");
            SerialPort testedPort = new SerialPort(comPort, 9600, Parity.None, 8, StopBits.One);
            try
            {
                testedPort.Open();
                if (!_connectedOnce)
                {
                    testedPort.Write("test");
                    testedPort.Close();
                    await Task.Delay(1500);
                    testedPort.Open();
                }
                testedPort.Write(askingString);
                testedPort.ReadTimeout = 2000;
                char[] buffer = new char[responseString.Length];
                result = testedPort.ReadLine().TrimEnd('\r');
                if (result != responseString)
                    Debug.WriteLine($"Port {comPort} said {result}");
                else
                {
                    _devicePort = testedPort;
                    _devicePort.DataReceived += SerialRead;
                    Debug.WriteLine($"Port {comPort} is IR receiver !");
                    SysTray.SelectPortName(comPort);
                    _connectedOnce = true;
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{comPort} : {e.Message}");
            }
            testedPort.Close();
            return false;
        }


        private static void SerialRead(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;
            Debug.WriteLine(port.ReadExisting());
        }

        //private static void ConfirmationReceive(object sender, SerialDataReceivedEventArgs e)
        //{
        //    SerialPort testedPort = (SerialPort)sender;
        //    string result = testedPort.ReadExisting().TrimEnd('\r', '\n');
        //    testedPort.DataReceived -= ConfirmationReceive;
        //    testedPort.Close();
        //    if (result != responseString)
        //    {
        //        Debug.WriteLine($"Port {testedPort.PortName} said {result}");
        //    }
        //    else
        //    {
        //        _devicePort = new SerialPort(testedPort.PortName, 9600, Parity.None, 8, StopBits.One);
        //        _devicePort.DataReceived += SerialRead;
        //        Debug.WriteLine($"Port {testedPort.PortName} is IR receiver !");
        //    }
        //}
    }
}
