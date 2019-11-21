using RemoteReceiver.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteReceiver.ViewModel
{
    public class SystrayViewModel : ViewModelBase
    {
        public bool IsAutoLaunchEnabled => PreferencesManager.IsAutoLaunchEnabled;
        public bool IsAutoDetectEnabled => PreferencesManager.IsAutoDetectEnabled;

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
    }
}
