using RemoteInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace RemoteReceiver
{
    public static class SocketCommandExecution
    {
        public static void ExecuteCommand(RemoteCommand command)
        {
            InputSimulator sim = new InputSimulator();
            VirtualKeyCode? key = null;

            switch(command)
            {
                case RemoteCommand.LeftArrow:
                    key = VirtualKeyCode.LEFT;
                    break;
                case RemoteCommand.RightArrow:
                    key = VirtualKeyCode.RIGHT;
                    break;
                case RemoteCommand.VolumeUp:
                    key = VirtualKeyCode.VOLUME_UP;
                    break;
                case RemoteCommand.VolumeDown:
                    key = VirtualKeyCode.VOLUME_DOWN;
                    break;
                case RemoteCommand.Space:
                    key = VirtualKeyCode.SPACE;
                    break;
                case RemoteCommand.PlayPause:
                    key = VirtualKeyCode.MEDIA_PLAY_PAUSE;
                    break;
                case RemoteCommand.Previous:
                    key = VirtualKeyCode.MEDIA_PREV_TRACK;
                    break;
                case RemoteCommand.Next:
                    key = VirtualKeyCode.MEDIA_NEXT_TRACK;
                    break;
                case RemoteCommand.Shutdown:
                    Process.Start(new ProcessStartInfo("shutdown", "/s /t 0 /f") { CreateNoWindow = true, UseShellExecute = false });
                    break; //lol
            }

            if (key != null)
               sim.Keyboard.KeyPress(key.Value);
        }
    }
}
