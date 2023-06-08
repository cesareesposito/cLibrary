using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web;

namespace cLibrary.Helper
{
    public static partial class cConvertExtensions
    {
        public static bool IsNull(object obj)
        {
            return obj == null || obj == DBNull.Value;
        }

        #region String

        public static string ToQueryString<T>(this T obj)
        {
            if (obj is null) return string.Empty;

            var querystring = new List<string>();

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);

                if (value != null)
                {
                    var encodedValue = GetEncodedValue(value);
                    var encodedName = GetEncodedName(property);

                    querystring.Add($"{encodedName}={encodedValue}");
                }
            }

            return "?" + string.Join("&", querystring);
        }

        private static string GetEncodedValue(object value)
        {
            if (value is DateTime dateTimeValue)
            {
                return HttpUtility.UrlEncode(dateTimeValue.ToString("yyyy-MM-ddTHH:mm:ss"));
            }

            if (value is IEnumerable<object> enumerableValue)
            {
                var encodedItems = enumerableValue.Select(item => HttpUtility.UrlEncode(item.ToString()));
                return string.Join("&", encodedItems);
            }

            return HttpUtility.UrlEncode(value.ToString());
        }

        private static string GetEncodedName(PropertyInfo property)
        {
            return HttpUtility.UrlEncode(property.Name);
        }


        public static bool IsNotNullOrEmpty(this string value)
        {
            return value == null ? false : !string.IsNullOrEmpty(value.Trim());
        }
        public static bool IsNullOrEmpty(this string value)
        {
            return !value.IsNotNullOrEmpty();
        }
        #endregion

        #region Number 
        public static int ToInt(this object value)
        {
            if (value == null)
                throw new Exception("Unable to convert null to int.");
            int? nullable = ToIntN(value);
            if (nullable.HasValue)
                return nullable.Value;
            throw new Exception(string.Format("Unable to convert {0} to int.", value));
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