using IFVisionEngine.UIComponents.CustomControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace IFVisionEngine.UIComponents.Common
{
    /// <summary>
    /// 줌과 패닝 기능을 제공하는 컨트롤러 클래스
    /// </summary>
    public class ZoomPanController
    {
        #region Constants
        private const float MIN_ZOOM = 0.1f;
        private const float MAX_ZOOM = 5.0f;
        private const float ZOOM_STEP = 1.2f;
        #endregion

        #region Private Fields
        private readonly Form _targetForm;
        private float _zoomFactor = 1.0f;
        private PointF _panOffset = PointF.Empty;
        private bool _isPanning = false;
        private Point _lastMousePosition;
        private Point _lastPanPosition = Point.Empty;
        private Dictionary<Control, ControlOriginalState> _originalStates = new Dictionary<Control, ControlOriginalState>();
        private float _accumulatedScale = 1.0f;
        #endregion

        #region Inner Classes
        private class ControlOriginalState
        {
            public Point Location { get; set; }
            public Size Size { get; set; }
        }
        #endregion

        #region Public Properties
        /// <summary>현재 줌 배율</summary>
        public float CurrentZoom => _zoomFactor;

        /// <summary>패닝 중인지 여부</summary>
        public bool IsPanning => _isPanning;
        #endregion

        #region Constructor
        public ZoomPanController(Form targetForm)
        {
            _targetForm = targetForm ?? throw new ArgumentNullException(nameof(targetForm));
            SetupEvents();
        }
        #endregion

        #region Event Setup
        private void SetupEvents()
        {
            _targetForm.MouseDown += OnFormMouseDown;
            _targetForm.MouseMove += OnFormMouseMove;
            _targetForm.MouseUp += OnFormMouseUp;
            _targetForm.MouseWheel += OnFormMouseWheel;
        }
        #endregion

        #region Public Methods
        /// <summary>줌인</summary>
        public void ZoomIn()
        {
            if (_targetForm != null)
            {
                ZoomAt(_targetForm.Width / 2, _targetForm.Height / 2, ZOOM_STEP);
            }
        }

        /// <summary>줌아웃</summary>
        public void ZoomOut()
        {
            if (_targetForm != null)
            {
                ZoomAt(_targetForm.Width / 2, _targetForm.Height / 2, 1f / ZOOM_STEP);
            }
        }

        /// <summary>줌 리셋</summary>
        public void ResetZoom()
        {
            foreach (Control control in _targetForm.Controls)
            {
                if (control is WindowWrapper windowWrapper)
                {
                    windowWrapper.ResetInnerControlScale();
                }
            }

            _zoomFactor = 1.0f;
            _panOffset = PointF.Empty;
            _accumulatedScale = 1.0f;
            _lastPanPosition = Point.Empty;
        }
        #endregion

        #region Private Methods
        /// <summary>특정 위치에서 줌 적용</summary>
        private void ZoomAt(float centerX, float centerY, float zoomStep)
        {
            float oldZoom = _zoomFactor;
            _zoomFactor = Math.Max(MIN_ZOOM, Math.Min(MAX_ZOOM, _zoomFactor * zoomStep));

            float actualZoomStep = _zoomFactor / oldZoom;

            // 줌 중심점을 기준으로 오프셋 재계산
            float newX = centerX - (centerX - _panOffset.X) * actualZoomStep;
            float newY = centerY - (centerY - _panOffset.Y) * actualZoomStep;

            float panDeltaX = newX - _panOffset.X;
            float panDeltaY = newY - _panOffset.Y;

            _panOffset = new PointF(newX, newY);

            // 상대적 변환 적용
            ApplyRelativeTransform(actualZoomStep, panDeltaX, panDeltaY);
        }

        /// <summary>상대적 변환 적용</summary>
        private void ApplyRelativeTransform(float zoomMultiplier, float panDeltaX, float panDeltaY)
        {
            if (_targetForm == null) return;

            // 스케일 팩터 누적
            _accumulatedScale *= zoomMultiplier;

            foreach (Control control in _targetForm.Controls)
            {
                // CustomTitleBar는 제외
                if (control is CustomTitleBar) continue;

                // 1. WindowWrapper 컨테이너 크기/위치 변경
                int newX = (int)(control.Location.X * zoomMultiplier + panDeltaX);
                int newY = (int)(control.Location.Y * zoomMultiplier + panDeltaY);
                int newWidth = (int)(control.Size.Width * zoomMultiplier);
                int newHeight = (int)(control.Size.Height * zoomMultiplier);

                control.Location = new Point(newX, newY);
                control.Size = new Size(newWidth, newHeight);

                // 2. WindowWrapper 내부 스케일링
                if (control is WindowWrapper windowWrapper)
                {
                    windowWrapper.ApplyInnerControlScale(_accumulatedScale);
                }
            }

            _targetForm.Invalidate();
        }

        /// <summary>패닝 적용</summary>
        private void ApplyPanning(float deltaX, float deltaY)
        {
            _panOffset = new PointF(_panOffset.X + deltaX, _panOffset.Y + deltaY);

            // 컨트롤들을 델타만큼 이동
            foreach (Control control in _targetForm.Controls)
            {
                // CustomTitleBar는 제외
                if (control is CustomTitleBar) continue;

                control.Location = new Point(
                    control.Location.X + (int)deltaX,
                    control.Location.Y + (int)deltaY
                );
            }

            _targetForm.Invalidate();
        }

        /// <summary>패닝 시작</summary>
        private void StartZoomPanning(Point mouseLocation)
        {
            _isPanning = true;
            _lastMousePosition = mouseLocation;
            if (_targetForm != null)
            {
                _targetForm.Cursor = Cursors.Hand;
            }
        }

        /// <summary>패닝 수행</summary>
        private void PerformZoomPanning(Point currentLocation)
        {
            if (!_isPanning) return;

            float deltaX = currentLocation.X - _lastMousePosition.X;
            float deltaY = currentLocation.Y - _lastMousePosition.Y;

            ApplyPanning(deltaX, deltaY);
            _lastMousePosition = currentLocation;
        }

        /// <summary>UserControl 위에 있는지 확인</summary>
        private bool IsMouseOverAnyUserControl(Point mouseLocation)
        {
            return FindAnyUserControlAtPosition(_targetForm, mouseLocation) != null;
        }

        /// <summary>위치에서 UserControl 찾기</summary>
        private UserControl FindAnyUserControlAtPosition(Control parent, Point mouseLocation)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is UserControl userControl && control.Visible)
                {
                    Rectangle bounds = GetAbsoluteBounds(userControl);
                    if (bounds.Contains(mouseLocation))
                    {
                        return userControl;
                    }
                }

                var found = FindAnyUserControlAtPosition(control, mouseLocation);
                if (found != null)
                {
                    return found;
                }
            }
            return null;
        }

        /// <summary>절대 좌표 계산</summary>
        private Rectangle GetAbsoluteBounds(Control control)
        {
            Point absoluteLocation = _targetForm.PointToClient(control.Parent.PointToScreen(control.Location));
            return new Rectangle(absoluteLocation, control.Size);
        }
        #endregion

        #region Event Handlers
        private void OnFormMouseDown(object sender, MouseEventArgs e)
        {
            // 중간 버튼 클릭 시 줌 패닝 시작
            if (e.Button == MouseButtons.Right)
            {
                StartZoomPanning(e.Location);
            }
        }

        private void OnFormMouseMove(object sender, MouseEventArgs e)
        {
            // 줌 패닝 중인 경우
            if (_isPanning)
            {
                PerformZoomPanning(e.Location);
            }
        }

        private void OnFormMouseUp(object sender, MouseEventArgs e)
        {
            // 줌 패닝 종료
            if (_isPanning && e.Button == MouseButtons.Right)
            {
                _isPanning = false;
                _targetForm.Cursor = Cursors.Default;
            }
        }

        /// <summary>마우스 휠 이벤트 - 줌인/줌아웃</summary>
        private void OnFormMouseWheel(object sender, MouseEventArgs e)
        {
            if (IsMouseOverAnyUserControl(e.Location))
            {
                return; // 사용자 컨트롤들의 기본 기능 우선
            }

            float zoomStep = e.Delta > 0 ? ZOOM_STEP : 1f / ZOOM_STEP;
            Point formPoint = _targetForm.PointToClient(Control.MousePosition);
            ZoomAt(formPoint.X, formPoint.Y, zoomStep);
        }
        #endregion

        #region Cleanup
        public void Dispose()
        {
            if (_targetForm != null)
            {
                _targetForm.MouseDown -= OnFormMouseDown;
                _targetForm.MouseMove -= OnFormMouseMove;
                _targetForm.MouseUp -= OnFormMouseUp;
                _targetForm.MouseWheel -= OnFormMouseWheel;
            }
            _originalStates.Clear();
        }
        #endregion
    }
}