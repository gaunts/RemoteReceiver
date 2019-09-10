using CustomPreferences;
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

        public ButtonActionCreation()
        {
            this.ButtonConfig = new KeyPressButtonConfig();
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
            ButtonActionCreation window = d as ButtonActionCreation;
            ButtonConfigType configType = (ButtonConfigType)e.NewValue;
            window.ButtonConfig = configType == ButtonConfigType.KeyPress ? (ButtonConfig)new KeyPressButtonConfig() : new CommandButtonConfig();
        }

        public ButtonConfigType ActionType
        {
            get { return (ButtonConfigType)GetValue(ActionTypeProperty); }
            set { SetValue(ActionTypeProperty, value); }
        }

        private ButtonConfig buttonConfig;

        public ButtonConfig ButtonConfig
        {
            get { return buttonConfig; }
            set { buttonConfig = value; NotifyPropertyChanged(nameof(ButtonConfig)); }
        }

    }
}
