using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace cLibrary.WinCustomControl.LogProcess
{
    public partial class cFrmLogProgress : Form
    {
        public cFrmLogProgress()
        {
            InitializeComponent();

            LoadLogElement(null, "");
        }
        public cFrmLogProgress(List<cLogElement> logElemToLoad, string title)
        {
            InitializeComponent();

            LoadLogElement(logElemToLoad, title);
        }

        private void LoadLogElement(List<cLogElement> logElemToLoad, string title)
        {
            DataTable dt = CreateLogTable();

            //load log element
            if (logElemToLoad != null)
            {
                foreach (cLogElement lg in logElemToLoad)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = lg.Timestamp;
                    dr[1] = lg.Activity;
                    dr[2] = lg.Severity;
                    dr[3] = lg.Message;
                    if (lg.Line >= 0)
                        dr[4] = lg.Line;
                    dr[5] = lg.Obj;
                    dt.Rows.Add(dr);
                }
            }
            dt.AcceptChanges();
            dgvLog.DataSource = dt;
            SetDgvColumnStyle();
            btnChiudi.Enabled = true;
            lblTitle.Text = "Riepilogo Log";
            if (string.IsNullOrEmpty(title))
                this.Text = "Log delle operazioni";
            else
                this.Text = title;
        }


        #region evento su completamento a buon fine
        public delegate void OnSuccessfulCompletionDelegate(cFrmLogProgress form, int totalOperations, int errors, List<cTaskDescriptor> tasks);
        public event OnSuccessfulCompletionDelegate OnSuccessfulCompletion;
        #endregion

        #region definizione e riempimento della lista dei task
        private List<cTaskDescriptor> _tasks = new List<cTaskDescriptor>();
        public void AddTask(cTaskDescriptor x)
        {
            _tasks.Add(x);
            x.Form = this;
        }

        public void AddTasks(cTaskDescriptor[] tasks)
        {
            foreach (cTaskDescriptor x in tasks)
                AddTask(x);
        }

        public void AddTasks(List<cTaskDescriptor> tasks)
        {
            AddTasks(tasks.ToArray());
        }
        #endregion

        #region definizione e riempimento del log
        private List<cLogElement> _log = new List<cLogElement>();


        static string _lastMsg = string.Empty;
        static string _lastObj = string.Empty;
        public void AddLogElement(cLogElement x)
        {
            if (InvokeRequired)
            {
                if (x.Message == _lastMsg && x.Obj == _lastObj)
                    return;
                Invoke(new MethodInvoker(delegate() { AddLogElement(x); }));
            }
            else
            {
                if (x.Message == _lastMsg && x.Obj == _lastObj)
                    return;
                _lastMsg = x.Message;
                _lastObj = x.Obj;
                int line = dgvLog.Rows.Count;
                _log.Add(x);
                if (dgvLog.DataSource == null)
                    dgvLog.DataSource = CreateLogTable();

                DataTable dt = LogDataTable;
                dt.Rows.Add(
                    x.Timestamp, x.Activity, x.Severity, x.Message, ((x.Line == 0) ? DBNull.Value : (object)x.Line), x.Obj);
                if (!cbxActivity.Items.Contains(x.Activity))
                    cbxActivity.Items.Add(x.Activity);
            }
        }

        private DataTable CreateLogTable()
        {
            DataTable dt = new DataTable("LOG");
            dt.Columns.Add(new DataColumn("Timestamp", typeof(DateTime)));
            dt.Columns.Add("Activity");
            dt.Columns.Add("Severity");
            dt.Columns.Add("Message");
            dt.Columns.Add(new DataColumn("Line", typeof(Int32)));
            dt.Columns.Add("Object");
            return dt;
        }
        #endregion


        #region esecuzione automatica della lista dei tasks

        int _i = 0;
        private Thread _thread = null;
        private object _state = 0;
        private int _taskState = 0;
        private cTaskDescriptor _currentTask;

        public DataTable LogDataTable
        {
            get
            {
                if (dgvLog.DataSource == null)
                    dgvLog.DataSource = CreateLogTable();
                SetDgvColumnStyle();
                DataTable dt = dgvLog.DataSource as DataTable;
                if (dt == null)
                    dt = ((DataView)(dgvLog.DataSource)).Table;
                return dt;
            }

        }

        private void SetDgvColumnStyle()
        {
            dgvLog.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
        }


        private void RunTasks()
        {
            if (_i >= _tasks.Count)
            { SummarizeTasks(); return; }

            _currentTask = _tasks[_i++];
            while (_currentTask == null && _i < _tasks.Count)
                _currentTask = _tasks[_i++];

            if (_currentTask == null && _i < _tasks.Count)
                return;

            lblTitle.Text = _currentTask.ShortTitle + " - " + _currentTask.Title;
            _taskState = 0;
            if (_currentTask.TaskDelegate == null)
            {
                _currentTask.TaskResult = cTaskDescriptor.ABORT;
                AddLogElement(new cLogElement(_currentTask.ShortTitle, 0, string.Empty, cLogSeverity.Error, "Errore non previsto il task non puo errere eseguito."));
                return;
            }
            _thread = new Thread(_currentTask.TaskDelegate);
            _thread.IsBackground = true;
            _thread.Start(_currentTask);
            bgw_ExecuteTask.RunWorkerAsync();
        }

        private void SummarizeTasks()
        {
            int err = 0;
            int total = _tasks.Count;
            int abort = 0;
            //pBar.Value = 100;
            AddLogElement(new cLogElement("FINE", 0, string.Empty, cLogSeverity.Message, "Fine delle Operazioni"));
            foreach (cTaskDescriptor x in _tasks)
            {
                if (x.TaskResult == cTaskDescriptor.ABORT)
                    abort++;
                else
                    if (x.TaskResult != 0)
                        err++;
            }

            if (err == 0 && abort == 0)
            {
                AddLogElement(new cLogElement("FINE", 0, string.Empty, cLogSeverity.Message, "Tutte le operazioni sono state completate correttamente"));
                lblTitle.Text = "Tutte le operazioni sono state completate correttamente";
                _result = DialogResult.OK;
            }
            else
            {
                var msg = String.Format("Completate {0} operazioni con successo, {1} operazioni complete con errori, {2} operazioni interrotte ", total - err, err, abort);
                AddLogElement(new cLogElement("FINE", 0, string.Empty, cLogSeverity.Message, msg));
                lblTitle.Text = msg;
            }
            if (OnSuccessfulCompletion != null && total > err)
                OnSuccessfulCompletion(this, total, err, _tasks);

            btnChiudi.Enabled = true;
            btnStampa.Enabled = true;
            //throw new NotImplementedException();
        }

        private void bgw_ExecuteTask_DoWork(object sender, DoWorkEventArgs e)
        {
            _taskState++;
            bgw_ExecuteTask.ReportProgress(0);
            Thread.Sleep(1000);
            while (_thread.IsAlive)
            {
                if (bgw_ExecuteTask.CancellationPending)
                {
                    try
                    { _thread.Abort(); }
                    catch { }
                    _currentTask.TaskResult = cTaskDescriptor.ABORT;
                }
                int oldProgress = _currentTask.Progress;
                Thread.Sleep(1000);
                if (_currentTask.Progress != oldProgress)
                    bgw_ExecuteTask.ReportProgress(_currentTask.Progress);
            }
        }

        private void bgw_ExecuteTask_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }

        private void bgw_ExecuteTask_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pBar.Value = pBar.Maximum;
            if (_taskState == 1)
            {
                // check task result for phase 1 
                if (_currentTask.TaskResult == 0)
                    AddLogElement(new cLogElement(_currentTask.ShortTitle, 0, null, cLogSeverity.Message, lblTitle.Text + " - completato"));
                else
                    AddLogElement(new cLogElement(_currentTask.ShortTitle, 0, null, cLogSeverity.Error, lblTitle.Text = lblTitle.Text + " - ERRORE !!!"));
            }
            if (!bgw_ExecuteTask.CancellationPending)
                RunTasks(); // Next task
        }

        private void cFrmLogProgress_Shown(object sender, EventArgs e)
        {
            cbxActivity.SelectedIndex = 0;
            cbxSeverity.SelectedIndex = 0;

            if (_tasks.Count != 0)
                RunTasks();
            //RunTask(_tasks[0]);
        }
        #endregion


        #region filtri sulla DataGridView del log
        private void cbxActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateFilterForDataSource();
        }

        private void cbxSeverity_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateFilterForDataSource();
        }

        private void GenerateFilterForDataSource()
        {
            if (dgvLog.DataSource == null)
                dgvLog.DataSource = CreateLogTable();
            SetDgvColumnStyle();
            DataTable dt = dgvLog.DataSource as DataTable;
            if (dt == null)
                dt = ((DataView)(dgvLog.DataSource)).Table;

            string condition = string.Empty;

            int i = cbxSeverity.SelectedIndex;
            switch (i)
            {
                case 0:
                    condition = "1=1";
                    break;
                case 1:
                    condition = "Severity='Error'";
                    break;
                case 2:
                    condition = "Severity='Warning'";
                    break;
                case 3:
                    condition = "Severity='Message'";
                    break;
            }

            i = cbxActivity.SelectedIndex;
            if (i != 0)
            {
                string text = cbxActivity.SelectedItem.ToString();
                condition += " AND Activity='" + text.Replace("'", "''") + "'";
            }

            dt.DefaultView.RowFilter = condition;
        }
        #endregion

        private DialogResult _result = DialogResult.Ignore;
        private void btnChiudi_Click(object sender, EventArgs e)
        {
            this.DialogResult = _result;
            this.Close();
        }

        private void cFrmLogProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_thread.IsAlive)
            {
                DialogResult r = MessageBox.Show("Sei di voler fermare il processo?", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r != DialogResult.Yes)
                    e.Cancel = true;
                else
                {
                    bgw_ExecuteTask.CancelAsync();
                }
            }
        }

        private void btnStampa_Click(object sender, EventArgs e)
        {

        }
    }
}
