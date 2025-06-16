using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IFVisionEngine.UIComponents.UserControls
{
    public partial class UcTitleBar: UserControl
    {
        public UcTitleBar(string title)
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;
            this.uiLabel1.Text = title; // 생성자에서 받은 텍스트로 제목을 설정합니다.
        }
    }
}
