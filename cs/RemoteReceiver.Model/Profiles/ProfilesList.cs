using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Configuration;

namespace RemoteReceiver.Model
{
    [Serializable]
    public class SerializableProfilesList : List<Profile>
    {
        internal SerializableProfilesList() : base() {}
    }

    public class ProfilesList : IReadOnlyList<Profile>
    {
        private SerializableProfilesList profiles;

        public int Count => ((IReadOnlyList<Profile>)profiles).Count;

        public Profile this[int index] => ((IReadOnlyList<Profile>)profiles)[index];

        public ProfilesList(SerializableProfilesList serializable)
        {
            profiles = serializable;
        }

        public Profile AddNewProfile()
        {
            var newProfile = new Profile();
            profiles.Add(newProfile);
            Settings.Default.Save();
            return newProfile;
        }

        public IEnumerator<Profile> GetEnumerator()
        {
            return ((IEnumerable<Profile>)profiles).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Profile>)profiles).GetEnumerator();
        }

        public void RemoveProfile(Profile profile)
        {
            profiles.Remove(profile);
            Settings.Default.Save();
        }
    }
}
