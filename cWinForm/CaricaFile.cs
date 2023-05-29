using cLibrary.WinCustomControl.LogProcess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cWinForm
{
    public class Param
    {
        public int id { set; get; }
        public string name { set; get; }
        public string tag { set; get; }
    }
    public class CaricaFile : cBaseLoader
    {
        private string s;
        public CaricaFile(cTaskDescriptor task, string file)
            : base(task)
        {
            s = file;
           _task = task;
        }


        public override void TaskWork()
        {
            AddLogElement(0, s, cLogSeverity.Message, "messaggio");            
            var task = _task;
            var del = _task.TaskDelegate;

            AddLogElement(34, null, cLogSeverity.Message, task.Title);

            var parm = (Param)_task.TaskParams;


            _task.TaskResult = SUCCESS;
        }

    }
}
