using IFVisionEngine.UIComponents.UserControls;
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
            ucNodeEditor = new UcNodeEditor();
        }
    }
}
