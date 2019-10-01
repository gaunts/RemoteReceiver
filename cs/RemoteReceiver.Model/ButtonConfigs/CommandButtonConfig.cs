using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace RemoteReceiver.Model
{
    public enum CommandType : int
    {
        [EnumLabel("ShutDown")]
        Shutdown,
        [EnumLabel("Sleep")]
        Sleep
    }

    [Serializable]
    internal class CommandButtonConfig : AButtonConfig
    {
        private CommandType CommandType = CommandType.Shutdown;
        public override Enum Value
        {
            get => CommandType;
            set => CommandType = (CommandType)value;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
