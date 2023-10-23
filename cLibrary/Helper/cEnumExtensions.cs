using cLibrary.Attributes;
using System.ComponentModel;
using System.Reflection;


namespace cLibrary.Helper
{
    public static class cEnumExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            StringValueAttribute[] attrs =
               fi.GetCustomAttributes(typeof(StringValueAttribute),
                                       false) as StringValueAttribute[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }

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
