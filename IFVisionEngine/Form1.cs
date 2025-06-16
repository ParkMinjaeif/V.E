using IFVisionEngine.Manager;
using IFVisionEngine.UIComponents.UserControls;
using NodeEditor;
using OpenCvSharp;
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
        private int _originalPnlLeftWidth, _originalPnlBotHeight;
        private bool _isPnlLeftExpanded = false, _isPnlBotExpanded = false;

        public Form1()
        {
            InitializeComponent();
            AppUIManager.Initialize(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _originalPnlLeftWidth = pnlLeft.Width;
            _originalPnlBotHeight = pnlBottom.Height;

            this.pnlLeft.Controls.Add(AppUIManager.ucNodeEditor);
            this.pnlRight.Controls.Add(AppUIManager.ucImageControler);
            this.pnlMid.Controls.Add(AppUIManager.ucNodeDataView);
        }

        #region Panel Toggle
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

        public void togglePnlBottom()
        {
            _isPnlBotExpanded = !_isPnlBotExpanded;
            if (_isPnlBotExpanded)
            {
                pnlBottom.Height = 256;
            }
            else
            {
                pnlBottom.Height = _originalPnlBotHeight;
            }
        }
        #endregion
    }
}
