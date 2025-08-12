using IFVisionEngine.Manager;
using IFVisionEngine.UIComponents.CustomControls;
using IFVisionEngine.UI.Views;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IFVisionEngine.UIComponents.Enums;
using IFVisionEngine.UIComponents.Common;

public class CustomTitleBar : BaseTitleBar
{
    #region Constants

    /// <summary>윈도우 캡션 드래그를 위한 Windows API 상수</summary>
    private const int WM_NCLBUTTONDOWN = 0xA1;
    /// <summary>캡션 영역을 나타내는 히트 테스트 상수</summary>
    private const int HTCAPTION = 0x2;
    /// <summary>폼의 최소 너비</summary>
    private const int MIN_FORM_WIDTH = 300;
    /// <summary>폼의 최소 높이</summary>
    private const int MIN_FORM_HEIGHT = 200;

    #endregion


    #region Win32 API Declarations

    /// <summary>마우스 캡처를 해제하는 Windows API</summary>
    [DllImport("user32.dll")]
    private static extern bool ReleaseCapture();

    /// <summary>윈도우에 메시지를 전송하는 Windows API</summary>
    /// <param name="hWnd">대상 윈도우 핸들</param>
    /// <param name="Msg">메시지 코드</param>
    /// <param name="wParam">추가 메시지 정보</param>
    /// <param name="lParam">추가 메시지 정보</param>
    /// <returns>메시지 처리 결과</returns>
    [DllImport("user32.dll")]
    private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    #endregion


    #region Private Fields

    #region UI Components

    /// <summary>부모 폼 참조 (드래그 이동 및 크기 조절 대상)</summary>
    private Form _parentForm;

    #endregion

    #region Window Management

    /// <summary>동적 창 관리를 위한 컨텍스트 메뉴</summary>
    private ContextMenuStrip _viewContextMenu;

    /// <summary>창 관리자 인스턴스</summary>
    private WindowManager _windowManager;

    #endregion

    #region Resize State Management

    /// <summary>현재 크기 조절 중인지 나타내는 플래그</summary>
    private bool _isResizing = false;
    /// <summary>크기 조절 시작 시점의 마우스 화면 좌표</summary>
    private Point _resizeStartPoint;
    /// <summary>크기 조절 시작 시점의 폼 크기</summary>
    private Size _resizeStartSize;
    /// <summary>크기 조절 시작 시점의 폼 위치</summary>
    private Point _resizeStartLocation;
    /// <summary>현재 크기 조절 방향</summary>
    private ResizeDirection _resizeDirection;

    #endregion

    #region Maximize/Restore State Management

    /// <summary>복원 시 사용할 폼의 위치와 크기 정보</summary>
    private Rectangle _restoreBounds;
    /// <summary>현재 폼이 최대화 상태인지 나타내는 플래그</summary>
    private bool _isFormMaximized = false;

    #endregion
    public ZoomPanController ZoomController { get; set; }
    #endregion

    #region Public Properties

    public WindowManager WindowManager => _windowManager;

    /// <summary>
    /// 부모 폼 참조 (드래그 이동 및 창 제어 대상)
    /// </summary>
    public new Form ParentForm
    {
        get => _parentForm;
        set => _parentForm = value;
    }
    public string FileName
    {
        get => TitleText;
        set => TitleText = value;
    }


    #endregion

    #region Constructor

    /// <summary>
    /// CustomTitleBar 인스턴스를 초기화합니다.
    /// 모든 UI 컴포넌트를 생성하고 이벤트를 설정합니다.
    /// </summary>
    public CustomTitleBar()
    {
        CreateViewMenu();

        // 부모가 설정된 후에 이벤트 연결
        this.ParentChanged += (s, e) => {
            if (this.Parent != null)
            {
                SetupEvents();
            }
        };
    }

    #endregion

    #region Initialization Methods   

    protected override void OnMinimizeClick()
    {
        if (_parentForm != null)
        {
            _parentForm.WindowState = FormWindowState.Minimized;
        }
    }

    protected override void OnMaximizeClick()
    {
        ToggleMaximizeRestore();
    }

    protected override void OnCloseClick()
    {
        if (_parentForm != null)
        {
            _parentForm.Close();
        }
    }

    protected override void OnTitleBarMouseDown(MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left || _parentForm == null) return;

        var parentForm = this.FindForm();
        if (parentForm == null) return;

        // 최대화 상태에서 드래그 시작하면 스마트 복원
        if (parentForm.WindowState == FormWindowState.Maximized)
        {
            PerformSmartRestore(parentForm);
        }

        // 드래그 이동 시작
        ReleaseCapture();
        SendMessage(_parentForm.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
    }

    protected override void OnTitleBarDoubleClick()
    {
        ToggleMaximizeRestore();
    }
    /// <summary>
    /// 창 제어 버튼들의 위치를 업데이트합니다.
    /// 오른쪽부터 역순으로 배치합니다.
    /// </summary>
    protected new virtual void UpdateButtonPositions()
    {
        const int buttonSpacing = 40;
        const int rightMargin = 5;
        const int topMargin = 5;

        _closeButton.Location = new Point(this.Width - (rightMargin + buttonSpacing), topMargin);
        _maximizeButton.Location = new Point(this.Width - (rightMargin + buttonSpacing * 2), topMargin);
        _minimizeButton.Location = new Point(this.Width - (rightMargin + buttonSpacing * 3), topMargin);
    }

    #endregion


    #region View Menu Management

    /// <summary>
    /// 동적 창 관리를 위한 "보기" 메뉴를 생성합니다.
    /// 파라미터 폼에서는 생성하지 않습니다.
    /// </summary>
    private void CreateViewMenu()
    {
        // 파라미터 폼에서는 보기 메뉴 생성 안함
        if (this.FileName == "IF Vision Engine Parameter")
        {
            return;
        }

        CreateViewLabel();
        CreateViewContextMenu();
    }
    private WindowManager GetWindowManager()
    {
        if (_windowManager == null)
        {
            InitializeWindowManager();
        }
        return _windowManager;
    }
    /// <summary>
    /// "보기" 라벨을 생성하고 이벤트를 연결합니다.
    /// </summary>
    private void CreateViewLabel()
    {
        var viewLabel = new Label
        {
            Text = "보기",
            Size = new Size(35, 30),
            Font = ThemeHelper.GetTitleBarFont(),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(150, 5),
            BackColor = Color.Transparent,
            Cursor = Cursors.Hand
        };

        // 다크 테마 적용
        ThemeHelper.ApplyDarkTheme(viewLabel);

        SetupViewLabelEvents(viewLabel);
        this.Controls.Add(viewLabel);
    }

    /// <summary>
    /// 보기 라벨의 마우스 이벤트를 설정합니다.
    /// </summary>
    /// <param name="viewLabel">이벤트를 설정할 라벨</param>
    private void SetupViewLabelEvents(Label viewLabel)
    {
        viewLabel.MouseEnter += (s, e) => viewLabel.BackColor = ThemeHelper.ButtonHover;
        viewLabel.MouseLeave += (s, e) => viewLabel.BackColor = Color.Transparent;
        viewLabel.MouseDown += (s, e) => viewLabel.BackColor = ThemeHelper.ButtonPressed;
        viewLabel.MouseUp += (s, e) => viewLabel.BackColor = ThemeHelper.ButtonHover;

        viewLabel.Click += (s, e) =>
        {
            UpdateWindowStatus();
            _viewContextMenu.Show(viewLabel, new Point(0, viewLabel.Height));
        };
    }

    /// <summary>
    /// 보기 메뉴의 컨텍스트 메뉴를 생성합니다.
    /// </summary>
    private void CreateViewContextMenu()
    {
        _viewContextMenu = new ContextMenuStrip();

        // 다크 테마 적용
        ThemeHelper.ApplyDarkTheme(_viewContextMenu);

        CreateViewMenuItems();
    }

    /// <summary>
    /// 창 관리 딕셔너리를 초기화합니다.
    /// </summary>
    private void InitializeWindowManager()
    {
        var parentForm = this.FindForm();
        if (parentForm != null)
        {
            _windowManager = new WindowManager(parentForm);
            System.Diagnostics.Debug.WriteLine("WindowManager initialized successfully");
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Parent form is null, WindowManager not initialized");
        }
    }

    /// <summary>
    /// 보기 메뉴의 개별 항목들을 생성합니다.
    /// </summary>
    private void CreateViewMenuItems()
    {
        var windowDefinitions = GetWindowDefinitions();

        foreach (var window in windowDefinitions)
        {
            var menuItem = new ToolStripMenuItem
            {
                Text = window.DisplayName,
                Tag = window.Key,
                CheckOnClick = true,
                BackColor = ThemeHelper.DarkBackground,
                ForeColor = Color.White
            };

            menuItem.Click += OnViewMenuItemClick;
            _viewContextMenu.Items.Add(menuItem);
        }
    }

    /// <summary>
    /// 창 정의 정보를 반환합니다.
    /// </summary>
    /// <returns>창 정의 정보 배열</returns>
    private (string DisplayName, string Key)[] GetWindowDefinitions()
    {
        return new[]
        {
            ("Node Editor", "NodeEditor"),
            ("Image Controler", "ImageControler"),
            ("Log", "LogView"),
            ("Selected Node", "NodeSelectedView"),
            ("Data View", "NodeExecutionView"),
            ("Result", "ResultView")
        };
    }

    /// <summary>
    /// 보기 메뉴 항목 클릭 이벤트를 처리합니다.
    /// </summary>
    /// <param name="sender">이벤트 발생자</param>
    /// <param name="e">이벤트 인수</param>
    private void OnViewMenuItemClick(object sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem menuItem && _windowManager != null)
        {
            string windowKey = menuItem.Tag.ToString();
            var windowManager = GetWindowManager();
            // 디버깅용 로그
            System.Diagnostics.Debug.WriteLine($"Menu clicked: {windowKey}, Checked: {menuItem.Checked}");

            if (menuItem.Checked && windowManager != null)
            {
                var window = _windowManager.ShowWindow(windowKey);
                System.Diagnostics.Debug.WriteLine($"Window created/shown: {window != null}");
            }
            else
            {
                bool hidden = _windowManager.HideWindow(windowKey);
                System.Diagnostics.Debug.WriteLine($"Window hidden: {hidden}");
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("WindowManager is null or sender is not ToolStripMenuItem");
        }
    }

    /// <summary>
    /// 현재 창들의 표시 상태를 업데이트합니다.
    /// </summary>
    public void UpdateWindowStatus()
    {
        var windowManager = GetWindowManager(); // 이 라인 추가
        if (windowManager == null || _viewContextMenu == null) 
            return;

        foreach (ToolStripMenuItem item in _viewContextMenu.Items)
        {
            string windowKey = item.Tag.ToString();
            // 창이 존재하고 표시되어 있으면 체크
            item.Checked = _windowManager.WindowExists(windowKey) && _windowManager.IsWindowVisible(windowKey);
        }
    }

    #endregion

    #region Event Setup

    /// <summary>
    /// 모든 이벤트 핸들러를 설정합니다.
    /// </summary>
    protected override void SetupEvents()
    {
        SetupUIEvents();
        SetupFormEvents();
    }

    /// <summary>
    /// UI 관련 이벤트를 설정합니다.
    /// </summary>
    private void SetupUIEvents()
    {
        // 크기 변경시 버튼 위치 업데이트
        this.SizeChanged += (s, e) => UpdateButtonPositions();

        // 타이틀바 드래그 이동 이벤트
        SetupDragEvents();

        // 타이틀바 더블클릭 이벤트
        SetupDoubleClickEvents();

        // 버튼 클릭 이벤트
        SetupButtonEvents();
    }

    /// <summary>
    /// 드래그 이동 관련 이벤트를 설정합니다.
    /// </summary>
    private void SetupDragEvents()
    {
        this.MouseDown += OnTitleBarMouseDown;
        _titleLabel.MouseDown += OnTitleBarMouseDown;
        _iconPictureBox.MouseDown += OnTitleBarMouseDown;
    }

    /// <summary>
    /// 더블클릭 관련 이벤트를 설정합니다.
    /// </summary>
    private void SetupDoubleClickEvents()
    {
        this.DoubleClick += OnTitleBarDoubleClick;
        _titleLabel.DoubleClick += OnTitleBarDoubleClick;
    }

    /// <summary>
    /// 버튼 클릭 이벤트를 설정합니다.
    /// </summary>
    private void SetupButtonEvents()
    {
        _minimizeButton.Click += OnMinimizeClick;
        _maximizeButton.Click += OnMaximizeClick;
        _closeButton.Click += OnCloseClick;
    }

    /// <summary>
    /// 폼 관련 이벤트를 설정합니다.
    /// </summary>
    private void SetupFormEvents()
    {
        var parentForm = this.FindForm();
        if (parentForm == null) return;

        SetupResizeEvents(parentForm);
        SetupFormStateEvents(parentForm);

        // 초기 상태 설정
        UpdateMaximizeState(parentForm);
    }

    /// <summary>
    /// 크기 조절 관련 이벤트를 설정합니다.
    /// </summary>
    /// <param name="parentForm">대상 폼</param>
    private void SetupResizeEvents(Form parentForm)
    {
        parentForm.MouseDown += OnFormMouseDown;
        parentForm.MouseMove += OnFormMouseMove;
        parentForm.MouseUp += OnFormMouseUp;
        parentForm.MouseLeave += (s, e) => {
            if (!_isResizing)
                parentForm.Cursor = Cursors.Default;
        };
    }

    /// <summary>
    /// 폼 상태 변경 관련 이벤트를 설정합니다.
    /// </summary>
    /// <param name="parentForm">대상 폼</param>
    private void SetupFormStateEvents(Form parentForm)
    {
        parentForm.Resize += OnFormResize;
        parentForm.LocationChanged += OnFormLocationChanged;
    }

    #endregion

    #region Event Handlers

    #region Title Bar Events

    /// <summary>
    /// 타이틀바 마우스 다운 이벤트를 처리합니다.
    /// 최대화 상태에서 드래그 시 스마트 복원을 수행합니다.
    /// </summary>
    /// <param name="sender">이벤트 발생자</param>
    /// <param name="e">마우스 이벤트 인수</param>
    private void OnTitleBarMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left || _parentForm == null) return;

        var parentForm = this.FindForm();
        if (parentForm == null) return;

        // 최대화 상태에서 드래그 시작하면 스마트 복원
        if (parentForm.WindowState == FormWindowState.Maximized)
        {
            PerformSmartRestore(parentForm);
        }

        // 드래그 이동 시작
        ReleaseCapture();
        SendMessage(_parentForm.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
    }

    /// <summary>
    /// 최대화 상태에서 드래그 시 스마트 복원을 수행합니다.
    /// 마우스 위치에 맞춰 창을 적절히 배치합니다.
    /// </summary>
    /// <param name="parentForm">대상 폼</param>
    private void PerformSmartRestore(Form parentForm)
    {
        RestoreForm();

        // 마우스 위치에 맞춰 창 위치 조정
        Point mousePos = Control.MousePosition;
        int newX = mousePos.X - (parentForm.Width / 2);
        int newY = mousePos.Y - (this.Height / 2);

        // 화면 경계를 벗어나지 않도록 조정
        var screen = Screen.FromPoint(mousePos);
        newX = Math.Max(0, Math.Min(newX, screen.WorkingArea.Width - parentForm.Width));
        newY = Math.Max(0, Math.Min(newY, screen.WorkingArea.Height - parentForm.Height));

        parentForm.Location = new Point(newX, newY);
    }

    /// <summary>
    /// 타이틀바 더블클릭 이벤트를 처리합니다.
    /// 최대화/복원을 토글합니다.
    /// </summary>
    /// <param name="sender">이벤트 발생자</param>
    /// <param name="e">이벤트 인수</param>
    private void OnTitleBarDoubleClick(object sender, EventArgs e)
    {
        ToggleMaximizeRestore();
    }

    #endregion

    #region Button Events

    /// <summary>
    /// 최소화 버튼 클릭 이벤트를 처리합니다.
    /// </summary>
    /// <param name="sender">이벤트 발생자</param>
    /// <param name="e">이벤트 인수</param>
    private void OnMinimizeClick(object sender, EventArgs e)
    {
        if (_parentForm != null)
        {
            _parentForm.WindowState = FormWindowState.Minimized;
        }
    }

    /// <summary>
    /// 최대화/복원 버튼 클릭 이벤트를 처리합니다.
    /// </summary>
    /// <param name="sender">이벤트 발생자</param>
    /// <param name="e">이벤트 인수</param>
    private void OnMaximizeClick(object sender, EventArgs e)
    {
        ToggleMaximizeRestore();
    }

    /// <summary>
    /// 닫기 버튼 클릭 이벤트를 처리합니다.
    /// </summary>
    /// <param name="sender">이벤트 발생자</param>
    /// <param name="e">이벤트 인수</param>
    private void OnCloseClick(object sender, EventArgs e)
    {
        if (_parentForm != null)
        {
            _parentForm.Close();
        }
    }

    #endregion

    #region Form State Events

    /// <summary>
    /// 폼 크기 변경 이벤트를 처리합니다.
    /// 최대화 상태를 업데이트하고 버튼 아이콘을 변경합니다.
    /// </summary>
    /// <param name="sender">이벤트 발생자</param>
    /// <param name="e">이벤트 인수</param>
    private void OnFormResize(object sender, EventArgs e)
    {
        if (sender is Form form)
        {
            UpdateMaximizeState(form);
            UpdateMaximizeButtonIcon();
        }
    }

    /// <summary>
    /// 폼 위치 변경 이벤트를 처리합니다.
    /// 최대화 상태를 업데이트합니다.
    /// </summary>
    /// <param name="sender">이벤트 발생자</param>
    /// <param name="e">이벤트 인수</param>
    private void OnFormLocationChanged(object sender, EventArgs e)
    {
        if (sender is Form form)
        {
            UpdateMaximizeState(form);
        }
    }

    #endregion
    
    #region Resize Events

    /// <summary>
    /// 폼 마우스 다운 이벤트를 처리합니다.
    /// 크기 조절을 시작합니다.
    /// </summary>
    /// <param name="sender">이벤트 발생자</param>
    /// <param name="e">마우스 이벤트 인수</param>
    private void OnFormMouseDown(object sender, MouseEventArgs e)
    {
        var form = sender as Form;
        if (form == null) return;

        // 기존 크기 조절 로직 (왼쪽 버튼)
        if (e.Button == MouseButtons.Left && form.WindowState == FormWindowState.Normal)
        {
            _resizeDirection = GetResizeDirection(e.Location, form);
            if (_resizeDirection != ResizeDirection.None)
            {
                StartResize(form);
            }
        }
    }

    /// <summary>
    /// 크기 조절을 시작합니다.
    /// </summary>
    /// <param name="form">대상 폼</param>
    private void StartResize(Form form)
    {
        _isResizing = true;
        _resizeStartPoint = Control.MousePosition;
        _resizeStartSize = form.Size;
        _resizeStartLocation = form.Location;

        // 복원 정보 업데이트
        _restoreBounds = new Rectangle(form.Location, form.Size);
    }

    /// <summary>
    /// 폼 마우스 이동 이벤트를 처리합니다.
    /// 크기 조절 또는 커서 변경을 수행합니다.
    /// </summary>
    /// <param name="sender">이벤트 발생자</param>
    /// <param name="e">마우스 이벤트 인수</param>
    private void OnFormMouseMove(object sender, MouseEventArgs e)
    {
        var form = sender as Form;
        if (form == null) return;

        // 크기 조절 중인 경우
        if (_isResizing)
        {
            PerformResize(form);
            return;
        }

        // 커서 변경 (크기 조절 영역 감지)
        var direction = GetResizeDirection(e.Location, form);
        SetResizeCursor(direction, form);
    }

    /// <summary>
    /// 폼 마우스 업 이벤트를 처리합니다.
    /// 크기 조절을 종료합니다.
    /// </summary>
    /// <param name="sender">이벤트 발생자</param>
    /// <param name="e">마우스 이벤트 인수</param>
    private void OnFormMouseUp(object sender, MouseEventArgs e)
    {
        var form = sender as Form;
        if (form == null) return;

        // 크기 조절 종료
        if (_isResizing && e.Button == MouseButtons.Left)
        {
            _isResizing = false;
            form.Cursor = Cursors.Default;
        }
    }

    #endregion

    #endregion

    #region Maximize/Restore Methods

    /// <summary>
    /// 최대화와 복원을 토글합니다.
    /// </summary>
    private void ToggleMaximizeRestore()
    {
        var parentForm = this.FindForm();
        if (parentForm == null) return;

        if (parentForm.WindowState == FormWindowState.Maximized)
        {
            RestoreForm();
        }
        else
        {
            MaximizeForm();
        }
    }

    /// <summary>
    /// 폼을 최대화합니다.
    /// 현재 상태를 복원 정보로 저장합니다.
    /// </summary>
    private void MaximizeForm()
    {
        var parentForm = this.FindForm();
        if (parentForm?.WindowState == FormWindowState.Maximized) return;

        // 현재 상태를 복원용으로 저장
        SaveCurrentBounds(parentForm);

        // 최대화 실행
        parentForm.WindowState = FormWindowState.Maximized;
        UpdateMaximizeButtonIcon();
    }

    /// <summary>
    /// 현재 폼의 위치와 크기를 복원 정보로 저장합니다.
    /// </summary>
    /// <param name="form">대상 폼</param>
    private void SaveCurrentBounds(Form form)
    {
        if (form != null)
        {
            _restoreBounds = new Rectangle(form.Location, form.Size);
        }
    }

    /// <summary>
    /// 폼을 복원합니다.
    /// 저장된 복원 정보를 사용하여 원래 크기와 위치로 돌립니다.
    /// </summary>
    private void RestoreForm()
    {
        var parentForm = this.FindForm();
        if (parentForm?.WindowState != FormWindowState.Maximized) return;

        // 일반 상태로 복원
        parentForm.WindowState = FormWindowState.Normal;

        // 저장된 크기와 위치로 복원
        RestoreBounds(parentForm);
        UpdateMaximizeButtonIcon();

    }

    /// <summary>
    /// 저장된 복원 정보로 폼의 위치와 크기를 복원합니다.
    /// </summary>
    /// <param name="form">대상 폼</param>
    private void RestoreBounds(Form form)
    {
        if (_restoreBounds != Rectangle.Empty)
        {
            form.Bounds = _restoreBounds;
        }
    }

    /// <summary>
    /// 최대화 버튼의 아이콘을 현재 상태에 맞게 업데이트합니다.
    /// </summary>
    private void UpdateMaximizeButtonIcon()
    {
        var parentForm = this.FindForm();
        if (parentForm == null) return;

        _maximizeButton.Text = parentForm.WindowState == FormWindowState.Maximized
            ? "🗗"  // 복원 아이콘 (두 개의 겹친 사각형)
            : "🗖"; // 최대화 아이콘 (단일 사각형)
    }

    /// <summary>
    /// 폼의 최대화 상태를 업데이트하고 상태 변경을 감지합니다.
    /// </summary>
    /// <param name="form">대상 폼</param>
    private void UpdateMaximizeState(Form form)
    {
        if (form == null) return;

        bool wasMaximized = _isFormMaximized;
        _isFormMaximized = form.WindowState == FormWindowState.Maximized;

        // 상태가 변경되었을 때만 처리
        if (wasMaximized != _isFormMaximized)
        {
            HandleStateChange(form, wasMaximized);
        }
    }

    /// <summary>
    /// 폼 상태 변경을 처리합니다.
    /// </summary>
    /// <param name="form">대상 폼</param>
    /// <param name="wasMaximized">이전 최대화 상태</param>
    private void HandleStateChange(Form form, bool wasMaximized)
    {
        if (_isFormMaximized)
        {
            OnFormMaximized(form);
        }
        else if (wasMaximized && form.WindowState == FormWindowState.Normal)
        {
            OnFormRestored(form);
        }
    }

    /// <summary>
    /// 폼이 최대화되었을 때 호출됩니다.
    /// 크기 조절을 비활성화하고 상태를 초기화합니다.
    /// </summary>
    /// <param name="form">대상 폼</param>
    private void OnFormMaximized(Form form)
    {
        // 최대화 시 복원 정보 저장 (WindowState 변경 전에 저장되지 않은 경우)
        if (form.WindowState != FormWindowState.Maximized)
        {
            SaveCurrentBounds(form);
        }

        // 크기 조절 비활성화
        DisableResize(form);

    }

    /// <summary>
    /// 크기 조절을 비활성화합니다.
    /// </summary>
    /// <param name="form">대상 폼</param>
    private void DisableResize(Form form)
    {
        form.Cursor = Cursors.Default;
        _isResizing = false;
    }

    /// <summary>
    /// 폼이 복원되었을 때 호출됩니다.
    /// </summary>
    /// <param name="form">대상 폼</param>
    private void OnFormRestored(Form form)
    {
        Console.WriteLine("폼이 복원되었습니다.");
    }

    #endregion

    #region Resize Logic

    /// <summary>
    /// 마우스 위치에 따른 크기 조절 커서를 업데이트합니다.
    /// </summary>
    /// <param name="mousePos">마우스 위치</param>
    /// <param name="form">대상 폼</param>
    private void UpdateCursorForResize(Point mousePos, Form form)
    {
        ResizeDirection direction = GetResizeDirection(mousePos, form);
        SetResizeCursor(direction, form);
    }

    /// <summary>
    /// 크기 조절 방향에 따른 커서를 설정합니다.
    /// </summary>
    /// <param name="direction">크기 조절 방향</param>
    /// <param name="form">대상 폼</param>
    private void SetResizeCursor(ResizeDirection direction, Form form)
    {
        // 최대화 상태에서는 기본 커서만 표시
        if (form.WindowState != FormWindowState.Normal)
        {
            form.Cursor = Cursors.Default;
            return;
        }

        Cursor newCursor = ResizeHelper.GetCursorForDirection(direction);

        if (form.Cursor != newCursor)
        {
            form.Cursor = newCursor;
        }
    }


    /// <summary>
    /// 마우스 위치에 따른 크기 조절 방향을 결정합니다.
    /// </summary>
    /// <param name="mousePos">마우스 위치 (폼 내 상대 좌표)</param>
    /// <param name="form">대상 폼</param>
    /// <returns>크기 조절 방향</returns>
    private ResizeDirection GetResizeDirection(Point mousePos, Form form)
    {
        return ResizeHelper.GetResizeDirectionForForm(mousePos, form, this.Height);
    }

    /// <summary>
    /// 마우스가 타이틀바 영역에 있는지 확인합니다.
    /// </summary>
    /// <param name="mousePos">마우스 위치</param>
    /// <returns>타이틀바 영역 여부</returns>
    private bool IsInTitleBarArea(Point mousePos)
    {
        return mousePos.Y > 0 && mousePos.Y <= this.Height && mousePos.Y > ResizeHelper.RESIZE_BORDER_WIDTH;
    }



    /// <summary>
    /// 실제 크기 조절을 수행합니다.
    /// 현재 마우스 위치와 시작 위치의 차이를 계산하여 새로운 크기와 위치를 결정합니다.
    /// </summary>
    /// <param name="form">크기를 조절할 폼</param>
    private void PerformResize(Form form)
    {
        Point currentScreenPos = Control.MousePosition;
        int deltaX = currentScreenPos.X - _resizeStartPoint.X;
        int deltaY = currentScreenPos.Y - _resizeStartPoint.Y;

        var newBounds = CalculateNewBounds(deltaX, deltaY);
        form.Bounds = newBounds;
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
      MIN_FORM_WIDTH, MIN_FORM_HEIGHT);
    }



    #endregion

    #region Public Methods

    /// <summary>
    /// 파일명과 아이콘을 한번에 설정합니다.
    /// </summary>
    /// <param name="fileName">설정할 파일명</param>
    /// <param name="icon">설정할 아이콘 (null이면 변경하지 않음)</param>
    public void SetFileInfo(string fileName, Image icon = null)
    {
        FileName = fileName;
        if (icon != null)
            TitleIcon = icon;
    }

    #endregion
}