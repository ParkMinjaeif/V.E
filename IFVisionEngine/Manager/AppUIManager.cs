using IFVisionEngine.UIComponents.UserControls;
using NodeEditor;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace IFVisionEngine.Manager
{
    public static class AppUIManager
    {
        private static Form1 _mainFormInstance;

        // 모든 주요 UI 컨트롤들을 정적 속성으로 관리합니다.
        public static UcNodeEditor ucNodeEditor;
        public static UcImageControler ucImageControler;
        public static UcLogView ucLogView;
        public static UcNodeDataView ucNodeDataView; // 컨테이너 역할
        public static UcNodeSelectedView ucNodeSelectedView; // 선택된 노드 속성 뷰
        public static UcNodeExecutionView ucNodeExecutionView; // 실행 흐름 뷰


        public static void Initialize(Form1 mainForm)
        {
            if (_mainFormInstance != null)
            {
                // 이미 초기화된 경우 (예외 처리 또는 로깅 등 적절히 대응)
                if (_mainFormInstance == mainForm) return; // 같은 인스턴스면 무시
                throw new InvalidOperationException("AppUIManager는 이미 다른 FormMain 인스턴스로 초기화되었습니다.");
            }

            _mainFormInstance = mainForm ?? throw new ArgumentNullException(nameof(mainForm));

            // 선행 UserControl 인스턴스 생성
            ucNodeSelectedView = new UcNodeSelectedView();
            ucNodeExecutionView = new UcNodeExecutionView();
            ucLogView = new UcLogView(_mainFormInstance);

            // FormMain 인스턴스를 사용하여 UserControl들 생성
            ucNodeEditor = new UcNodeEditor(_mainFormInstance);
            ucImageControler = new UcImageControler(_mainFormInstance);
            ucNodeDataView = new UcNodeDataView(_mainFormInstance);

            // *** 이벤트 핸들러 연결 ***
            var nodeContext = ucNodeEditor.GetContext();
            if (nodeContext != null)
            {
                // MyNodesContext에서 발생하는 FeedbackInfo 이벤트를 구독합니다.
                nodeContext.FeedbackInfo += OnNodeFeedbackReceived;
            }
            // *** 노드 선택 변경 이벤트를 구독합니다. ***
            ucNodeEditor.SelectedNodeContextChanged += OnSelectedNodeContextChanged;
        }

        /// <summary>
        /// 노드로부터 피드백 신호가 올 때마다 호출되는 이벤트 핸들러입니다.
        /// 이 메서드가 중앙에서 신호를 받아 각 UI 컨트롤에 작업을 분배합니다.
        /// </summary>
        private static void OnNodeFeedbackReceived(string message, NodeVisual node, FeedbackType type, object data, bool stop)
        {
            // 로그를 추가합니다.
            ucLogView?.AddLog(type, $"[{node.Name}] {message}");

            // 데이터가 이미지이면 이미지 뷰에 표시합니다.
            if (data is Mat imageToShow)
            {
                ucImageControler?.DisplayImage(imageToShow);
            }

            // *** 추가된 부분 ***
            // 실행된 노드 정보를 Execution View에 전달하여 누적합니다.
            ucNodeExecutionView?.AddExecutionData(node);
        }

        /// <summary>
        /// UcNodeEditor에서 선택된 노드가 변경될 때 호출됩니다.
        /// </summary>
        private static void OnSelectedNodeContextChanged(object contextObject)
        {
            // UcNodeDataView에 선택된 노드의 컨텍스트(속성 정보)를 표시하도록 요청합니다.
            // contextObject가 null이면 PropertyGrid는 자동으로 비워집니다.
            ucNodeSelectedView?.DisplayNowData(contextObject);
        }
    }
}


