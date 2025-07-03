using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NodeEditor;
using OpenCvSharp;
using IFVisionEngine.Manager;
using IFVisionEngine.UIComponents.Dialogs;
using IFVisionEngine.UIComponents.Managers;
using IFVisionEngine.UIComponents.Data;
using static MyNodesContext;

public partial class MyNodesContext
{
    #region LineValidation

    /// <summary>
    /// 선 검증 파라미터 클래스
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class LineValidationParameters
    {
        [Description("허용 오차 임계값 (%)")]
        public double ErrorThreshold { get; set; } = 10.0;

        [Description("오차 계산 모드 (StdDev/Range/CV)")]
        public string ErrorMode { get; set; } = "StdDev";

        [Description("상세 결과 포함 여부")]
        public bool IncludeDetails { get; set; } = true;

        public override string ToString()
        {
            return $"임계값:{ErrorThreshold}%, 모드:{ErrorMode}";
        }
    }

    /// <summary>
    /// 선 통계 정보 클래스
    /// </summary>
    public class LineStatistics
    {
        public int Count { get; set; }
        public double Mean { get; set; }
        public double StdDev { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Range { get; set; }
        public double CV { get; set; }  // 변동계수
    }

    /// <summary>
    /// 선 검증 노드 - IResultNode 인터페이스 구현
    /// </summary>
    [Node(name: "LineValidation", menu: "검증", description: "선의 길이 데이터를 검증하고 오차를 측정합니다.", Width = 200)]
    public void ValidateLines(string lineData,
                             LineValidationParameters LineValidationparameters,
                             out string validationResult)
    {
        validationResult = null;

        if (string.IsNullOrEmpty(lineData))
        {
            validationResult = "ERROR: 입력 데이터 없음";
            FeedbackInfo?.Invoke("선 데이터가 비어있습니다.", CurrentProcessingNode, FeedbackType.Error, null, true);
            return;
        }

        try
        {
            Console.WriteLine($"[LineValidation] 입력 데이터: '{lineData}'");

            // 1. 데이터 파싱
            var lineValues = ParseLineData(lineData);
            Console.WriteLine($"[LineValidation] 파싱된 값 개수: {lineValues.Count}");

            if (lineValues.Count == 0)
            {
                validationResult = "ERROR: 파싱 실패";
                FeedbackInfo?.Invoke("데이터 파싱에 실패했습니다.", CurrentProcessingNode, FeedbackType.Error, null, true);
                return;
            }

            // 2. 통계 계산
            var stats = CalculateStatistics(lineValues);

            // 3. 오차 계산
            double errorValue = CalculateError(stats, LineValidationparameters.ErrorMode);

            // 4. 검증 결과 생성
            bool isValid = errorValue <= LineValidationparameters.ErrorThreshold;
            validationResult = GenerateValidationResult(stats, errorValue, isValid, LineValidationparameters);

            // 5. 노드 컨텍스트에 결과 저장
            var nodeContext = CurrentProcessingNode.GetNodeContext() as DynamicNodeContext;
            if (nodeContext != null)
            {
                nodeContext["validationResult"] = validationResult;
                Console.WriteLine($"[LineValidation] DynamicNodeContext에 validationResult 설정: '{validationResult}'");
            }

            // 6. 결과 매니저에 자동 등록
            RegisterResultToManager(stats, errorValue, isValid, LineValidationparameters);

            // 7. 피드백
            string status = isValid ? "✅ 검증 통과" : "⚠️ 검증 실패";
            FeedbackInfo?.Invoke($"{status} - 오차: {errorValue:F2}", CurrentProcessingNode,
                               isValid ? FeedbackType.Information : FeedbackType.Warning, null, false);

        }
        catch (Exception ex)
        {
            validationResult = $"ERROR: {ex.Message}";
            Console.WriteLine($"[LineValidation] 예외 발생: {ex.Message}");
            Console.WriteLine($"[LineValidation] 스택 트레이스: {ex.StackTrace}");
            FeedbackInfo?.Invoke($"검증 중 오류: {ex.Message}", CurrentProcessingNode, FeedbackType.Error, null, true);

            RegisterErrorToManager(ex);
        }
    }

    /// <summary>
    /// IResultNode 인터페이스 구현 - 노드의 결과 데이터를 반환
    /// </summary>
    public string GetResultData()
    {
        try
        {
            var nodeContext = CurrentProcessingNode?.GetNodeContext() as DynamicNodeContext;
            if (nodeContext != null)
            {
                var memberNames = nodeContext.GetDynamicMemberNames();
                if (memberNames.Contains("validationResult"))
                {
                    var result = nodeContext["validationResult"];
                    return result?.ToString() ?? "결과 없음";
                }
            }
            return "결과 데이터를 찾을 수 없습니다.";
        }
        catch (Exception ex)
        {
            return $"결과 조회 오류: {ex.Message}";
        }
    }

    /// <summary>
    /// IResultNode 인터페이스 구현 - 노드의 타입을 반환
    /// </summary>
    public string GetNodeType()
    {
        return "LineValidation";
    }

    /// <summary>
    /// IResultNode 인터페이스 구현 - 결과가 유효한지 확인
    /// </summary>
    public bool IsResultValid()
    {
        try
        {
            string resultData = GetResultData();
            return !string.IsNullOrEmpty(resultData) &&
                   !resultData.StartsWith("ERROR") &&
                   !resultData.Contains("오류");
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region Private Helper Methods

    /// <summary>
    /// 검증 결과를 결과 매니저에 등록
    /// </summary>
    private void RegisterResultToManager(LineStatistics stats, double errorValue, bool isValid, LineValidationParameters parameters)
    {
        try
        {
            var nodeName = CurrentProcessingNode?.Name ?? "LineValidation";
            var resultContent = GenerateDetailedResult(stats, errorValue, isValid, parameters);

            var resultData = new ResultData(
                nodeName,
                "LineValidation",
                resultContent,
                isValid
            );

            ResultsManager.Instance.AddResult(resultData);
            Console.WriteLine($"[LineValidation] 결과 매니저에 등록 완료: {resultData.GetSummary()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LineValidation] 결과 매니저 등록 실패: {ex.Message}");
        }
    }

    /// <summary>
    /// 오류를 결과 매니저에 등록
    /// </summary>
    private void RegisterErrorToManager(Exception ex)
    {
        try
        {
            var nodeName = CurrentProcessingNode?.Name ?? "LineValidation";
            var errorContent = $"오류 발생: {ex.Message}\n스택 트레이스:\n{ex.StackTrace}";

            var resultData = new ResultData(
                nodeName,
                "LineValidation",
                errorContent,
                false
            );

            ResultsManager.Instance.AddResult(resultData);
            Console.WriteLine($"[LineValidation] 오류 결과 등록 완료");
        }
        catch (Exception regEx)
        {
            Console.WriteLine($"[LineValidation] 오류 결과 등록 실패: {regEx.Message}");
        }
    }

    /// <summary>
    /// 상세한 결과 내용을 생성
    /// </summary>
    private string GenerateDetailedResult(LineStatistics stats, double errorValue, bool isValid, LineValidationParameters parameters)
    {
        var result = new StringBuilder();

        result.AppendLine("=== 선 검증 결과 ===");
        result.AppendLine($"검증 시간: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        result.AppendLine($"검증 상태: {(isValid ? "통과" : "실패")}");
        result.AppendLine($"오차 값: {errorValue:F2}");
        result.AppendLine($"허용 임계값: {parameters.ErrorThreshold}");
        result.AppendLine($"오차 모드: {parameters.ErrorMode}");
        result.AppendLine();

        result.AppendLine("=== 통계 정보 ===");
        result.AppendLine($"데이터 개수: {stats.Count}");
        result.AppendLine($"평균 길이: {stats.Mean:F2}");
        result.AppendLine($"표준편차: {stats.StdDev:F2}");
        result.AppendLine($"최소값: {stats.Min:F2}");
        result.AppendLine($"최대값: {stats.Max:F2}");
        result.AppendLine($"범위: {stats.Range:F2}");
        result.AppendLine($"변동계수: {stats.CV:F2}%");

        if (!isValid)
        {
            result.AppendLine();
            result.AppendLine("=== 권장사항 ===");
            result.AppendLine("• 측정 조건을 재검토하세요.");
            result.AppendLine("• 데이터 품질을 확인하세요.");
            result.AppendLine("• 임계값 설정이 적절한지 검토하세요.");
        }

        return result.ToString();
    }

    /// <summary>
    /// 선 데이터를 파싱하여 숫자 리스트로 변환
    /// </summary>
    private List<double> ParseLineData(string data)
    {
        var values = new List<double>();

        try
        {
            // "Center0:10.5,20.3|Center1:..." 형식 처리
            if (data.Contains("|"))
            {
                var allValues = new List<double>();
                string[] sets = data.Split('|');

                foreach (string set in sets)
                {
                    string valuesPart = set.Contains(":") ? set.Split(':')[1] : set;
                    string[] parts = valuesPart.Split(',');

                    foreach (string part in parts)
                    {
                        if (double.TryParse(part.Trim(), out double value))
                            allValues.Add(value);
                    }
                }
                return allValues;
            }

            // 단순 쉼표 구분 "10.5,20.3,15.2"
            if (data.Contains(","))
            {
                string[] parts = data.Split(',');
                foreach (string part in parts)
                {
                    if (double.TryParse(part.Trim(), out double value))
                        values.Add(value);
                }
            }
            // 공백 구분 "10.5 20.3 15.2"
            else if (data.Contains(" "))
            {
                string[] parts = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (double.TryParse(part.Trim(), out double value))
                        values.Add(value);
                }
            }
            // 단일 값
            else
            {
                if (double.TryParse(data.Trim(), out double singleValue))
                    values.Add(singleValue);
            }
        }
        catch
        {
            values.Clear();
        }

        return values;
    }

    /// <summary>
    /// 통계 정보 계산
    /// </summary>
    private LineStatistics CalculateStatistics(List<double> values)
    {
        var stats = new LineStatistics
        {
            Count = values.Count
        };

        if (values.Count == 0) return stats;

        stats.Mean = values.Average();
        stats.Min = values.Min();
        stats.Max = values.Max();
        stats.Range = stats.Max - stats.Min;

        if (values.Count > 1)
        {
            double variance = values.Sum(x => Math.Pow(x - stats.Mean, 2)) / (values.Count - 1);
            stats.StdDev = Math.Sqrt(variance);
        }

        stats.CV = stats.Mean != 0 ? (stats.StdDev / stats.Mean) * 100 : 0;

        return stats;
    }

    /// <summary>
    /// 오차 계산
    /// </summary>
    private double CalculateError(LineStatistics stats, string errorMode)
    {
        switch (errorMode.ToUpper())
        {
            case "STDDEV":
            case "STD":
                return stats.StdDev;
            case "RANGE":
                return stats.Range;
            case "CV":
                return stats.CV;
            default:
                return stats.StdDev;
        }
    }

    /// <summary>
    /// 검증 결과 생성
    /// </summary>
    private string GenerateValidationResult(LineStatistics stats, double errorValue, bool isValid, LineValidationParameters parameters)
    {
        var result = new StringBuilder();

        // 기본 결과
        string status = isValid ? "PASS" : "FAIL";
        result.AppendLine($"STATUS: {status}");
        result.AppendLine($"ERROR: {errorValue:F2}");
        result.AppendLine($"THRESHOLD: {parameters.ErrorThreshold}");

        // 상세 정보 포함
        if (parameters.IncludeDetails)
        {
            result.AppendLine($"COUNT: {stats.Count}");
            result.AppendLine($"MEAN: {stats.Mean:F2}");
            result.AppendLine($"STDDEV: {stats.StdDev:F2}");
            result.AppendLine($"RANGE: {stats.Range:F2}");
            result.AppendLine($"CV: {stats.CV:F2}%");
            result.AppendLine($"MIN: {stats.Min:F2}");
            result.AppendLine($"MAX: {stats.Max:F2}");
        }

        return result.ToString().Trim();
    }

    #endregion
}