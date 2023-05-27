using cLibrary.Models.Base;

namespace cLibrary.Models.Messages
{
    public class ErrorMessage : MessageModel
    {
        public ErrorMessage(string detail = "")
        {
            Severity = "error";
            Summary = "Errore";
            Detail = detail;
        }
    }
}
