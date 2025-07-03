using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NodeEditor;
using IFVisionEngine.Manager;
using OpenCvSharp;
using IFVisionEngine.UIComponents.Data;
using IFVisionEngine.UIComponents.Managers;

namespace IFVisionEngine.UIComponents.UserControls
{
    /// <summary>
    /// 노드 실행 결과를 관리하고 시각화하는 사용자 컨트롤
    /// </summary>
    public partial class UcNodeExecutionView : UserControl
    {
        #region Data Classes and Fields

        /// <summary>
        /// 노드의 키와 이름을 매핑하는 데이터 클래스
        /// </summary>
        public class key_name
        {
            public string key { get; set; }
            public string name { get; set; }
        }

        /// <summary>
        /// PropertyGrid에 표시할 결과값 래퍼 클래스
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ResultDisplayWrapper
        {
            [Category("검증 결과")]
            [DisplayName("상태")]
            [Description("검증 통과 여부")]
            public string Status { get; set; }

            [Category("검증 결과")]
            [DisplayName("오차값")]
            [Description("계산된 오차값")]
            public string ErrorValue { get; set; }

            [Category("검증 결과")]
            [DisplayName("임계값")]
            [Description("허용 임계값")]
            public string Threshold { get; set; }

            [Category("통계 정보")]
            [DisplayName("데이터 개수")]
            [Description("분석된 데이터의 개수")]
            public string Count { get; set; }

            [Category("통계 정보")]
            [DisplayName("평균")]
            [Description("데이터의 평균값")]
            public string Mean { get; set; }

            [Category("통계 정보")]
            [DisplayName("표준편차")]
            [Description("데이터의 표준편차")]
            public string StandardDeviation { get; set; }

            [Category("통계 정보")]
            [DisplayName("범위")]
            [Description("최대값과 최소값의 차이")]
            public string Range { get; set; }

            [Category("통계 정보")]
            [DisplayName("변동계수(%)")]
            [Description("상대적 변동성")]
            public string CV { get; set; }

            [Category("상세 정보")]
            [DisplayName("최소값")]
            public string MinValue { get; set; }

            [Category("상세 정보")]
            [DisplayName("최대값")]
            public string MaxValue { get; set; }

            [Category("상세 정보")]
            [DisplayName("실행 시간")]
            [Description("결과 생성 시간")]
            public string ExecutionTime { get; set; }

            [Category("원본 데이터")]
            [DisplayName("전체 결과")]
            [Description("원본 결과 데이터")]
            [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public string FullResult { get; set; }

            public ResultDisplayWrapper(string resultData)
            {
                ParseResultData(resultData);
            }

            private void ParseResultData(string resultData)
            {
                Console.WriteLine($"[ResultDisplayWrapper] 파싱 시작: {resultData}");

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

                        // 새로운 한글 형식 처리
                        else if (trimmedLine.Contains("검증 상태:"))
                            Status = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.Contains("오차 값:"))
                            ErrorValue = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.Contains("허용 임계값:"))
                            Threshold = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.Contains("데이터 개수:"))
                            Count = ExtractValue(trimmedLine, ":");
                        else if (trimmedLine.Contains("평균 길이:"))
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

                    ExecutionTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    FullResult = resultData;

                    // 기본값 설정
                    SetDefaultValues();

                    Console.WriteLine($"[ResultDisplayWrapper] 파싱 완료 - Status: {Status}, Error: {ErrorValue}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ResultDisplayWrapper] 파싱 오류: {ex.Message}");
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

        // 노드 이미지 히스토리 관리
        public List<key_name> nodeImageKeyHistory = new List<key_name>();

        // 결과값 노드 히스토리 관리
        private Dictionary<string, ResultData> resultNodeHistory = new Dictionary<string, ResultData>();

        // 결과값 노드 타입 정의
        private static readonly HashSet<string> ResultNodeTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "LineValidation",
            "QualityCheck",
            "ValidationResult",
            "AnalysisResult",
            "TestResult"
        };

        // 이벤트 핸들러 해제를 위한 플래그
        private bool _eventHandlersConnected = false;
        private bool _disposed = false;
        #endregion

        #region Constructor and Initialization
        public UcNodeExecutionView()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            SetupEventHandlers();
            ConnectToResultsManager();

            Console.WriteLine("[UcNodeExecutionView] 초기화 완료");
        }

        private void SetupEventHandlers()
        {
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            treeView1.AfterSelect += TreeView_AfterSelect;
            treeView1.MouseDown += treeView1_MouseDown;
        }

        private void ConnectToResultsManager()
        {
            if (!_eventHandlersConnected && !_disposed)
            {
                try
                {
                    ResultsManager.Instance.OnResultAdded += OnResultAdded;
                    _eventHandlersConnected = true;
                    Console.WriteLine("[UcNodeExecutionView] ResultsManager 이벤트 연결됨");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[UcNodeExecutionView] ResultsManager 연결 실패: {ex.Message}");
                }
            }
        }

        private void DisconnectFromResultsManager()
        {
            if (_eventHandlersConnected && !_disposed)
            {
                try
                {
                    ResultsManager.Instance.OnResultAdded -= OnResultAdded;
                    _eventHandlersConnected = false;
                    Console.WriteLine("[UcNodeExecutionView] ResultsManager 이벤트 해제됨");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[UcNodeExecutionView] 이벤트 해제 중 오류: {ex.Message}");
                }
            }
        }
        #endregion

        #region Node Execution Management
        /// <summary>
        /// 실행된 노드 정보를 TreeView에 추가
        /// </summary>
        public void AddExecutionData(NodeVisual node)
        {
            if (this.treeView1.InvokeRequired)
            {
                this.treeView1.Invoke(new MethodInvoker(() => AddNodeToTreeView(node)));
            }
            else
            {
                AddNodeToTreeView(node);
            }
        }

        private void AddNodeToTreeView(NodeVisual node)
        {
            if (node == null) return;

            TreeNode treeNode = new TreeNode(node.Name);
            treeNode.Tag = node.GetNodeContext();

            // 결과값 노드인지 확인하고 아이콘 추가 (색상은 기본 유지)
            if (IsResultNode(node.Name))
            {
                treeNode.Text = $"📊 {node.Name}";
                Console.WriteLine($"[UcNodeExecutionView] 결과 노드 추가: {node.Name}");
            }

            this.treeView1.Nodes.Add(treeNode);
        }

        /// <summary>
        /// TreeView의 모든 내용을 삭제
        /// </summary>
        public void ClearData()
        {
            nodeImageKeyHistory.Clear();
            resultNodeHistory.Clear();

            if (this.treeView1.InvokeRequired)
            {
                this.treeView1.Invoke(new MethodInvoker(() => {
                    this.treeView1.Nodes.Clear();
                    this.propertyGrid1.SelectedObject = null;
                }));
            }
            else
            {
                this.treeView1.Nodes.Clear();
                this.propertyGrid1.SelectedObject = null;
            }

            Console.WriteLine("[UcNodeExecutionView] 데이터 초기화 완료");
        }

        private void OnResultAdded(ResultData result)
        {
            if (_disposed) return;

            Console.WriteLine($"[UcNodeExecutionView] 결과 추가됨: {result.NodeName} - {result.Status}");

            if (this.InvokeRequired)
            {
                this.Invoke(new Action<ResultData>(OnResultAdded), result);
                return;
            }

            try
            {
                // 결과값 히스토리에 저장
                resultNodeHistory[result.NodeName] = result;
                Console.WriteLine($"[UcNodeExecutionView] 결과 저장됨: {result.NodeName}");

                // 현재 선택된 노드가 이 결과의 노드이고 결과 노드인 경우에만 PropertyGrid 업데이트
                if (treeView1.SelectedNode != null)
                {
                    string selectedNodeName = GetCleanNodeName(treeView1.SelectedNode.Text);
                    Console.WriteLine($"[UcNodeExecutionView] 현재 선택된 노드: {selectedNodeName}");

                    if (selectedNodeName == result.NodeName && IsResultNode(selectedNodeName))
                    {
                        Console.WriteLine($"[UcNodeExecutionView] 선택된 결과 노드와 일치, PropertyGrid 업데이트");
                        DisplayResultInPropertyGrid(result);
                    }
                }

                // TreeView에서 해당 노드 찾아서 아이콘 업데이트 (색상은 변경하지 않음)
                UpdateTreeNodeAppearance(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UcNodeExecutionView] OnResultAdded 처리 중 오류: {ex.Message}");
            }
        }

        private void UpdateTreeNodeAppearance(ResultData result)
        {
            try
            {
                foreach (TreeNode node in treeView1.Nodes)
                {
                    string nodeName = GetCleanNodeName(node.Text);
                    if (nodeName == result.NodeName)
                    {
                        // 색상은 변경하지 않고 아이콘만 추가
                        if (!node.Text.StartsWith("📊"))
                        {
                            node.Text = $"📊 {node.Text}";
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UcNodeExecutionView] TreeNode 업데이트 중 오류: {ex.Message}");
            }
        }
        #endregion

        #region TreeView Event Handlers
        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;

            string nodeName = GetCleanNodeName(e.Node.Text);
            Console.WriteLine($"[UcNodeExecutionView] 노드 선택됨: {nodeName}");

            // 결과값 노드인 경우에만 결과 표시, 다른 노드는 원래 기능만 작동
            if (IsResultNode(nodeName) && resultNodeHistory.ContainsKey(nodeName))
            {
                Console.WriteLine($"[UcNodeExecutionView] 결과 노드 표시: {nodeName}");
                DisplayResultInPropertyGrid(resultNodeHistory[nodeName]);
            }
            else
            {
                Console.WriteLine($"[UcNodeExecutionView] 일반 노드 표시: {nodeName}");
                this.propertyGrid1.SelectedObject = e.Node.Tag;
                // PropertyGrid 색상을 기본값으로 유지
                ResetPropertyGridAppearance();
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                string nodeText = GetCleanNodeName(treeView1.SelectedNode.Text);

                if (nodeText == "시작점")
                    return;

                // 결과값 노드인 경우에만 결과 표시
                if (IsResultNode(nodeText))
                {
                    if (resultNodeHistory.ContainsKey(nodeText))
                    {
                        DisplayResultInPropertyGrid(resultNodeHistory[nodeText]);
                    }
                    return;
                }

                // 일반 노드인 경우 원래 기능 (이미지 표시)
                DisplayNodeImage(nodeText);
                ResetPropertyGridAppearance();
            }
        }

        private void DisplayNodeImage(string nodeText)
        {
            var found = nodeImageKeyHistory.FirstOrDefault(x => x.name == nodeText);
            if (found != null)
            {
                Mat image = ImageDataManager.GetImage(found.key);
                if (image != null && !image.Empty())
                {
                    AppUIManager.ucImageControler.DisplayImage(image);
                }
                else
                {
                    MessageBox.Show("이미지를 찾을 수 없습니다.");
                }
            }
            else
            {
                MessageBox.Show("리스트에 해당 노드 이름이 없습니다.");
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            var clickedNode = treeView1.GetNodeAt(e.X, e.Y);
            if (clickedNode != null)
                treeView1.SelectedNode = clickedNode;
        }
        #endregion

        #region Result Display Methods
        private void DisplayResultInPropertyGrid(ResultData result)
        {
            try
            {
                Console.WriteLine($"[UcNodeExecutionView] PropertyGrid에 결과 표시 시작: {result.NodeName}");

                var wrapper = new ResultDisplayWrapper(result.ResultContent);
                this.propertyGrid1.SelectedObject = wrapper;

                // PropertyGrid 색상을 흰색으로 유지 (기본값)
                ResetPropertyGridAppearance();

                Console.WriteLine($"[UcNodeExecutionView] PropertyGrid 결과 표시 완료: {result.NodeName} - {result.Status}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UcNodeExecutionView] 결과 표시 오류: {ex.Message}");

                // 오류 발생 시 원본 데이터라도 표시
                var errorWrapper = new ResultDisplayWrapper($"ERROR: {ex.Message}\n{result.ResultContent}");
                this.propertyGrid1.SelectedObject = errorWrapper;
                ResetPropertyGridAppearance();
            }
        }

        private void ResetPropertyGridAppearance()
        {
            // PropertyGrid를 기본 흰색 배경으로 유지
            this.propertyGrid1.LineColor = SystemColors.InactiveBorder;
            this.propertyGrid1.BackColor = Color.White;
        }

        private bool IsResultNode(string nodeName)
        {
            if (string.IsNullOrEmpty(nodeName)) return false;

            string cleanName = GetCleanNodeName(nodeName);
            bool isResult = ResultNodeTypes.Any(type => cleanName.Contains(type));

            if (isResult)
            {
                Console.WriteLine($"[UcNodeExecutionView] 결과 노드 감지: {cleanName}");
            }

            return isResult;
        }

        private string GetCleanNodeName(string nodeText)
        {
            if (string.IsNullOrEmpty(nodeText)) return "";

            // 아이콘 제거
            return nodeText.Replace("📊 ", "").Trim();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 외부에서 결과값을 직접 추가
        /// </summary>
        public void AddResultData(string nodeName, string resultContent, bool isValid)
        {
            try
            {
                Console.WriteLine($"[UcNodeExecutionView] 외부에서 결과 추가: {nodeName}");
                var result = new ResultData(nodeName, "ManualResult", resultContent, isValid);
                ResultsManager.Instance.AddResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UcNodeExecutionView] 외부 결과 추가 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 특정 노드의 결과값을 강제로 표시
        /// </summary>
        public void ShowNodeResult(string nodeName)
        {
            try
            {
                Console.WriteLine($"[UcNodeExecutionView] 노드 결과 강제 표시: {nodeName}");

                if (resultNodeHistory.ContainsKey(nodeName))
                {
                    DisplayResultInPropertyGrid(resultNodeHistory[nodeName]);

                    // TreeView에서 해당 노드 선택
                    foreach (TreeNode node in treeView1.Nodes)
                    {
                        if (GetCleanNodeName(node.Text) == nodeName)
                        {
                            treeView1.SelectedNode = node;
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"[UcNodeExecutionView] 결과를 찾을 수 없음: {nodeName}");

                    // ResultsManager에서 다시 찾기 시도
                    var result = ResultsManager.Instance.GetLatestResult(nodeName);
                    if (result != null)
                    {
                        Console.WriteLine($"[UcNodeExecutionView] ResultsManager에서 결과 찾음: {nodeName}");
                        resultNodeHistory[nodeName] = result;
                        DisplayResultInPropertyGrid(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UcNodeExecutionView] ShowNodeResult 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 현재 표시된 결과값의 요약 정보를 반환
        /// </summary>
        public string GetCurrentResultSummary()
        {
            if (propertyGrid1.SelectedObject is ResultDisplayWrapper wrapper)
            {
                return $"상태: {wrapper.Status}, 오차: {wrapper.ErrorValue}";
            }
            return "결과 없음";
        }

        /// <summary>
        /// 현재 저장된 결과값 개수를 반환
        /// </summary>
        public int GetResultCount()
        {
            return resultNodeHistory.Count;
        }

        /// <summary>
        /// 모든 결과값 목록을 반환
        /// </summary>
        public Dictionary<string, ResultData> GetAllResults()
        {
            return new Dictionary<string, ResultData>(resultNodeHistory);
        }

        /// <summary>
        /// 결과값 히스토리를 텍스트로 내보내기
        /// </summary>
        public string ExportResultsToText()
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== 노드 실행 결과 요약 ===");
            sb.AppendLine($"생성 시간: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"총 결과 수: {resultNodeHistory.Count}");
            sb.AppendLine();

            foreach (var kvp in resultNodeHistory)
            {
                sb.AppendLine($"노드: {kvp.Key}");
                sb.AppendLine($"상태: {kvp.Value.Status}");
                sb.AppendLine($"시간: {kvp.Value.Timestamp:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine("결과:");
                sb.AppendLine(kvp.Value.ResultContent);
                sb.AppendLine(new string('-', 50));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 강제로 결과 매니저와 다시 연결
        /// </summary>
        public void ReconnectToResultsManager()
        {
            DisconnectFromResultsManager();
            ConnectToResultsManager();
            Console.WriteLine("[UcNodeExecutionView] ResultsManager 재연결 완료");
        }
        #endregion
    }
}