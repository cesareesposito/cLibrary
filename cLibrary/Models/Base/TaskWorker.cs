using cLibrary.Models.Enums;
using cLibrary.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cLibrary.Models.Base
{
    //public abstract class TaskWorker
    //{
    //    protected TaskDescriptor _task;

    //    public TaskWorker(TaskDescriptor task)
    //    {
    //        task.TaskDelegate = new System.Threading.ParameterizedThreadStart(Task_Delegate);
    //        _task = task;
    //    }

    //    protected void Task_Delegate(object param)
    //    {
    //        try
    //        {
    //            TaskWork();
    //        }
    //        catch
    //        {
    //            _task.TaskResult = TaskResult.FAILURE;
    //        }
    //    }

    //    public virtual void TaskWork()
    //    {
    //        _task.TaskResult = TaskResult.TASKEMPTY;
    //    }

    //    protected void AddLogElement(int line, object obj, LogSeverity severity, String message)
    //    {
    //        _task.Task.AddLogElement(new LogElement(_task.ShortTitle, line, obj, severity, message));
    //    }
    //}
}
