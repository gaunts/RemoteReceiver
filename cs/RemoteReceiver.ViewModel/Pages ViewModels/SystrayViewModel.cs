using RemoteReceiver.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteReceiver.ViewModel
{
    public class SystrayViewModel : ViewModelBase
    {
        public bool IsAutoLaunchEnabled => PreferencesManager.IsAutoLaunchEnabled;
        public bool IsAutoDetectEnabled => PreferencesManager.IsAutoDetectEnabled;

        public ObservableCollection<string> AvailablePorts { get; private set; } = new ObservableCollection<string>();

        public SystrayViewModel()
        {
            UpdateAvailablePorts();
            RemoteSerialListener.AvailableComPortsChanged += UpdateAvailablePorts;
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
                AvailablePorts.Add(portName);
            }
        }
    }
}
