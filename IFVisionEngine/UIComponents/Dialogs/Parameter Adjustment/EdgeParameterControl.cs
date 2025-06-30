using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static MyNodesContext;

namespace IFVisionEngine.UIComponents.Dialogs
{
    /// <summary>
    /// Edge Detection(엣지 검출) 파라미터를 설정하는 사용자 컨트롤
    /// Canny Edge Detection 알고리즘의 임계값을 조정할 수 있습니다.
    /// </summary>
    public partial class EdgeParameterControl : UserControl, IPreprocessParameterControl, IParameterLoadable
    {
        #region Events
        public event Action<double, double,string> OnParametersChanged; // threshold1, threshold2 변경 이벤트
        public event Action OnParametersChangedBase; // 기본 파라미터 변경 이벤트
        #endregion

        #region Private Fields
        private bool _suppressEvents = false; // 이벤트 중복 발생 방지 플래그
        #endregion

        #region Constructor
        public EdgeParameterControl()
        {
            InitializeComponent();
            this.Load += EdgeParameterControl_Load;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 외부에서 전달된 파라미터값을 UI 컨트롤에 설정합니다.
        /// </summary>
        /// <param name="parameters">설정할 파라미터 딕셔너리</param>
        public void SetCurrentParameters(Dictionary<string, object> parameters)
        {
            if (parameters == null) return;

            _suppressEvents = true; // 이벤트 억제 시작

            try
            {
                SetEdgeMethod(parameters);
                SetCannyThresholdParameters(parameters); // Canny 임계값 파라미터 설정
                SetLegacyThresholdParameters(parameters); // 하위 호환성을 위한 파라미터 설정
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Edge 파라미터 설정 실패: {ex.Message}");
                ResetParametersToDefault(); // 실패시 기본값으로 복원
            }
            finally
            {
                _suppressEvents = false; // 이벤트 억제 해제
                SafeRaiseParameterChanged(); // 안전하게 이벤트 발생
            }
        }

        /// <summary>
        /// 현재 설정된 파라미터값들을 반환합니다.
        /// </summary>
        /// <returns>파라미터 딕셔너리</returns>
        public Dictionary<string, object> GetParameters()
        {
            return new Dictionary<string, object>
            {
                { "Method", comboBox_edgeMethod.SelectedItem ?? EdgeDetectionParameters.EdgeMethod.Canny }, // 엣지 검출 방법
                { "CannyThreshold1", (double)numericUpDown_threshold1.Value }, // 낮은 임계값
                { "CannyThreshold2", (double)numericUpDown_threshold2.Value }, // 높은 임계값
                { "KernelSize", 3 } // 커널 크기 (고정값)
            };
        }

        /// <summary>
        /// 모든 파라미터를 기본값으로 초기화합니다.
        /// </summary>
        public void ResetParametersToDefault()
        {
            _suppressEvents = true;

            try
            {
                trackBar_threshold1.Value = 100; // 낮은 임계값 기본값
                trackBar_threshold2.Value = 200; // 높은 임계값 기본값
            }
            finally
            {
                _suppressEvents = false;
            }

            RaiseParameterChanged();
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// 컨트롤 로드 시 초기화를 수행합니다.
        /// </summary>
        private void EdgeParameterControl_Load(object sender, EventArgs e)
        {
            _suppressEvents = true; // 초기화 중 이벤트 억제

            try
            {
                InitializeControls(); // 컨트롤 초기값 설정
                SetupControlSynchronization(); // 컨트롤 간 동기화 설정
                UpdateMinMaxLabels(); // 최소/최대값 라벨 업데이트
            }
            finally
            {
                _suppressEvents = false; // 초기화 완료
            }

            RaiseParameterChanged(); // 초기값 이벤트 발생
        }
        #endregion

        #region Private Helper Methods
        /// <summary>
        /// Canny 임계값 파라미터를 설정합니다.
        /// </summary>
        private void SetCannyThresholdParameters(Dictionary<string, object> parameters)
        {
            // CannyThreshold1 (낮은 임계값) 설정
            if (parameters.ContainsKey("CannyThreshold1"))
            {
                if (double.TryParse(parameters["CannyThreshold1"].ToString(), out double threshold1))
                {
                    threshold1 = Math.Max(0, Math.Min(255, threshold1)); // 범위 제한 (0 ~ 255)
                    numericUpDown_threshold1.Value = (decimal)threshold1;
                    trackBar_threshold1.Value = (int)threshold1;
                }
            }

            // CannyThreshold2 (높은 임계값) 설정
            if (parameters.ContainsKey("CannyThreshold2"))
            {
                if (double.TryParse(parameters["CannyThreshold2"].ToString(), out double threshold2))
                {
                    threshold2 = Math.Max(0, Math.Min(255, threshold2)); // 범위 제한 (0 ~ 255)
                    numericUpDown_threshold2.Value = (decimal)threshold2;
                    trackBar_threshold2.Value = (int)threshold2;
                }
            }
        }
        private void SetEdgeMethod(Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey("Method"))
            {
                var methodStr = parameters["Method"].ToString();
                for (int i = 0; i < comboBox_edgeMethod.Items.Count; i++)
                {
                    if (comboBox_edgeMethod.Items[i].ToString().Equals(methodStr, StringComparison.OrdinalIgnoreCase))
                    {
                        comboBox_edgeMethod.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 하위 호환성을 위한 레거시 파라미터를 설정합니다.
        /// </summary>
        private void SetLegacyThresholdParameters(Dictionary<string, object> parameters)
        {
            // LowThreshold (하위 호환성)
            if (parameters.ContainsKey("LowThreshold"))
            {
                if (double.TryParse(parameters["LowThreshold"].ToString(), out double lowThreshold))
                {
                    lowThreshold = Math.Max(0, Math.Min(255, lowThreshold));
                    numericUpDown_threshold1.Value = (decimal)lowThreshold;
                    trackBar_threshold1.Value = (int)lowThreshold;
                }
            }

            // HighThreshold (하위 호환성)
            if (parameters.ContainsKey("HighThreshold"))
            {
                if (double.TryParse(parameters["HighThreshold"].ToString(), out double highThreshold))
                {
                    highThreshold = Math.Max(0, Math.Min(255, highThreshold));
                    numericUpDown_threshold2.Value = (decimal)highThreshold;
                    trackBar_threshold2.Value = (int)highThreshold;
                }
            }
        }

        /// <summary>
        /// 모든 컨트롤의 초기값을 설정합니다.
        /// </summary>
        private void InitializeControls()
        {
            // Threshold1 컨트롤 설정
            trackBar_threshold1.Minimum = 0;
            trackBar_threshold1.Maximum = 255;
            trackBar_threshold1.Value = 100;
            numericUpDown_threshold1.Minimum = 0;
            numericUpDown_threshold1.Maximum = 255;
            numericUpDown_threshold1.Value = 100;

            // Threshold2 컨트롤 설정
            trackBar_threshold2.Minimum = 0;
            trackBar_threshold2.Maximum = 255;
            trackBar_threshold2.Value = 200;
            numericUpDown_threshold2.Minimum = 0;
            numericUpDown_threshold2.Maximum = 255;
            numericUpDown_threshold2.Value = 200;

            // comboBox 컨트롤 설정
            comboBox_edgeMethod.Items.Clear();
            comboBox_edgeMethod.Items.Add(EdgeDetectionParameters.EdgeMethod.Canny.ToString());
            comboBox_edgeMethod.Items.Add(EdgeDetectionParameters.EdgeMethod.Sobel.ToString());
            comboBox_edgeMethod.Items.Add(EdgeDetectionParameters.EdgeMethod.Laplacian.ToString());
            comboBox_edgeMethod.SelectedIndex = 0; // 기본값
            comboBox_edgeMethod.SelectedIndexChanged += (s, e) => {
                if (_suppressEvents) return;
                RaiseParameterChanged();
            };
        }

        /// <summary>
        /// TrackBar와 NumericUpDown 간의 동기화 이벤트를 설정합니다.
        /// </summary>
        private void SetupControlSynchronization()
        {
            // Threshold1 동기화
            trackBar_threshold1.ValueChanged += (s, ev) =>
            {
                if (_suppressEvents) return;
                if (numericUpDown_threshold1.Value != trackBar_threshold1.Value)
                    numericUpDown_threshold1.Value = trackBar_threshold1.Value;
                RaiseParameterChanged();
            };

            numericUpDown_threshold1.ValueChanged += (s, ev) =>
            {
                if (_suppressEvents) return;
                if (trackBar_threshold1.Value != (int)numericUpDown_threshold1.Value)
                    trackBar_threshold1.Value = (int)numericUpDown_threshold1.Value;
                RaiseParameterChanged();
            };

            // Threshold2 동기화
            trackBar_threshold2.ValueChanged += (s, ev) =>
            {
                if (_suppressEvents) return;
                if (numericUpDown_threshold2.Value != trackBar_threshold2.Value)
                    numericUpDown_threshold2.Value = trackBar_threshold2.Value;
                RaiseParameterChanged();
            };

            numericUpDown_threshold2.ValueChanged += (s, ev) =>
            {
                if (_suppressEvents) return;
                if (trackBar_threshold2.Value != (int)numericUpDown_threshold2.Value)
                    trackBar_threshold2.Value = (int)numericUpDown_threshold2.Value;
                RaiseParameterChanged();
            };
        }

        /// <summary>
        /// 최소/최대값 표시 라벨을 업데이트합니다.
        /// </summary>
        private void UpdateMinMaxLabels()
        {
            Threshold1minimum.Text = trackBar_threshold1.Minimum.ToString(); // Threshold1 최소값
            Threshold1Maximum.Text = trackBar_threshold1.Maximum.ToString(); // Threshold1 최대값
            Threshold2minimum.Text = trackBar_threshold2.Minimum.ToString(); // Threshold2 최소값
            Threshold2Maximum.Text = trackBar_threshold2.Maximum.ToString(); // Threshold2 최대값
        }

        /// <summary>
        /// 파라미터 변경 이벤트를 발생시킵니다.
        /// </summary>
        private void RaiseParameterChanged()
        {
            if (_suppressEvents) return;

            double t1 = trackBar_threshold1.Value; // 낮은 임계값
            double t2 = trackBar_threshold2.Value; // 높은 임계값
            string method = comboBox_edgeMethod.Text.ToString();
            OnParametersChanged?.Invoke(t1, t2,method); // 파라미터 변경 이벤트 발생
            OnParametersChangedBase?.Invoke(); // 기본 이벤트 발생
                                               // Canny 선택 여부
            bool isCanny = method == EdgeDetectionParameters.EdgeMethod.Canny.ToString();

            // 컨트롤 활성/비활성 및 색상
            trackBar_threshold1.Enabled = isCanny;
            trackBar_threshold2.Enabled = isCanny;
            numericUpDown_threshold1.Enabled = isCanny;
            numericUpDown_threshold2.Enabled = isCanny;

            // 흐리게 처리 (BackColor는 NumericUpDown에만 잘 적용, TrackBar는 보통 Enabled만)
            numericUpDown_threshold1.BackColor = isCanny ? SystemColors.Window : SystemColors.ControlLight;
            numericUpDown_threshold2.BackColor = isCanny ? SystemColors.Window : SystemColors.ControlLight;
        }

        /// <summary>
        /// 안전하게 파라미터 변경 이벤트를 발생시킵니다.
        /// </summary>
        private void SafeRaiseParameterChanged()
        {
            try
            {
                RaiseParameterChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ RaiseParameterChanged 실행 실패: {ex.Message}");
            }
        }
        #endregion
    }
}