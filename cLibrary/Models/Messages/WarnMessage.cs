using cLibrary.Models.Base;
using cLibrary.Models.Enums;

namespace cLibrary.Models.Messages
{
    public class WarnMessage : MessageModel
    {
        public WarnMessage(string detail = "")
        {
            Severity = LogSeverity.Warning.Severity;
            Summary = LogSeverity.Warning.Summary;
            Detail = detail;
        }
    }
}
