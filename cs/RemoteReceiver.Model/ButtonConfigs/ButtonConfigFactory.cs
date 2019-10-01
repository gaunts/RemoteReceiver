using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteReceiver.Model
{
    public class ButtonConfigFactory
    {
        public AButtonConfig BuildButtonConfig(ButtonConfigType type)
        {
            AButtonConfig res;
            switch (type)
            {
                case ButtonConfigType.Command:
                    res = new CommandButtonConfig();
                    break;
                case ButtonConfigType.KeyPress:
                    res = new KeyPressButtonConfig();
                    break;
                case ButtonConfigType.MouseAction:
                    res = new MouseButtonConfig();
                    break;
                default:
                    throw new NotImplementedException();
            }
            return res;
        }
    }
}
