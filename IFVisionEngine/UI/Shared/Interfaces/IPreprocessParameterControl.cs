using System;
using System.Collections.Generic;

namespace IFVisionEngine.UIComponents.Dialogs
{
    /// <summary>
    /// 전처리 파라미터 컨트롤이 구현해야 하는 기본 인터페이스입니다.
    /// 모든 파라미터 컨트롤의 공통 기능을 정의합니다.
    /// </summary>
    public interface IPreprocessParameterControl
    {
        /// <summary>
        /// 모든 파라미터를 기본값으로 초기화합니다.
        /// </summary>
        void ResetParametersToDefault();

        /// <summary>
        /// 파라미터가 변경될 때마다 발생하는 기본 이벤트입니다.
        /// 파라미터 종류에 관계없이 공통으로 처리할 작업에 사용됩니다.
        /// </summary>
        event Action OnParametersChangedBase;

        /// <summary>
        /// 현재 설정된 모든 파라미터값을 Dictionary 형태로 반환합니다.
        /// </summary>
        /// <returns>파라미터 이름을 키로, 파라미터 값을 밸류로 하는 딕셔너리</returns>
        Dictionary<string, object> GetParameters();
    }

    /// <summary>
    /// 외부에서 파라미터값을 로드할 수 있는 컨트롤이 구현해야 하는 인터페이스입니다.
    /// 기존 설정값을 UI에 복원하는 기능을 제공합니다.
    /// </summary>
    public interface IParameterLoadable
    {
        /// <summary>
        /// 외부에서 전달된 파라미터값들을 컨트롤의 UI에 설정합니다.
        /// 노드의 기존 설정값을 파라미터 창에 로드할 때 사용됩니다.
        /// </summary>
        /// <param name="parameters">설정할 파라미터 딕셔너리 (키: 파라미터명, 값: 파라미터값)</param>
        void SetCurrentParameters(Dictionary<string, object> parameters);
    }
}