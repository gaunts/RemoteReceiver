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
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SysTray.Init();
            RemoteServer server = new RemoteServer();
        }

        //private async Task LaunchTestsAsync()
        //{
        //    await Task.Delay(2000);
        //    InputSimulator sim = new InputSimulator();
        //    sim.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
        //}
    }
}
