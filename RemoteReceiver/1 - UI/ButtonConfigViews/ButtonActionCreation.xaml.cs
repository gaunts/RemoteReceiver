using Profiles;
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
using System.Windows.Shapes;

namespace RemoteReceiver
{
    /// <summary>
    /// Interaction logic for ButtonActionCreation.xaml
    /// </summary>
    public partial class ButtonActionCreation : Window, INotifyPropertyChanged
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

        private ButtonConfigFactory buttonFactory;
        private Profile profile;

        private AButtonConfig buttonConfig;
        public AButtonConfig ButtonConfig
        {
            get { return buttonConfig; }
            set { buttonConfig = value; NotifyPropertyChanged(nameof(ButtonConfig)); }
        }

        public ButtonConfigType ActionType
        {
            get { return (ButtonConfigType)GetValue(ActionTypeProperty); }
            set { SetValue(ActionTypeProperty, value); }
        }

        public ButtonActionCreation(Profile profile)
        {
            this.profile = profile;
            this.buttonFactory = new ButtonConfigFactory();
            this.ButtonConfig = buttonFactory.BuildButtonConfig(ButtonConfigType.KeyPress);
            InitializeComponent();
        }

        public static readonly DependencyProperty ActionTypeProperty = DependencyProperty.RegisterAttached(
            "ActionType",
            typeof(ButtonConfigType),
            typeof(ButtonActionCreation),
             new PropertyMetadata(
                ButtonConfigType.KeyPress, new PropertyChangedCallback(ActionTypePropertyChanged))
            );

        private static void ActionTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ButtonActionCreation dependencyObject = d as ButtonActionCreation;
            ButtonConfigType configType = (ButtonConfigType)e.NewValue;
            dependencyObject.ButtonConfig = dependencyObject.buttonFactory.BuildButtonConfig(configType);
        }

        private void SaveButtonAction(object sender, RoutedEventArgs e)
        {
            ConfigurationHelper.AddButtonToProfile(this.ButtonConfig, this.profile);
        }
    }
}
