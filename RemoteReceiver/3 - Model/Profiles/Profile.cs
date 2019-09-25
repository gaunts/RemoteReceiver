using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteReceiver.Model.Profiles
{
    [Serializable]
    public class Profile : INotifyPropertyChanged, IEquatable<Profile>
    {
        public List<AButtonConfig> Buttons { get; set; }
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
            Buttons = new List<AButtonConfig>();
            Name = "New profile";
        }

        public bool Equals(Profile other)
        {
            return other != null && String.Equals(this.Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Profile);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
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
}
