namespace cLibrary.Models.Attributes
{
    public class StringValueAttribute : Attribute
    {
        #region Properties
        public string Value { get; protected set; }
        #endregion

        #region Constructor
        public StringValueAttribute(string value)
        {
            this.Value = value;
        }
        #endregion
    }
}
