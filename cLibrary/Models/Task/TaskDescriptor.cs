using cLibrary.Helper;
using cLibrary.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cLibrary.Models.Task
{
    public class TaskDescriptor
    {
        private string _shortTitle;
        private string _title;
        private cTaskManager _task;
        private ParameterizedThreadStart _taskDelegate;
        private object _taskParams;
        private TaskResult _taskResult = TaskResult.ABORT;
        private int _progress = 0;

        public string ShortTitle
        {
            get { return _shortTitle; }
            set { _shortTitle = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public cTaskManager Task
        {
            get { return _task; }
            set { _task = value; }
        }

        public ParameterizedThreadStart TaskDelegate
        {
            get { return _taskDelegate; }
            set { _taskDelegate = value; }
        }

        public TaskResult TaskResult
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
            set { _taskParams = value; }
        }

        public TaskDescriptor(string shortTitle, string title,
            cTaskManager task,
            object taskParams)
        {
            _shortTitle = shortTitle;
            _title = title;
            _task = task;
            _taskParams = taskParams;
        }
    }
}
