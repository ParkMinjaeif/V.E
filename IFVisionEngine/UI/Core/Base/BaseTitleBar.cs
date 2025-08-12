using System;
using System.Drawing;
using System.Windows.Forms;
using Sunny.UI;

namespace IFVisionEngine.UIComponents.Common
{
    /// <summary>
    /// 타이틀바의 기본 기능을 제공하는 추상 클래스
    /// Form용과 Window용 타이틀바의 공통 기능을 담당합니다.
    /// </summary>
    public abstract class BaseTitleBar : UIPanel
    {
        #region Protected Fields

        /// <summary>왼쪽 아이콘을 표시하는 PictureBox</summary>
        protected PictureBox _iconPictureBox;
        /// <summary>파일명/제목을 표시하는 Label</summary>
        protected Label _titleLabel;
        /// <summary>최소화 버튼</summary>
        protected Button _minimizeButton;
        /// <summary>최대화/복원 버튼</summary>
        protected Button _maximizeButton;
        /// <summary>닫기 버튼</summary>
        protected Button _closeButton;

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
        /// 타이틀바에 표시할 제목
        /// </summary>
        public string TitleText
        {
            get => _titleLabel?.Text ?? string.Empty;
            set
            {
                if (_titleLabel != null)
                    _titleLabel.Text = value ?? string.Empty;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// BaseTitleBar 인스턴스를 초기화합니다.
        /// </summary>
        protected BaseTitleBar()
        {
            InitializeTitleBar();
            CreateBaseControls();
            ApplyTheme();
        }

        #endregion

        #region Initialization

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
        /// 기본 UI 컨트롤들을 생성합니다.
        /// </summary>
        private void CreateBaseControls()
        {
            CreateIconAndTitle();
            CreateWindowButtons();
            AddControlsToTitleBar();
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

            // 제목 라벨
            _titleLabel = new Label
            {
                Text = "제목",
                Location = new Point(35, 10),
                AutoSize = true,
                BackColor = Color.Transparent,
                Font = ThemeHelper.GetDefaultFont(10F, FontStyle.Regular)
            };

            // 테마 적용
            ThemeHelper.ApplyDarkTheme(_titleLabel);
        }

        /// <summary>
        /// 창 제어 버튼들을 생성합니다.
        /// </summary>
        private void CreateWindowButtons()
        {
            // 닫기 버튼 (가장 오른쪽)
            _closeButton = CreateWindowButton("✕");

            // 최대화 버튼
            _maximizeButton = CreateWindowButton("🗖");

            // 최소화 버튼
            _minimizeButton = CreateWindowButton("⎯");

            // 테마 적용
            ApplyButtonThemes();
        }

        /// <summary>
        /// 창 제어 버튼의 공통 속성으로 버튼을 생성합니다.
        /// </summary>
        /// <param name="text">버튼에 표시할 텍스트</param>
        /// <returns>생성된 버튼 인스턴스</returns>
        private Button CreateWindowButton(string text)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(40, 30),
                Font = ThemeHelper.GetButtonFont(),
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            return button;
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
        /// 테마를 적용합니다.
        /// </summary>
        private void ApplyTheme()
        {
            // UIPanel 테마 적용
            ThemeHelper.ApplyDarkTheme(this);
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

        #endregion

        #region Protected Methods

        /// <summary>
        /// 창 제어 버튼들의 위치를 업데이트합니다.
        /// 파생 클래스에서 재정의 가능합니다.
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
        /// 최대화 버튼의 아이콘을 업데이트합니다.
        /// </summary>
        /// <param name="isMaximized">최대화 상태 여부</param>
        protected void UpdateMaximizeButtonIcon(bool isMaximized)
        {
            _maximizeButton.Text = isMaximized ? "🗗" : "🗖";
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// 최소화 버튼 클릭 처리 (파생 클래스에서 구현)
        /// </summary>
        protected abstract void OnMinimizeClick();

        /// <summary>
        /// 최대화 버튼 클릭 처리 (파생 클래스에서 구현)
        /// </summary>
        protected abstract void OnMaximizeClick();

        /// <summary>
        /// 닫기 버튼 클릭 처리 (파생 클래스에서 구현)
        /// </summary>
        protected abstract void OnCloseClick();

        /// <summary>
        /// 타이틀바 드래그 처리 (파생 클래스에서 구현)
        /// </summary>
        /// <param name="e">마우스 이벤트 인수</param>
        protected abstract void OnTitleBarMouseDown(MouseEventArgs e);

        /// <summary>
        /// 타이틀바 더블클릭 처리 (파생 클래스에서 구현)
        /// </summary>
        protected abstract void OnTitleBarDoubleClick();

        #endregion

        #region Event Setup

        /// <summary>
        /// 이벤트 핸들러를 설정합니다.
        /// </summary>
        protected virtual void SetupEvents()
        {
            // 크기 변경시 버튼 위치 업데이트
            this.SizeChanged += (s, e) => UpdateButtonPositions();

            // 버튼 클릭 이벤트
            _minimizeButton.Click += (s, e) => OnMinimizeClick();
            _maximizeButton.Click += (s, e) => OnMaximizeClick();
            _closeButton.Click += (s, e) => OnCloseClick();

            // 타이틀바 드래그 이벤트
            this.MouseDown += (s, e) => OnTitleBarMouseDown(e);
            _titleLabel.MouseDown += (s, e) => OnTitleBarMouseDown(e);
            _iconPictureBox.MouseDown += (s, e) => OnTitleBarMouseDown(e);

            // 타이틀바 더블클릭 이벤트
            this.DoubleClick += (s, e) => OnTitleBarDoubleClick();
            _titleLabel.DoubleClick += (s, e) => OnTitleBarDoubleClick();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 파일명과 아이콘을 한번에 설정합니다.
        /// </summary>
        /// <param name="title">설정할 제목</param>
        /// <param name="icon">설정할 아이콘 (null이면 변경하지 않음)</param>
        public void SetTitleInfo(string title, Image icon = null)
        {
            TitleText = title;
            if (icon != null)
                TitleIcon = icon;
        }

        #endregion
    }
}