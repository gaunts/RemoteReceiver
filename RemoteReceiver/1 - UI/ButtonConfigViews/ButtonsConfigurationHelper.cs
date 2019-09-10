using CustomPreferences;
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
    public class ButtonsConfigurationHelper : ApplicationSettingsBase
    {
        public static void ShowConfigurationWindow()
        {
            ConfigurationWindow window = new ConfigurationWindow();
            window.Show();
        }

        internal static Profile AddProfile()
        {
            Profile profile = new Profile();
            PreferencesManager.CustomProfiles.Add(profile);
            Settings.Default.Save();
            return profile;
        }

        internal static void DeleteProfile(Profile profile)
        {
            PreferencesManager.CustomProfiles.Remove(profile);
            Settings.Default.Save();
        }

        internal static void CreateButtonForProfile(Profile selectedItem)
        {
            ButtonActionCreation window = new ButtonActionCreation();
            window.ShowDialog();
        }
    }
}
