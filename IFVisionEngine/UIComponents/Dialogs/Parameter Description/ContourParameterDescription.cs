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
    public partial class ContourParameterDescription : UserControl
    {
        public ContourParameterDescription()
        {
            InitializeComponent();
            InitRichDescription();
        }

        private void InitRichDescription()
        {
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Clear();

            // 들여쓰기용 공백
            string indent = " ";

            // 1. 검출 모드
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText("1. 검출 모드 (RetrievalMode)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "컨투어를 어떤 방식으로 추출할지 결정합니다.\n");
            richTextBox1.AppendText(indent + "• External: 가장 외부 컨투어만 검출 (가장 빠름)\n");
            richTextBox1.AppendText(indent + "• List: 모든 컨투어를 계층 없이 평면적으로 검출\n");
            richTextBox1.AppendText(indent + "• CComp: 외부와 내부 홀(hole) 컨투어만 검출\n");
            richTextBox1.AppendText(indent + "• Tree: 모든 컨투어를 전체 계층구조로 검출 (가장 정확)\n\n");

            // 2. 근사화 방법
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("2. 근사화 방법 (ApproximationMethod)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "컨투어 점들을 어떻게 압축/근사화할지 결정합니다.\n");
            richTextBox1.AppendText(indent + "• None: 모든 컨투어 점을 저장 (정확하지만 메모리 사용량 높음)\n");
            richTextBox1.AppendText(indent + "• Simple: 수평/수직/대각선 세그먼트 압축 (일반적 선택)\n");
            richTextBox1.AppendText(indent + "• TC89_L1: Teh-Chin 체인 근사 알고리즘 (고급)\n\n");

            // 3. 최소 면적
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("3. 최소 면적 (MinContourArea)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "이 값보다 작은 면적의 컨투어는 노이즈로 간주하여 제거합니다.\n");
            richTextBox1.AppendText(indent + "값을 올리면: 작은 노이즈 제거, 세밀한 객체도 함께 제거될 위험\n");
            richTextBox1.AppendText(indent + "값을 내리면: 더 많은 세부사항 보존, 노이즈도 함께 포함\n");
            richTextBox1.AppendText(indent + "추천 범위: 50~500 (객체 크기에 따라 조정)\n\n");

            // 4. 최대 면적
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("4. 최대 면적 (MaxContourArea)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "이 값보다 큰 면적의 컨투어는 배경으로 간주하여 제거합니다.\n");
            richTextBox1.AppendText(indent + "값을 내리면: 큰 배경 영역 제거, 큰 객체도 함께 제거될 위험\n");
            richTextBox1.AppendText(indent + "값을 올리면: 더 큰 객체까지 포함, 배경도 함께 포함될 위험\n");
            richTextBox1.AppendText(indent + "추천 범위: 10000~1000000 (이미지 크기에 따라 조정)\n\n");

            // 5. 원본에 그리기
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("5. 원본에 그리기 (DrawOnOriginal)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "컨투어를 원본 컬러 이미지에 그릴지, 이진 이미지에 그릴지 선택합니다.\n");
            richTextBox1.AppendText(indent + "활성화: 원본 이미지 위에 컨투어 표시 (직관적)\n");
            richTextBox1.AppendText(indent + "비활성화: 이진 이미지 위에 컨투어 표시 (분석 용이)\n\n");

            // 6. 선 두께
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("6. 선 두께 (Thickness)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "컨투어를 그릴 때 사용할 선의 두께입니다.\n");
            richTextBox1.AppendText(indent + "값을 올리면: 컨투어가 더 뚜렷하게 보임, 세밀한 부분 가려질 수 있음\n");
            richTextBox1.AppendText(indent + "값을 내리면: 세밀한 컨투어 확인 가능, 작은 화면에서 보기 어려울 수 있음\n");
            richTextBox1.AppendText(indent + "추천 범위: 1~5 (이미지 해상도에 따라 조정)\n\n");

            // 7. 색상 모드
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("7. 색상 모드 (ColorMode)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "각 컨투어를 어떤 색상으로 표시할지 결정합니다.\n");
            richTextBox1.AppendText(indent + "• Fixed: 모든 컨투어를 동일한 고정 색상으로 표시\n");
            richTextBox1.AppendText(indent + "• Random: 각 컨투어마다 랜덤한 색상 (구별 용이)\n");
            richTextBox1.AppendText(indent + "• SizeBased: 면적 크기에 따라 색상 변화 (분석 용이)\n\n");

            // 8. 고정 색상
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("8. 고정 색상 (FixedColor)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "색상 모드가 'Fixed'일 때 사용할 색상을 설정합니다.\n");
            richTextBox1.AppendText(indent + "버튼을 클릭하여 원하는 색상을 선택할 수 있습니다.\n");
            richTextBox1.AppendText(indent + "추천: 배경과 대비되는 색상 선택 (예: 어두운 배경에 밝은 색)\n\n");

            // 9. 번호 표시
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("9. 번호 표시 (ShowContourNumbers)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "각 컨투어의 중심에 번호를 표시할지 결정합니다.\n");
            richTextBox1.AppendText(indent + "활성화: 컨투어 구별 및 분석에 유용\n");
            richTextBox1.AppendText(indent + "비활성화: 깔끔한 시각화, 번호가 겹치는 문제 방지\n");
            richTextBox1.AppendText(indent + "참고: 컨투어가 많을 때는 비활성화 권장\n");
            // 10. 시각화 표시
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText("10. 시각화 표시 (ShowVisualization)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "컨투어를 이미지에 시각적으로 표시할지 여부를 결정합니다.\n");
            richTextBox1.AppendText(indent + "활성화: 컨투어가 선으로 그려져서 시각적으로 확인 가능\n");
            richTextBox1.AppendText(indent + "비활성화: 원본 이미지만 표시, 컨투어는 감지되지만 보이지 않음\n");
            richTextBox1.AppendText(indent + "용도: 시각화 없이 순수한 분석만 원할 때 유용\n");
            richTextBox1.AppendText(indent + "참고: 비활성화해도 컨투어 검출과 데이터 출력은 정상 작동\n\n");

            // 11. 데이터 출력
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("11. 데이터 출력 (OutputContourData)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "검출된 컨투어의 좌표 데이터를 텍스트 형태로 출력할지 결정합니다.\n");
            richTextBox1.AppendText(indent + "활성화: 컨투어의 모든 좌표점, 면적, 둘레 등의 정보를 문자열로 출력\n");
            richTextBox1.AppendText(indent + "비활성화: 이미지 결과만 출력\n");
            richTextBox1.AppendText(indent + "용도: 후속 노드에서 컨투어 좌표를 직접 활용하고 싶을 때\n");
            richTextBox1.AppendText(indent + "활용 예시: 좌표 기반 분석, 외부 시스템 연동, 데이터 로깅\n\n");

            // 12. JSON 형태
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("12. JSON 형태 (OutputAsJson)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "데이터 출력이 활성화되었을 때 출력 형식을 결정합니다.\n");
            richTextBox1.AppendText(indent + "활성화 (JSON): 구조화된 JSON 형태로 출력\n");
            richTextBox1.AppendText(indent + "  - 프로그래밍 친화적, 파싱 용이, 다른 시스템과의 연동 편리\n");
            richTextBox1.AppendText(indent + "  - 예시: {\"ContourCount\": 3, \"Contours\": [{\"Index\": 0, \"Points\": [...]}]}\n");
            richTextBox1.AppendText(indent + "비활성화 (텍스트): 사람이 읽기 쉬운 텍스트 형태로 출력\n");
            richTextBox1.AppendText(indent + "  - 직관적 이해 용이, 디버깅 편리, 보고서 작성 적합\n");
            richTextBox1.AppendText(indent + "  - 예시: \"Contour #0: 150 points, Area: 2500.5\"\n");
            richTextBox1.AppendText(indent + "참고: 데이터 출력이 비활성화되면 이 설정은 무시됨\n\n");

            // === 사용 팁과 활용 예시 ===
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.DarkBlue;
            richTextBox1.AppendText("💡 활용 팁\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText(indent + "• 성능 최적화: 시각화 OFF + 데이터 출력 ON = 빠른 분석\n");
            richTextBox1.AppendText(indent + "• 디버깅: 시각화 ON + 텍스트 출력 = 직관적 확인\n");
            richTextBox1.AppendText(indent + "• 자동화: 시각화 OFF + JSON 출력 = 시스템 연동\n");
            richTextBox1.AppendText(indent + "• 보고서: 시각화 ON + 텍스트 출력 = 문서화\n\n");

            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.DarkGreen;
            richTextBox1.AppendText("🔧 권장 설정\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText(indent + "• 일반 분석: 시각화 ON, 데이터 출력 OFF\n");
            richTextBox1.AppendText(indent + "• 정밀 분석: 시각화 ON, JSON 출력 ON\n");
            richTextBox1.AppendText(indent + "• 배치 처리: 시각화 OFF, JSON 출력 ON\n");
            richTextBox1.AppendText(indent + "• 결과 확인: 시각화 ON, 텍스트 출력 ON\n");
        }
    }
}