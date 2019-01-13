using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Resources;
using System.Windows;
using RemoteReceiver.Properties;
using static System.Windows.Forms.Menu;

namespace RemoteReceiver
{
    public class SysTray
    {
        private static NotifyIcon nIcon;

        public static void Init()
        {
            if (nIcon == null)
            {
                InitIcon();
                CreateContextMenu();
                UpdateContextMenuChecks();
                UpdateAvailablePorts();
                nIcon.Visible = true;
            }
            else
                throw new Exception("systray can only init once");
        }

        private static void InitIcon()
        {
            nIcon = new NotifyIcon();
            StreamResourceInfo sri = System.Windows.Application.GetResourceStream(new Uri("Icons/fuck.ico", UriKind.Relative));
            nIcon.Icon = new System.Drawing.Icon(sri.Stream);
            nIcon.Text = "Fuck you bitch";
        }

        private static void CreateContextMenu()
        {
            nIcon.ContextMenu = new ContextMenu();

            var a = Settings.Default.PropertyValues;

            nIcon.ContextMenu.MenuItems.Add("Launch at windows startup").Click += (s, c) => { Preferences.IsAutoLaunchEnabled = !Preferences.IsAutoLaunchEnabled; UpdateContextMenuChecks(); };
            nIcon.ContextMenu.MenuItems.Add("Auto detect receiver").Click += (s, c) => { Preferences.IsAutoDetectEnabled = !Preferences.IsAutoDetectEnabled; UpdateContextMenuChecks(); };
            nIcon.ContextMenu.MenuItems.Add("Receiver").Click += (s, c) => { RemoteSerialListener.TestWrite(); };
            nIcon.ContextMenu.MenuItems.Add("Exit").Click += (s, c) => { nIcon.Dispose(); System.Windows.Application.Current.Shutdown(); };
        }

        private static void UpdateContextMenuChecks()
        {
            nIcon.ContextMenu.MenuItems[0].Checked = Preferences.IsAutoLaunchEnabled;
            nIcon.ContextMenu.MenuItems[1].Checked = Preferences.IsAutoDetectEnabled;
        }

        public static void UpdateAvailablePorts()
        {
            MenuItem item = nIcon.ContextMenu.MenuItems[2];
            item.MenuItems.Clear();
            foreach (string portName in SerialPortService.GetAvailableSerialPorts().OrderBy(s => s))
            {
                item.MenuItems.Add(portName).Click += async (s, c) => 
                {
                    bool isChecked = (s as MenuItem).Checked;
                    RemoteSerialListener.ClearCurrentPort();
                    if (!isChecked)
                    {
                        if (!(await RemoteSerialListener.TestPort(portName)))
                        {
                            System.Windows.MessageBox.Show($"Port {portName} has not been recognized");
                        }
                    }
                };
            }
        }

        public static void SelectPortName(string portName)
        {
            MenuItem item = nIcon.ContextMenu.MenuItems[2];

            foreach (MenuItem subItem in item.MenuItems)
                subItem.Checked = (subItem.Text == portName);
        }

        internal static void DisplayString(string text)
        {
            nIcon.ShowBalloonTip(2000, null, text, ToolTipIcon.None);
        }
    }
}
