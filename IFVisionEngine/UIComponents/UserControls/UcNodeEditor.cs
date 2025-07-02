using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NodeEditor;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.Linq.Expressions;
using System.Xml.Linq;
using static MyNodesContext;
using System.Reflection;

namespace IFVisionEngine.UIComponents.UserControls
{
    /// <summary>
    /// 노드 기반 비주얼 프로그래밍 에디터를 제공하는 사용자 컨트롤입니다.
    /// 노드 생성, 연결, 실행 및 파라미터 설정 기능을 포함합니다.
    /// </summary>
    public partial class UcNodeEditor : UserControl
    {
        #region Fields and Properties
        // 1. 메인 폼 및 노드 컨텍스트 참조
        public MyNodesContext _nodesContext;   // 노드 에디터의 컨텍스트 (노드 관리 및 실행)
        private Form1 _formMainInstance;       // 메인 폼 인스턴스 참조

        // 2. 이벤트 정의
        public event Action<List<NodeVisual>> NodeSelectionChanged;    // 노드 선택 변경시 발생하는 이벤트
        public event Action<object> SelectedNodeContextChanged;        // 선택된 노드 컨텍스트 변경시 발생하는 이벤트

        // 3. 클릭/드래그/더블클릭 상태 관리 변수
        private bool _nodeWasJustSelected = false;      // 노드가 방금 선택되었는지 여부
        private bool _isDragging = false;               // 현재 드래그 중인지 여부
        private bool _isDoubleClick = false;            // 더블클릭 감지 여부
        private DateTime _firstClickTime = DateTime.MinValue;  // 첫 번째 클릭 시간 (더블클릭 판정용)
        private Timer _doubleClickTimer;                // 더블클릭 타이머

        // 4. 더블클릭 관련 상수
        private const int DOUBLECLICK_TIMEOUT = 300;    // 더블클릭 허용 시간 (밀리초)
        private const int DOUBLECLICK_RESET_DELAY = 100; // 더블클릭 플래그 리셋 지연시간 (밀리초)

        // 5. 중심좌표 저장 관련
        private Dictionary<string, CentroidData> _storedCentroids = new Dictionary<string, CentroidData>();
        private CentroidData _lastCentroid = null;  // 마지막으로 감지된 중심좌표

        /// <summary>
        /// 중심좌표 데이터를 저장하는 클래스
        /// </summary>
        public class CentroidData
        {
            public double X { get; set; }
            public double Y { get; set; }

            public CentroidData(double x, double y)
            {
                X = x;
                Y = y;
            }
        }
        #endregion

        #region Constructor and Initialization
        /// <summary>
        /// 생성자 - 메인폼 참조, 컨트롤 Dock, 노드 컨텍스트 초기화
        /// </summary>
        /// <param name="mainForm">메인 폼 인스턴스</param>
        public UcNodeEditor(Form1 mainForm)
        {
            InitializeComponent();
            _formMainInstance = mainForm;
            this.Dock = DockStyle.Fill;

            InitializeNodeContext();
        }

        /// <summary>
        /// 노드 컨텍스트를 초기화하고 노드 컨트롤에 연결합니다.
        /// </summary>
        private void InitializeNodeContext()
        {
            _nodesContext = new MyNodesContext();
            _nodesContext.Invoker = this;
            nodesControl1.Context = _nodesContext;
        }

        /// <summary>
        /// 사용자 컨트롤 로드 완료 시 노드 컨트롤을 실행합니다.
        /// </summary>
        private void UcNodeEditor_Load(object sender, EventArgs e)
        {
            nodesControl1.Execute();
        }

        /// <summary>
        /// 현재 노드 컨텍스트를 반환합니다.
        /// </summary>
        /// <returns>MyNodesContext 인스턴스</returns>
        public MyNodesContext GetContext() => _nodesContext;
        #endregion

        #region Centroid Management
        /// <summary>
        /// 노드의 컨텍스트에서 중심좌표 정보를 추출합니다.
        /// </summary>
        /// <param name="node">대상 노드</param>
        /// <returns>중심좌표 데이터 또는 null</returns>
        private CentroidData ExtractCentroidFromNodeContext(NodeVisual node)
        {
            try
            {
                dynamic ctx = node.GetNodeContext();
                if (ctx is DynamicNodeContext dynCtx)
                {
                    // 노드 컨텍스트의 모든 멤버를 확인
                    foreach (var memberName in dynCtx.GetDynamicMemberNames())
                    {
                        try
                        {
                            var memberValue = dynCtx[memberName];
                            if (memberValue != null)
                            {
                                // moments 관련 데이터에서 중심좌표 추출
                                var centroid = ExtractCentroidFromData(memberValue);
                                if (centroid != null)
                                {
                                    LogDebug($"노드 컨텍스트에서 중심좌표 추출: ({centroid.X:F2}, {centroid.Y:F2})");
                                    return centroid;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogDebug($"멤버 '{memberName}' 확인 중 오류: {ex.Message}");
                        }
                    }

                    // 특별히 'input' 또는 'output' 키로 저장된 데이터 확인
                    try
                    {
                        var inputData = dynCtx["input"];
                        if (inputData != null)
                        {
                            var centroid = ExtractCentroidFromData(inputData);
                            if (centroid != null) return centroid;
                        }
                    }
                    catch { }

                    try
                    {
                        var outputData = dynCtx["output"];
                        if (outputData != null)
                        {
                            var centroid = ExtractCentroidFromData(outputData);
                            if (centroid != null) return centroid;
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                LogDebug($"중심좌표 추출 중 오류: {ex.Message}");
            }
            return null;
        }

        /// <summary>
        /// 데이터 객체에서 중심좌표를 추출합니다.
        /// </summary>
        /// <param name="data">입력 데이터</param>
        /// <returns>중심좌표 데이터 또는 null</returns>
        private CentroidData ExtractCentroidFromData(object data)
        {
            try
            {
                // 1. Dictionary 형태의 moments 데이터
                if (data is Dictionary<string, object> dict)
                {
                    if (dict.ContainsKey("CentroidX") && dict.ContainsKey("CentroidY"))
                    {
                        if (double.TryParse(dict["CentroidX"].ToString(), out double x) &&
                            double.TryParse(dict["CentroidY"].ToString(), out double y))
                        {
                            return new CentroidData(x, y);
                        }
                    }

                    // m10/m00, m01/m00 형태로 저장된 경우
                    if (dict.ContainsKey("m10") && dict.ContainsKey("m01") && dict.ContainsKey("m00"))
                    {
                        if (double.TryParse(dict["m10"].ToString(), out double m10) &&
                            double.TryParse(dict["m01"].ToString(), out double m01) &&
                            double.TryParse(dict["m00"].ToString(), out double m00) && m00 != 0)
                        {
                            return new CentroidData(m10 / m00, m01 / m00);
                        }
                    }
                }

                // 2. Dynamic 객체
                if (data is IDynamicMetaObjectProvider dynData)
                {
                    dynamic dyn = data;
                    try
                    {
                        if (dyn.CentroidX != null && dyn.CentroidY != null)
                        {
                            return new CentroidData((double)dyn.CentroidX, (double)dyn.CentroidY);
                        }
                    }
                    catch { }
                }

                // 3. 문자열 형태 "x,y"
                if (data is string str && str.Contains(","))
                {
                    var parts = str.Split(',');
                    if (parts.Length >= 2 &&
                        double.TryParse(parts[0].Trim(), out double x) &&
                        double.TryParse(parts[1].Trim(), out double y))
                    {
                        return new CentroidData(x, y);
                    }
                }

                // 4. OpenCV Moments 객체 (리플렉션 사용)
                var dataType = data.GetType();
                if (dataType.Name.Contains("Moments"))
                {
                    var m10Prop = dataType.GetProperty("M10");
                    var m01Prop = dataType.GetProperty("M01");
                    var m00Prop = dataType.GetProperty("M00");

                    if (m10Prop != null && m01Prop != null && m00Prop != null)
                    {
                        double m10 = Convert.ToDouble(m10Prop.GetValue(data));
                        double m01 = Convert.ToDouble(m01Prop.GetValue(data));
                        double m00 = Convert.ToDouble(m00Prop.GetValue(data));

                        if (m00 != 0)
                        {
                            return new CentroidData(m10 / m00, m01 / m00);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogDebug($"데이터에서 중심좌표 추출 실패: {ex.Message}");
            }
            return null;
        }

        /// <summary>
        /// 노드별로 중심좌표를 저장합니다.
        /// </summary>
        /// <param name="nodeId">노드 ID</param>
        /// <param name="centroid">중심좌표</param>
        private void StoreCentroidForNode(string nodeId, CentroidData centroid)
        {
            _storedCentroids[nodeId] = centroid;
            _lastCentroid = centroid;
            LogDebug($"노드 '{nodeId}'의 중심좌표 저장: ({centroid.X:F2}, {centroid.Y:F2})");
        }

        /// <summary>
        /// 저장된 중심좌표를 반환합니다.
        /// </summary>
        /// <returns>마지막 중심좌표 또는 null</returns>
        public CentroidData GetLastCentroid()
        {
            return _lastCentroid;
        }

        /// <summary>
        /// 특정 노드의 저장된 중심좌표를 반환합니다.
        /// </summary>
        /// <param name="nodeId">노드 ID</param>
        /// <returns>중심좌표 또는 null</returns>
        public CentroidData GetStoredCentroid(string nodeId)
        {
            return _storedCentroids.ContainsKey(nodeId) ? _storedCentroids[nodeId] : null;
        }
        #endregion

        #region Toolbar Events
        /// <summary>
        /// '저장' 버튼 클릭 - 노드 레이아웃을 JSON 파일로 저장
        /// </summary>
        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog
            {
                Filter = "JSON 파일 (*.json)|*.json|모든 파일 (*.*)|*.*",
                Title = "노드 레이아웃 저장"
            })
            {
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveNodeLayoutToJson(nodesControl1, saveDialog.FileName);
                }
            }
        }

        /// <summary>
        /// '불러오기' 버튼 클릭 - JSON 파일에서 노드 레이아웃을 불러옴
        /// </summary>
        private void toolStripButton_Load_Click(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog
            {
                Filter = "JSON 파일 (*.json)|*.json|모든 파일 (*.*)|*.*",
                Title = "노드 레이아웃 불러오기",
                CheckFileExists = true,
                CheckPathExists = true
            })
            {
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadNodeLayoutFromJson(nodesControl1, openDialog.FileName);
                }
            }
        }

        /// <summary>
        /// '크기 토글' 버튼 클릭 - 왼쪽 패널 크기를 토글
        /// </summary>
        private void toolStripButton_toggleSize_Click(object sender, EventArgs e)
        {
            _formMainInstance.togglePnlLeft();
        }

        /// <summary>
        /// '실행' 버튼 클릭 - 실행 뷰를 초기화하고 노드를 실행
        /// </summary>
        private void toolStripButton_Run_Click(object sender, EventArgs e)
        {
            IFVisionEngine.Manager.AppUIManager.ucNodeExecutionView.ClearData();
            nodesControl1.Execute();
        }
        #endregion

        #region Save/Load Methods
        /// <summary>
        /// 노드 레이아웃을 JSON 파일로 저장합니다.
        /// </summary>
        /// <param name="nodesCtrl">노드 컨트롤 인스턴스</param>
        /// <param name="filePath">저장할 파일 경로</param>
        public void SaveNodeLayoutToJson(NodesControl nodesCtrl, string filePath)
        {
            try
            {
                // 1. 노드 컨트롤을 직렬화
                byte[] serializedData = nodesCtrl.Serialize();
                if (serializedData == null)
                {
                    Console.WriteLine("직렬화 실패");
                    return;
                }

                // 2. Base64로 인코딩하여 JSON 형태로 래핑
                string base64Data = Convert.ToBase64String(serializedData);
                var jsonData = new { NodeGraphData = base64Data };
                string jsonString = System.Text.Json.JsonSerializer.Serialize(jsonData,
                    new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

                // 3. 파일로 저장
                System.IO.File.WriteAllText(filePath, jsonString, Encoding.UTF8);
                MessageBox.Show("레이아웃이 저장되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"저장 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// JSON 파일에서 노드 레이아웃을 불러옵니다.
        /// </summary>
        /// <param name="nodesCtrl">노드 컨트롤 인스턴스</param>
        /// <param name="filePath">불러올 파일 경로</param>
        public void LoadNodeLayoutFromJson(NodesControl nodesCtrl, string filePath)
        {
            try
            {
                // 1. 파일 존재 여부 확인
                if (!System.IO.File.Exists(filePath))
                {
                    MessageBox.Show("파일을 찾을 수 없습니다.");
                    return;
                }

                // 2. JSON 파일 읽기 및 역직렬화
                string jsonString = System.IO.File.ReadAllText(filePath, Encoding.UTF8);
                var jsonData = System.Text.Json.JsonSerializer.Deserialize<JsonNodeGraphWrapper>(jsonString);

                if (jsonData == null || string.IsNullOrEmpty(jsonData.NodeGraphData))
                {
                    MessageBox.Show("데이터 읽기 실패");
                    return;
                }

                // 3. Base64 디코딩하여 노드 컨트롤에 적용
                byte[] serializedData = Convert.FromBase64String(jsonData.NodeGraphData);
                nodesCtrl.Deserialize(serializedData);
                MessageBox.Show("레이아웃을 불러왔습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"로드 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// JSON 노드 그래프 데이터를 래핑하는 클래스
        /// </summary>
        public class JsonNodeGraphWrapper
        {
            public string NodeGraphData { get; set; }
        }
        #endregion

        #region Mouse Events
        /// <summary>
        /// 마우스 버튼 누름 - 더블클릭이 아닌 경우 드래그 상태로 전환
        /// </summary>
        private void nodesControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_isDoubleClick) _isDragging = true;
        }

        /// <summary>
        /// 마우스 이동 - 더블클릭 또는 드래그 상태가 아닌 경우에만 처리
        /// </summary>
        private void nodesControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDoubleClick || !_isDragging) return;
            // (드래그 로직이 있다면 여기에)
        }

        /// <summary>
        /// 마우스 버튼 뗌 - 드래그 종료 및 선택 상태 초기화
        /// </summary>
        private void nodesControl1_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            if (!_nodeWasJustSelected && !_isDoubleClick)
                SelectedNodeContextChanged?.Invoke(null);
            _nodeWasJustSelected = false;
        }
        #endregion

        #region Node Selection and Parameter Handling
        /// <summary>
        /// 노드 컨텍스트 선택 시 호출 - 더블클릭 감지 및 파라미터 다이얼로그 표시
        /// </summary>
        /// <param name="obj">선택된 노드 객체</param>
        private void nodesControl1_OnNodeContextSelected(object obj)
        {
            DetectDoubleClick();

            if (_isDoubleClick)
            {
                // 더블클릭 시 상태 초기화 및 더블클릭 처리
                _isDragging = false;
                _nodeWasJustSelected = false;
                ForceResetNodeEditorDragState();
                HandleDoubleClick(obj);
            }
            else
            {
                _nodeWasJustSelected = true;

                // 노드 선택 시 중심좌표 추출 시도
                TryExtractAndStoreCentroid(obj);
            }
            SelectedNodeContextChanged?.Invoke(obj);
        }

        /// <summary>
        /// 선택된 노드에서 중심좌표를 추출하고 저장을 시도합니다.
        /// </summary>
        /// <param name="obj">선택된 노드 객체</param>
        private void TryExtractAndStoreCentroid(object obj)
        {
            try
            {
                var nodeInfo = ExtractNodeInfo(obj);

                // RadialLines 노드이거나 중심좌표가 필요한 노드인 경우
                if (IsNodeTypeThatNeedsCentroid(nodeInfo.NodeName))
                {
                    // 해당 노드를 찾아서 중심좌표 추출
                    string simpleNodeName = ExtractNodeName(nodeInfo.NodeName);
                    var nodes = nodesControl1.GetNodes(simpleNodeName);

                    if (nodes.Count > 0)
                    {
                        var node = nodes[0];
                        var centroid = ExtractCentroidFromNodeContext(node);

                        if (centroid != null)
                        {
                            StoreCentroidForNode(nodeInfo.NodeName, centroid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogDebug($"중심좌표 추출 시도 중 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 노드가 중심좌표가 필요한 타입인지 확인합니다.
        /// </summary>
        /// <param name="nodeName">노드 이름</param>
        /// <returns>중심좌표가 필요한 노드 여부</returns>
        private bool IsNodeTypeThatNeedsCentroid(string nodeName)
        {
            if (string.IsNullOrEmpty(nodeName)) return false;

            string nodeType = ExtractNodeName(nodeName);
            return nodeType.Equals("RadialLines", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 더블클릭을 감지합니다 (시간 기반 판정)
        /// </summary>
        private void DetectDoubleClick()
        {
            var now = DateTime.Now;
            if ((now - _firstClickTime).TotalMilliseconds > DOUBLECLICK_TIMEOUT)
            {
                // 타임아웃 초과 시 첫 번째 클릭으로 간주
                _firstClickTime = now;
                _isDoubleClick = false;
            }
            else
            {
                // 타임아웃 내 클릭 시 더블클릭으로 판정
                _isDoubleClick = true;
                _firstClickTime = DateTime.MinValue;
                ResetDoubleClickFlag();
            }
        }

        /// <summary>
        /// 더블클릭 플래그를 지연 후 리셋합니다.
        /// </summary>
        private void ResetDoubleClickFlag()
        {
            if (_doubleClickTimer == null)
            {
                _doubleClickTimer = new Timer();
                _doubleClickTimer.Interval = DOUBLECLICK_RESET_DELAY;
                _doubleClickTimer.Tick += (s, args) => {
                    _isDoubleClick = false;
                    _isDragging = false;
                    ForceResetNodeEditorDragState();
                    _doubleClickTimer.Stop();
                };
            }
            _doubleClickTimer.Stop();
            _doubleClickTimer.Start();
        }

        /// <summary>
        /// 현재 노드의 파라미터를 가져옵니다.
        /// </summary>
        /// <param name="nodeName">노드 이름</param>
        /// <param name="nodeContext">노드 컨텍스트</param>
        /// <returns>파라미터 딕셔너리</returns>
        private IDictionary<string, object> GetCurrentNodeParameters(string nodeName, object nodeContext)
        {
            var parameters = new Dictionary<string, object>();
            try
            {
                // 1. 노드 이름에서 단순 이름 추출
                string simpleNodeName = ExtractNodeName(nodeName);
                var nodes = nodesControl1.GetNodes(simpleNodeName);
                if (nodes.Count > 0)
                {
                    var node = nodes[0];
                    dynamic ctx = node.GetNodeContext();

                    // 2. DynamicNodeContext에서 파라미터 추출
                    if (ctx is DynamicNodeContext dynCtx)
                    {
                        var currentParamObj = dynCtx[nodeName];
                        if (currentParamObj != null)
                        {
                            // 3. 리플렉션을 통해 모든 속성을 딕셔너리로 변환
                            var paramType = currentParamObj.GetType();
                            var properties = paramType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                            foreach (var prop in properties)
                            {
                                if (prop.CanRead)
                                    parameters[prop.Name] = prop.GetValue(currentParamObj);
                            }
                        }
                    }

                    // 4. RadialLines 노드인 경우 중심좌표 정보 추가
                    if (IsNodeTypeThatNeedsCentroid(nodeName))
                    {
                        var centroid = GetLastCentroid();
                        if (centroid != null)
                        {
                            // moments 형태로도 추가 (기존 코드와의 호환성)
                            parameters["moments"] = new Dictionary<string, object>
                            {
                                ["CentroidX"] = centroid.X,
                                ["CentroidY"] = centroid.Y
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[파라미터 추출 실패] {ex.Message}");
            }
            return parameters;
        }

        /// <summary>
        /// 더블클릭 시 호출되어 노드 파라미터 다이얼로그를 표시합니다.
        /// </summary>
        /// <param name="obj">선택된 노드 객체</param>
        private void HandleDoubleClick(object obj)
        {
            var nodeInfo = ExtractNodeInfo(obj);

            if (nodeInfo.HasParameters)
            {
                // 상태 초기화 후 파라미터 다이얼로그 표시
                ForceResetAllStates();
                var currentParameters = GetCurrentNodeParameters(nodeInfo.NodeName, obj);
                ShowParameterDialog(nodeInfo, currentParameters);
                ForceResetAllStates();
            }
        }

        /// <summary>
        /// 노드 객체에서 노드 정보를 추출합니다.
        /// </summary>
        /// <param name="obj">노드 객체</param>
        /// <returns>노드 정보 튜플</returns>
        private (string InputKey, string NodeName, string OutputKey, bool HasParameters) ExtractNodeInfo(object obj)
        {
            string inputKey = null;
            string nodeName = null;
            string outputKey = null;
            bool hasParameters = false;

            // 동적 객체에서 멤버 정보 추출
            if (obj is IDynamicMetaObjectProvider dynObj)
            {
                foreach (var memberName in dynObj.GetMetaObject(Expression.Constant(dynObj)).GetDynamicMemberNames())
                {
                    // 파라미터 존재 여부 확인
                    if (memberName?.ToLower().Contains("parameters") == true)
                    {
                        hasParameters = true;
                        nodeName = memberName;
                    }
                    var value = ((dynamic)obj)[memberName]?.ToString();
                    switch (memberName.ToLower())
                    {
                        case "input": inputKey = value; break;
                        case "output": outputKey = value; break;
                    }
                }
            }
            return (inputKey, nodeName, outputKey, hasParameters);
        }

        /// <summary>
        /// 파라미터 설정 다이얼로그를 표시합니다.
        /// </summary>
        /// <param name="nodeInfo">노드 정보</param>
        /// <param name="currentParameters">현재 파라미터 값들</param>
        private void ShowParameterDialog(
            (string InputKey, string NodeName, string OutputKey, bool HasParameters) nodeInfo,
            IDictionary<string, object> currentParameters = null)
        {
            using (var dlg = new IFVisionEngine.UIComponents.Dialogs.FormParameters(
                nodeInfo.InputKey,
                nodeInfo.NodeName,
                nodeInfo.OutputKey,
                currentParameters))
            {
                ForceResetAllStates();
                if (dlg.ShowDialog() == DialogResult.OK)
                    SetNodeParametersAndExecute(nodeInfo.NodeName, dlg.SelectedParameters);
                ForceResetAllStates();
            }
        }

        /// <summary>
        /// 모든 상태를 강제로 리셋합니다.
        /// </summary>
        private void ForceResetAllStates()
        {
            _isDragging = false;
            _nodeWasJustSelected = false;
            _isDoubleClick = false;
            ForceResetNodeEditorDragState();
        }

        /// <summary>
        /// 노드 에디터의 내부 드래그 상태를 강제로 리셋합니다.
        /// (리플렉션을 통한 내부 필드 접근)
        /// </summary>
        private void ForceResetNodeEditorDragState()
        {
            try
            {
                var nodesControlType = nodesControl1.GetType();

                // 1. 마우스 다운 상태 리셋
                var mdownField = nodesControlType.GetField("mdown",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                mdownField?.SetValue(nodesControl1, false);

                // 2. 드래그 소켓 상태 리셋
                var dragSocketField = nodesControlType.GetField("dragSocket",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                dragSocketField?.SetValue(nodesControl1, null);

                // 3. 화면 갱신
                nodesControl1.Invalidate();
                nodesControl1.Update();
            }
            catch (Exception ex)
            {
                // 리플렉션 실패 시 대안적 방법 (커서 이동)
                Console.WriteLine($"[경고] NodeEditor 내부 상태 리셋 실패: {ex.Message}");
                var currentPos = Cursor.Position;
                Cursor.Position = new Point(currentPos.X + 1000, currentPos.Y + 1000);
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
                Cursor.Position = currentPos;
                Application.DoEvents();
            }
        }
        #endregion

        #region Parameter Setting
        /// <summary>
        /// 노드 파라미터를 설정하고 실행합니다.
        /// </summary>
        /// <param name="nodeName">노드 이름</param>
        /// <param name="param">설정할 파라미터</param>
        private void SetNodeParametersAndExecute(string nodeName, object param)
        {
            // 1. 단순 노드 이름 추출 및 노드 검색
            string simpleNodeName = ExtractNodeName(nodeName);
            var nodes = nodesControl1.GetNodes(simpleNodeName);
            if (nodes.Count == 0) return;
            var node = nodes[0];

            // 2. 파라미터 설정 및 노드 실행
            if (!SetNodeParameters(node, nodeName, param)) return;
            IFVisionEngine.Manager.AppUIManager.ucNodeExecutionView.ClearData();
            ExecuteNode(node);
        }

        /// <summary>
        /// 특정 노드에 파라미터를 설정합니다.
        /// </summary>
        /// <param name="node">대상 노드</param>
        /// <param name="paramName">파라미터 이름</param>
        /// <param name="param">파라미터 값</param>
        /// <returns>설정 성공 여부</returns>
        private bool SetNodeParameters(NodeVisual node, string paramName, object param)
        {
            dynamic ctx = node.GetNodeContext();

            if (ctx is DynamicNodeContext dynCtx && param is IDictionary<string, object> dict)
            {
                // 1. 노드 타입 추출 및 파라미터 객체 생성
                var nodeType = ExtractNodeName(paramName);
                object paramObject = CreateParameterObject(nodeType, dict);
                if (paramObject != null)
                {
                    try
                    {
                        // 2. 동적 컨텍스트에 파라미터 설정
                        dynCtx[paramName] = paramObject;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[파라미터 설정 실패] {ex.Message}");
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 노드 이름에서 "parameters" 접미사를 제거하여 단순 이름을 추출합니다.
        /// </summary>
        /// <param name="nodeName">전체 노드 이름</param>
        /// <returns>단순 노드 이름</returns>
        private string ExtractNodeName(string nodeName)
        {
            const string suffix = "parameters";
            if (!string.IsNullOrEmpty(nodeName) && nodeName.EndsWith(suffix))
                return nodeName.Substring(0, nodeName.Length - suffix.Length);
            return nodeName;
        }

        /// <summary>
        /// 노드 타입에 따라 적절한 파라미터 객체를 생성합니다.
        /// </summary>
        /// <param name="nodeType">노드 타입</param>
        /// <param name="dict">파라미터 딕셔너리</param>
        /// <returns>생성된 파라미터 객체</returns>
        private object CreateParameterObject(string nodeType, IDictionary<string, object> dict)
        {
            try
            {
                switch (nodeType)
                {
                    case "Edge": return CreateEdgeParameters(dict);
                    case "GaussianBlur": return CreateGaussianBlurParameters(dict);
                    case "CLAHE": return CreateClaheParameters(dict);
                    case "Binarization": return CreateBinarizationParameters(dict);
                    case "Contour": return CreateContourParameters(dict);
                    case "Moments": return CreateMomentsParameters(dict);
                    case "RadialLines": return CreateRadialLinesParameters(dict);
                    case "Grayscale": return new GrayscaleParameters();
                    default: return LogAndReturnNull($"알 수 없는 노드 타입: {nodeType}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[파라미터 객체 생성 실패] {ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// RadialLines 파라미터 객체를 생성합니다.
        /// </summary>
        private RadialLinesParameters CreateRadialLinesParameters(IDictionary<string, object> dict)
        {
            var param = new RadialLinesParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }
        /// <summary>
        /// Contour 파라미터 객체를 생성합니다.
        /// </summary>
        private ContourParameters CreateContourParameters(IDictionary<string, object> dict)
        {
            var param = new ContourParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }
        /// <summary>
        /// Moments 파라미터 객체를 생성합니다.
        /// </summary>
        private MomentsParameters CreateMomentsParameters(IDictionary<string, object> dict)
        {
            var param = new MomentsParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }
        /// <summary>
        /// Edge Detection 파라미터 객체를 생성합니다.
        /// </summary>
        private EdgeDetectionParameters CreateEdgeParameters(IDictionary<string, object> dict)
        {
            var param = new EdgeDetectionParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }

        /// <summary>
        /// Gaussian Blur 파라미터 객체를 생성합니다.
        /// </summary>
        private GaussianBlurParameters CreateGaussianBlurParameters(IDictionary<string, object> dict)
        {
            var param = new GaussianBlurParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }

        /// <summary>
        /// CLAHE 파라미터 객체를 생성합니다.
        /// </summary>
        private ClaheParameters CreateClaheParameters(IDictionary<string, object> dict)
        {
            var param = new ClaheParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }

        /// <summary>
        /// Binarization 파라미터 객체를 생성합니다.
        /// </summary>
        private BinarizationParameters CreateBinarizationParameters(IDictionary<string, object> dict)
        {
            var param = new BinarizationParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }

        /// <summary>
        /// 딕셔너리의 값들을 대상 객체의 속성에 설정합니다.
        /// </summary>
        /// <param name="targetObject">대상 객체</param>
        /// <param name="dict">속성값 딕셔너리</param>
        private void SetPropertiesFromDictionary(object targetObject, IDictionary<string, object> dict)
        {
            Type type = targetObject.GetType();

            foreach (var kv in dict)
            {
                var prop = type.GetProperty(kv.Key);
                if (prop?.CanWrite == true)
                {
                    try
                    {
                        // 열거형 처리 및 타입 변환
                        object value = prop.PropertyType.IsEnum && kv.Value is string enumStr
                            ? Enum.Parse(prop.PropertyType, enumStr)
                            : Convert.ChangeType(kv.Value, prop.PropertyType);
                        prop.SetValue(targetObject, value);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[속성 '{kv.Key}' 설정 실패] {ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// 특정 노드를 실행합니다.
        /// </summary>
        /// <param name="node">실행할 노드</param>
        private void ExecuteNode(NodeVisual node)
        {
            try
            {
                nodesControl1.Execute(node);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[노드 실행 실패] {ex.Message}\n{ex.StackTrace}");
            }
        }
        #endregion

        #region Logging
        /// <summary>
        /// 디버그 로그를 출력합니다.
        /// </summary>
        /// <param name="message">로그 메시지</param>
        private void LogDebug(string message) => Console.WriteLine($"[디버그] {message}");

        /// <summary>
        /// 경고 로그를 출력합니다.
        /// </summary>
        /// <param name="message">로그 메시지</param>
        private void LogWarning(string message) => Console.WriteLine($"[경고] {message}");

        /// <summary>
        /// 오류 로그를 출력합니다.
        /// </summary>
        /// <param name="message">로그 메시지</param>
        private void LogError(string message) => Console.WriteLine($"[오류] {message}");

        /// <summary>
        /// 경고 로그를 출력하고 null을 반환합니다.
        /// </summary>
        /// <param name="message">로그 메시지</param>
        /// <returns>항상 null</returns>
        private object LogAndReturnNull(string message)
        {
            LogWarning(message);
            return null;
        }
        #endregion
    }
}