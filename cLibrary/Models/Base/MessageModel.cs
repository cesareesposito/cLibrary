using cLibrary.Models.Enums;

namespace cLibrary.Models.Base
{
    [Serializable]
    public class MessageModel
    {
        public string Severity { get; protected set; } = LogSeverity.Success.Severity;
        public string Summary { get; protected set; } = LogSeverity.Success.Summary;

        private string _deatail = OperationResult.DefaultMessage;
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
}
