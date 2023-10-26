using cLibrary.Models.Messages;
using System.Text.Json.Serialization;

namespace cLibrary.Models
{

    [Serializable]
    public class OperationResult
    {
        #region altri costruttori    
        [JsonConstructor]
        public OperationResult() { Message = new ErrorMessage(DefaultMessages.DefaultErrorMessage); }       

        public OperationResult(bool result = false)
        {
            this.Success = result;
            this.Message = Success ? new SuccessMessage()
                 : _rowWrited == 0
                 ? new WarnMessage(DefaultMessages.DefaultWarningMessage)
                 : new ErrorMessage(DefaultMessages.DefaultErrorMessage);
        }
        public OperationResult(Func<int?> saveFunc, string errorMessage = null, string successMessage = null, dynamic data = null)
        {
            try
            {
                this._rowWrited = saveFunc() ?? _rowWrited;
                this.Success = _rowWrited > 0;
                this.Message = Success ? new SuccessMessage() { Detail = successMessage ?? DefaultMessages.DefaultMessage }
                    : _rowWrited == 0
                    ? new WarnMessage(DefaultMessages.DefaultWarningMessage)
                    : new ErrorMessage(errorMessage ?? DefaultMessages.DefaultErrorMessage);
            }
            catch (Exception ex)
            {
                this.Success = false;
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
            if (Success && data is MessageModel) this.Message = data;
        }
        public OperationResult(dynamic data)
           : this((bool)(data != null))
        {
            this.Data = data;
            if (Success && data is MessageModel) this.Message = data;
        }
        public OperationResult(string errorMessage, dynamic data = null)
           : this(() => -1, errorMessage)
        {
            this.Data = data;
            if (Success && data is MessageModel) this.Message = data;
        }
        public OperationResult(List<string> errorMessage, dynamic data = null)
           : this(() => -1, string.Join(Environment.NewLine, errorMessage))
        {
            this.Data = data;
            if (Success && data is MessageModel) this.Message = data;
        }
        public OperationResult(Exception ex, string errorMessage = null)
        {
            this.Success = false;
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
        public bool Success { get; set; } = false;
        public dynamic Data { get; set; }
        public MessageModel Message { get; set; }
        public string ExMessage { get; set; }
    }
}
