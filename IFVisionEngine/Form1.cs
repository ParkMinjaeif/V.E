using IFVisionEngine.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IFVisionEngine
{
    public partial class Form1: Form
    {
        private int _originalPnlLeftWidth;
        private bool _isPnlLeftExpanded = false;

        public Form1()
        {
            InitializeComponent();
            AppUIManager.Initialize(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pnlLeft.Controls.Add(AppUIManager.ucNodeEditor);
            _originalPnlLeftWidth = pnlLeft.Width;
        }

        public void togglePnlLeft()
        {
            _isPnlLeftExpanded = !_isPnlLeftExpanded;
            if (_isPnlLeftExpanded)
            {
                pnlLeft.Width = 1904; 
            }
            else
            {
                pnlLeft.Width = _originalPnlLeftWidth;
            }
        }
    }


}
