using cLibrary.models.Base;

namespace cLibrary.models.Messages
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
