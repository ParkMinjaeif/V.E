using IFVisionEngine.Manager;
using IFVisionEngine.Themes;
using IFVisionEngine.UIComponents.UserControls;
using NodeEditor;
using OpenCvSharp;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace IFVisionEngine
{
    public partial class Form1:Form
    {
        public Form1()
        {
            InitializeComponent();
            AppUIManager.Initialize(this);
            SetupWindowSystem();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyThemeToControl(this);
        }
        private void SetupWindowSystem()
        {
            // 기본 타이틀바 제거
            this.FormBorderStyle = FormBorderStyle.None;

            // 커스텀 타이틀바 추가
            CustomTitleBar titleBar = new CustomTitleBar();
            titleBar.ParentForm = this;
            titleBar.SetFileInfo("IF Vision Engine", Properties.Resources.IF);
            this.Controls.Add(titleBar);
        }
    }
}
