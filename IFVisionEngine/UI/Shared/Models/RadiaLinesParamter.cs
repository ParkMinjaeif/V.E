using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace IFVisionEngine.UIComponents.Data
{
    public class RadialLinesParameter
    {
        // 시각화 설정
        public bool ShowVisualization { get; set; }
        public bool ShowCenter { get; set; }
        public bool ShowAngles { get; set; }
        public bool ShowDistances { get; set; }

        // 중심점 설정
        public string CenterMethod { get; set; }
        public int ManualX { get; set; }
        public int ManualY { get; set; }

        // 무게중심 좌표 (이전 노드에서 받아온 값)
        public double CentroidX { get; set; }
        public double CentroidY { get; set; }
        public bool HasCentroidData { get; set; }

        // 범위 및 라인 설정
        public string RangeMethod { get; set; }
        public int FixedLength { get; set; }
        public int LineCount { get; set; }
        public int StartAngle { get; set; }

        // 스타일 설정
        public Color LineColor { get; set; }
        public int LineThickness { get; set; }
        public string Style { get; set; }

        // 임계값 설정
        public int BinaryThreshold { get; set; }
        public int BrightnessThreshold { get; set; }

        // 출력 설정
        public bool OutputLengthData { get; set; }

        // 기본값으로 초기화하는 생성자
        public RadialLinesParameter()
        {
            ShowVisualization = true;
            ShowCenter = true;
            ShowAngles = false;
            ShowDistances = false;
            CenterMethod = "ImageCenter";
            ManualX = 0;
            ManualY = 0;
            CentroidX = 0;
            CentroidY = 0;
            HasCentroidData = false;
            RangeMethod = "EdgeDetection";
            FixedLength = 100;
            LineCount = 8;
            StartAngle = 0;
            LineColor = Color.Red;
            LineThickness = 1;
            Style = "Solid";
            BinaryThreshold = 128;
            BrightnessThreshold = 128;
            OutputLengthData = false;
        }
    }
}
