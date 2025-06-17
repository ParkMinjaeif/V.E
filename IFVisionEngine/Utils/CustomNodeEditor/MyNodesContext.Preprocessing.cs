using System;
using System.ComponentModel;
using NodeEditor;
using OpenCvSharp;

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
    [Node(name: "가우시안 블러", menu: "전처리/필터", description: "이미지에 가우시안 블러를 적용합니다.")]
    public void ApplyGaussianBlur(Mat inputImage, GaussianBlurParameters parameters, out Mat outputImage)
    {
        if (inputImage == null || inputImage.Empty())
        {
            FeedbackInfo?.Invoke("입력 이미지가 없습니다.", CurrentProcessingNode, FeedbackType.Error, null, true);
            outputImage = null;
            return;
        }

        outputImage = new Mat();
        try
        {
            Size ksize = new Size(parameters.KernelWidth, parameters.KernelHeight);
            Cv2.GaussianBlur(inputImage, outputImage, ksize, parameters.SigmaX, parameters.SigmaY, BorderTypes.Default);
            FeedbackInfo?.Invoke("가우시안 블러 적용 완료.", CurrentProcessingNode, FeedbackType.Information, outputImage.Clone(), false);
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"가우시안 블러 처리 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
            outputImage?.Dispose();
            outputImage = null;
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
    [Node(name: "이진화", menu: "전처리/필터", description: "이미지를 흑과 백으로 변환(이진화)합니다.")]
    public void ApplyBinarization(Mat inputImage, BinarizationParameters parameters, out Mat outputImage)
    {
        if (inputImage == null || inputImage.Empty())
        {
            FeedbackInfo?.Invoke("입력 이미지가 없습니다.", CurrentProcessingNode, FeedbackType.Error, null, true);
            outputImage = null;
            return;
        }

        outputImage = new Mat();
        try
        {
            // 이진화는 단일 채널(그레이스케일) 이미지에서 수행되어야 합니다.
            // 입력 이미지가 컬러이면 자동으로 그레이스케일로 변환합니다.
            using (Mat grayImage = new Mat())
            {
                if (inputImage.Channels() >= 3)
                {
                    Cv2.CvtColor(inputImage, grayImage, ColorConversionCodes.BGR2GRAY);
                }
                else
                {
                    inputImage.CopyTo(grayImage);
                }

                // 선택된 이진화 방식과 Otsu 옵션을 조합하여 최종 ThresholdType을 결정합니다.
                ThresholdTypes thresholdType = (ThresholdTypes)parameters.Method;
                if (parameters.UseOtsu)
                {
                    thresholdType |= ThresholdTypes.Otsu;
                }

                // 이진화를 수행합니다.
                Cv2.Threshold(grayImage, outputImage, parameters.ThresholdValue, parameters.MaxValue, thresholdType);

                FeedbackInfo?.Invoke("이진화 적용 완료.", CurrentProcessingNode, FeedbackType.Information, outputImage.Clone(), false);
            }
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"이진화 처리 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
            outputImage?.Dispose();
            outputImage = null;
        }
    }
    #endregion
}
