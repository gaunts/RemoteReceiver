using CustomPreferences;
using RemoteReceiver.Properties;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RemoteReceiver
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

        public ProfileView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DisplayedProfileProperty = DependencyProperty.RegisterAttached(
            "DisplayedProfile",
            typeof(Profile),
            typeof(ProfileView),
             new PropertyMetadata(
                null,
                null)
            );

        public Profile DisplayedProfile
        {
            get { return (Profile)GetValue(DisplayedProfileProperty); }
            set { SetValue(DisplayedProfileProperty, value); }
        }

        private async void Grid_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2)
                return;
            Block.Visibility = Visibility.Collapsed;
            Box.Visibility = Visibility.Visible;
            await Task.Delay(10).ConfigureAwait(true);
            Box.Focusable = true;
            Keyboard.Focus(Box);
            Box.SelectAll();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Box.Text.Length > 0)
            {
                 DisplayedProfile.Name = Box.Text;
                PreferencesManager.SaveProfiles();
            }
            Block.Visibility = Visibility.Visible;
            Box.Visibility = Visibility.Collapsed;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Box.Text = DisplayedProfile.Name;
            if (e.Key == Key.Enter || e.Key == Key.Escape)
                Box.Focusable = false;
        }
    }
}
