using cLibrary.Models.Enums;

namespace cLibrary.Models
{
    [Serializable]
    public class MessageModel
    {
        public string Severity { get; protected set; } = LogSeverity.Success.Severity;
        public string Summary { get; protected set; } = LogSeverity.Success.Summary;

        private string _deatail = DefaultMessages.DefaultMessage;
        public string Detail
        {
            get
            {
                if (!_deatail.EndsWith(Environment.NewLine))
                    _deatail += Environment.NewLine;
                return _deatail;
            }
            set
            {
                if (!value.EndsWith(Environment.NewLine))
                    value += Environment.NewLine;
                _deatail = value;
            }
        }
    }

    public static class DefaultMessages
    {
        public static string DefaultErrorMessage = "An error occurred while processing your request.";
        public static string DefaultWarningMessage = "The operation did not produce any changes.";
        public static string DefaultMessage = "Operation success.";
        public static string NotFound = "Element not found.";
        public static string Exist = "This already exists.";
    }
}
