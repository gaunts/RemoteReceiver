using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace RemoteReceiver
{

    public static class SerialPortService
    {
        private static string[] _serialPorts;

        private static ManagementEventWatcher arrival;
        private static ManagementEventWatcher removal;

        static SerialPortService()
        {
            _serialPorts = GetAvailableSerialPorts();
            MonitorDeviceChanges();
        }

        /// <summary>
        /// If this method isn't called, an InvalidComObjectException will be thrown (like below):
        /// System.Runtime.InteropServices.InvalidComObjectException was unhandled
        ///Message=COM object that has been separated from its underlying RCW cannot be used.
        ///Source=mscorlib
        ///StackTrace:
        ///     at System.StubHelpers.StubHelpers.StubRegisterRCW(Object pThis, IntPtr pThread)
        ///     at System.Management.IWbemServices.CancelAsyncCall_(IWbemObjectSink pSink)
        ///     at System.Management.SinkForEventQuery.Cancel()
        ///     at System.Management.ManagementEventWatcher.Stop()
        ///     at System.Management.ManagementEventWatcher.Finalize()
        ///InnerException: 
        /// </summary>
        public static void CleanUp()
        {
            arrival.Stop();
            removal.Stop();
        }

        public static event EventHandler<PortsChangedArgs> PortsChanged;

        private static void MonitorDeviceChanges()
        {
            try
            {
                var deviceArrivalQuery = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
                var deviceRemovalQuery = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");

                arrival = new ManagementEventWatcher(deviceArrivalQuery);
                removal = new ManagementEventWatcher(deviceRemovalQuery);

                arrival.EventArrived += (o, args) => RaisePortsChangedIfNecessary(EventType.Insertion);
                removal.EventArrived += (sender, eventArgs) => RaisePortsChangedIfNecessary(EventType.Removal);

                // Start listening for events
                arrival.Start();
                removal.Start();
            }
            catch (ManagementException err)
            {
                Debug.WriteLine($"{err.Message}");
            }
        }

        private static void RaisePortsChangedIfNecessary(EventType eventType)
        {
            lock (_serialPorts)
            {
                var availableSerialPorts = GetAvailableSerialPorts();
                if (!_serialPorts.SequenceEqual(availableSerialPorts))
                {
                    var changes = eventType == EventType.Removal ? _serialPorts.Except(availableSerialPorts) : availableSerialPorts.Except(_serialPorts);
                    _serialPorts = availableSerialPorts;
                    PortsChanged?.Invoke(null, new PortsChangedArgs(eventType, _serialPorts, changes.ToArray()));
                }
            }
        }

        public static string[] GetAvailableSerialPorts()
        {
            return SerialPort.GetPortNames();
        }
    }

    public enum EventType
    {
        Insertion,
        Removal,
    }

    public class PortsChangedArgs : EventArgs
    {
        private readonly EventType _eventType;
        private readonly string[] _serialPorts;
        private readonly string[] _changedPorts;

        public PortsChangedArgs(EventType eventType, string[] serialPorts, string[] changedPorts) {
            _eventType = eventType;
            _serialPorts = serialPorts;
            _changedPorts = changedPorts;
        }

        public string[] SerialPorts
        {
            get
            {
                return _serialPorts;
            }
        }

        public EventType EventType
        {
            get
            {
                return _eventType;
            }
        }

        public string[] ChangedPorts
        {
            get
            {
                return _changedPorts;
            }
        }
    }
}
