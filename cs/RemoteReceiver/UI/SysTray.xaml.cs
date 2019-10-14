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
        NotifyIcon nIcon;

        public SysTray()
        {
            //this.Loaded += HasLoaded;

            InitializeComponent();
        }

        private void HasLoaded(object sender, RoutedEventArgs e)
        {
            //nIcon = new NotifyIcon();
            //StreamResourceInfo sri = System.Windows.Application.GetResourceStream(new Uri("Icons/fuck.ico", UriKind.Relative));
            //nIcon.Icon = new System.Drawing.Icon(sri.Stream);
            //nIcon.Text = "Fuck you bitch";
            //nIcon.Visible = true;
        }

        private void WindowsStartup_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
