using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RemoteReceiver
{
    /// <summary>
    /// Used to associate an enum with a string.
    /// </summary>
    public class EnumLabelAttribute : System.Attribute
    {
        public string Label { get; set; }

        public EnumLabelAttribute(string value)
        {
            Label = value;
        }
    }


    public static class EnumExtensions
    {
        public static string GetEnumCode(this Enum enumValue)
        {
            EnumCodeAttribute att = enumValue.GetCustomAttribute(typeof(EnumCodeAttribute)) as EnumCodeAttribute;
            return att?.StringValue;
        }

        public static Attribute GetCustomAttribute(this Enum enumValue, Type attributeType)
        {
            Type enumType = enumValue.GetType();
            var field = enumType.GetMember(enumValue.ToString()).FirstOrDefault();
            var attribute = field.GetCustomAttribute(attributeType);

            return attribute;
        }
    }

    public static class EnumStrings
    {
        /// <summary>
        /// Returns the localized string corresponding to the given value. Value must be from LabelledEnums.
        /// Throws an exception if value is invalid     
        /// </summary>
        /// <param name="labelledEnumValue"></param>
        /// <returns></returns>
        public static string GetEnumLabel(Enum labelledEnumValue)
        {
            return (labelledEnumValue.GetCustomAttribute(typeof(EnumLabelAttribute)) as EnumLabelAttribute).Label ;
        }
    }

    public class EnumComboBox : ComboBox
    {
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
            if (combobox.ItemsSource == null)
            {
                combobox.InitItemsSourceFromType(e.NewValue.GetType());
                combobox.SelectedIndex = Array.IndexOf(combobox.enumsList, e.NewValue);
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
            EnumValue = enumsList.GetValue(index);
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
