using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace IFVisionEngine.UIComponents.Dialogs
{
    /// <summary>
    /// Contour Detection 파라미터를 설정하는 사용자 컨트롤
    /// </summary>
    public partial class ContourParameterControl : UserControl, IPreprocessParameterControl, IParameterLoadable
    {
        #region Events
        public event Action<string, string, double, double, bool, int, string, Color, bool,
                       bool, bool, bool> OnParametersChanged;
        public event Action OnParametersChangedBase;
        #endregion

        #region Private Fields
        private bool _suppressEvents = false;
        #endregion

        #region Constructor
        public ContourParameterControl()
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
                if (parameters.ContainsKey("RetrievalMode"))
                {
                    string mode = parameters["RetrievalMode"].ToString();
                    int index = comboBox_RetrievalMode.Items.IndexOf(mode);
                    if (index >= 0) comboBox_RetrievalMode.SelectedIndex = index;
                }

                if (parameters.ContainsKey("ApproximationMethod"))
                {
                    string method = parameters["ApproximationMethod"].ToString();
                    int index = comboBox_ApproximationMethod.Items.IndexOf(method);
                    if (index >= 0) comboBox_ApproximationMethod.SelectedIndex = index;
                }

                if (parameters.ContainsKey("MinContourArea"))
                {
                    if (double.TryParse(parameters["MinContourArea"].ToString(), out double minArea))
                    {
                        minArea = Math.Max(0, Math.Min(10000, minArea));
                        trackBar_MinArea.Value = (int)minArea;
                        numericUpDown_MinArea.Value = (decimal)minArea;
                    }
                }

                if (parameters.ContainsKey("MaxContourArea"))
                {
                    if (double.TryParse(parameters["MaxContourArea"].ToString(), out double maxArea))
                    {
                        maxArea = Math.Max(1000, Math.Min(10000000, maxArea));
                        int trackBarValue = (int)(maxArea / 1000);
                        trackBar_MaxArea.Value = Math.Min(trackBar_MaxArea.Maximum, trackBarValue);
                        numericUpDown_MaxArea.Value = (decimal)maxArea;
                    }
                }

                if (parameters.ContainsKey("DrawOnOriginal"))
                {
                    if (bool.TryParse(parameters["DrawOnOriginal"].ToString(), out bool drawOnOriginal))
                        checkBox_DrawOnOriginal.Checked = drawOnOriginal;
                }

                if (parameters.ContainsKey("Thickness"))
                {
                    if (int.TryParse(parameters["Thickness"].ToString(), out int thickness))
                    {
                        thickness = Math.Max(1, Math.Min(10, thickness));
                        trackBar_Thickness.Value = thickness;
                        numericUpDown_Thickness.Value = thickness;
                    }
                }

                if (parameters.ContainsKey("ColorMode"))
                {
                    string colorMode = parameters["ColorMode"].ToString();
                    int index = comboBox_ColorMode.Items.IndexOf(colorMode);
                    if (index >= 0) comboBox_ColorMode.SelectedIndex = index;
                }

                if (parameters.ContainsKey("FixedColor"))
                {
                    if (parameters["FixedColor"] is Color color)
                        button_FixedColor.BackColor = color;
                }

                if (parameters.ContainsKey("ShowContourNumbers"))
                {
                    if (bool.TryParse(parameters["ShowContourNumbers"].ToString(), out bool showNumbers))
                        checkBox_ShowNumbers.Checked = showNumbers;
                }
                if (parameters.ContainsKey("ShowVisualization"))
                {
                    if (bool.TryParse(parameters["ShowVisualization"].ToString(), out bool showVisualization))
                        checkBox_ShowVisualization.Checked = showVisualization;
                }

                if (parameters.ContainsKey("OutputContourData"))
                {
                    if (bool.TryParse(parameters["OutputContourData"].ToString(), out bool outputData))
                        checkBox_OutputData.Checked = outputData;
                }

                if (parameters.ContainsKey("OutputAsJson"))
                {
                    if (bool.TryParse(parameters["OutputAsJson"].ToString(), out bool outputAsJson))
                        checkBox_OutputAsJson.Checked = outputAsJson;
                }
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
                { "RetrievalMode", comboBox_RetrievalMode.SelectedItem?.ToString() ?? "External" },
                { "ApproximationMethod", comboBox_ApproximationMethod.SelectedItem?.ToString() ?? "Simple" },
                { "MinContourArea", (double)numericUpDown_MinArea.Value },
                { "MaxContourArea", (double)numericUpDown_MaxArea.Value },
                { "DrawOnOriginal", checkBox_DrawOnOriginal.Checked },
                { "Thickness", (int)numericUpDown_Thickness.Value },
                { "ColorMode", comboBox_ColorMode.SelectedItem?.ToString() ?? "Fixed" },
                { "FixedColor", button_FixedColor.BackColor },
                { "ShowContourNumbers", checkBox_ShowNumbers.Checked },
                           { "ShowVisualization", checkBox_ShowVisualization.Checked },
            { "OutputContourData", checkBox_OutputData.Checked },
            { "OutputAsJson", checkBox_OutputAsJson.Checked }
            };
        }

        public void ResetParametersToDefault()
        {
            _suppressEvents = true;
            try
            {
                comboBox_RetrievalMode.SelectedIndex = 0;
                comboBox_ApproximationMethod.SelectedIndex = 1;
                trackBar_MinArea.Value = 100;
                numericUpDown_MinArea.Value = 100;
                trackBar_MaxArea.Value = 100;
                numericUpDown_MaxArea.Value = 100000;
                checkBox_DrawOnOriginal.Checked = true;
                trackBar_Thickness.Value = 2;
                numericUpDown_Thickness.Value = 2;
                comboBox_ColorMode.SelectedIndex = 0;
                button_FixedColor.BackColor = Color.Green;
                checkBox_ShowNumbers.Checked = false;
                checkBox_ShowVisualization.Checked = true;
                checkBox_OutputData.Checked = false;
                checkBox_OutputAsJson.Checked = true;
            }
            finally
            {
                _suppressEvents = false;
                RaiseParameterChanged();
            }
        }
        #endregion

        #region Event Handlers
        private void ContourParameterControl_Load(object sender, EventArgs e)
        {
            _suppressEvents = true;
            try
            {
                InitializeControls();
                SetupEventHandlers();
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
            comboBox_RetrievalMode.Items.AddRange(new string[] { "External", "List", "CComp", "Tree" });
            comboBox_RetrievalMode.SelectedIndex = 0;

            comboBox_ApproximationMethod.Items.AddRange(new string[] { "None", "Simple", "TC89_L1" });
            comboBox_ApproximationMethod.SelectedIndex = 1;

            comboBox_ColorMode.Items.AddRange(new string[] { "Fixed", "Random", "SizeBased" });
            comboBox_ColorMode.SelectedIndex = 0;

            // TrackBar + NumericUpDown 초기화
            trackBar_MinArea.Minimum = 0;
            trackBar_MinArea.Maximum = 10000;
            trackBar_MinArea.Value = 100;
            numericUpDown_MinArea.Minimum = 0;
            numericUpDown_MinArea.Maximum = 10000;
            numericUpDown_MinArea.Value = 100;

            trackBar_MaxArea.Minimum = 1;
            trackBar_MaxArea.Maximum = 10000; // 실제값은 1000배
            trackBar_MaxArea.Value = 100;
            numericUpDown_MaxArea.Minimum = 1000;
            numericUpDown_MaxArea.Maximum = 10000000;
            numericUpDown_MaxArea.Value = 100000;

            trackBar_Thickness.Minimum = 1;
            trackBar_Thickness.Maximum = 10;
            trackBar_Thickness.Value = 2;
            numericUpDown_Thickness.Minimum = 1;
            numericUpDown_Thickness.Maximum = 10;
            numericUpDown_Thickness.Value = 2;

            // 기타
            checkBox_DrawOnOriginal.Checked = true;
            button_FixedColor.BackColor = Color.Green;
            checkBox_ShowNumbers.Checked = false;
            checkBox_ShowVisualization.Checked = true;
            checkBox_OutputData.Checked = false;
            checkBox_OutputAsJson.Checked = true;
            checkBox_OutputAsJson.Enabled = false;
        }

        private void SetupEventHandlers()
        {
            // ComboBox 이벤트
            comboBox_RetrievalMode.SelectedIndexChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };
            comboBox_ApproximationMethod.SelectedIndexChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };
            comboBox_ColorMode.SelectedIndexChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };

            // TrackBar - NumericUpDown 동기화
            trackBar_MinArea.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    numericUpDown_MinArea.Value = trackBar_MinArea.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };
            numericUpDown_MinArea.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    trackBar_MinArea.Value = (int)numericUpDown_MinArea.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            trackBar_MaxArea.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    numericUpDown_MaxArea.Value = trackBar_MaxArea.Value * 1000;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };
            numericUpDown_MaxArea.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    trackBar_MaxArea.Value = (int)(numericUpDown_MaxArea.Value / 1000);
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            trackBar_Thickness.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    numericUpDown_Thickness.Value = trackBar_Thickness.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };
            numericUpDown_Thickness.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    trackBar_Thickness.Value = (int)numericUpDown_Thickness.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            // CheckBox, Button 이벤트
            checkBox_DrawOnOriginal.CheckedChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };
            checkBox_ShowNumbers.CheckedChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };

            button_FixedColor.Click += (s, e) => {
                using (var colorDialog = new ColorDialog())
                {
                    colorDialog.Color = button_FixedColor.BackColor;
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        button_FixedColor.BackColor = colorDialog.Color;
                        if (!_suppressEvents) RaiseParameterChanged();
                    }
                }
            };
            checkBox_ShowVisualization.CheckedChanged += (s, e) => {
                if (!_suppressEvents) RaiseParameterChanged();
            };

            checkBox_OutputData.CheckedChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    // OutputData가 체크되었을 때만 OutputAsJson 활성화
                    checkBox_OutputAsJson.Enabled = checkBox_OutputData.Checked;
                    RaiseParameterChanged();
                }
            };

            checkBox_OutputAsJson.CheckedChanged += (s, e) => {
                if (!_suppressEvents) RaiseParameterChanged();
            };
        }

        private void RaiseParameterChanged()
        {
            if (_suppressEvents) return;

            string retrievalMode = comboBox_RetrievalMode.SelectedItem?.ToString() ?? "External";
            string approximationMethod = comboBox_ApproximationMethod.SelectedItem?.ToString() ?? "Simple";
            double minArea = (double)numericUpDown_MinArea.Value;
            double maxArea = (double)numericUpDown_MaxArea.Value;
            bool drawOnOriginal = checkBox_DrawOnOriginal.Checked;
            int thickness = (int)numericUpDown_Thickness.Value;
            string colorMode = comboBox_ColorMode.SelectedItem?.ToString() ?? "Fixed";
            Color fixedColor = button_FixedColor.BackColor;
            bool showNumbers = checkBox_ShowNumbers.Checked;

            bool showVisualization = checkBox_ShowVisualization.Checked;
            bool outputData = checkBox_OutputData.Checked;
            bool outputAsJson = checkBox_OutputAsJson.Checked;

            OnParametersChanged?.Invoke(retrievalMode, approximationMethod, minArea, maxArea,
                                      drawOnOriginal, thickness, colorMode, fixedColor, showNumbers,
                                      showVisualization, outputData, outputAsJson);
            OnParametersChangedBase?.Invoke();
        }
        #endregion
    }
}