using cLibrary.Helper;
using System.ComponentModel;

namespace cLibrary.Models.Enums
{
    //public enum LogSeverity
    //{
    //    success,
    //    info,
    //    warn,
    //    error
    //}
    public class LogSeverity : cEnum
    {
        public static LogSeverity Success = new LogSeverity("success", "Success");
        public static LogSeverity Info = new LogSeverity("info", "Info");
        public static LogSeverity Warning = new LogSeverity("warn", "Warning");
        public static LogSeverity Error = new LogSeverity("error", "Error");

        public string Severity { get; private set; }
        public string Summary { get; private set; }

        public LogSeverity() { }

        public LogSeverity(object id, string label)
            : base(id, label)
        {
        }
    }
}