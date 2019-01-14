using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace CustomPreferences
{
    public enum Fuck
    {
        lol,
        merde
    }

    [Serializable]
    public class ProfilesList
    {
        public readonly List<Profile> Profiles = null;

        public ProfilesList()
        {
            Profiles = new List<Profile>();
        }
    }

    [Serializable]
    public class Profile : INotifyPropertyChanged
    {
        public List<ButtonConfig> Buttons { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        public Profile()
        {
            Buttons = new List<ButtonConfig>();
            Name = "New profile";
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }

    [Serializable]
    public abstract class ButtonConfig
    {
        private string Name { get; set; }

        public ButtonConfig()
        {
            Name = "New button";
        }
    }

    [Serializable]
    public class KeyPressButtonConfig : ButtonConfig
    {
        public VirtualKeyCode KeyCode { get; set; }
    }

    [Serializable]
    public class CommandButtonConfig : ButtonConfig
    {
        public string Command;
        public string Parameters;
    }
}
