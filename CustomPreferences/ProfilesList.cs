using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using Utils;

namespace CustomPreferences
{
    [Serializable]
    public class ProfilesList : List<Profile>
    {
    }

    [Serializable]
    public class Profile : INotifyPropertyChanged, IEquatable<Profile>
    {
        public List<ButtonConfig> Buttons { get; set; }
        public string Id { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        public Profile()
        {
            Id = Guid.NewGuid().ToString("N");
            Buttons = new List<ButtonConfig>();
            Name = "New profile";
        }

        bool IEquatable<Profile>.Equals(Profile other)
        {
            return other != null && String.Equals(this.Id, other.Id);
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

    public enum ButtonConfigType : int
    {
        [EnumLabel("Key press")]
        KeyPress,
        //[EnumLabel("Mouse action")]
        //MouseAction,
        [EnumLabel("Command")]
        Command
    }

    [Serializable]
    public abstract class ButtonConfig
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public abstract Enum Value { get; set; }

        public ButtonConfig()
        {
            Name = "New button";
            Code = null;
        }
    }

    [Serializable]
    public class KeyPressButtonConfig : ButtonConfig
    {
        private VirtualKeyCode Keycode = VirtualKeyCode.SPACE;
        public override Enum Value { get => Keycode; set => Keycode = (VirtualKeyCode)value; }
    }

    public enum CommandType : int
    {
        [EnumLabel("ShutDown")]
        Shutdown,
        [EnumLabel("Sleep")]
        Sleep
    }

    [Serializable]
    public class CommandButtonConfig : ButtonConfig
    {
        private CommandType CommandType = CommandType.Shutdown;
        public override Enum Value { get => CommandType; set => CommandType = (CommandType)value; }
    }
}
