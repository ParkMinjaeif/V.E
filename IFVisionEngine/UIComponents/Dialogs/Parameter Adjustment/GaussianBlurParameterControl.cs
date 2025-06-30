using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IFVisionEngine.UIComponents.Dialogs
{
    /// <summary>
    /// Gaussian Blur(가우시안 블러) 파라미터를 설정하는 사용자 컨트롤
    /// 커널 크기를 조정하여 블러 강도를 제어할 수 있습니다.
    /// </summary>
    public partial class GaussianBlurParameterControl : UserControl, IPreprocessParameterControl
    {
        #region Events
        public event Action<int> OnParametersChanged; // 커널 크기 변경 이벤트
        public event Action OnParametersChangedBase; // 기본 파라미터 변경 이벤트
        #endregion

        #region Private Fields
        private bool _suppressEvents = false; // 이벤트 중복 발생 방지 플래그
        #endregion

        #region Constructor
        public GaussianBlurParameterControl()
        {
            InitializeComponent();
            this.Load += GaussianBlurParameterControl_Load;
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
                int kernelSize = ExtractKernelSizeFromParameters(parameters); // 파라미터에서 커널 크기 추출
                kernelSize = ValidateAndFixKernelSize(kernelSize); // 커널 크기 검증 및 수정
                ApplyKernelSizeToControls(kernelSize); // UI 컨트롤에 적용
                LogAdditionalParameters(parameters); // 추가 파라미터 로깅
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
            int kernel = EnsureOddKernelSize((int)numericUpDown1.Value); // 홀수 커널 크기 보장

            return new Dictionary<string, object>
            {
                { "KernelWidth", kernel }, // 커널 너비 (홀수 보장)
                { "KernelHeight", kernel }, // 커널 높이 (홀수 보장)
                { "SigmaX", (double)0 }, // X축 표준편차 (0이면 자동 계산)
                { "SigmaY", (double)0 } // Y축 표준편차 (0이면 자동 계산)
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
                trackBar1.Value = 5; // 기본 커널 크기
                numericUpDown1.Value = 5; // 기본 커널 크기
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
        /// 파라미터 딕셔너리에서 커널 크기를 추출합니다.
        /// </summary>
        private int ExtractKernelSizeFromParameters(Dictionary<string, object> parameters)
        {
            int kernelSize = 5; // 기본값

            // KernelSize 우선 확인
            if (parameters.ContainsKey("KernelSize"))
            {
                if (int.TryParse(parameters["KernelSize"].ToString(), out int kSize))
                    kernelSize = kSize;
            }
            // 하위 호환성을 위한 KernelWidth 확인
            else if (parameters.ContainsKey("KernelWidth"))
            {
                if (int.TryParse(parameters["KernelWidth"].ToString(), out int kWidth))
                    kernelSize = kWidth;
            }

            return kernelSize;
        }

        /// <summary>
        /// 커널 크기를 검증하고 유효한 값으로 수정합니다.
        /// </summary>
        private int ValidateAndFixKernelSize(int kernelSize)
        {
            kernelSize = EnsureOddKernelSize(kernelSize); // 홀수 보장
            kernelSize = Math.Max(1, Math.Min(31, kernelSize)); // 범위 제한 (1 ~ 31)
            return kernelSize;
        }

        /// <summary>
        /// 커널 크기가 홀수가 되도록 보장합니다.
        /// </summary>
        private int EnsureOddKernelSize(int kernelSize)
        {
            if (kernelSize % 2 == 0) kernelSize += 1; // 짝수면 +1하여 홀수로 만듦
            return kernelSize;
        }

        /// <summary>
        /// 검증된 커널 크기를 UI 컨트롤에 적용합니다.
        /// </summary>
        private void ApplyKernelSizeToControls(int kernelSize)
        {
            trackBar1.Value = kernelSize;
            numericUpDown1.Value = kernelSize;
        }

        /// <summary>
        /// 추가 파라미터들을 로깅합니다 (현재는 SigmaX, SigmaY만 로깅).
        /// </summary>
        private void LogAdditionalParameters(Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey("SigmaX"))
            {
                Console.WriteLine($"SigmaX 값: {parameters["SigmaX"]}");
            }

            if (parameters.ContainsKey("SigmaY"))
            {
                Console.WriteLine($"SigmaY 값: {parameters["SigmaY"]}");
            }
        }

        /// <summary>
        /// 모든 컨트롤의 초기값을 설정합니다.
        /// </summary>
        private void InitializeControls()
        {
            // TrackBar 설정 (홀수만 허용)
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 31;
            trackBar1.Value = 5;
            trackBar1.SmallChange = 2; // 2씩 증가하여 홀수 유지

            // NumericUpDown 설정 (홀수만 허용)
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 31;
            numericUpDown1.Value = 5;
            numericUpDown1.Increment = 2; // 2씩 증가하여 홀수 유지
        }

        /// <summary>
        /// TrackBar와 NumericUpDown 간의 동기화 이벤트를 설정합니다.
        /// </summary>
        private void SetupControlSynchronization()
        {
            // TrackBar → NumericUpDown 동기화
            trackBar1.ValueChanged += (s, ev) =>
            {
                if (_suppressEvents) return;

                int val = EnsureOddKernelSize(trackBar1.Value); // 홀수 보장

                _suppressEvents = true; // 상호 호출 방지
                try
                {
                    if (numericUpDown1.Value != val)
                        numericUpDown1.Value = val;
                }
                finally
                {
                    _suppressEvents = false;
                }

                RaiseParameterChanged();
            };

            // NumericUpDown → TrackBar 동기화
            numericUpDown1.ValueChanged += (s, ev) =>
            {
                if (_suppressEvents) return;

                int val = EnsureOddKernelSize((int)numericUpDown1.Value); // 홀수 보장

                _suppressEvents = true; // 상호 호출 방지
                try
                {
                    if (trackBar1.Value != val)
                        trackBar1.Value = val;
                }
                finally
                {
                    _suppressEvents = false;
                }

                RaiseParameterChanged();
            };
        }

        /// <summary>
        /// 최소/최대값 표시 라벨을 업데이트합니다.
        /// </summary>
        private void UpdateMinMaxLabels()
        {
            kernelminimum.Text = trackBar1.Minimum.ToString(); // 커널 최소값
            kernelMaximum.Text = trackBar1.Maximum.ToString(); // 커널 최대값
        }

        /// <summary>
        /// 파라미터 변경 이벤트를 발생시킵니다.
        /// </summary>
        private void RaiseParameterChanged()
        {
            if (_suppressEvents) return;

            int ksize = EnsureOddKernelSize(trackBar1.Value); // 홀수 커널 크기 보장

            OnParametersChanged?.Invoke(ksize); // 파라미터 변경 이벤트 발생
            OnParametersChangedBase?.Invoke(); // 기본 이벤트 발생
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
                trackBar1.Value = 5;
                numericUpDown1.Value = 5;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 기본값 설정 실패: {ex.Message}");
            }
        }
        #endregion

        #region Debug Methods
        /// <summary>
        /// 디버깅용: 현재 설정값을 콘솔에 출력합니다.
        /// </summary>
        public void LogCurrentValues()
        {
            Console.WriteLine("=== GaussianBlur 현재 설정 ===");
            Console.WriteLine($"TrackBar Value: {trackBar1.Value}");
            Console.WriteLine($"NumericUpDown Value: {numericUpDown1.Value}");
            Console.WriteLine($"GetParameters() 결과:");

            var parameters = GetParameters();
            foreach (var param in parameters)
            {
                Console.WriteLine($"  {param.Key}: {param.Value}");
            }
            Console.WriteLine("=============================");
        }
        #endregion
    }
}