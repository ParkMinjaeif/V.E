using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFVisionEngine.UIComponents.Enums
{
    /// <summary>
    /// 크기 조절 방향을 정의하는 열거형
    /// 8방향 크기 조절을 지원 (4개 모서리 + 4개 가장자리)
    /// </summary>
    public enum ResizeDirection
    {
        /// <summary>크기 조절 불가 영역</summary>
        None,
        /// <summary>상단 가장자리 - 세로 크기만 조절</summary>
        Top,
        /// <summary>하단 가장자리 - 세로 크기만 조절</summary>
        Bottom,
        /// <summary>좌측 가장자리 - 가로 크기만 조절</summary>
        Left,
        /// <summary>우측 가장자리 - 가로 크기만 조절</summary>
        Right,
        /// <summary>좌상단 모서리 - 가로+세로 동시 조절, 위치 이동</summary>
        TopLeft,
        /// <summary>우상단 모서리 - 가로+세로 동시 조절, Y 위치 이동</summary>
        TopRight,
        /// <summary>좌하단 모서리 - 가로+세로 동시 조절, X 위치 이동</summary>
        BottomLeft,
        /// <summary>우하단 모서리 - 가로+세로 동시 조절, 위치 고정</summary>
        BottomRight
    }
}