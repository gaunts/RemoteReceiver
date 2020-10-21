using RemoteReceiver.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteReceiver.ViewModel
{
    public class ComPortInfo
    {
        public string PortName { get; set; }
        public bool IsSelected { get; set; }
    }

    public class SystrayViewModel : ViewModelBase
    {
        public bool IsAutoLaunchEnabled => PreferencesManager.IsAutoLaunchEnabled;
        public bool IsAutoDetectEnabled => PreferencesManager.IsAutoDetectEnabled;
        public string SelectedPortName { get; set; }

        public event Action PortSelectionChanged;

        public ObservableCollection<ComPortInfo> AvailablePorts { get; private set; } = new ObservableCollection<ComPortInfo>();

        public SystrayViewModel()
        {
            UpdateAvailablePorts();
            RemoteSerialListener.AvailableComPortsChanged += UpdateAvailablePorts;
            RemoteSerialListener.IRReceiverComPortChanged += PortSelectionChangedHandler;
        }

        private void PortSelectionChangedHandler(string newPort)
        {
            SelectedPortName = newPort;
            NotifyPropertyChanged(nameof(SelectedPortName));
        }

        ~SystrayViewModel()
        {
            RemoteSerialListener.AvailableComPortsChanged -= UpdateAvailablePorts;
        }

        public void SetAutoLaunch(bool enabled)
        {
            PreferencesManager.IsAutoLaunchEnabled = enabled;
            NotifyPropertyChanged(nameof(IsAutoLaunchEnabled));
        }

        public void SetAutoDetect(bool enabled)
        {
            PreferencesManager.IsAutoDetectEnabled = enabled;
            NotifyPropertyChanged(nameof(IsAutoDetectEnabled));
        }

        public async Task<bool> SelectPort(string portName)
        {
            RemoteSerialListener.ClearCurrentPort();
            return await RemoteSerialListener.TestPort(portName);
        }

        public void UpdateAvailablePorts()
        {
            AvailablePorts.Clear();
            foreach (string portName in SerialPortService.GetAvailableSerialPorts().OrderBy(s => s))
            {
                AvailablePorts.Add(new ComPortInfo() { PortName = portName });
            }
        }
    }
}
