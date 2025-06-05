// MyNodesContext.Preprocessing.cs
using System;
using System.ComponentModel; // GaussianBlurParameters의 DescriptionAttribute는 유지
using NodeEditor;
using OpenCvSharp;

public partial class MyNodesContext
{
    // GaussianBlurParameters 클래스는 그대로 유지 (내부의 DescriptionAttribute는 PropertyGrid 등에 사용될 수 있음)
    [Serializable]
    [System.ComponentModel.TypeConverter(typeof(ExpandableObjectConverter))]
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

        public GaussianBlurParameters() { }
    }

    [Node(name: "가우시안 블러", menu: "전처리/필터", description: "이미지에 가우시안 블러를 적용합니다.")]
    // [return: NodeOutput(name: "블러 처리된 이미지", type: typeof(Mat), description:"가우시안 블러가 적용된 이미지입니다.")] // <--- 이 줄 제거 또는 주석 처리

    public Mat ApplyGaussianBlur(
        Mat inputImage,
        GaussianBlurParameters parameters)
    {
        // ... (초반 null 체크 로직) ...

        Mat outputImage = new Mat();
        try
        {
            Size ksize = new Size(parameters.KernelWidth, parameters.KernelHeight);
            Cv2.GaussianBlur(inputImage, outputImage, ksize, parameters.SigmaX, parameters.SigmaY, BorderTypes.Default);
            FeedbackInfo?.Invoke("가우시안 블러 적용 완료.", CurrentProcessingNode, FeedbackType.Information, outputImage.Clone(), false);
        }
        catch (OpenCvSharpException cvEx) // OpenCV 관련 예외
        {
            // 수정된 부분: cvEx.ErrMsg와 cvEx.Code 대신 cvEx.Message 사용
            FeedbackInfo?.Invoke($"가우시안 블러 OpenCV 오류: {cvEx.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
            // 만약 특정 오류 코드가 필요하다면 cvEx.HResult (COM 에러 코드) 등을 확인해 볼 수 있으나,
            // cvEx.Message에 이미 충분한 정보가 있을 것입니다.
            // 예: FeedbackInfo?.Invoke($"가우시안 블러 OpenCV 오류: {cvEx.Message} (HResult: {cvEx.HResult})", CurrentProcessingNode, FeedbackType.Error, null, true);

            outputImage?.Dispose();
            return inputImage.Clone(); // 오류 시 원본 복제본 반환
        }
        catch (Exception ex) // 일반적인 .NET 예외
        {
            FeedbackInfo?.Invoke($"가우시안 블러 처리 중 일반 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
            outputImage?.Dispose();
            return inputImage.Clone(); // 오류 시 원본 복제본 반환
        }
        return outputImage; // 성공 시 처리된 이미지 반환
    }
}