using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils
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
            return (labelledEnumValue.GetCustomAttribute(typeof(EnumLabelAttribute)) as EnumLabelAttribute)?.Label ?? labelledEnumValue.ToString();
        }
    }

}
