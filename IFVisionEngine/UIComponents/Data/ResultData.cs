using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IFVisionEngine.UIComponents.Data
{
    /// <summary>
    /// 노드 실행 결과 데이터를 저장하는 클래스
    /// </summary>
    public class  ResultData
    {
        public string NodeName { get; set; }
        public string NodeType { get; set; }
        public string ResultContent { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsValid { get; set; }
        public string Status { get; set; }
        public string NodeId { get; set; }

        public ResultData()
        {
            Timestamp = DateTime.Now;
            IsValid = false;
            Status = "Unknown";
            NodeId = GenerateShortId();
        }

        public ResultData(string nodeName, string nodeType, string resultContent, bool isValid)
        {
            NodeName = nodeName ?? "Unknown";
            NodeType = nodeType ?? "Unknown";
            ResultContent = resultContent ?? "";
            IsValid = isValid;
            Timestamp = DateTime.Now;
            Status = isValid ? "Success" : "Failed";
            NodeId = GenerateShortId();
        }

        /// <summary>
        /// C# 7.3 호환을 위한 8자리 ID 생성
        /// </summary>
        private string GenerateShortId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8);
        }

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss}] {NodeName} ({NodeType}) - {Status}";
        }

        /// <summary>
        /// 결과 요약 정보를 반환합니다
        /// </summary>
        public string GetSummary()
        {
            if (string.IsNullOrEmpty(ResultContent))
                return "No data";

            var lines = ResultContent.Split('\n');
            return lines.Length > 0 ? lines[0].Trim() : "Empty";
        }

        /// <summary>
        /// 상태에 따른 색상을 반환합니다
        /// </summary>
        public Color GetStatusColor()
        {
            switch (Status.ToLower())
            {
                case "success": return Color.Green;
                case "failed": return Color.Red;
                case "warning": return Color.Orange;
                default: return Color.Gray;
            }
        }
    }
}