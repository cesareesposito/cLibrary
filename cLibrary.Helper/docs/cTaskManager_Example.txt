using cLibrary.Helper;
using cLibrary.Models.Enums;
using cLibrary.Models.Task;

namespace Test
{
    internal class cTaskManager_Example
    {
        public class TaskToWork : TaskDescriptor
        {
            public TaskToWork(string shortTitle, string title,
                object taskParams)
                : base(shortTitle, title, taskParams) { }

            public override void TaskWork()
            {
                AddLogElement(new LogElement(ShortTitle, 0, null, LogSeverity.Info, Title + " TaskWork messaggio 1"));
                Thread.Sleep(5000);

                AddLogElement(new LogElement(ShortTitle, 0, null, LogSeverity.Info, Title + " TaskWork messaggio 2"));
                Thread.Sleep(1000);
                var parm = TaskParams;
                AddLogElement(new LogElement(ShortTitle, 0, parm, LogSeverity.Info, Title + " TaskWork messaggio 3"));

                TaskResult = TaskResult.SUCCESS;
            }

        }
        public cTaskManager_Example()
        {

        }

        public void Run()
        {
            var _manager = new cTaskManager();
            _manager.OnLogElement += new cTaskManager.onLogElementDelegate(LogElement);
            _manager.OnCompleted += new cTaskManager.OnCompletedDelegate(TaskCompletati);

            var task1 = new TaskToWork("Short Title 1", "Long Title 1", new { id = 1, name = "Casare", tag = ".net" });
            var task2 = new TaskToWork("Short Title 2", "Long Title 2", new { id = 1, name = "Test", tag = "c#" });

            _manager.AddTask(task1);
            _manager.AddTask(task2);

            _manager.RunTasks();
        }
        public void LogElement(LogElement log)
        {
            Console.WriteLine("Activity : " + log.Activity + " - Message : " + log.Message + " - Obj : " + log.Obj.ToString());
            Console.WriteLine();
        }
        public void TaskCompletati(List<TaskDescriptor> tasks, int totop, int toterr)
        {
            Console.WriteLine("tot : " + totop + " err : " + toterr);
            Console.ReadLine();
        }
    }
}
