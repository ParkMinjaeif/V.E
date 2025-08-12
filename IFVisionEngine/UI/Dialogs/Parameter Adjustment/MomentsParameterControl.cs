using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace IFVisionEngine.UIComponents.Dialogs
{
    /// <summary>
    /// Moments 분석 파라미터를 설정하는 사용자 컨트롤
    /// </summary>
    public partial class MomentsParameterControl : UserControl, IPreprocessParameterControl, IParameterLoadable
    {
        #region Events
        public event Action<int, bool, bool, bool, bool, bool, Color, int> OnParametersChanged;
        public event Action OnParametersChangedBase;
        #endregion

        #region Private Fields
        private bool _suppressEvents = false;
        #endregion

        #region Constructor
        public MomentsParameterControl()
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
                if (parameters.ContainsKey("BinaryThreshold"))
                {
                    if (int.TryParse(parameters["BinaryThreshold"].ToString(), out int threshold))
                    {
                        threshold = Math.Max(0, Math.Min(255, threshold));
                        trackBar_Threshold.Value = threshold;
                        numericUpDown_Threshold.Value = threshold;
                    }
                }

                if (parameters.ContainsKey("ShowCentroid"))
                {
                    if (bool.TryParse(parameters["ShowCentroid"].ToString(), out bool showCentroid))
                        checkBox_ShowCentroid.Checked = showCentroid;
                }

                if (parameters.ContainsKey("ShowArea"))
                {
                    if (bool.TryParse(parameters["ShowArea"].ToString(), out bool showArea))
                        checkBox_ShowArea.Checked = showArea;
                }

                if (parameters.ContainsKey("ShowOrientation"))
                {
                    if (bool.TryParse(parameters["ShowOrientation"].ToString(), out bool showOrientation))
                        checkBox_ShowOrientation.Checked = showOrientation;
                }

                if (parameters.ContainsKey("ShowBoundingBox"))
                {
                    if (bool.TryParse(parameters["ShowBoundingBox"].ToString(), out bool showBoundingBox))
                        checkBox_ShowBoundingBox.Checked = showBoundingBox;
                }

                if (parameters.ContainsKey("ShowEccentricity"))
                {
                    if (bool.TryParse(parameters["ShowEccentricity"].ToString(), out bool showEccentricity))
                        checkBox_ShowEccentricity.Checked = showEccentricity;
                }

                if (parameters.ContainsKey("DrawColor"))
                {
                    if (parameters["DrawColor"] is Color color)
                        button_DrawColor.FillColor = color;
                }

                if (parameters.ContainsKey("LineThickness"))
                {
                    if (int.TryParse(parameters["LineThickness"].ToString(), out int thickness))
                    {
                        thickness = Math.Max(1, Math.Min(10, thickness));
                        trackBar_LineThickness.Value = thickness;
                        numericUpDown_LineThickness.Value = thickness;
                    }
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
                { "BinaryThreshold", (int)numericUpDown_Threshold.Value },
                { "ShowCentroid", checkBox_ShowCentroid.Checked },
                { "ShowArea", checkBox_ShowArea.Checked },
                { "ShowOrientation", checkBox_ShowOrientation.Checked },
                { "ShowBoundingBox", checkBox_ShowBoundingBox.Checked },
                { "ShowEccentricity", checkBox_ShowEccentricity.Checked },
                { "DrawColor", button_DrawColor.FillColor },
                { "LineThickness", (int)numericUpDown_LineThickness.Value }
            };
        }

        public void ResetParametersToDefault()
        {
            _suppressEvents = true;
            try
            {
                trackBar_Threshold.Value = 127;
                numericUpDown_Threshold.Value = 127;
                checkBox_ShowCentroid.Checked = true;
                checkBox_ShowArea.Checked = true;
                checkBox_ShowOrientation.Checked = false;
                checkBox_ShowBoundingBox.Checked = true;
                checkBox_ShowEccentricity.Checked = false;
                button_DrawColor.FillColor = Color.Red;
                trackBar_LineThickness.Value = 2;
                numericUpDown_LineThickness.Value = 2;
            }
            finally
            {
                _suppressEvents = false;
                RaiseParameterChanged();
            }
        }
        #endregion

        #region Event Handlers
        private void MomentsParameterControl_Load(object sender, EventArgs e)
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
            // TrackBar + NumericUpDown 초기화 - 이진화 임계값
            trackBar_Threshold.Minimum = 0;
            trackBar_Threshold.Maximum = 255;
            trackBar_Threshold.Value = 127;
            numericUpDown_Threshold.Minimum = 0;
            numericUpDown_Threshold.Maximum = 255;
            numericUpDown_Threshold.Value = 127;

            // TrackBar + NumericUpDown 초기화 - 선 두께
            trackBar_LineThickness.Minimum = 1;
            trackBar_LineThickness.Maximum = 10;
            trackBar_LineThickness.Value = 2;
            numericUpDown_LineThickness.Minimum = 1;
            numericUpDown_LineThickness.Maximum = 10;
            numericUpDown_LineThickness.Value = 2;

            // CheckBox 초기화
            checkBox_ShowCentroid.Checked = true;
            checkBox_ShowArea.Checked = true;
            checkBox_ShowOrientation.Checked = false;
            checkBox_ShowBoundingBox.Checked = true;
            checkBox_ShowEccentricity.Checked = false;

            // 색상 버튼 초기화
            button_DrawColor.FillColor = Color.Red;
        }

        private void SetupEventHandlers()
        {
            // TrackBar - NumericUpDown 동기화 (이진화 임계값)
            trackBar_Threshold.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    numericUpDown_Threshold.Value = trackBar_Threshold.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };
            numericUpDown_Threshold.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    trackBar_Threshold.Value = (int)numericUpDown_Threshold.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            // TrackBar - NumericUpDown 동기화 (선 두께)
            trackBar_LineThickness.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    numericUpDown_LineThickness.Value = trackBar_LineThickness.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };
            numericUpDown_LineThickness.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    trackBar_LineThickness.Value = (int)numericUpDown_LineThickness.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            // CheckBox 이벤트
            checkBox_ShowCentroid.CheckedChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };
            checkBox_ShowArea.CheckedChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };
            checkBox_ShowOrientation.CheckedChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };
            checkBox_ShowBoundingBox.CheckedChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };
            checkBox_ShowEccentricity.CheckedChanged += (s, e) => { if (!_suppressEvents) RaiseParameterChanged(); };

            // 색상 버튼 이벤트
            button_DrawColor.Click += (s, e) => {
                using (var colorDialog = new ColorDialog())
                {
                    colorDialog.Color = button_DrawColor.FillColor;
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        button_DrawColor.FillColor = colorDialog.Color;
                        if (!_suppressEvents) RaiseParameterChanged();
                    }
                }
            };
        }

        private void RaiseParameterChanged()
        {
            if (_suppressEvents) return;

            int threshold = (int)numericUpDown_Threshold.Value;
            bool showCentroid = checkBox_ShowCentroid.Checked;
            bool showArea = checkBox_ShowArea.Checked;
            bool showOrientation = checkBox_ShowOrientation.Checked;
            bool showBoundingBox = checkBox_ShowBoundingBox.Checked;
            bool showEccentricity = checkBox_ShowEccentricity.Checked;
            Color drawColor = button_DrawColor.FillColor;
            int lineThickness = (int)numericUpDown_LineThickness.Value;

            OnParametersChanged?.Invoke(threshold, showCentroid, showArea, showOrientation,
                                      showBoundingBox, showEccentricity, drawColor, lineThickness);
            OnParametersChangedBase?.Invoke();
        }
        #endregion
    }
}