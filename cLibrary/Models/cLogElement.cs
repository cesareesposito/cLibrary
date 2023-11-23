namespace cLibrary.Models
{
    public class cLogElement
    {
        private DateTime _timestamp;
        private string _activity;
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

        public cLogElement(string activity, int line, object obj, string message)
        {
            _activity = activity;
            _timestamp = DateTime.Now;
            _line = line;
            if (obj != null)
                _obj = obj.ToString();
            _message = message;
        }
    }
}
