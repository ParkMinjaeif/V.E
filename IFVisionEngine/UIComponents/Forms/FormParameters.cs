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
using System.Text;
using System.Web.UI.WebControls;
using IFVisionEngine.UIComponents.Data;
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
        private double _centroidX = 0;
        private double _centroidY = 0;
        private bool HasCentroidData = false;
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
                    return new GaussianBlurParameterControl();
                case "edgeparameters":
                    return new EdgeParameterControl();
                case "contourparameters":
                    return new ContourParameterControl();
                case "momentsparameters":
                    return new MomentsParameterControl();
                case "radiallinesparameters":
                    return new RadialLinesParameterControl();
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
                case "contourparameters":
                    return new ContourParameterDescription();
                case "momentsparameters":
                    return new MomentsParameterDescription();
                case "radiallinesparameters":
                    return new RadialLinesParameterDescription();

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
            else if (control is ContourParameterControl contour)
            {
                contour.OnParametersChanged += ContourParameterChanged;
                Console.WriteLine("Contour 이벤트 연결됨");
            }
            else if (control is MomentsParameterControl moments)
            {
                moments.OnParametersChanged += MomentsParameterChanged; // Moments 파라미터 변경 이벤트
                Console.WriteLine("Moments 이벤트 연결됨");
            }
            else if (control is RadialLinesParameterControl radialLines)
            {
                radialLines.OnParametersChanged += RadialLinesParameterChanged; // RadialLines 파라미터 변경 이벤트
                Console.WriteLine("RadialLines 이벤트 연결됨");
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
                else if (_currentParameterControl is ContourParameterControl contourControl)
                {
                    Console.WriteLine("✅ Contour 컨트롤에 현재 파라미터 설정");
                    contourControl.SetCurrentParameters(parameterDict);
                }
                else if (_currentParameterControl is MomentsParameterControl momentsControl)
                {
                    Console.WriteLine("✅ Moments 컨트롤에 현재 파라미터 설정");
                    momentsControl.SetCurrentParameters(parameterDict);
                }
                else if (_currentParameterControl is RadialLinesParameterControl radialLinesControl)
                {
                    Console.WriteLine("✅ RadialLines 컨트롤에 현재 파라미터 설정");

                    // === 무게중심 데이터 처리 로직 개선 ===
                    if (parameterDict.ContainsKey("moments"))
                    {
                        var moments = parameterDict["moments"];
                        Console.WriteLine($"[디버그] moments 값: {moments}");

                        if (moments != null)
                        {
                            bool centroidExtracted = false;

                            // moments 객체에서 무게중심 좌표 추출
                            if (moments is Dictionary<string, object> momentsDict)
                            {
                                Console.WriteLine("[디버그] moments는 Dictionary 타입");
                                foreach (var m in momentsDict)
                                    Console.WriteLine($"  - {m.Key} : {m.Value}");

                                if (momentsDict.ContainsKey("CentroidX") && momentsDict.ContainsKey("CentroidY"))
                                {
                                    Console.WriteLine($"[디버그] CentroidX, CentroidY 찾음: {momentsDict["CentroidX"]}, {momentsDict["CentroidY"]}");
                                    if (double.TryParse(momentsDict["CentroidX"].ToString(), out double centroidX) &&
                                        double.TryParse(momentsDict["CentroidY"].ToString(), out double centroidY))
                                    {
                                        _centroidX = centroidX;
                                        _centroidY = centroidY;
                                        HasCentroidData = true;
                                        centroidExtracted = true;
                                        Console.WriteLine($"[디버그] 무게중심 파싱 성공: X={_centroidX}, Y={_centroidY}");
                                    }
                                }

                                // m10/m00, m01/m00 형태로 저장된 경우
                                if (!centroidExtracted && momentsDict.ContainsKey("m10") && momentsDict.ContainsKey("m01") && momentsDict.ContainsKey("m00"))
                                {
                                    if (double.TryParse(momentsDict["m10"].ToString(), out double m10) &&
                                        double.TryParse(momentsDict["m01"].ToString(), out double m01) &&
                                        double.TryParse(momentsDict["m00"].ToString(), out double m00) && m00 != 0)
                                    {
                                        _centroidX = m10 / m00;
                                        _centroidY = m01 / m00;
                                        HasCentroidData = true;
                                        centroidExtracted = true;
                                        Console.WriteLine($"[디버그] moments 계산으로 무게중심 파싱 성공: X={_centroidX}, Y={_centroidY}");
                                    }
                                }
                            }
                            // 또는 직접 좌표가 전달되는 경우
                            else if (!centroidExtracted && moments.ToString().Contains(","))
                            {
                                Console.WriteLine("[디버그] moments 문자열 좌표 형태: " + moments.ToString());
                                string[] coords = moments.ToString().Split(',');
                                if (coords.Length >= 2)
                                {
                                    if (double.TryParse(coords[0].Trim(), out double centroidX) &&
                                        double.TryParse(coords[1].Trim(), out double centroidY))
                                    {
                                        _centroidX = centroidX;
                                        _centroidY = centroidY;
                                        HasCentroidData = true;
                                        centroidExtracted = true;
                                        Console.WriteLine($"[디버그] 무게중심 파싱 성공: X={_centroidX}, Y={_centroidY}");
                                    }
                                }
                            }

                            if (!centroidExtracted)
                            {
                                Console.WriteLine("[디버그] moments 타입 미지원 또는 파싱 실패: " + moments.GetType().Name);
                            }
                        }
                        else
                        {
                            Console.WriteLine("[디버그] moments가 null임");
                        }
                    }
                    else
                    {
                        Console.WriteLine("[디버그] parameterDict에 'moments' 키 없음");
                    }

                    // === 무게중심 데이터를 파라미터 딕셔너리에 명시적으로 추가 ===
                    if (HasCentroidData)
                    {
                        parameterDict["CentroidX"] = _centroidX;
                        parameterDict["CentroidY"] = _centroidY;
                        parameterDict["HasCentroidData"] = true;

                        Console.WriteLine($"[디버그] 무게중심 데이터를 파라미터에 추가: CentroidX={_centroidX}, CentroidY={_centroidY}");
                    }
                    else
                    {
                        parameterDict["HasCentroidData"] = false;
                        Console.WriteLine("[디버그] 무게중심 데이터 없음으로 설정");
                    }

                    // 컨트롤에 파라미터 설정
                    radialLinesControl.SetCurrentParameters(parameterDict);
                }
                else
                {
                    Console.WriteLine("[디버그] 알 수 없는 컨트롤 타입. Reflection 시도");
                    TrySetParametersUsingReflection(parameterDict); // Reflection 사용
                }

                Console.WriteLine($"=== SetParametersToUserControl 완료 (HasCentroidData={HasCentroidData}, _centroidX={_centroidX}, _centroidY={_centroidY}) ===");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ UserControl 파라미터 설정 실패: {ex.Message}");
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
        private void EdgeParameterChanged(double threshold1, double threshold2, string method)
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
        /// RadialLines 파라미터가 변경될 때 실시간으로 이미지에 적용합니다.
        /// </summary>
        private void RadialLinesParameterChanged(RadialLinesParameter parameters)
        {
            if (_originalMat == null) return;

            try
            {
                using (Mat binaryImage = new Mat())
                using (Mat outputImage = new Mat())
                {
                    // 1. 이진화 준비
                    PrepareBinaryImageForPreview(_originalMat, binaryImage, parameters.BinaryThreshold);

                    // 2. 출력 이미지 준비
                    if (parameters.ShowVisualization)
                    {
                        if (_originalMat.Channels() >= 3)
                        {
                            _originalMat.CopyTo(outputImage);
                        }
                        else
                        {
                            Cv2.CvtColor(_originalMat, outputImage, ColorConversionCodes.GRAY2BGR);
                        }
                    }
                    else
                    {
                        if (_originalMat.Channels() >= 3)
                        {
                            _originalMat.CopyTo(outputImage);
                        }
                        else
                        {
                            Cv2.CvtColor(_originalMat, outputImage, ColorConversionCodes.GRAY2BGR);
                        }
                    }

                    // 3. 중심점 계산
                    var centers = GetRadialCentersForPreview(_originalMat, parameters.CenterMethod,
                                                           parameters.ManualX, parameters.ManualY,
                                                           parameters.BinaryThreshold);

                    if (centers.Count == 0)
                    {
                        // 중심점을 찾을 수 없을 때
                        Cv2.PutText(outputImage, "No center point found",
                                   new OpenCvSharp.Point(10, 30), HersheyFonts.HersheySimplex, 0.7, new Scalar(0, 0, 255), 2);
                        SetPictureBoxImage(pictureBox_main, OpenCvSharp.Extensions.BitmapConverter.ToBitmap(outputImage));
                        return;
                    }

                    // 4. 각 중심점에 대해 방사선 그리기
                    var allLengthData = new List<string>();

                    for (int centerIndex = 0; centerIndex < centers.Count; centerIndex++)
                    {
                        OpenCvSharp.Point center = centers[centerIndex];
                        var lengths = new List<double>();

                        // 방사선 그리기 및 길이 측정
                        for (int i = 0; i < parameters.LineCount; i++)
                        {
                            double angle = parameters.StartAngle + (i * 360.0 / parameters.LineCount);

                            // 끝점 계산
                            OpenCvSharp.Point endPoint = CalculateRadialEndPointForPreview(_originalMat, binaryImage, center,
                                                                                           angle, parameters.RangeMethod,
                                                                                           parameters.FixedLength,
                                                                                           parameters.BrightnessThreshold);

                            // 길이 계산
                            double length = Math.Sqrt(Math.Pow(endPoint.X - center.X, 2) + Math.Pow(endPoint.Y - center.Y, 2));
                            lengths.Add(length);

                            // 시각화
                            if (parameters.ShowVisualization)
                            {
                                DrawRadialLineForPreview(outputImage, center, endPoint, angle, length,
                                                       parameters.LineColor, parameters.LineThickness,
                                                       parameters.Style, parameters.ShowAngles,
                                                       parameters.ShowDistances);
                            }
                        }

                        // 중심점 표시
                        if (parameters.ShowVisualization && parameters.ShowCenter)
                        {
                            DrawCenterPointForPreview(outputImage, center, centerIndex,
                                                    parameters.LineColor, parameters.LineThickness);
                        }

                        // 길이 데이터 저장
                        allLengthData.Add($"Center{centerIndex}:" + string.Join(",", lengths.Select(l => l.ToString("F2"))));
                    }

                    // 5. 시각화 꺼져있을 때 정보 표시
                    if (!parameters.ShowVisualization)
                    {
                        Cv2.PutText(outputImage, $"RadialLines: {centers.Count} centers, {parameters.LineCount} lines each",
                                   new OpenCvSharp.Point(10, 30), HersheyFonts.HersheySimplex, 0.7, new Scalar(0, 255, 0), 2);
                        Cv2.PutText(outputImage, "(Visualization OFF)",
                                   new OpenCvSharp.Point(10, 55), HersheyFonts.HersheySimplex, 0.6, new Scalar(0, 255, 0), 2);
                    }

                    // 6. 추가 정보 표시
                    if (parameters.OutputLengthData)
                    {
                        string lengthDataInfo = $"Length Data: {allLengthData.Count} sets";
                        Cv2.PutText(outputImage, lengthDataInfo,
                                   new OpenCvSharp.Point(10, outputImage.Height - 20),
                                   HersheyFonts.HersheySimplex, 0.5, new Scalar(255, 255, 0), 1);
                    }

                    // 7. 결과 이미지 표시
                    SetPictureBoxImage(pictureBox_main, OpenCvSharp.Extensions.BitmapConverter.ToBitmap(outputImage));

                    // 8. 콘솔에 미리보기 데이터 출력
                    if (parameters.OutputLengthData && allLengthData.Count > 0)
                    {
                        Console.WriteLine("=== RadialLines Length Data Preview ===");
                        foreach (string data in allLengthData.Take(3)) // 최대 3개만 미리보기
                        {
                            Console.WriteLine(data);
                        }
                        if (allLengthData.Count > 3)
                        {
                            Console.WriteLine($"... and {allLengthData.Count - 3} more");
                        }
                        Console.WriteLine("=======================================");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RadialLines 적용 실패: " + ex.Message);
            }
        }

        /// <summary>
        /// 미리보기용 이진화 이미지 준비
        /// </summary>
        private void PrepareBinaryImageForPreview(Mat inputImage, Mat binaryImage, int threshold)
        {
            if (inputImage.Channels() >= 3)
            {
                using (Mat grayImage = new Mat())
                {
                    Cv2.CvtColor(inputImage, grayImage, ColorConversionCodes.BGR2GRAY);
                    Cv2.Threshold(grayImage, binaryImage, threshold, 255, ThresholdTypes.Binary);
                }
            }
            else
            {
                Cv2.Threshold(inputImage, binaryImage, threshold, 255, ThresholdTypes.Binary);
            }
        }

        /// <summary>
        /// 미리보기용 중심점들 계산
        /// </summary>
        private List<OpenCvSharp.Point> GetRadialCentersForPreview(Mat image, string centerMethod, int manualX, int manualY, int binaryThreshold)
        {
            var centers = new List<OpenCvSharp.Point>();

            switch (centerMethod)
            {
                case "ImageCenter":
                    centers.Add(new OpenCvSharp.Point(image.Width / 2, image.Height / 2));
                    break;

                case "AutoCentroid":
                    var centroid = FindAutoCentroidForPreview(image, binaryThreshold);
                    if (centroid.HasValue)
                    {
                        centers.Add(centroid.Value);
                    }
                    else
                    {
                        centers.Add(new OpenCvSharp.Point(image.Width / 2, image.Height / 2));
                    }
                    break;

                case "Manual":
                    centers.Add(new OpenCvSharp.Point(
                        Math.Min(manualX, image.Width - 1),
                        Math.Min(manualY, image.Height - 1)
                    ));
                    break;

                case "ExternalCoordinates":
                    // 이전 노드에서 받아온 무게중심 좌표 사용
                    if (HasCentroidData)
                    {
                        // 무게중심 좌표를 정수로 변환해서 사용
                        int centroidX = (int)Math.Round(_centroidX);
                        int centroidY = (int)Math.Round(_centroidY);

                        // 이미지 경계 체크
                        centroidX = Math.Max(0, Math.Min(image.Width - 1, centroidX));
                        centroidY = Math.Max(0, Math.Min(image.Height - 1, centroidY));

                        centers.Add(new OpenCvSharp.Point(centroidX, centroidY));
                        Console.WriteLine($"[디버그] ExternalCoordinates 사용: ({centroidX}, {centroidY})");
                    }
                    else
                    {
                        // 무게중심 데이터가 없는 경우 이미지 중심 사용 (fallback)
                        centers.Add(new OpenCvSharp.Point(image.Width / 2, image.Height / 2));
                        Console.WriteLine("[디버그] ExternalCoordinates 데이터 없음, 이미지 중심 사용");
                    }
                    break;

                case "MaxBrightness":
                    centers.Add(FindMaxBrightnessPointForPreview(image));
                    break;

                default:
                    centers.Add(new OpenCvSharp.Point(image.Width / 2, image.Height / 2));
                    break;
            }

            return centers;
        }

        /// <summary>
        /// 미리보기용 자동 무게중심 찾기
        /// </summary>
        private OpenCvSharp.Point? FindAutoCentroidForPreview(Mat image, int threshold)
        {
            try
            {
                using (Mat binaryImage = new Mat())
                {
                    PrepareBinaryImageForPreview(image, binaryImage, threshold);

                    OpenCvSharp.Point[][] contours;
                    HierarchyIndex[] hierarchy;
                    Cv2.FindContours(binaryImage, out contours, out hierarchy,
                                    RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                    if (contours.Length == 0) return null;

                    double maxArea = 0;
                    OpenCvSharp.Point? bestCentroid = null;

                    foreach (var contour in contours)
                    {
                        if (contour.Length < 5) continue;

                        var moments = Cv2.Moments(contour);
                        if (moments.M00 == 0) continue;

                        double area = moments.M00;
                        if (area > maxArea)
                        {
                            maxArea = area;
                            var centroidX = (int)(moments.M10 / moments.M00);
                            var centroidY = (int)(moments.M01 / moments.M00);
                            bestCentroid = new OpenCvSharp.Point(centroidX, centroidY);
                        }
                    }

                    return bestCentroid;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 미리보기용 최대 밝기 지점 찾기
        /// </summary>
        private OpenCvSharp.Point FindMaxBrightnessPointForPreview(Mat image)
        {
            using (Mat grayImage = new Mat())
            {
                if (image.Channels() >= 3)
                    Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);
                else
                    image.CopyTo(grayImage);

                OpenCvSharp.Point maxLoc;
                Cv2.MinMaxLoc(grayImage, out double minVal, out double maxVal, out OpenCvSharp.Point minLoc, out maxLoc);
                return maxLoc;
            }
        }

        /// <summary>
        /// 미리보기용 방사선 끝점 계산
        /// </summary>
        private OpenCvSharp.Point CalculateRadialEndPointForPreview(Mat originalImage, Mat binaryImage, OpenCvSharp.Point center,
                                                                   double angle, string rangeMethod, int fixedLength, int brightnessThreshold)
        {
            double radians = angle * Math.PI / 180.0;
            double dx = Math.Cos(radians);
            double dy = Math.Sin(radians);

            switch (rangeMethod)
            {
                case "FixedLength":
                    return new OpenCvSharp.Point(
                        center.X + (int)(fixedLength * dx),
                        center.Y + (int)(fixedLength * dy)
                    );

                case "ImageBoundary":
                    return CalculateImageBoundaryPointForPreview(originalImage, center, dx, dy);

                case "EdgeDetection":
                    return CalculateEdgeDetectionPointForPreview(binaryImage, center, dx, dy);

                case "BrightnessChange":
                    return CalculateBrightnessChangePointForPreview(originalImage, center, dx, dy, brightnessThreshold);

                default:
                    return CalculateImageBoundaryPointForPreview(originalImage, center, dx, dy);
            }
        }

        /// <summary>
        /// 미리보기용 이미지 경계 계산
        /// </summary>
        private OpenCvSharp.Point CalculateImageBoundaryPointForPreview(Mat image, OpenCvSharp.Point center, double dx, double dy)
        {
            double tMax = double.MaxValue;

            if (dx > 0) tMax = Math.Min(tMax, (image.Width - 1 - center.X) / dx);
            else if (dx < 0) tMax = Math.Min(tMax, -center.X / dx);

            if (dy > 0) tMax = Math.Min(tMax, (image.Height - 1 - center.Y) / dy);
            else if (dy < 0) tMax = Math.Min(tMax, -center.Y / dy);

            return new OpenCvSharp.Point(
                center.X + (int)(tMax * dx),
                center.Y + (int)(tMax * dy)
            );
        }

        /// <summary>
        /// 적응형 미리보기용 경계선 감지 - 중심점 색상 기반
        /// </summary>
        private OpenCvSharp.Point CalculateEdgeDetectionPointForPreview(Mat binaryImage, OpenCvSharp.Point center, double dx, double dy, int colorDifferenceThreshold = 50)
        {
            try
            {
                // 1. 중심점의 픽셀값을 기준값으로 설정
                byte basePixelValue = binaryImage.At<byte>(center.Y, center.X);

                int maxDistance = Math.Max(binaryImage.Width, binaryImage.Height);

                for (int t = 1; t < maxDistance; t++)
                {
                    int x = center.X + (int)(t * dx);
                    int y = center.Y + (int)(t * dy);

                    // 이미지 경계 검사
                    if (x < 0 || x >= binaryImage.Width || y < 0 || y >= binaryImage.Height)
                    {
                        return new OpenCvSharp.Point(
                            Math.Max(0, Math.Min(binaryImage.Width - 1, x)),
                            Math.Max(0, Math.Min(binaryImage.Height - 1, y))
                        );
                    }

                    // 현재 위치의 픽셀값 확인
                    byte currentPixelValue = binaryImage.At<byte>(y, x);

                    // 🔥 핵심 로직: 기준값과 충분히 다른 색상을 만나면 경계로 판단
                    int colorDifference = Math.Abs(currentPixelValue - basePixelValue);
                    if (colorDifference >= colorDifferenceThreshold)
                    {
                        return new OpenCvSharp.Point(x, y);
                    }
                }

                // 최대 거리까지 도달 (색상 변화가 없는 경우)
                return new OpenCvSharp.Point(
                    center.X + (int)(maxDistance * dx),
                    center.Y + (int)(maxDistance * dy)
                );
            }
            catch
            {
                return CalculateImageBoundaryPointForPreview(binaryImage, center, dx, dy);
            }
        }

        /// <summary>
        /// 미리보기용 밝기 변화 감지
        /// </summary>
        private OpenCvSharp.Point CalculateBrightnessChangePointForPreview(Mat image, OpenCvSharp.Point center, double dx, double dy, int threshold)
        {
            try
            {
                using (Mat grayImage = new Mat())
                {
                    if (image.Channels() >= 3)
                        Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);
                    else
                        image.CopyTo(grayImage);

                    byte centerBrightness = grayImage.At<byte>(center.Y, center.X);
                    int maxDistance = Math.Max(image.Width, image.Height);

                    for (int t = 1; t < maxDistance; t++)
                    {
                        int x = center.X + (int)(t * dx);
                        int y = center.Y + (int)(t * dy);

                        if (x < 0 || x >= image.Width || y < 0 || y >= image.Height)
                        {
                            return new OpenCvSharp.Point(
                                Math.Max(0, Math.Min(image.Width - 1, x)),
                                Math.Max(0, Math.Min(image.Height - 1, y))
                            );
                        }

                        byte currentBrightness = grayImage.At<byte>(y, x);
                        int brightnessDiff = Math.Abs(centerBrightness - currentBrightness);

                        if (brightnessDiff > threshold)
                        {
                            return new OpenCvSharp.Point(x, y);
                        }
                    }

                    return new OpenCvSharp.Point(
                        center.X + (int)(maxDistance * dx),
                        center.Y + (int)(maxDistance * dy)
                    );
                }
            }
            catch
            {
                return CalculateImageBoundaryPointForPreview(image, center, dx, dy);
            }
        }

        /// <summary>
        /// 미리보기용 방사선 그리기
        /// </summary>
        private void DrawRadialLineForPreview(Mat image, OpenCvSharp.Point center, OpenCvSharp.Point endPoint, double angle, double length,
                                            Color lineColor, int lineThickness, string style, bool showAngles, bool showDistances)
        {
            Scalar color = new Scalar(lineColor.B, lineColor.G, lineColor.R);

            // 선 그리기
            if (style == "Solid")
            {
                Cv2.Line(image, center, endPoint, color, lineThickness);
            }
            else
            {
                DrawStyledLineForPreview(image, center, endPoint, color, lineThickness, style);
            }

            // 각도 표시
            if (showAngles)
            {
                var textPoint = new OpenCvSharp.Point(
                    center.X + (int)(30 * Math.Cos(angle * Math.PI / 180)),
                    center.Y + (int)(30 * Math.Sin(angle * Math.PI / 180))
                );
                Cv2.PutText(image, $"{angle:F0}°", textPoint,
                           HersheyFonts.HersheySimplex, 0.4, color, 1);
            }

            // 거리 표시
            if (showDistances)
            {
                var textPoint = new OpenCvSharp.Point((center.X + endPoint.X) / 2, (center.Y + endPoint.Y) / 2);
                Cv2.PutText(image, $"{length:F0}", textPoint,
                           HersheyFonts.HersheySimplex, 0.4, color, 1);
            }
        }

        /// <summary>
        /// 미리보기용 스타일 선 그리기
        /// </summary>
        private void DrawStyledLineForPreview(Mat image, OpenCvSharp.Point start, OpenCvSharp.Point end, Scalar color, int thickness, string style)
        {
            double distance = Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
            int segments = style == "Dotted" ? (int)distance / 10 : (int)distance / 20;

            for (int i = 0; i < segments; i += 2)
            {
                double t1 = (double)i / segments;
                double t2 = Math.Min((double)(i + 1) / segments, 1.0);

                var p1 = new OpenCvSharp.Point(
                    start.X + (int)(t1 * (end.X - start.X)),
                    start.Y + (int)(t1 * (end.Y - start.Y))
                );
                var p2 = new OpenCvSharp.Point(
                    start.X + (int)(t2 * (end.X - start.X)),
                    start.Y + (int)(t2 * (end.Y - start.Y))
                );

                Cv2.Line(image, p1, p2, color, thickness);
            }
        }

        /// <summary>
        /// 미리보기용 중심점 그리기
        /// </summary>
        private void DrawCenterPointForPreview(Mat image, OpenCvSharp.Point center, int centerIndex, Color lineColor, int lineThickness)
        {
            Scalar color = new Scalar(lineColor.B, lineColor.G, lineColor.R);

            Cv2.Circle(image, center, 5, color, lineThickness);
            Cv2.Circle(image, center, 2, new Scalar(255, 255, 255), -1);

            if (centerIndex >= 0)
            {
                Cv2.PutText(image, centerIndex.ToString(),
                           new OpenCvSharp.Point(center.X + 10, center.Y - 10),
                           HersheyFonts.HersheySimplex, 0.6, color, 2);
            }
        }
        /// <summary>
        /// Moments 파라미터가 변경될 때 실시간으로 이미지에 적용합니다.
        /// </summary>
        private void MomentsParameterChanged(int threshold, bool showCentroid, bool showArea,
            bool showOrientation, bool showBoundingBox, bool showEccentricity, Color drawColor, int lineThickness)
        {
            if (_originalMat == null) return;

            try
            {
                using (Mat binaryImage = new Mat())
                using (Mat outputImage = new Mat())
                {
                    // 1. 이진화 처리
                    if (_originalMat.Channels() >= 3)
                    {
                        using (Mat grayImage = new Mat())
                        {
                            Cv2.CvtColor(_originalMat, grayImage, ColorConversionCodes.BGR2GRAY);
                            Cv2.Threshold(grayImage, binaryImage, threshold, 255, ThresholdTypes.Binary);
                        }
                    }
                    else
                    {
                        Cv2.Threshold(_originalMat, binaryImage, threshold, 255, ThresholdTypes.Binary);
                    }

                    // 2. 출력 이미지 준비 (원본을 복사하여 컬러로 표시)
                    if (_originalMat.Channels() >= 3)
                    {
                        _originalMat.CopyTo(outputImage);
                    }
                    else
                    {
                        Cv2.CvtColor(_originalMat, outputImage, ColorConversionCodes.GRAY2BGR);
                    }

                    // 3. 컨투어 찾기 (모멘트 계산을 위해)
                    OpenCvSharp.Point[][] contours;
                    HierarchyIndex[] hierarchy;
                    Cv2.FindContours(binaryImage, out contours, out hierarchy,
                                    RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                    Scalar color = new Scalar(drawColor.B, drawColor.G, drawColor.R); // BGR 순서

                    // 4. 각 컨투어에 대해 모멘트 분석
                    for (int i = 0; i < contours.Length; i++)
                    {
                        if (contours[i].Length < 5) continue; // 너무 작은 컨투어 제외

                        // 모멘트 계산
                        var moments = Cv2.Moments(contours[i]);
                        if (moments.M00 == 0) continue; // 면적이 0인 경우 제외

                        // 중심점 계산
                        var centroidX = (int)(moments.M10 / moments.M00);
                        var centroidY = (int)(moments.M01 / moments.M00);
                        var centroid = new OpenCvSharp.Point(centroidX, centroidY);

                        // 중심점 표시
                        if (showCentroid)
                        {
                            Cv2.Circle(outputImage, centroid, 5, color, lineThickness);
                            Cv2.PutText(outputImage, $"({centroidX},{centroidY})",
                                       new OpenCvSharp.Point(centroidX + 10, centroidY - 10),
                                       HersheyFonts.HersheySimplex, 0.5, color, 1);
                        }

                        // 면적 표시
                        if (showArea)
                        {
                            double area = moments.M00;
                            Cv2.PutText(outputImage, $"Area: {area:F0}",
                                       new OpenCvSharp.Point(centroidX + 10, centroidY + 10),
                                       HersheyFonts.HersheySimplex, 0.5, color, 1);
                        }

                        // 방향각 계산 및 표시
                        if (showOrientation)
                        {
                            // 중심 모멘트 계산
                            double mu20 = moments.M20 - (moments.M10 * moments.M10) / moments.M00;
                            double mu02 = moments.M02 - (moments.M01 * moments.M01) / moments.M00;
                            double mu11 = moments.M11 - (moments.M10 * moments.M01) / moments.M00;

                            if (Math.Abs(mu20 - mu02) > 1e-6 || Math.Abs(mu11) > 1e-6)
                            {
                                double angle = 0.5 * Math.Atan2(2 * mu11, mu20 - mu02) * 180.0 / Math.PI;

                                // 방향선 그리기 (길이 50픽셀)
                                int lineLength = 50;
                                double radians = angle * Math.PI / 180.0;
                                var endPoint = new OpenCvSharp.Point(
                                    centroidX + (int)(lineLength * Math.Cos(radians)),
                                    centroidY + (int)(lineLength * Math.Sin(radians))
                                );

                                Cv2.Line(outputImage, centroid, endPoint, color, lineThickness);
                                Cv2.PutText(outputImage, $"{angle:F1}°",
                                           new OpenCvSharp.Point(centroidX + 10, centroidY + 30),
                                           HersheyFonts.HersheySimplex, 0.5, color, 1);
                            }
                        }

                        // 경계박스 표시
                        if (showBoundingBox)
                        {
                            var boundingRect = Cv2.BoundingRect(contours[i]);
                            Cv2.Rectangle(outputImage, boundingRect, color, lineThickness);

                            double aspectRatio = (double)boundingRect.Width / boundingRect.Height;
                            Cv2.PutText(outputImage, $"W:{boundingRect.Width} H:{boundingRect.Height}",
                                       new OpenCvSharp.Point(boundingRect.X, boundingRect.Y - 10),
                                       HersheyFonts.HersheySimplex, 0.4, color, 1);
                            Cv2.PutText(outputImage, $"Ratio:{aspectRatio:F2}",
                                       new OpenCvSharp.Point(boundingRect.X, boundingRect.Y - 25),
                                       HersheyFonts.HersheySimplex, 0.4, color, 1);
                        }

                        // 편심률 계산 및 표시
                        if (showEccentricity)
                        {
                            // 중심 모멘트 계산
                            double mu20 = moments.M20 - (moments.M10 * moments.M10) / moments.M00;
                            double mu02 = moments.M02 - (moments.M01 * moments.M01) / moments.M00;
                            double mu11 = moments.M11 - (moments.M10 * moments.M01) / moments.M00;

                            // 공분산 행렬의 고유값 계산
                            double trace = mu20 + mu02;
                            double det = mu20 * mu02 - mu11 * mu11;

                            if (det > 0 && trace > 0)
                            {
                                double discriminant = trace * trace - 4 * det;
                                if (discriminant >= 0)
                                {
                                    double lambda1 = (trace + Math.Sqrt(discriminant)) / 2.0;
                                    double lambda2 = (trace - Math.Sqrt(discriminant)) / 2.0;

                                    double lambdaMin = Math.Min(lambda1, lambda2);
                                    double lambdaMax = Math.Max(lambda1, lambda2);

                                    if (lambdaMax > 0)
                                    {
                                        double eccentricity = Math.Sqrt(1.0 - lambdaMin / lambdaMax);
                                        Cv2.PutText(outputImage, $"Ecc:{eccentricity:F3}",
                                                   new OpenCvSharp.Point(centroidX + 10, centroidY + 50),
                                                   HersheyFonts.HersheySimplex, 0.5, color, 1);
                                    }
                                }
                            }
                        }
                    }

                    // 5. 결과 이미지 표시
                    SetPictureBoxImage(pictureBox_main, OpenCvSharp.Extensions.BitmapConverter.ToBitmap(outputImage));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Moments 적용 실패: " + ex.Message);
            }
        }
        /// <summary>
        /// Contour Detection 파라미터가 변경될 때 실시간으로 이미지에 적용합니다.
        /// </summary>
        private void ContourParameterChanged(string retrievalMode, string approximationMethod,
    double minArea, double maxArea, bool drawOnOriginal, int thickness,
    string colorMode, Color fixedColor, bool showNumbers,
    bool showVisualization, bool outputData, bool outputAsJson) // 3개 파라미터 추가
        {
            if (_originalMat == null) return;

            try
            {
                using (Mat binaryImage = new Mat())
                using (Mat outputImage = new Mat())
                {
                    // 1. 이진 이미지 준비 (기존과 동일)
                    if (_originalMat.Channels() >= 3)
                    {
                        using (Mat grayImage = new Mat())
                        {
                            Cv2.CvtColor(_originalMat, grayImage, ColorConversionCodes.BGR2GRAY);
                            Scalar mean = Cv2.Mean(grayImage);
                            if (mean.Val0 > 50 && mean.Val0 < 200)
                            {
                                Cv2.Threshold(grayImage, binaryImage, 127, 255, ThresholdTypes.Binary);
                            }
                            else
                            {
                                grayImage.CopyTo(binaryImage);
                            }
                        }
                    }
                    else
                    {
                        _originalMat.CopyTo(binaryImage);
                    }

                    // 2-4. 컨투어 검출 및 필터링 (기존과 동일)
                    RetrievalModes retrievalModeEnum;
                    switch (retrievalMode)
                    {
                        case "External": retrievalModeEnum = RetrievalModes.External; break;
                        case "List": retrievalModeEnum = RetrievalModes.List; break;
                        case "CComp": retrievalModeEnum = RetrievalModes.CComp; break;
                        case "Tree": retrievalModeEnum = RetrievalModes.Tree; break;
                        default: retrievalModeEnum = RetrievalModes.External; break;
                    }

                    ContourApproximationModes approximationModeEnum;
                    switch (approximationMethod)
                    {
                        case "None": approximationModeEnum = ContourApproximationModes.ApproxNone; break;
                        case "Simple": approximationModeEnum = ContourApproximationModes.ApproxSimple; break;
                        case "TC89_L1": approximationModeEnum = ContourApproximationModes.ApproxTC89L1; break;
                        default: approximationModeEnum = ContourApproximationModes.ApproxSimple; break;
                    }

                    OpenCvSharp.Point[][] contours;
                    HierarchyIndex[] hierarchy;
                    Cv2.FindContours(binaryImage, out contours, out hierarchy,
                                    retrievalModeEnum, approximationModeEnum);

                    var filteredContours = new List<OpenCvSharp.Point[]>();
                    for (int i = 0; i < contours.Length; i++)
                    {
                        double area = Cv2.ContourArea(contours[i]);
                        if (area >= minArea && area <= maxArea)
                        {
                            filteredContours.Add(contours[i]);
                        }
                    }

                    // === 5. 시각화 처리 (조건부로 변경) ===
                    if (showVisualization)
                    {
                        // 출력 이미지 준비
                        if (drawOnOriginal && _originalMat.Channels() >= 3)
                        {
                            _originalMat.CopyTo(outputImage);
                        }
                        else
                        {
                            Cv2.CvtColor(binaryImage, outputImage, ColorConversionCodes.GRAY2BGR);
                        }

                        // 컨투어 그리기
                        Random random = new Random();
                        for (int i = 0; i < filteredContours.Count; i++)
                        {
                            Scalar color;

                            switch (colorMode)
                            {
                                case "Random":
                                    color = new Scalar(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
                                    break;
                                case "SizeBased":
                                    double area = Cv2.ContourArea(filteredContours[i]);
                                    double normalizedArea = Math.Min(area / 10000.0, 1.0);
                                    color = new Scalar(
                                        (int)(255 * (1 - normalizedArea)),
                                        (int)(255 * normalizedArea),
                                        (int)(128 + 127 * normalizedArea)
                                    );
                                    break;
                                case "Fixed":
                                default:
                                    color = new Scalar(fixedColor.B, fixedColor.G, fixedColor.R);
                                    break;
                            }

                            Cv2.DrawContours(outputImage, filteredContours, i, color, thickness);

                            if (showNumbers)
                            {
                                var moments = Cv2.Moments(filteredContours[i]);
                                if (moments.M00 != 0)
                                {
                                    var centroid = new OpenCvSharp.Point(
                                        (int)(moments.M10 / moments.M00),
                                        (int)(moments.M01 / moments.M00)
                                    );
                                    Cv2.PutText(outputImage, i.ToString(), centroid,
                                               HersheyFonts.HersheySimplex, 0.8,
                                               new Scalar(255, 255, 255), 2);
                                }
                            }
                        }
                    }
                    else
                    {
                        // === 시각화가 꺼져있으면 원본 이미지 그대로 표시 ===
                        if (_originalMat.Channels() >= 3)
                        {
                            _originalMat.CopyTo(outputImage);
                        }
                        else
                        {
                            Cv2.CvtColor(_originalMat, outputImage, ColorConversionCodes.GRAY2BGR);
                        }

                        // 시각화가 꺼져있어도 정보는 표시
                        Cv2.PutText(outputImage, $"Contours: {filteredContours.Count} (Visualization OFF)",
                                   new OpenCvSharp.Point(10, 30),
                                   HersheyFonts.HersheySimplex, 0.7, new Scalar(0, 255, 0), 2);
                    }

                    // === 6. 데이터 출력 상태 표시 ===
                    if (outputData)
                    {
                        string dataStatus = $"Data Output: {(outputAsJson ? "JSON" : "Text")}";
                        Cv2.PutText(outputImage, dataStatus,
                                   new OpenCvSharp.Point(10, outputImage.Height - 20),
                                   HersheyFonts.HersheySimplex, 0.6, new Scalar(255, 255, 0), 2);
                    }

                    // 7. 결과 이미지 표시
                    SetPictureBoxImage(pictureBox_main, OpenCvSharp.Extensions.BitmapConverter.ToBitmap(outputImage));

                    // === 8. 데이터 출력이 활성화되어 있으면 콘솔에 미리보기 출력 ===
                    if (outputData)
                    {
                        string previewData = GenerateContourDataPreview(filteredContours, outputAsJson);
                        Console.WriteLine("=== Contour Data Preview ===");
                        Console.WriteLine(previewData);
                        Console.WriteLine("=============================");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Contour 적용 실패: " + ex.Message);
            }
        }

        /// <summary>
        /// 컨투어 데이터 미리보기 생성 (실시간 피드백용)
        /// </summary>
        private string GenerateContourDataPreview(List<OpenCvSharp.Point[]> contours, bool asJson)
        {
            if (contours.Count == 0) return "No contours found.";

            var sb = new StringBuilder();

            if (asJson)
            {
                sb.AppendLine("{");
                sb.AppendLine($"  \"ContourCount\": {contours.Count},");
                sb.AppendLine("  \"Summary\": [");

                for (int i = 0; i < Math.Min(contours.Count, 3); i++) // 최대 3개만 미리보기
                {
                    var contour = contours[i];
                    var area = Cv2.ContourArea(contour);
                    var perimeter = Cv2.ArcLength(contour, true);

                    sb.AppendLine($"    {{\"Index\": {i}, \"Points\": {contour.Length}, \"Area\": {area:F1}, \"Perimeter\": {perimeter:F1}}}");
                    if (i < Math.Min(contours.Count, 3) - 1) sb.Append(",");
                }

                if (contours.Count > 3)
                    sb.AppendLine($"    ... and {contours.Count - 3} more contours");

                sb.AppendLine("  ]");
                sb.AppendLine("}");
            }
            else
            {
                sb.AppendLine($"Total Contours: {contours.Count}");
                sb.AppendLine("Preview (first 3):");

                for (int i = 0; i < Math.Min(contours.Count, 3); i++)
                {
                    var contour = contours[i];
                    var area = Cv2.ContourArea(contour);
                    var perimeter = Cv2.ArcLength(contour, true);

                    sb.AppendLine($"  #{i}: {contour.Length} points, Area: {area:F1}, Perimeter: {perimeter:F1}");
                }

                if (contours.Count > 3)
                    sb.AppendLine($"  ... and {contours.Count - 3} more contours");
            }

            return sb.ToString();
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
        private void SetPictureBoxImage(PictureBox target, System.Drawing.Image src)
        {
            target.Image?.Dispose(); // 기존 이미지 메모리 해제
            target.Image = null;

            if (src != null)
                target.Image = (System.Drawing.Image)src.Clone(); // 새 이미지 복사본 설정
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