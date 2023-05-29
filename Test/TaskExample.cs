using cLibrary.Models.Base;
using cLibrary.Models.Enums;
using cLibrary.Models.Task;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Param
    {
        public int id { set; get; }
        public string name { set; get; }
        public string tag { set; get; }
    }
    public class CaricaFile : TaskWorker
    {
        private string _param1;
        private string _param2;
        public CaricaFile(TaskDescriptor task, string param1,string param2)
            : base(task)
        {
            _param1 = param1;
            _param2 = param2;
            _task = task;
        }


        public override void TaskWork()
        {
            AddLogElement(0, _param1, LogSeverity.Info, "messaggio");
            var task = _task;
            var del = _task.TaskDelegate;
            Thread.Sleep(1000);

            AddLogElement(34, null, LogSeverity.Info, task.Title);

            var parm = (Param)_task.TaskParams;


            _task.TaskResult = TaskResult.SUCCESS;
        }

    }
    internal class TaskExample
    {
    }
}
