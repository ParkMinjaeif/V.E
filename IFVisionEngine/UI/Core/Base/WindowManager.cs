using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IFVisionEngine.Manager;
using IFVisionEngine.UIComponents.CustomControls;

namespace IFVisionEngine.UIComponents.Common
{
    /// <summary>
    /// 창 관리 전담 클래스
    /// 동적 창 생성, 관리, 배치 등을 담당합니다.
    /// </summary>
    public class WindowManager
    {
        #region Private Fields

        /// <summary>부모 폼 참조</summary>
        private readonly Form _parentForm;

        /// <summary>생성된 창들을 관리하는 딕셔너리</summary>
        private readonly Dictionary<string, WindowWrapper> _windows;

        /// <summary>창 배치 오프셋</summary>
        private const int WINDOW_OFFSET_INCREMENT = 30;

        #endregion

        #region Constructor

        /// <summary>
        /// WindowManager 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="parentForm">창들을 관리할 부모 폼</param>
        public WindowManager(Form parentForm)
        {
            _parentForm = parentForm ?? throw new ArgumentNullException(nameof(parentForm));
            _windows = new Dictionary<string, WindowWrapper>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 지정된 창을 표시합니다. 이미 생성된 창이 있으면 활성화하고, 없으면 새로 생성합니다.
        /// </summary>
        /// <param name="windowKey">창 식별 키</param>
        /// <returns>생성 또는 활성화된 창, 실패시 null</returns>
        public WindowWrapper ShowWindow(string windowKey)
        {
            if (string.IsNullOrEmpty(windowKey))
                return null;

            // 이미 생성된 창 확인
            if (_windows.TryGetValue(windowKey, out WindowWrapper existingWindow))
            {
                ActivateExistingWindow(existingWindow);
                return existingWindow;
            }

            // 새 창 생성
            return CreateNewWindow(windowKey);
        }

        /// <summary>
        /// 지정된 창을 숨깁니다.
        /// </summary>
        /// <param name="windowKey">창 식별 키</param>
        /// <returns>성공 여부</returns>
        public bool HideWindow(string windowKey)
        {
            if (_windows.TryGetValue(windowKey, out WindowWrapper window))
            {
                window.Visible = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 지정된 창을 완전히 제거합니다.
        /// </summary>
        /// <param name="windowKey">창 식별 키</param>
        /// <returns>성공 여부</returns>
        public bool RemoveWindow(string windowKey)
        {
            if (_windows.TryGetValue(windowKey, out WindowWrapper window))
            {
                _parentForm.Controls.Remove(window);
                window.Dispose();
                _windows.Remove(windowKey);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 창의 표시 상태를 확인합니다.
        /// </summary>
        /// <param name="windowKey">창 식별 키</param>
        /// <returns>표시 여부</returns>
        public bool IsWindowVisible(string windowKey)
        {
            if (_windows.TryGetValue(windowKey, out WindowWrapper window))
            {
                return window.Visible;
            }
            return false;
        }

        /// <summary>
        /// 창이 존재하는지 확인합니다.
        /// </summary>
        /// <param name="windowKey">창 식별 키</param>
        /// <returns>존재 여부</returns>
        public bool WindowExists(string windowKey)
        {
            return _windows.ContainsKey(windowKey);
        }

        /// <summary>
        /// 특정 창을 가져옵니다.
        /// </summary>
        /// <param name="windowKey">창 식별 키</param>
        /// <returns>창 인스턴스, 없으면 null</returns>
        public WindowWrapper GetWindow(string windowKey)
        {
            _windows.TryGetValue(windowKey, out WindowWrapper window);
            return window;
        }

        /// <summary>
        /// 모든 창을 숨깁니다.
        /// </summary>
        public void HideAllWindows()
        {
            foreach (var window in _windows.Values)
            {
                window.Visible = false;
            }
        }

        /// <summary>
        /// 모든 창을 표시합니다.
        /// </summary>
        public void ShowAllWindows()
        {
            foreach (var window in _windows.Values)
            {
                window.Visible = true;
            }
        }

        /// <summary>
        /// 모든 창을 제거합니다.
        /// </summary>
        public void RemoveAllWindows()
        {
            var windowKeys = new List<string>(_windows.Keys);
            foreach (var key in windowKeys)
            {
                RemoveWindow(key);
            }
        }

        /// <summary>
        /// 현재 관리 중인 창들의 상태 정보를 반환합니다.
        /// </summary>
        /// <returns>창 상태 정보 딕셔너리</returns>
        public Dictionary<string, bool> GetWindowStatus()
        {
            var status = new Dictionary<string, bool>();
            foreach (var kvp in _windows)
            {
                status[kvp.Key] = kvp.Value.Visible;
            }
            return status;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 기존 창을 활성화합니다.
        /// </summary>
        /// <param name="window">활성화할 창</param>
        private void ActivateExistingWindow(WindowWrapper window)
        {
            window.Visible = true;
            window.BringToFront();
        }

        /// <summary>
        /// 새로운 창을 생성하고 표시합니다.
        /// </summary>
        /// <param name="windowKey">창 식별 키</param>
        /// <returns>생성된 창, 실패시 null</returns>
        private WindowWrapper CreateNewWindow(string windowKey)
        {
            UserControl innerControl = GetInnerControl(windowKey);
            if (innerControl == null)
                return null;

            string title = GetWindowTitle(windowKey);
            Size windowSize = GetWindowSize(windowKey);

            var windowWrapper = new WindowWrapper(title, innerControl, windowSize);

            // 창 위치 설정 (계단식 배치)
            PositionNewWindow(windowWrapper);

            // 창 닫기 이벤트 연결
            windowWrapper.WindowClosing += (s, e) => {
                // 창이 닫힐 때 숨김 처리만 (완전 제거는 하지 않음)
                windowWrapper.Visible = false;
            };

            // 부모 폼에 추가
            _parentForm.Controls.Add(windowWrapper);
            windowWrapper.BringToFront();
            // 딕셔너리에 등록
            _windows[windowKey] = windowWrapper;

            return windowWrapper;
        }

        /// <summary>
        /// 새 창의 위치를 설정합니다. (계단식 배치)
        /// </summary>
        /// <param name="windowWrapper">위치를 설정할 창</param>
        private void PositionNewWindow(WindowWrapper windowWrapper)
        {
            int offset = _windows.Count * WINDOW_OFFSET_INCREMENT;
            windowWrapper.Location = new Point(50 + offset, 50 + offset);
        }

        /// <summary>
        /// 창 키에 해당하는 내부 컨트롤을 반환합니다.
        /// </summary>
        /// <param name="windowKey">창 식별 키</param>
        /// <returns>내부 컨트롤 인스턴스</returns>
        private UserControl GetInnerControl(string windowKey)
        {
            switch (windowKey)
            {
                case "NodeEditor":
                    return AppUIManager.ucNodeEditor;
                case "ImageControler":
                    return AppUIManager.ucImageControler;
                case "LogView":
                    return AppUIManager.ucLogView;
                case "NodeSelectedView":
                    return AppUIManager.ucNodeSelectedView;
                case "NodeExecutionView":
                    return AppUIManager.ucNodeExecutionView;
                case "ResultView":
                    return AppUIManager.ucResultView;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 창 키에 해당하는 기본 창 크기를 반환합니다.
        /// </summary>
        /// <param name="windowKey">창 식별 키</param>
        /// <returns>창 크기</returns>
        private Size GetWindowSize(string windowKey)
        {
            switch (windowKey)
            {
                case "NodeEditor":
                    return new Size(600, 950);
                case "ImageControler":
                    return new Size(900, 700);
                case "LogView":
                    return new Size(900, 200);
                case "NodeSelectedView":
                    return new Size(300, 250);
                case "NodeExecutionView":
                    return new Size(300, 250);
                case "ResultView":
                    return new Size(300, 400);
                default:
                    return new Size(400, 300); // 기본 크기
            }
        }

        /// <summary>
        /// 창 키에 해당하는 창 제목을 반환합니다.
        /// </summary>
        /// <param name="windowKey">창 식별 키</param>
        /// <returns>창 제목</returns>
        private string GetWindowTitle(string windowKey)
        {
            switch (windowKey)
            {
                case "NodeEditor":
                    return "Node Editor";
                case "ImageControler":
                    return "Image Controler";
                case "LogView":
                    return "Log";
                case "NodeSelectedView":
                    return "Selected Node";
                case "NodeExecutionView":
                    return "Node Data";
                case "ResultView":
                    return "Result";
                default:
                    return "창";
            }
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// 리소스를 정리합니다.
        /// </summary>
        public void Dispose()
        {
            RemoveAllWindows();
        }

        #endregion
    }
}