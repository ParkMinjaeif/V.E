using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NodeEditor;
using IFVisionEngine.Manager;

namespace IFVisionEngine.UIComponents.UserControls
{
    public partial class UcNodeDataView: UserControl
    {
        private Form1 _formMainInstance;

        public UcNodeDataView(Form1 formMainInstance)
        {
            InitializeComponent();
            _formMainInstance = formMainInstance;

            this.Dock = DockStyle.Fill;
            this.pnlTop.Controls.Add(AppUIManager.ucNodeExecutionView);
            this.pnlBot.Controls.Add(AppUIManager.ucNodeSelectedView);
        }
    }
}
