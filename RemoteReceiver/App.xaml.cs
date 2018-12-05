using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace RemoteReceiver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        NotifyIcon nIcon = new NotifyIcon();

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitIcon();
            CreateContextMenu();
            this.nIcon.Visible = true;
            await Task.Delay(2000);
            this.nIcon.BalloonTipText = "Nique ta mere";
            PressTheSpacebar();
        }

        private void InitIcon()
        {
            this.nIcon.Icon = new System.Drawing.Icon(@"../../fuck.ico");
            this.nIcon.Text = "Fuck you bitch";
        }

        private void CreateContextMenu()
        {
            this.nIcon.ContextMenu = new System.Windows.Forms.ContextMenu();
            this.nIcon.ContextMenu.MenuItems.Add("Exit").Click += (s, c) => { nIcon.Dispose(); System.Windows.Application.Current.Shutdown(); };
        }

        private void PressTheSpacebar()
        {
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
            this.nIcon.ShowBalloonTip(2000);
        }
    }
}
