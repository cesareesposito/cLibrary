using cLibrary.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cLibrary.Models.Task
{
    public class LogElement
    {
        private DateTime _timestamp;
        private string _activity;
        private LogSeverity _severity = LogSeverity.Info;
        private int _line = 0;
        private string _obj = string.Empty;
        private string _message = string.Empty;

        public DateTime Timestamp
        {
            get { return _timestamp; }
        }

        public string Activity
        {
            get { return _activity; }
        }

        public LogSeverity Severity
        {
            get { return _severity; }
        }

        public int Line
        {
            get { return _line; }
        }

        public string Obj
        {
            get { return _obj; }
        }

        public string Message
        {
            get { return _message; }
        }

        public LogElement(string activity, int line, object obj, LogSeverity severity, string message)
        {
            _activity = activity;
            _timestamp = DateTime.Now;
            _line = line;
            if (obj != null)
                _obj = obj.ToString();
            _severity = severity;
            _message = message;
        }
    }
}
