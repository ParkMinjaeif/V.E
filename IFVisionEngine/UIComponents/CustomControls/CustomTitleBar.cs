using IFVisionEngine.Manager;
using IFVisionEngine.UIComponents.CustomControls;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/// <summary>
/// UIPanel 기반의 고급 커스텀 타이틀바 컨트롤
/// 
/// 주요 기능:
/// - 드래그를 통한 창 이동
/// - 8방향 크기 조절 (모서리 및 가장자리)
/// - 최대화/복원 기능 (스마트 드래그 복원 포함)
/// - 다크 테마 지원
/// - 동적 창 관리 시스템 (보기 메뉴)
/// - Windows 표준 창 동작 완벽 구현
/// </summary>
public class CustomTitleBar : UIPanel
{
    #region Constants

    /// <summary>윈도우 캡션 드래그를 위한 Windows API 상수</summary>
    private const int WM_NCLBUTTONDOWN = 0xA1;
    /// <summary>캡션 영역을 나타내는 히트 테스트 상수</summary>
    private const int HTCAPTION = 0x2;
    /// <summary>크기 조절 감지 영역의 픽셀 두께</summary>
    private const int RESIZE_BORDER_WIDTH = 8;
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

    #region Enums

    /// <summary>
    /// 크기 조절 방향을 정의하는 열거형
    /// 8방향 크기 조절을 지원 (4개 모서리 + 4개 가장자리)
    /// </summary>
    private enum ResizeDirection
    {
        /// <summary>크기 조절 불가 영역</summary>
        None,
        /// <summary>상단 가장자리 - 세로 크기만 조절</summary>
        Top,
        /// <summary>하단 가장자리 - 세로 크기만 조절</summary>
        Bottom,
        /// <summary>좌측 가장자리 - 가로 크기만 조절</summary>
        Left,
        /// <summary>우측 가장자리 - 가로 크기만 조절</summary>
        Right,
        /// <summary>좌상단 모서리 - 가로+세로 동시 조절, 위치 이동</summary>
        TopLeft,
        /// <summary>우상단 모서리 - 가로+세로 동시 조절, Y 위치 이동</summary>
        TopRight,
        /// <summary>좌하단 모서리 - 가로+세로 동시 조절, X 위치 이동</summary>
        BottomLeft,
        /// <summary>우하단 모서리 - 가로+세로 동시 조절, 위치 고정</summary>
        BottomRight
    }

    #endregion

    #region Private Fields

    #region UI Components

    /// <summary>부모 폼 참조 (드래그 이동 및 크기 조절 대상)</summary>
    private Form _parentForm;
    /// <summary>왼쪽 아이콘을 표시하는 PictureBox</summary>
    private PictureBox _iconPictureBox;
    /// <summary>파일명/제목을 표시하는 Label</summary>
    private Label _titleLabel;
    /// <summary>최소화 버튼</summary>
    private Button _minimizeButton;
    /// <summary>최대화/복원 버튼</summary>
    private Button _maximizeButton;
    /// <summary>닫기 버튼</summary>
    private Button _closeButton;

    #endregion

    #region Window Management

    /// <summary>동적 창 관리를 위한 컨텍스트 메뉴</summary>
    private ContextMenuStrip _viewContextMenu;
    /// <summary>생성된 창들을 관리하는 딕셔너리 (키: 창 식별자, 값: WindowWrapper 인스턴스)</summary>
    private Dictionary<string, UserControl> _windowControls;

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

    #endregion

    #region Public Properties

    /// <summary>
    /// 타이틀바에 표시할 아이콘 이미지
    /// </summary>
    public Image TitleIcon
    {
        get => _iconPictureBox?.Image;
        set
        {
            if (_iconPictureBox != null)
                _iconPictureBox.Image = value;
        }
    }

    /// <summary>
    /// 타이틀바에 표시할 파일명 또는 제목
    /// </summary>
    public string FileName
    {
        get => _titleLabel?.Text ?? string.Empty;
        set
        {
            if (_titleLabel != null)
                _titleLabel.Text = value ?? string.Empty;
        }
    }

    /// <summary>
    /// 부모 폼 참조 (드래그 이동 및 창 제어 대상)
    /// </summary>
    public new Form ParentForm
    {
        get => _parentForm;
        set => _parentForm = value;
    }

    /// <summary>
    /// 현재 폼이 최대화 상태인지 확인
    /// </summary>
    public bool IsMaximized => _isFormMaximized;

    #endregion

    #region Constructor

    /// <summary>
    /// CustomTitleBar 인스턴스를 초기화합니다.
    /// 모든 UI 컴포넌트를 생성하고 이벤트를 설정합니다.
    /// </summary>
    public CustomTitleBar()
    {
        InitializeTitleBar();
        CreateControls();
        ApplyDarkTheme();
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

    /// <summary>
    /// 타이틀바의 기본 속성을 초기화합니다.
    /// </summary>
    private void InitializeTitleBar()
    {
        this.Dock = DockStyle.Top;
        this.Height = 40;
        this.Padding = new Padding(0);
        this.Margin = new Padding(0);
    }

    /// <summary>
    /// 타이틀바의 모든 UI 컨트롤을 생성하고 배치합니다.
    /// </summary>
    private void CreateControls()
    {
        CreateIconAndTitle();
        CreateWindowButtons();
        AddControlsToTitleBar();
        UpdateButtonPositions();
    }

    /// <summary>
    /// 아이콘과 제목 라벨을 생성합니다.
    /// </summary>
    private void CreateIconAndTitle()
    {
        // 왼쪽 아이콘
        _iconPictureBox = new PictureBox
        {
            Size = new Size(20, 20),
            Location = new Point(10, 10),
            SizeMode = PictureBoxSizeMode.StretchImage,
            BackColor = Color.Transparent,
            BorderStyle = BorderStyle.None
        };

        // 왼쪽 파일명 라벨
        _titleLabel = new Label
        {
            Text = "파일명.txt",
            Location = new Point(35, 10),
            AutoSize = true,
            BackColor = Color.Transparent,
            Font = new Font("Segoe UI", 10F, FontStyle.Regular)
        };
    }

    /// <summary>
    /// 창 제어 버튼들 (최소화, 최대화, 닫기)을 생성합니다.
    /// </summary>
    private void CreateWindowButtons()
    {
        // 닫기 버튼 (가장 오른쪽)
        _closeButton = CreateWindowButton("✕");

        // 최대화 버튼
        _maximizeButton = CreateWindowButton("🗖");

        // 최소화 버튼
        _minimizeButton = CreateWindowButton("⎯");
    }

    /// <summary>
    /// 창 제어 버튼의 공통 속성으로 버튼을 생성합니다.
    /// </summary>
    /// <param name="text">버튼에 표시할 텍스트</param>
    /// <returns>생성된 버튼 인스턴스</returns>
    private Button CreateWindowButton(string text)
    {
        return new Button
        {
            Text = text,
            Size = new Size(40, 30),
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10F),
            TextAlign = ContentAlignment.MiddleCenter,
            Anchor = AnchorStyles.Top | AnchorStyles.Right
        };
    }

    /// <summary>
    /// 생성된 모든 컨트롤을 타이틀바에 추가합니다.
    /// </summary>
    private void AddControlsToTitleBar()
    {
        this.Controls.Add(_iconPictureBox);
        this.Controls.Add(_titleLabel);
        this.Controls.Add(_closeButton);
        this.Controls.Add(_maximizeButton);
        this.Controls.Add(_minimizeButton);
    }

    /// <summary>
    /// 창 제어 버튼들의 위치를 업데이트합니다.
    /// 오른쪽부터 역순으로 배치합니다.
    /// </summary>
    protected virtual void UpdateButtonPositions()
    {
        const int buttonSpacing = 40;
        const int rightMargin = 5;
        const int topMargin = 5;

        _closeButton.Location = new Point(this.Width - (rightMargin + buttonSpacing), topMargin);
        _maximizeButton.Location = new Point(this.Width - (rightMargin + buttonSpacing * 2), topMargin);
        _minimizeButton.Location = new Point(this.Width - (rightMargin + buttonSpacing * 3), topMargin);
    }

    /// <summary>
    /// 다크 테마를 모든 컨트롤에 적용합니다.
    /// </summary>
    private void ApplyDarkTheme()
    {
        ApplyPanelTheme();
        ApplyTitleLabelTheme();
        ApplyButtonThemes();
    }

    /// <summary>
    /// UIPanel에 다크 테마를 적용합니다.
    /// </summary>
    private void ApplyPanelTheme()
    {
        this.FillColor = Color.FromArgb(45, 45, 48);
        this.RectColor = Color.FromArgb(45, 45, 48);
        this.RectSize = 0;
        this.Radius = 0;
    }

    /// <summary>
    /// 제목 라벨에 다크 테마를 적용합니다.
    /// </summary>
    private void ApplyTitleLabelTheme()
    {
        _titleLabel.ForeColor = Color.FromArgb(241, 241, 241);
    }

    /// <summary>
    /// 모든 버튼에 다크 테마를 적용합니다.
    /// </summary>
    private void ApplyButtonThemes()
    {
        ApplyButtonTheme(_minimizeButton);
        ApplyButtonTheme(_maximizeButton);
        ApplyButtonTheme(_closeButton, true); // 닫기 버튼은 빨간색 호버
    }

    /// <summary>
    /// 개별 버튼에 다크 테마를 적용합니다.
    /// </summary>
    /// <param name="button">테마를 적용할 버튼</param>
    /// <param name="isCloseButton">닫기 버튼 여부 (빨간색 호버 효과)</param>
    public void ApplyButtonTheme(Button button, bool isCloseButton = false)
    {
        if (button == null) return;

        button.BackColor = Color.Transparent;
        button.ForeColor = Color.FromArgb(241, 241, 241);
        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseOverBackColor = isCloseButton
            ? Color.FromArgb(232, 17, 35)  // 닫기 버튼은 빨간색
            : Color.FromArgb(70, 70, 70);  // 다른 버튼은 회색
        button.FlatAppearance.MouseDownBackColor = isCloseButton
            ? Color.FromArgb(200, 15, 30)  // 닫기 버튼 클릭시
            : Color.FromArgb(50, 50, 50);  // 다른 버튼 클릭시
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
        InitializeWindowControls();
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
            Font = new Font("Segoe UI", 9F),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(150, 5),
            BackColor = Color.Transparent,
            ForeColor = Color.FromArgb(241, 241, 241),
            Cursor = Cursors.Hand
        };

        SetupViewLabelEvents(viewLabel);
        this.Controls.Add(viewLabel);
    }

    /// <summary>
    /// 보기 라벨의 마우스 이벤트를 설정합니다.
    /// </summary>
    /// <param name="viewLabel">이벤트를 설정할 라벨</param>
    private void SetupViewLabelEvents(Label viewLabel)
    {
        viewLabel.MouseEnter += (s, e) => viewLabel.BackColor = Color.FromArgb(70, 70, 70);
        viewLabel.MouseLeave += (s, e) => viewLabel.BackColor = Color.Transparent;
        viewLabel.MouseDown += (s, e) => viewLabel.BackColor = Color.FromArgb(50, 50, 50);
        viewLabel.MouseUp += (s, e) => viewLabel.BackColor = Color.FromArgb(70, 70, 70);

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
        _viewContextMenu = new ContextMenuStrip
        {
            BackColor = Color.FromArgb(45, 45, 48),
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9F),
            Renderer = new DarkContextMenuRenderer()
        };

        CreateViewMenuItems();
    }

    /// <summary>
    /// 창 관리 딕셔너리를 초기화합니다.
    /// </summary>
    private void InitializeWindowControls()
    {
        _windowControls = new Dictionary<string, UserControl>();
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
                BackColor = Color.FromArgb(45, 45, 48),
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
            ("Node Data", "NodeExecutionView")
        };
    }

    /// <summary>
    /// 보기 메뉴 항목 클릭 이벤트를 처리합니다.
    /// </summary>
    /// <param name="sender">이벤트 발생자</param>
    /// <param name="e">이벤트 인수</param>
    private void OnViewMenuItemClick(object sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem menuItem)
        {
            string windowKey = menuItem.Tag.ToString();

            if (menuItem.Checked)
            {
                ShowWindow(windowKey);
            }
            else
            {
                HideWindow(windowKey);
            }
        }
    }

    /// <summary>
    /// 현재 창들의 표시 상태를 업데이트합니다.
    /// </summary>
    public void UpdateWindowStatus()
    {
        foreach (ToolStripMenuItem item in _viewContextMenu.Items)
        {
            string windowKey = item.Tag.ToString();
            if (_windowControls.ContainsKey(windowKey))
            {
                item.Checked = _windowControls[windowKey].Visible;
            }
        }
    }

    #endregion

    #region Window Management Methods

    /// <summary>
    /// 지정된 창을 표시합니다. 이미 생성된 창이 있으면 활성화하고, 없으면 새로 생성합니다.
    /// </summary>
    /// <param name="windowKey">창 식별 키</param>
    private void ShowWindow(string windowKey)
    {
        var parentForm = this.FindForm();
        if (parentForm == null) return;

        // 이미 생성된 창 확인
        if (_windowControls.ContainsKey(windowKey))
        {
            ActivateExistingWindow(windowKey);
            return;
        }

        // 새 창 생성
        CreateNewWindow(windowKey, parentForm);
    }

    /// <summary>
    /// 기존 창을 활성화합니다.
    /// </summary>
    /// <param name="windowKey">창 식별 키</param>
    private void ActivateExistingWindow(string windowKey)
    {
        if (_windowControls[windowKey] is WindowWrapper existingWrapper)
        {
            existingWrapper.Visible = true;
            existingWrapper.BringToFront();
        }
    }

    /// <summary>
    /// 새로운 창을 생성하고 표시합니다.
    /// </summary>
    /// <param name="windowKey">창 식별 키</param>
    /// <param name="parentForm">부모 폼</param>
    private void CreateNewWindow(string windowKey, Form parentForm)
    {
        UserControl innerControl = GetInnerControl(windowKey);
        if (innerControl == null) return;

        string title = GetWindowTitle(windowKey);
        Size windowSize = GetWindowSize(windowKey);

        var windowWrapper = new WindowWrapper(title, innerControl, windowSize);
        PositionNewWindow(windowWrapper);

        parentForm.Controls.Add(windowWrapper);
        windowWrapper.BringToFront();

        _windowControls[windowKey] = windowWrapper;
    }

    /// <summary>
    /// 새 창의 위치를 설정합니다. (계단식 배치)
    /// </summary>
    /// <param name="windowWrapper">위치를 설정할 창</param>
    private void PositionNewWindow(WindowWrapper windowWrapper)
    {
        const int offsetIncrement = 30;
        int offset = _windowControls.Count * offsetIncrement;
        windowWrapper.Location = new Point(50 + offset, 50 + offset);
    }

    /// <summary>
    /// 지정된 창을 숨깁니다.
    /// </summary>
    /// <param name="windowKey">창 식별 키</param>
    private void HideWindow(string windowKey)
    {
        if (_windowControls.ContainsKey(windowKey))
        {
            _windowControls[windowKey].Visible = false;
        }
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
                return new Size(400, 900);
            case "ImageControler":
                return new Size(900, 900);
            case "LogView":
                return new Size(700, 200);
            case "NodeSelectedView":
                return new Size(400, 400);
            case "NodeExecutionView":
                return new Size(500, 600);
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
            default:
                return "창";
        }
    }


    #endregion

    #region Event Setup

    /// <summary>
    /// 모든 이벤트 핸들러를 설정합니다.
    /// </summary>
    private void SetupEvents()
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
        if (form == null || e.Button != MouseButtons.Left ||
            form.WindowState != FormWindowState.Normal)
            return;

        _resizeDirection = GetResizeDirection(e.Location, form);
        if (_resizeDirection == ResizeDirection.None)
            return;

        StartResize(form);
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
        if (form == null)
            return;

        if (_isResizing && form.WindowState == FormWindowState.Normal)
        {
            PerformResize(form);
            UpdateRestoreBounds(form);
        }
        else if (form.WindowState == FormWindowState.Normal)
        {
            UpdateCursorForResize(e.Location, form);
        }
    }

    /// <summary>
    /// 복원 정보를 업데이트합니다.
    /// </summary>
    /// <param name="form">대상 폼</param>
    private void UpdateRestoreBounds(Form form)
    {
        _restoreBounds = new Rectangle(form.Location, form.Size);
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
        if (form == null)
            return;

        _isResizing = false;

        // 최대화 상태가 아닐 때만 커서 리셋
        if (form.WindowState == FormWindowState.Normal)
        {
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

        Cursor newCursor = GetCursorForDirection(direction);

        if (form.Cursor != newCursor)
        {
            form.Cursor = newCursor;
        }
    }

    /// <summary>
    /// 크기 조절 방향에 맞는 커서를 반환합니다.
    /// </summary>
    /// <param name="direction">크기 조절 방향</param>
    /// <returns>해당 방향의 커서</param>
    private static Cursor GetCursorForDirection(ResizeDirection direction)
    {
        switch (direction)
        {
            case ResizeDirection.Top:
            case ResizeDirection.Bottom:
                return Cursors.SizeNS;
            case ResizeDirection.Left:
            case ResizeDirection.Right:
                return Cursors.SizeWE;
            case ResizeDirection.TopLeft:
            case ResizeDirection.BottomRight:
                return Cursors.SizeNWSE;
            case ResizeDirection.TopRight:
            case ResizeDirection.BottomLeft:
                return Cursors.SizeNESW;
            default:
                return Cursors.Default;
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
        // 최대화 상태에서는 크기 조절 불가
        if (form.WindowState != FormWindowState.Normal)
            return ResizeDirection.None;

        // 타이틀바 영역은 제외 (드래그 이동과 충돌 방지)
        if (IsInTitleBarArea(mousePos))
            return ResizeDirection.None;

        return DetermineResizeDirection(mousePos, form); // static 제거
    }

    /// <summary>
    /// 마우스가 타이틀바 영역에 있는지 확인합니다.
    /// </summary>
    /// <param name="mousePos">마우스 위치</param>
    /// <returns>타이틀바 영역 여부</returns>
    private bool IsInTitleBarArea(Point mousePos)
    {
        return mousePos.Y > 0 && mousePos.Y <= this.Height && mousePos.Y > RESIZE_BORDER_WIDTH;
    }

    /// <summary>
    /// 실제 크기 조절 방향을 결정합니다.
    /// </summary>
    /// <param name="mousePos">마우스 위치</param>
    /// <param name="form">대상 폼</param>
    /// <returns>크기 조절 방향</returns>
    private ResizeDirection DetermineResizeDirection(Point mousePos, Form form)
    {
        bool left = mousePos.X <= RESIZE_BORDER_WIDTH;
        bool right = mousePos.X >= form.Width - RESIZE_BORDER_WIDTH;
        bool top = mousePos.Y <= RESIZE_BORDER_WIDTH;
        bool bottom = mousePos.Y >= form.Height - RESIZE_BORDER_WIDTH;

        // 코너 우선 체크 (대각선 크기 조절)
        if (top && left) return ResizeDirection.TopLeft;
        if (top && right) return ResizeDirection.TopRight;
        if (bottom && left) return ResizeDirection.BottomLeft;
        if (bottom && right) return ResizeDirection.BottomRight;

        // 가장자리 체크 (단방향 크기 조절)
        if (top) return ResizeDirection.Top;
        if (bottom) return ResizeDirection.Bottom;
        if (left) return ResizeDirection.Left;
        if (right) return ResizeDirection.Right;

        return ResizeDirection.None;
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
        int newX = _resizeStartLocation.X;
        int newY = _resizeStartLocation.Y;
        int newWidth = _resizeStartSize.Width;
        int newHeight = _resizeStartSize.Height;

        switch (_resizeDirection)
        {
            case ResizeDirection.Right:
                newWidth = Math.Max(MIN_FORM_WIDTH, _resizeStartSize.Width + deltaX);
                break;

            case ResizeDirection.Left:
                (newWidth, newX) = CalculateLeftResize(deltaX);
                break;

            case ResizeDirection.Bottom:
                newHeight = Math.Max(MIN_FORM_HEIGHT, _resizeStartSize.Height + deltaY);
                break;

            case ResizeDirection.Top:
                (newHeight, newY) = CalculateTopResize(deltaY);
                break;

            case ResizeDirection.BottomRight:
                newWidth = Math.Max(MIN_FORM_WIDTH, _resizeStartSize.Width + deltaX);
                newHeight = Math.Max(MIN_FORM_HEIGHT, _resizeStartSize.Height + deltaY);
                break;

            case ResizeDirection.BottomLeft:
                (newWidth, newX) = CalculateLeftResize(deltaX);
                newHeight = Math.Max(MIN_FORM_HEIGHT, _resizeStartSize.Height + deltaY);
                break;

            case ResizeDirection.TopRight:
                newWidth = Math.Max(MIN_FORM_WIDTH, _resizeStartSize.Width + deltaX);
                (newHeight, newY) = CalculateTopResize(deltaY);
                break;

            case ResizeDirection.TopLeft:
                (newWidth, newX) = CalculateLeftResize(deltaX);
                (newHeight, newY) = CalculateTopResize(deltaY);
                break;
        }

        return new Rectangle(newX, newY, newWidth, newHeight);
    }

    /// <summary>
    /// 왼쪽 크기 조절 계산을 수행합니다.
    /// </summary>
    /// <param name="deltaX">X축 이동 거리</param>
    /// <returns>새로운 너비와 X 위치</returns>
    private (int newWidth, int newX) CalculateLeftResize(int deltaX)
    {
        int widthChange = Math.Max(MIN_FORM_WIDTH - _resizeStartSize.Width, -deltaX);
        int newWidth = _resizeStartSize.Width + widthChange;
        int newX = _resizeStartLocation.X - widthChange;
        return (newWidth, newX);
    }

    /// <summary>
    /// 상단 크기 조절 계산을 수행합니다.
    /// </summary>
    /// <param name="deltaY">Y축 이동 거리</param>
    /// <returns>새로운 높이와 Y 위치</returns>
    private (int newHeight, int newY) CalculateTopResize(int deltaY)
    {
        int heightChange = Math.Max(MIN_FORM_HEIGHT - _resizeStartSize.Height, -deltaY);
        int newHeight = _resizeStartSize.Height + heightChange;
        int newY = _resizeStartLocation.Y - heightChange;
        return (newHeight, newY);
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

    /// <summary>
    /// 프로그래밍 방식으로 폼을 최대화합니다.
    /// </summary>
    public void MaximizeWindow()
    {
        MaximizeForm();
    }

    /// <summary>
    /// 프로그래밍 방식으로 폼을 복원합니다.
    /// </summary>
    public void RestoreWindow()
    {
        RestoreForm();
    }

    /// <summary>
    /// 최대화/복원을 토글합니다.
    /// </summary>
    public void ToggleMaximize()
    {
        ToggleMaximizeRestore();
    }

    #endregion

    #region Dark Context Menu Renderer

    /// <summary>
    /// 다크 테마 컨텍스트 메뉴 렌더러
    /// 보기 메뉴의 다크 테마 스타일을 제공합니다.
    /// </summary>
    public class DarkContextMenuRenderer : ToolStripProfessionalRenderer
    {
        /// <summary>
        /// 메뉴 항목의 배경을 렌더링합니다.
        /// </summary>
        /// <param name="e">렌더링 이벤트 인수</param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
            Color backgroundColor = e.Item.Selected
                ? Color.FromArgb(70, 70, 70)    // 선택된 항목
                : Color.FromArgb(45, 45, 48);   // 기본 배경

            using (SolidBrush brush = new SolidBrush(backgroundColor))
            {
                e.Graphics.FillRectangle(brush, rc);
            }
        }

        /// <summary>
        /// 체크 마크를 렌더링합니다.
        /// </summary>
        /// <param name="e">이미지 렌더링 이벤트 인수</param>
        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(0, 122, 204), 2))
            {
                Point[] checkPoints = {
                    new Point(e.ImageRectangle.X + 3, e.ImageRectangle.Y + 5),
                    new Point(e.ImageRectangle.X + 6, e.ImageRectangle.Y + 8),
                    new Point(e.ImageRectangle.X + 11, e.ImageRectangle.Y + 3)
                };
                e.Graphics.DrawLines(pen, checkPoints);
            }
        }

        /// <summary>
        /// 툴스트립의 테두리를 렌더링합니다.
        /// </summary>
        /// <param name="e">렌더링 이벤트 인수</param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(30, 30, 30)))
            {
                Rectangle borderRect = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
                e.Graphics.DrawRectangle(pen, borderRect);
            }
        }

        /// <summary>
        /// 이미지 마진 영역을 렌더링합니다.
        /// </summary>
        /// <param name="e">렌더링 이벤트 인수</param>
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            Rectangle imageMarginRect = new Rectangle(0, 0, 5, e.ToolStrip.Height);
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(35, 35, 35)))
            {
                e.Graphics.FillRectangle(brush, imageMarginRect);
            }
        }

        /// <summary>
        /// 구분선을 렌더링합니다.
        /// </summary>
        /// <param name="e">구분선 렌더링 이벤트 인수</param>
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(60, 60, 60)))
            {
                int y = e.Item.Height / 2;
                e.Graphics.DrawLine(pen, 25, y, e.Item.Width, y);
            }
        }
    }

    #endregion

}