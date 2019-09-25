using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using Utils;
using System.Collections;
using RemoteReceiver.Properties;
using System.Windows.Data;

namespace RemoteReceiver.Model.Profiles
{
    [Serializable]
    public class ProfilesList : IEnumerable<Profile>
    {
        private List<Profile> profiles;

        public ProfilesList()
        {
            profiles = new List<Profile>();
        }

        public void AddProfile(Profile profile)
        {
            profiles.Add(profile);
            Settings.Default.Save();
        }

        public void RemoveProfile(Profile profile)
        {
            profiles.Remove(profile);
            Settings.Default.Save();
        }

        public IEnumerator<Profile> GetEnumerator()
        {
            return profiles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return profiles.GetEnumerator();
        }
    }
}
