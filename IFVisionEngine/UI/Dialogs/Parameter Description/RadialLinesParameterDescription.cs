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
    public partial class RadialLinesParameterDescription : UserControl
    {
        public RadialLinesParameterDescription()
        {
            InitializeComponent();
            InitRichDescription();
        }

        private void InitRichDescription()
        {
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Clear();

            string indent = " ";

            // 1. 시각화 표시
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText("1. 시각화 표시 (ShowVisualization)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "방사선을 이미지에 시각적으로 표시할지 여부를 결정합니다.\n");
            richTextBox1.AppendText(indent + "활성화: 방사선이 그려져서 시각적으로 확인 가능\n");
            richTextBox1.AppendText(indent + "비활성화: 원본 이미지만 표시, 방사선 길이는 계산되지만 보이지 않음\n");
            richTextBox1.AppendText(indent + "용도: 시각화 없이 순수한 분석만 원할 때 유용\n\n");

            // 2. 중심점 방법
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("2. 중심점 방법 (CenterMethod)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "방사선의 중심점을 결정하는 방법을 선택합니다.\n");
            richTextBox1.AppendText(indent + "• ImageCenter: 이미지의 중심점 사용 (가장 단순)\n");
            richTextBox1.AppendText(indent + "• AutoCentroid: 가장 큰 객체의 무게중심 자동 계산\n");
            richTextBox1.AppendText(indent + "• Manual: 수동으로 지정한 X, Y 좌표 사용\n");
            richTextBox1.AppendText(indent + "• ExternalCoordinates: 모멘트 노드 등에서 받은 외부 좌표 사용\n");
            richTextBox1.AppendText(indent + "• MaxBrightness: 이미지에서 가장 밝은 지점 사용\n\n");

            // 3. 수동 좌표
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("3. 수동 좌표 (ManualX, ManualY)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "중심점 방법이 'Manual'일 때 사용할 좌표를 직접 입력합니다.\n");
            richTextBox1.AppendText(indent + "범위: 0 ~ 2000 픽셀\n");
            richTextBox1.AppendText(indent + "주의: 이미지 크기를 벗어나는 좌표는 자동으로 경계로 조정됩니다.\n\n");

            // 4. 범위 방법
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("4. 범위 방법 (RangeMethod)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "방사선이 어디까지 뻗어나갈지를 결정하는 방법입니다.\n");
            richTextBox1.AppendText(indent + "• FixedLength: 고정된 길이까지 (정확한 거리 측정)\n");
            richTextBox1.AppendText(indent + "• ImageBoundary: 이미지 경계까지 (전체 이미지 분석)\n");
            richTextBox1.AppendText(indent + "• EdgeDetection: 객체 경계까지 (이진화 기반, 가장 유용)\n");
            richTextBox1.AppendText(indent + "• BrightnessChange: 밝기 변화 지점까지 (그라디언트 기반)\n\n");

            // 5. 고정 길이
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("5. 고정 길이 (FixedLength)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "범위 방법이 'FixedLength'일 때 사용할 방사선의 길이입니다.\n");
            richTextBox1.AppendText(indent + "범위: 10 ~ 1000 픽셀\n");
            richTextBox1.AppendText(indent + "용도: 특정 반경 내에서만 분석하고 싶을 때\n\n");

            // 6. 선 개수
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("6. 선 개수 (LineCount)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "중심점에서 그릴 방사선의 개수입니다.\n");
            richTextBox1.AppendText(indent + "범위: 4 ~ 360개\n");
            richTextBox1.AppendText(indent + "권장값: 8~16개 (기본 분석), 32~64개 (정밀 분석)\n");
            richTextBox1.AppendText(indent + "참고: 선이 많을수록 정확하지만 처리 시간이 증가합니다.\n\n");

            // 7. 시작 각도
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("7. 시작 각도 (StartAngle)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "첫 번째 방사선의 시작 각도를 설정합니다.\n");
            richTextBox1.AppendText(indent + "범위: 0 ~ 359도\n");
            richTextBox1.AppendText(indent + "기준: 0도는 오른쪽(3시 방향), 90도는 아래쪽(6시 방향)\n");
            richTextBox1.AppendText(indent + "용도: 특정 방향부터 분석을 시작하고 싶을 때\n\n");

            // 8. 선 색상
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("8. 선 색상 (LineColor)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "방사선을 그릴 때 사용할 색상을 선택합니다.\n");
            richTextBox1.AppendText(indent + "기본값: 빨간색\n");
            richTextBox1.AppendText(indent + "팁: 배경과 대비되는 색상을 선택하면 가시성이 좋습니다.\n\n");

            // 9. 선 두께
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("9. 선 두께 (LineThickness)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "방사선을 그릴 때 사용할 선의 두께입니다.\n");
            richTextBox1.AppendText(indent + "범위: 1 ~ 10 픽셀\n");
            richTextBox1.AppendText(indent + "권장값: 1~2 (정밀 분석), 3~5 (일반 시각화)\n\n");

            // 10. 선 스타일
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("10. 선 스타일 (Style)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "방사선의 그리기 스타일을 선택합니다.\n");
            richTextBox1.AppendText(indent + "• Solid: 실선 (기본, 명확한 시각화)\n");
            richTextBox1.AppendText(indent + "• Dotted: 점선 (부드러운 시각화)\n");
            richTextBox1.AppendText(indent + "• Dashed: 파선 (중간 수준의 시각화)\n\n");

            // 11-13. 표시 옵션들
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("11. 표시 옵션들\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "• 중심점 표시 (ShowCenter): 중심점에 원을 그려 위치 표시\n");
            richTextBox1.AppendText(indent + "• 각도 표시 (ShowAngles): 각 방사선의 각도를 텍스트로 표시\n");
            richTextBox1.AppendText(indent + "• 거리 표시 (ShowDistances): 각 방사선의 길이를 텍스트로 표시\n");
            richTextBox1.AppendText(indent + "참고: 모든 표시 옵션을 켜면 화면이 복잡해질 수 있습니다.\n\n");

            // 14. 이진화 임계값
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("12. 이진화 임계값 (BinaryThreshold)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "EdgeDetection 모드일 때 사용할 이진화 임계값입니다.\n");
            richTextBox1.AppendText(indent + "범위: 0 ~ 255\n");
            richTextBox1.AppendText(indent + "기본값: 127 (중간값)\n");
            richTextBox1.AppendText(indent + "조정법: 밝은 이미지는 높게, 어두운 이미지는 낮게\n\n");

            // 15. 밝기 임계값
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("13. 밝기 임계값 (BrightnessThreshold)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "BrightnessChange 모드일 때 사용할 밝기 변화 감지 임계값입니다.\n");
            richTextBox1.AppendText(indent + "범위: 0 ~ 255\n");
            richTextBox1.AppendText(indent + "기본값: 50\n");
            richTextBox1.AppendText(indent + "의미: 중심점 밝기와 이 값 이상 차이나는 지점에서 정지\n\n");

            // 16. 길이 데이터 출력
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.AppendText("14. 길이 데이터 출력 (OutputLengthData)\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.AppendText(indent + "각 방사선의 길이 데이터를 문자열로 출력할지 결정합니다.\n");
            richTextBox1.AppendText(indent + "활성화: 다른 노드에서 원형 검사 등에 활용 가능\n");
            richTextBox1.AppendText(indent + "출력 형식: \"Center0:10.5,20.3,15.2,...\"\n");
            richTextBox1.AppendText(indent + "용도: 원형도 분석, 형태 특성 추출, 품질 검사\n\n");

            // === 사용 팁과 활용 예시 ===
            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.DarkBlue;
            richTextBox1.AppendText("💡 활용 팁\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText(indent + "• 원형 검사: AutoCentroid + EdgeDetection + 길이 데이터 출력\n");
            richTextBox1.AppendText(indent + "• 모멘트 연계: ExternalCoordinates로 모멘트 노드 결과 활용\n");
            richTextBox1.AppendText(indent + "• 성능 최적화: 시각화 OFF + 필요한 선 개수만 설정\n");
            richTextBox1.AppendText(indent + "• 정밀 분석: 선 개수 64개 이상 + EdgeDetection\n");
            richTextBox1.AppendText(indent + "• 빠른 검사: 선 개수 8개 + FixedLength\n\n");

            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.DarkGreen;
            richTextBox1.AppendText("🔧 권장 설정 조합\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText(indent + "• 기본 원형 검사: AutoCentroid + EdgeDetection + 16개 선\n");
            richTextBox1.AppendText(indent + "• 정밀 원형 검사: AutoCentroid + EdgeDetection + 64개 선\n");
            richTextBox1.AppendText(indent + "• 다중 객체 분석: ExternalCoordinates + EdgeDetection\n");
            richTextBox1.AppendText(indent + "• 반지름 측정: 중심점 지정 + FixedLength + 거리 표시\n");
            richTextBox1.AppendText(indent + "• 형태 분석: AutoCentroid + EdgeDetection + 길이 데이터 출력\n\n");

            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.DarkRed;
            richTextBox1.AppendText("⚠️ 주의사항\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText(indent + "• 선 개수가 많을수록 정확하지만 처리 시간 증가\n");
            richTextBox1.AppendText(indent + "• AutoCentroid는 가장 큰 객체만 대상으로 함\n");
            richTextBox1.AppendText(indent + "• EdgeDetection은 이진화가 전제되므로 임계값 조정 중요\n");
            richTextBox1.AppendText(indent + "• 수동 좌표는 이미지 범위 내에서만 유효\n");
            richTextBox1.AppendText(indent + "• 시각화 옵션을 모두 켜면 화면이 복잡해질 수 있음\n");

            richTextBox1.SelectionFont = new Font("맑은 고딕", 11F, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.Purple;
            richTextBox1.AppendText("\n🔗 노드 연결 예시\n");
            richTextBox1.SelectionFont = new Font("맑은 고딕", 10F);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText(indent + "1. 이미지 → 전처리 → RadialLines → 원형 검사\n");
            richTextBox1.AppendText(indent + "2. 이미지 → 모멘트 → RadialLines(외부좌표) → 형태 분석\n");
            richTextBox1.AppendText(indent + "3. 이미지 → RadialLines(자동중심) → 품질 검사\n");
        }
    }
}