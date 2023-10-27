using cLibrary.Enums;
using System.Text.Json.Serialization;

namespace cLibrary.Models
{

    [Serializable]
    public class cLogSeverity
    {
        public string Severity { get; set; }
        public string Summary { get; set; }

        private string _deatail = MessagesModel.Success;
        public string Detail { get; set; }

        public void AddDetail(string detail)
        {
            if (!_deatail.EndsWith(Environment.NewLine))
                _deatail += Environment.NewLine;

            _deatail += detail;
        }
        public void AddDetail(IEnumerable<string> detail)
        {
            foreach (var item in detail)
            {
                if (!_deatail.EndsWith(Environment.NewLine))
                    _deatail += Environment.NewLine;

                _deatail += item;
            }
        }

        public cLogSeverity() { }
        public cLogSeverity(string severity = null, string summary = null, string deatail = null)
        {
            Severity = severity;
            Summary = summary;
            _deatail = deatail;
        }
    }

    public class cLogError : cLogSeverity
    {
        public cLogError(string deatail)
        : base("error", "Error")
        {
            Detail = deatail ?? MessagesModel.Success;
        }
        public cLogError(string severity = "error", string summary = "Error", string deatail = null)
            : base(severity, summary)
        {
            Detail = deatail ?? MessagesModel.Error;
        }
    }
    public class cLogSuccess : cLogSeverity
    {
        public cLogSuccess(string deatail)
        : base("success", "Success")
        {
            Detail = deatail ?? MessagesModel.Success;
        }
        public cLogSuccess(string severity = "success", string summary = "Success", string deatail=null)
            : base(severity, summary)
        {
            Detail = deatail ?? MessagesModel.Success;
        }
    }
    public class cLogInfo : cLogSeverity
    {
        public cLogInfo(string deatail)
        : base("info", "Info")
        {
            Detail = deatail;
        }
        public cLogInfo(string severity = "info", string summary = "Info", string deatail = null)
            : base(severity, summary)
        {
            Detail = deatail;
        }
    }
    public class cLogWarning : cLogSeverity
    {
        public cLogWarning(string deatail)
        : base("warn", "Warning")
        {
            Detail = deatail ?? MessagesModel.Warning;
        }
        public cLogWarning(string severity = "warn", string summary = "Warning", string deatail = null)
            : base(severity, summary)
        {
            Detail = deatail?? MessagesModel.Warning;
        }
    }
}