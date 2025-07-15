using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IFVisionEngine.UIComponents.Dialogs
{
    /// <summary>
    /// Gaussian Blur 파라미터를 설정하는 사용자 컨트롤
    /// </summary>
    public partial class GaussianBlurParameterControl : UserControl, IPreprocessParameterControl, IParameterLoadable
    {
        #region Events
        public event Action<int> OnParametersChanged; // KernelSize 변경 이벤트 (단순화된 버전)
        public event Action OnParametersChangedBase; // 기본 파라미터 변경 이벤트
        #endregion

        #region Private Fields
        private bool _suppressEvents = false; // 이벤트 중복 발생 방지 플래그
        #endregion

        #region Constructor
        public GaussianBlurParameterControl()
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
                if (parameters.ContainsKey("KernelWidth"))
                {
                    if (int.TryParse(parameters["KernelWidth"].ToString(), out int kernelWidth))
                    {
                        // 홀수만 허용, 범위 제한 (1 ~ 51)
                        if (kernelWidth % 2 == 0) kernelWidth++;
                        kernelWidth = Math.Max(1, Math.Min(51, kernelWidth));

                        trackBar_KernelWidth.Value = kernelWidth;
                        numericUpDown_KernelWidth.Value = kernelWidth;
                    }
                }

                if (parameters.ContainsKey("KernelHeight"))
                {
                    if (int.TryParse(parameters["KernelHeight"].ToString(), out int kernelHeight))
                    {
                        // 홀수만 허용, 범위 제한 (1 ~ 51)
                        if (kernelHeight % 2 == 0) kernelHeight++;
                        kernelHeight = Math.Max(1, Math.Min(51, kernelHeight));

                        trackBar_KernelHeight.Value = kernelHeight;
                        numericUpDown_KernelHeight.Value = kernelHeight;
                    }
                }

                if (parameters.ContainsKey("SigmaX"))
                {
                    if (double.TryParse(parameters["SigmaX"].ToString(), out double sigmaX))
                    {
                        sigmaX = Math.Max(0.0, Math.Min(10.0, sigmaX)); // 범위 제한 (0.0 ~ 10.0)

                        // TrackBar는 10배 스케일링 (0~100)
                        int trackBarValue = (int)(sigmaX * 10);
                        trackBar_SigmaX.Value = Math.Min(trackBar_SigmaX.Maximum, Math.Max(trackBar_SigmaX.Minimum, trackBarValue));
                        numericUpDown_SigmaX.Value = sigmaX;
                    }
                }

                if (parameters.ContainsKey("SigmaY"))
                {
                    if (double.TryParse(parameters["SigmaY"].ToString(), out double sigmaY))
                    {
                        sigmaY = Math.Max(0.0, Math.Min(10.0, sigmaY)); // 범위 제한 (0.0 ~ 10.0)

                        // TrackBar는 10배 스케일링 (0~100)
                        int trackBarValue = (int)(sigmaY * 10);
                        trackBar_SigmaY.Value = Math.Min(trackBar_SigmaY.Maximum, Math.Max(trackBar_SigmaY.Minimum, trackBarValue));
                        numericUpDown_SigmaY.Value = sigmaY;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ GaussianBlur 파라미터 설정 실패: {ex.Message}");
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
            return new Dictionary<string, object>
            {
                { "KernelWidth", (int)numericUpDown_KernelWidth.Value },
                { "KernelHeight", (int)numericUpDown_KernelHeight.Value },
                { "SigmaX", (double)numericUpDown_SigmaX.Value },
                { "SigmaY", (double)numericUpDown_SigmaY.Value }
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
                trackBar_KernelWidth.Value = 5;
                numericUpDown_KernelWidth.Value = 5;
                trackBar_KernelHeight.Value = 5;
                numericUpDown_KernelHeight.Value = 5;
                trackBar_SigmaX.Value = 0; // 0.0
                numericUpDown_SigmaX.Value = (double)0.0m;
                trackBar_SigmaY.Value = 0; // 0.0
                numericUpDown_SigmaY.Value = (double)0.0m;
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
        private void GaussianBlurParameterControl_Load(object sender, EventArgs e)
        {
            _suppressEvents = true; // 초기화 중 이벤트 억제

            try
            {
                InitializeControls();
                SetupEventHandlers();
            }
            finally
            {
                _suppressEvents = false; // 초기화 완료
            }

            RaiseParameterChanged(); // 초기값 이벤트 발생
        }

        private void InitializeControls()
        {
            // KernelWidth 컨트롤 설정 (홀수만, 1 ~ 51)
            trackBar_KernelWidth.Minimum = 1;
            trackBar_KernelWidth.Maximum = 51;
            trackBar_KernelWidth.Value = 5;
            numericUpDown_KernelWidth.Minimum = 1;
            numericUpDown_KernelWidth.Maximum = 51;
            numericUpDown_KernelWidth.Value = 5;

            // KernelHeight 컨트롤 설정 (홀수만, 1 ~ 51)
            trackBar_KernelHeight.Minimum = 1;
            trackBar_KernelHeight.Maximum = 51;
            trackBar_KernelHeight.Value = 5;
            numericUpDown_KernelHeight.Minimum = 1;
            numericUpDown_KernelHeight.Maximum = 51;
            numericUpDown_KernelHeight.Value = 5;

            // SigmaX 컨트롤 설정 (0.0 ~ 10.0, TrackBar는 0~100)
            trackBar_SigmaX.Minimum = 0;
            trackBar_SigmaX.Maximum = 100; // 10.0 * 10
            trackBar_SigmaX.Value = 0; // 0.0
            numericUpDown_SigmaX.Minimum = (double)0.0m;
            numericUpDown_SigmaX.Maximum = (double)10.0m;
            numericUpDown_SigmaX.DecimalPlaces = 1;
            numericUpDown_SigmaX.Value = (double)0.0m;

            // SigmaY 컨트롤 설정 (0.0 ~ 10.0, TrackBar는 0~100)
            trackBar_SigmaY.Minimum = 0;
            trackBar_SigmaY.Maximum = 100; // 10.0 * 10
            trackBar_SigmaY.Value = 0; // 0.0
            numericUpDown_SigmaY.Minimum = (double)0.0m;
            numericUpDown_SigmaY.Maximum = (double)10.0m;
            numericUpDown_SigmaY.DecimalPlaces = 1;
            numericUpDown_SigmaY.Value = (double)0.0m;
        }

        private void SetupEventHandlers()
        {
            // KernelWidth 동기화 (홀수 강제)
            trackBar_KernelWidth.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    int value = trackBar_KernelWidth.Value;
                    if (value % 2 == 0) value++; // 홀수로 강제 변환
                    if (value > trackBar_KernelWidth.Maximum) value = trackBar_KernelWidth.Maximum - (trackBar_KernelWidth.Maximum % 2 == 0 ? 1 : 0);
                    trackBar_KernelWidth.Value = value;
                    numericUpDown_KernelWidth.Value = value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            numericUpDown_KernelWidth.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    int value = (int)numericUpDown_KernelWidth.Value;
                    if (value % 2 == 0) value++; // 홀수로 강제 변환
                    if (value > numericUpDown_KernelWidth.Maximum) value = (int)numericUpDown_KernelWidth.Maximum - ((int)numericUpDown_KernelWidth.Maximum % 2 == 0 ? 1 : 0);
                    numericUpDown_KernelWidth.Value = value;
                    trackBar_KernelWidth.Value = value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            // KernelHeight 동기화 (홀수 강제)
            trackBar_KernelHeight.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    int value = trackBar_KernelHeight.Value;
                    if (value % 2 == 0) value++; // 홀수로 강제 변환
                    if (value > trackBar_KernelHeight.Maximum) value = trackBar_KernelHeight.Maximum - (trackBar_KernelHeight.Maximum % 2 == 0 ? 1 : 0);
                    trackBar_KernelHeight.Value = value;
                    numericUpDown_KernelHeight.Value = value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            numericUpDown_KernelHeight.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    int value = (int)numericUpDown_KernelHeight.Value;
                    if (value % 2 == 0) value++; // 홀수로 강제 변환
                    if (value > numericUpDown_KernelHeight.Maximum) value = (int)numericUpDown_KernelHeight.Maximum - ((int)numericUpDown_KernelHeight.Maximum % 2 == 0 ? 1 : 0);
                    numericUpDown_KernelHeight.Value = value;
                    trackBar_KernelHeight.Value = value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            // SigmaX 동기화 (10배 스케일링)
            trackBar_SigmaX.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    decimal numValue = trackBar_SigmaX.Value / 10.0m;
                    numericUpDown_SigmaX.Value = (double)numValue;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            numericUpDown_SigmaX.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    int trkValue = (int)(numericUpDown_SigmaX.Value * 10);
                    trackBar_SigmaX.Value = Math.Min(trackBar_SigmaX.Maximum, Math.Max(trackBar_SigmaX.Minimum, trkValue));
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            // SigmaY 동기화 (10배 스케일링)
            trackBar_SigmaY.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    decimal numValue = trackBar_SigmaY.Value / 10.0m;
                    numericUpDown_SigmaY.Value = (double)numValue;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            numericUpDown_SigmaY.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    int trkValue = (int)(numericUpDown_SigmaY.Value * 10);
                    trackBar_SigmaY.Value = Math.Min(trackBar_SigmaY.Maximum, Math.Max(trackBar_SigmaY.Minimum, trkValue));
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };
        }

        /// <summary>
        /// 파라미터 변경 이벤트를 발생시킵니다.
        /// </summary>
        private void RaiseParameterChanged()
        {
            if (_suppressEvents) return;

            // 단순화된 버전: KernelWidth만 전달 (기존 호환성 유지)
            int kernelSize = (int)numericUpDown_KernelWidth.Value;

            OnParametersChanged?.Invoke(kernelSize);
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
                trackBar_KernelWidth.Value = 5;
                numericUpDown_KernelWidth.Value = 5;
                trackBar_KernelHeight.Value = 5;
                numericUpDown_KernelHeight.Value = 5;
                trackBar_SigmaX.Value = 0;
                numericUpDown_SigmaX.Value = (double)0.0m;
                trackBar_SigmaY.Value = 0;
                numericUpDown_SigmaY.Value = (double)0.0m;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 기본값 설정 실패: {ex.Message}");
            }
        }
        #endregion
    }
}