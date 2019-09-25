using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace RemoteReceiver.Model.Profiles
{
    internal class KeyPressButtonConfig : AButtonConfig
    {
        private VirtualKeyCode Keycode = VirtualKeyCode.SPACE;
        public override Enum Value
        {
            get => Keycode;
            set => Keycode = (VirtualKeyCode)value;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
