using System.Collections;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace cLibrary.Helper
{
    public class cProConvert
    {
        public static bool IsNull(object obj)
        {
            return obj == null || obj == DBNull.Value;
        }

        #region Funzioni INT32
        public static class cInt
        {
            public static int Parse(object Value)
            {
                return _ParseN(Value, 0).Value;
            }
            public static int Parse(object Value, int DefaultValue)
            {
                return _ParseN(Value, DefaultValue).Value;
            }
            public static int? ParseN(object Value)
            {
                return _ParseN(Value, null);
            }
            internal static int? _ParseN(object Value, int? DefaultValue)
            {
                if (cProConvert.IsNull(Value)) return DefaultValue;
                try
                {
                    int x;
                    if (int.TryParse(Value.ToString(), out x))
                        return x;
                    else
                        return DefaultValue;
                }
                catch { return DefaultValue; }
            }
        }

        public static int ToInt(object intValue)
        { return cInt.Parse(intValue); }
        public static int ToInt(object intValue, int DefaultValue)
        { return cInt.Parse(intValue, DefaultValue); }
        public static int? ToIntN(object intValue)
        { return cInt.ParseN(intValue); }

        /// <summary>
        /// Convert the value to Int and then to String, useful for getting enum IDs in string format.
        /// </summary>
        /// <param name="intValue"></param>
        /// <returns>Return string.Empty if intValue is null or not convertible to int.</returns>
        public static string ToIntStr(object intValue)
        {
            return ToIntStr(intValue, null);
        }
        /// <summary> Convert the value to Int and then to String, useful for getting enum IDs in string format. </summary>
        public static string ToIntStr(object intValue, int? DefaultValue)
        {
            var iValue = cInt._ParseN(intValue, DefaultValue);
            return iValue.HasValue ? iValue.Value.ToString() : string.Empty;
        }
        #endregion

        #region Funzioni Float
        public static class cFloat
        {
            public static float Parse(object Value)
            {
                return Convert.ToSingle(cDouble._ParseN(Value, 0).Value);
            }
            public static float Parse(object Value, double DefaultValue)
            {
                return Convert.ToSingle(cDouble._ParseN(Value, DefaultValue).Value);
            }
            public static float? ParseN(object Value)
            {
                double? dbl = cDouble._ParseN(Value, null);
                return dbl.HasValue ? Convert.ToSingle(dbl.Value) : null as float?;
            }
        }
        #endregion

        #region Funzioni Double
        public static class cDouble
        {
            public static double Parse(object Value)
            {
                return _ParseN(Value, 0).Value;
            }
            public static double Parse(object Value, double DefaultValue)
            {
                return _ParseN(Value, DefaultValue).Value;
            }
            public static double? ParseN(object Value)
            {
                return _ParseN(Value, null);
            }

            internal static double? _ParseN(object Value, double? defaultValue)
            {
                if (cProConvert.IsNull(Value)) return defaultValue;
                if (!(Value is string))
                    if (cProConvert.Type.TypeIsNumber(Value))
                    {
                        return Convert.ToDouble(Value);
                    }
                try
                {
                    string sValue = Value.ToString();
                    if (sValue.Trim() != "")
                    {
                        NumberFormatInfo format;
                        int virgole = cProConvert.String.CountOf(sValue, ",", false);
                        if (virgole == 0 || virgole > 1)
                        {
                            format = NumberFormats.Decimali99_SeparatorePunto_MigliaiaVirgola;
                        }
                        else
                        {
                            int punti = cProConvert.String.CountOf(sValue, ".", false);
                            if (virgole == 1 && (punti == 0 || punti > 1))
                                format = NumberFormats.Decimali99_SeparatoreVirgola_MigliaiaPunto;
                            else
                                format = NumberFormats.Decimali99_SeparatorePunto_MigliaiaVirgola;
                        }
                        return Convert.ToDouble(sValue, format);
                    }
                }
                catch { }
                return defaultValue;
            }

            public static string ToString(object Value, int NumeroDecimali, string SeparatoreDecimali, string SeparatoreMigliaia)
            { return ToString(Parse(Value), NumeroDecimali, SeparatoreDecimali, SeparatoreMigliaia); }
            public static string ToString(double Value, int NumeroDecimali, string SeparatoreDecimali, string SeparatoreMigliaia)
            {
                var format = NumberFormats.GetNumberFormat(NumeroDecimali, SeparatoreDecimali, SeparatoreMigliaia);
                return Value.ToString(format);
            }
        }

        public static double ToDouble(object DblValue)
        { return cDouble.Parse(DblValue); }
        public static double ToDouble(object DblValue, double DefaultValue)
        { return cDouble.Parse(DblValue, DefaultValue); }
        public static double? ToDoubleN(object DblValue)
        { return cDouble.ParseN(DblValue); }

        #endregion

        public static class NumberFormats
        {

            public static NumberFormatInfo GetNumberFormat(int NumeroDecimali, string SeparatoreDecimali, string SeparatoreMigliaia)
            {
                if (NumeroDecimali == 99)
                {
                    if (SeparatoreDecimali == "," && SeparatoreMigliaia == "")
                        return Decimali99_SeparatoreVirgola_MigliaiaVuoto;
                    else if (SeparatoreDecimali == "," && SeparatoreMigliaia == ".")
                        return Decimali99_SeparatoreVirgola_MigliaiaPunto;
                    else if (SeparatoreDecimali == "." && SeparatoreMigliaia == "")
                        return Decimali99_SeparatorePunto_MigliaiaVuoto;
                    else if (SeparatoreDecimali == "." && SeparatoreMigliaia == ",")
                        return Decimali99_SeparatorePunto_MigliaiaVirgola;
                }
                return _GetNewNumberFormat(NumeroDecimali, SeparatoreDecimali, SeparatoreMigliaia);
            }

            internal static System.Globalization.NumberFormatInfo _GetNewNumberFormat(int NumeroDecimali, string SeparatoreDecimali, string SeparatoreMigliaia)
            {
                var format = CultureInfo.InvariantCulture.NumberFormat.Clone() as NumberFormatInfo;
                format.NumberDecimalDigits = NumeroDecimali;
                format.NumberDecimalSeparator = SeparatoreDecimali;
                format.NumberGroupSeparator = SeparatoreMigliaia;
                return format;
            }
            //private static bool Inizialized;
            //public static void Inizializza()
            //{
            //    if (Inizialized) return;
            //    Inizialized = true;
            //    Decimali99_SeparatoreVirgola_MigliaiaVuoto = _GetNewNumberFormat(99, ",", "");
            //    Decimali99_SeparatoreVirgola_MigliaiaPunto = _GetNewNumberFormat(99, ",", ".");
            //    Decimali99_SeparatorePunto_MigliaiaVuoto = _GetNewNumberFormat(99, ".", "");
            //    Decimali99_SeparatorePunto_MigliaiaVirgola = _GetNewNumberFormat(99, ".", ",");
            //}
            public static NumberFormatInfo Decimali99_SeparatoreVirgola_MigliaiaVuoto = _GetNewNumberFormat(99, ",", "");//  { get; private set; }
            public static NumberFormatInfo Decimali99_SeparatoreVirgola_MigliaiaPunto = _GetNewNumberFormat(99, ",", ".");
            public static NumberFormatInfo Decimali99_SeparatorePunto_MigliaiaVirgola = _GetNewNumberFormat(99, ".", "");
            public static NumberFormatInfo Decimali99_SeparatorePunto_MigliaiaVuoto = _GetNewNumberFormat(99, ".", ",");
        }

        #region Funzioni Bool

        public static class cBool
        {
            #region PARSING
            public static bool Parse(object Value)
            {
                return ParseN(Value, false).Value;
            }
            public static bool Parse(object Value, bool DefaultValue)
            {
                return ParseN(Value, DefaultValue).Value;
            }
            public static bool Parse(bool? Value, bool DefaultValue)
            {
                return !Value.HasValue ? DefaultValue : Value.Value;
            }
            public static bool? ParseN(object Value)
            {
                return ParseN(Value, null);
            }
            public static bool? ParseN(object Value, bool? DefaultValue)
            {
                if (cProConvert.IsNull(Value))
                    return DefaultValue;
                if (Value is bool)
                    return (bool)Value;
                if (cProConvert.Type.TypeIsNumber(Value))
                {
                    int i = Convert.ToInt32(Value);
                    if (i == 0) return false;
                    if (i == 1 || i == -1) return true;
                    return DefaultValue;
                }
                try
                {
                    string s = Value.ToString().Trim();
                    if (s != "")
                    {
                        s = String.ToLowerFast(s);
                        if (s == "1" || s == "-1" || s == "si" || s == "s" || s == "true" || s == "t" || s == "yes" || s == "y")
                            return true;
                        else if (s == "0" || s == "no" || s == "n" || s == "false" || s == "f")
                            return false;
                    }
                }
                catch { }
                return DefaultValue;
            }


            #endregion
            public static string ToString(object bObj, string stringFormat)
            { return ToString(ParseN(bObj), stringFormat); }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bObj"></param>
            /// <param name="stringFormat">deve essere una stringa del formato: "Si;No;Niente" oppure "Si;No"</param>
            /// <returns></returns>
            public static string ToString(bool? bObj, string stringFormat)
            {
                if (stringFormat.Contains(";"))
                {
                    string[] fBool = stringFormat.Split(';');
                    if (bObj.HasValue && bObj == true)
                        return fBool[0];
                    else if (bObj.HasValue && bObj == false)
                        return fBool[1];
                    else if (!bObj.HasValue)
                    {
                        if (fBool.Length <= 2)
                            return fBool[1];
                        else
                            return fBool[2];
                    }
                }
                return "";
            }

        }

        public static bool ToBool(object BoolValue)
        { return cBool.Parse(BoolValue); }
        public static bool ToBool(object BoolValue, bool DefaultValue)
        { return cBool.Parse(BoolValue, DefaultValue); }
        public static bool? ToBoolN(object BoolValue)
        { return cBool.ParseN(BoolValue); }
        #endregion

        public static class cInt64
        {
            public static long Parse(object Value)
            {
                return _ParseN(Value, 0).Value;
            }
            public static long Parse(object Value, long DefaultValue)
            {
                return _ParseN(Value, DefaultValue).Value;
            }
            public static long? ParseN(object Value)
            {
                return _ParseN(Value, null);
            }
            private static long? _ParseN(object Value, long? DefaultValue)
            {
                if (cProConvert.IsNull(Value)) return DefaultValue;
                try { return Convert.ToInt64(Value); }
                catch { return DefaultValue; }
            }
        }

        public static class String
        {
            public static void SplitFull(string strToSplit, char[] charsForSplit, out string[] arrayWithWords, out string[] arrayWithSplitValues)
            {
                var lstWithWords = new List<string>(); var lstSplitValues = new List<string>(); bool? lastCharIsSplitter = null;
                for (int i = 0; i < strToSplit.Length; i++)
                {
                    string currStrChar = strToSplit[i].ToString();
                    bool charIsSplitter = Array.IndexOf<char>(charsForSplit, strToSplit[i]) > -1;
                    if (!lastCharIsSplitter.HasValue || lastCharIsSplitter != charIsSplitter)
                    { lstWithWords.Add(null); lstSplitValues.Add(null); }
                    lastCharIsSplitter = charIsSplitter;
                    var lst = (charIsSplitter) ? lstSplitValues : lstWithWords;
                    lst[lst.Count - 1] += currStrChar;
                }
                arrayWithWords = lstWithWords.ToArray(); arrayWithSplitValues = lstSplitValues.ToArray();
            }
            public static void SplitFull(string strToSplit, string[] stringsForSplit, StringComparison splitComparition, out string[] arrayWithWords, out string[] arrayWithSplitValues)
            {
                var lstWithWords = new List<string>(); var lstSplitValues = new List<string>(); bool? lastCharIsSplitter = null;
                for (int i = 0; i < strToSplit.Length; i++)
                {
                    string currStrChar = strToSplit[i].ToString();
                    bool charIsSplitter = false; //bool charIsSplitter = Array.IndexOf<char>(charsForSplit, strToSplit[i]) > -1;                    
                    foreach (string strSplit in stringsForSplit)
                    {
                        if (strSplit.Length <= 1)
                        {
                            if (string.Equals(currStrChar, strSplit, splitComparition))
                            { charIsSplitter = true; break; }
                        }
                        else
                        {
                            if (strSplit.Length + i <= strToSplit.Length)
                            {
                                string strSuccessiva = strToSplit.Substring(i, strSplit.Length);
                                if (string.Equals(strSuccessiva, strSplit, splitComparition))
                                {
                                    i += strSplit.Length - 1;
                                    currStrChar = strSuccessiva;
                                    charIsSplitter = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!lastCharIsSplitter.HasValue || lastCharIsSplitter != charIsSplitter)
                    { lstWithWords.Add(null); lstSplitValues.Add(null); }
                    lastCharIsSplitter = charIsSplitter;
                    var lst = (charIsSplitter) ? lstSplitValues : lstWithWords;
                    lst[lst.Count - 1] += currStrChar;
                }
                arrayWithWords = lstWithWords.ToArray(); arrayWithSplitValues = lstSplitValues.ToArray();
            }

            public static bool EqualsInvariant(object obj1, object obj2)
            {
                string s1 = obj1 == null ? null : obj1.ToString();
                string s2 = obj2 == null ? null : obj2.ToString();
                return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
            }
            /// <summary>
            /// Inserisce gli spazi prima degli upper case: ProvaDiEsempio -> "Prova Di Esempio"
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public static string AddSpacesToSentence(string text)
            {
                if (string.IsNullOrEmpty(text))
                    return "";
                StringBuilder newText = new StringBuilder(text.Length * 2);
                newText.Append(text[0]);
                for (int i = 1; i < text.Length; i++)
                {
                    if (char.IsUpper(text[i]) && text[i - 1] != ' ' && text[i - 1] != '\'')
                        newText.Append(' ');
                    newText.Append(text[i]);
                }
                return newText.ToString();
            }

            public static string Camelize(string str)
            {
                if (String.IsNullOrEmpty(str)) return "";

                char[] chars = str.ToLower().ToCharArray();
                for (int i = 0; i < chars.Length; ++i)
                {
                    if (i == 0 || //First char 
                        chars[i - 1] == ' ' ||
                        chars[i - 1] == '-' ||
                        chars[i - 1] == '.' ||
                        chars[i - 1] == '\'' ||
                        chars[i - 1] == '/'
                        )
                    {
                        chars[i] = Char.ToUpper(chars[i]);
                    }
                }
                return new string(chars);
            }


            public static string TrimAndLower(string str)
            {
                if (str == null)
                {
                    return null;
                }

                int i = 0;
                int j = str.Length - 1;
                StringBuilder sb;

                while (i < str.Length)
                {
                    if (Char.IsWhiteSpace(str[i])) // or say "if (str[i] == ' ')" if you only care about spaces
                    {
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }

                while (j > i)
                {
                    if (Char.IsWhiteSpace(str[j])) // or say "if (str[j] == ' ')" if you only care about spaces
                    {
                        j--;
                    }
                    else
                    {
                        break;
                    }
                }

                if (i > j)
                {
                    return "";
                }

                sb = new StringBuilder(j - i + 1);

                while (i <= j)
                {
                    // I was originally check for IsUpper before calling ToLower, probably not needed
                    sb.Append(Char.ToLower(str[i]));
                    i++;
                }

                return sb.ToString();
            }

            public static string ToLowerFast(string value)
            {
                char[] output = value.ToCharArray();
                for (int i = 0; i < output.Length; i++)
                {
                    if (output[i] >= 'A' && output[i] <= 'Z')
                    {
                        output[i] = (char)(output[i] + 32);
                    }
                }
                return new string(output);
            }
            private static Regex _StarGetRegEx(string stringToSearch, bool ignoreCase)
            {
                string allChars = @".*?";
                string start = ""; string end = "";
                if (stringToSearch.StartsWith("*"))
                { start = ""; stringToSearch = stringToSearch.Substring(1); }
                else
                { start = "^"; }
                if (stringToSearch.EndsWith("*"))
                { end = ""; stringToSearch = stringToSearch.Substring(0, stringToSearch.Length - 1); }
                else
                { end = "$"; }
                string regExString = start + stringToSearch.Replace(allChars, "*").Replace("*", allChars) + end;
                if (ignoreCase)
                    return new Regex(regExString, RegexOptions.IgnoreCase);
                else
                    return new Regex(regExString);
            }

            public static Match StarMatch(string stringaWhereSearch, string stringToSearch, bool ignoreCase)
            {
                var reg = _StarGetRegEx(stringToSearch, ignoreCase);
                var result = reg.Match(stringaWhereSearch);
                return result;
            }
            public static MatchCollection StarMatchs(string stringaWhereSearch, string stringToSearch, bool ignoreCase)
            {
                var reg = _StarGetRegEx(stringToSearch, ignoreCase);
                var result = reg.Matches(stringaWhereSearch);
                return result;
            }
            public static string BytesToString(byte[] byteArray)
            { return BytesToString(byteArray, null); }
            public static string BytesToString(byte[] byteArray, Encoding defaultEncoding)
            { return BytesToString(byteArray, ref defaultEncoding); }
            public static string BytesToString(byte[] byteArray, ref Encoding defaultEncoding)
            { return StreamToString(new MemoryStream(byteArray), ref defaultEncoding, true); }

            public static string StreamToString(Stream strm, Encoding defaultEncoding, bool CloseStream)
            { return StreamToString(strm, ref defaultEncoding, CloseStream); }
            public static string StreamToString(Stream inputStream, ref Encoding defaultEncoding, bool CloseStream)
            {
                if (inputStream == null) return null;
                if (defaultEncoding == null) defaultEncoding = Encoding.UTF8;
                string contents;
                StreamReader reader = new StreamReader(inputStream, defaultEncoding, true);
                contents = reader.ReadToEnd();
                defaultEncoding = reader.CurrentEncoding;
                if (CloseStream)
                { inputStream.Close(); inputStream.Dispose(); }
                return contents;
            }
            public static string Substring(string stringaFull, string stringaPartenza, string stringaFine, bool IgnoreCase)
            {
                StringComparison comp = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                int indx = stringaFull.IndexOf(stringaPartenza, comp);
                if (indx == -1) return "";
                indx += stringaPartenza.Length;
                int indxFine = stringaFull.IndexOf(stringaFine, indx, comp);
                if (indxFine == -1) return "";
                string subStr = stringaFull.Substring(indx, indxFine - indx);
                return subStr;
            }
            public static string Trim(object stringa)
            { return Parse(stringa).Trim(); }
            public static string Parse(object stringa)
            {
                if (cProConvert.IsNull(stringa)) return "";
                return stringa.ToString();
            }
            public static string ParseN(object stringa)
            {
                if (IsNullOrEmpty(stringa)) return null;
                return stringa.ToString();
            }
            public static string ReplaceIgnCase(string originalString, string oldValue, string newValue)
            { return Replace(originalString, oldValue, newValue, StringComparison.OrdinalIgnoreCase); }
            public static string Replace(string originalString, string oldValue, string newValue, StringComparison comparisonType)
            {
                int startIndex = 0;
                while (true)
                {
                    startIndex = (originalString + "").IndexOf(oldValue, startIndex, comparisonType);
                    if (startIndex == -1)
                        break;

                    originalString = originalString.Substring(0, startIndex) + newValue + originalString.Substring(startIndex + oldValue.Length);

                    startIndex += newValue.Length;
                }
                return originalString;
            }
            public static string Left(object stringa, int numeroCaratteri)
            {
                if (cProConvert.IsNull(stringa)) return null;
                string str = stringa.ToString();
                if (str.Length > numeroCaratteri)
                    str = str.Substring(0, numeroCaratteri);
                return str;
            }
            public static string Right(object stringa, int numeroCaratteri)
            {
                if (cProConvert.IsNull(stringa)) return null;
                string str = stringa.ToString();
                if (str.Length > numeroCaratteri)
                    str = str.Substring(str.Length - numeroCaratteri, numeroCaratteri);
                return str;
            }

            public static bool IsNullOrEmpty(object str)
            {
                return cProConvert.IsNull(str) || (str is string && str.ToString() == string.Empty);
            }
            public static bool IsNullOrEmptyTrim(object str)
            {
                return cProConvert.IsNull(str) || (str is string && str.ToString().Trim() == string.Empty);
            }
            public static int CountOf(string StringaFull, string stringaToSearch, bool IgnoreCase)
            {
                StringComparison comparer = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                int indx = (StringaFull + "").IndexOf(stringaToSearch, comparer);
                if (indx == -1)
                    return 0;
                int count = 0;
                int pos = -1;
                while ((pos = (StringaFull + "").IndexOf(stringaToSearch, pos + 1, comparer)) > -1) count++;
                return count;
            }
            public static bool GetIndexOfConsecutivi(string stringaFull, string StringIndexOfStart, string StringIndexOfEnd, out int IndexOfStart, out int IndexOfEnd)
            {
                IndexOfStart = -1; IndexOfEnd = -1;
                try
                {
                    IndexOfStart = stringaFull.IndexOf(StringIndexOfStart);
                    if (IndexOfStart == -1) return false;
                    IndexOfEnd = stringaFull.IndexOf(StringIndexOfEnd, IndexOfStart + 1);
                    if (IndexOfEnd == -1) return false;
                    return true;
                }
                catch { return false; }
            }


            //private static System.Globalization.NumberFormatInfo _ToJsStrFormat;
            public static string ToJsStr(double ob)
            {
                return ob.ToString(NumberFormats.Decimali99_SeparatorePunto_MigliaiaVuoto);
            }
            public static string ToJsStr(System.DateTime dt)
            {
                return string.Format("new Date({0}, {1}, {2})", dt.Year, dt.Month - 1, dt.Day);
            }
            public static string ToJsStr(bool ob)
            {
                return ((bool)ob) ? "true" : "false";
            }
            public static string ToJsStr(object ob)
            {
                if (cProConvert.IsNull(ob))
                    return "null";
                if (ob is System.DateTime)
                    return ToJsStr((System.DateTime)ob);
                else if (ob is bool)
                    return ((bool)ob) ? "true" : "false";
                else if (ob is string)
                    return "'" + ob.ToString().Replace("'", @"\'") + "'";
                else if (cProConvert.Type.TypeIsNumber(ob))
                    return ToJsStr(Convert.ToDouble(ob));
                else if (ob is IEnumerable)
                {
                    List<string> objects = new List<string>();
                    foreach (object objc in (ob as IEnumerable))
                        objects.Add(ToJsStr(objc));
                    return "[" + string.Join(",", objects.ToArray()) + "]";
                }
                return ob.ToString();
            }


            /// <summary>
            /// Permette di splittare una stringa come connectionstring in namevalues, ad esempio: p1=prova;p2=xxx;p3=ciao
            /// </summary>
            /// <param name="stringa"></param>
            /// <returns></returns>
            public static System.Collections.Specialized.NameValueCollection StringToNameValueCollection(string stringa)
            {
                return StringToNameValueCollection(stringa, ";", "=");
            }

            /// <summary>
            /// Permette di splittare una stringa: ad esempio: p1=prova;p2=xxx;p3=ciao settando separator: ';' e NameValueSeparator: '='
            /// </summary>
            /// <param name="stringa"></param>
            /// <param name="separator"></param>
            /// <param name="NameValueSeparator"></param>
            /// <returns></returns>
            public static System.Collections.Specialized.NameValueCollection StringToNameValueCollection(string stringa, string separator, string NameValueSeparator)
            {
                var vCollection = new System.Collections.Specialized.NameValueCollection();
                if (string.IsNullOrEmpty(stringa))
                    return vCollection;
                string[] aryStrings = stringa.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                string[] nameAndValue;
                foreach (string s in aryStrings)
                {
                    nameAndValue = s.Split(new string[] { NameValueSeparator }, StringSplitOptions.None);
                    vCollection.Add(nameAndValue[0], nameAndValue.Length > 1 ? nameAndValue[1] : "");
                }
                return vCollection;
            }


            public static string EscapeLikeValueForSqlQuery(string value)
            {
                StringBuilder sb = new StringBuilder(value.Length);
                for (int i = 0; i < value.Length; i++)
                {
                    char c = value[i];
                    switch (c)
                    {
                        case ']':
                        case '[':
                        case '%':
                        case '*':
                            sb.Append("[").Append(c).Append("]");
                            break;
                        case '\'':
                            sb.Append("''");
                            break;
                        default:
                            sb.Append(c);
                            break;
                    }
                }
                return sb.ToString();
            }

            /// <summary>
            /// sostituisce i valori accentati con le lettere non accentate à->a, È->E.
            /// </summary>
            /// <param name="value"></param>
            /// <param name="stringToConcatIfFindedAccentato">Valore da concatenare alla lettera sostituita, ad esempio se si passa apice, si avrà à->a'</param>
            /// <returns></returns>
            public static string ReplaceCharAccentati(string value, string stringToConcatIfFindedAccentato)
            {
                StringBuilder sb = new StringBuilder(value.Length);
                for (int i = 0; i < value.Length; i++)
                {
                    char newChar = value[i];
                    switch (newChar)
                    {
                        case 'à':
                            newChar = 'a'; break;
                        case 'Á':
                        case 'À':
                            newChar = 'A'; break;
                        case 'è':
                        case 'é':
                            newChar = 'e'; break;
                        case 'È':
                        case 'É':
                            newChar = 'E'; break;
                        case 'ì':
                            newChar = 'i'; break;
                        case 'Ì':
                        case 'Í':
                            newChar = 'I'; break;
                        case 'ò':
                            newChar = 'o'; break;
                        case 'Ò':
                        case 'Ó':
                            newChar = 'O'; break;
                        case 'ù':
                            newChar = 'u'; break;
                        case 'Ù':
                        case 'Ú':
                            newChar = 'U'; break;
                        default:
                            sb.Append(newChar); continue;
                    }
                    sb.Append(newChar).Append(stringToConcatIfFindedAccentato + "");
                }
                return sb.ToString();
            }


        }

        public static class Type
        {
            public static TipoOut GetTypeCastedOrDefault<TipoOut>(object valueToCast)
            {
                if (valueToCast != null)
                    try { return (TipoOut)valueToCast; }
                    catch { }
                return default(TipoOut);
            }
            public static System.Type GetTypeOfNullable(System.Type TipoOrNullableType)
            {
                try
                {
                    System.Type tipoOfNullable = Nullable.GetUnderlyingType(TipoOrNullableType);
                    return tipoOfNullable == null ? TipoOrNullableType : tipoOfNullable;
                }
                catch { }
                return null;
            }
            public static bool TypeIsNumber(object obj)
            {
                if (obj is Type)
                    return TypeIsNumber(obj as Type);
                if (obj is int || obj is short || obj is long || obj is decimal || obj is double || obj is float)
                    return true;
                return false;
            }
            public static bool TypeIsNumber(System.Type t)
            {
                if (t != null)
                {
                    if (t == typeof(int) || t == typeof(short) || t == typeof(long) || t == typeof(decimal) || t == typeof(double) || t == typeof(float))
                        return true;
                }
                return false;
            }
            public static bool TypeIsNullable(System.Type tipo)
            {
                return (tipo.IsGenericType && tipo.GetGenericTypeDefinition() == typeof(Nullable<>));
            }
            public static bool TypeIsList(System.Type tipo)
            {
                return (tipo.IsGenericType && tipo.GetGenericTypeDefinition() == typeof(List<>));
            }
            public static System.Type GetTypeOfList(System.Type Lista, System.Type DefaultValueOnError)
            {
                if (!TypeIsList(Lista))
                    return DefaultValueOnError;
                try { return Lista.GetGenericArguments()[0]; }
                catch { return DefaultValueOnError; }
            }
            public static object CreateInstanceFromType(System.Type ObjectTypeToCloneAndCrateInstance)
            {
                object result = null; //grab the type and create a new instance of that type 
                if (ObjectTypeToCloneAndCrateInstance.IsGenericType)
                {
                    System.Type typeDef = ObjectTypeToCloneAndCrateInstance.GetGenericTypeDefinition();
                    if (typeDef != typeof(Nullable<>))
                        result = CreateInstanceOfGenericType(typeDef, ObjectTypeToCloneAndCrateInstance.GetGenericArguments());
                }
                else
                    result = Activator.CreateInstance(ObjectTypeToCloneAndCrateInstance);
                return result;
            }

            /// <summary>
            /// Usato ad esempio per creare una List di object, passando typeof(List<>) e come argoument il tipo della lista, ad esempio typeof(object)
            /// </summary>
            /// <param name="GenericTypeObject"></param>
            /// <param name="ArgoumentsOfGenericType"></param>
            /// <returns></returns>
            public static object CreateInstanceOfGenericType(System.Type GenericTypeObject, params System.Type[] ArgoumentsOfGenericType)
            {
                //Creating the Generic List.
                //Type GenericTypeObject = typeof(List<>);
                //Creating the Type for Generic List.
                //Type[] typeArgs = { tipoOggetti };
                System.Type makeme = GenericTypeObject.MakeGenericType(ArgoumentsOfGenericType);
                //Creating the Instance for List.
                object result = Activator.CreateInstance(makeme);
                return result;
            }

            public static object GetDefaultValue(System.Type Tipo)
            {
                if (Tipo == null || !Tipo.IsValueType)
                    return null;
                else if (Tipo == typeof(System.DateTime))
                    return System.DateTime.MinValue;
                else if (Tipo == typeof(bool))
                    return false;
                else if (Tipo == typeof(int))
                    return (int)0;
                else if (Tipo == typeof(double))
                    return (double)0.0d;
                else if (Tipo == typeof(float))
                    return (float)0.0f;
                else if (Tipo == typeof(decimal))
                    return (decimal)0;
                else if (Tipo == typeof(Nullable<System.DateTime>) || Tipo == typeof(Nullable<bool>)
                           || Tipo == typeof(Nullable<int>) || Tipo == typeof(Nullable<double>)
                           || Tipo == typeof(Nullable<float>) || Tipo == typeof(Nullable<decimal>))
                    return null;
                return CreateInstanceFromType(Tipo);
                //Activator.CreateInstance(Tipo);
                //var defaultGeneratorType = typeof(DefaultGenerator<>).MakeGenericType(Tipo);
                //object value = defaultGeneratorType.InvokeMember("GetDefault", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[0]);
                //return value;
            }
            private class DefaultGenerator<T>
            { public static T GetDefault() { return default(T); } }

            public static T ChangeTypeOrDefault<T>(object value)
            {
                try { return (T)ChangeType(value, typeof(T), false); }
                catch { return default(T); }
            }
            public static T ChangeType<T>(object value, T defaultValue)
            {
                try
                {
                    if (cProConvert.IsNull(value))
                        return defaultValue;
                    return (T)ChangeType(value, typeof(T), false);
                }
                catch { return defaultValue; }
            }
            public static object ChangeType(object value, System.Type conversionType, bool ForDataRow)
            {
                if (cProConvert.IsNull(value))
                    if (ForDataRow)
                        return DBNull.Value;
                    else
                        return cProConvert.Type.GetDefaultValue(conversionType);
                System.Type tipoOutput = cProConvert.Type.GetTypeOfNullable(conversionType);
                bool IsNullableOutput = tipoOutput != conversionType;
                if (tipoOutput == typeof(bool))
                {
                    bool? res = cProConvert.cBool.ParseN(value, (IsNullableOutput ? (null as bool?) : false));
                    return res;
                }
                else if (tipoOutput.IsEnum)
                {
                    object res = cProConvert.Enum.ParseN(tipoOutput, value);
                    return res;
                }
                object output = null;
                try { output = Convert.ChangeType(value, tipoOutput); }
                catch
                {
                    if (IsNullableOutput) return null;
                    throw;
                }
                return output;
            }

        }

        public static class Bytes
        {
            public static MemoryStream StreamFromBytes(byte[] inputBytes)
            {
                var ms = new MemoryStream();
                ms.Write(inputBytes, 0, inputBytes.Length);
                return ms;
            }

            public static byte[] StreamToBytes(System.IO.Stream inputStream)
            { return StreamToBytes(inputStream, false); }
            public static byte[] StreamToBytes(System.IO.Stream inputStream, bool CloseStream)
            {
                byte[] output = null;
                if (inputStream is MemoryStream)
                    output = (inputStream as MemoryStream).ToArray();
                else
                    using (MemoryStream ms = StreamToMemoryStream(inputStream, CloseStream))
                        output = ms.ToArray();
                if (CloseStream)
                { inputStream.Close(); inputStream.Dispose(); }
                return output;
            }

            public static MemoryStream StreamToMemoryStream(System.IO.Stream inputStream, bool CloseStreamOrigine)
            {
                return StreamToMemoryStream(inputStream, CloseStreamOrigine, false);
            }

            public static MemoryStream StreamToMemoryStream(System.IO.Stream inputStream, bool CloseStreamOrigine, bool CloneIfIsMemoryStream)
            {
                MemoryStream ms;
                if (!CloneIfIsMemoryStream && inputStream is MemoryStream)
                    return (inputStream as MemoryStream);
                else
                {
                    if (inputStream.CanSeek) // This is optional
                        inputStream.Seek(0, System.IO.SeekOrigin.Begin);
                    byte[] buffer = new byte[16 * 1024];
                    ms = new MemoryStream();
                    int read;
                    while ((read = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }
                if (CloseStreamOrigine)
                { inputStream.Close(); inputStream.Dispose(); }
                return ms;
            }
        }

        public static class Size
        {
            public enum eSizes
            {
                Bytes = 1,
                KB = 2,
                MB = 3,
                GB = 4,
                TB = 5,
                PB = 6,
                EB = 7
            }

            public static double? ParseN(string DimensioneOriginal, eSizes? TipoDimensioneOriginal, eSizes TipoDimensioneOutput)
            {
                if (string.IsNullOrEmpty(DimensioneOriginal))
                    return null;
                if (DimensioneOriginal.Trim() == "0")
                    return 0;
                if (!TipoDimensioneOriginal.HasValue)
                {
                    foreach (eSizes eSz in cProConvert.Enum.GetList<eSizes>())
                    {
                        string sz = (eSz == eSizes.Bytes ? "byte" : eSz.ToString().ToLower());
                        if (DimensioneOriginal.ToLower().Contains(sz.ToString().ToLower()))
                        { TipoDimensioneOriginal = eSz; break; }
                    }
                }
                if (!TipoDimensioneOriginal.HasValue)
                    return null;
                string eTipo = TipoDimensioneOriginal == eSizes.Bytes ? "byte" : TipoDimensioneOriginal.Value.ToString().ToLower();
                string sSize = DimensioneOriginal.ToLower().Replace(eTipo, "").Replace("s", "").Trim();
                try
                {
                    double dblSize = Convert.ToDouble(sSize);
                    return Parse(dblSize, TipoDimensioneOriginal.Value, TipoDimensioneOutput);
                }
                catch { return null; }
            }

            public static double Parse(double DimensioneOriginal, eSizes TipoDimensioneOriginal, eSizes TipoDimensioneOutput)
            {
                int Diff = Convert.ToInt32(TipoDimensioneOriginal) - Convert.ToInt32(TipoDimensioneOutput);
                double Fattore = (Diff < 0) ? 1024.0 : (1.0 / 1024.0);

                for (int i = 0; i <= Math.Abs(Diff) - 1; i++)
                {
                    DimensioneOriginal = DimensioneOriginal / Fattore;
                }

                int Digits = -1;
                string Dimens = DimensioneOriginal.ToString();
                if (Digits == -1)
                    Digits = Dimens.IndexOf(".");
                if (Digits == -1)
                    Digits = Dimens.IndexOf(",");
                if (Digits > -1)
                {
                    Dimens = Dimens.Substring(Digits + 1);
                    for (int i = 0; i <= Dimens.Length - 1; i++)
                    {
                        if (Dimens[i] != '0')
                        {
                            Digits = i + 2; break;
                        }
                    }
                }
                else
                {
                    Digits = 2;
                }
                DimensioneOriginal = Math.Round(DimensioneOriginal, Digits);
                return DimensioneOriginal;
            }

            private static Dictionary<string, string> ByteConvertAutoDict = new Dictionary<string, string>();
            public static string ToString(string DimensioneOriginal, eSizes? TipoDimensioneOriginal)
            {
                double? dblSize = ParseN(DimensioneOriginal, TipoDimensioneOriginal, eSizes.KB);
                if (!dblSize.HasValue) return "";
                return ToString(dblSize.Value, eSizes.KB);
            }
            public static string ToString(double DimensioneOriginal, eSizes TipoDimensioneOriginal)
            {
                string KeyDict = DimensioneOriginal.ToString() + "#" + Convert.ToInt32(TipoDimensioneOriginal);
                if (ByteConvertAutoDict.ContainsKey(KeyDict))
                    return ByteConvertAutoDict[KeyDict];
                for (int cnt = 0; cnt < 10; cnt++)
                {
                    if (DimensioneOriginal >= 1000 && DimensioneOriginal <= 1024 && TipoDimensioneOriginal != eSizes.EB)
                    {
                        //DimensioneOriginal /= 1024
                        DimensioneOriginal = 1;
                        // + (DimensioneOriginal - Math.Truncate(DimensioneOriginal))
                        TipoDimensioneOriginal += 1;
                        break;
                    }
                    else if (DimensioneOriginal < 0.995 && TipoDimensioneOriginal != eSizes.Bytes)
                    {
                        DimensioneOriginal *= 1024;
                        TipoDimensioneOriginal -= 1;
                    }
                    else if (DimensioneOriginal > 1024 && TipoDimensioneOriginal != eSizes.EB)
                    {
                        DimensioneOriginal /= 1024;
                        TipoDimensioneOriginal += 1;
                    }
                    else
                    {
                        break;
                    }
                }
                DimensioneOriginal = Math.Round(DimensioneOriginal, 2);
                string Res = DimensioneOriginal.ToString() + " " + TipoDimensioneOriginal.ToString();
                try { ByteConvertAutoDict.Add(KeyDict, Res); }
                catch { }
                return Res;
            }
        }

        #region Funzioni DateTime
        public static class DateTime
        {
            public static int CalculateAge(System.DateTime birthDate)
            {
                System.DateTime now = System.DateTime.Now;
                int age = now.Year - birthDate.Year;
                if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                    age--;
                return age;
            }
            public static System.DateTime? FromGmt(object DateValue)
            {
                return Parse(DateValue, null, "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'");
            }
            public static string ToGmt(object DateValue)
            {
                return ToString(DateValue, "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'");
            }


            public static bool HaveTime(System.DateTime CurrData)
            {
                if (CurrData.Hour > 0 || CurrData.Minute > 0 || CurrData.Second > 0 || CurrData.Millisecond > 0)
                    return true;
                return false;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="DateValue"></param>
            /// <param name="FormatOutput">stringa che può essere: dd/MM/yyyy HH:mm:ss o yyyyMMdd_HHmmss (per files)</param>
            /// <returns></returns>
            public static string ToString(object DateValue, string FormatOutput)
            {
                return ToString(DateValue, FormatOutput, null, "");
            }

            /// <summary> Restituisce stringa in formato "dd/MM/yyyy", "" se la data è null </summary>
            public static string ToStringDate(object DateValue)
            {
                return ToString(DateValue, "dd/MM/yyyy");
            }
            /// <summary> Restituisce stringa in formato "dd/MM/yyyy HH:mm", "" se la data è null </summary>
            public static string ToStringDateTime(object DateValue)
            {
                return ToString(DateValue, "dd/MM/yyyy HH:mm");
            }
            /// <summary> Restituisce stringa in formato "dd/MM/yyyy HH:mm:ss", "" se la data è null </summary>
            public static string ToStringDateTimeS(object DateValue)
            {
                return ToString(DateValue, "dd/MM/yyyy HH:mm:ss");
            }

            public static string ToString(object DateValue, string FormatOutput, string MaskForRead, string ValueForNothing)
            {
                System.DateTime? dt = Parse(DateValue, null, MaskForRead);
                if (!dt.HasValue)
                    return ValueForNothing;
                return dt.Value.ToString(FormatOutput, System.Globalization.CultureInfo.InvariantCulture);
            }
            public static System.DateTime? Parse(params System.DateTime?[] DateValues)
            {
                foreach (System.DateTime? dt in DateValues)
                    if (dt.HasValue)
                        return dt;
                return null;
            }
            public static System.DateTime? Parse(object DateValue, System.DateTime? ValueForNothing, string MaskForRead)
            {
                if (cProConvert.IsNull(DateValue))
                    return ValueForNothing;
                if (DateValue is System.DateTime)
                    return (System.DateTime)DateValue;

                string s = DateValue.ToString();
                if (!string.IsNullOrEmpty(MaskForRead))
                {
                    try { return System.DateTime.ParseExact(s.Trim(), MaskForRead.Trim(), System.Globalization.CultureInfo.InvariantCulture); }
                    catch (Exception) { }
                }

                try { return System.DateTime.Parse(s.Trim(), System.Globalization.CultureInfo.GetCultureInfo(1040)); }
                catch (Exception) { }

                return ValueForNothing;
            }
        }

        public static System.DateTime ToDate(object DateValue)
        { return DateTime.Parse(DateValue, System.DateTime.MinValue, null).Value; }
        public static System.DateTime ToDate(object DateValue, string MaskForRead)
        { return DateTime.Parse(DateValue, System.DateTime.MinValue, MaskForRead).Value; }
        public static System.DateTime ToDate(object DateValue, System.DateTime DefaultValue)
        { return DateTime.Parse(DateValue, DefaultValue, null).Value; }
        public static System.DateTime? ToDateN(object DateValue)
        { return DateTime.Parse(DateValue, null, null); }
        public static System.DateTime? ToDateN(object DateValue, string MaskForRead)
        { return DateTime.Parse(DateValue, null, MaskForRead); }
        public static string ToDateStr(object DateValue, string FormatOutput)
        { return DateTime.ToString(DateValue, FormatOutput); }

        #endregion

        #region Funzioni Enum
        public static class Enum
        {
            public static System.Data.DataTable ToDataTable<T>(string IdField, string DescrField, bool SortByDescr, int[] ListToSkip) where T : struct, IConvertible
            {
                if (ListToSkip == null) ListToSkip = new int[] { };
                DataTable dt = new DataTable(typeof(T).Name);
                dt.Columns.Add(IdField, typeof(int));
                dt.Columns.Add(DescrField, typeof(string));
                Array lstEnumValues = System.Enum.GetValues(typeof(T));
                if (SortByDescr)
                {
                    SortedDictionary<string, int> valuesSorted = new SortedDictionary<string, int>();
                    foreach (object value in lstEnumValues)
                        if (Array.IndexOf(ListToSkip, Convert.ToInt32(value)) == -1)
                            valuesSorted.Add(value.ToString(), Convert.ToInt32(value));
                    foreach (string key in valuesSorted.Keys)
                    {
                        var dr = dt.NewRow();
                        dr[DescrField] = key; dr[IdField] = valuesSorted[key];
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    SortedDictionary<int, string> valuesSorted = new SortedDictionary<int, string>();
                    foreach (object value in lstEnumValues)
                        if (Array.IndexOf(ListToSkip, Convert.ToInt32(value)) == -1)
                            valuesSorted.Add(Convert.ToInt32(value), value.ToString());
                    foreach (int id in valuesSorted.Keys)
                    {
                        var dr = dt.NewRow();
                        dr[DescrField] = valuesSorted[id]; dr[IdField] = id;
                        dt.Rows.Add(dr);
                    }
                }
                return dt;
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
            public static object ParseN(System.Type tipo, object enumValue)
            {
                if (cProConvert.String.IsNullOrEmpty(enumValue))
                {
                    return null;
                }
                if (enumValue.GetType() == tipo)
                    return enumValue;
                int? iEnumValue = cProConvert.cInt.ParseN(enumValue);
                object result = null;
                if (enumValue.GetType().IsEnum) // se è un enum, faccio la ricerca prima per nome
                {
                    result = ParseNFromString(tipo, enumValue.ToString());
                    if (result == null && iEnumValue.HasValue)
                        result = ParseNFromId(tipo, iEnumValue.Value);
                }
                else
                {
                    if (iEnumValue.HasValue)
                        result = ParseNFromId(tipo, iEnumValue.Value);
                    if (result == null)
                        result = ParseNFromString(tipo, enumValue.ToString());
                }
                return result;
            }
            public static T? ParseN<T>(object enumValue, T? DefaultValue) where T : struct, IConvertible
            {
                if (cProConvert.String.IsNullOrEmpty(enumValue))
                {
                    if (!DefaultValue.HasValue) return null;
                    return GetValidValue<T>(DefaultValue.Value);
                }
                if (enumValue.GetType() == typeof(T))
                    return (T)enumValue;
                int? iEnumValue = cProConvert.cInt.ParseN(enumValue);
                T? result = null;
                if (enumValue.GetType().IsEnum) // se è un enum, faccio la ricerca prima per nome
                {
                    result = ParseNFromString<T>(enumValue.ToString());
                    if (!result.HasValue && iEnumValue.HasValue)
                        result = ParseNFromId<T>(iEnumValue.Value);
                }
                else
                {
                    if (iEnumValue.HasValue)
                        result = ParseNFromId<T>(iEnumValue.Value);
                    if (!result.HasValue)
                        result = ParseNFromString<T>(enumValue.ToString());
                }
                return result;
            }
            public static object ParseNFromId(System.Type tipo, object enumValue)
            {
                try { return System.Enum.ToObject(tipo, enumValue); }
                catch { return null; }
            }
            public static T ParseFromId<T>(object enumValue, T DefaultValue) where T : struct, IConvertible
            {
                if (cProConvert.IsNull(enumValue)) return GetValidValue<T>(DefaultValue);
                try { return (T)System.Enum.ToObject(typeof(T), Convert.ToInt32(enumValue)); }
                catch { return GetValidValue<T>(DefaultValue); }
            }
            public static T? ParseNFromId<T>(object enumValue) where T : struct, IConvertible
            {
                if (cProConvert.IsNull(enumValue)) return null;
                try
                {
                    var v = (T)System.Enum.ToObject(typeof(T), Convert.ToInt32(enumValue));
                    if (System.Enum.IsDefined(typeof(T), v)) return v;
                }
                catch { }
                return null;
            }
            public static T? ParseNFromId<T>(int enumValue) where T : struct, IConvertible
            {
                try
                {
                    var v = (T)System.Enum.ToObject(typeof(T), enumValue);
                    if (System.Enum.IsDefined(typeof(T), v)) return v;
                }
                catch { }
                return null;
            }

            public static T? ParseNFromString<T>(string enumName) where T : struct, IConvertible
            {
                try { object res = ParseNFromString(typeof(T), enumName); if (res != null) return (T)res; }
                catch { }
                return null;
            }
            public static object ParseNFromString(System.Type tipo, string enumName)
            {
                try
                {
                    string[] names = System.Enum.GetNames(tipo);
                    foreach (string st in names)
                    {
                        if (string.Equals(st, enumName, StringComparison.InvariantCultureIgnoreCase))
                            return System.Enum.Parse(tipo, st);
                    }
                }
                catch { }
                return null;
            }

            public static T GetValidValue<T>(T DefaultValue)
            {
                if (System.Enum.IsDefined(typeof(T), DefaultValue))
                    return DefaultValue;
                return default(T);
            }
        }

        public static T ToEnum<T>(object enumValue, T DefaultValue) where T : struct, IConvertible
        { return Enum.ParseN<T>(enumValue, DefaultValue).GetValueOrDefault(); }
        public static T? ToEnumN<T>(object enumValue) where T : struct, IConvertible
        { return Enum.ParseN<T>(enumValue, null); }

        #endregion

        public static class Guid
        {
            public const string GuidRegexPattern = @"(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})";

            /// <summary>
            /// Validate that a string is a valid GUID
            /// </summary>
            /// <param name="GUIDCheck"></param>
            /// <returns></returns>
            public static bool IsValidGUID(string GUIDCheck)
            {
                if (!string.IsNullOrEmpty(GUIDCheck))
                {
                    return new Regex("^" + GuidRegexPattern + "$").IsMatch(GUIDCheck);
                }
                return false;
            }
        }

        #region Funzioni Colors
        public static class Color
        {
            /// <summary>
            /// Converte un color in rgb per JavaScript: "rgb(R, G, B)"
            /// </summary>
            /// <param name="color"></param>
            /// <returns></returns>
            public static string ToJsRGB(System.Drawing.Color color)
            {
                string jscolor = string.Format("rgb({0}, {1}, {2})", color.R, color.G, color.B);
                return jscolor;
            }
            /// <summary>
            /// returns HTML-ized color strings
            /// </summary>
            public static string ToHtmlColor(System.Drawing.Color color)
            {
                if (color.IsEmpty)
                    return "";
                //if (color.IsNamedColor)
                //{
                //    return color.ToKnownColor().ToString();
                //}
                //if (color.IsSystemColor)
                //{
                //    return color.ToString();
                //}
                return "#" + color.ToArgb().ToString("x").Substring(2);
            }
        }
        #endregion
    }
}
