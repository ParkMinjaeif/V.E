using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using NodeEditor;
using IFVisionEngine.Manager;
using IFVisionEngine.UIComponents.Data;
using IFVisionEngine.UIComponents.Dialogs;
using IFVisionEngine.UIComponents.Managers;
using static MyNodesContext;
using System.Dynamic;
using System.Drawing.Imaging;
using IFVisionEngine.Themes;

namespace IFVisionEngine.UIComponents.UserControls
{
    /// <summary>
    /// 노드 기반 비주얼 프로그래밍 에디터를 제공하는 사용자 컨트롤
    /// </summary>
    public partial class UcNodeEditor : UserControl
    {
        #region Fields and Properties
        public MyNodesContext _nodesContext;
        private Form1 _formMainInstance;
        // 이벤트
        public event Action<object> SelectedNodeContextChanged;

        // 클릭/드래그/더블클릭 상태 관리
        private bool _nodeWasJustSelected = false;
        private bool _isDragging = false;
        private bool _isDoubleClick = false;
        private DateTime _firstClickTime = DateTime.MinValue;
        private Timer _doubleClickTimer;

        // 더블클릭 관련 상수
        private const int DOUBLECLICK_TIMEOUT = 300;
        private const int DOUBLECLICK_RESET_DELAY = 100;

        // 중심좌표 저장
        private Dictionary<string, CentroidData> _storedCentroids = new Dictionary<string, CentroidData>();
        private CentroidData _lastCentroid = null;

        /// <summary>
        /// 중심좌표 데이터 클래스
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
        public UcNodeEditor(Form1 mainForm)
        {
            InitializeComponent();
            _formMainInstance = mainForm;
            //this.Dock = DockStyle.Fill;
            InitializeNodeContext();
            ThemeManager.ApplyThemeToControl(this);
            this.toolStripButton_Load.Image = Properties.Resources.Load;
            this.toolStripButton_Run.Image = Properties.Resources.Play;
            this.toolStripButton_Save.Image = Properties.Resources.Save;
        }

        private void InitializeNodeContext()
        {
            _nodesContext = new MyNodesContext();
            _nodesContext.Invoker = this;
            nodesControl1.Context = _nodesContext;
        }
        private void UcNodeEditor_Load(object sender, EventArgs e)
        {
            nodesControl1.Execute();
        }

        public MyNodesContext GetContext() => _nodesContext;
        #endregion

        #region Centroid Management
        private CentroidData ExtractCentroidFromNodeContext(NodeVisual node)
        {
            try
            {
                dynamic ctx = node.GetNodeContext();
                if (ctx is DynamicNodeContext dynCtx)
                {
                    foreach (var memberName in dynCtx.GetDynamicMemberNames())
                    {
                        try
                        {
                            var memberValue = dynCtx[memberName];
                            if (memberValue != null)
                            {
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

                    // 특별 키 확인
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

        private CentroidData ExtractCentroidFromData(object data)
        {
            try
            {
                // Dictionary 형태의 moments 데이터
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

                    // m10/m00, m01/m00 형태
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

                // Dynamic 객체
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

                // 문자열 형태 "x,y"
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

                // OpenCV Moments 객체 (리플렉션)
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

        private void StoreCentroidForNode(string nodeId, CentroidData centroid)
        {
            _storedCentroids[nodeId] = centroid;
            _lastCentroid = centroid;
            LogDebug($"노드 '{nodeId}'의 중심좌표 저장: ({centroid.X:F2}, {centroid.Y:F2})");
        }

        public CentroidData GetLastCentroid() => _lastCentroid;

        public CentroidData GetStoredCentroid(string nodeId)
        {
            return _storedCentroids.ContainsKey(nodeId) ? _storedCentroids[nodeId] : null;
        }
        #endregion

        #region Toolbar Events
        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            Console.WriteLine("=== PropertyGrid 진단 시작 ===");
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


        private void toolStripButton_Run_Click(object sender, EventArgs e)
        {
            IFVisionEngine.Manager.AppUIManager.ucNodeExecutionView.ClearData();
            nodesControl1.Execute();
        }
        #endregion

        #region Save/Load Methods
        public void SaveNodeLayoutToJson(NodesControl nodesCtrl, string filePath)
        {
            try
            {
                byte[] serializedData = nodesCtrl.Serialize();
                if (serializedData == null)
                {
                    Console.WriteLine("직렬화 실패");
                    return;
                }

                string base64Data = Convert.ToBase64String(serializedData);
                var jsonData = new { NodeGraphData = base64Data };
                string jsonString = System.Text.Json.JsonSerializer.Serialize(jsonData,
                    new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

                System.IO.File.WriteAllText(filePath, jsonString, Encoding.UTF8);
                MessageBox.Show("레이아웃이 저장되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"저장 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadNodeLayoutFromJson(NodesControl nodesCtrl, string filePath)
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    MessageBox.Show("파일을 찾을 수 없습니다.");
                    return;
                }

                string jsonString = System.IO.File.ReadAllText(filePath, Encoding.UTF8);
                var jsonData = System.Text.Json.JsonSerializer.Deserialize<JsonNodeGraphWrapper>(jsonString);

                if (jsonData == null || string.IsNullOrEmpty(jsonData.NodeGraphData))
                {
                    MessageBox.Show("데이터 읽기 실패");
                    return;
                }

                byte[] serializedData = Convert.FromBase64String(jsonData.NodeGraphData);
                nodesCtrl.Deserialize(serializedData);
                MessageBox.Show("레이아웃을 불러왔습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"로드 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class JsonNodeGraphWrapper
        {
            public string NodeGraphData { get; set; }
        }
        #endregion

        #region Mouse Events
        private void nodesControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_isDoubleClick) _isDragging = true;
        }

        private void nodesControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDoubleClick || !_isDragging) return;
        }

        private void nodesControl1_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            if (!_nodeWasJustSelected && !_isDoubleClick)
                SelectedNodeContextChanged?.Invoke(null);
            _nodeWasJustSelected = false;
        }
        #endregion

        #region Node Selection and Parameter Handling
        private void nodesControl1_OnNodeContextSelected(object obj)
        {
            DetectDoubleClick();

            if (_isDoubleClick)
            {
                _isDragging = false;
                _nodeWasJustSelected = false;
                ForceResetNodeEditorDragState();
                HandleDoubleClick(obj);
            }
            else
            {
                _nodeWasJustSelected = true;
                TryExtractAndStoreCentroid(obj);
            }
            SelectedNodeContextChanged?.Invoke(obj);
        }

        private void TryExtractAndStoreCentroid(object obj)
        {
            try
            {
                var nodeInfo = ExtractNodeInfo(obj);

                if (IsNodeTypeThatNeedsCentroid(nodeInfo.NodeName))
                {
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

        private bool IsNodeTypeThatNeedsCentroid(string nodeName)
        {
            if (string.IsNullOrEmpty(nodeName)) return false;
            string nodeType = ExtractNodeName(nodeName);
            return nodeType.Equals("RadialLines", StringComparison.OrdinalIgnoreCase);
        }

        private void DetectDoubleClick()
        {
            var now = DateTime.Now;
            if ((now - _firstClickTime).TotalMilliseconds > DOUBLECLICK_TIMEOUT)
            {
                _firstClickTime = now;
                _isDoubleClick = false;
            }
            else
            {
                _isDoubleClick = true;
                _firstClickTime = DateTime.MinValue;
                ResetDoubleClickFlag();
            }
        }

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

        private IDictionary<string, object> GetCurrentNodeParameters(string nodeName, object nodeContext)
        {
            var parameters = new Dictionary<string, object>();
            try
            {
                string simpleNodeName = ExtractNodeName(nodeName);
                var nodes = nodesControl1.GetNodes(simpleNodeName);
                if (nodes.Count > 0)
                {
                    var node = nodes[0];
                    dynamic ctx = node.GetNodeContext();

                    if (ctx is DynamicNodeContext dynCtx)
                    {
                        // DynamicNodeContext에서 해당 멤버가 존재하는지 확인 (HasMember 대신)
                        var memberNames = dynCtx.GetDynamicMemberNames();
                        if (memberNames.Contains(nodeName))
                        {
                            var currentParamObj = dynCtx[nodeName];
                            if (currentParamObj != null)
                            {
                                var paramType = currentParamObj.GetType();
                                var properties = paramType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                                foreach (var prop in properties)
                                {
                                    if (prop.CanRead)
                                        parameters[prop.Name] = prop.GetValue(currentParamObj);
                                }
                            }
                        }
                    }

                    // RadialLines 노드인 경우 중심좌표 정보 추가
                    if (IsNodeTypeThatNeedsCentroid(nodeName))
                    {
                        var centroid = GetLastCentroid();
                        if (centroid != null)
                        {
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

        private void HandleDoubleClick(object obj)
        {
            var nodeInfo = ExtractNodeInfo(obj);

            if (IsResultNode(nodeInfo.NodeName))
            {
                //ShowResultInExecutionView(nodeInfo);
            }
            else if (nodeInfo.HasParameters)
            {
                ForceResetAllStates();
                var currentParameters = GetCurrentNodeParameters(nodeInfo.NodeName, obj);
                ShowParameterDialog(nodeInfo, currentParameters);
                ForceResetAllStates();
            }
        }

        private bool IsResultNode(string nodeName)
        {
            if (string.IsNullOrEmpty(nodeName)) return false;

            try
            {
                var resultNodeTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "LineValidation",
                    "QualityCheck",
                    "MeasurementResult",
                    "ValidationResult",
                    "AnalysisResult",
                    "TestResult"
                };

                string nodeType = ExtractNodeName(nodeName);
                bool isResult = resultNodeTypes.Contains(nodeType);

                if (isResult)
                {
                    Console.WriteLine($"[NodeEditor] 결과값 노드 감지: {nodeType}");
                }

                return isResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NodeEditor] 결과값 노드 판별 오류: {ex.Message}");
                return false;
            }
        }

        private ResultData ExtractNodeResultData(string nodeName)
        {
            try
            {
                string simpleNodeName = ExtractNodeName(nodeName);
                var nodes = nodesControl1.GetNodes(simpleNodeName);

                if (nodes.Count > 0)
                {
                    var node = nodes[0];
                    dynamic ctx = node.GetNodeContext();

                    if (ctx is DynamicNodeContext dynCtx)
                    {
                        string resultContent = "";
                        bool isValid = false;
                        string nodeType = simpleNodeName;

                        var members = dynCtx.GetDynamicMemberNames();
                        foreach (var member in members)
                        {
                            try
                            {
                                var value = dynCtx[member];
                                if (value != null)
                                {
                                    if (member.ToLower().Contains("result") ||
                                        member.ToLower().Contains("validation"))
                                    {
                                        resultContent = value.ToString();

                                        string valueStr = value.ToString().ToLower();
                                        if (valueStr.Contains("pass") || valueStr.Contains("success"))
                                        {
                                            isValid = true;
                                        }
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[NodeEditor] 멤버 '{member}' 추출 오류: {ex.Message}");
                            }
                        }

                        if (string.IsNullOrEmpty(resultContent))
                        {
                            resultContent = "결과 데이터를 찾을 수 없습니다.";
                        }

                        return new ResultData(simpleNodeName, nodeType, resultContent, isValid);
                    }
                }

                return new ResultData(simpleNodeName, "Unknown", "노드 데이터에 접근할 수 없습니다.", false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NodeEditor] 결과 데이터 추출 오류: {ex.Message}");
                return new ResultData(nodeName, "Error", $"오류: {ex.Message}", false);
            }
        }

        private void HandleNodeExecutionComplete(NodeVisual node)
        {
            try
            {
                if (node == null) return;

                string nodeName = node.Name ?? "Unknown";

                if (IsResultNode(nodeName + "parameters"))
                {
                    Console.WriteLine($"[NodeEditor] 결과값 노드 실행 완료: {nodeName}");

                    var resultData = ExtractNodeResultData(nodeName + "parameters");
                    if (resultData != null)
                    {
                        ResultsManager.Instance.AddResult(resultData);
                        Console.WriteLine($"[NodeEditor] 결과 자동 등록: {resultData.GetSummary()}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NodeEditor] 노드 실행 완료 처리 오류: {ex.Message}");
            }
        }

        private (string InputKey, string NodeName, string OutputKey, bool HasParameters) ExtractNodeInfo(object obj)
        {
            string inputKey = null;
            string nodeName = null;
            string outputKey = null;
            bool hasParameters = false;

            if (obj is IDynamicMetaObjectProvider dynObj)
            {
                foreach (var memberName in dynObj.GetMetaObject(Expression.Constant(dynObj)).GetDynamicMemberNames())
                {
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

        private void ForceResetAllStates()
        {
            _isDragging = false;
            _nodeWasJustSelected = false;
            _isDoubleClick = false;
            ForceResetNodeEditorDragState();
        }

        private void ForceResetNodeEditorDragState()
        {
            try
            {
                var nodesControlType = nodesControl1.GetType();

                var mdownField = nodesControlType.GetField("mdown",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                mdownField?.SetValue(nodesControl1, false);

                var dragSocketField = nodesControlType.GetField("dragSocket",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                dragSocketField?.SetValue(nodesControl1, null);

                nodesControl1.Invalidate();
                nodesControl1.Update();
            }
            catch (Exception ex)
            {
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
        private void SetNodeParametersAndExecute(string nodeName, object param)
        {
            string simpleNodeName = ExtractNodeName(nodeName);
            var nodes = nodesControl1.GetNodes(simpleNodeName);
            if (nodes.Count == 0) return;
            var node = nodes[0];

            if (!SetNodeParameters(node, nodeName, param)) return;
            IFVisionEngine.Manager.AppUIManager.ucNodeExecutionView.ClearData();
            ExecuteNode(node);
        }

        private bool SetNodeParameters(NodeVisual node, string paramName, object param)
        {
            dynamic ctx = node.GetNodeContext();

            if (ctx is DynamicNodeContext dynCtx && param is IDictionary<string, object> dict)
            {
                var nodeType = ExtractNodeName(paramName);
                object paramObject = CreateParameterObject(nodeType, dict);
                if (paramObject != null)
                {
                    try
                    {
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

        private string ExtractNodeName(string nodeName)
        {
            const string suffix = "parameters";
            if (!string.IsNullOrEmpty(nodeName) && nodeName.EndsWith(suffix))
                return nodeName.Substring(0, nodeName.Length - suffix.Length);
            return nodeName;
        }

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

        private RadialLinesParameters CreateRadialLinesParameters(IDictionary<string, object> dict)
        {
            var param = new RadialLinesParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }

        private ContourParameters CreateContourParameters(IDictionary<string, object> dict)
        {
            var param = new ContourParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }

        private MomentsParameters CreateMomentsParameters(IDictionary<string, object> dict)
        {
            var param = new MomentsParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }

        private EdgeDetectionParameters CreateEdgeParameters(IDictionary<string, object> dict)
        {
            var param = new EdgeDetectionParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }

        private GaussianBlurParameters CreateGaussianBlurParameters(IDictionary<string, object> dict)
        {
            var param = new GaussianBlurParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }

        private ClaheParameters CreateClaheParameters(IDictionary<string, object> dict)
        {
            var param = new ClaheParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }

        private BinarizationParameters CreateBinarizationParameters(IDictionary<string, object> dict)
        {
            var param = new BinarizationParameters();
            SetPropertiesFromDictionary(param, dict);
            return param;
        }

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

        private void ExecuteNode(NodeVisual node)
        {
            try
            {
                nodesControl1.Execute(node);
                HandleNodeExecutionComplete(node);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[노드 실행 실패] {ex.Message}\n{ex.StackTrace}");
            }
        }
        #endregion

        #region Logging
        private void LogDebug(string message) => Console.WriteLine($"[디버그] {message}");
        private void LogWarning(string message) => Console.WriteLine($"[경고] {message}");
        private void LogError(string message) => Console.WriteLine($"[오류] {message}");

        private object LogAndReturnNull(string message)
        {
            LogWarning(message);
            return null;
        }
        #endregion
    }
}