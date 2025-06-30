using IFVisionEngine.UIComponents.UserControls;
using System;
using System.Windows.Forms;
using System.Drawing;
using OpenCvSharp;
using System.Collections.Generic;
using System.Linq;
using IFVisionEngine.UIComponents.Dialogs.Parameter_Description;
using static MyNodesContext;
using System.Security.Cryptography;

namespace IFVisionEngine.UIComponents.Dialogs
{
    /// <summary>
    /// 이미지 전처리 파라미터를 설정하는 다이얼로그 폼
    /// 실시간 미리보기와 픽셀 정보 표시 기능을 제공합니다.
    /// </summary>
    public partial class FormParameters : Form
    {
        #region Private Fields
        private IPreprocessParameterControl _currentParameterControl; // 현재 활성화된 파라미터 컨트롤
        private Mat _originalMat; // 원본 이미지 (OpenCV Mat 형식)
        private IDictionary<string, object> _currentParameters; // 현재 파라미터값들
        #endregion

        #region Public Properties
        public Dictionary<string, object> SelectedParameters { get; private set; } // 사용자가 선택한 최종 파라미터값
        #endregion

        #region Constructor
        public FormParameters(string inputkey, string name, string outputkey, IDictionary<string, object> currentParameters = null)
        {
            InitializeComponent();
            _currentParameters = currentParameters ?? new Dictionary<string, object>();

            LogParameterInfo(name); // 파라미터 정보 로깅
            LoadInputOutputImages(inputkey, outputkey); // 입출력 이미지 로드
            ShowPreprocessParameterControl(name); // 파라미터 컨트롤 생성
            SetupPictureBoxEvents(); // PictureBox 마우스 이벤트 설정
            SetupFormEvents(); // Form 이벤트 설정
        }
        #endregion

        #region Form Event Setup
        /// <summary>
        /// Form 레벨 이벤트들을 설정합니다.
        /// </summary>
        private void SetupFormEvents()
        {
            this.Load += FormParameters_Load; // Form 로드 이벤트
            this.Shown += FormParameters_Shown; // Form 표시 이벤트
        }

        /// <summary>
        /// Form이 로드될 때 호출됩니다.
        /// </summary>
        private void FormParameters_Load(object sender, EventArgs e)
        {
            Console.WriteLine("=== FormParameters_Load 이벤트 ===");
        }

        /// <summary>
        /// Form이 화면에 표시된 후 호출됩니다. 파라미터 로딩에 가장 안전한 시점입니다.
        /// </summary>
        private void FormParameters_Shown(object sender, EventArgs e)
        {
            Console.WriteLine("=== FormParameters_Shown 이벤트 ===");

            var timer = new Timer(); // 약간의 지연을 위한 타이머
            timer.Interval = 100; // 100ms 후 실행
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                timer.Dispose();
                LoadCurrentParametersToUI(); // 파라미터를 UI에 로드
            };
            timer.Start();
        }
        #endregion

        #region PictureBox Events Setup
        /// <summary>
        /// PictureBox의 마우스 관련 이벤트들을 설정합니다.
        /// </summary>
        private void SetupPictureBoxEvents()
        {
            try
            {
                pictureBox_main.MouseMove += PictureBox_main_MouseMove; // 마우스 움직임 - 실시간 좌표/픽셀값 표시
                pictureBox_main.MouseEnter += PictureBox_main_MouseEnter; // 마우스 진입 - 커서 변경
                pictureBox_main.MouseLeave += PictureBox_main_MouseLeave; // 마우스 이탈 - 정보 클리어
                pictureBox_main.MouseClick += PictureBox_main_MouseClick; // 마우스 클릭 - 정보 고정

                Console.WriteLine("✅ PictureBox 이벤트 등록 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ PictureBox 이벤트 등록 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 마우스가 PictureBox 위에서 움직일 때 실시간으로 좌표와 픽셀값을 업데이트합니다.
        /// </summary>
        private void PictureBox_main_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                UpdateCoordinateAndPixelInfo(e.Location, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ MouseMove 이벤트 처리 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 마우스 클릭 시 해당 위치의 좌표와 픽셀값을 고정 표시합니다.
        /// </summary>
        private void PictureBox_main_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                UpdateCoordinateAndPixelInfo(e.Location, true);
                Console.WriteLine($"픽셀 정보 고정: 좌표({e.X}, {e.Y})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ MouseClick 이벤트 처리 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 마우스가 PictureBox에 진입할 때 커서를 십자선으로 변경합니다.
        /// </summary>
        private void PictureBox_main_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_main.Cursor = Cursors.Cross; // 십자선 커서로 변경
        }

        /// <summary>
        /// 마우스가 PictureBox에서 나갈 때 커서를 원래대로 하고 정보를 클리어합니다.
        /// </summary>
        private void PictureBox_main_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_main.Cursor = Cursors.Default; // 기본 커서로 복원
            lbl_ordinate.Text = "X: -, Y: -"; // 좌표 정보 클리어
            lbl_PixelValue.Text = "R: -, G: -, B: -"; // 픽셀값 정보 클리어
        }
        #endregion

        #region Coordinate and Pixel Info Methods
        /// <summary>
        /// 마우스 위치에 해당하는 이미지 좌표와 픽셀값을 업데이트합니다.
        /// </summary>
        /// <param name="mouseLocation">PictureBox 상의 마우스 좌표</param>
        /// <param name="isClicked">클릭 여부 (디버그 로그용)</param>
        private void UpdateCoordinateAndPixelInfo(System.Drawing.Point mouseLocation, bool isClicked)
        {
            try
            {
                if (pictureBox_main.Image == null)
                {
                    SetEmptyCoordinateInfo(); // 이미지가 없으면 빈 정보 표시
                    return;
                }

                var imageCoordinates = ConvertPictureBoxToImageCoordinates(mouseLocation); // PictureBox 좌표를 이미지 좌표로 변환

                if (imageCoordinates.HasValue)
                {
                    var imgX = imageCoordinates.Value.X;
                    var imgY = imageCoordinates.Value.Y;

                    lbl_ordinate.Text = $"X: {imgX}, Y: {imgY}"; // 좌표 표시
                    lbl_PixelValue.Text = GetPixelValueFormatted(imgX, imgY); // 픽셀값 표시

                    if (isClicked)
                    {
                        Console.WriteLine($"클릭 위치 - PictureBox:({mouseLocation.X}, {mouseLocation.Y}), 이미지:({imgX}, {imgY})");
                    }
                }
                else
                {
                    SetEmptyCoordinateInfo(); // 이미지 영역 밖이면 빈 정보 표시
                }
            }
            catch (Exception ex)
            {
                SetEmptyCoordinateInfo();
                Console.WriteLine($"❌ 좌표/픽셀값 업데이트 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 좌표와 픽셀값 정보를 빈 상태로 설정합니다.
        /// </summary>
        private void SetEmptyCoordinateInfo()
        {
            lbl_ordinate.Text = "X: -, Y: -";
            lbl_PixelValue.Text = "R: -, G: -, B: -";
        }

        /// <summary>
        /// 지정된 이미지 좌표의 픽셀값을 지정된 형식으로 반환합니다.
        /// </summary>
        /// <param name="x">이미지 X 좌표</param>
        /// <param name="y">이미지 Y 좌표</param>
        /// <returns>"R: 값, G: 값, B: 값" 형식의 문자열</returns>
        private string GetPixelValueFormatted(int x, int y)
        {
            try
            {
                // 방법 1: Bitmap에서 직접 가져오기
                if (pictureBox_main.Image is Bitmap bitmap)
                {
                    if (IsValidCoordinate(x, y, bitmap.Width, bitmap.Height))
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        return $"R: {pixel.R}, G: {pixel.G}, B: {pixel.B}";
                    }
                }

                // 방법 2: OpenCV Mat에서 가져오기 (더 정확함)
                if (_originalMat != null && !_originalMat.IsDisposed)
                {
                    if (IsValidCoordinate(x, y, _originalMat.Width, _originalMat.Height))
                    {
                        return GetMatPixelValueFormatted(_originalMat, x, y);
                    }
                }

                return "R: -, G: -, B: -";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 픽셀값 가져오기 실패: {ex.Message}");
                return "R: -, G: -, B: -";
            }
        }

        /// <summary>
        /// OpenCV Mat에서 픽셀값을 추출하여 RGB 형식으로 반환합니다.
        /// </summary>
        /// <param name="mat">OpenCV Mat 객체</param>
        /// <param name="x">X 좌표</param>
        /// <param name="y">Y 좌표</param>
        /// <returns>"R: 값, G: 값, B: 값" 형식의 문자열</returns>
        private string GetMatPixelValueFormatted(Mat mat, int x, int y)
        {
            try
            {
                if (mat.Channels() == 1)
                {
                    byte grayValue = mat.At<byte>(y, x); // 그레이스케일
                    return $"R: {grayValue}, G: {grayValue}, B: {grayValue}";
                }
                else if (mat.Channels() == 3)
                {
                    var bgr = mat.At<OpenCvSharp.Vec3b>(y, x); // BGR 컬러
                    return $"R: {bgr.Item2}, G: {bgr.Item1}, B: {bgr.Item0}"; // BGR → RGB 순서 변경
                }
                else if (mat.Channels() == 4)
                {
                    var bgra = mat.At<OpenCvSharp.Vec4b>(y, x); // BGRA 컬러
                    return $"R: {bgra.Item2}, G: {bgra.Item1}, B: {bgra.Item0}"; // Alpha 무시, BGR → RGB
                }
                else
                {
                    return "R: -, G: -, B: -";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Mat 픽셀값 가져오기 실패: {ex.Message}");
                return "R: -, G: -, B: -";
            }
        }

        /// <summary>
        /// PictureBox 좌표를 실제 이미지 좌표로 변환합니다.
        /// </summary>
        /// <param name="pictureBoxLocation">PictureBox 상의 좌표</param>
        /// <returns>이미지 상의 좌표 (유효하지 않으면 null)</returns>
        private System.Drawing.Point? ConvertPictureBoxToImageCoordinates(System.Drawing.Point pictureBoxLocation)
        {
            try
            {
                if (pictureBox_main.Image == null) return null;

                var pictureBoxSize = pictureBox_main.ClientSize;
                var imageSize = pictureBox_main.Image.Size;

                float scaleX, scaleY;
                int offsetX = 0, offsetY = 0;

                // PictureBox SizeMode에 따른 좌표 변환
                switch (pictureBox_main.SizeMode)
                {
                    case PictureBoxSizeMode.Zoom:
                        CalculateZoomModeTransform(pictureBoxSize, imageSize, out scaleX, out scaleY, out offsetX, out offsetY);
                        break;

                    case PictureBoxSizeMode.StretchImage:
                        scaleX = (float)imageSize.Width / pictureBoxSize.Width; // 비율 무시하고 늘림
                        scaleY = (float)imageSize.Height / pictureBoxSize.Height;
                        break;

                    case PictureBoxSizeMode.Normal:
                    case PictureBoxSizeMode.AutoSize:
                    default:
                        scaleX = 1; // 1:1 매핑
                        scaleY = 1;
                        break;
                }

                return CalculateFinalImageCoordinates(pictureBoxLocation, scaleX, scaleY, offsetX, offsetY, imageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 좌표 변환 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Zoom 모드에서의 스케일과 오프셋을 계산합니다.
        /// </summary>
        private void CalculateZoomModeTransform(System.Drawing.Size pictureBoxSize, System.Drawing.Size imageSize, out float scaleX, out float scaleY, out int offsetX, out int offsetY)
        {
            float scaleWidth = (float)pictureBoxSize.Width / imageSize.Width;
            float scaleHeight = (float)pictureBoxSize.Height / imageSize.Height;
            float scale = Math.Min(scaleWidth, scaleHeight); // 비율 유지

            int scaledWidth = (int)(imageSize.Width * scale);
            int scaledHeight = (int)(imageSize.Height * scale);

            offsetX = (pictureBoxSize.Width - scaledWidth) / 2; // 중앙 정렬을 위한 오프셋
            offsetY = (pictureBoxSize.Height - scaledHeight) / 2;

            scaleX = (float)imageSize.Width / scaledWidth;
            scaleY = (float)imageSize.Height / scaledHeight;
        }

        /// <summary>
        /// 최종 이미지 좌표를 계산하고 유효성을 검사합니다.
        /// </summary>
        private System.Drawing.Point? CalculateFinalImageCoordinates(System.Drawing.Point pictureBoxLocation, float scaleX, float scaleY, int offsetX, int offsetY, System.Drawing.Size imageSize)
        {
            int adjustedX = pictureBoxLocation.X - offsetX; // 오프셋 적용
            int adjustedY = pictureBoxLocation.Y - offsetY;

            if (adjustedX < 0 || adjustedY < 0) return null; // 이미지 영역 밖

            int imageX = (int)(adjustedX * scaleX); // 스케일 적용
            int imageY = (int)(adjustedY * scaleY);

            // 이미지 경계 검사
            if (!IsValidCoordinate(imageX, imageY, imageSize.Width, imageSize.Height))
                return null;

            return new System.Drawing.Point(imageX, imageY);
        }

        /// <summary>
        /// 좌표가 유효한 범위 내에 있는지 확인합니다.
        /// </summary>
        private bool IsValidCoordinate(int x, int y, int width, int height)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }
        #endregion

        #region Parameter Loading Methods
        /// <summary>
        /// 파라미터 정보를 콘솔에 로깅합니다.
        /// </summary>
        private void LogParameterInfo(string name)
        {
            Console.WriteLine("=== FormParameters 생성자 시작 ===");
            Console.WriteLine($"노드명: {name}");
            Console.WriteLine($"현재 파라미터 개수: {_currentParameters.Count}");
            foreach (var param in _currentParameters)
            {
                Console.WriteLine($"  파라미터: {param.Key} = {param.Value} ({param.Value?.GetType().Name ?? "null"})");
            }
        }

        /// <summary>
        /// 현재 파라미터값들을 UI 컨트롤에 로드합니다.
        /// </summary>
        private void LoadCurrentParametersToUI()
        {
            try
            {
                Console.WriteLine("=== LoadCurrentParametersToUI 시작 ===");

                if (_currentParameters == null || _currentParameters.Count == 0)
                {
                    Console.WriteLine("현재 파라미터가 없음 - 기본값 사용");
                    return;
                }

                if (_currentParameterControl == null)
                {
                    Console.WriteLine("❌ _currentParameterControl이 null입니다!");
                    return;
                }

                Console.WriteLine($"✅ 파라미터 컨트롤 타입: {_currentParameterControl.GetType().Name}");
                SetParametersToUserControl(); // 실제 파라미터 설정
                Console.WriteLine("=== LoadCurrentParametersToUI 완료 ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 파라미터 UI 로드 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 파라미터를 해당하는 UserControl에 설정합니다.
        /// </summary>
        private void SetParametersToUserControl()
        {
            if (_currentParameterControl == null) return;

            try
            {
                Console.WriteLine($"=== SetParametersToUserControl 시작 ===");

                var parameterDict = _currentParameters.ToDictionary(x => x.Key, x => x.Value);

                // 각 컨트롤 타입별로 SetCurrentParameters 호출
                if (_currentParameterControl is GaussianBlurParameterControl gaussianControl)
                {
                    Console.WriteLine("✅ GaussianBlur 컨트롤에 현재 파라미터 설정");
                    gaussianControl.SetCurrentParameters(parameterDict);
                }
                else if (_currentParameterControl is CLAHEParameterControl claheControl)
                {
                    Console.WriteLine("✅ CLAHE 컨트롤에 현재 파라미터 설정");
                    claheControl.SetCurrentParameters(parameterDict);
                }
                else if (_currentParameterControl is EdgeParameterControl edgeControl)
                {
                    Console.WriteLine("✅ Edge 컨트롤에 현재 파라미터 설정");
                    edgeControl.SetCurrentParameters(parameterDict);
                }
                else
                {
                    TrySetParametersUsingReflection(parameterDict); // Reflection 사용
                }

                Console.WriteLine("=== SetParametersToUserControl 완료 ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ UserControl 파라미터 설정 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// Reflection을 사용하여 SetCurrentParameters 메서드를 호출합니다.
        /// </summary>
        private void TrySetParametersUsingReflection(Dictionary<string, object> parameterDict)
        {
            Console.WriteLine($"❌ 알 수 없는 파라미터 컨트롤 타입: {_currentParameterControl.GetType().Name}");

            var method = _currentParameterControl.GetType().GetMethod("SetCurrentParameters");
            if (method != null)
            {
                Console.WriteLine("✅ Reflection으로 SetCurrentParameters 메서드 발견, 호출 시도");
                method.Invoke(_currentParameterControl, new object[] { parameterDict });
            }
            else
            {
                Console.WriteLine("❌ SetCurrentParameters 메서드를 찾을 수 없습니다.");
            }
        }
        #endregion

        #region Image Loading Methods
        /// <summary>
        /// 입력 및 출력 이미지를 로드하여 PictureBox에 표시합니다.
        /// </summary>
        private void LoadInputOutputImages(string inputkey, string outputkey)
        {
            _originalMat?.Dispose(); // 기존 Mat 해제
            _originalMat = null;

            LoadInputImage(inputkey); // 입력 이미지 로드
            LoadOutputImage(outputkey); // 출력 이미지 로드
        }

        /// <summary>
        /// 입력 이미지를 로드합니다.
        /// </summary>
        private void LoadInputImage(string inputkey)
        {
            if (string.IsNullOrWhiteSpace(inputkey)) return;

            var inputImage = IFVisionEngine.Manager.ImageDataManager.GetImage(inputkey);
            if (inputImage != null)
            {
                pictureBox_input_1.Image?.Dispose(); // 기존 이미지 해제
                pictureBox_input_1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(inputImage);
                _originalMat = inputImage.Clone(); // 원본 Mat 저장
            }
        }

        /// <summary>
        /// 출력 이미지를 로드합니다.
        /// </summary>
        private void LoadOutputImage(string outputkey)
        {
            if (string.IsNullOrWhiteSpace(outputkey)) return;

            var outputImage = IFVisionEngine.Manager.ImageDataManager.GetImage(outputkey);
            if (outputImage != null)
            {
                pictureBox_output_1.Image?.Dispose(); // 기존 이미지 해제
                pictureBox_output_1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(outputImage);
            }
        }
        #endregion

        #region Parameter Control Management
        /// <summary>
        /// 노드 이름에 따라 적절한 파라미터 컨트롤을 생성하고 설정합니다.
        /// </summary>
        private void ShowPreprocessParameterControl(string preprocessName)
        {
            UserControl control = CreateParameterControl(preprocessName); // 컨트롤 생성
            SetupParameterControlEvents(control); // 이벤트 연결
            AddControlToPanel(control); // 패널에 추가
            UserControl control2 = CreateDescriptionControl(preprocessName); // 컨트롤 생성
            AddDescriptionControlToPanel(control2); // 패널에 추가
        }

        /// <summary>
        /// 노드 이름에 따라 적절한 파라미터 컨트롤을 생성합니다.
        /// </summary>
        private UserControl CreateParameterControl(string preprocessName)
        {
            Console.WriteLine($"파라미터 컨트롤 생성: {preprocessName}");

            switch (preprocessName?.ToLower())
            {
                case "claheparameters":
                    return new CLAHEParameterControl();
                case "gaussianblurparameters":
                    Console.WriteLine("GaussianBlurParameterControl 생성됨");
                    return new GaussianBlurParameterControl();
                case "edgeparameters":
                    return new EdgeParameterControl();
                default:
                    Console.WriteLine($"알 수 없는 전처리 타입: {preprocessName}");
                    return null;
            }
        }
        /// <summary>
        /// 노드 이름에 따라 적절한 파라미터 설명 컨트롤을 생성합니다.
        /// </summary>
        private UserControl CreateDescriptionControl(string preprocessName)
        {
            Console.WriteLine($"파라미터 설명 컨트롤 생성: {preprocessName}");
            switch (preprocessName?.ToLower())
            {
                case "claheparameters":
                    return new CLAHEParameterDescription();
                case "gaussianblurparameters":
                    return new GaussianBlurParameterDescription();
                case "edgeparameters":
                    return new EdgeParameterDescription();
                default:
                    Console.WriteLine($"알 수 없는 전처리 타입(설명): {preprocessName}");
                    return null;
            }
        }
        /// <summary>
        /// 파라미터 컨트롤의 이벤트들을 연결합니다.
        /// </summary>
        private void SetupParameterControlEvents(UserControl control)
        {
            if (control is CLAHEParameterControl clahe)
            {
                clahe.OnParametersChanged += ClaheParameterChanged; // CLAHE 파라미터 변경 이벤트
                Console.WriteLine("CLAHE 이벤트 연결됨");
            }
            else if (control is GaussianBlurParameterControl gb)
            {
                gb.OnParametersChanged += GaussianBlurParameterChanged; // GaussianBlur 파라미터 변경 이벤트
                Console.WriteLine("GaussianBlur 이벤트 연결됨");
            }
            else if (control is EdgeParameterControl edge)
            {
                edge.OnParametersChanged += EdgeParameterChanged; // Edge 파라미터 변경 이벤트
                Console.WriteLine("Edge 이벤트 연결됨");
            }
        }

        /// <summary>
        /// 컨트롤을 TableLayoutPanel에 추가합니다.
        /// </summary>
        private void AddControlToPanel(UserControl control)
        {
            // 기존 컨트롤 제거
            if (tableLayoutPanel7.Controls.Count > 1)
                tableLayoutPanel7.Controls.RemoveAt(1);

            if (control != null)
            {
                tableLayoutPanel7.Controls.Add(control, 0, 1);
                control.Dock = DockStyle.Fill;

                _currentParameterControl = control as IPreprocessParameterControl;
                if (_currentParameterControl != null)
                {
                    _currentParameterControl.OnParametersChangedBase += OnAnyParameterChanged; // 공통 이벤트 연결
                    Console.WriteLine("파라미터 컨트롤 설정 완료");
                }
            }
            else
            {
                _currentParameterControl = null;
                Console.WriteLine("파라미터 컨트롤이 null입니다");
            }
        }
        /// <summary>
        /// 설명 컨트롤을 TableLayoutPanel8의 두 번째 Row(1, 0)에 추가합니다.
        /// </summary>
        private void AddDescriptionControlToPanel(UserControl control)
        {
            // 기존 설명 컨트롤 제거 (2번째 Row만 제거)
            if (tableLayoutPanel8.Controls.Count > 1)
                tableLayoutPanel8.Controls.RemoveAt(1);

            if (control != null)
            {
                tableLayoutPanel8.Controls.Add(control, 0, 1); // (Column 0, Row 1)
                control.Dock = DockStyle.Fill;
                Console.WriteLine("설명 컨트롤 설정 완료");
            }
            else
            {
                Console.WriteLine("설명 컨트롤이 null입니다");
            }
        }
        #endregion

        #region Image Processing Event Handlers
        /// <summary>
        /// CLAHE 파라미터가 변경될 때 실시간으로 이미지에 적용합니다.
        /// </summary>
        private void ClaheParameterChanged(double clipLimit, int tileHeight, int tileWidth)
        {
            if (_originalMat == null) return;

            try
            {
                var clahe = Cv2.CreateCLAHE(clipLimit, new OpenCvSharp.Size(tileWidth, tileHeight));
                var dst = new Mat();
                clahe.Apply(_originalMat, dst);
                SetPictureBoxImage(pictureBox_main, OpenCvSharp.Extensions.BitmapConverter.ToBitmap(dst));
                dst.Dispose(); // 메모리 해제
            }
            catch (Exception ex)
            {
                MessageBox.Show("CLAHE 적용 실패: " + ex.Message);
            }
        }

        /// <summary>
        /// Gaussian Blur 파라미터가 변경될 때 실시간으로 이미지에 적용합니다.
        /// </summary>
        private void GaussianBlurParameterChanged(int kernelSize)
        {
            if (_originalMat == null) return;

            try
            {
                var dst = new Mat();
                Cv2.GaussianBlur(_originalMat, dst, new OpenCvSharp.Size(kernelSize, kernelSize), 0);
                SetPictureBoxImage(pictureBox_main, OpenCvSharp.Extensions.BitmapConverter.ToBitmap(dst));
                dst.Dispose(); // 메모리 해제
            }
            catch (Exception ex)
            {
                MessageBox.Show("GaussianBlur 적용 실패: " + ex.Message);
            }
        }

        /// <summary>
        /// Edge Detection 파라미터가 변경될 때 실시간으로 이미지에 적용합니다.
        /// </summary>
        private void EdgeParameterChanged(double threshold1, double threshold2,string method)
        {
            if (_originalMat == null) return;

            try
            {

                var dst = new Mat();
                if (method == EdgeDetectionParameters.EdgeMethod.Canny.ToString())
                {
                    Cv2.Canny(_originalMat, dst, threshold1, threshold2);
                }
                else if (method == EdgeDetectionParameters.EdgeMethod.Sobel.ToString())
                {
                    int ksize = 3; // 커널크기 고정
                    var gradX = new Mat();
                    var gradY = new Mat();
                    Cv2.Sobel(_originalMat, gradX, MatType.CV_16S, 1, 0, ksize);
                    Cv2.Sobel(_originalMat, gradY, MatType.CV_16S, 0, 1, ksize);
                    Cv2.ConvertScaleAbs(gradX, gradX);
                    Cv2.ConvertScaleAbs(gradY, gradY);
                    Cv2.AddWeighted(gradX, 0.5, gradY, 0.5, 0, dst);
                    gradX.Dispose(); gradY.Dispose();
                }
                else if (method == EdgeDetectionParameters.EdgeMethod.Laplacian.ToString())
                {
                    int ksize = 3; // 커널크기 고정
                    Cv2.Laplacian(_originalMat, dst, MatType.CV_16S, ksize);
                    Cv2.ConvertScaleAbs(dst, dst);
                }
                SetPictureBoxImage(pictureBox_main, OpenCvSharp.Extensions.BitmapConverter.ToBitmap(dst));
                dst.Dispose(); // 메모리 해제
            }
            catch (Exception ex)
            {
                MessageBox.Show("Edge 적용 실패: " + ex.Message);
            }
        }

        /// <summary>
        /// 모든 파라미터 컨트롤에서 공통으로 호출되는 이벤트 핸들러입니다.
        /// </summary>
        private void OnAnyParameterChanged()
        {
            // 필요시 공통 처리 로직 추가
        }
        #endregion

        #region UI Event Handlers
        /// <summary>
        /// 입력 이미지 클릭 시 메인 PictureBox에 표시하고 파라미터를 기본값으로 리셋합니다.
        /// </summary>
        private void pictureBox_input_1_Click(object sender, EventArgs e)
        {
            _currentParameterControl?.ResetParametersToDefault(); // 파라미터 기본값으로 리셋
            SetPictureBoxImage(pictureBox_main, pictureBox_input_1.Image); // 입력 이미지를 메인에 표시
        }

        /// <summary>
        /// 출력 이미지 클릭 시 메인 PictureBox에 표시하고 파라미터를 기본값으로 리셋합니다.
        /// </summary>
        private void pictureBox_output_1_Click(object sender, EventArgs e)
        {
            _currentParameterControl?.ResetParametersToDefault(); // 파라미터 기본값으로 리셋
            SetPictureBoxImage(pictureBox_main, pictureBox_output_1.Image); // 출력 이미지를 메인에 표시
        }

        /// <summary>
        /// OK 버튼 클릭 시 현재 설정된 파라미터를 가져와서 다이얼로그를 닫습니다.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            SelectedParameters = _currentParameterControl?.GetParameters(); // 현재 파라미터값 추출
            CleanupResources(); // 리소스 정리
            this.DialogResult = DialogResult.OK; // 다이얼로그 결과 설정
            this.Close(); // 다이얼로그 닫기
        }

        /// <summary>
        /// Form이 닫힐 때 리소스를 정리합니다.
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            CleanupResources(); // 리소스 정리
            base.OnFormClosed(e);
        }
        #endregion

        #region Utility Methods
        /// <summary>
        /// PictureBox의 이미지를 안전하게 교체합니다. 기존 이미지는 메모리에서 해제됩니다.
        /// </summary>
        /// <param name="target">대상 PictureBox</param>
        /// <param name="src">새로 설정할 이미지</param>
        private void SetPictureBoxImage(PictureBox target, Image src)
        {
            target.Image?.Dispose(); // 기존 이미지 메모리 해제
            target.Image = null;

            if (src != null)
                target.Image = (Image)src.Clone(); // 새 이미지 복사본 설정
        }

        /// <summary>
        /// 폼이 닫힐 때 수동으로 리소스를 정리합니다.
        /// </summary>
        private void CleanupResources()
        {
            try
            {
                _originalMat?.Dispose(); // OpenCV Mat 리소스 해제

                pictureBox_input_1.Image?.Dispose(); // PictureBox 이미지 리소스 해제
                pictureBox_output_1.Image?.Dispose();
                pictureBox_main.Image?.Dispose();

                Console.WriteLine("✅ FormParameters 리소스 정리 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 리소스 정리 중 오류: {ex.Message}");
            }
        }
        #endregion
    }
}