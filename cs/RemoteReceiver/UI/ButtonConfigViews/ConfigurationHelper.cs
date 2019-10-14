using RemoteReceiver.ViewModel;
using RemoteInterface;
using RemoteReceiver.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteReceiver
{
    public class ConfigurationHelper
    {
        public static void ShowConfigurationWindow()
        {
            ConfigurationWindow window = new ConfigurationWindow();
            window.Show();
        }

        //internal static void AddButtonToProfile(AButtonConfig button, Profile profile)
        //{
        //    profile.Buttons.Add(button);
        //    Settings.Default.Save();
        //}

        //internal static void DeleteProfile(Profile profile)
        //{
        //    PreferencesManager.CustomProfiles.Remove(profile);
        //    Settings.Default.Save();
        //}
    }
}
