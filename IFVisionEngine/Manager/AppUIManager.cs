using IFVisionEngine.UIComponents.Dialogs;
using IFVisionEngine.UIComponents.UserControls;
using NodeEditor;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static IFVisionEngine.UIComponents.UserControls.UcNodeExecutionView;

namespace IFVisionEngine.Manager
{
    /// <summary>
    /// 애플리케이션의 모든 UI 컨트롤들을 중앙에서 관리하는 정적 매니저 클래스입니다.
    /// 각 UI 컨트롤 간의 통신과 이벤트 처리를 담당합니다.
    /// </summary>
    public static class AppUIManager
    {
        #region Private Fields
        private static Form1 _mainFormInstance; // 메인 폼 인스턴스 (중복 초기화 방지용)
        #endregion

        #region Public UI Control Properties
        public static UcNodeEditor ucNodeEditor; // 노드 에디터 컨트롤 - 노드 그래프 편집 및 실행
        public static UcImageControler ucImageControler; // 이미지 컨트롤러 - 이미지 표시 및 관리
        public static UcLogView ucLogView; // 로그 뷰 컨트롤 - 시스템 로그 및 메시지 표시
        public static UcNodeDataView ucNodeDataView; // 노드 데이터 뷰 컨테이너 - 노드 관련 데이터 표시
        public static UcNodeSelectedView ucNodeSelectedView; // 선택된 노드의 속성 및 정보를 표시하는 뷰
        public static UcNodeExecutionView ucNodeExecutionView; // 노드 실행 히스토리 및 흐름을 표시하는 뷰
        public static UcResultView ucResultView; // 결과 뷰 컨트롤 - 실행 결과 및 이미지 히스토리 표시
        #endregion

        #region Initialization Methods
        /// <summary>
        /// AppUIManager를 초기화하고 모든 UI 컨트롤들을 생성합니다.
        /// 이벤트 핸들러를 연결하여 컨트롤 간 통신을 설정합니다.
        /// </summary>
        /// <param name="mainForm">메인 폼 인스턴스</param>
        /// <exception cref="ArgumentNullException">mainForm이 null인 경우</exception>
        /// <exception cref="InvalidOperationException">이미 다른 인스턴스로 초기화된 경우</exception>
        public static void Initialize(Form1 mainForm)
        {
            // 중복 초기화 방지
            ValidateInitialization(mainForm);

            // 메인 폼 인스턴스 저장
            _mainFormInstance = mainForm;

            // UI 컨트롤들 생성
            CreateUIControls();

            // 이벤트 핸들러 연결
            SetupEventHandlers();
        }

        /// <summary>
        /// 초기화 유효성을 검사합니다.
        /// </summary>
        /// <param name="mainForm">검사할 메인 폼 인스턴스</param>
        private static void ValidateInitialization(Form1 mainForm)
        {
            if (mainForm == null)
                throw new ArgumentNullException(nameof(mainForm), "메인 폼 인스턴스가 null입니다.");

            if (_mainFormInstance != null)
            {
                // 같은 인스턴스면 중복 초기화 무시
                if (_mainFormInstance == mainForm)
                    return;

                throw new InvalidOperationException("AppUIManager는 이미 다른 Form1 인스턴스로 초기화되었습니다.");
            }
        }

        /// <summary>
        /// 모든 UI 컨트롤 인스턴스를 생성합니다.
        /// 의존성 순서에 따라 생성 순서가 중요합니다.
        /// </summary>
        private static void CreateUIControls()
        {
            try
            {
                // 독립적인 컨트롤들을 먼저 생성
                ucNodeSelectedView = new UcNodeSelectedView();
                ucNodeExecutionView = new UcNodeExecutionView();
                ucLogView = new UcLogView();

                // 메인 폼에 의존하는 컨트롤들 생성
                ucNodeEditor = new UcNodeEditor(_mainFormInstance);
                ucImageControler = new UcImageControler(_mainFormInstance);
                ucNodeDataView = new UcNodeDataView(_mainFormInstance);

                ucResultView = new UcResultView();

                Console.WriteLine("✅ 모든 UI 컨트롤 생성 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ UI 컨트롤 생성 실패: {ex.Message}");
                throw new InvalidOperationException("UI 컨트롤 생성 중 오류가 발생했습니다.", ex);
            }
        }

        /// <summary>
        /// 각 UI 컨트롤 간의 이벤트 핸들러를 연결합니다.
        /// </summary>
        private static void SetupEventHandlers()
        {
            try
            {
                // 노드 에디터의 컨텍스트에서 이벤트 연결
                SetupNodeEditorEvents();

                Console.WriteLine("✅ 모든 이벤트 핸들러 연결 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 이벤트 핸들러 연결 실패: {ex.Message}");
                throw new InvalidOperationException("이벤트 핸들러 연결 중 오류가 발생했습니다.", ex);
            }
        }

        /// <summary>
        /// 노드 에디터 관련 이벤트들을 설정합니다.
        /// </summary>
        private static void SetupNodeEditorEvents()
        {
            if (ucNodeEditor == null)
            {
                Console.WriteLine("❌ ucNodeEditor가 null입니다.");
                return;
            }

            var nodeContext = ucNodeEditor.GetContext();
            if (nodeContext != null)
            {
                nodeContext.FeedbackInfo += OnNodeFeedbackReceived; // 노드 피드백 이벤트 구독 (로그, 이미지 표시, 실행 히스토리)
                nodeContext.ImageKeySelected += OnImageKeySelected; // 이미지 키 선택 이벤트 구독 (이미지 히스토리 관리)
                Console.WriteLine("✅ 노드 컨텍스트 이벤트 연결 완료");
            }
            else
            {
                Console.WriteLine("❌ 노드 컨텍스트를 가져올 수 없습니다.");
            }

            ucNodeEditor.SelectedNodeContextChanged += OnSelectedNodeContextChanged; // 노드 선택 변경 이벤트 구독
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// 노드에서 피드백 신호를 받았을 때 호출되는 이벤트 핸들러입니다.
        /// 각 UI 컨트롤에 적절한 작업을 분배합니다.
        /// </summary>
        /// <param name="message">피드백 메시지</param>
        /// <param name="node">피드백을 발생시킨 노드</param>
        /// <param name="type">피드백 유형 (Debug, Warning, Error 등)</param>
        /// <param name="data">추가 데이터 (이미지, 결과값 등)</param>
        /// <param name="stop">실행 중단 여부</param>
        private static void OnNodeFeedbackReceived(string message, NodeVisual node, FeedbackType type, object data, bool stop)
        {
            try
            {
                ucLogView?.AddLog(type, $"[{node?.Name ?? "Unknown"}] {message}"); // 로그 뷰에 메시지 추가

                if (data is Mat imageMatrix)
                {
                    ucImageControler?.DisplayImage(imageMatrix); // 데이터가 이미지인 경우 이미지 컨트롤러에 표시
                }

                if (node != null)
                {
                    ucNodeExecutionView?.AddExecutionData(node); // 노드 실행 히스토리에 추가
                }

                if (stop)
                {
                    Console.WriteLine($"⚠️ 노드 실행 중단 요청: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 노드 피드백 처리 중 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 이미지 키가 선택되었을 때 호출되는 이벤트 핸들러입니다.
        /// 실행 뷰의 이미지 히스토리에 추가합니다.
        /// </summary>
        /// <param name="imageKey">선택된 이미지의 키</param>
        /// <param name="name">이미지 이름</param>
        private static void OnImageKeySelected(string imageKey, string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(imageKey) && !string.IsNullOrEmpty(name))
                {
                    var keyNamePair = new key_name
                    {
                        key = imageKey,
                        name = name
                    };

                    ucNodeExecutionView?.nodeImageKeyHistory.Add(keyNamePair); // 이미지 키 히스토리에 추가
                    Console.WriteLine($"✅ 이미지 키 히스토리 추가: {name} ({imageKey})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 이미지 키 선택 처리 중 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 노드 에디터에서 선택된 노드가 변경되었을 때 호출되는 이벤트 핸들러입니다.
        /// 선택된 노드의 정보를 노드 선택 뷰에 표시합니다.
        /// </summary>
        /// <param name="contextObject">선택된 노드의 컨텍스트 객체</param>
        private static void OnSelectedNodeContextChanged(object contextObject)
        {
            try
            {
                ucNodeSelectedView?.DisplayNowData(contextObject); // 선택된 노드 정보를 노드 선택 뷰에 표시

                if (contextObject != null)
                {
                    Console.WriteLine($"✅ 선택된 노드 컨텍스트 업데이트: {contextObject.GetType().Name}");
                }
                else
                {
                    Console.WriteLine("✅ 노드 선택 해제");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 선택된 노드 컨텍스트 변경 처리 중 오류: {ex.Message}");
            }
        }
        #endregion

        #region Utility Methods
        /// <summary>
        /// 현재 초기화 상태를 확인합니다.
        /// </summary>
        /// <returns>초기화 완료 여부</returns>
        public static bool IsInitialized()
        {
            return _mainFormInstance != null &&
                   ucNodeEditor != null &&
                   ucImageControler != null &&
                   ucLogView != null;
        }

        /// <summary>
        /// 모든 UI 컨트롤들의 리소스를 정리합니다.
        /// 애플리케이션 종료 시 호출해야 합니다.
        /// </summary>
        public static void Cleanup()
        {
            try
            {
                if (ucNodeEditor != null)
                {
                    var nodeContext = ucNodeEditor.GetContext();
                    if (nodeContext != null)
                    {
                        nodeContext.FeedbackInfo -= OnNodeFeedbackReceived; // 이벤트 핸들러 해제
                        nodeContext.ImageKeySelected -= OnImageKeySelected; // 이벤트 핸들러 해제
                    }
                    ucNodeEditor.SelectedNodeContextChanged -= OnSelectedNodeContextChanged; // 이벤트 핸들러 해제
                }

                ucNodeEditor?.Dispose(); // UI 컨트롤 리소스 해제
                ucImageControler?.Dispose(); // UI 컨트롤 리소스 해제
                ucLogView?.Dispose(); // UI 컨트롤 리소스 해제
                ucNodeDataView?.Dispose(); // UI 컨트롤 리소스 해제
                ucNodeSelectedView?.Dispose(); // UI 컨트롤 리소스 해제
                ucNodeExecutionView?.Dispose(); // UI 컨트롤 리소스 해제
                ucResultView?.Dispose(); // UI 컨트롤 리소스 해제

                _mainFormInstance = null; // 참조 초기화
                ucNodeEditor = null; // 참조 초기화
                ucImageControler = null; // 참조 초기화
                ucLogView = null; // 참조 초기화
                ucNodeDataView = null; // 참조 초기화
                ucNodeSelectedView = null; // 참조 초기화
                ucNodeExecutionView = null; // 참조 초기화
                ucResultView = null; // 참조 초기화

                Console.WriteLine("✅ AppUIManager 정리 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ AppUIManager 정리 중 오류: {ex.Message}");
            }
        }
        #endregion
    }
}