using IFVisionEngine.UIComponents.UserControls;
using NodeEditor;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace IFVisionEngine.Manager
{
    public static class AppUIManager
    {
        private static Form1 _mainFormInstance;

        public static UcNodeEditor ucNodeEditor;
        public static UcImageControler ucImageControler;
        public static UcLogView ucLogView;

        public static void Initialize(Form1 mainForm)
        {
            if (_mainFormInstance != null)
            {
                // 이미 초기화된 경우 (예외 처리 또는 로깅 등 적절히 대응)
                if (_mainFormInstance == mainForm) return; // 같은 인스턴스면 무시
                throw new InvalidOperationException("AppUIManager는 이미 다른 FormMain 인스턴스로 초기화되었습니다.");
            }

            _mainFormInstance = mainForm ?? throw new ArgumentNullException(nameof(mainForm));

            // FormMain 인스턴스를 사용하여 UserControl들 생성
            ucNodeEditor = new UcNodeEditor(_mainFormInstance);
            ucImageControler = new UcImageControler(_mainFormInstance);
            ucLogView = new UcLogView(_mainFormInstance);

            // *** 추가된 로직: 이벤트 핸들러 연결 ***
            var nodeContext = ucNodeEditor.GetContext();
            if (nodeContext != null)
            {
                // MyNodesContext에서 발생하는 FeedbackInfo 이벤트를 구독합니다.
                nodeContext.FeedbackInfo += OnNodeFeedbackReceived;
            }
        }

        /// <summary>
        /// 노드로부터 피드백 신호가 올 때마다 호출되는 이벤트 핸들러입니다.
        /// 이 메서드가 중앙에서 신호를 받아 각 UI 컨트롤에 작업을 분배합니다.
        /// </summary>
        private static void OnNodeFeedbackReceived(string message, NodeVisual node, FeedbackType type, object data, bool stop)
        {
            // 로그를 추가합니다.
            AppUIManager.ucLogView.AddLog(type, $"[{node.Name}] {message}");

            // 2. 전달된 데이터가 Mat 타입이고 ucImageControler가 있다면 이미지를 표시합니다.
            if (data is Mat imageToShow)
            {
                ucImageControler?.DisplayImage(imageToShow);
            }
        }
    }
}


