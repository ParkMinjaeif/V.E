using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IFVisionEngine.UIComponents.Dialogs
{
    /// <summary>
    /// CLAHE(Contrast Limited Adaptive Histogram Equalization) 파라미터를 설정하는 사용자 컨트롤
    /// </summary>
    public partial class CLAHEParameterControl : UserControl, IPreprocessParameterControl, IParameterLoadable
    {
        #region Events
        public event Action<double, int, int> OnParametersChanged; // ClipLimit, TileHeight, TileWidth 변경 이벤트
        public event Action OnParametersChangedBase; // 기본 파라미터 변경 이벤트
        #endregion

        #region Private Fields
        private bool _suppressEvents = false; // 이벤트 중복 발생 방지 플래그
        #endregion

        #region Constructor
        public CLAHEParameterControl()
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
                if (parameters.ContainsKey("ClipLimit"))
                {
                    if (double.TryParse(parameters["ClipLimit"].ToString(), out double clipLimit))
                    {
                        clipLimit = Math.Max(1.0, Math.Min(8.0, clipLimit)); // 범위 제한 (1.0 ~ 8.0)

                        // TrackBar는 10배 스케일링 (10~80)
                        int trackBarValue = (int)(clipLimit * 10);
                        trackBar_ClipLimit.Value = Math.Min(trackBar_ClipLimit.Maximum, Math.Max(trackBar_ClipLimit.Minimum, trackBarValue));
                        numericUpDown_ClipLimit.Value = (decimal)clipLimit;
                    }
                }

                if (parameters.ContainsKey("TileGridWidth"))
                {
                    if (int.TryParse(parameters["TileGridWidth"].ToString(), out int tileWidth))
                    {
                        tileWidth = Math.Max(4, Math.Min(32, tileWidth)); // 범위 제한 (4 ~ 32)
                        trackBar_TileWidth.Value = tileWidth;
                        numericUpDown_TileWidth.Value = tileWidth;
                    }
                }

                if (parameters.ContainsKey("TileGridHeight"))
                {
                    if (int.TryParse(parameters["TileGridHeight"].ToString(), out int tileHeight))
                    {
                        tileHeight = Math.Max(4, Math.Min(32, tileHeight)); // 범위 제한 (4 ~ 32)
                        trackBar_TileHeight.Value = tileHeight;
                        numericUpDown_TileHeight.Value = tileHeight;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ CLAHE 파라미터 설정 실패: {ex.Message}");
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
                { "TileGridWidth", (int)numericUpDown_TileWidth.Value },
                { "TileGridHeight", (int)numericUpDown_TileHeight.Value },
                { "ClipLimit", (double)numericUpDown_ClipLimit.Value }
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
                trackBar_ClipLimit.Value = 20; // ClipLimit: 2.0
                numericUpDown_ClipLimit.Value = 2.0m;
                trackBar_TileHeight.Value = 8;  // TileHeight: 8
                numericUpDown_TileHeight.Value = 8;
                trackBar_TileWidth.Value = 8;  // TileWidth: 8
                numericUpDown_TileWidth.Value = 8;
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
        private void CLAHEParameterControl_Load(object sender, EventArgs e)
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
            // ClipLimit 컨트롤 설정 (1.0 ~ 8.0, TrackBar는 10~80)
            trackBar_ClipLimit.Minimum = 10;   // 1.0
            trackBar_ClipLimit.Maximum = 80;   // 8.0  
            trackBar_ClipLimit.Value = 20;     // 2.0

            numericUpDown_ClipLimit.Minimum = 1.0m;
            numericUpDown_ClipLimit.Maximum = 8.0m;
            numericUpDown_ClipLimit.DecimalPlaces = 1;
            numericUpDown_ClipLimit.Increment = 0.1m;
            numericUpDown_ClipLimit.Value = 2.0m;

            // TileHeight 컨트롤 설정
            trackBar_TileHeight.Minimum = 4;
            trackBar_TileHeight.Maximum = 32;
            trackBar_TileHeight.Value = 8;
            numericUpDown_TileHeight.Minimum = 4;
            numericUpDown_TileHeight.Maximum = 32;
            numericUpDown_TileHeight.Value = 8;

            // TileWidth 컨트롤 설정
            trackBar_TileWidth.Minimum = 4;
            trackBar_TileWidth.Maximum = 32;
            trackBar_TileWidth.Value = 8;
            numericUpDown_TileWidth.Minimum = 4;
            numericUpDown_TileWidth.Maximum = 32;
            numericUpDown_TileWidth.Value = 8;
        }

        private void SetupEventHandlers()
        {
            // ClipLimit 동기화 (10배 스케일링)
            trackBar_ClipLimit.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    decimal numValue = trackBar_ClipLimit.Value / 10.0m;
                    numericUpDown_ClipLimit.Value = numValue;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            numericUpDown_ClipLimit.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    int trkValue = (int)(numericUpDown_ClipLimit.Value * 10);
                    trackBar_ClipLimit.Value = Math.Min(trackBar_ClipLimit.Maximum, Math.Max(trackBar_ClipLimit.Minimum, trkValue));
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            // TileHeight 동기화
            trackBar_TileHeight.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    numericUpDown_TileHeight.Value = trackBar_TileHeight.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            numericUpDown_TileHeight.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    trackBar_TileHeight.Value = (int)numericUpDown_TileHeight.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            // TileWidth 동기화
            trackBar_TileWidth.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    numericUpDown_TileWidth.Value = trackBar_TileWidth.Value;
                    _suppressEvents = false;
                    RaiseParameterChanged();
                }
            };

            numericUpDown_TileWidth.ValueChanged += (s, e) => {
                if (!_suppressEvents)
                {
                    _suppressEvents = true;
                    trackBar_TileWidth.Value = (int)numericUpDown_TileWidth.Value;
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

            double clipLimit = Math.Max(1.0, (double)numericUpDown_ClipLimit.Value); // 0으로 나누기 방지
            int tileHeight = Math.Max(1, (int)numericUpDown_TileHeight.Value); // 최소값 보장
            int tileWidth = Math.Max(1, (int)numericUpDown_TileWidth.Value); // 최소값 보장

            OnParametersChanged?.Invoke(clipLimit, tileHeight, tileWidth); // 파라미터 변경 이벤트 발생
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
                numericUpDown_ClipLimit.Value = 2.0m;
                trackBar_ClipLimit.Value = 20;
                numericUpDown_TileHeight.Value = 8;
                trackBar_TileHeight.Value = 8;
                numericUpDown_TileWidth.Value = 8;
                trackBar_TileWidth.Value = 8;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 기본값 설정 실패: {ex.Message}");
            }
        }
        #endregion
    }
}