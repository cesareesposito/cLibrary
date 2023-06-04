using cLibrary.Models.Base;
using cLibrary.Models.Enums;

namespace cLibrary.Models.Messages
{
    public class SuccessMessage : MessageModel
    {
        public SuccessMessage(string detail = "")
        {
            Severity = LogSeverity.Success.Severity;
            Summary = LogSeverity.Success.Summary;
            Detail = detail;
        }
    }
}
