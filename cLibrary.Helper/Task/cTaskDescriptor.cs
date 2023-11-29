using cLibrary.Models;

namespace cLibrary.Task
{
    public abstract class TaskDescriptor
    {
        private string _shortTitle;
        private string _title;
        //private cTaskManager _task;
        private ParameterizedThreadStart _taskDelegate;
        private object _taskParams;
        private cTaskResult _taskResult = cTaskResult.ABORT;
        private int _progress = 0;

        public string ShortTitle
        {
            get { return _shortTitle; }
            set { _shortTitle = value; }
        }
        public string Title
        {
            get { return _title; }
        }       
        public ParameterizedThreadStart TaskDelegate
        {
            get { return _taskDelegate; }
        }
        public cTaskResult TaskResult
        {
            get { return _taskResult; }
            set { _taskResult = value; }
        }
        public int Progress
        {
            get { return _progress; }
            set
            {
                if (value <= 100)
                    _progress = value;
            }
        }
        public object TaskParams
        {
            get { return _taskParams; }
        }

        public Action<cLogElement> AddLogElement { get; internal set; }

        public TaskDescriptor(string shortTitle, string title,
            object taskParams)
        {
            _shortTitle = shortTitle;
            _title = title;
            _taskParams = taskParams;
            _taskDelegate += new ParameterizedThreadStart(Task_Delegate);
        }

        protected void Task_Delegate(object param)
        {
            try
            {
                TaskWork();
            }
            catch
            {
                TaskResult = cTaskResult.FAILURE;
            }
        }

        public virtual void TaskWork()
        {
            TaskResult = cTaskResult.TASKEMPTY;
        }
    }
}
