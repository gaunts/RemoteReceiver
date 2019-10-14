using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace RemoteReceiver.Model
{
    public enum ButtonConfigType
    {
        [EnumLabel("Key press")]
        KeyPress,
        [EnumLabel("Mouse action")]
        MouseAction,
        [EnumLabel("Command")]
        Command
    }

    [Serializable]
    public abstract class AButtonConfig
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public abstract Enum Value { get; set; }

        internal AButtonConfig()
        {
            Name = "New button";
            Code = null;
        }

        public abstract void Execute();
    }
}
