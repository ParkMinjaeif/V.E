// MyNodesContext.ImageIO.cs
using System;
// using System.ComponentModel; // DescriptionAttribute는 NodeInput/Output에 사용했으므로 일단 주석
using NodeEditor;
using OpenCvSharp; // Mat, Cv2, ImreadModes 등

public partial class MyNodesContext
{
    [Node(name: "이미지 로드", menu: "이미지 IO", description: "파일 시스템에서 이미지를 불러옵니다.")]
    // [return: NodeOutput(name:"출력 이미지", type: typeof(Mat), description:"불러온 이미지 Mat 객체입니다.")] // <--- 이 줄 제거 또는 주석 처리
    public Mat LoadImageFromFile(
        /*[NodeInput(name:"파일 경로", type: typeof(string), description:"불러올 이미지 파일의 전체 경로입니다.")]*/ string filePath) // <--- 어트리뷰트 제거 또는 주석 처리
    {
        if (string.IsNullOrEmpty(filePath))
        {
            FeedbackInfo?.Invoke("파일 경로가 지정되지 않았습니다.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return null;
        }

        if (!System.IO.File.Exists(filePath))
        {
            FeedbackInfo?.Invoke($"파일을 찾을 수 없습니다: {filePath}", CurrentProcessingNode, FeedbackType.Error, null, true);
            return null;
        }

        Mat image = null;
        try
        {
            image = Cv2.ImRead(filePath, ImreadModes.Color);
            if (image.Empty())
            {
                FeedbackInfo?.Invoke($"이미지 로드 실패 (파일은 존재하나 읽을 수 없음): {filePath}", CurrentProcessingNode, FeedbackType.Error, null, true);
                return null;
            }
            FeedbackInfo?.Invoke($"이미지 로드 성공: {filePath} ({image.Width}x{image.Height})", CurrentProcessingNode, FeedbackType.Information, image.Clone(), false);
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"이미지 로드 중 오류 발생: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
            image?.Dispose();
            return null;
        }
        return image;
    }

    [Node(name: "이미지 표시 요청", menu: "이미지 IO", description: "입력된 이미지를 UI에 표시하도록 요청합니다.")]
    public void RequestDisplayImage(
        /*[NodeInput(name: "표시할 이미지", type: typeof(Mat))]*/ Mat imageToDisplay, // <--- 어트리뷰트 제거 또는 주석 처리
        /*[NodeInput(name: "창 제목", type: typeof(string), defaultValue:"결과 이미지")]*/ string windowTitle = "결과 이미지") // <--- 어트리뷰트 제거 또는 주석 처리
                                                                                                                  // defaultValue는 NodeAttribute 자체에 있거나 라이브러리가 다르게 처리할 수 있습니다.
    {
        if (imageToDisplay == null || imageToDisplay.Empty())
        {
            FeedbackInfo?.Invoke("표시할 이미지가 없습니다.", CurrentProcessingNode, FeedbackType.Warning, null, false);
            return;
        }
        FeedbackInfo?.Invoke(windowTitle, CurrentProcessingNode, FeedbackType.Information, imageToDisplay.Clone(), true);
    }
}