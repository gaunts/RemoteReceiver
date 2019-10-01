using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RemoteReceiver.ViewModel
{
    public class ConfigurationWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ProfileViewModel> Profiles { get; private set; }

        public ConfigurationWindowViewModel()
        {
            _ = LoadProfilesAsync();
        }

        private async Task LoadProfilesAsync()
        {
            var profiles = new List<ProfileViewModel>();
            await Task.Run(() =>
            {
                foreach (var profile in Model.PreferencesManager.CustomProfiles)
                {
                    profiles.Add(new ProfileViewModel(profile));
                }
            });
            Profiles = new ObservableCollection<ProfileViewModel>(profiles);
            NotifyPropertyChanged(nameof(Profiles));
        }

        public void AddNewProfile()
        {
            var newProfile = Model.PreferencesManager.CustomProfiles.AddNewProfile();
            Profiles.Add(new ProfileViewModel(newProfile));
        }
    }
}
