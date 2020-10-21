using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;
using static System.Windows.Forms.Menu;

namespace RemoteReceiver
{
    /// <summary>
    /// Interaction logic for SysTray.xaml
    /// </summary>
    public partial class SysTray : System.Windows.Controls.UserControl
    {
        public SysTray()
        {
            InitializeComponent();
            SystrayViewModel.PropertyChanged += PropertyChanged;
        }

        private void WindowsStartup_Click(object sender, RoutedEventArgs e)
        {
            SystrayViewModel.SetAutoLaunch(this.AutoLaunchMenuItem.IsChecked);
        }

        private void Autodetect_Click(object sender, RoutedEventArgs e)
        {
            SystrayViewModel.SetAutoDetect(this.AutoDetectMenuItem.IsChecked);
        }

        private void Configure_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationWindow.ShowConfigurationWindow();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SystrayViewModel.SelectedPortName))
            {
                //foreach (var subItem in ReceiverMenuItem.Items.)
                //{
                //    //subItem.IsChecked = ((subItem.Header as string) == SystrayViewModel.SelectedPort);
                //}
            }
        }

        private async void ComPort_Click(object sender, RoutedEventArgs e)
        {
            string selectedString = (sender as System.Windows.Controls.MenuItem)?.DataContext as string;
            bool result = await SystrayViewModel.SelectPort(selectedString);
            if (!result)
                notifyIcon.ShowBalloonTip("Meh", $"{selectedString} is not an IR receiver", Hardcodet.Wpf.TaskbarNotification.BalloonIcon.None);
            else
                notifyIcon.ShowBalloonTip("Yay", $"{selectedString} connected succesfully", Hardcodet.Wpf.TaskbarNotification.BalloonIcon.None);
        }
    }
}
