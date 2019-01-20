using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Utils;

namespace RemoteReceiver
{
    public class EnumComboBox : ComboBox
    {
        private Type _type;

        #region EnumValue Property
        public static readonly DependencyProperty EnumValueProperty = DependencyProperty.RegisterAttached(
            "EnumValue",
            typeof(Enum),
            typeof(EnumComboBox),
            new PropertyMetadata(
            null,
        new PropertyChangedCallback(OnEnumValuePropertyChanged))
            );

        private static void OnEnumValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnumComboBox combobox = d as EnumComboBox;
            if (e.NewValue == null)
                return;
            if (combobox.ItemsSource == null || !EqualityComparer<Type>.Default.Equals(e.NewValue?.GetType(), combobox._type))
            {
                combobox._type = e.NewValue?.GetType();
                combobox.InitItemsSourceFromType(e.NewValue.GetType());
                int newIndex = Array.IndexOf(combobox.enumsList, e.NewValue);
                if (combobox.SelectedIndex != newIndex)
                    combobox.SelectedIndex = newIndex;
            }
        }

        public static void SetEnumValue(EnumComboBox element, object value)
        {
            element.SetValue(EnumValueProperty, value);
        }

        public static object GetEnumValue(EnumComboBox element)
        {
            return (object)element.GetValue(EnumValueProperty);
        }

        public object EnumValue
        {
            get { return (object)GetValue(EnumValueProperty); }
            set { SetValue(EnumValueProperty, value); }
        }

        #endregion

        public EnumComboBox() : base()
        {
            this.SelectionChanged += Combobox_SelectionChanged;
        }

        private void Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = this.SelectedIndex;
            EnumValue = index == -1 ? null : enumsList.GetValue(index);
        }

        private Array enumsList;

        private void InitItemsSourceFromType(Type type)
        {
            enumsList = Enum.GetValues(type);
            List<string> labels = new List<string>();
            foreach (Enum value in enumsList)
            {
                labels.Add(EnumStrings.GetEnumLabel(value));
            }
            this.ItemsSource = labels;
        }
    }
}
