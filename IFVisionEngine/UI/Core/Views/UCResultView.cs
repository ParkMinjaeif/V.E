using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IFVisionEngine.UIComponents.Data;
using IFVisionEngine.UIComponents.Managers;
using IFVisionEngine.Themes;

namespace IFVisionEngine.UI.Views
{
    public partial class UcResultView : UserControl
    {
        #region Data Classes

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ResultDisplayWrapper
        {
            [Category("검증 결과")]
            [DisplayName("상태")]
            public string Status { get; set; }

            [Category("검증 결과")]
            [DisplayName("오차값")]
            public string ErrorValue { get; set; }

            [Category("검증 결과")]
            [DisplayName("임계값")]
            public string Threshold { get; set; }

            [Category("통계 정보")]
            [DisplayName("데이터 개수")]
            public string Count { get; set; }

            [Category("통계 정보")]
            [DisplayName("평균")]
            public string Mean { get; set; }

            [Category("통계 정보")]
            [DisplayName("표준편차")]
            public string StandardDeviation { get; set; }

            [Category("통계 정보")]
            [DisplayName("범위")]
            public string Range { get; set; }

            [Category("통계 정보")]
            [DisplayName("변동계수(%)")]
            public string CV { get; set; }

            [Category("상세 정보")]
            [DisplayName("최소값")]
            public string MinValue { get; set; }

            [Category("상세 정보")]
            [DisplayName("최대값")]
            public string MaxValue { get; set; }

            [Category("원본 데이터")]
            [DisplayName("전체 결과")]
            [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public string FullResult { get; set; }

            public ResultDisplayWrapper(string resultData)
            {
                ParseResultData(resultData);
            }

            private void ParseResultData(string resultData)
            {
                if (string.IsNullOrEmpty(resultData))
                {
                    Status = "데이터 없음";
                    return;
                }

                try
                {
                    var lines = resultData.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines)
                    {
                        var trimmedLine = line.Trim();
                        if (string.IsNullOrEmpty(trimmedLine)) continue;

                        // 기존 형식 (STATUS:, ERROR: 등) 처리
                        if (trimmedLine.StartsWith("STATUS:"))
                            Status = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.StartsWith("ERROR:"))
                            ErrorValue = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.StartsWith("THRESHOLD:"))
                            Threshold = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.StartsWith("COUNT:"))
                            Count = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.StartsWith("MEAN:"))
                            Mean = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.StartsWith("STDDEV:"))
                            StandardDeviation = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.StartsWith("RANGE:"))
                            Range = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.StartsWith("CV:"))
                            CV = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.StartsWith("MIN:"))
                            MinValue = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.StartsWith("MAX:"))
                            MaxValue = ExtractValue(trimmedLine, ":");

                        // 한국어 형식 추가 처리
                        else if (trimmedLine.Contains("검증 상태:"))
                            Status = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.Contains("오차 값:"))
                            ErrorValue = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.Contains("허용 임계값:"))
                            Threshold = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.Contains("데이터 개수:"))
                            Count = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.Contains("평균 길이:") || trimmedLine.Contains("평균:"))
                            Mean = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.Contains("표준편차:"))
                            StandardDeviation = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.Contains("범위:"))
                            Range = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.Contains("변동계수:"))
                            CV = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.Contains("최소값:"))
                            MinValue = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.Contains("최대값:"))
                            MaxValue = ExtractValue(trimmedLine, ":");
                    }

                    FullResult = resultData;
                    SetDefaultValues();

                    // 디버깅용 로그
                    Console.WriteLine($"[ResultDisplayWrapper] 파싱 완료 - Status: {Status}, ErrorValue: {ErrorValue}");
                }
                catch (Exception ex)
                {
                    Status = "파싱 오류";
                    FullResult = $"오류: {ex.Message}\n원본: {resultData}";
                }
            }

            private void SetDefaultValues()
            {
                if (string.IsNullOrEmpty(Status)) Status = "알 수 없음";
                if (string.IsNullOrEmpty(ErrorValue)) ErrorValue = "N/A";
                if (string.IsNullOrEmpty(Threshold)) Threshold = "N/A";
                if (string.IsNullOrEmpty(Count)) Count = "N/A";
                if (string.IsNullOrEmpty(Mean)) Mean = "N/A";
                if (string.IsNullOrEmpty(StandardDeviation)) StandardDeviation = "N/A";
                if (string.IsNullOrEmpty(Range)) Range = "N/A";
                if (string.IsNullOrEmpty(CV)) CV = "N/A";
                if (string.IsNullOrEmpty(MinValue)) MinValue = "N/A";
                if (string.IsNullOrEmpty(MaxValue)) MaxValue = "N/A";
            }

            private string ExtractValue(string line, string separator)
            {
                var separatorIndex = line.IndexOf(separator);
                if (separatorIndex >= 0 && separatorIndex < line.Length - 1)
                {
                    return line.Substring(separatorIndex + 1).Trim();
                }
                return "";
            }
        }

        #endregion

        #region Fields

        private Dictionary<string, ResultData> resultHistory = new Dictionary<string, ResultData>();
        private Panel statusPanel;
        private PictureBox statusIcon;

        #endregion

        #region Constructor

        public UcResultView()
        {
            InitializeComponent();
            CreateStatusPanel();
            ConnectToResultsManager();

            // PropertyGrid 초기 설정
            InitializePropertyGrid();

            ThemeManager.ApplyThemeToControl(this);
        }

        private void InitializePropertyGrid()
        {
            try
            {
                // PropertyGrid 기본 설정
                propertyGrid1.PropertySort = PropertySort.Categorized;
                propertyGrid1.HelpVisible = true;
                propertyGrid1.ToolbarVisible = true;

                Console.WriteLine("[UcResultView] PropertyGrid 초기화 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UcResultView] PropertyGrid 초기화 오류: {ex.Message}");
            }
        }

        #endregion

        #region UI Creation

        private void CreateStatusPanel()
        {
            statusPanel = new Panel
            {
                Height = 40,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(45, 45, 48)
            };

            statusIcon = new PictureBox
            {
                Size = new Size(32, 32),
                Location = new Point(10, 4),
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent
            };

            statusPanel.Controls.Add(statusIcon);
            this.Controls.Add(statusPanel);
            statusPanel.BringToFront();

            SetDefaultStatus();
        }

        private void SetDefaultStatus()
        {
            statusIcon.Image = CreateStatusIcon(Color.Gray, "?");
        }

        private Bitmap CreateStatusIcon(Color color, string symbol)
        {
            Bitmap bitmap = new Bitmap(32, 32);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                if (symbol == "✓")
                {
                    // 초록색 동그라미
                    using (Brush brush = new SolidBrush(Color.Green))
                    {
                        g.FillEllipse(brush, 2, 2, 28, 28);
                    }
                    // 흰색 체크마크
                    using (Pen pen = new Pen(Color.White, 3))
                    {
                        g.DrawLines(pen, new Point[] {
                            new Point(8, 16),
                            new Point(14, 22),
                            new Point(24, 10)
                        });
                    }
                }
                else if (symbol == "✗")
                {
                    // 빨간색 동그라미
                    using (Brush brush = new SolidBrush(Color.Red))
                    {
                        g.FillEllipse(brush, 2, 2, 28, 28);
                    }
                    // 흰색 X 마크
                    using (Pen pen = new Pen(Color.White, 3))
                    {
                        g.DrawLine(pen, 10, 10, 22, 22);
                        g.DrawLine(pen, 22, 10, 10, 22);
                    }
                }
                else
                {
                    // 회색 동그라미
                    using (Brush brush = new SolidBrush(color))
                    {
                        g.FillEllipse(brush, 2, 2, 28, 28);
                    }
                }
            }
            return bitmap;
        }

        #endregion

        #region Result Management

        private void ConnectToResultsManager()
        {
            try
            {
                ResultsManager.Instance.OnResultAdded += OnResultAdded;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UcResultView] ResultsManager 연결 실패: {ex.Message}");
            }
        }

        private void OnResultAdded(ResultData result)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<ResultData>(OnResultAdded), result);
                return;
            }

            try
            {
                resultHistory[result.NodeName] = result;
                DisplayResult(result);
                UpdateStatusIcon(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UcResultView] 결과 처리 중 오류: {ex.Message}");
            }
        }

        public void DisplayResult(ResultData result)
        {
            try
            {
                Console.WriteLine($"[UcResultView] DisplayResult 시작: {result.NodeName}");
                Console.WriteLine($"[UcResultView] ResultContent: {result.ResultContent}");

                var wrapper = new ResultDisplayWrapper(result.ResultContent);

                // 디버깅: 파싱된 데이터 확인
                Console.WriteLine($"[UcResultView] 파싱된 Status: {wrapper.Status}");
                Console.WriteLine($"[UcResultView] 파싱된 ErrorValue: {wrapper.ErrorValue}");

                // UI 스레드에서 실행 확인
                if (propertyGrid1.InvokeRequired)
                {
                    propertyGrid1.Invoke(new MethodInvoker(() => {
                        propertyGrid1.SelectedObject = wrapper;
                        propertyGrid1.Refresh();
                    }));
                }
                else
                {
                    propertyGrid1.SelectedObject = wrapper;
                    propertyGrid1.Refresh();
                }

                Console.WriteLine($"[UcResultView] PropertyGrid 업데이트 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UcResultView] DisplayResult 오류: {ex.Message}");
                var errorWrapper = new ResultDisplayWrapper($"ERROR: {ex.Message}\n{result.ResultContent}");

                if (propertyGrid1.InvokeRequired)
                {
                    propertyGrid1.Invoke(new MethodInvoker(() => {
                        propertyGrid1.SelectedObject = errorWrapper;
                        propertyGrid1.Refresh();
                    }));
                }
                else
                {
                    propertyGrid1.SelectedObject = errorWrapper;
                    propertyGrid1.Refresh();
                }
            }
        }

        private void UpdateStatusIcon(ResultData result)
        {
            try
            {
                // ResultData의 Status나 IsValid를 기반으로 아이콘 결정
                if (result.IsValid || result.Status?.ToLower().Contains("success") == true ||
                    result.Status?.ToLower().Contains("통과") == true)
                {
                    statusIcon.Image = CreateStatusIcon(Color.Green, "✓");
                }
                else
                {
                    statusIcon.Image = CreateStatusIcon(Color.Red, "✗");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UcResultView] 상태 아이콘 업데이트 오류: {ex.Message}");
                statusIcon.Image = CreateStatusIcon(Color.Gray, "?");
            }
        }

        public void ClearResults()
        {
            resultHistory.Clear();
            propertyGrid1.SelectedObject = null;
            SetDefaultStatus();
        }

        /// <summary>
        /// 테스트용: 더미 결과 데이터로 PropertyGrid 테스트
        /// </summary>
        public void TestDisplayResult()
        {
            try
            {
                var testResult = new ResultData(
                    "TestNode",
                    "TestType",
                    "STATUS:통과\nERROR:0.5\nTHRESHOLD:1.0\nCOUNT:100\nMEAN:10.5",
                    true
                );

                Console.WriteLine("[UcResultView] 테스트 결과 표시 시작");
                DisplayResult(testResult);
                UpdateStatusIcon(testResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UcResultView] 테스트 실패: {ex.Message}");
            }
        }

        #endregion
    }
}