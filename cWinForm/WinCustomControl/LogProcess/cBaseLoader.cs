using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace cLibrary.WinCustomControl.LogProcess
{
    public class cBaseLoader
    {
        public const int ABORT = -100;
        public const int TASKEMPTY = -1000;

        protected const string preparazioneDati = "Preparazione dati da salvare.";
        protected const string salvataggioInfoCaricamento = "Salvataggio delle informazioni del caricamento";
        
        protected const string salvataggioDatiSlot = "Salvataggio dei dati.";
        protected const string operazioneInterrotta = "Operazione Interrotta dall'utente";
        protected const string ERRORE_PERS = "Errore nella persistenza dei dati!";
        protected const string fineCar = "Caricamento Terminato";

        protected const int SUCCESS = 0;
        protected const int FAILURE = -1;

        protected cTaskDescriptor _task;

        public cBaseLoader(cTaskDescriptor task)
        {
            task.TaskDelegate = new System.Threading.ParameterizedThreadStart(Task_Delegate);
            _task = task;
        }

        protected void Task_Delegate(object param)
        {
            try
            {
                TaskWork();
            }
            catch
            {
                _task.TaskResult = FAILURE;
            }
        }

        public virtual void TaskWork()
        {
            _task.TaskResult = TASKEMPTY;
        }

        protected void AddLogElement(int line, object obj, cLogSeverity severity, String message)
        {
            _task.Form.AddLogElement(new cLogElement(_task.ShortTitle, line, obj, severity, message));
        }
    }
}
