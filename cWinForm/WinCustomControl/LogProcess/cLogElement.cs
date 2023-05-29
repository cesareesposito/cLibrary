using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cLibrary.WinCustomControl.LogProcess
{
    public class cLogElement
    {
        private DateTime _timestamp;
        private string _activity;
        private cLogSeverity _severity = cLogSeverity.Message;
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

        public cLogSeverity Severity
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

        public cLogElement(string activity, int line, object obj, cLogSeverity severity, string message)
        {
            _activity = activity;
            _timestamp = DateTime.Now;
            _line = line;
            if (obj!=null)
                _obj = obj.ToString();
            _severity = severity;
            _message = message;
        }
    }
}
