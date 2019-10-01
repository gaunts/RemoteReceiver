using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteReceiver.ViewModel
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        internal ProfileViewModel(Model.Profile profile)
        {
            this.Id = profile.Id;
            this.Name = profile.Name;
        }
    }
}
