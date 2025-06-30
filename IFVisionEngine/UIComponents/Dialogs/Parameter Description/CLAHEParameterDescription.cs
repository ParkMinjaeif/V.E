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
    public partial class CLAHEParameterDescription: UserControl
    {
        public CLAHEParameterDescription()
        {
            InitializeComponent();
            InitRichDescription();
        }
        private void InitRichDescription()
        {
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Clear();
            // 번호 + 파라미터명(굵게)
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText("1. ClipLimit\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            // 들여쓰기: 띄어쓰기 1번 (\u00A0 = Non-Breaking Space, \t 대신)
            string indent = " ";
            richTextBox1.AppendText(indent + "CLAHE(Contrast Limited Adaptive Histogram Equalization) 알고리즘에서 히스토그램이 클리핑되는 최대 임계값입니다.\n");
            richTextBox1.AppendText(indent + "너무 큰 값은 국부 대비 증가(노이즈도 증폭), 너무 작으면 평탄화 효과 약화.\n");
            richTextBox1.AppendText(indent + "값을 내리면: 대비 증가량이 제한(과도한 노이즈 억제)\n");
            richTextBox1.AppendText(indent + "값을 올리면: 더 큰 대비 효과(하지만 노이즈도 함께 증가)\n");
            richTextBox1.AppendText(indent + "추천 범위: 2.0 ~ 8.0 (일반적으로 3~5 사이를 많이 사용)\n\n");

            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("2. TileGridSize (Width)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "이미지를 가로로 나누는 그리드(타일) 수.\n");
            richTextBox1.AppendText(indent + "작을수록 세밀한 지역별 처리, 너무 작으면 부자연스럽게 보일 수 있음.\n");
            richTextBox1.AppendText(indent + "값을 내리면: 더 큰 타일(적은 그리드) → 전역적 보정\n");
            richTextBox1.AppendText(indent + "값을 올리면: 더 작은 타일(많은 그리드) → 지역대비 극대화, 경계부 노이즈 위험\n");
            richTextBox1.AppendText(indent + "추천 범위: 4~16 (실무/논문에서 8이나 16도 많이 씀)\n\n");

            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("3. TileGridSize (Height)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "이미지를 세로로 나누는 그리드(타일) 수.\n");
            richTextBox1.AppendText(indent + "Width와 원리 동일\n");
            richTextBox1.AppendText(indent + "값을 내리면: 더 큰 타일 → 전역효과 증가\n");
            richTextBox1.AppendText(indent + "값을 올리면: 더 작은 타일(많은 그리드) → 세밀 조정, 경계 artifacts 발생 가능\n");
            richTextBox1.AppendText(indent + "추천 범위: 4~16\n");

        }
    }
}
