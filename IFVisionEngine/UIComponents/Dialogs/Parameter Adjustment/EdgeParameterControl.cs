using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IFVisionEngine.UIComponents.Dialogs
{
    /// <summary>
    /// Edge Detection 파라미터를 설정하는 사용자 컨트롤
    /// </summary>
    public partial class EdgeParameterControl : UserControl, IPreprocessParameterControl, IParameterLoadable
    {
        #region Events
        public event Action<double, double, string> OnParametersChanged; // Threshold1, Threshold2, Method 변경 이벤트
        public event Action OnParametersChangedBase; // 기본 파라미터 변경 이벤트
        #endregion

        #region Private Fields
        private bool _suppressEvents = false; // 이벤트 중복 발생 방지 플래그
        #endregion

        #region Constructor
        public EdgeParameterControl()
        {
            InitializeComponent();
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
                if (parameters.ContainsKey("Method"))
                {
                    string method = parameters["Method"].ToString();
                    int index = comboBox_Method.Items.IndexOf(method);
                    if (index >= 0)
                    {
                        comboBox_Method.SelectedIndex = index;
                        UpdateParameterVisibility(); // 메서드에 따라 파라미터 표시/숨김
                    }
                }

                if (parameters.ContainsKey("CannyThreshold1"))
                {
                    if (double.TryParse(parameters["CannyThreshold1"].ToString(), out double threshold1))
                    {
                        threshold1 = Math.Max(0, Math.Min(500, threshold1)); // 범위 제한 (0 ~ 500)
                        trackBar_Threshold1.Value = (int)threshold1;
                        numericUpDown_Threshold1.Value = (decimal)threshold1;
                    }
                }

                if (parameters.ContainsKey("CannyThreshold2"))
                {
                    if (double.TryParse(parameters["CannyThreshold2"].ToString(), out double threshold2))
                    {
                        threshold2 = Math.Max(0, Math.Min(500, threshold2)); // 범위 제한 (0 ~ 500)
                        trackBar_Threshold2.Value = (int)threshold2;
                        numericUpDown_Threshold2.Value = (decimal)threshold2;
                    }
                }

                if (parameters.ContainsKey("KernelSize"))
                {
                    if (int.TryParse(parameters["KernelSize"].ToString(), out int kernelSize))
                    {
                        // 홀수만 허용 (1, 3, 5, 7, ...)
                        if (kernelSize % 2 == 0) kernelSize++;
                        kernelSize = Math.Max(1, Math.Min(15, kernelSize)); // 범위 제한 (1 ~ 15)

                        // ComboBox에서 해당 값 찾기
                        int index = comboBox_KernelSize.Items.IndexOf(kernelSize.ToString());
                        if (index >= 0) comboBox_KernelSize.SelectedIndex = index;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Edge 파라미터 설정 실패: {ex.Message}");
                ResetToDefaultValues(); // 실패시 기본값으로 복원
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
            var parameters = new Dictionary<string, object>
            {
                { "Method", comboBox_Method.SelectedItem?.ToString() ?? "Canny" }
            };

            string selectedMethod = comboBox_Method.SelectedItem?.ToString() ?? "Canny";

            if (selectedMethod == "Canny")
            {
                parameters.Add("CannyThreshold1", (double)numericUpDown_Threshold1.Value);
                parameters.Add("CannyThreshold2", (double)numericUpDown_Threshold2.Value);
            }
            else if (selectedMethod == "Sobel" || selectedMethod == "Laplacian")
            {
                int kernelSize = int.Parse(comboBox_KernelSize.SelectedItem?.ToString() ?? "3");
                parameters.Add("KernelSize", kernelSize);
            }

            return parameters;
        }

        /// <summary>
        /// 모든 파라미터를 기본값으로 초기화합니다.
        /// </summary>
        public void ResetParametersToDefault()
        {
            _suppressEvents = true;

            try
            {
                comboBox_Method.SelectedIndex = 0; // Canny
                trackBar_Threshold1.Value = 100;
                numericUpDown_Threshold1.Value = 100;
                trackBar_Threshold2.Value = 200;
                numericUpDown_Threshold2.Value = 200;
                comboBox_KernelSize.SelectedIndex = 1; // 3
                UpdateParameterVisibility();
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
                InitializeControls();
                SetupEventHandlers();
                UpdateParameterVisibility();
            }
            finally
            {
                _suppressEvents = false; // 초기화 완료
            }

            RaiseParameterChanged(); // 초기값 이벤트 발생
        }

        private void InitializeControls()
        {
            // Method ComboBox 초기화
            comboBox_Method.Items.AddRange(new string[] { "Canny", "Sobel", "Laplacian" });
            comboBox_Method.SelectedIndex = 0; // Canny

            // KernelSize ComboBox 초기화 (홀수만)
            comboBox_KernelSize.Items.AddRange(new string[] { "1", "3", "5", "7", "9", "11", "13", "15" });
            comboBox_KernelSize.SelectedIndex = 1; // 3

            // Threshold1 컨트롤 설정 (0 ~ 500)
            trackBar_Threshold1.Minimum = 0;
            trackBar_Threshold1.Maximum = 500;
            trackBar_Threshold1.Value = 100;
            numericUpDown_Threshold1.Minimum = 0;
            numericUpDown_Threshold1.Maximum = 500;
            numericUpDown_Threshold1.Value = 100;

            // Threshold2 컨트롤 설정 (0 ~ 500)
            trackBar_Threshold2.Minimum = 0;
            trackBar_Threshold2.Maximum = 500;
            trackBar_Threshold2.Value = 200;
            numericUpDown_Threshold2.Minimum = 0;
            numericUpDown_Threshold2.Maximum = 500;
            numericUpDown_Threshold2.Value = 200;
        }

        private void SetupEventHandlers()
        {
            // Method ComboBox 이벤트
            comboBox_Method.SelectedIndexChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    UpdateParameterVisibility();
                    RaiseParameterChanged();
                }
            };

            // KernelSize ComboBox 이벤트
            comboBox_KernelSize.SelectedIndexChanged += (s, e) => {
                if (!_suppressEvents) RaiseParameterChanged();
            };

            // Threshold1 동기화
            trackBar_Threshold1.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    numericUpDown_Threshold1.Value = trackBar_Threshold1.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            numericUpDown_Threshold1.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    trackBar_Threshold1.Value = (int)numericUpDown_Threshold1.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            // Threshold2 동기화
            trackBar_Threshold2.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    numericUpDown_Threshold2.Value = trackBar_Threshold2.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            numericUpDown_Threshold2.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    trackBar_Threshold2.Value = (int)numericUpDown_Threshold2.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };
        }

        /// <summary>
        /// 선택된 메서드에 따라 파라미터 컨트롤의 표시/숨김을 업데이트합니다.
        /// </summary>
        private void UpdateParameterVisibility()
        {
            string selectedMethod = comboBox_Method.SelectedItem?.ToString() ?? "Canny";

            if (selectedMethod == "Canny")
            {
                // Canny: Threshold1, Threshold2 표시
                label_Threshold1.Visible = true;
                panel_Threshold1.Visible = true;
                label_Threshold2.Visible = true;
                panel_Threshold2.Visible = true;
                label_KernelSize.Visible = false;
                comboBox_KernelSize.Visible = false;

                label_Threshold1.Text = "하위 임계값:";
                label_Threshold2.Text = "상위 임계값:";
            }
            else if (selectedMethod == "Sobel" || selectedMethod == "Laplacian")
            {
                // Sobel/Laplacian: KernelSize만 표시
                label_Threshold1.Visible = false;
                panel_Threshold1.Visible = false;
                label_Threshold2.Visible = false;
                panel_Threshold2.Visible = false;
                label_KernelSize.Visible = true;
                comboBox_KernelSize.Visible = true;
            }
        }

        /// <summary>
        /// 파라미터 변경 이벤트를 발생시킵니다.
        /// </summary>
        private void RaiseParameterChanged()
        {
            if (_suppressEvents) return;

            double threshold1 = (double)numericUpDown_Threshold1.Value;
            double threshold2 = (double)numericUpDown_Threshold2.Value;
            string method = comboBox_Method.SelectedItem?.ToString() ?? "Canny";

            OnParametersChanged?.Invoke(threshold1, threshold2, method);
            OnParametersChangedBase?.Invoke();
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

        /// <summary>
        /// 모든 컨트롤을 기본값으로 설정합니다.
        /// </summary>
        private void ResetToDefaultValues()
        {
            try
            {
                comboBox_Method.SelectedIndex = 0;
                trackBar_Threshold1.Value = 100;
                numericUpDown_Threshold1.Value = 100;
                trackBar_Threshold2.Value = 200;
                numericUpDown_Threshold2.Value = 200;
                comboBox_KernelSize.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 기본값 설정 실패: {ex.Message}");
            }
        }
        #endregion
    }
}