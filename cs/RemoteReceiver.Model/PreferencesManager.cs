using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        private static string _appName;
        private static string _appFullPath;

        public static void CorrectAutoLaunchPath(string appFullPath)
        {
            _appFullPath = appFullPath;
            _appName = Path.GetFileNameWithoutExtension(appFullPath);
            try
            {
                if (IsAutoLaunchEnabled)
                {
                    if (AutoLaunchRegistryKey.GetValue(_appName) is string savedPath &&
                        savedPath != _appFullPath)
                    {
                        AutoLaunchRegistryKey.DeleteValue(_appName);
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
                    return AutoLaunchRegistryKey.GetValue(_appName) != null;
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                if (value)
                    AutoLaunchRegistryKey.SetValue(_appName, _appFullPath);
                else
                    AutoLaunchRegistryKey.DeleteValue(_appName);
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
