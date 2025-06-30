using System;
using IFVisionEngine.Manager;
using NodeEditor;
using OpenCvSharp;

public partial class MyNodesContext
{
    /// <summary>
    /// *** 수정된 부분 ***
    /// 이미지를 로드하고, ImageDataManager에 등록한 뒤, 해당 이미지에 접근할 수 있는 '키'를 반환(return)합니다.
    /// </summary>
    /// <param name="filePath">불러올 이미지 파일의 전체 경로입니다.</param>
    /// <returns>ImageDataManager에 등록된 이미지의 고유 키입니다. 실패 시 null을 반환합니다.</returns>
    [Node(name: "이미지 로드", menu: "이미지 IO", description: "파일 시스템에서 이미지를 불러옵니다.", width: 200)]
    public void LoadImageFromFile(string filePath, out string outputImageKey)
    {
        // out 매개변수 초기화
        outputImageKey = null;

        if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
        {
            FeedbackInfo?.Invoke("파일 경로가 유효하지 않습니다: " + filePath, CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        try
        {
            using (Mat image = Cv2.ImRead(filePath, ImreadModes.Color))
            {
                if (image.Empty())
                {
                    FeedbackInfo?.Invoke("이미지 로드 실패: " + filePath, CurrentProcessingNode, FeedbackType.Error, null, true);
                    return;
                }

                // 1. 현재 노드의 고유 ID(해시코드)를 키로 사용합니다.
                outputImageKey = CurrentProcessingNode.GetHashCode().ToString();

                // 2. 생성된 이미지를 ImageDataManager에 등록합니다.
                ImageDataManager.RegisterImage(outputImageKey, image);
                ImageKeySelected?.Invoke(outputImageKey, CurrentProcessingNode.Name);
                // 3. 실행 성공 신호를 보냅니다.
                FeedbackInfo?.Invoke($"이미지 로드 성공: {filePath} ({image.Width}x{image.Height})", CurrentProcessingNode, FeedbackType.Information, image.Clone(), false);
            }
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"이미지 로드 중 오류 발생: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
            outputImageKey = null;
        }
    }
}
