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
    public partial class MomentsParameterDescription : UserControl
    {
        public MomentsParameterDescription()
        {
            InitializeComponent();
            InitRichDescription();
        }

        private void InitRichDescription()
        {
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Clear();

            // 들여쓰기용 공백
            string indent = "    ";

            // 제목
            richTextBox1.SelectionFont = new Font("맑은 고딕", 12F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.DarkBlue;
            richTextBox1.AppendText("이미지 모멘트 분석 (Image Moments Analysis)\n\n");

            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText("이미지 모멘트는 객체의 기하학적 특성을 수치화하여 모양, 크기, 방향 등을 분석하는 기법입니다.\n\n");

            // 1. 이진화 임계값
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText("1. 이진화 임계값 (Binary Threshold)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "그레이스케일 이미지를 이진 이미지로 변환할 때 사용하는 기준값입니다.\n");
            richTextBox1.AppendText(indent + "이 값보다 밝은 픽셀은 흰색(255), 어두운 픽셀은 검은색(0)으로 변환됩니다.\n");
            richTextBox1.AppendText(indent + "값을 올리면: 더 밝은 영역만 객체로 인식 (노이즈 감소, 세부사항 손실)\n");
            richTextBox1.AppendText(indent + "값을 내리면: 더 어두운 영역까지 객체로 인식 (세부사항 보존, 노이즈 증가)\n");
            richTextBox1.AppendText(indent + "추천 범위: 80~200 (이미지 밝기에 따라 조정)\n\n");

            // 2. 중심점 표시
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("2. 중심점 표시 (Show Centroid)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "객체의 무게중심(centroid)을 계산하여 표시합니다.\n");
            richTextBox1.AppendText(indent + "중심점은 다음 공식으로 계산됩니다:\n");
            richTextBox1.AppendText(indent + "X 중심점 = M10 / M00,  Y 중심점 = M01 / M00\n");
            richTextBox1.AppendText(indent + "활용: 객체 추적, 위치 측정, 정렬 기준점 설정\n");
            richTextBox1.AppendText(indent + "참고: 복잡한 형태나 구멍이 있는 객체에서는 기하학적 중심과 다를 수 있음\n\n");

            // 3. 면적 표시
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("3. 면적 표시 (Show Area)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "객체의 전체 면적을 픽셀 단위로 계산하여 표시합니다.\n");
            richTextBox1.AppendText(indent + "면적은 0차 모멘트(M00)와 동일합니다.\n");
            richTextBox1.AppendText(indent + "활용: 객체 크기 비교, 필터링, 분류 기준\n");
            richTextBox1.AppendText(indent + "참고: 실제 면적을 구하려면 픽셀 크기를 실제 크기로 변환 필요\n");
            richTextBox1.AppendText(indent + "예시: 면적이 1000픽셀이고 1픽셀=0.1mm이면 실제 면적은 10mm²\n\n");

            // 4. 방향각 표시
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("4. 방향각 표시 (Show Orientation)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "객체의 주축(principal axis) 방향을 각도로 계산하여 표시합니다.\n");
            richTextBox1.AppendText(indent + "2차 중심 모멘트를 사용하여 계산됩니다:\n");
            richTextBox1.AppendText(indent + "각도 = 0.5 * atan2(2*μ11, μ20-μ02)\n");
            richTextBox1.AppendText(indent + "활용: 객체 회전 감지, 정렬, 방향성 분석\n");
            richTextBox1.AppendText(indent + "참고: 대칭적인 객체(원형)에서는 방향이 불명확할 수 있음\n");
            richTextBox1.AppendText(indent + "표시: 객체 중심에서 주축 방향으로 선을 그려 시각화\n\n");

            // 5. 경계박스 표시
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("5. 경계박스 표시 (Show Bounding Box)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "객체를 완전히 포함하는 최소 직사각형을 표시합니다.\n");
            richTextBox1.AppendText(indent + "수직/수평 경계박스(AABB: Axis-Aligned Bounding Box)를 계산합니다.\n");
            richTextBox1.AppendText(indent + "계산: 객체의 최소/최대 X,Y 좌표를 찾아 직사각형 형성\n");
            richTextBox1.AppendText(indent + "활용: 객체 검출, 크기 추정, 충돌 감지, ROI 설정\n");
            richTextBox1.AppendText(indent + "표시 정보: 너비, 높이, 종횡비(가로/세로 비율)\n");
            richTextBox1.AppendText(indent + "참고: 회전된 객체의 경우 실제 크기보다 큰 박스가 될 수 있음\n\n");

            // 6. 편심률 표시
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("6. 편심률 표시 (Show Eccentricity)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "객체가 원형에서 얼마나 벗어났는지를 나타내는 수치입니다.\n");
            richTextBox1.AppendText(indent + "2차 중심 모멘트의 고유값을 사용하여 계산됩니다.\n");
            richTextBox1.AppendText(indent + "편심률 = √(1 - λ_min/λ_max), 범위: 0~1\n");
            richTextBox1.AppendText(indent + "0에 가까울수록: 원형에 가까운 형태\n");
            richTextBox1.AppendText(indent + "1에 가까울수록: 직선에 가까운 형태\n");
            richTextBox1.AppendText(indent + "활용: 형태 분류, 타원 정도 측정, 객체 모양 분석\n");
            richTextBox1.AppendText(indent + "예시: 원(0.0), 정사각형(0.2), 직사각형(0.8), 직선(1.0)\n\n");

            // 7. 그리기 색상
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("7. 그리기 색상 (Draw Color)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "모멘트 분석 결과를 시각화할 때 사용할 색상을 설정합니다.\n");
            richTextBox1.AppendText(indent + "적용 대상: 중심점, 방향각 선, 경계박스, 텍스트 정보\n");
            richTextBox1.AppendText(indent + "버튼을 클릭하여 원하는 색상을 선택할 수 있습니다.\n");
            richTextBox1.AppendText(indent + "추천: 원본 이미지와 대비되는 색상 선택\n");
            richTextBox1.AppendText(indent + "예시: 어두운 이미지에는 밝은 색(빨강, 노랑), 밝은 이미지에는 어두운 색(파랑, 검정)\n\n");

            // 8. 선 두께
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("8. 선 두께 (Line Thickness)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "시각화 요소들(경계박스, 방향각 선 등)을 그릴 때 사용할 선의 두께입니다.\n");
            richTextBox1.AppendText(indent + "값을 올리면: 선이 더 뚜렷하게 보임, 세밀한 부분 가려질 수 있음\n");
            richTextBox1.AppendText(indent + "값을 내리면: 세밀한 표시 가능, 작은 화면에서 보기 어려울 수 있음\n");
            richTextBox1.AppendText(indent + "추천 범위: 1~5 (이미지 해상도와 객체 크기에 따라 조정)\n");
            richTextBox1.AppendText(indent + "고해상도 이미지: 2~5, 저해상도 이미지: 1~3\n\n");

            // 활용 예시
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.DarkGreen;
            richTextBox1.AppendText("주요 활용 분야\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText(indent + "• 객체 검출 및 추적: 중심점, 면적 기반\n");
            richTextBox1.AppendText(indent + "• 품질 검사: 형태 편심률, 방향각 측정\n");
            richTextBox1.AppendText(indent + "• 문자 인식: 문자 방향, 크기 분석\n");
            richTextBox1.AppendText(indent + "• 의료 영상: 세포, 조직 모양 분석\n");
            richTextBox1.AppendText(indent + "• 로봇 비전: 물체 인식 및 조작\n");

            // 모멘트 이론 추가 설명
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.DarkRed;
            richTextBox1.AppendText("\n\n모멘트 이론 배경\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText(indent + "이미지 모멘트는 통계학의 모멘트 개념을 이미지에 적용한 것입니다.\n");
            richTextBox1.AppendText(indent + "• (p,q)차 모멘트: M_pq = ∑∑ x^p * y^q * f(x,y)\n");
            richTextBox1.AppendText(indent + "• 중심 모멘트: μ_pq = ∑∑ (x-x̄)^p * (y-ȳ)^q * f(x,y)\n");
            richTextBox1.AppendText(indent + "• 정규화 모멘트: η_pq = μ_pq / μ_00^((p+q)/2+1)\n");
            richTextBox1.AppendText(indent + "이러한 수학적 기반을 통해 객체의 불변 특성을 추출할 수 있습니다.\n");

            // 실용적 팁
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.Purple;
            richTextBox1.AppendText("\n실용적 사용 팁\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText(indent + "• 전처리: 노이즈 제거 후 모멘트 계산으로 정확도 향상\n");
            richTextBox1.AppendText(indent + "• 스케일 불변: 정규화 모멘트 사용으로 크기 변화에 강건\n");
            richTextBox1.AppendText(indent + "• 회전 불변: Hu 모멘트 활용으로 회전에 독립적인 특성 추출\n");
            richTextBox1.AppendText(indent + "• 성능 최적화: 관심 영역(ROI) 설정으로 계산 속도 향상\n");
        }
    }
}