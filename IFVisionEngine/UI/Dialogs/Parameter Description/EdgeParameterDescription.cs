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
    public partial class EdgeParameterDescription: UserControl
    {
        public EdgeParameterDescription()
        {
            InitializeComponent();
            InitRichDescription();
        }
        private void InitRichDescription()
        {
            richTextBox1.Clear();
            string indent = " "; // 들여쓰기: 공백 한 칸

            // 1. Edge Method
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText("1. Edge Method\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "엣지(경계) 검출에 사용할 알고리즘 선택 (Canny, Sobel, Laplacian).\n");
            richTextBox1.AppendText(indent + "Canny: 가장 보편적, 두 임계값으로 경계 검출, 노이즈에 강함.\n");
            richTextBox1.AppendText(indent + "Sobel: 방향 미분 기반, 단순/빠름, 경계 방향성 정보.\n");
            richTextBox1.AppendText(indent + "Laplacian: 2차 미분 기반, 경계/코너 강하게 검출.\n");
            richTextBox1.AppendText(indent + "용도/이미지 특성에 맞게 선택.\n\n");

            // 2. Canny Threshold1 (하위 임계값)
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("2. Canny Threshold1 (하위 임계값)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "Canny 선택 시 적용, 엣지로 인식할 최소 값.\n");
            richTextBox1.AppendText(indent + "값을 내리면: 더 많은 엣지 검출 (노이즈도 증가).\n");
            richTextBox1.AppendText(indent + "값을 올리면: 약한 엣지는 무시, 진한 경계만 검출.\n");
            richTextBox1.AppendText(indent + "추천 범위: 50 ~ 150\n\n");

            // 3. Canny Threshold2 (상위 임계값)
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("3. Canny Threshold2 (상위 임계값)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "Canny 선택 시 적용, 강한 엣지로 판단할 임계값.\n");
            richTextBox1.AppendText(indent + "보통 Threshold1보다 2~3배 정도 크게 설정.\n");
            richTextBox1.AppendText(indent + "추천 범위: 100 ~ 300\n\n");

            // 4. Kernel Size (Sobel, Laplacian)
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("4. Kernel Size (Sobel, Laplacian)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "Sobel, Laplacian 선택 시 적용, 미분 계산에 사용될 커널의 크기 (홀수).\n");
            richTextBox1.AppendText(indent + "크기를 올리면 잡음 억제, 경계 부드럽게.\n");
            richTextBox1.AppendText(indent + "너무 크면 세부 경계 손실.\n");
            richTextBox1.AppendText(indent + "추천 범위: 3\n");
        }
    }
}
