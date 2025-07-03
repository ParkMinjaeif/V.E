using System;
using System.ComponentModel;
using NodeEditor;
using OpenCvSharp;
using IFVisionEngine.Manager; // ImageDataManager를 사용하기 위해 추가
using IFVisionEngine.UIComponents.Dialogs;
using System.Windows.Forms;
using static MyNodesContext;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Sunny.UI;
using IFVisionEngine.UIComponents.Managers;

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
    [Node(name: "GaussianBlur", menu: "전처리/필터", description: "이미지에 가우시안 블러를 적용합니다.", Width = 200)]
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
}
