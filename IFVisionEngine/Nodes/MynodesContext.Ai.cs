using System;
using System.Collections.Generic;
using NodeEditor;
using OpenCvSharp;
using IFVisionEngine.Manager;
using Microsoft.ML.OnnxRuntime;
using System.Linq;

public partial class MyNodesContext
{
    #region onnx

    [Node(name: "FindOutlier", menu: "딥러닝", description: "이상점을 측정하고 결과를 도출합니다.", Width = 200)]
    public void FindOutlier(string input,out string output)
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
            OnnxInspector inspector = new OnnxInspector("best.onnx");
            Mat results = inspector.Inspect(inputImage);
            output = CurrentProcessingNode.GetHashCode().ToString();
            ImageDataManager.RegisterImage(output, results);
            ImageKeySelected?.Invoke(output, CurrentProcessingNode.Name);
            FeedbackInfo?.Invoke("딥 러닝 적용 완료.", CurrentProcessingNode, FeedbackType.Information, results.Clone(), false);
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"Onnx 파일 처리 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
        }
    }
    #endregion


}
public class OnnxInspector
{
    private InferenceSession session;

    public OnnxInspector(string modelPath)
    {
        session = new InferenceSession(modelPath);
    }
    // 이미지 전처리 및 텐서 변환 메서드
    public Mat Inspect(Mat originalImage)
    {
        // 원본 이미지 보존을 위해 복사본 생성
        Mat image = originalImage.Clone();

        // 1. 이미지 전처리
        image = ResizeWithAspectRatio(image, 1920);
        image.ConvertTo(image, MatType.CV_32FC3, 1.0 / 255.0); // 정규화

        // 2. 텐서 변환
        float[] inputData = new float[3 * 1920 * 1920];
        image.GetArray(out Vec3f[] pixels);
        int idx = 0;
        for (int c = 0; c < 3; c++)
            for (int h = 0; h < 1920; h++)
                for (int w = 0; w < 1920; w++)
                    inputData[idx++] = pixels[h * 1920 + w][c];

        // 3. 추론 실행
        var inputTensor = OrtValue.CreateTensorValueFromMemory(inputData,
            new long[] { 1, 3, 1920, 1920 });
        var inputs = new Dictionary<string, OrtValue> { { "images", inputTensor } };
        var outputs = session.Run(new RunOptions(), inputs, session.OutputNames);
        float[] result = outputs[0].GetTensorDataAsSpan<float>().ToArray();

        // 4. 원본 이미지에 불량 표시 
        Mat resultImage = DrawDefects(originalImage, result);

        // 5. 메모리 정리
        image.Dispose();

        return resultImage;
    }
    public Mat DrawDefects(Mat originalImage, float[] results, float threshold = 0.1f)
    {
        Mat result = originalImage.Clone();
        int numDetections = 75600;
        string[] classNames = { "Bright spot", "Dark spot", "Bright ring", "Dark ring", "Bright corner", "Dark corner" };

        // 원본 크기로 변환할 스케일 계산
        int modelSize = 1920;
        float aspectRatio = (float)originalImage.Width / originalImage.Height;

        int actualWidth, actualHeight;
        int offsetX = 0, offsetY = 0;

        if (aspectRatio > 1.0f) // 가로가 더 긴 경우
        {
            actualWidth = modelSize;
            actualHeight = (int)(modelSize / aspectRatio);
            offsetY = (modelSize - actualHeight) / 2;  // 상하 패딩
        }
        else // 세로가 더 긴 경우
        {
            actualHeight = modelSize;
            actualWidth = (int)(modelSize * aspectRatio);
            offsetX = (modelSize - actualWidth) / 2;   // 좌우 패딩
        }

        float scaleX = (float)originalImage.Width / actualWidth;
        float scaleY = (float)originalImage.Height / actualHeight;

        var detections = new List<(float score, int classId, float x, float y, float w, float h)>();

        // 불량 검출
        for (int i = 0; i < numDetections; i++)
        {
            float centerX = results[i];
            float centerY = results[numDetections + i];
            float width = results[numDetections * 2 + i];
            float height = results[numDetections * 3 + i];

            float maxScore = float.MinValue;
            int bestClass = -1;

            for (int c = 0; c < 6; c++)
            {
                int scoreIndex = numDetections * (4 + c) + i;
                if (scoreIndex < results.Length)
                {
                    float score = results[scoreIndex];
                    if (score > maxScore)
                    {
                        maxScore = score;
                        bestClass = c;
                    }
                }
            }

            if (maxScore >= threshold && centerX >= 0 && centerY >= 0 && centerX <= 1920 && centerY <= 1920)
            {
                detections.Add((maxScore, bestClass, centerX, centerY, width, height));
            }
        }

        // 점수 순 정렬
        detections.Sort((a, b) => b.score.CompareTo(a.score));

        // 바운딩 박스 그리기 (동일 좌표는 최고 점수만)
        var finalDetections = new List<(float score, int classId, int x, int y, int w, int h, int index)>();
        int overlapThreshold = 20; // 20픽셀 이내면 같은 위치로 간주

        for (int i = 0; i < detections.Count; i++)
        {
            var detection = detections[i];

            // 패딩 오프셋 제거 후 좌표 변환
            float adjustedX = detection.x - offsetX;
            float adjustedY = detection.y - offsetY;

            // 실제 이미지 영역 내에 있는지 확인
            if (adjustedX >= 0 && adjustedY >= 0 && adjustedX <= actualWidth && adjustedY <= actualHeight)
            {
                // 한 번만 계산해서 최종 좌표 구하기
                int finalX = (int)((adjustedX - detection.w / 2) * scaleX);
                int finalY = (int)((adjustedY - detection.h / 2) * scaleY);
                int finalW = (int)(detection.w * scaleX);
                int finalH = (int)(detection.h * scaleY);

                // 기존 검출과 겹치는지 확인
                bool isOverlapping = false;
                for (int j = 0; j < finalDetections.Count; j++)
                {
                    var existing = finalDetections[j];

                    // 중심점 거리 계산
                    int centerX1 = finalX + finalW / 2;
                    int centerY1 = finalY + finalH / 2;
                    int centerX2 = existing.x + existing.w / 2;
                    int centerY2 = existing.y + existing.h / 2;

                    double distance = Math.Sqrt(Math.Pow(centerX1 - centerX2, 2) + Math.Pow(centerY1 - centerY2, 2));

                    if (distance < overlapThreshold)
                    {
                        isOverlapping = true;

                        // 더 높은 점수면 교체
                        if (detection.score > existing.score)
                        {
                            finalDetections[j] = (detection.score, detection.classId, finalX, finalY, finalW, finalH, i);
                        }
                        break;
                    }
                }

                // 겹치지 않으면 새로 추가
                if (!isOverlapping)
                {
                    finalDetections.Add((detection.score, detection.classId, finalX, finalY, finalW, finalH, i));
                }
            }
        }
        foreach (var kvp in finalDetections.OrderByDescending(d => d.score))
        {
            int x = kvp.x;
            int y = kvp.y;
            int w = kvp.w;
            int h = kvp.h;

            // 이미지 범위 내로 제한
            x = Math.Max(0, Math.Min(x, originalImage.Width - w));
            y = Math.Max(0, Math.Min(y, originalImage.Height - h));

            // 빨간 박스 그리기
            Cv2.Rectangle(result, new Rect(x, y, w, h), new Scalar(0, 0, 255), 2);

            // 라벨 그리기
            string label = $"{classNames[kvp.classId]} {kvp.score}";
            Cv2.PutText(result, label, new Point(x, Math.Max(15, y - 5)), HersheyFonts.HersheySimplex, 0.5, new Scalar(0, 0, 255), 1);

            Console.WriteLine($"{kvp.index + 1}. {classNames[kvp.classId]} - 위치({x}, {y}) 점수({kvp.score:E2})");
        }

        Console.WriteLine($"총 {detections.Count}개 불량 표시");
        return result;
    }
    // 이미지를 비율을 유지하며 리사이즈하고, 검은색 배경에 중앙 배치하는 함수
    public Mat ResizeWithAspectRatio(Mat image, int targetSize = 1920)
    {
        int originalWidth = image.Width;
        int originalHeight = image.Height;

        // 비율 계산 (긴 쪽을 기준으로)
        float scale = (float)targetSize / Math.Max(originalWidth, originalHeight);

        // 새로운 크기 계산 (비율 유지)
        int newWidth = (int)(originalWidth * scale);
        int newHeight = (int)(originalHeight * scale);

        // 1단계: 비율 유지하며 리사이즈
        Mat resized = new Mat();
        Cv2.Resize(image, resized, new Size(newWidth, newHeight));

        // 2단계: 검은색 배경에 중앙 배치 (Letterbox)
        Mat letterboxed = new Mat(targetSize, targetSize, MatType.CV_8UC3, Scalar.Black);

        // 중앙 배치 위치 계산
        int offsetX = (targetSize - newWidth) / 2;
        int offsetY = (targetSize - newHeight) / 2;

        // ROI 설정하여 중앙에 배치
        Rect roi = new Rect(offsetX, offsetY, newWidth, newHeight);
        resized.CopyTo(letterboxed[roi]);

        Console.WriteLine($"원본: {originalWidth}x{originalHeight} → 리사이즈: {newWidth}x{newHeight} → 최종: {targetSize}x{targetSize}");

        resized.Dispose();
        return letterboxed;
    }
}