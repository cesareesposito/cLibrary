using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;

namespace cLibrary.WinCustomControl.LogProcess
{
    public class cTaskDescriptor
    {
        public const int ABORT = -100;

        private string _shortTitle;
        private string _title;
        private cFrmLogProgress _form;
        private ParameterizedThreadStart _taskDelegate;
        private object _taskParams;
        private int _taskResult = -1;
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

        public cFrmLogProgress Form
        {
            get { return _form; }
            set { _form = value; }
        }

        public ParameterizedThreadStart TaskDelegate
        {
            get { return _taskDelegate; }
            set { _taskDelegate = value; }
        }

        public int TaskResult
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

        public cTaskDescriptor(string shortTitle, string title, cFrmLogProgress form,
            object taskParams)
        {
            _shortTitle = shortTitle;
            _title = title;
            _form = form;
            _taskParams = taskParams;
        }
    }
}
