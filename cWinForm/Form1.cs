using cLibrary.WinCustomControl.LogProcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void run()
        {
            var frm = new cFrmLogProgress();

            var task = new cTaskDescriptor("Carica", "Prova caricamento", frm, new Param { id= 1,name="Casare", tag=".net"});
            var task2 = new cTaskDescriptor("Salva", "Prova salva", frm, new Param { id = 1, name = "Test", tag = "c#" });

            cBaseLoader b = new CaricaFile(task, "path");
            cBaseLoader b2 = new CaricaFile(task2, "path2");
            var tasklist = new List<cTaskDescriptor>();
            tasklist.Add(task);
            tasklist.Add(task2);
            //cTaskList tasklist = new cTaskList();
            //tasklist.Add(task, new CaricaFile(task));
            //tasklist.Add(task2, new CaricaFile(task));
            frm.AddTasks(tasklist);
            frm.OnSuccessfulCompletion += new cFrmLogProgress.OnSuccessfulCompletionDelegate(TaskCompletati);
            if (frm.ShowDialog() == DialogResult.OK)
            { }
        }

        public void TaskCompletati(cFrmLogProgress form, int totop, int toterr, List<cTaskDescriptor> tasks)
        {
            MessageBox.Show("fine");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            run();
        }
    }
}
