using cLibrary.Models.Enums;

namespace cLibrary.Models.Messages
{
    public class InfoMessage : MessageModel
    {
        public InfoMessage(string detail = "")
        {
            Severity = LogSeverity.Info.Severity;
            Summary = LogSeverity.Info.Summary;
            Detail = detail;
        }
    }
}
