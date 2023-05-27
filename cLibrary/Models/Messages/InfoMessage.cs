using cLibrary.Models.Base;

namespace cLibrary.Models.Messages
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
