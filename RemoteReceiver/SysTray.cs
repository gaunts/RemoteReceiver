using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteReceiver
{
    class SysTray
    {
        //private static SysTray Instance;
        private static NotifyIcon nIcon;

        public static void Init()
        {
            if (nIcon == null)
            {
                InitIcon();
                CreateContextMenu();
                nIcon.Visible = true;
            }
            else
                throw new Exception("systray can only init once");
        }

        private static void InitIcon()
        {
            nIcon = new NotifyIcon();
            nIcon.Icon = new System.Drawing.Icon(@"../../fuck.ico");
            nIcon.Text = "Fuck you bitch";
        }

        private static void CreateContextMenu()
        {
            nIcon.ContextMenu = new System.Windows.Forms.ContextMenu();
            nIcon.ContextMenu.MenuItems.Add("Start microsoft").Click += (s, c) => { AsynchronousSocketListener.StartListening(); };
            nIcon.ContextMenu.MenuItems.Add("Start me").Click += (s, c) => {  RemoteServer.StartListening(); Debug.WriteLine("wallah"); };
            nIcon.ContextMenu.MenuItems.Add("shut down").Click += (s, c) => { Process.Start(new ProcessStartInfo("shutdown", "/s /t 0") { CreateNoWindow = true, UseShellExecute = false }); };
            nIcon.ContextMenu.MenuItems.Add("Exit").Click += (s, c) => { nIcon.Dispose(); System.Windows.Application.Current.Shutdown(); };
        }

        internal static void DisplayString(string text)
        {
            nIcon.ShowBalloonTip(2000, null, text, ToolTipIcon.None);
        }
    }
}
