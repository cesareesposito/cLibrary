using cLibrary.Models;
using cLibrary.Extensions;

namespace cLibrary.Task
{    
    public class cTaskManager
    {
        #region definizione e riempimento del log
        private List<cLogElement> _log = new List<cLogElement>();
        public List<cLogElement> Logs { get { return _log; } }

        public delegate void onLogElementDelegate(cLogElement log);
        public event onLogElementDelegate OnLogElement;
        public void AddLogElement(cLogElement log)
        {
            _log.Add(log);
            if (OnLogElement != null)
                OnLogElement(log);
        }
        #endregion

        #region definizione e riempimento della lista dei task
        private List<TaskDescriptor> _tasks = new List<TaskDescriptor>();
        public void AddTask(TaskDescriptor x)
        {
            x.AddLogElement = AddLogElement;
            _tasks.Add(x);
            //x.Task = this;
        }
        public void AddTasks(TaskDescriptor[] tasks)
        {
            foreach (TaskDescriptor x in tasks)
                AddTask(x);
        }
        public void AddTasks(List<TaskDescriptor> tasks)
        {
            AddTasks(tasks.ToArray());
        }
        #endregion

        #region evento su completamento a buon fine
        public delegate void OnCompletedDelegate(List<TaskDescriptor> tasks, int totalOperations, int errors);
        public event OnCompletedDelegate OnCompleted;
        #endregion

        #region esecuzione automatica della lista dei tasks

        int _i = 0;
        private Thread _thread = null;
        private TaskDescriptor _currentTask;

        public void RunTasks()
        {
            if (_i >= _tasks.Count)
            { TasksCompleted(); return; }

            _currentTask = _tasks[_i++];
            while (_currentTask == null && _i < _tasks.Count)
                _currentTask = _tasks[_i++];

            if (_currentTask == null && _i < _tasks.Count)
                return;

            AddLogElement(new cLogElement(_currentTask.ShortTitle, 0, string.Empty, _currentTask.Title));

            if (_currentTask.TaskDelegate == null)
            {
                _currentTask.TaskResult = cTaskResult.TASKEMPTY;
                AddLogElement(new cLogElement(_currentTask.ShortTitle, 0, string.Empty, "Task is empty."));
                return;
            }
            _thread = new Thread(_currentTask.TaskDelegate);
            _thread.IsBackground = true;
            _thread.Start(_currentTask);
            TaskDoWork();
        }
        private void TaskDoWork()
        {
            _currentTask.Progress = 0;
            Thread.Sleep(1000);

            while (_thread.IsAlive)
                Thread.Sleep(1000);

            TaskCompleted();
        }
        private void TaskCompleted()
        {
            _currentTask.Progress = 100;
            var message = $"{_currentTask.Title} {(_currentTask.TaskResult == cTaskResult.SUCCESS ? " - Completed." : _currentTask.TaskResult.Label)}";
            AddLogElement(new cLogElement(_currentTask.ShortTitle, 0, null, message));
            RunTasks(); // Next task
        }
        private void TasksCompleted()
        {
            int err = 0;
            int total = _tasks.Count;
            int abort = 0;
            //pBar.Value = 100;
            AddLogElement(new cLogElement("Completed", 0, string.Empty, "All operations have been completed."));

            _tasks.cForEach(it =>
            {
                if (it.TaskResult == cTaskResult.ABORT)
                    abort++;
                else
                    if (it.TaskResult != cTaskResult.SUCCESS)
                    err++;
            });

            if (err == 0 && abort == 0)
                AddLogElement(new cLogElement("Completed", 0, string.Empty, "All operations have been completed successfully."));
            else
            {
                var msg = String.Format("Completed {0} operations successfully, {1} operations completed with errors, {2} operations aborted.", total - err, err, abort);
                AddLogElement(new cLogElement("Completed", 0, string.Empty, msg));
            }
            if (OnCompleted != null && total > err)
                OnCompleted(_tasks, total, err);
        }
        #endregion
    }
}
