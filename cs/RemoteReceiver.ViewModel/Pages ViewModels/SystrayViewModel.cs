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
        public bool IsAutoLaunchEnabled
        {
            get { return PreferencesManager.IsAutoLaunchEnabled; }
        }

        public void ReverseAutoLaunch()
        {
            PreferencesManager.IsAutoLaunchEnabled = PreferencesManager.IsAutoLaunchEnabled;
            NotifyPropertyChanged(nameof(IsAutoLaunchEnabled));
        }
    }
}
