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
        [Description("컨투어를 시각적으로 표시할지 여부를 설정합니다.")]
        public bool ShowVisualization { get; set; } = true;

        [Description("컨투어 좌표 데이터를 출력할지 여부를 설정합니다.")]
        public bool OutputContourData { get; set; } = false;

        [Description("좌표 데이터를 JSON 형태로 출력할지 여부를 설정합니다.")]
        public bool OutputAsJson { get; set; } = true;
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
            return $"Mode:{RetrievalMode}, Method:{ApproximationMethod}, " +
                   $"Area:{MinContourArea:F0}~{(MaxContourArea == double.MaxValue ? "∞" : MaxContourArea.ToString("F0"))}, " +
                   $"Visual:{ShowVisualization}, Output:{OutputContourData}";
        }
    }

    [Node(name: "Contour", menu: "분석/검출", description: "이미지에서 컨투어를 검출하고 시각화합니다.", Width = 220)]
    public void ApplyContourDetection(string input, ContourParameters Contourparameters,
                                out string output, out string contourData)
    {
        output = null;
        contourData = null;

        // 입력 검증은 기존과 동일...
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
                // 1. 이진 이미지 준비 (기존과 동일)
                if (inputImage.Channels() >= 3)
                {
                    using (Mat grayImage = new Mat())
                    {
                        Cv2.CvtColor(inputImage, grayImage, ColorConversionCodes.BGR2GRAY);
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

                // 2. 컨투어 검출 (기존과 동일)
                Point[][] contours;
                HierarchyIndex[] hierarchy;
                Cv2.FindContours(binaryImage, out contours, out hierarchy,
                                (RetrievalModes)Contourparameters.RetrievalMode,
                                (ContourApproximationModes)Contourparameters.ApproximationMethod);

                // 3. 면적 기준으로 컨투어 필터링 (기존과 동일)
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

                // === 4. 컨투어 데이터 출력 (새로 추가) ===
                if (Contourparameters.OutputContourData)
                {
                    contourData = ExportContourData(filteredContours, Contourparameters.OutputAsJson);
                    var nodeContext = CurrentProcessingNode.GetNodeContext() as DynamicNodeContext;
                    if (nodeContext != null)
                    {
                        nodeContext["contourData"] = contourData;  // moments 출력 소켓에 값 직접 설정
                        Console.WriteLine($"[모멘트 노드] DynamicNodeContext에 moments 설정: '{contourData}'");
                    }
                }

                // === 5. 시각화 처리 (조건부로 변경) ===
                if (Contourparameters.ShowVisualization)
                {
                    // 출력 이미지 준비
                    if (Contourparameters.DrawOnOriginal && inputImage.Channels() >= 3)
                    {
                        inputImage.CopyTo(outputImage);
                    }
                    else
                    {
                        Cv2.CvtColor(binaryImage, outputImage, ColorConversionCodes.GRAY2BGR);
                    }

                    // 컨투어 그리기 (기존 로직과 동일)
                    DrawContours(outputImage, filteredContours, Contourparameters);
                }
                else
                {
                    // 시각화가 꺼져있으면 원본 이미지 그대로 출력
                    if (inputImage.Channels() >= 3)
                    {
                        inputImage.CopyTo(outputImage);
                    }
                    else
                    {
                        Cv2.CvtColor(inputImage, outputImage, ColorConversionCodes.GRAY2BGR);
                    }
                }

                // 6. 결과 등록
                output = CurrentProcessingNode.GetHashCode().ToString();
                ImageDataManager.RegisterImage(output, outputImage);
                ImageKeySelected?.Invoke(output, CurrentProcessingNode.Name);

                string resultMessage = $"컨투어 검출 완료. 총 {contours.Length}개 검출, {filteredContours.Count}개 필터링됨";
                if (Contourparameters.OutputContourData)
                {
                    resultMessage += $", 데이터 출력: {(Contourparameters.OutputAsJson ? "JSON" : "텍스트")}";
                }
                FeedbackInfo?.Invoke(resultMessage, CurrentProcessingNode, FeedbackType.Information, outputImage.Clone(), false);
            }
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"컨투어 검출 처리 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
        }
    }
    /// <summary>
    /// 컨투어 그리기 로직을 별도 메서드로 분리
    /// </summary>
    private void DrawContours(Mat outputImage, List<Point[]> filteredContours, ContourParameters parameters)
    {
        Random random = new Random();
        for (int i = 0; i < filteredContours.Count; i++)
        {
            Scalar color;

            switch (parameters.ColorMode)
            {
                case ContourParameters.ContourColorMode.Random:
                    color = new Scalar(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
                    break;
                case ContourParameters.ContourColorMode.SizeBased:
                    double area = Cv2.ContourArea(filteredContours[i]);
                    double normalizedArea = Math.Min(area / 10000.0, 1.0);
                    color = new Scalar(
                        (int)(255 * (1 - normalizedArea)),
                        (int)(255 * normalizedArea),
                        (int)(128 + 127 * normalizedArea)
                    );
                    break;
                case ContourParameters.ContourColorMode.Fixed:
                default:
                    var fixedColor = parameters.FixedColor;
                    color = new Scalar(fixedColor.B, fixedColor.G, fixedColor.R);
                    break;
            }

            // 컨투어 그리기
            Cv2.DrawContours(outputImage, filteredContours, i, color, parameters.Thickness);

            // 컨투어 번호 표시
            if (parameters.ShowContourNumbers)
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
    }

    /// <summary>
    /// 컨투어 데이터를 문자열로 내보내기
    /// </summary>
    private string ExportContourData(List<Point[]> contours, bool asJson)
    {
        if (asJson)
        {
            return ExportContourDataAsJson(contours);
        }
        else
        {
            return ExportContourDataAsText(contours);
        }
    }

    /// <summary>
    /// 컨투어 데이터를 JSON 형태로 내보내기
    /// </summary>
    private string ExportContourDataAsJson(List<Point[]> contours)
    {
        var contourData = new
        {
            ContourCount = contours.Count,
            Contours = contours.Select((contour, index) => new
            {
                Index = index,
                PointCount = contour.Length,
                Area = Cv2.ContourArea(contour),
                Perimeter = Cv2.ArcLength(contour, true),
                BoundingRect = Cv2.BoundingRect(contour),
                Points = contour.Select(p => new { X = p.X, Y = p.Y }).ToArray()
            }).ToArray()
        };

        // 간단한 JSON 직렬화 (Newtonsoft.Json 없이)
        var json = new StringBuilder();
        json.AppendLine("{");
        json.AppendLine($"  \"ContourCount\": {contourData.ContourCount},");
        json.AppendLine("  \"Contours\": [");

        for (int i = 0; i < contourData.Contours.Length; i++)
        {
            var contour = contourData.Contours[i];
            json.AppendLine("    {");
            json.AppendLine($"      \"Index\": {contour.Index},");
            json.AppendLine($"      \"PointCount\": {contour.PointCount},");
            json.AppendLine($"      \"Area\": {contour.Area:F2},");
            json.AppendLine($"      \"Perimeter\": {contour.Perimeter:F2},");
            json.AppendLine($"      \"BoundingRect\": {{\"X\": {contour.BoundingRect.X}, \"Y\": {contour.BoundingRect.Y}, \"Width\": {contour.BoundingRect.Width}, \"Height\": {contour.BoundingRect.Height}}},");
            json.AppendLine("      \"Points\": [");

            for (int j = 0; j < contour.Points.Length; j++)
            {
                var point = contour.Points[j];
                json.Append($"        {{\"X\": {point.X}, \"Y\": {point.Y}}}");
                if (j < contour.Points.Length - 1) json.Append(",");
                json.AppendLine();
            }

            json.AppendLine("      ]");
            json.Append("    }");
            if (i < contourData.Contours.Length - 1) json.Append(",");
            json.AppendLine();
        }

        json.AppendLine("  ]");
        json.AppendLine("}");

        return json.ToString();
    }

    /// <summary>
    /// 컨투어 데이터를 텍스트 형태로 내보내기
    /// </summary>
    private string ExportContourDataAsText(List<Point[]> contours)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Total Contours: {contours.Count}");
        sb.AppendLine("=" + new string('=', 50));

        for (int i = 0; i < contours.Count; i++)
        {
            var contour = contours[i];
            var area = Cv2.ContourArea(contour);
            var perimeter = Cv2.ArcLength(contour, true);
            var boundingRect = Cv2.BoundingRect(contour);

            sb.AppendLine($"Contour #{i}:");
            sb.AppendLine($"  Points: {contour.Length}");
            sb.AppendLine($"  Area: {area:F2}");
            sb.AppendLine($"  Perimeter: {perimeter:F2}");
            sb.AppendLine($"  Bounding Rect: X={boundingRect.X}, Y={boundingRect.Y}, W={boundingRect.Width}, H={boundingRect.Height}");
            sb.AppendLine("  Coordinates:");

            for (int j = 0; j < contour.Length; j++)
            {
                sb.Append($"    ({contour[j].X},{contour[j].Y})");
                if ((j + 1) % 5 == 0) sb.AppendLine(); // 5개씩 줄바꿈
            }
            if (contour.Length % 5 != 0) sb.AppendLine();
            sb.AppendLine();
        }

        return sb.ToString();
    }
    #endregion
    #region Moments
    // --- 모멘트 분석 파라미터 클래스 ---
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MomentsParameters
    {
        private int _binaryThreshold = 127;
        [Description("이진화에 사용할 임계값입니다. (0-255)")]
        public int BinaryThreshold
        {
            get => _binaryThreshold;
            set => _binaryThreshold = Math.Max(0, Math.Min(255, value));
        }

        [Description("객체의 중심점(무게중심)을 표시할지 여부를 설정합니다.")]
        public bool ShowCentroid { get; set; } = true;

        [Description("객체의 면적을 표시할지 여부를 설정합니다.")]
        public bool ShowArea { get; set; } = true;

        [Description("객체의 주축 방향각을 표시할지 여부를 설정합니다.")]
        public bool ShowOrientation { get; set; } = false;

        [Description("객체의 경계박스를 표시할지 여부를 설정합니다.")]
        public bool ShowBoundingBox { get; set; } = true;

        [Description("객체의 편심률(원형에서 벗어난 정도)을 표시할지 여부를 설정합니다.")]
        public bool ShowEccentricity { get; set; } = false;

        [Description("시각화에 사용할 색상입니다.")]
        public System.Drawing.Color DrawColor { get; set; } = System.Drawing.Color.Red;

        private int _lineThickness = 2;
        [Description("선과 도형을 그릴 때 사용할 두께입니다. (1-10)")]
        public int LineThickness
        {
            get => _lineThickness;
            set => _lineThickness = Math.Max(1, Math.Min(10, value));
        }

        public override string ToString()
        {
            var features = new List<string>();
            if (ShowCentroid) features.Add("중심점");
            if (ShowArea) features.Add("면적");
            if (ShowOrientation) features.Add("방향각");
            if (ShowBoundingBox) features.Add("경계박스");
            if (ShowEccentricity) features.Add("편심률");

            return $"임계값:{BinaryThreshold}, 표시:[{string.Join(",", features)}]";
        }
    }

    // === 모멘트 분석 노드 - 중심좌표 반환 기능 추가 ===

    // --- 모멘트 분석 노드 ---
    [Node(name: "Moments", menu: "분석/형태", description: "이미지 모멘트를 계산하여 객체의 기하학적 특성을 분석합니다.", Width = 220)]
    public void ApplyMomentsAnalysis(string input, MomentsParameters Momentsparameters,
                                   out string output,             // 시각화된 이미지
                                   out string moments) // 중심좌표 데이터
    {
        output = null;
        moments = null;

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
                // 1. 이진화 처리
                if (inputImage.Channels() >= 3)
                {
                    using (Mat grayImage = new Mat())
                    {
                        Cv2.CvtColor(inputImage, grayImage, ColorConversionCodes.BGR2GRAY);
                        Cv2.Threshold(grayImage, binaryImage, Momentsparameters.BinaryThreshold, 255, ThresholdTypes.Binary);
                    }
                }
                else
                {
                    Cv2.Threshold(inputImage, binaryImage, Momentsparameters.BinaryThreshold, 255, ThresholdTypes.Binary);
                }

                // 2. 출력 이미지 준비 (원본을 복사하여 컬러로 표시)
                if (inputImage.Channels() >= 3)
                {
                    inputImage.CopyTo(outputImage);
                }
                else
                {
                    Cv2.CvtColor(inputImage, outputImage, ColorConversionCodes.GRAY2BGR);
                }

                // 3. 컨투어 찾기 (모멘트 계산을 위해)
                Point[][] contours;
                HierarchyIndex[] hierarchy;
                Cv2.FindContours(binaryImage, out contours, out hierarchy,
                                RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                Scalar color = new Scalar(Momentsparameters.DrawColor.B, Momentsparameters.DrawColor.G, Momentsparameters.DrawColor.R); // BGR 순서

                int validObjectCount = 0;
                double totalArea = 0;
                var results = new List<string>(); // 분석 결과 저장
                var centroids = new List<Point>(); // === 중심점 좌표 저장용 ===

                // 4. 각 컨투어에 대해 모멘트 분석
                for (int i = 0; i < contours.Length; i++)
                {
                    if (contours[i].Length < 5) continue; // 너무 작은 컨투어 제외

                    // 모멘트 계산
                    var varmoments = Cv2.Moments(contours[i]);
                    if (varmoments.M00 == 0) continue; // 면적이 0인 경우 제외

                    validObjectCount++;
                    totalArea += varmoments.M00;

                    // 중심점 계산
                    var centroidX = (int)(varmoments.M10 / varmoments.M00);
                    var centroidY = (int)(varmoments.M01 / varmoments.M00);
                    var centroid = new Point(centroidX, centroidY);

                    // === 중심점 좌표 저장 ===
                    centroids.Add(centroid);

                    var objectInfo = new List<string>();
                    objectInfo.Add($"객체 {validObjectCount}");

                    // 중심점 표시
                    if (Momentsparameters.ShowCentroid)
                    {
                        Cv2.Circle(outputImage, centroid, 5, color, Momentsparameters.LineThickness);
                        Cv2.PutText(outputImage, $"({centroidX},{centroidY})",
                                   new Point(centroidX + 10, centroidY - 10),
                                   HersheyFonts.HersheySimplex, 0.5, color, 1);
                        objectInfo.Add($"중심점: ({centroidX}, {centroidY})");
                    }

                    // 면적 표시
                    if (Momentsparameters.ShowArea)
                    {
                        double area = varmoments.M00;
                        Cv2.PutText(outputImage, $"Area: {area:F0}",
                                   new Point(centroidX + 10, centroidY + 10),
                                   HersheyFonts.HersheySimplex, 0.5, color, 1);
                        objectInfo.Add($"면적: {area:F0} 픽셀²");
                    }

                    // 방향각 계산 및 표시
                    if (Momentsparameters.ShowOrientation)
                    {
                        // 중심 모멘트 계산
                        double mu20 = varmoments.M20 - (varmoments.M10 * varmoments.M10) / varmoments.M00;
                        double mu02 = varmoments.M02 - (varmoments.M01 * varmoments.M01) / varmoments.M00;
                        double mu11 = varmoments.M11 - (varmoments.M10 * varmoments.M01) / varmoments.M00;

                        if (Math.Abs(mu20 - mu02) > 1e-6 || Math.Abs(mu11) > 1e-6)
                        {
                            double angle = 0.5 * Math.Atan2(2 * mu11, mu20 - mu02) * 180.0 / Math.PI;

                            // 방향선 그리기 (길이 50픽셀)
                            int lineLength = 50;
                            double radians = angle * Math.PI / 180.0;
                            var endPoint = new Point(
                                centroidX + (int)(lineLength * Math.Cos(radians)),
                                centroidY + (int)(lineLength * Math.Sin(radians))
                            );

                            Cv2.Line(outputImage, centroid, endPoint, color, Momentsparameters.LineThickness);
                            Cv2.PutText(outputImage, $"{angle:F1}°",
                                       new Point(centroidX + 10, centroidY + 30),
                                       HersheyFonts.HersheySimplex, 0.5, color, 1);
                            objectInfo.Add($"방향각: {angle:F1}°");
                        }
                    }

                    // 경계박스 표시
                    if (Momentsparameters.ShowBoundingBox)
                    {
                        var boundingRect = Cv2.BoundingRect(contours[i]);
                        Cv2.Rectangle(outputImage, boundingRect, color, Momentsparameters.LineThickness);

                        double aspectRatio = (double)boundingRect.Width / boundingRect.Height;
                        Cv2.PutText(outputImage, $"W:{boundingRect.Width} H:{boundingRect.Height}",
                                   new Point(boundingRect.X, boundingRect.Y - 10),
                                   HersheyFonts.HersheySimplex, 0.4, color, 1);
                        Cv2.PutText(outputImage, $"Ratio:{aspectRatio:F2}",
                                   new Point(boundingRect.X, boundingRect.Y - 25),
                                   HersheyFonts.HersheySimplex, 0.4, color, 1);
                        objectInfo.Add($"경계박스: {boundingRect.Width}×{boundingRect.Height} (비율: {aspectRatio:F2})");
                    }

                    // 편심률 계산 및 표시
                    if (Momentsparameters.ShowEccentricity)
                    {
                        // 중심 모멘트 계산
                        double mu20 = varmoments.M20 - (varmoments.M10 * varmoments.M10) / varmoments.M00;
                        double mu02 = varmoments.M02 - (varmoments.M01 * varmoments.M01) / varmoments.M00;
                        double mu11 = varmoments.M11 - (varmoments.M10 * varmoments.M01) / varmoments.M00;

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
                                               new Point(centroidX + 10, centroidY + 50),
                                               HersheyFonts.HersheySimplex, 0.5, color, 1);
                                    objectInfo.Add($"편심률: {eccentricity:F3} (0=원형, 1=직선)");
                                }
                            }
                        }
                    }

                    results.Add(string.Join(", ", objectInfo));
                }

                // === 중심좌표 데이터 생성 ===
                moments = GenerateCentroidCoordinatesData(centroids);
                var nodeContext = CurrentProcessingNode.GetNodeContext() as DynamicNodeContext;
                if (nodeContext != null)
                {
                    nodeContext["moments"] = moments;  // moments 출력 소켓에 값 직접 설정
                    Console.WriteLine($"[모멘트 노드] DynamicNodeContext에 moments 설정: '{moments}'");
                }
                output = CurrentProcessingNode.GetHashCode().ToString();
                ImageDataManager.RegisterImage(output, outputImage);
                ImageKeySelected?.Invoke(output, CurrentProcessingNode.Name);

                string resultMessage = $"모멘트 분석 완료. 객체 수: {validObjectCount}, 총 면적: {totalArea:F0} 픽셀²";
                if (centroids.Count > 0)
                {
                    resultMessage += $", 중심점 {centroids.Count}개 추출됨";
                }
                if (results.Count > 0)
                {
                    resultMessage += "\n분석 결과:\n" + string.Join("\n", results);
                }

                FeedbackInfo?.Invoke(resultMessage, CurrentProcessingNode, FeedbackType.Information, outputImage.Clone(), false);
            }
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"모멘트 분석 처리 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
        }
    }

    /// <summary>
    /// 중심점 좌표들을 문자열 데이터로 변환합니다.
    /// </summary>
    private string GenerateCentroidCoordinatesData(List<Point> centroids)
    {
        if (centroids == null || centroids.Count == 0)
        {
            return ""; // 중심점이 없으면 빈 문자열 반환
        }

        var coordinatesList = new List<string>();

        for (int i = 0; i < centroids.Count; i++)
        {
            // 각 중심점을 "X,Y" 형태로 저장
            coordinatesList.Add($"{centroids[i].X},{centroids[i].Y}");
        }

        // 여러 중심점을 "|" 구분자로 연결
        // 형태: "X1,Y1|X2,Y2|X3,Y3"
        return string.Join("|", coordinatesList);
    }

    /// <summary>
    /// 다른 노드에서 중심좌표 데이터를 파싱하여 사용할 수 있는 헬퍼 메서드
    /// </summary>
    public static List<Point> ParseCentroidCoordinates(string centroidData)
    {
        var centroids = new List<Point>();

        if (string.IsNullOrEmpty(centroidData))
            return centroids;

        try
        {
            // "|" 구분자로 각 좌표 분리
            string[] coordinatePairs = centroidData.Split('|');

            foreach (string pair in coordinatePairs)
            {
                if (string.IsNullOrEmpty(pair)) continue;

                // "X,Y" 형태를 ","로 분리
                string[] xy = pair.Split(',');
                if (xy.Length == 2)
                {
                    if (int.TryParse(xy[0], out int x) && int.TryParse(xy[1], out int y))
                    {
                        centroids.Add(new Point(x, y));
                    }
                }
            }
        }
        catch (Exception)
        {
            // 파싱 실패 시 빈 리스트 반환
            centroids.Clear();
        }

        return centroids;
    }
    #endregion
    #region RadialLines
    // --- 방사선 분석 파라미터 클래스 ---
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RadialLinesParameters
    {
        // 중심점 모드 열거형
        public enum CenterMode
        {
            [Description("이미지 중심점 사용")]
            ImageCenter,
            [Description("자동 무게중심 찾기 (가장 큰 객체)")]
            AutoCentroid,
            [Description("수동 좌표 입력")]
            Manual,
            [Description("외부 좌표 데이터 사용 (모멘트 노드 등에서)")]
            ExternalCoordinates,
            [Description("최대 밝기 지점")]
            MaxBrightness
        }

        // 방사 범위 모드 열거형
        public enum RangeMode
        {
            [Description("고정 길이")]
            FixedLength,
            [Description("이미지 경계까지")]
            ImageBoundary,
            [Description("경계선 감지까지 (이진화 기반)")]
            EdgeDetection,
            [Description("밝기 변화 감지까지")]
            BrightnessChange
        }

        // 선 스타일 열거형
        public enum LineStyle
        {
            [Description("실선")]
            Solid,
            [Description("점선")]
            Dotted,
            [Description("파선")]
            Dashed
        }

        [Description("시각화를 활성화할지 여부를 설정합니다.")]
        public bool ShowVisualization { get; set; } = true;

        [Description("중심점을 결정하는 방법을 선택합니다.")]
        public CenterMode CenterMethod { get; set; } = CenterMode.ImageCenter;

        private int _manualX = 0;
        [Description("수동 모드일 때 사용할 X 좌표입니다.")]
        public int ManualX
        {
            get => _manualX;
            set => _manualX = Math.Max(0, value);
        }

        private int _manualY = 0;
        [Description("수동 모드일 때 사용할 Y 좌표입니다.")]
        public int ManualY
        {
            get => _manualY;
            set => _manualY = Math.Max(0, value);
        }

        [Description("방사선의 범위를 결정하는 방법을 선택합니다.")]
        public RangeMode RangeMethod { get; set; } = RangeMode.EdgeDetection;

        private int _fixedLength = 100;
        [Description("고정 길이 모드일 때 사용할 선의 길이입니다.")]
        public int FixedLength
        {
            get => _fixedLength;
            set => _fixedLength = Math.Max(10, Math.Min(1000, value));
        }

        private int _lineCount = 8;
        [Description("그릴 방사선의 개수입니다. (4-360)")]
        public int LineCount
        {
            get => _lineCount;
            set => _lineCount = Math.Max(4, Math.Min(360, value));
        }

        private int _startAngle = 0;
        [Description("첫 번째 선의 시작 각도입니다. (0-359도)")]
        public int StartAngle
        {
            get => _startAngle;
            set => _startAngle = Math.Max(0, Math.Min(359, value));
        }

        [Description("선을 그릴 때 사용할 색상입니다.")]
        public System.Drawing.Color LineColor { get; set; } = System.Drawing.Color.Red;

        private int _lineThickness = 2;
        [Description("선의 두께입니다. (1-10)")]
        public int LineThickness
        {
            get => _lineThickness;
            set => _lineThickness = Math.Max(1, Math.Min(10, value));
        }

        [Description("선의 스타일을 선택합니다.")]
        public LineStyle Style { get; set; } = LineStyle.Solid;

        [Description("중심점에 원을 표시할지 여부를 설정합니다.")]
        public bool ShowCenter { get; set; } = true;

        [Description("각 선에 각도를 표시할지 여부를 설정합니다.")]
        public bool ShowAngles { get; set; } = false;

        [Description("각 선에 거리를 표시할지 여부를 설정합니다.")]
        public bool ShowDistances { get; set; } = false;

        private int _binaryThreshold = 127;
        [Description("경계 감지 시 사용할 이진화 임계값입니다. (0-255)")]
        public int BinaryThreshold
        {
            get => _binaryThreshold;
            set => _binaryThreshold = Math.Max(0, Math.Min(255, value));
        }

        private int _brightnessThreshold = 50;
        [Description("밝기 변화 감지 시 사용할 임계값입니다. (0-255)")]
        public int BrightnessThreshold
        {
            get => _brightnessThreshold;
            set => _brightnessThreshold = Math.Max(0, Math.Min(255, value));
        }

        [Description("방사선 길이 데이터를 출력할지 여부를 설정합니다.")]
        public bool OutputLengthData { get; set; } = true;

        public override string ToString()
        {
            return $"중심:{CenterMethod}, 범위:{RangeMethod}, 선개수:{LineCount}, 시각화:{ShowVisualization}";
        }
    }

    // --- 방사선 분석 노드 ---
    [Node(name: "RadialLines", menu: "분석/형태", description: "중심점에서 방사형으로 선을 그려 객체의 형태를 분석합니다.", Width = 250)]
    public void ApplyRadialLines(string input,
                               RadialLinesParameters RadialLinesparameters,
                               string moments, // 외부에서 받은 좌표 (모멘트 노드 등에서)
                               out string output,          // 시각화된 이미지
                               out string radialLengths)   // 방사선 길이 데이터
    {
        Console.WriteLine($"방사선 노드 입력 moments: '{moments}'");
        Console.WriteLine($"CenterMethod: {RadialLinesparameters.CenterMethod}");

        output = null;
        radialLengths = null;
        // 입력 검증
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
                // 1. 이진화 준비 (경계 감지용)
                PrepareBinaryImage(inputImage, binaryImage, RadialLinesparameters.BinaryThreshold);

                // 2. 출력 이미지 준비
                if (RadialLinesparameters.ShowVisualization)
                {
                    if (inputImage.Channels() >= 3)
                    {
                        inputImage.CopyTo(outputImage);
                    }
                    else
                    {
                        Cv2.CvtColor(inputImage, outputImage, ColorConversionCodes.GRAY2BGR);
                    }
                }
                else
                {
                    // 시각화 꺼져있으면 원본만
                    if (inputImage.Channels() >= 3)
                    {
                        inputImage.CopyTo(outputImage);
                    }
                    else
                    {
                        Cv2.CvtColor(inputImage, outputImage, ColorConversionCodes.GRAY2BGR);
                    }
                }

                // 3. 중심점 계산
                var centers = GetRadialCenters(inputImage, moments, RadialLinesparameters);

                if (centers.Count == 0)
                {
                    FeedbackInfo?.Invoke("중심점을 찾을 수 없습니다. 파라미터를 확인하세요.", CurrentProcessingNode, FeedbackType.Error, null, true);
                    return;
                }

                // 4. 각 중심점에 대해 방사선 그리기 및 길이 측정
                var allLengthData = new List<string>();

                for (int centerIndex = 0; centerIndex < centers.Count; centerIndex++)
                {
                    Point center = centers[centerIndex];
                    var lengths = new List<double>();

                    // 방사선 그리기 및 길이 측정
                    for (int i = 0; i < RadialLinesparameters.LineCount; i++)
                    {
                        double angle = RadialLinesparameters.StartAngle + (i * 360.0 / RadialLinesparameters.LineCount);

                        // 끝점 계산
                        Point endPoint = CalculateRadialEndPoint(inputImage, binaryImage, center, angle, RadialLinesparameters);

                        // 길이 계산
                        double length = Math.Sqrt(Math.Pow(endPoint.X - center.X, 2) + Math.Pow(endPoint.Y - center.Y, 2));
                        lengths.Add(length);

                        // 시각화
                        if (RadialLinesparameters.ShowVisualization)
                        {
                            DrawRadialLine(outputImage, center, endPoint, angle, length, i, RadialLinesparameters);
                        }
                    }

                    // 중심점 표시
                    if (RadialLinesparameters.ShowVisualization && RadialLinesparameters.ShowCenter)
                    {
                        DrawCenterPoint(outputImage, center, centerIndex, RadialLinesparameters);
                    }

                    // 길이 데이터 저장 (중심점별로)
                    allLengthData.Add($"Center{centerIndex}:" + string.Join(",", lengths.Select(l => l.ToString("F2"))));
                }

                // 5. 방사선 길이 데이터 생성
                if (RadialLinesparameters.OutputLengthData)
                {
                    radialLengths = string.Join("|", allLengthData);
                    var nodeContext = CurrentProcessingNode.GetNodeContext() as DynamicNodeContext;
                    if (nodeContext != null)
                    {
                        nodeContext["radialLengths"] = radialLengths;  // moments 출력 소켓에 값 직접 설정
                        Console.WriteLine($"[모멘트 노드] DynamicNodeContext에 moments 설정: '{radialLengths}'");
                    }
                }

                // 6. 시각화 꺼져있을 때 정보 표시
                if (!RadialLinesparameters.ShowVisualization)
                {
                    Cv2.PutText(outputImage, $"RadialLines: {centers.Count} centers, {RadialLinesparameters.LineCount} lines each",
                               new Point(10, 30), HersheyFonts.HersheySimplex, 0.7, new Scalar(0, 255, 0), 2);
                    Cv2.PutText(outputImage, "(Visualization OFF)",
                               new Point(10, 55), HersheyFonts.HersheySimplex, 0.6, new Scalar(0, 255, 0), 2);
                }

                // 7. 결과 등록
                output = CurrentProcessingNode.GetHashCode().ToString();
                ImageDataManager.RegisterImage(output, outputImage);
                ImageKeySelected?.Invoke(output, CurrentProcessingNode.Name);

                string resultMessage = $"방사선 분석 완료. 중심점 {centers.Count}개, 각각 {RadialLinesparameters.LineCount}개 방사선 생성";
                if (RadialLinesparameters.OutputLengthData)
                {
                    resultMessage += ", 길이 데이터 출력됨";
                }

                FeedbackInfo?.Invoke(resultMessage, CurrentProcessingNode, FeedbackType.Information, outputImage.Clone(), false);
            }
        }
        catch (Exception ex)
        {
            FeedbackInfo?.Invoke($"방사선 분석 처리 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);
        }
    }

    // === 보조 메서드들 ===

    /// <summary>
    /// 이진화 이미지 준비
    /// </summary>
    private void PrepareBinaryImage(Mat inputImage, Mat binaryImage, int threshold)
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
    /// 중심점들을 계산합니다
    /// </summary>
    private List<Point> GetRadialCenters(Mat image, string externalCoordinates, RadialLinesParameters parameters)
    {
        var centers = new List<Point>();

        switch (parameters.CenterMethod)
        {
            case RadialLinesParameters.CenterMode.ImageCenter:
                centers.Add(new Point(image.Width / 2, image.Height / 2));
                break;

            case RadialLinesParameters.CenterMode.AutoCentroid:
                var centroid = FindAutoCentroid(image, parameters.BinaryThreshold);
                if (centroid.HasValue)
                {
                    centers.Add(centroid.Value);
                }
                else
                {
                    // 무게중심을 찾지 못하면 이미지 중심 사용
                    centers.Add(new Point(image.Width / 2, image.Height / 2));
                }
                break;

            case RadialLinesParameters.CenterMode.Manual:
                centers.Add(new Point(
                    Math.Min(parameters.ManualX, image.Width - 1),
                    Math.Min(parameters.ManualY, image.Height - 1)
                ));
                break;

            case RadialLinesParameters.CenterMode.ExternalCoordinates:
                if (!string.IsNullOrEmpty(externalCoordinates))
                {
                    // 모멘트 노드에서 온 좌표 파싱 (형식: "X1,Y1|X2,Y2|...")
                    var externalCenters = ParseExternalCoordinates(externalCoordinates);
                    if (externalCenters.Count > 0)
                    {
                        centers.AddRange(externalCenters);
                    }
                    else
                    {
                        // 파싱 실패 시 이미지 중심 사용
                        centers.Add(new Point(image.Width / 2, image.Height / 2));
                    }
                }
                else
                {
                    // 외부 좌표가 없으면 이미지 중심 사용
                    centers.Add(new Point(image.Width / 2, image.Height / 2));
                }
                break;

            case RadialLinesParameters.CenterMode.MaxBrightness:
                centers.Add(FindMaxBrightnessPoint(image));
                break;

            default:
                centers.Add(new Point(image.Width / 2, image.Height / 2));
                break;
        }

        return centers;
    }

    /// <summary>
    /// 외부 좌표 데이터를 파싱합니다
    /// </summary>
    private List<Point> ParseExternalCoordinates(string coordinatesData)
    {
        var points = new List<Point>();

        try
        {
            if (string.IsNullOrEmpty(coordinatesData)) return points;

            // "|" 구분자로 각 좌표 분리
            string[] coordinatePairs = coordinatesData.Split('|');

            foreach (string pair in coordinatePairs)
            {
                if (string.IsNullOrEmpty(pair)) continue;

                string[] xy = pair.Split(',');
                if (xy.Length == 2)
                {
                    if (int.TryParse(xy[0], out int x) && int.TryParse(xy[1], out int y))
                    {
                        points.Add(new Point(x, y));
                    }
                }
            }

        }
        catch (Exception)
        {
            // 파싱 실패 시 빈 리스트 반환
            points.Clear();
        }

        return points;
    }

    /// <summary>
    /// 자동으로 가장 큰 객체의 무게중심을 찾습니다
    /// </summary>
    private Point? FindAutoCentroid(Mat image, int threshold)
    {
        try
        {
            using (Mat binaryImage = new Mat())
            {
                // 이진화
                if (image.Channels() >= 3)
                {
                    using (Mat grayImage = new Mat())
                    {
                        Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);
                        Cv2.Threshold(grayImage, binaryImage, threshold, 255, ThresholdTypes.Binary);
                    }
                }
                else
                {
                    Cv2.Threshold(image, binaryImage, threshold, 255, ThresholdTypes.Binary);
                }

                // 컨투어 찾기
                Point[][] contours;
                HierarchyIndex[] hierarchy;
                Cv2.FindContours(binaryImage, out contours, out hierarchy,
                                RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                if (contours.Length == 0) return null;

                // 가장 큰 컨투어 찾기
                double maxArea = 0;
                Point? bestCentroid = null;

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
                        bestCentroid = new Point(centroidX, centroidY);
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
    /// 최대 밝기 지점을 찾습니다
    /// </summary>
    private Point FindMaxBrightnessPoint(Mat image)
    {
        using (Mat grayImage = new Mat())
        {
            if (image.Channels() >= 3)
                Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);
            else
                image.CopyTo(grayImage);

            Point maxLoc;
            Cv2.MinMaxLoc(grayImage, out double minVal, out double maxVal, out Point minLoc, out maxLoc);
            return maxLoc;
        }
    }

    /// <summary>
    /// 방사선의 끝점을 계산합니다
    /// </summary>
    private Point CalculateRadialEndPoint(Mat originalImage, Mat binaryImage, Point center, double angle, RadialLinesParameters parameters)
    {
        double radians = angle * Math.PI / 180.0;
        double dx = Math.Cos(radians);
        double dy = Math.Sin(radians);

        switch (parameters.RangeMethod)
        {
            case RadialLinesParameters.RangeMode.FixedLength:
                return new Point(
                    center.X + (int)(parameters.FixedLength * dx),
                    center.Y + (int)(parameters.FixedLength * dy)
                );

            case RadialLinesParameters.RangeMode.ImageBoundary:
                return CalculateImageBoundaryPoint(originalImage, center, dx, dy);

            case RadialLinesParameters.RangeMode.EdgeDetection:
                return CalculateEdgeDetectionPoint(binaryImage, center, dx, dy);

            case RadialLinesParameters.RangeMode.BrightnessChange:
                return CalculateBrightnessChangePoint(originalImage, center, dx, dy, parameters.BrightnessThreshold);

            default:
                return CalculateImageBoundaryPoint(originalImage, center, dx, dy);
        }
    }

    /// <summary>
    /// 이미지 경계까지의 끝점을 계산합니다
    /// </summary>
    private Point CalculateImageBoundaryPoint(Mat image, Point center, double dx, double dy)
    {
        double tMax = double.MaxValue;

        // 각 경계와의 교점 계산
        if (dx > 0) tMax = Math.Min(tMax, (image.Width - 1 - center.X) / dx);
        else if (dx < 0) tMax = Math.Min(tMax, -center.X / dx);

        if (dy > 0) tMax = Math.Min(tMax, (image.Height - 1 - center.Y) / dy);
        else if (dy < 0) tMax = Math.Min(tMax, -center.Y / dy);

        return new Point(
            center.X + (int)(tMax * dx),
            center.Y + (int)(tMax * dy)
        );
    }

    /// <summary>
    /// 개선된 EdgeDetection - 시작점 색상과 다른 색상을 만나면 멈추는 적응형 로직
    /// </summary>
    private Point CalculateEdgeDetectionPoint(Mat binaryImage, Point center, double dx, double dy, int colorDifferenceThreshold = 50)
    {
        try
        {
            Console.WriteLine($"[적응형EdgeDetection] 시작 - 중심점: ({center.X}, {center.Y}), 방향: ({dx:F3}, {dy:F3})");

            // 1. 중심점의 픽셀값을 기준값으로 설정
            byte basePixelValue = binaryImage.At<byte>(center.Y, center.X);
            Console.WriteLine($"[적응형EdgeDetection] 기준 픽셀값: {basePixelValue}");

            int maxDistance = Math.Max(binaryImage.Width, binaryImage.Height);
            Console.WriteLine($"[적응형EdgeDetection] 최대 탐색 거리: {maxDistance}");

            for (int t = 1; t < maxDistance; t++)
            {
                int x = center.X + (int)(t * dx);
                int y = center.Y + (int)(t * dy);

                // 이미지 경계 검사
                if (x < 0 || x >= binaryImage.Width || y < 0 || y >= binaryImage.Height)
                {
                    Point boundaryPoint = new Point(
                        Math.Max(0, Math.Min(binaryImage.Width - 1, x)),
                        Math.Max(0, Math.Min(binaryImage.Height - 1, y))
                    );
                    Console.WriteLine($"[적응형EdgeDetection] 이미지 경계 도달 at t={t}: ({boundaryPoint.X}, {boundaryPoint.Y})");
                    return boundaryPoint;
                }

                // 현재 위치의 픽셀값 확인
                byte currentPixelValue = binaryImage.At<byte>(y, x);

                // 기준값과 현재값의 차이 계산
                int colorDifference = Math.Abs(currentPixelValue - basePixelValue);

                // 처음 몇 개 픽셀의 값을 로그로 출력 (디버깅용)
                if (t <= 10)
                {
                    Console.WriteLine($"[적응형EdgeDetection] t={t}, 위치:({x},{y}), 픽셀값:{currentPixelValue}, 차이:{colorDifference}");
                }

                // 🔥 핵심 로직: 기준값과 충분히 다른 색상을 만나면 경계로 판단
                if (colorDifference >= colorDifferenceThreshold)
                {
                    Console.WriteLine($"[적응형EdgeDetection] 색상 변화 감지 at t={t}: ({x}, {y}) - 기준:{basePixelValue} vs 현재:{currentPixelValue}, 차이:{colorDifference}");
                    return new Point(x, y);
                }
            }

            // 최대 거리까지 도달 (색상 변화가 없는 경우)
            Point maxPoint = new Point(
                center.X + (int)(maxDistance * dx),
                center.Y + (int)(maxDistance * dy)
            );
            Console.WriteLine($"[적응형EdgeDetection] 최대 거리 도달 (색상 변화 없음): ({maxPoint.X}, {maxPoint.Y})");
            return maxPoint;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[적응형EdgeDetection] 예외 발생: {ex.Message}");
            // 오류 시 이미지 경계까지 계산
            return CalculateImageBoundaryPoint(binaryImage, center, dx, dy);
        }
    }


    /// <summary>
    /// 밝기 변화 지점까지의 끝점을 계산합니다
    /// </summary>
    private Point CalculateBrightnessChangePoint(Mat image, Point center, double dx, double dy, int threshold)
    {
        try
        {
            using (Mat grayImage = new Mat())
            {
                // 그레이스케일 변환
                if (image.Channels() >= 3)
                {
                    Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);
                }
                else
                {
                    image.CopyTo(grayImage);
                }

                // 중심점의 밝기값
                byte centerBrightness = grayImage.At<byte>(center.Y, center.X);
                int maxDistance = Math.Max(image.Width, image.Height);

                for (int t = 1; t < maxDistance; t++)
                {
                    int x = center.X + (int)(t * dx);
                    int y = center.Y + (int)(t * dy);

                    // 이미지 경계 검사
                    if (x < 0 || x >= image.Width || y < 0 || y >= image.Height)
                    {
                        return new Point(
                            Math.Max(0, Math.Min(image.Width - 1, x)),
                            Math.Max(0, Math.Min(image.Height - 1, y))
                        );
                    }

                    // 밝기 차이 검사
                    byte currentBrightness = grayImage.At<byte>(y, x);
                    int brightnessDiff = Math.Abs(centerBrightness - currentBrightness);

                    if (brightnessDiff > threshold)
                    {
                        return new Point(x, y);
                    }
                }

                return new Point(
                    center.X + (int)(maxDistance * dx),
                    center.Y + (int)(maxDistance * dy)
                );
            }
        }
        catch
        {
            return CalculateImageBoundaryPoint(image, center, dx, dy);
        }
    }

    /// <summary>
    /// 방사선을 그립니다
    /// </summary>
    private void DrawRadialLine(Mat image, Point center, Point endPoint, double angle, double length, int lineIndex, RadialLinesParameters parameters)
    {
        Scalar color = new Scalar(parameters.LineColor.B, parameters.LineColor.G, parameters.LineColor.R);

        // 선 그리기
        if (parameters.Style == RadialLinesParameters.LineStyle.Solid)
        {
            Cv2.Line(image, center, endPoint, color, parameters.LineThickness);
        }
        else
        {
            DrawStyledLine(image, center, endPoint, color, parameters);
        }

        // 각도 표시
        if (parameters.ShowAngles)
        {
            var textPoint = new Point(
                center.X + (int)(30 * Math.Cos(angle * Math.PI / 180)),
                center.Y + (int)(30 * Math.Sin(angle * Math.PI / 180))
            );
            Cv2.PutText(image, $"{angle:F0}°", textPoint,
                       HersheyFonts.HersheySimplex, 0.4, color, 1);
        }

        // 거리 표시
        if (parameters.ShowDistances)
        {
            var textPoint = new Point((center.X + endPoint.X) / 2, (center.Y + endPoint.Y) / 2);
            Cv2.PutText(image, $"{length:F0}", textPoint,
                       HersheyFonts.HersheySimplex, 0.4, color, 1);
        }
    }

    /// <summary>
    /// 스타일이 적용된 선을 그립니다
    /// </summary>
    private void DrawStyledLine(Mat image, Point start, Point end, Scalar color, RadialLinesParameters parameters)
    {
        // 점선이나 파선 구현
        double distance = Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
        int segments = parameters.Style == RadialLinesParameters.LineStyle.Dotted ? (int)distance / 10 : (int)distance / 20;

        for (int i = 0; i < segments; i += 2)
        {
            double t1 = (double)i / segments;
            double t2 = Math.Min((double)(i + 1) / segments, 1.0);

            var p1 = new Point(
                start.X + (int)(t1 * (end.X - start.X)),
                start.Y + (int)(t1 * (end.Y - start.Y))
            );
            var p2 = new Point(
                start.X + (int)(t2 * (end.X - start.X)),
                start.Y + (int)(t2 * (end.Y - start.Y))
            );

            Cv2.Line(image, p1, p2, color, parameters.LineThickness);
        }
    }

    /// <summary>
    /// 중심점을 표시합니다
    /// </summary>
    private void DrawCenterPoint(Mat image, Point center, int centerIndex, RadialLinesParameters parameters)
    {
        Scalar color = new Scalar(parameters.LineColor.B, parameters.LineColor.G, parameters.LineColor.R);

        // 중심점 원 그리기
        Cv2.Circle(image, center, 5, color, parameters.LineThickness);
        Cv2.Circle(image, center, 2, new Scalar(255, 255, 255), -1); // 흰색 중심점

        // 중심점 번호 표시 (여러 중심점이 있을 때)
        if (centerIndex >= 0)
        {
            Cv2.PutText(image, centerIndex.ToString(),
                       new Point(center.X + 10, center.Y - 10),
                       HersheyFonts.HersheySimplex, 0.6, color, 2);
        }
    }

    /// <summary>
    /// 방사선 길이 데이터를 파싱하는 헬퍼 메서드 (다른 노드에서 사용)
    /// </summary>
    public static List<List<double>> ParseRadialLengthData(string lengthData)
    {
        var result = new List<List<double>>();

        if (string.IsNullOrEmpty(lengthData)) return result;

        try
        {
            // "|" 구분자로 각 중심점별 데이터 분리
            string[] centerData = lengthData.Split('|');

            foreach (string data in centerData)
            {
                if (string.IsNullOrEmpty(data)) continue;

                // "Center0:10.5,20.3,15.2,..." 형태에서 길이 부분만 추출
                var colonIndex = data.IndexOf(':');
                if (colonIndex > 0 && colonIndex < data.Length - 1)
                {
                    string lengthsStr = data.Substring(colonIndex + 1);
                    string[] lengthStrs = lengthsStr.Split(',');

                    var lengths = new List<double>();
                    foreach (string lengthStr in lengthStrs)
                    {
                        if (double.TryParse(lengthStr, out double length))
                        {
                            lengths.Add(length);
                        }
                    }

                    if (lengths.Count > 0)
                    {
                        result.Add(lengths);
                    }
                }
            }
        }
        catch (Exception)
        {
            result.Clear();
        }

        return result;
    }

    #endregion
}
