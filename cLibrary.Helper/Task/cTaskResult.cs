using cLibrary.Enums;

namespace cLibrary.Task
{
    public class cTaskResult : cEnum
    {
        
        public static cTaskResult ABORT = new cTaskResult(-1000, "Abort.");              
        public static cTaskResult FAILURE = new cTaskResult(-100, "Failure.");
        public static cTaskResult TASKEMPTY = new cTaskResult(-1, "Task empty.");
        public static cTaskResult SUCCESS = new cTaskResult(0, "Success.");
        public static cTaskResult INPROGRESS = new cTaskResult(1, "Working in progress.");

        public cTaskResult() { }

        public cTaskResult(int id, string label)
            : base(id, label)
        {
        }
    }
}
