namespace cLibrary.Models.Enums
{
    public class TaskResult : cEnum
    {
        
        public static TaskResult ABORT = new TaskResult(-1000, "Abort.", LogSeverity.Error);              
        public static TaskResult FAILURE = new TaskResult(-100, "Failure.", LogSeverity.Error);
        public static TaskResult TASKEMPTY = new TaskResult(-1, "Task empty.", LogSeverity.Warning);
        public static TaskResult SUCCESS = new TaskResult(0, "Success.", LogSeverity.Success);
        public static TaskResult INPROGRESS = new TaskResult(1, "Working in progress.", LogSeverity.Info);

        public LogSeverity LogSeverity { get; private set; }

        public TaskResult() { }

        public TaskResult(int id, string label, LogSeverity logSeverity)
            : base(id, label)
        {
            LogSeverity = logSeverity;
        }
    }
}
