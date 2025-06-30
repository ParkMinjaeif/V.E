using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IFVisionEngine.UIComponents.Dialogs.Parameter_Description
{
    public partial class GaussianBlurParameterDescription: UserControl
    {
        public GaussianBlurParameterDescription()
        {
            InitializeComponent();
            InitRichDescription();
        }
        private void InitRichDescription()
        {
            richTextBox1.Clear();
            string indent = " "; // 들여쓰기: 공백 한 칸

            // 1. Kernel Size (Width)
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText("1. Kernel Size (Width)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "가우시안 커널의 가로 크기 (홀수만 가능: 1, 3, 5, ...).\n");
            richTextBox1.AppendText(indent + "크기를 올리면 블러(흐림) 효과가 강해짐.\n");
            richTextBox1.AppendText(indent + "너무 크면 가장자리 손실 및 흐림 부작용 가능.\n");
            richTextBox1.AppendText(indent + "추천 범위: 3 ~ 11 (실제 사용은 3, 5, 7이 많음)\n\n");

            // 2. Kernel Size (Height)
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("2. Kernel Size (Height)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "가우시안 커널의 세로 크기 (홀수만 가능: 1, 3, 5, ...).\n");
            richTextBox1.AppendText(indent + "Width와 동일한 원리.\n");
            richTextBox1.AppendText(indent + "너무 크면 흐림 부작용.\n");
            richTextBox1.AppendText(indent + "추천 범위: 3 ~ 11\n\n");

            // 3. SigmaX
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("3. SigmaX\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "X(가로) 방향의 가우시안 분포 표준편차.\n");
            richTextBox1.AppendText(indent + "0으로 두면 커널 크기에서 자동 계산됨.\n");
            richTextBox1.AppendText(indent + "값을 올리면 흐림 효과 증가.\n");
            richTextBox1.AppendText(indent + "추천 범위: 0\n\n");

            // 4. SigmaY
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("4. SigmaY\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "Y(세로) 방향의 가우시안 분포 표준편차.\n");
            richTextBox1.AppendText(indent + "0으로 두면 SigmaX와 동일하게 자동 결정.\n");
            richTextBox1.AppendText(indent + "값을 올리면 흐림 효과 증가.\n");
            richTextBox1.AppendText(indent + "추천 범위: 0\n");
        }
    }
}
