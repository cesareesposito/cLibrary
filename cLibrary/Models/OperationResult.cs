using cLibrary.Models.Base;
using cLibrary.Models.Messages;

namespace cLibrary.Models
{
    public class OperationResult
    {
        private const string DefaultErrorMessage = "Si è verificato un errore imprevisto nell'elaborazione della richiesta. Contattare il supporto tecnico per maggiori informazioni.";
        private const string DefaultWarningMessage = "L'operazione non ha prodotto nessuna modifica. Contattare il supporto tecnico per maggiori informazioni.";
        public const string DefaultMessage = "Operazione completata.";

        #region altri costruttori        
        public OperationResult(bool result = false)
        {
            this.Result = result;
            this.Message = Result ? new SuccessMessage()
                 : _rowWrited == 0
                 ? new WarnMessage(DefaultWarningMessage)
                 : new ErrorMessage(DefaultErrorMessage);
        }
        public OperationResult(Func<int?> saveFunc, string errorMessage = null, dynamic data = null)
        {
            try
            {
                this._rowWrited = saveFunc() ?? _rowWrited;
                this.Result = _rowWrited > 0;
                this.Message = Result ? new SuccessMessage()
                    : _rowWrited == 0
                    ? new WarnMessage(DefaultWarningMessage)
                    : new ErrorMessage(errorMessage ?? DefaultErrorMessage);
            }
            catch (Exception ex)
            {
                this.Result = false;
#if DEBUG                
                this.ExMessage = ex.InnerException?.Message ?? ex.Message;
                this.Message = new ErrorMessage(ExMessage);
#else
                this.Message = new ErrorMessage(errorMessage ?? DefaultErrorMessage);
                this.ExMessage = ex.InnerException?.Message ?? ex.Message;
#endif
            }
        }
        public OperationResult(Func<int?> saveFunc, dynamic data)
            : this(saveFunc)
        {
            this.Data = data;
            if (Result && data is MessageModel) this.Message = data;
        }
        public OperationResult(dynamic data)
           : this((bool)(data != null))
        {
            this.Data = data;
            if (Result && data is MessageModel) this.Message = data;
        }
        public OperationResult(string errorMessage, dynamic data = null)
           : this(() => -1, errorMessage)
        {
            this.Data = data;
            if (Result && data is MessageModel) this.Message = data;
        }
        public OperationResult(List<string> errorMessage, dynamic data = null)
           : this(() => -1, string.Join(Environment.NewLine, errorMessage))
        {
            this.Data = data;
            if (Result && data is MessageModel) this.Message = data;
        }
        public OperationResult(Exception ex, string errorMessage = null)
        {
            this.Result = false;
#if DEBUG
            this.ExMessage = ex.InnerException?.Message ?? ex.Message;
            this.Message = new ErrorMessage(ExMessage);
#else
                this.Message = new ErrorMessage(errorMessage ?? DefaultErrorMessage);
                this.ExMessage = ex.InnerException?.Message ?? ex.Message;
#endif
        }
        #endregion

        private int _rowWrited { get; set; } = 0;
        public bool Result { get; set; } = false;
        public dynamic Data { get; set; }
        public MessageModel Message { get; set; }
        public string ExMessage { get; set; }
    }
}
