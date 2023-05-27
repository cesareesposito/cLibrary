using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace csLibrary.Helper
{
    public static partial class cConvertExtensions
    {
        #region String
        public static bool IsNotNullOrEmpty(this string value)
        {
            return value == null ? false : !string.IsNullOrEmpty(value.Trim());
        }
        #endregion

        #region Number 
        public static int ToInt(this object value)
        {
            if (value == null)
                throw new Exception("Impossibile convertire il valore null in int");
            int? nullable = ToIntN(value);
            if (nullable.HasValue)
                return nullable.Value;
            throw new Exception(string.Format("Impossibile convertire il valore {0} in int", value));
        }
        public static int? ToIntN(this object value)
        {
            if (value == null)
                return new int?();
            if (value is int)
                return new int?((int)value);

            try { return new int?(Convert.ToInt32(value)); }
            catch { }
            return new int?();
        }
        public static decimal ToDecimal(this object value)
        {
            if (value == null)
                throw new Exception("Impossibile convertire il valore null in decimal");
            decimal? nullable = ToDecimalN(value);
            if (nullable.HasValue)
                return nullable.Value;
            throw new Exception(string.Format("Impossibile convertire il valore {0} in decimal", value));
        }
        public static decimal? ToDecimalN(this object value)
        {
            if (value == null)
                return new decimal?();
            if (value is decimal)
                return new decimal?((decimal)value);
            string textValue = value as string;
            if (textValue != null)
                return ToDecimalN(textValue);
            try
            {
                return new decimal?(Convert.ToDecimal(value));
            }
            catch
            {
                return new decimal?();
            }
        }
        #endregion

        #region DateTime
        public static DateTime ToDateTime(this object value)
        {
            if (value == null)
                return new DateTime(1900, 1, 1);
            DateTime? nullable = ToDateTimeN(value);
            if (nullable.HasValue)
                return nullable.Value;
            throw new Exception(string.Format("Impossibile convertire il valore {0} in datetime", value));
        }
        public static DateTime? ToDateTimeN(this object value)
        {
            if (value == null)
                return new DateTime?();
            var textValue = value as string;
            try
            {
                return new DateTime?(Convert.ToDateTime(textValue));
            }
            catch
            {
                return new DateTime?();
            }
        }
        public static DateTime ToAbsoluteEnd(this DateTime date)
        {
            return date.Date.AddDays(1).AddMilliseconds(-3);
        }
        public static DateTime ToAbsoluteStart(this DateTime date)
        {
            return date.Date;
        }
        public static DateTime ToEndOfMonth(this DateTime date)
        {
            var days = DateTime.DaysInMonth(date.Year, date.Month);
            return date.AddDays(days - date.Day);
        }
        public static DateTime ToStartOfMonth(this DateTime date)
        {
            return date.Day != 1 ? date.AddDays(-date.Day) : date;
        }
        #endregion

        #region Enum
        public static string Description(this Enum enumToEvaluate)
        {
            try
            {
                var fieldInfo = enumToEvaluate.GetType().GetField(enumToEvaluate.ToString());

                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                    return attributes[0].Description;

                var dispattributes = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (dispattributes.Length > 0)
                    return dispattributes[0].Name;

                var dispnameattributes = (DisplayNameAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                if (dispnameattributes.Length > 0)
                    return dispnameattributes[0].DisplayName;

                return enumToEvaluate.ToString();
            }
            catch { return string.Empty; }
        }
        public static T ToEnum<T>(this object value)
        {
            if (value != null)
            {
                return (T)Enum.Parse(typeof(T), value.ToString(), true);
            }
            return (T)Enum.Parse(typeof(T), "", true);
        }
        public static Dictionary<string, string> GetEnumForBinding(Type enumeration)
        {
            var source = Enum.GetValues(enumeration);

            var dict = new Dictionary<string, string>();
            foreach (var it in source)
            {
                var field = it.GetType().GetField(it.ToString());
                var attrs = (DisplayAttribute)field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
                var text = it.ToString();
                if (attrs == null)
                {
                    var descr = (DescriptionAttribute)field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                    text = descr != null ? descr.Description : text.ToString();
                }
                var key = (it.ToIntN() ?? it).ToString();
                dict.Add(key, text);
            }
            return dict;
        }
        #endregion
    }
}