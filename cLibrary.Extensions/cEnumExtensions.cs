using System.ComponentModel;
using System.Reflection;

namespace cLibrary.Extensions
{
    public class StringValueAttribute : Attribute
    {
        #region Properties
        public string Value { get; protected set; }
        #endregion

        #region Constructor
        public StringValueAttribute(string value)
        {
            Value = value;
        }
        #endregion
    }

    public static class cEnumExtensions
    {     
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
        public static T[] GetList<T>() where T : struct, IConvertible
        {
            try
            {
                T[] lista = (T[])System.Enum.GetValues(typeof(T));
                return lista;
            }
            catch { return new T[] { }; }
        }
    }
}
