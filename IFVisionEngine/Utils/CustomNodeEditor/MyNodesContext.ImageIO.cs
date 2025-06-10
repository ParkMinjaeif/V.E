// MyNodesContext.ImageIO.cs
using System;
using System.ComponentModel;
using IFVisionEngine.Manager;
using NodeEditor;
using OpenCvSharp; // Mat, Cv2, ImreadModes 등

public partial class MyNodesContext
{
    /// <summary>
    /// 파일 경로를 받아 이미지를 로드하고, 결과를 'out' 매개변수를 통해 출력 핀으로 내보냅니다.
    /// </summary>
    /// <param name="filePath">불러올 이미지 파일의 전체 경로입니다.</param>
    /// <param name="outputImage">성공 시 로드된 Mat 객체, 실패 시 null이 할당되는 출력 매개변수입니다.</param>
    [Node(name: "이미지 로드", menu: "이미지 IO", description: "파일 시스템에서 이미지를 불러옵니다.")]
    public void LoadImageFromFile(string filePath, out Mat outputImage)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            FeedbackInfo?.Invoke("파일 경로가 지정되지 않았습니다.", CurrentProcessingNode, FeedbackType.Error, null, true);
            outputImage = null; // 출력 매개변수를 null로 설정하고 종료
            return;
        }

        if (!System.IO.File.Exists(filePath))
        {
            FeedbackInfo?.Invoke($"파일을 찾을 수 없습니다: {filePath}", CurrentProcessingNode, FeedbackType.Error, null, true);
            outputImage = null; // 출력 매개변수를 null로 설정하고 종료
            return;
        }

        try
        {
            // using 문을 사용하여 로컬 Mat 객체가 확실히 해제되도록 합니다.
            using (Mat image = Cv2.ImRead(filePath, ImreadModes.Color))
            {
                if (image.Empty())
                {
                    FeedbackInfo?.Invoke($"이미지 로드 실패 (파일은 존재하나 읽을 수 없음): {filePath}", CurrentProcessingNode, FeedbackType.Error, null, true);
                    outputImage = null;
                    return;
                }

                FeedbackInfo?.Invoke($"이미지 로드 성공: {filePath} ({image.Width}x{image.Height})", CurrentProcessingNode, FeedbackType.Information, image.Clone(), false);
                Console.WriteLine("이미지 로드 성공");
                AppUIManager.ucImageControler.DisplayImage(image);

                // *** 가장 중요한 수정 사항 ***
                // 원본 객체가 아닌, 완전히 독립된 복제본을 출력으로 내보냅니다.
                outputImage = image.Clone();
            }
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"이미지 로드 중 오류 발생: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
            outputImage = null;
        }
    }
}


