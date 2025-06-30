using System;
using System.ComponentModel;
using NodeEditor;
using OpenCvSharp;
using IFVisionEngine.Manager; // ImageDataManager를 사용하기 위해 추가
using IFVisionEngine.UIComponents.Dialogs;
using System.Windows.Forms;
using static MyNodesContext;
using System.Collections.Generic;

public partial class MyNodesContext
{
    #region GaussianBlur
    // --- 가우시안 블러 파라미터 클래스 ---
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GaussianBlurParameters
    {
        private int _kernelWidth = 5;
        [Description("커널의 너비입니다. 반드시 홀수여야 합니다. (예: 1, 3, 5, ...)")]
        public int KernelWidth
        {
            get => _kernelWidth;
            set => _kernelWidth = (value <= 0) ? 1 : (value % 2 == 0) ? value + 1 : value;
        }

        private int _kernelHeight = 5;
        [Description("커널의 높이입니다. 반드시 홀수여야 합니다. (예: 1, 3, 5, ...)")]
        public int KernelHeight
        {
            get => _kernelHeight;
            set => _kernelHeight = (value <= 0) ? 1 : (value % 2 == 0) ? value + 1 : value;
        }

        [Description("X 방향 가우시안 커널 표준 편차입니다. 0이면 커널 크기로부터 자동 계산됩니다.")]
        public double SigmaX { get; set; } = 0;

        [Description("Y 방향 가우시안 커널 표준 편차입니다. 0이면 SigmaX와 동일하게 설정됩니다.")]
        public double SigmaY { get; set; } = 0;

        public override string ToString()
        {
            return $"Size:({KernelWidth}x{KernelHeight}), SigmaX:{SigmaX:F1}, SigmaY:{SigmaY:F1}";
        }
    }

    // --- 가우시안 블러 노드 ---
    [Node(name: "GaussianBlur", menu: "전처리/필터", description: "이미지에 가우시안 블러를 적용합니다.")]
    public void ApplyGaussianBlur(string input, GaussianBlurParameters GaussianBlurparameters, out string output)
    {
        output = null;

        // 먼저 입력 키가 유효한지 확인합니다.
        if (string.IsNullOrEmpty(input))
        {
            FeedbackInfo?.Invoke("입력으로 전달된 이미지 키가 비어있습니다. 이전 노드의 실행 결과를 확인하세요.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        Mat inputImage = ImageDataManager.GetImage(input);

        if (inputImage == null || inputImage.Empty())
        {
            FeedbackInfo?.Invoke($"이미지 키 '{input}'에 해당하는 이미지를 찾을 수 없습니다.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        try
        {
            using (Mat tempOutput = new Mat())
            {
                Size ksize = new Size(GaussianBlurparameters.KernelWidth, GaussianBlurparameters.KernelHeight);
                Cv2.GaussianBlur(inputImage, tempOutput, ksize, GaussianBlurparameters.SigmaX, GaussianBlurparameters.SigmaY);

                output = CurrentProcessingNode.GetHashCode().ToString();
                ImageDataManager.RegisterImage(output, tempOutput);
                ImageKeySelected?.Invoke(output,CurrentProcessingNode.Name);
                FeedbackInfo?.Invoke("가우시안 블러 적용 완료.", CurrentProcessingNode, FeedbackType.Information, tempOutput.Clone(), false);
            }
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"가우시안 블러 처리 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
        }
    }
    #endregion
    #region Binarization 
    // --- 이진화 파라미터 클래스 ---
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class BinarizationParameters
    {
        public enum BinarizationMethod
        {
            Binary,      // 픽셀값이 임계값보다 크면 MaxValue, 아니면 0
            BinaryInv,   // 픽셀값이 임계값보다 크면 0, 아니면 MaxValue
            Trunc,       // 픽셀값이 임계값보다 크면 임계값, 아니면 그대로
            Tozero,      // 픽셀값이 임계값보다 크면 그대로, 아니면 0
            TozeroInv    // 픽셀값이 임계값보다 크면 0, 아니면 그대로
        }

        [Description("이진화 방식을 선택합니다.")]
        public BinarizationMethod Method { get; set; } = BinarizationMethod.Binary;

        [Description("Otsu의 알고리즘을 사용하여 최적의 임계값을 자동으로 찾으려면 체크하세요. 체크 시 아래 '임계값'은 무시됩니다.")]
        public bool UseOtsu { get; set; } = true;

        [Description("임계값 (0-255). UseOtsu가 체크 해제된 경우에만 사용됩니다.")]
        public double ThresholdValue { get; set; } = 127.0;

        [Description("임계값을 통과한 픽셀에 적용할 최대값입니다. 보통 255(흰색)를 사용합니다.")]
        public double MaxValue { get; set; } = 255.0;

        public override string ToString()
        {
            return UseOtsu ? $"Method: {Method}, Otsu" : $"Method: {Method}, Threshold: {ThresholdValue}";
        }
    }

    // --- 이진화 노드 ---
    [Node(name: "Binarization", menu: "전처리/필터", description: "이미지를 흑과 백으로 변환(이진화)합니다.")]
    public void ApplyBinarization(string input, BinarizationParameters Binarizationparameters, out string output)
    {
        output = null;
        // 먼저 입력 키가 유효한지 확인합니다.
        if (string.IsNullOrEmpty(input))
        {
            FeedbackInfo?.Invoke("입력으로 전달된 이미지 키가 비어있습니다. 이전 노드의 실행 결과를 확인하세요.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        Mat inputImage = ImageDataManager.GetImage(input);

        if (inputImage == null || inputImage.Empty())
        {
            FeedbackInfo?.Invoke($"이미지 키 '{input}'에 해당하는 이미지를 찾을 수 없습니다.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        try
        {
            using (Mat grayImage = new Mat())
            using (Mat tempOutput = new Mat())
            {
                if (inputImage.Channels() >= 3)
                {
                    Cv2.CvtColor(inputImage, grayImage, ColorConversionCodes.BGR2GRAY);
                }
                else
                {
                    inputImage.CopyTo(grayImage);
                }

                ThresholdTypes thresholdType = (ThresholdTypes)Binarizationparameters.Method;
                if (Binarizationparameters.UseOtsu)
                {
                    thresholdType |= ThresholdTypes.Otsu;
                }

                Cv2.Threshold(grayImage, tempOutput, Binarizationparameters.ThresholdValue, Binarizationparameters.MaxValue, thresholdType);

                output = CurrentProcessingNode.GetHashCode().ToString();
                ImageDataManager.RegisterImage(output, tempOutput);
                ImageKeySelected?.Invoke(output, CurrentProcessingNode.Name);
                FeedbackInfo?.Invoke("이진화 적용 완료.", CurrentProcessingNode, FeedbackType.Information, tempOutput.Clone(), false);
            }
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"이진화 처리 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
        }
    }
    #endregion
    #region CLAHE
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ClaheParameters
    {
        [Description("타일 그리드의 너비.")]
        public int TileGridWidth { get; set; } = 8;
        [Description("타일 그리드의 높이.")]
        public int TileGridHeight { get; set; } = 8;
        [Description("대비 제한 (ClipLimit). 일반적으로 2.0~4.0 권장")]
        public double ClipLimit { get; set; } = 2.0;

        public override string ToString()
        {
            return $"TileGrid:({TileGridWidth}x{TileGridHeight}), ClipLimit:{ClipLimit:F1}";
        }
    }
    [Node(name: "CLAHE", menu: "전처리/필터", description: "이미지 대비를 CLAHE 알고리즘으로 향상시킵니다.")]
    public void ApplyClahe(string input, ClaheParameters CLAHEparameters, out string output)
    {
        output = null;

        if (string.IsNullOrEmpty(input))
        {
            FeedbackInfo?.Invoke("입력으로 전달된 이미지 키가 비어있습니다. 이전 노드의 실행 결과를 확인하세요.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        Mat inputImage = ImageDataManager.GetImage(input);

        if (inputImage == null || inputImage.Empty())
        {
            FeedbackInfo?.Invoke($"이미지 키 '{input}'에 해당하는 이미지를 찾을 수 없습니다.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        try
        {
            using (Mat grayImage = new Mat())
            using (Mat tempOutput = new Mat())
            {
                // 입력이 컬러면 그레이로 변환
                if (inputImage.Channels() >= 3)
                    Cv2.CvtColor(inputImage, grayImage, ColorConversionCodes.BGR2GRAY);
                else
                    inputImage.CopyTo(grayImage);

                // CLAHE 객체 생성 및 적용
                var clahe = Cv2.CreateCLAHE(CLAHEparameters.ClipLimit, new Size(CLAHEparameters.TileGridWidth, CLAHEparameters.TileGridHeight));
                clahe.Apply(grayImage, tempOutput);

                output = CurrentProcessingNode.GetHashCode().ToString();
                ImageDataManager.RegisterImage(output, tempOutput);
                ImageKeySelected?.Invoke(output, CurrentProcessingNode.Name);
                FeedbackInfo?.Invoke("CLAHE 적용 완료.", CurrentProcessingNode, FeedbackType.Information, tempOutput.Clone(), false);
            }
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"CLAHE 처리 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
        }
    }
    #endregion
    #region edge
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class EdgeDetectionParameters
    {
        public enum EdgeMethod
        {
            Canny,
            Sobel,
            Laplacian
        }

        [Description("엣지 검출 방법을 선택합니다.")]
        public EdgeMethod Method { get; set; } = EdgeMethod.Canny;

        [Description("Canny: 하위 임계값")] public double CannyThreshold1 { get; set; } = 100;
        [Description("Canny: 상위 임계값")] public double CannyThreshold2 { get; set; } = 200;

        [Description("Sobel/Laplacian: 커널 크기")] public int KernelSize { get; set; } = 3;

        public override string ToString()
        {
            switch (Method)
            {
                case EdgeMethod.Canny:
                    return $"Canny ({CannyThreshold1}, {CannyThreshold2})";
                case EdgeMethod.Sobel:
                    return $"Sobel (k={KernelSize})";
                case EdgeMethod.Laplacian:
                    return $"Laplacian (k={KernelSize})";
                default:
                    return Method.ToString();
            }
        }

    }
    [Node(name: "Edge", menu: "전처리/필터", description: "이미지 엣지를 검출합니다.")]
    public void ApplyEdgeDetection(string input, EdgeDetectionParameters Edgeparameters, out string output)
    {
        output = null;

        if (string.IsNullOrEmpty(input))
        {
            FeedbackInfo?.Invoke("입력으로 전달된 이미지 키가 비어있습니다. 이전 노드의 실행 결과를 확인하세요.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        Mat inputImage = ImageDataManager.GetImage(input);

        if (inputImage == null || inputImage.Empty())
        {
            FeedbackInfo?.Invoke($"이미지 키 '{input}'에 해당하는 이미지를 찾을 수 없습니다.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        try
        {
            using (Mat grayImage = new Mat())
            using (Mat tempOutput = new Mat())
            {
                // 컬러는 그레이로 변환
                if (inputImage.Channels() >= 3)
                    Cv2.CvtColor(inputImage, grayImage, ColorConversionCodes.BGR2GRAY);
                else
                    inputImage.CopyTo(grayImage);

                switch (Edgeparameters.Method)
                {
                    case EdgeDetectionParameters.EdgeMethod.Canny:
                        Cv2.Canny(grayImage, tempOutput, Edgeparameters.CannyThreshold1, Edgeparameters.CannyThreshold2);
                        break;
                    case EdgeDetectionParameters.EdgeMethod.Sobel:
                        // 개선된 Sobel 엣지 검출 - X, Y 방향을 분리하여 정확한 그래디언트 계산
                        using (var gradX = new Mat())
                        using (var gradY = new Mat())
                        using (var absGradX = new Mat())
                        using (var absGradY = new Mat())
                        {
                            // X 방향 그래디언트 계산 (16비트로 정밀도 향상)
                            Cv2.Sobel(grayImage, gradX, MatType.CV_16S, 1, 0, Edgeparameters.KernelSize);

                            // Y 방향 그래디언트 계산 (16비트로 정밀도 향상)
                            Cv2.Sobel(grayImage, gradY, MatType.CV_16S, 0, 1, Edgeparameters.KernelSize);

                            // 절댓값 변환 (16비트 → 8비트, 음수값 처리)
                            Cv2.ConvertScaleAbs(gradX, absGradX);
                            Cv2.ConvertScaleAbs(gradY, absGradY);

                            // 최종 그래디언트 크기 계산 (|Gx| + |Gy|의 가중합)
                            Cv2.AddWeighted(absGradX, 0.5, absGradY, 0.5, 0, tempOutput);
                        }
                        break;
                    case EdgeDetectionParameters.EdgeMethod.Laplacian:
                        Cv2.Laplacian(grayImage, tempOutput, MatType.CV_8U, Edgeparameters.KernelSize);
                        break;
                }

                output = CurrentProcessingNode.GetHashCode().ToString();
                ImageDataManager.RegisterImage(output, tempOutput);
                ImageKeySelected?.Invoke(output, CurrentProcessingNode.Name);
                FeedbackInfo?.Invoke("엣지 검출 완료.", CurrentProcessingNode, FeedbackType.Information, tempOutput.Clone(), false);
            }
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"엣지 검출 처리 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
        }
    }
    #endregion
    #region Grayscale
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GrayscaleParameters
    {
        // (옵션 파라미터가 필요하면 여기에 추가)
        public override string ToString() => "Grayscale";
    }

    [Node(name: "Grayscale", menu: "전처리/필터", description: "컬러 이미지를 그레이스케일로 변환합니다.")]
    public void ApplyGrayscale(string input, GrayscaleParameters Grayscale, out string output)
    {
        output = null;

        if (string.IsNullOrEmpty(input))
        {
            FeedbackInfo?.Invoke("입력으로 전달된 이미지 키가 비어있습니다. 이전 노드의 실행 결과를 확인하세요.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        Mat inputImage = ImageDataManager.GetImage(input);

        if (inputImage == null || inputImage.Empty())
        {
            FeedbackInfo?.Invoke($"이미지 키 '{input}'에 해당하는 이미지를 찾을 수 없습니다.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        try
        {
            using (Mat gray = new Mat())
            {
                if (inputImage.Channels() >= 3)
                    Cv2.CvtColor(inputImage, gray, ColorConversionCodes.BGR2GRAY);
                else
                    inputImage.CopyTo(gray);

                output = CurrentProcessingNode.GetHashCode().ToString();
                ImageDataManager.RegisterImage(output, gray);
                ImageKeySelected?.Invoke(output, CurrentProcessingNode.Name);
                FeedbackInfo?.Invoke("Grayscale 변환 완료.", CurrentProcessingNode, FeedbackType.Information, gray.Clone(), false);
            }
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"Grayscale 처리 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
        }
    }
    #endregion
    #region Contour
    // --- 컨투어 검출 파라미터 클래스 ---
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ContourParameters
    {
        public enum ContourRetrievalMode
        {
            [Description("외부 컨투어만 검출")]
            External = RetrievalModes.External,
            [Description("모든 컨투어를 계층 없이 검출")]
            List = RetrievalModes.List,
            [Description("외부와 내부 홀(hole) 컨투어만 검출")]
            CComp = RetrievalModes.CComp,
            [Description("모든 컨투어를 전체 계층으로 검출")]
            Tree = RetrievalModes.Tree
        }

        public enum ContourApproximationMethod
        {
            [Description("모든 컨투어 점을 저장 (압축 없음)")]
            None = ContourApproximationModes.ApproxNone,
            [Description("수평, 수직, 대각선 세그먼트를 압축하여 끝점만 남김")]
            Simple = ContourApproximationModes.ApproxSimple,
            [Description("TC89_L1 알고리즘 적용")]
            TC89_L1 = ContourApproximationModes.ApproxTC89L1
        }

        [Description("컨투어 검출 모드를 선택합니다.")]
        public ContourRetrievalMode RetrievalMode { get; set; } = ContourRetrievalMode.External;

        [Description("컨투어 근사화 방법을 선택합니다.")]
        public ContourApproximationMethod ApproximationMethod { get; set; } = ContourApproximationMethod.Simple;

        private double _minContourArea = 100.0;
        [Description("최소 컨투어 면적입니다. 이 값보다 작은 컨투어는 필터링됩니다.")]
        public double MinContourArea
        {
            get => _minContourArea;
            set => _minContourArea = Math.Max(0, value);
        }

        private double _maxContourArea = double.MaxValue;
        [Description("최대 컨투어 면적입니다. 이 값보다 큰 컨투어는 필터링됩니다.")]
        public double MaxContourArea
        {
            get => _maxContourArea;
            set => _maxContourArea = Math.Max(_minContourArea, value);
        }

        [Description("컨투어를 원본 이미지에 그릴지 여부를 설정합니다.")]
        public bool DrawOnOriginal { get; set; } = true;

        [Description("컨투어 선의 두께입니다.")]
        public int Thickness { get; set; } = 2;

        public enum ContourColorMode
        {
            [Description("고정 색상 사용")]
            Fixed,
            [Description("각 컨투어마다 랜덤 색상")]
            Random,
            [Description("컨투어 크기에 따른 색상")]
            SizeBased
        }

        [Description("컨투어 색상 모드를 선택합니다.")]
        public ContourColorMode ColorMode { get; set; } = ContourColorMode.Fixed;

        [Description("고정 색상 모드일 때 사용할 색상 (BGR 순서)")]
        public System.Drawing.Color FixedColor { get; set; } = System.Drawing.Color.Green;

        [Description("컨투어 번호를 표시할지 여부를 설정합니다.")]
        public bool ShowContourNumbers { get; set; } = false;

        public override string ToString()
        {
            return $"Mode:{RetrievalMode}, Method:{ApproximationMethod}, Area:{MinContourArea:F0}~{(MaxContourArea == double.MaxValue ? "∞" : MaxContourArea.ToString("F0"))}";
        }
    }

    // --- 컨투어 검출 노드 ---
    [Node(name: "Contour", menu: "분석/검출", description: "이미지에서 컨투어를 검출하고 시각화합니다.")]
    public void ApplyContourDetection(string input, ContourParameters Contourparameters, out string output)
    {
        output = null;

        // 먼저 입력 키가 유효한지 확인합니다.
        if (string.IsNullOrEmpty(input))
        {
            FeedbackInfo?.Invoke("입력으로 전달된 이미지 키가 비어있습니다. 이전 노드의 실행 결과를 확인하세요.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        Mat inputImage = ImageDataManager.GetImage(input);
        if (inputImage == null || inputImage.Empty())
        {
            FeedbackInfo?.Invoke($"이미지 키 '{input}'에 해당하는 이미지를 찾을 수 없습니다.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        try
        {
            using (Mat binaryImage = new Mat())
            using (Mat outputImage = new Mat())
            {
                // 컨투어 검출을 위해 이진 이미지 준비
                if (inputImage.Channels() >= 3)
                {
                    using (Mat grayImage = new Mat())
                    {
                        Cv2.CvtColor(inputImage, grayImage, ColorConversionCodes.BGR2GRAY);
                        // 이미 이진화된 이미지인지 확인 (대부분의 픽셀이 0 또는 255인지)
                        Scalar mean = Cv2.Mean(grayImage);
                        if (mean.Val0 > 50 && mean.Val0 < 200) // 그레이스케일이면 임계값 적용
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
                    inputImage.CopyTo(binaryImage);
                }

                // 컨투어 검출
                Point[][] contours;
                HierarchyIndex[] hierarchy;
                Cv2.FindContours(binaryImage, out contours, out hierarchy,
                                (RetrievalModes)Contourparameters.RetrievalMode,
                                (ContourApproximationModes)Contourparameters.ApproximationMethod);

                // 면적 기준으로 컨투어 필터링
                var filteredContours = new List<Point[]>();
                var filteredIndices = new List<int>();

                for (int i = 0; i < contours.Length; i++)
                {
                    double area = Cv2.ContourArea(contours[i]);
                    if (area >= Contourparameters.MinContourArea && area <= Contourparameters.MaxContourArea)
                    {
                        filteredContours.Add(contours[i]);
                        filteredIndices.Add(i);
                    }
                }

                // 출력 이미지 준비
                if (Contourparameters.DrawOnOriginal && inputImage.Channels() >= 3)
                {
                    inputImage.CopyTo(outputImage);
                }
                else
                {
                    // 이진 이미지를 3채널로 변환하여 컬러 컨투어 그리기
                    Cv2.CvtColor(binaryImage, outputImage, ColorConversionCodes.GRAY2BGR);
                }

                // 컨투어 그리기
                Random random = new Random();
                for (int i = 0; i < filteredContours.Count; i++)
                {
                    Scalar color;

                    switch (Contourparameters.ColorMode)
                    {
                        case ContourParameters.ContourColorMode.Random:
                            color = new Scalar(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
                            break;
                        case ContourParameters.ContourColorMode.SizeBased:
                            double area = Cv2.ContourArea(filteredContours[i]);
                            double normalizedArea = Math.Min(area / 10000.0, 1.0); // 10000을 최대값으로 정규화
                            color = new Scalar(
                                (int)(255 * (1 - normalizedArea)), // Blue
                                (int)(255 * normalizedArea),       // Green  
                                (int)(128 + 127 * normalizedArea)  // Red
                            );
                            break;
                        case ContourParameters.ContourColorMode.Fixed:
                        default:
                            var fixedColor = Contourparameters.FixedColor;
                            color = new Scalar(fixedColor.B, fixedColor.G, fixedColor.R); // BGR 순서
                            break;
                    }

                    // 컨투어 그리기
                    Cv2.DrawContours(outputImage, filteredContours, i, color, Contourparameters.Thickness);

                    // 컨투어 번호 표시
                    if (Contourparameters.ShowContourNumbers)
                    {
                        var moments = Cv2.Moments(filteredContours[i]);
                        if (moments.M00 != 0)
                        {
                            var centroid = new Point(
                                (int)(moments.M10 / moments.M00),
                                (int)(moments.M01 / moments.M00)
                            );
                            Cv2.PutText(outputImage, i.ToString(), centroid,
                                       HersheyFonts.HersheySimplex, 0.8,
                                       new Scalar(255, 255, 255), 2);
                        }
                    }
                }

                output = CurrentProcessingNode.GetHashCode().ToString();
                ImageDataManager.RegisterImage(output, outputImage);
                ImageKeySelected?.Invoke(output, CurrentProcessingNode.Name);

                string resultMessage = $"컨투어 검출 완료. 총 {contours.Length}개 검출, {filteredContours.Count}개 표시";
                FeedbackInfo?.Invoke(resultMessage, CurrentProcessingNode, FeedbackType.Information, outputImage.Clone(), false);
            }
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"컨투어 검출 처리 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
        }
    }
    #endregion
    #region Moments
    // --- 모멘트 중심좌표 계산 파라미터 클래스 ---
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MomentsParameters
    {
        public enum AnalysisMode
        {
            [Description("전체 이미지의 중심좌표 계산")]
            WholeImage,
            [Description("컨투어별 중심좌표 계산")]
            Contours,
            [Description("연결된 구성요소별 중심좌표 계산")]
            ConnectedComponents
        }

        public enum VisualizationMode
        {
            [Description("원본 이미지에 표시")]
            OnOriginal,
            [Description("이진 이미지에 표시")]
            OnBinary,
            [Description("새로운 검은 배경에 표시")]
            OnBlack
        }

        [Description("분석 모드를 선택합니다.")]
        public AnalysisMode Mode { get; set; } = AnalysisMode.Contours;

        [Description("시각화 모드를 선택합니다.")]
        public VisualizationMode Visualization { get; set; } = VisualizationMode.OnOriginal;

        private double _minContourArea = 50.0;
        [Description("최소 컨투어 면적입니다. 이 값보다 작은 객체는 무시됩니다.")]
        public double MinContourArea
        {
            get => _minContourArea;
            set => _minContourArea = Math.Max(0, value);
        }

        private double _maxContourArea = double.MaxValue;
        [Description("최대 컨투어 면적입니다. 이 값보다 큰 객체는 무시됩니다.")]
        public double MaxContourArea
        {
            get => _maxContourArea;
            set => _maxContourArea = Math.Max(_minContourArea, value);
        }

        [Description("중심점 마커의 크기입니다.")]
        public int MarkerSize { get; set; } = 10;

        [Description("중심점 마커의 두께입니다.")]
        public int MarkerThickness { get; set; } = 2;

        [Description("중심점 마커 색상 (BGR 순서)")]
        public System.Drawing.Color MarkerColor { get; set; } = System.Drawing.Color.Red;

        [Description("십자가 마커를 사용할지 여부입니다.")]
        public bool UseCrossMarker { get; set; } = true;

        [Description("원형 마커를 사용할지 여부입니다.")]
        public bool UseCircleMarker { get; set; } = false;

        [Description("좌표값을 텍스트로 표시할지 여부입니다.")]
        public bool ShowCoordinates { get; set; } = true;

        [Description("객체 번호를 표시할지 여부입니다.")]
        public bool ShowObjectNumbers { get; set; } = true;

        [Description("면적 정보를 표시할지 여부입니다.")]
        public bool ShowArea { get; set; } = false;

        [Description("텍스트 폰트 크기입니다.")]
        public double FontScale { get; set; } = 0.6;

        [Description("텍스트 색상 (BGR 순서)")]
        public System.Drawing.Color TextColor { get; set; } = System.Drawing.Color.Yellow;

        public override string ToString()
        {
            return $"Mode:{Mode}, Area:{MinContourArea:F0}~{(MaxContourArea == double.MaxValue ? "∞" : MaxContourArea.ToString("F0"))}, Marker:{MarkerSize}px";
        }
    }

    // --- 모멘트 중심좌표 계산 노드 ---
    [Node(name: "Moments", menu: "분석/측정", description: "이미지 모멘트를 계산하여 객체의 중심좌표를 구합니다.")]
    public void ApplyMomentsAnalysis(string input, MomentsParameters Momentsparameters, out string output)
    {
        output = null;

        // 먼저 입력 키가 유효한지 확인합니다.
        if (string.IsNullOrEmpty(input))
        {
            FeedbackInfo?.Invoke("입력으로 전달된 이미지 키가 비어있습니다. 이전 노드의 실행 결과를 확인하세요.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        Mat inputImage = ImageDataManager.GetImage(input);
        if (inputImage == null || inputImage.Empty())
        {
            FeedbackInfo?.Invoke($"이미지 키 '{input}'에 해당하는 이미지를 찾을 수 없습니다.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        try
        {
            using (Mat binaryImage = new Mat())
            using (Mat outputImage = new Mat())
            {
                // 이진 이미지 준비
                if (inputImage.Channels() >= 3)
                {
                    using (Mat grayImage = new Mat())
                    {
                        Cv2.CvtColor(inputImage, grayImage, ColorConversionCodes.BGR2GRAY);
                        // 이미 이진화된 이미지인지 확인
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
                    inputImage.CopyTo(binaryImage);
                }

                // 출력 이미지 준비
                switch (Momentsparameters.Visualization)
                {
                    case MomentsParameters.VisualizationMode.OnOriginal:
                        if (inputImage.Channels() >= 3)
                            inputImage.CopyTo(outputImage);
                        else
                            Cv2.CvtColor(inputImage, outputImage, ColorConversionCodes.GRAY2BGR);
                        break;
                    case MomentsParameters.VisualizationMode.OnBinary:
                        Cv2.CvtColor(binaryImage, outputImage, ColorConversionCodes.GRAY2BGR);
                        break;
                    case MomentsParameters.VisualizationMode.OnBlack:
                        {
                            using (var blackImage = new Mat(inputImage.Size(), MatType.CV_8UC3, Scalar.All(0)))
                            {
                                blackImage.CopyTo(outputImage);
                            }
                        }
                        break;
                }

                List<Point2f> centroids = new List<Point2f>();
                List<double> areas = new List<double>();
                int objectCount = 0;

                // 분석 모드에 따른 처리
                switch (Momentsparameters.Mode)
                {
                    case MomentsParameters.AnalysisMode.WholeImage:
                        {
                            // 전체 이미지의 모멘트 계산
                            var moments = Cv2.Moments(binaryImage, true);
                            if (moments.M00 != 0)
                            {
                                float cx = (float)(moments.M10 / moments.M00);
                                float cy = (float)(moments.M01 / moments.M00);
                                centroids.Add(new Point2f(cx, cy));
                                areas.Add(moments.M00);
                                objectCount = 1;
                            }
                        }
                        break;

                    case MomentsParameters.AnalysisMode.Contours:
                        {
                            // 컨투어별 모멘트 계산
                            Point[][] contours;
                            HierarchyIndex[] hierarchy;
                            Cv2.FindContours(binaryImage, out contours, out hierarchy,
                                            RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                            foreach (var contour in contours)
                            {
                                double area = Cv2.ContourArea(contour);
                                if (area >= Momentsparameters.MinContourArea && area <= Momentsparameters.MaxContourArea)
                                {
                                    var moments = Cv2.Moments(contour);
                                    if (moments.M00 != 0)
                                    {
                                        float cx = (float)(moments.M10 / moments.M00);
                                        float cy = (float)(moments.M01 / moments.M00);
                                        centroids.Add(new Point2f(cx, cy));
                                        areas.Add(area);
                                        objectCount++;
                                    }
                                }
                            }
                        }
                        break;

                    case MomentsParameters.AnalysisMode.ConnectedComponents:
                        {
                            // 연결된 구성요소별 모멘트 계산
                            using (Mat labels = new Mat())
                            using (Mat stats = new Mat())
                            using (Mat centroids_cc = new Mat())
                            {
                                int numLabels = Cv2.ConnectedComponentsWithStats(binaryImage, labels, stats, centroids_cc, PixelConnectivity.Connectivity8);

                                for (int i = 1; i < numLabels; i++) // 0은 배경이므로 제외
                                {
                                    double area = stats.At<int>(i, (int)ConnectedComponentsTypes.Area);
                                    if (area >= Momentsparameters.MinContourArea && area <= Momentsparameters.MaxContourArea)
                                    {
                                        double cx = centroids_cc.At<double>(i, 0);
                                        double cy = centroids_cc.At<double>(i, 1);
                                        centroids.Add(new Point2f((float)cx, (float)cy));
                                        areas.Add(area);
                                        objectCount++;
                                    }
                                }
                            }
                        }
                        break;
                }

                // 마커 색상 설정
                var markerColor = Momentsparameters.MarkerColor;
                Scalar markerScalar = new Scalar(markerColor.B, markerColor.G, markerColor.R);
                var textColor = Momentsparameters.TextColor;
                Scalar textScalar = new Scalar(textColor.B, textColor.G, textColor.R);

                // 중심점 그리기 및 정보 표시
                for (int i = 0; i < centroids.Count; i++)
                {
                    Point center = new Point((int)centroids[i].X, (int)centroids[i].Y);

                    // 십자가 마커
                    if (Momentsparameters.UseCrossMarker)
                    {
                        int size = Momentsparameters.MarkerSize;
                        Cv2.Line(outputImage,
                                new Point(center.X - size, center.Y),
                                new Point(center.X + size, center.Y),
                                markerScalar, Momentsparameters.MarkerThickness);
                        Cv2.Line(outputImage,
                                new Point(center.X, center.Y - size),
                                new Point(center.X, center.Y + size),
                                markerScalar, Momentsparameters.MarkerThickness);
                    }

                    // 원형 마커
                    if (Momentsparameters.UseCircleMarker)
                    {
                        Cv2.Circle(outputImage, center, Momentsparameters.MarkerSize / 2,
                                  markerScalar, Momentsparameters.MarkerThickness);
                    }

                    // 텍스트 정보 표시
                    List<string> textLines = new List<string>();

                    if (Momentsparameters.ShowObjectNumbers)
                        textLines.Add($"#{i + 1}");

                    if (Momentsparameters.ShowCoordinates)
                        textLines.Add($"({center.X}, {center.Y})");

                    if (Momentsparameters.ShowArea)
                        textLines.Add($"A:{areas[i]:F0}");

                    // 텍스트 그리기
                    Point textPos = new Point(center.X + Momentsparameters.MarkerSize + 5, center.Y - 10);
                    for (int j = 0; j < textLines.Count; j++)
                    {
                        Point linePos = new Point(textPos.X, textPos.Y + j * 20);
                        Cv2.PutText(outputImage, textLines[j], linePos,
                                   HersheyFonts.HersheySimplex, Momentsparameters.FontScale,
                                   textScalar, 1);
                    }
                }

                output = CurrentProcessingNode.GetHashCode().ToString();
                ImageDataManager.RegisterImage(output, outputImage);
                ImageKeySelected?.Invoke(output, CurrentProcessingNode.Name);

                string resultMessage = $"모멘트 분석 완료. {objectCount}개 객체의 중심좌표 계산됨";
                FeedbackInfo?.Invoke(resultMessage, CurrentProcessingNode, FeedbackType.Information, outputImage.Clone(), false);
            }
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"모멘트 분석 처리 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
        }
    }
    #endregion
}
