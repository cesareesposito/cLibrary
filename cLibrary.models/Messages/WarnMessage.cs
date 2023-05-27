using cLibrary.models.Base;

namespace cLibrary.models.Messages
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
