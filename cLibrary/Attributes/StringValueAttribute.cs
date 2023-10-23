namespace cLibrary.Attributes
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
}
