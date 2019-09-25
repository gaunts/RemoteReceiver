using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteReceiver.Model.Profiles
{
    public enum MouseActionType
    {
        Click,
        DoubleClick,
        Move
    }

    internal class MouseButtonConfig : AButtonConfig
    {
        private MouseActionType MouseActionType;

        public override Enum Value
        {
            get => MouseActionType;
            set => MouseActionType = (MouseActionType)value;
        }
        
        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
