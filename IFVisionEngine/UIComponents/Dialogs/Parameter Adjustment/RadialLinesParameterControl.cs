using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IFVisionEngine.UIComponents.Data;
namespace IFVisionEngine.UIComponents.Dialogs
{

    /// <summary>
    /// RadialLines 파라미터를 설정하는 사용자 컨트롤
    /// </summary>
    public partial class RadialLinesParameterControl : UserControl, IPreprocessParameterControl, IParameterLoadable
    {
        #region Events
        public event Action<RadialLinesParameter> OnParametersChanged;
        public event Action OnParametersChangedBase;
        #endregion

        #region Private Fields
        private bool _suppressEvents = false;
        private double _centroidX = 0;
        private double _centroidY = 0;
        private bool _hasCentroidData = false;
        #endregion

        #region Constructor
        public RadialLinesParameterControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Methods
        public void SetCurrentParameters(Dictionary<string, object> parameters)
        {
            if (parameters == null) return;

            _suppressEvents = true;
            try
            {
                // 무게중심 데이터 처리
                if (parameters.ContainsKey("moments"))
                {
                    var moments = parameters["moments"];
                    if (moments != null)
                    {
                        // moments 객체에서 무게중심 좌표 추출
                        if (moments is Dictionary<string, object> momentsDict)
                        {
                            if (momentsDict.ContainsKey("CentroidX") && momentsDict.ContainsKey("CentroidY"))
                            {
                                if (double.TryParse(momentsDict["CentroidX"].ToString(), out double centroidX) &&
                                    double.TryParse(momentsDict["CentroidY"].ToString(), out double centroidY))
                                {
                                    _centroidX = centroidX;
                                    _centroidY = centroidY;
                                    _hasCentroidData = true;
                                }
                            }
                        }
                        // 또는 직접 좌표가 전달되는 경우
                        else if (moments.ToString().Contains(","))
                        {
                            string[] coords = moments.ToString().Split(',');
                            if (coords.Length >= 2)
                            {
                                if (double.TryParse(coords[0].Trim(), out double centroidX) &&
                                    double.TryParse(coords[1].Trim(), out double centroidY))
                                {
                                    _centroidX = centroidX;
                                    _centroidY = centroidY;
                                    _hasCentroidData = true;
                                }
                            }
                        }
                    }
                }

                // 기존 파라미터들 처리
                if (parameters.ContainsKey("CentroidX") && parameters.ContainsKey("CentroidY"))
                {
                    if (double.TryParse(parameters["CentroidX"].ToString(), out double centroidX) &&
                        double.TryParse(parameters["CentroidY"].ToString(), out double centroidY))
                    {
                        _centroidX = centroidX;
                        _centroidY = centroidY;
                        _hasCentroidData = true;
                    }
                }

                if (parameters.ContainsKey("HasCentroidData"))
                {
                    if (bool.TryParse(parameters["HasCentroidData"].ToString(), out bool hasCentroidData))
                        _hasCentroidData = hasCentroidData;
                }

                if (parameters.ContainsKey("ShowVisualization"))
                {
                    if (bool.TryParse(parameters["ShowVisualization"].ToString(), out bool showVisualization))
                        checkBox_ShowVisualization.Checked = showVisualization;
                }

                if (parameters.ContainsKey("CenterMethod"))
                {
                    string centerMethod = parameters["CenterMethod"].ToString();
                    int index = comboBox_CenterMethod.Items.IndexOf(centerMethod);
                    if (index >= 0) comboBox_CenterMethod.SelectedIndex = index;
                }

                if (parameters.ContainsKey("ManualX"))
                {
                    if (int.TryParse(parameters["ManualX"].ToString(), out int manualX))
                    {
                        manualX = Math.Max(0, Math.Min(2000, manualX));
                        numericUpDown_ManualX.Value = manualX;
                    }
                }

                if (parameters.ContainsKey("ManualY"))
                {
                    if (int.TryParse(parameters["ManualY"].ToString(), out int manualY))
                    {
                        manualY = Math.Max(0, Math.Min(2000, manualY));
                        numericUpDown_ManualY.Value = manualY;
                    }
                }

                if (parameters.ContainsKey("RangeMethod"))
                {
                    string rangeMethod = parameters["RangeMethod"].ToString();
                    int index = comboBox_RangeMethod.Items.IndexOf(rangeMethod);
                    if (index >= 0) comboBox_RangeMethod.SelectedIndex = index;
                }

                if (parameters.ContainsKey("FixedLength"))
                {
                    if (int.TryParse(parameters["FixedLength"].ToString(), out int fixedLength))
                    {
                        fixedLength = Math.Max(10, Math.Min(1000, fixedLength));
                        trackBar_FixedLength.Value = fixedLength;
                        numericUpDown_FixedLength.Value = fixedLength;
                    }
                }

                if (parameters.ContainsKey("LineCount"))
                {
                    if (int.TryParse(parameters["LineCount"].ToString(), out int lineCount))
                    {
                        lineCount = Math.Max(4, Math.Min(360, lineCount));
                        trackBar_LineCount.Value = lineCount;
                        numericUpDown_LineCount.Value = lineCount;
                    }
                }

                if (parameters.ContainsKey("StartAngle"))
                {
                    if (int.TryParse(parameters["StartAngle"].ToString(), out int startAngle))
                    {
                        startAngle = Math.Max(0, Math.Min(359, startAngle));
                        trackBar_StartAngle.Value = startAngle;
                        numericUpDown_StartAngle.Value = startAngle;
                    }
                }

                if (parameters.ContainsKey("LineColor"))
                {
                    if (parameters["LineColor"] is Color lineColor)
                        button_LineColor.BackColor = lineColor;
                }

                if (parameters.ContainsKey("LineThickness"))
                {
                    if (int.TryParse(parameters["LineThickness"].ToString(), out int lineThickness))
                    {
                        lineThickness = Math.Max(1, Math.Min(10, lineThickness));
                        trackBar_LineThickness.Value = lineThickness;
                        numericUpDown_LineThickness.Value = lineThickness;
                    }
                }

                if (parameters.ContainsKey("Style"))
                {
                    string style = parameters["Style"].ToString();
                    int index = comboBox_Style.Items.IndexOf(style);
                    if (index >= 0) comboBox_Style.SelectedIndex = index;
                }

                if (parameters.ContainsKey("ShowCenter"))
                {
                    if (bool.TryParse(parameters["ShowCenter"].ToString(), out bool showCenter))
                        checkBox_ShowCenter.Checked = showCenter;
                }

                if (parameters.ContainsKey("ShowAngles"))
                {
                    if (bool.TryParse(parameters["ShowAngles"].ToString(), out bool showAngles))
                        checkBox_ShowAngles.Checked = showAngles;
                }

                if (parameters.ContainsKey("ShowDistances"))
                {
                    if (bool.TryParse(parameters["ShowDistances"].ToString(), out bool showDistances))
                        checkBox_ShowDistances.Checked = showDistances;
                }

                if (parameters.ContainsKey("BinaryThreshold"))
                {
                    if (int.TryParse(parameters["BinaryThreshold"].ToString(), out int binaryThreshold))
                    {
                        binaryThreshold = Math.Max(0, Math.Min(255, binaryThreshold));
                        trackBar_BinaryThreshold.Value = binaryThreshold;
                        numericUpDown_BinaryThreshold.Value = binaryThreshold;
                    }
                }

                if (parameters.ContainsKey("BrightnessThreshold"))
                {
                    if (int.TryParse(parameters["BrightnessThreshold"].ToString(), out int brightnessThreshold))
                    {
                        brightnessThreshold = Math.Max(0, Math.Min(255, brightnessThreshold));
                        trackBar_BrightnessThreshold.Value = brightnessThreshold;
                        numericUpDown_BrightnessThreshold.Value = brightnessThreshold;
                    }
                }

                if (parameters.ContainsKey("OutputLengthData"))
                {
                    if (bool.TryParse(parameters["OutputLengthData"].ToString(), out bool outputLengthData))
                        checkBox_OutputLengthData.Checked = outputLengthData;
                }

                // 무게중심 정보 표시 업데이트
                UpdateCentroidDisplay();
            }
            finally
            {
                _suppressEvents = false;
                RaiseParameterChanged();
            }
        }

        public Dictionary<string, object> GetParameters()
        {
            return new Dictionary<string, object>
            {
                { "ShowVisualization", checkBox_ShowVisualization.Checked },
                { "CenterMethod", comboBox_CenterMethod.SelectedItem?.ToString() ?? "ImageCenter" },
                { "ManualX", (int)numericUpDown_ManualX.Value },
                { "ManualY", (int)numericUpDown_ManualY.Value },
                { "CentroidX", _centroidX },
                { "CentroidY", _centroidY },
                { "HasCentroidData", _hasCentroidData },
                { "RangeMethod", comboBox_RangeMethod.SelectedItem?.ToString() ?? "EdgeDetection" },
                { "FixedLength", (int)numericUpDown_FixedLength.Value },
                { "LineCount", (int)numericUpDown_LineCount.Value },
                { "StartAngle", (int)numericUpDown_StartAngle.Value },
                { "LineColor", button_LineColor.BackColor },
                { "LineThickness", (int)numericUpDown_LineThickness.Value },
                { "Style", comboBox_Style.SelectedItem?.ToString() ?? "Solid" },
                { "ShowCenter", checkBox_ShowCenter.Checked },
                { "ShowAngles", checkBox_ShowAngles.Checked },
                { "ShowDistances", checkBox_ShowDistances.Checked },
                { "BinaryThreshold", (int)numericUpDown_BinaryThreshold.Value },
                { "BrightnessThreshold", (int)numericUpDown_BrightnessThreshold.Value },
                { "OutputLengthData", checkBox_OutputLengthData.Checked }
            };
        }

        public void ResetParametersToDefault()
        {
            _suppressEvents = true;
            try
            {
                checkBox_ShowVisualization.Checked = true;
                comboBox_CenterMethod.SelectedIndex = 0; // ImageCenter
                numericUpDown_ManualX.Value = 0;
                numericUpDown_ManualY.Value = 0;
                comboBox_RangeMethod.SelectedIndex = 2; // EdgeDetection
                trackBar_FixedLength.Value = 100;
                numericUpDown_FixedLength.Value = 100;
                trackBar_LineCount.Value = 8;
                numericUpDown_LineCount.Value = 8;
                trackBar_StartAngle.Value = 0;
                numericUpDown_StartAngle.Value = 0;
                button_LineColor.BackColor = Color.Red;
                trackBar_LineThickness.Value = 2;
                numericUpDown_LineThickness.Value = 2;
                comboBox_Style.SelectedIndex = 0; // Solid
                checkBox_ShowCenter.Checked = true;
                checkBox_ShowAngles.Checked = false;
                checkBox_ShowDistances.Checked = false;
                trackBar_BinaryThreshold.Value = 127;
                numericUpDown_BinaryThreshold.Value = 127;
                trackBar_BrightnessThreshold.Value = 50;
                numericUpDown_BrightnessThreshold.Value = 50;
                checkBox_OutputLengthData.Checked = true;

                // 무게중심 데이터는 리셋하지 않음 (유지)
                UpdateCentroidDisplay();
            }
            finally
            {
                _suppressEvents = false;
                RaiseParameterChanged();
            }
        }
        #endregion

        #region Event Handlers
        private void RadialLinesParameterControl_Load(object sender, EventArgs e)
        {
            _suppressEvents = true;
            try
            {
                InitializeControls();
                SetupEventHandlers();
                UpdateControlVisibility();
                UpdateCentroidDisplay();
            }
            finally
            {
                _suppressEvents = false;
                RaiseParameterChanged();
            }
        }

        private void InitializeControls()
        {
            // ComboBox 초기화
            comboBox_CenterMethod.Items.AddRange(new string[] {
                "ImageCenter", "AutoCentroid", "Manual", "ExternalCoordinates", "MaxBrightness"
            });
            comboBox_CenterMethod.SelectedIndex = 0;

            comboBox_RangeMethod.Items.AddRange(new string[] {
                "FixedLength", "ImageBoundary", "EdgeDetection", "BrightnessChange"
            });
            comboBox_RangeMethod.SelectedIndex = 2;

            comboBox_Style.Items.AddRange(new string[] { "Solid", "Dotted", "Dashed" });
            comboBox_Style.SelectedIndex = 0;

            // TrackBar + NumericUpDown 초기화
            SetupTrackBarNumericPair(trackBar_FixedLength, numericUpDown_FixedLength, 10, 1000, 100);
            SetupTrackBarNumericPair(trackBar_LineCount, numericUpDown_LineCount, 4, 360, 8);
            SetupTrackBarNumericPair(trackBar_StartAngle, numericUpDown_StartAngle, 0, 359, 0);
            SetupTrackBarNumericPair(trackBar_LineThickness, numericUpDown_LineThickness, 1, 10, 2);
            SetupTrackBarNumericPair(trackBar_BinaryThreshold, numericUpDown_BinaryThreshold, 0, 255, 127);
            SetupTrackBarNumericPair(trackBar_BrightnessThreshold, numericUpDown_BrightnessThreshold, 0, 255, 50);

            // Manual X, Y 좌표
            numericUpDown_ManualX.Minimum = 0;
            numericUpDown_ManualX.Maximum = 2000;
            numericUpDown_ManualX.Value = 0;

            numericUpDown_ManualY.Minimum = 0;
            numericUpDown_ManualY.Maximum = 2000;
            numericUpDown_ManualY.Value = 0;

            // 기타
            checkBox_ShowVisualization.Checked = true;
            checkBox_ShowCenter.Checked = true;
            checkBox_ShowAngles.Checked = false;
            checkBox_ShowDistances.Checked = false;
            checkBox_OutputLengthData.Checked = true;
            button_LineColor.BackColor = Color.Red;
        }

        private void SetupTrackBarNumericPair(TrackBar trackBar, NumericUpDown numericUpDown, int min, int max, int defaultValue)
        {
            trackBar.Minimum = min;
            trackBar.Maximum = max;
            trackBar.Value = defaultValue;

            numericUpDown.Minimum = min;
            numericUpDown.Maximum = max;
            numericUpDown.Value = defaultValue;
        }

        private void SetupEventHandlers()
        {
            // CheckBox 이벤트
            checkBox_ShowVisualization.CheckedChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };
            checkBox_ShowCenter.CheckedChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };
            checkBox_ShowAngles.CheckedChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };
            checkBox_ShowDistances.CheckedChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };
            checkBox_OutputLengthData.CheckedChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };

            // ComboBox 이벤트
            comboBox_CenterMethod.SelectedIndexChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    UpdateControlVisibility();
                    UpdateCentroidDisplay();
                    RaiseParameterChanged();
                }
            };
            comboBox_RangeMethod.SelectedIndexChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    UpdateControlVisibility();
                    RaiseParameterChanged();
                }
            };
            comboBox_Style.SelectedIndexChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };

            // TrackBar - NumericUpDown 동기화
            SetupTrackBarNumericEvents(trackBar_FixedLength, numericUpDown_FixedLength);
            SetupTrackBarNumericEvents(trackBar_LineCount, numericUpDown_LineCount);
            SetupTrackBarNumericEvents(trackBar_StartAngle, numericUpDown_StartAngle);
            SetupTrackBarNumericEvents(trackBar_LineThickness, numericUpDown_LineThickness);
            SetupTrackBarNumericEvents(trackBar_BinaryThreshold, numericUpDown_BinaryThreshold);
            SetupTrackBarNumericEvents(trackBar_BrightnessThreshold, numericUpDown_BrightnessThreshold);

            // Manual 좌표 이벤트
            numericUpDown_ManualX.ValueChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };
            numericUpDown_ManualY.ValueChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };

            // 색상 버튼 이벤트
            button_LineColor.Click += (s, e) => {
                using (var colorDialog = new ColorDialog())
                {
                    colorDialog.Color = button_LineColor.BackColor;
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        button_LineColor.BackColor = colorDialog.Color;
                        if (!_suppressEvents) RaiseParameterChanged();
                    }
                }
            };
        }

        private void SetupTrackBarNumericEvents(TrackBar trackBar, NumericUpDown numericUpDown)
        {
            trackBar.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    numericUpDown.Value = trackBar.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            numericUpDown.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    trackBar.Value = (int)numericUpDown.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };
        }

        private void UpdateControlVisibility()
        {
            // CenterMethod에 따른 Manual 좌표 컨트롤 활성화/비활성화
            bool isManualMode = comboBox_CenterMethod.SelectedItem?.ToString() == "Manual";
            label_ManualX.Enabled = isManualMode;
            numericUpDown_ManualX.Enabled = isManualMode;
            label_ManualY.Enabled = isManualMode;
            numericUpDown_ManualY.Enabled = isManualMode;

            // RangeMethod에 따른 컨트롤 활성화/비활성화
            string rangeMethod = comboBox_RangeMethod.SelectedItem?.ToString();

            bool isFixedLength = rangeMethod == "FixedLength";
            label_FixedLength.Enabled = isFixedLength;
            trackBar_FixedLength.Enabled = isFixedLength;
            numericUpDown_FixedLength.Enabled = isFixedLength;

            bool isEdgeDetection = rangeMethod == "EdgeDetection";
            label_BinaryThreshold.Enabled = isEdgeDetection;
            trackBar_BinaryThreshold.Enabled = isEdgeDetection;
            numericUpDown_BinaryThreshold.Enabled = isEdgeDetection;

            bool isBrightnessChange = rangeMethod == "BrightnessChange";
            label_BrightnessThreshold.Enabled = isBrightnessChange;
            trackBar_BrightnessThreshold.Enabled = isBrightnessChange;
            numericUpDown_BrightnessThreshold.Enabled = isBrightnessChange;
        }

        private void UpdateCentroidDisplay()
        {
            // 무게중심 데이터가 있는지만 확인 (UI 표시 없음)
            // 실제 처리는 다른 클래스에서 GetParameters()를 통해 가져가서 사용
        }

        private void RaiseParameterChanged()
        {
            if (_suppressEvents) return;

            var parameters = new RadialLinesParameter
            {
                ShowVisualization = checkBox_ShowVisualization.Checked,
                CenterMethod = comboBox_CenterMethod.SelectedItem?.ToString() ?? "ImageCenter",
                ManualX = (int)numericUpDown_ManualX.Value,
                ManualY = (int)numericUpDown_ManualY.Value,
                CentroidX = _centroidX,
                CentroidY = _centroidY,
                HasCentroidData = _hasCentroidData,
                RangeMethod = comboBox_RangeMethod.SelectedItem?.ToString() ?? "EdgeDetection",
                FixedLength = (int)numericUpDown_FixedLength.Value,
                LineCount = (int)numericUpDown_LineCount.Value,
                StartAngle = (int)numericUpDown_StartAngle.Value,
                LineColor = button_LineColor.BackColor,
                LineThickness = (int)numericUpDown_LineThickness.Value,
                Style = comboBox_Style.SelectedItem?.ToString() ?? "Solid",
                ShowCenter = checkBox_ShowCenter.Checked,
                ShowAngles = checkBox_ShowAngles.Checked,
                ShowDistances = checkBox_ShowDistances.Checked,
                BinaryThreshold = (int)numericUpDown_BinaryThreshold.Value,
                BrightnessThreshold = (int)numericUpDown_BrightnessThreshold.Value,
                OutputLengthData = checkBox_OutputLengthData.Checked
            };

            OnParametersChanged?.Invoke(parameters);
            OnParametersChangedBase?.Invoke();
        }
        #endregion
    }
}