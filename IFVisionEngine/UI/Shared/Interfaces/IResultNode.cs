using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFVisionEngine.UIComponents.Interfaces
{
    /// <summary>
    /// 결과값을 제공하는 노드임을 표시하는 인터페이스
    /// </summary>
    public interface IResultNode
    {
        /// <summary>
        /// 노드의 결과 데이터를 반환합니다
        /// </summary>
        string GetResultData();

        /// <summary>
        /// 노드의 타입을 반환합니다
        /// </summary>
        string GetNodeType();

        /// <summary>
        /// 결과가 유효한지 확인합니다
        /// </summary>
        bool IsResultValid();
    }
}