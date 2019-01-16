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
    //public static class TranslationHelper
    //{
    //    private static ResourceLoader Loader { get; set; }

    //    public static string LocalizedStringFromKey(string key)
    //    {
    //        if (Loader == null)
    //            Loader = ResourceLoader.GetForViewIndependentUse();
    //        return Loader.GetString(key);
    //    }
    //}

    /// <summary>
    /// Used to associate an enum with a code. Usualy used to convert an enum in the viewModel to a string value in the Model.
    /// </summary>
    public class EnumCodeAttribute : System.Attribute
    {
        public string StringValue { get; set; }
        public int ComparedCharsCount { get; set; }
        public string DynamicValueFieldName { get; set; }

        public EnumCodeAttribute(string value)
        {
            StringValue = value;
            ComparedCharsCount = -1;
            DynamicValueFieldName = null;
        }

        public EnumCodeAttribute(string value, int comparedCharsCount, string dynamicValueFieldName)
        {
            StringValue = value;
            ComparedCharsCount = comparedCharsCount;
            DynamicValueFieldName = dynamicValueFieldName;
        }
    }

    /// <summary>
    /// Every labelled enum must have this attribute.
    /// Used to launch an exception if you try to get a string from an invalid enum
    /// </summary>
    public class LabelledEnumTypeAttribute : LocalizedEnumAttribute { }

    /// <summary>
    /// Use this if an enum value from a LabelledEnumType enum does not have a label.
    /// Used to launch an exception if you try to get a string from an invalid enum
    /// </summary>
    public class NoLabelEnumValue : Attribute { }

    /// <summary>
    /// To be inherited by LabelledEnumTypeAttribute and MessageEnumTypeAttribute
    /// </summary>
    public abstract class LocalizedEnumAttribute : Attribute { }

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
        public static string GetEnumLocalizationKey(Enum localizedEnumValue)
        {
            if (localizedEnumValue.GetType().GetTypeInfo().GetCustomAttribute<LocalizedEnumAttribute>(true) == null)
                throw new Exception("Enums : This enum is not localized. Need to implement LocalizedEnumAttribute");

            string localizationKey = String.Format("{0}_{1}", localizedEnumValue.GetType().Name, localizedEnumValue.ToString());
            return localizationKey;
        }

        /// <summary>
        /// Returns the localized string corresponding to the given value. Value must be from LabelledEnums.
        /// Throws an exception if value is invalid     
        /// </summary>
        /// <param name="labelledEnumValue"></param>
        /// <returns></returns>
        public static string GetEnumLabel(Enum labelledEnumValue)
        {
            if (labelledEnumValue.GetType().GetTypeInfo().GetCustomAttribute<LabelledEnumTypeAttribute>(true) == null)
                throw new Exception("Enums : This enum is not localized. Need to implement LabelledEnumTypeAttribute");

            if (labelledEnumValue.GetCustomAttribute(typeof(NoLabelEnumValue)) != null)
                return null;

            //return labelledEnumValue;
            //return TranslationHelper.LocalizedStringFromKey(GetEnumLocalizationKey(labelledEnumValue));
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
