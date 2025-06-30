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
                SetClipLimitParameter(parameters); // ClipLimit 파라미터 설정
                SetTileGridWidthParameter(parameters); // TileGridWidth 파라미터 설정  
                SetTileGridHeightParameter(parameters); // TileGridHeight 파라미터 설정
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
                { "TileGridWidth", (int)numericUpDown3.Value },
                { "TileGridHeight", (int)numericUpDown2.Value },
                { "ClipLimit", (double)numericUpDown1.Value }
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
                trackBar1.Value = 20; // ClipLimit: 2.0
                trackBar2.Value = 8;  // TileHeight: 8
                trackBar3.Value = 8;  // TileWidth: 8
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
        private void CLAHE_Load(object sender, EventArgs e)
        {
            _suppressEvents = true; // 초기화 중 이벤트 억제

            try
            {
                InitTrackbarsAndNumericUpDowns(); // 컨트롤 초기값 설정
                SyncTrackbarsWithNumericUpDowns(); // 컨트롤 간 동기화 설정
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
        /// ClipLimit 파라미터를 설정합니다.
        /// </summary>
        private void SetClipLimitParameter(Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey("ClipLimit"))
            {
                if (double.TryParse(parameters["ClipLimit"].ToString(), out double clipLimit))
                {
                    clipLimit = Math.Max(1.0, Math.Min(8.0, clipLimit)); // 범위 제한 (1.0 ~ 4.0)

                    numericUpDown1.Value = (decimal)clipLimit; // NumericUpDown 설정

                    int trackBarValue = Math.Max(10, (int)(clipLimit * 10)); // TrackBar 값 계산 (0으로 나누기 방지)
                    trackBar1.Value = Math.Min(trackBar1.Maximum, Math.Max(trackBar1.Minimum, trackBarValue)); // TrackBar 설정
                }
            }
        }

        /// <summary>
        /// TileGridWidth 파라미터를 설정합니다.
        /// </summary>
        private void SetTileGridWidthParameter(Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey("TileGridWidth"))
            {
                if (int.TryParse(parameters["TileGridWidth"].ToString(), out int tileWidth))
                {
                    tileWidth = Math.Max(4, Math.Min(32, tileWidth)); // 범위 제한 (4 ~ 32)

                    numericUpDown3.Value = tileWidth;
                    trackBar3.Value = tileWidth;
                }
            }
        }

        /// <summary>
        /// TileGridHeight 파라미터를 설정합니다.
        /// </summary>
        private void SetTileGridHeightParameter(Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey("TileGridHeight"))
            {
                if (int.TryParse(parameters["TileGridHeight"].ToString(), out int tileHeight))
                {
                    tileHeight = Math.Max(4, Math.Min(32, tileHeight)); // 범위 제한 (4 ~ 32)

                    numericUpDown2.Value = tileHeight;
                    trackBar2.Value = tileHeight;
                }
            }
        }

        /// <summary>
        /// 모든 컨트롤의 초기값을 설정합니다.
        /// </summary>
        private void InitTrackbarsAndNumericUpDowns()
        {
            // ClipLimit 컨트롤 설정
            trackBar1.Minimum = 10;   // 1.0
            trackBar1.Maximum = 80;   // 4.0  
            trackBar1.Value = 20;     // 2.0

            numericUpDown1.Minimum = 1.0m;
            numericUpDown1.Maximum = 8.0m;
            numericUpDown1.DecimalPlaces = 1;
            numericUpDown1.Increment = 0.1m;
            numericUpDown1.Value = 2.0m;

            // TileHeight 컨트롤 설정
            trackBar2.Minimum = 4;
            trackBar2.Maximum = 32;
            trackBar2.Value = 8;
            numericUpDown2.Minimum = 4;
            numericUpDown2.Maximum = 32;
            numericUpDown2.Value = 8;

            // TileWidth 컨트롤 설정
            trackBar3.Minimum = 4;
            trackBar3.Maximum = 32;
            trackBar3.Value = 8;
            numericUpDown3.Minimum = 4;
            numericUpDown3.Maximum = 32;
            numericUpDown3.Value = 8;
        }

        /// <summary>
        /// TrackBar와 NumericUpDown 간의 동기화 이벤트를 설정합니다.
        /// </summary>
        private void SyncTrackbarsWithNumericUpDowns()
        {
            // ClipLimit 동기화
            trackBar1.ValueChanged += (s, e) => {
                if (_suppressEvents) return;
                decimal numValue = trackBar1.Value / 10.0m;
                if (numericUpDown1.Value != numValue)
                    numericUpDown1.Value = numValue;
                RaiseParameterChanged();
            };

            numericUpDown1.ValueChanged += (s, e) => {
                if (_suppressEvents) return;
                int trkValue = (int)(numericUpDown1.Value * 10);
                if (trackBar1.Value != trkValue)
                    trackBar1.Value = trkValue;
                RaiseParameterChanged();
            };

            // TileHeight 동기화
            trackBar2.ValueChanged += (s, e) => {
                if (_suppressEvents) return;
                if (numericUpDown2.Value != trackBar2.Value)
                    numericUpDown2.Value = trackBar2.Value;
                RaiseParameterChanged();
            };

            numericUpDown2.ValueChanged += (s, e) => {
                if (_suppressEvents) return;
                if (trackBar2.Value != (int)numericUpDown2.Value)
                    trackBar2.Value = (int)numericUpDown2.Value;
                RaiseParameterChanged();
            };

            // TileWidth 동기화
            trackBar3.ValueChanged += (s, e) => {
                if (_suppressEvents) return;
                if (numericUpDown3.Value != trackBar3.Value)
                    numericUpDown3.Value = trackBar3.Value;
                RaiseParameterChanged();
            };

            numericUpDown3.ValueChanged += (s, e) => {
                if (_suppressEvents) return;
                if (trackBar3.Value != (int)numericUpDown3.Value)
                    trackBar3.Value = (int)numericUpDown3.Value;
                RaiseParameterChanged();
            };
        }

        /// <summary>
        /// 최소/최대값 표시 라벨을 업데이트합니다.
        /// </summary>
        private void UpdateMinMaxLabels()
        {
            CLipminimum.Text = (trackBar1.Minimum / 10.0).ToString("F1"); // ClipLimit 최소값
            ClipMaximum.Text = (trackBar1.Maximum / 10.0).ToString("F1"); // ClipLimit 최대값

            TileHeightminimum.Text = trackBar2.Minimum.ToString(); // TileHeight 최소값
            TileHeightMaximum.Text = trackBar2.Maximum.ToString(); // TileHeight 최대값

            TileWidthminimum.Text = trackBar3.Minimum.ToString(); // TileWidth 최소값
            TileWidthMaximum.Text = trackBar3.Maximum.ToString(); // TileWidth 최대값
        }

        /// <summary>
        /// 파라미터 변경 이벤트를 발생시킵니다.
        /// </summary>
        private void RaiseParameterChanged()
        {
            if (_suppressEvents) return;

            double clipLimit = Math.Max(1.0, trackBar1.Value / 10.0); // 0으로 나누기 방지
            int tileHeight = Math.Max(1, trackBar2.Value); // 최소값 보장
            int tileWidth = Math.Max(1, trackBar3.Value); // 최소값 보장

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
                numericUpDown1.Value = 2.0m;
                trackBar1.Value = 20;
                numericUpDown2.Value = 8;
                trackBar2.Value = 8;
                numericUpDown3.Value = 8;
                trackBar3.Value = 8;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 기본값 설정 실패: {ex.Message}");
            }
        }
        #endregion
    }
}