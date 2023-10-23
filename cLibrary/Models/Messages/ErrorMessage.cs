using cLibrary.Models.Enums;

namespace cLibrary.Models.Messages
{
    public class ErrorMessage : MessageModel
    {
        public ErrorMessage(string detail = "")
        {
            Severity = LogSeverity.Error.Severity;
            Summary = LogSeverity.Error.Summary;
            Detail = detail;
        }
    }
}
