using cLibrary.models.Base;

namespace cLibrary.models.Messages
{
    internal class InfoMessage : MessageModel
    {
        public InfoMessage(string detail = "")
        {
            Severity = "info";
            Summary = "Info";
            Detail = detail;
        }
    }
}
