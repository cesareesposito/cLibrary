using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace cLibrary.Models
{

    [Serializable]
    public class OperationResult
    {
        #region altri costruttori    
        [JsonConstructor]
        public OperationResult() { ExMessage = new cLogError(); }

        public OperationResult(bool result = false)
        {
            this.Success = result;
            this.ExMessage = Success ? new cLogSuccess()
                 : _rowWrited == 0
                 ? new cLogWarning()
                 : new cLogError();
            this.Message = this.ExMessage.Detail;
        }
        public OperationResult(string errorMessage)
           : this(() => -1, errorMessage)
        { }
        public OperationResult(List<string> errorMessage)
           : this(() => -1, string.Join(Environment.NewLine, errorMessage))
        { }
        public OperationResult(bool success, string errorMessage = null, string successMessage = null, string data = null)
            : this(() => (success ? 1 : 0), errorMessage, successMessage, data)
        { }
        public OperationResult(Func<int?> saveFunc, string errorMessage = null, string successMessage = null, string data = null)
        {
            try
            {
                this._rowWrited = saveFunc() ?? _rowWrited;
                this.Success = _rowWrited >= 0;
                this.ExMessage = Success ? new cLogSuccess(successMessage)
                 : _rowWrited == 0
                 ? new cLogWarning()
                 : new cLogError(errorMessage);
                this.Message = this.ExMessage.Detail;
                this.JsonData = data;
            }
            catch (Exception ex)
            {
                this.Success = false;
                var _exception = ex.InnerException ?? ex;
                var _detail = string.Join(Environment.NewLine,
                    new
                    {
                        _exception.Source,
                        _exception.StackTrace
                    });

                this.ExMessage = new cLogError(
                    summary: _exception.Message,
                    deatail: _detail);
#if DEBUG
                this.Message = _exception.Message;
#else
                this.Message = errorMessage ?? MessagesModel.Error;                
#endif
            }
        }

        public OperationResult(Exception ex, string errorMessage = null)
        {
            this.Success = false;
            this.Success = false;
            var _exception = ex.InnerException ?? ex;
            var _detail = string.Join(Environment.NewLine,
                new
                {
                    _exception.Source,
                    _exception.StackTrace
                });

            this.ExMessage = new cLogError(
                summary: _exception.Message,
                deatail: _detail);
#if DEBUG
            this.Message = _exception.Message;
#else
            this.Message = errorMessage ?? MessagesModel.Error;
#endif
        }
        #endregion

        private int _rowWrited { get; set; } = 0;
        public bool Success { get; set; } = false;
        public string JsonData { get; set; }
        public string Message { get; set; }
        public cLogSeverity ExMessage { get; set; }
    }
}
