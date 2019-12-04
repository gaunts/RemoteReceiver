using System;
using System.Collections.Generic;
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
        private bool _hasLoaded = false;

        public SysTray()
        {
            this.Loaded += HasLoaded;
            InitializeComponent();
        }

        private void HasLoaded(object sender, RoutedEventArgs e)
        {
            _hasLoaded = true;
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

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private async void ComPort_Click(object sender, RoutedEventArgs e)
        {
            await SystrayViewModel.SelectPort((sender as System.Windows.Controls.MenuItem)?.DataContext as string);
        }
    }
}
