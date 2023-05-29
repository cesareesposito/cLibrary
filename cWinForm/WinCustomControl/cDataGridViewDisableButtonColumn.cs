using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cLibrary.WinCustomControl
{
    public class cDataGridViewButtonColumn : DataGridViewButtonColumn
    {
        public cDataGridViewButtonColumn()
        {
            this.CellTemplate = new cDataGridViewButtonCell();
        }
    }
}
