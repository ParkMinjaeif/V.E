using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IFVisionEngine.UIComponents.Common;
using IFVisionEngine.UIComponents.Enums;
namespace IFVisionEngine.UIComponents.CustomControls
{
    /// <summary>
    /// 기존 UserControl을 창처럼 보이게 만드는 고급 래퍼 클래스
    /// 
    /// 주요 기능:
    /// - 커스텀 타이틀바 (최소화, 최대화, 닫기 버튼)
    /// - 드래그를 통한 창 이동
    /// - 8방향 크기 조절 (모서리 및 가장자리)
    /// - 최대화/복원 기능
    /// - 다크 테마 지원
    /// - Windows 표준 창 동작 구현
    /// </summary>
    public partial class WindowWrapper : UserControl
    {
        #region Constants
        /// <summary>창의 최소 너비</summary>
        private const int MIN_WINDOW_WIDTH = 200;
        /// <summary>창의 최소 높이</summary>
        private const int MIN_WINDOW_HEIGHT = 100;
        /// <summary>타이틀바 높이</summary>
        private const int TITLEBAR_HEIGHT = 30;
        /// <summary>기본 복원 크기</summary>
        private static readonly Size DEFAULT_RESTORE_SIZE = new Size(400, 300);

        #endregion

        #region Private Fields

        #region UI Components

        /// <summary>타이틀바를 담는 패널</summary>
        private Panel _titleBarPanel;
        /// <summary>실제 내용이 표시되는 패널</summary>
        private Panel _contentPanel;
        /// <summary>래핑할 실제 기능 컨트롤 (예: UcNodeEditor)</summary>
        private UserControl _innerControl;
        /// <summary>창 제목을 표시하는 라벨</summary>
        private Label _titleLabel;
        /// <summary>닫기 버튼</summary>
        private Button _closeButton;
        /// <summary>최대화/복원 버튼</summary>
        private Button _maximizeButton;
        /// <summary>최소화 버튼</summary>
        private Button _minimizeButton;

        #endregion

        #region State Management

        /// <summary>창 제목 저장용 백킹 필드</summary>
        private string _windowTitle = "창";
        /// <summary>현재 드래그 이동 중인지 나타내는 플래그</summary>
        private bool _isDragging = false;
        /// <summary>현재 크기 조절 중인지 나타내는 플래그</summary>
        private bool _isResizing = false;
        /// <summary>현재 창이 최대화 상태인지 나타내는 플래그</summary>
        private bool _isMaximized = false;

        #endregion

        #region Drag and Resize Data

        /// <summary>드래그 시작 시점의 마우스 화면 좌표</summary>
        private Point _dragStartPoint;
        /// <summary>크기 조절 시작 시점의 마우스 화면 좌표</summary>
        private Point _resizeStartPoint;
        /// <summary>크기 조절 시작 시점의 창 크기</summary>
        private Size _resizeStartSize;
        /// <summary>크기 조절 시작 시점의 창 위치</summary>
        private Point _resizeStartLocation;
        /// <summary>현재 크기 조절 방향</summary>
        private ResizeDirection _resizeDirection;
        /// <summary>복원 시 사용할 창의 위치와 크기 정보</summary>
        private Rectangle _restoreBounds;
        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// 창 제목
        /// null 안전성을 보장하며 타이틀 라벨과 동기화됩니다.
        /// </summary>
        public string WindowTitle
        {
            get => _titleLabel?.Text ?? _windowTitle;
            set
            {
                _windowTitle = value ?? string.Empty;
                if (_titleLabel != null)
                    _titleLabel.Text = _windowTitle;
            }
        }

        #endregion

        #region Events

        /// <summary>창이 닫힐 때 발생하는 이벤트</summary>
        public event EventHandler WindowClosing;
        /// <summary>창이 최소화될 때 발생하는 이벤트</summary>
        public event EventHandler WindowMinimized;
        /// <summary>창이 최대화될 때 발생하는 이벤트</summary>
        public event EventHandler WindowMaximized;
        /// <summary>창이 복원될 때 발생하는 이벤트</summary>
        public event EventHandler WindowRestored;

        #endregion
        
        #region Constructor

        /// <summary>
        /// WindowWrapper 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="title">창 제목</param>
        /// <param name="innerControl">래핑할 UserControl</param>
        /// <param name="initialSize">초기 창 크기</param>
        public WindowWrapper(string title, UserControl innerControl, Size initialSize)
        {
            if (innerControl == null)
                throw new ArgumentNullException(nameof(innerControl), "내부 컨트롤은 null일 수 없습니다.");

            ConfigureControlStyles();

            _innerControl = innerControl;

            InitializeWrapper(initialSize);
            CreateContentArea();
            SetupInnerControl();
            CreateTitleBar();
            SetupEvents();
            WindowTitle = title;
        }

        #endregion

        #region Initialization

        /// <summary>
        /// 컨트롤 스타일을 성능 최적화를 위해 설정합니다.
        /// </summary>
        private void ConfigureControlStyles()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.DoubleBuffer |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.Selectable, true);
            this.UpdateStyles();
        }

        /// <summary>
        /// 래퍼의 기본 속성을 초기화합니다.
        /// </summary>
        /// <param name="initialSize">초기 크기</param>
        private void InitializeWrapper(Size initialSize)
        {
            this.Size = initialSize;
            this.BorderStyle = BorderStyle.FixedSingle;

            // 다크 테마 적용
            ThemeHelper.ApplyDarkTheme(this);

            // 초기 복원 경계 설정
            _restoreBounds = new Rectangle(this.Location, this.Size);
        }
        


        #endregion

        #region UI Creation

        /// <summary>
        /// 타이틀바와 모든 버튼을 생성합니다.
        /// </summary>
        private void CreateTitleBar()
        {
            CreateTitleBarPanel();
            CreateTitleLabel();
            CreateWindowButtons();
            AssembleTitleBar();
        }

        /// <summary>
        /// 타이틀바 패널을 생성합니다.
        /// </summary>
        private void CreateTitleBarPanel()
        {
            _titleBarPanel = new Panel
            {
                Height = TITLEBAR_HEIGHT,
                Dock = DockStyle.Top
            };

            // 다크 테마 적용 (타이틀바 스타일)
            ThemeHelper.ApplyDarkTheme(_titleBarPanel, false);
        }

        /// <summary>
        /// 창 제목 라벨을 생성합니다.
        /// </summary>
        private void CreateTitleLabel()
        {
            _titleLabel = new Label
            {
                Text = _windowTitle,
                Location = new Point(8, 6),
                AutoSize = true,
                Font = ThemeHelper.GetTitleBarFont()
            };

            // 다크 테마 적용
            ThemeHelper.ApplyDarkTheme(_titleLabel);
        }

        /// <summary>
        /// 창 제어 버튼들 (최소화, 최대화, 닫기)을 생성합니다.
        /// </summary>
        private void CreateWindowButtons()
        {
            _closeButton = CreateTitleBarButton("✕", 30);
            _maximizeButton = CreateTitleBarButton("🗖", 55);
            _minimizeButton = CreateTitleBarButton("⎯", 80);

            ApplyButtonThemes();
        }

        /// <summary>
        /// 타이틀바 버튼을 생성합니다.
        /// </summary>
        /// <param name="text">버튼 텍스트</param>
        /// <param name="rightOffset">오른쪽에서의 거리</param>
        /// <returns>생성된 버튼</returns>
        private Button CreateTitleBarButton(string text, int rightOffset)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(25, 25),
                Location = new Point(_titleBarPanel.Width - rightOffset, 5),
                Font = ThemeHelper.GetButtonFont(),
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            return button;
        }

        /// <summary>
        /// 버튼들에 테마를 적용합니다.
        /// </summary>
        private void ApplyButtonThemes()
        {
            ThemeHelper.ApplyDarkTheme(_minimizeButton);
            ThemeHelper.ApplyDarkTheme(_maximizeButton);
            ThemeHelper.ApplyDarkTheme(_closeButton, true); // 닫기 버튼은 빨간색 호버
        }

        /// <summary>
        /// 타이틀바 컴포넌트들을 조립합니다.
        /// </summary>
        private void AssembleTitleBar()
        {
            _titleBarPanel.Controls.AddRange(new Control[] {
                _titleLabel, _closeButton, _maximizeButton, _minimizeButton
            });

            _contentPanel.Controls.Add(_titleBarPanel);
        }

        /// <summary>
        /// 내용 영역 패널을 생성합니다.
        /// </summary>
        private void CreateContentArea()
        {
            _contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(2)
            };

            // 다크 테마 적용 (내용 영역 스타일)
            ThemeHelper.ApplyDarkTheme(_contentPanel, true);

            this.Controls.Add(_contentPanel);
        }

        /// <summary>
        /// 내부 컨트롤을 내용 영역에 배치합니다.
        /// </summary>
        private void SetupInnerControl()
        {
            if (_innerControl != null)
            {
                _innerControl.Dock = DockStyle.Fill;
                _contentPanel.Controls.Add(_innerControl);
            }
        }

        #endregion

        #region Event Setup

        /// <summary>
        /// 모든 이벤트 핸들러를 설정합니다.
        /// </summary>
        private void SetupEvents()
        {
            SetupButtonEvents();
            SetupDragEvents();
            SetupResizeEvents();
        }

        /// <summary>
        /// 버튼 클릭 이벤트를 설정합니다.
        /// </summary>
        private void SetupButtonEvents()
        {
            _closeButton.Click += OnCloseButtonClick;
            _maximizeButton.Click += OnMaximizeButtonClick;
            _minimizeButton.Click += OnMinimizeButtonClick;
        }

        /// <summary>
        /// 드래그 이동 관련 이벤트를 설정합니다.
        /// </summary>
        private void SetupDragEvents()
        {
            _titleBarPanel.MouseDown += OnTitleBarMouseDown;
            _titleBarPanel.MouseMove += OnTitleBarMouseMove;
            _titleBarPanel.MouseUp += OnTitleBarMouseUp;
            _titleBarPanel.DoubleClick += OnTitleBarDoubleClick;
        }

        /// <summary>
        /// 크기 조절 관련 이벤트를 설정합니다.
        /// </summary>
        private void SetupResizeEvents()
        {
            _contentPanel.MouseDown += OnResizeMouseDown;
            _contentPanel.MouseMove += OnResizeMouseMove;
            _contentPanel.MouseUp += OnResizeMouseUp;
            _contentPanel.MouseLeave += OnContentPanelMouseLeave;
        }

        #endregion

        #region Event Handlers

        #region Button Events

        /// <summary>
        /// 닫기 버튼 클릭 이벤트를 처리합니다.
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인수</param>
        private void OnCloseButtonClick(object sender, EventArgs e)
        {
            WindowClosing?.Invoke(this, EventArgs.Empty);
            this.Visible = false;
        }

        /// <summary>
        /// 최대화 버튼 클릭 이벤트를 처리합니다.
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인수</param>
        private void OnMaximizeButtonClick(object sender, EventArgs e)
        {
            ToggleMaximize();
        }

        /// <summary>
        /// 최소화 버튼 클릭 이벤트를 처리합니다.
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인수</param>
        private void OnMinimizeButtonClick(object sender, EventArgs e)
        {
            MinimizeWindow();
        }

        #endregion

        #region Drag Events

        /// <summary>
        /// 타이틀바 마우스 다운 이벤트를 처리합니다.
        /// 드래그 이동을 시작합니다.
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">마우스 이벤트 인수</param>
        private void OnTitleBarMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !_isMaximized)
            {
                StartDragging();
            }
        }

        /// <summary>
        /// 드래그를 시작합니다.
        /// </summary>
        private void StartDragging()
        {
            _isDragging = true;
            _dragStartPoint = Control.MousePosition;
            this.BringToFront();
        }

        /// <summary>
        /// 타이틀바 마우스 이동 이벤트를 처리합니다.
        /// 드래그 이동을 수행합니다.
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">마우스 이벤트 인수</param>
        private void OnTitleBarMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                PerformDragMove();
            }
        }

        /// <summary>
        /// 드래그 이동을 수행합니다.
        /// </summary>
        private void PerformDragMove()
        {
            Point currentScreenPos = Control.MousePosition;
            Point delta = new Point(
                currentScreenPos.X - _dragStartPoint.X,
                currentScreenPos.Y - _dragStartPoint.Y
            );

            this.Location = new Point(
                this.Location.X + delta.X,
                this.Location.Y + delta.Y
            );

            _dragStartPoint = currentScreenPos;

            // 이동 중 복원 정보 업데이트
            if (!_isMaximized)
            {
                _restoreBounds = new Rectangle(this.Location, this.Size);
            }
        }

        /// <summary>
        /// 타이틀바 마우스 업 이벤트를 처리합니다.
        /// 드래그를 종료합니다.
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">마우스 이벤트 인수</param>
        private void OnTitleBarMouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        /// <summary>
        /// 타이틀바 더블클릭 이벤트를 처리합니다.
        /// 최대화/복원을 토글합니다.
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인수</param>
        private void OnTitleBarDoubleClick(object sender, EventArgs e)
        {
            ToggleMaximize();
        }

        #endregion

        #region Resize Events

        /// <summary>
        /// 크기 조절 마우스 다운 이벤트를 처리합니다.
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">마우스 이벤트 인수</param>
        private void OnResizeMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !_isMaximized)
            {
                TryStartResize(e.Location);
            }
        }

        /// <summary>
        /// 크기 조절을 시작할 수 있는지 확인하고 시작합니다.
        /// </summary>
        /// <param name="mouseLocation">마우스 위치</param>
        private void TryStartResize(Point mouseLocation)
        {
            _resizeDirection = GetResizeDirection(mouseLocation);
            if (_resizeDirection != ResizeDirection.None)
            {
                StartResize();
            }
        }

        /// <summary>
        /// 크기 조절을 시작합니다.
        /// </summary>
        private void StartResize()
        {
            _isResizing = true;
            _resizeStartPoint = Control.MousePosition;
            _resizeStartSize = this.Size;
            _resizeStartLocation = this.Location;
            this.BringToFront();
        }

        /// <summary>
        /// 크기 조절 마우스 이동 이벤트를 처리합니다.
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">마우스 이벤트 인수</param>
        private void OnResizeMouseMove(object sender, MouseEventArgs e)
        {
            if (_isResizing)
            {
                PerformResize();
            }
            else if (!_isMaximized)
            {
                UpdateCursorForResize(e.Location);
            }
        }

        /// <summary>
        /// 크기 조절 마우스 업 이벤트를 처리합니다.
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">마우스 이벤트 인수</param>
        private void OnResizeMouseUp(object sender, MouseEventArgs e)
        {
            _isResizing = false;
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 마우스가 내용 패널을 벗어날 때 커서를 리셋합니다.
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인수</param>
        private void OnContentPanelMouseLeave(object sender, EventArgs e)
        {
            if (!_isResizing)
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #endregion

        #region Resize Logic

        /// <summary>
        /// 마우스 위치에 따른 크기 조절 커서를 업데이트합니다.
        /// </summary>
        /// <param name="mousePos">마우스 위치 (컨트롤 상대 좌표)</param>
        private void UpdateCursorForResize(Point mousePos)
        {
            ResizeDirection direction = GetResizeDirection(mousePos);
            SetResizeCursor(direction);
        }

        /// <summary>
        /// 크기 조절 방향에 따른 커서를 설정합니다.
        /// </summary>
        /// <param name="direction">크기 조절 방향</param>
        private void SetResizeCursor(ResizeDirection direction)
        {
            if (_isMaximized)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            Cursor newCursor = ResizeHelper.GetCursorForDirection(direction);

            if (this.Cursor != newCursor)
            {
                this.Cursor = newCursor;
            }
        }


        /// <summary>
        /// 마우스 위치에 따른 크기 조절 방향을 결정합니다.
        /// </summary>
        /// <param name="mousePos">마우스 위치 (컨트롤 상대 좌표)</param>
        /// <returns>크기 조절 방향</returns>
        private ResizeDirection GetResizeDirection(Point mousePos)
        {
            if (_isMaximized) return ResizeDirection.None;

            return ResizeHelper.GetResizeDirection(mousePos, this.Size, TITLEBAR_HEIGHT);
        }


        /// <summary>
        /// 실제 크기 조절을 수행합니다.
        /// 현재 마우스 위치와 시작 위치의 차이를 계산하여 새로운 크기와 위치를 결정합니다.
        /// </summary>
        private void PerformResize()
        {
            Point currentScreenPos = Control.MousePosition;
            int deltaX = currentScreenPos.X - _resizeStartPoint.X;
            int deltaY = currentScreenPos.Y - _resizeStartPoint.Y;

            Rectangle newBounds = CalculateNewBounds(deltaX, deltaY);
            this.Bounds = newBounds;

            // 크기 조절 중 복원 정보 업데이트
            _restoreBounds = newBounds;
        }

        /// <summary>
        /// 델타 값을 기반으로 새로운 경계를 계산합니다.
        /// </summary>
        /// <param name="deltaX">X축 이동 거리</param>
        /// <param name="deltaY">Y축 이동 거리</param>
        /// <returns>새로운 경계 Rectangle</returns>
        private Rectangle CalculateNewBounds(int deltaX, int deltaY)
        {
            return ResizeHelper.CalculateNewBounds(
                _resizeDirection,
                deltaX, deltaY,
                _resizeStartLocation, _resizeStartSize,
                MIN_WINDOW_WIDTH, MIN_WINDOW_HEIGHT);
        }


        public void ApplyInnerControlScale(float scaleFactor)
        {

        }

        #endregion

        #region Maximize/Restore Logic

        /// <summary>
        /// 최대화와 복원을 토글합니다.
        /// </summary>
        private void ToggleMaximize()
        {
            if (_isMaximized)
            {
                RestoreWindow();
            }
            else
            {
                MaximizeWindow();
            }
        }

        /// <summary>
        /// 창을 최대화합니다.
        /// 현재 상태를 복원 정보로 저장하고 부모 컨테이너에 맞춰 확장합니다.
        /// </summary>
        private void MaximizeWindow()
        {
            if (_isMaximized) return;

            // 현재 상태를 복원용으로 저장
            SaveCurrentBounds();

            // 부모 컨테이너에 맞춰 최대화
            if (this.Parent != null)
            {
                this.Dock = DockStyle.Fill;
                _isMaximized = true;
                UpdateMaximizeButtonIcon();

                WindowMaximized?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 창을 복원합니다.
        /// 저장된 복원 정보를 사용하여 원래 크기와 위치로 돌립니다.
        /// </summary>
        private void RestoreWindow()
        {
            if (!_isMaximized) return;

            this.Dock = DockStyle.None;

            // 저장된 크기와 위치로 복원
            if (_restoreBounds != Rectangle.Empty)
            {
                this.Bounds = _restoreBounds;
            }
            else
            {
                // 복원 정보가 없으면 기본 크기로
                this.Size = DEFAULT_RESTORE_SIZE;
                this.Location = new Point(50, 50);
            }

            _isMaximized = false;
            UpdateMaximizeButtonIcon();

            WindowRestored?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 창을 최소화합니다. (숨김 처리)
        /// </summary>
        private void MinimizeWindow()
        {
            this.Visible = false;
            WindowMinimized?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 현재 창의 위치와 크기를 복원 정보로 저장합니다.
        /// </summary>
        private void SaveCurrentBounds()
        {
            if (!_isMaximized && this.Dock == DockStyle.None)
            {
                _restoreBounds = new Rectangle(this.Location, this.Size);
            }
        }

        /// <summary>
        /// 최대화 버튼의 아이콘을 현재 상태에 맞게 업데이트합니다.
        /// </summary>
        private void UpdateMaximizeButtonIcon()
        {
            _maximizeButton.Text = _isMaximized ? "🗗" : "🗖";
        }

        #endregion
    }
}