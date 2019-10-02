using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RemoteReceiver.Model
{
    public static class PreferencesManager
    {
        public delegate void BoolPreferenceChangedEventHandler(bool newValue);
        public static event BoolPreferenceChangedEventHandler AutoDetectChanged;

        public static bool IsAutoDetectEnabled
        {
            get
            {
                return Settings.Default.AutoDetect;
            }
            set
            {
                Settings.Default.AutoDetect = value;
                Settings.Default.Save();
                AutoDetectChanged?.Invoke(value);
            }
        }

        private static RegistryKey AutoLaunchRegistryKey
            => Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        private static Assembly CurrentAssembly 
            => Assembly.GetExecutingAssembly();

        public static void CorrectAutoLaunchPath()
        {
            try
            {
                if (IsAutoLaunchEnabled)
                {
                    if (AutoLaunchRegistryKey.GetValue(CurrentAssembly.GetName().Name) is string path &&
                        path != CurrentAssembly.Location)
                    {
                        AutoLaunchRegistryKey.DeleteValue(CurrentAssembly.GetName().Name);
                        IsAutoLaunchEnabled = true;
                    }
                }
            }
            catch { }
        }

        public static bool IsAutoLaunchEnabled
        {
            get
            {
                try
                {
                    return AutoLaunchRegistryKey.GetValue(CurrentAssembly.GetName().Name) != null;
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                if (value)
                    AutoLaunchRegistryKey.SetValue(CurrentAssembly.GetName().Name, CurrentAssembly.Location);
                else
                    AutoLaunchRegistryKey.DeleteValue(CurrentAssembly.GetName().Name);
            }
        }

        public static Model.ProfilesList CustomProfiles
        {
            get
            {
                if (Settings.Default.ProfilesList == null)
                {
                    Settings.Default.ProfilesList = new Model.SerializableProfilesList();
                    Settings.Default.Save();
                }
                return new ProfilesList(Settings.Default.ProfilesList);
            }
        }

        public static Model.Profile SelectedProfile
        {
            get => Settings.Default.SelectedProfile;
            set { Settings.Default.SelectedProfile = value; Settings.Default.Save(); }
        }
    }
}
