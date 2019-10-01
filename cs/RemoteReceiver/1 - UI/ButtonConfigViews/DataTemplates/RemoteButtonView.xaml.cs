using RemoteReceiver.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ButtonView.xaml
    /// </summary>
    public partial class RemoteButtonView : Grid
    {
        public RemoteButtonView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DisplayedButtonConfigProperty = DependencyProperty.RegisterAttached(
            "DisplayedButtonConfig",
            typeof(ButtonConfigViewModel),
            typeof(ProfileView),
             new PropertyMetadata(
                null,
                null)
            );

        public ButtonConfigViewModel DisplayedButtonConfig
        {
            get { return (ButtonConfigViewModel)GetValue(DisplayedButtonConfigProperty); }
            set { SetValue(DisplayedButtonConfigProperty, value); }
        }
    }
}
