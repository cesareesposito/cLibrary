using cLibrary.Models.Base;

namespace cLibrary.Models.Messages
{
    public class WarnMessage : MessageModel
    {
        public WarnMessage(string detail = "")
        {
            Severity = "warn";
            Summary = "Attenzione";
            Detail = detail;
        }
    }
}
