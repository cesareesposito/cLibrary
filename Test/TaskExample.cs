using cLibrary.Helper;
using cLibrary.Models.Base;
using cLibrary.Models.Enums;
using cLibrary.Models.Task;

namespace Test
{
    public class Param
    {
        public int id { set; get; }
        public string name { set; get; }
        public string tag { set; get; }
    }
    public class TaskToWork : TaskDescriptor
    {
        public TaskToWork(string shortTitle, string title,
            object taskParams)
            : base(shortTitle, title, taskParams) { }

        public override void TaskWork()
        {
            AddLogElement(new LogElement("1",0,null, LogSeverity.Info, "TaskWork messaggio 1"));            
            Thread.Sleep(10000);

            AddLogElement(new LogElement("2", 0, null, LogSeverity.Info, "TaskWork messaggio 2"));
            Thread.Sleep(10000);
            var parm = (Param)TaskParams;
            AddLogElement(new LogElement("3", 0, parm, LogSeverity.Info, "TaskWork messaggio 3"));

            TaskResult = TaskResult.SUCCESS;
        }

    }
    internal class TaskExample
    {
        private cTaskManager _manager;
        public TaskExample()
        {
            _manager = new cTaskManager();
            _manager.OnLogElement += new cTaskManager.onLogElementDelegate(LogElement);
            _manager.OnCompleted += new cTaskManager.OnCompletedDelegate(TaskCompletati);
        }

        public void Run()
        {
            var task1 = new TaskToWork("Short Title 1", "Long Title 1", new Param { id = 1, name = "Casare", tag = ".net" });
            var task2 = new TaskToWork("Short Title 2", "Long Title 2", new Param { id = 1, name = "Test", tag = "c#" });
            
            _manager.AddTask(task1);
            _manager.AddTask(task2);

            _manager.RunTasks();
        }

        public void LogElement(LogElement log)
        {
            Console.WriteLine("Activity : " + log.Activity);
            Console.WriteLine("Message : " + log.Message);
            Console.WriteLine("Obj : " + log.Obj.ToString());
        }

        public void TaskCompletati(List<TaskDescriptor> tasks, int totop, int toterr)
        {
            Console.WriteLine("totop : " + totop);
            Console.WriteLine("toterr : " + toterr);
        }
    }
}
