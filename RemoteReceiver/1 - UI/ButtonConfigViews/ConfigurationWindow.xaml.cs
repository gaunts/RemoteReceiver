using CustomPreferences;
using RemoteInterface;
using RemoteReceiver.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace RemoteReceiver
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        public ConfigurationWindow()
        {
            LoadedProfilesList = new ObservableCollection<Profile>(PreferencesManager.CustomProfiles);
            InitializeComponent();
            Loaded += HasLoaded;
        }

        private void HasLoaded(object sender, RoutedEventArgs e)
        {
        }

        public ObservableCollection<Profile> LoadedProfilesList { get; set; }
        public ObservableCollection<ButtonConfig> LoadedButtonsList { get; set; }

        #region Buttons

        private void NewProfileButton_Click(object sender, RoutedEventArgs e)
        {
            LoadedProfilesList.Add(ButtonsConfigurationHelper.AddProfile());
        }

        private void DeleteProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var profile = ProfilesList.SelectedItem as Profile;
            ButtonsConfigurationHelper.DeleteProfile(profile);
            LoadedProfilesList.Remove(profile);
        }

        private void NewButtonConfig_Click(object sender, RoutedEventArgs e)
        {
            ButtonsConfigurationHelper.CreateButtonForProfile((Profile)ProfilesList.SelectedItem);
        }

        private void DeleteButtonConfig_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Micelanious
        private void ProfilesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadedButtonsList = ProfilesList.SelectedItem != null ? new ObservableCollection<ButtonConfig>(((Profile)ProfilesList.SelectedItem).Buttons) : new ObservableCollection<ButtonConfig>();
            PreferencesManager.SelectedProfile = (Profile)ProfilesList.SelectedItem;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            base.OnClosing(e);
        }

        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.FocusedElement is TextBox)
            {
                Keyboard.FocusedElement.Focusable = false;
            }
        }
        #endregion

        private void Border_MouseUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Listview_Loaded(object sender, RoutedEventArgs e)
        {
            ProfilesList.SelectedValuePath = nameof(Profile.Id);
            ProfilesList.SelectedValue = PreferencesManager.SelectedProfile.Id;

            //ProfilesList.SelectedItem = PreferencesManager.SelectedProfile;
            ProfilesList.ItemContainerGenerator.StatusChanged += Test;

           //ListViewItem item = ProfilesList.item
            //item.IsSelected = true;
        }

        private void Test(object sender, EventArgs e)
        {
            var item = ProfilesList.ItemContainerGenerator.ContainerFromItem(ProfilesList.SelectedItem);
            //var b = item.IsSealed;
        }
    }
}
