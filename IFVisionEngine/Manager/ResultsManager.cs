using System;
using System.Collections.Generic;
using System.Linq;
using IFVisionEngine.UIComponents.Data;

namespace IFVisionEngine.UIComponents.Managers
{
    /// <summary>
    /// 노드 결과값을 중앙에서 관리하는 클래스
    /// </summary>
    public class ResultsManager
    {
        private static ResultsManager _instance;
        private static readonly object _lock = new object();

        private List<ResultData> _results;
        private int _maxResults = 100; // 최대 저장할 결과 개수

        // 이벤트
        public event Action<ResultData> OnResultAdded;
        public event Action<ResultData> OnResultUpdated;
        public event Action OnResultsCleared;

        private ResultsManager()
        {
            _results = new List<ResultData>();
        }

        /// <summary>
        /// 싱글톤 인스턴스
        /// </summary>
        public static ResultsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new ResultsManager();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 새 결과를 추가합니다
        /// </summary>
        public void AddResult(ResultData result)
        {
            if (result == null) return;

            lock (_lock)
            {
                // 최대 개수 초과 시 오래된 것 제거
                if (_results.Count >= _maxResults)
                {
                    _results.RemoveAt(0);
                }

                _results.Add(result);
                Console.WriteLine($"[ResultsManager] 결과 추가: {result.NodeName} - {result.GetSummary()}");
            }

            OnResultAdded?.Invoke(result);
        }

        /// <summary>
        /// 노드 이름으로 결과를 추가합니다
        /// </summary>
        public void AddResult(string nodeName, string nodeType, string resultContent, bool isValid)
        {
            var result = new ResultData(nodeName, nodeType, resultContent, isValid);
            AddResult(result);
        }

        /// <summary>
        /// 특정 노드의 결과를 업데이트합니다
        /// </summary>
        public void UpdateResult(string nodeName, string resultContent, bool isValid)
        {
            lock (_lock)
            {
                var existingResult = _results.LastOrDefault(r => r.NodeName == nodeName);
                if (existingResult != null)
                {
                    existingResult.ResultContent = resultContent;
                    existingResult.IsValid = isValid;
                    existingResult.Status = isValid ? "Success" : "Failed";
                    existingResult.Timestamp = DateTime.Now;

                    OnResultUpdated?.Invoke(existingResult);
                }
            }
        }

        /// <summary>
        /// 모든 결과를 반환합니다
        /// </summary>
        public List<ResultData> GetAllResults()
        {
            lock (_lock)
            {
                return new List<ResultData>(_results);
            }
        }

        /// <summary>
        /// 특정 노드의 결과를 반환합니다
        /// </summary>
        public List<ResultData> GetResultsByNodeName(string nodeName)
        {
            lock (_lock)
            {
                return _results.Where(r => r.NodeName.Contains(nodeName)).ToList();
            }
        }

        /// <summary>
        /// 특정 노드 타입의 결과를 반환합니다
        /// </summary>
        public List<ResultData> GetResultsByNodeType(string nodeType)
        {
            lock (_lock)
            {
                return _results.Where(r => r.NodeType == nodeType).ToList();
            }
        }

        /// <summary>
        /// 특정 노드의 최신 결과를 반환합니다
        /// </summary>
        public ResultData GetLatestResult(string nodeName)
        {
            lock (_lock)
            {
                return _results.LastOrDefault(r => r.NodeName == nodeName);
            }
        }

        /// <summary>
        /// 유효한 결과만 반환합니다
        /// </summary>
        public List<ResultData> GetValidResults()
        {
            lock (_lock)
            {
                return _results.Where(r => r.IsValid).ToList();
            }
        }

        /// <summary>
        /// 실패한 결과만 반환합니다
        /// </summary>
        public List<ResultData> GetFailedResults()
        {
            lock (_lock)
            {
                return _results.Where(r => !r.IsValid).ToList();
            }
        }

        /// <summary>
        /// 특정 시간 이후의 결과를 반환합니다
        /// </summary>
        public List<ResultData> GetResultsSince(DateTime since)
        {
            lock (_lock)
            {
                return _results.Where(r => r.Timestamp >= since).ToList();
            }
        }

        /// <summary>
        /// 모든 결과를 삭제합니다
        /// </summary>
        public void ClearResults()
        {
            lock (_lock)
            {
                _results.Clear();
                Console.WriteLine("[ResultsManager] 모든 결과 삭제됨");
            }

            OnResultsCleared?.Invoke();
        }

        /// <summary>
        /// 특정 노드의 결과를 삭제합니다
        /// </summary>
        public void RemoveResultsByNodeName(string nodeName)
        {
            lock (_lock)
            {
                int removedCount = _results.RemoveAll(r => r.NodeName == nodeName);
                Console.WriteLine($"[ResultsManager] {nodeName} 노드의 결과 {removedCount}개 삭제됨");
            }
        }

        /// <summary>
        /// 최대 저장 개수를 설정합니다
        /// </summary>
        public void SetMaxResults(int maxResults)
        {
            _maxResults = Math.Max(10, maxResults);
        }

        /// <summary>
        /// 통계 정보를 반환합니다
        /// </summary>
        public Dictionary<string, object> GetStatistics()
        {
            lock (_lock)
            {
                var stats = new Dictionary<string, object>();
                stats["TotalResults"] = _results.Count;
                stats["ValidResults"] = _results.Count(r => r.IsValid);
                stats["FailedResults"] = _results.Count(r => !r.IsValid);
                stats["NodeTypes"] = _results.Select(r => r.NodeType).Distinct().Count();
                stats["UniqueNodes"] = _results.Select(r => r.NodeName).Distinct().Count();

                if (_results.Count > 0)
                {
                    stats["OldestResult"] = _results.First().Timestamp;
                    stats["LatestResult"] = _results.Last().Timestamp;
                }

                return stats;
            }
        }

        /// <summary>
        /// 결과를 텍스트로 내보냅니다
        /// </summary>
        public string ExportToText()
        {
            lock (_lock)
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine("=== 노드 실행 결과 요약 ===");
                sb.AppendLine($"생성 시간: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine($"총 결과 수: {_results.Count}");
                sb.AppendLine();

                foreach (var result in _results)
                {
                    sb.AppendLine($"[{result.Timestamp:yyyy-MM-dd HH:mm:ss}] {result.NodeName} ({result.NodeType})");
                    sb.AppendLine($"상태: {result.Status}");
                    sb.AppendLine($"결과:");
                    sb.AppendLine(result.ResultContent);
                    sb.AppendLine(new string('-', 50));
                }

                return sb.ToString();
            }
        }
    }
}