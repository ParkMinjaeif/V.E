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
using IFVisionEngine.Themes;

namespace IFVisionEngine.UIComponents.UserControls
{
    public partial class UcNodeDataView: UserControl
    {
        private Main _formMainInstance;

        public UcNodeDataView(Main formMainInstance)
        {
            InitializeComponent();
            _formMainInstance = formMainInstance;
            ThemeManager.ApplyThemeToControl(this);
            //this.Dock = DockStyle.Fill;
            //this.pnlTop.Controls.Add(AppUIManager.ucNodeExecutionView);
            //this.pnlBot.Controls.Add(AppUIManager.ucNodeSelectedView);
        }
    }
}
