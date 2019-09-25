using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RemoteReceiver.ViewModel
{
    public class ConfigurationWindowViewModel
    {
        public ICollectionView Profiles { get; private set; }

        public ConfigurationWindowViewModel()
        {
            Profiles = CollectionViewSource.GetDefaultView(PreferencesManager.CustomProfiles);

        }
    }
}
