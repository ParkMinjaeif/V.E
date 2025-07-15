using System.Drawing;
using System.Windows.Forms;
using Sunny.UI;

namespace IFVisionEngine.UIComponents.Common
{
    /// <summary>
    /// 다크 테마 관련 공통 유틸리티 클래스
    /// 일관된 다크 테마 스타일을 제공합니다.
    /// </summary>
    public static class ThemeHelper
    {
        #region Color Constants

        /// <summary>기본 다크 배경색</summary>
        public static readonly Color DarkBackground = Color.FromArgb(45, 45, 48);

        /// <summary>패널 배경색</summary>
        public static readonly Color DarkPanel = Color.FromArgb(50, 50, 50);

        /// <summary>타이틀바 배경색</summary>
        public static readonly Color DarkTitleBar = Color.FromArgb(60, 60, 60);

        /// <summary>기본 텍스트 색상</summary>
        public static readonly Color LightText = Color.FromArgb(241, 241, 241);

        /// <summary>버튼 호버 색상 (일반)</summary>
        public static readonly Color ButtonHover = Color.FromArgb(70, 70, 70);

        /// <summary>버튼 클릭 색상 (일반)</summary>
        public static readonly Color ButtonPressed = Color.FromArgb(50, 50, 50);

        /// <summary>닫기 버튼 호버 색상</summary>
        public static readonly Color CloseButtonHover = Color.FromArgb(232, 17, 35);

        /// <summary>닫기 버튼 클릭 색상</summary>
        public static readonly Color CloseButtonPressed = Color.FromArgb(200, 15, 30);

        /// <summary>테두리 색상</summary>
        public static readonly Color DarkBorder = Color.FromArgb(30, 30, 30);

        /// <summary>이미지 마진 색상</summary>
        public static readonly Color DarkImageMargin = Color.FromArgb(35, 35, 35);

        /// <summary>구분선 색상</summary>
        public static readonly Color DarkSeparator = Color.FromArgb(60, 60, 60);

        #endregion

        #region UIPanel Theme Application

        /// <summary>
        /// UIPanel에 다크 테마를 적용합니다.
        /// </summary>
        /// <param name="panel">테마를 적용할 UIPanel</param>
        public static void ApplyDarkTheme(UIPanel panel)
        {
            if (panel == null) return;

            panel.FillColor = DarkBackground;
            panel.RectColor = DarkBackground;
            panel.RectSize = 0;
            panel.Radius = 0;
        }

        #endregion

        #region Control Theme Application

        /// <summary>
        /// 일반 패널에 다크 테마를 적용합니다.
        /// </summary>
        /// <param name="panel">테마를 적용할 Panel</param>
        /// <param name="useContentStyle">내용 영역 스타일 사용 여부</param>
        public static void ApplyDarkTheme(Panel panel, bool useContentStyle = false)
        {
            if (panel == null) return;

            panel.BackColor = useContentStyle ? DarkPanel : DarkTitleBar;
        }

        /// <summary>
        /// 라벨에 다크 테마를 적용합니다.
        /// </summary>
        /// <param name="label">테마를 적용할 Label</param>
        public static void ApplyDarkTheme(Label label)
        {
            if (label == null) return;

            label.ForeColor = LightText;
            label.BackColor = Color.Transparent;
        }

        /// <summary>
        /// 버튼에 다크 테마를 적용합니다.
        /// </summary>
        /// <param name="button">테마를 적용할 Button</param>
        /// <param name="isCloseButton">닫기 버튼 여부 (빨간색 호버 효과)</param>
        public static void ApplyDarkTheme(Button button, bool isCloseButton = false)
        {
            if (button == null) return;

            button.BackColor = Color.Transparent;
            button.ForeColor = LightText;
            button.FlatStyle = FlatStyle.Flat;
            button.TextAlign = ContentAlignment.MiddleCenter; // 텍스트 중앙 정렬 명시적 설정
            button.UseVisualStyleBackColor = false; // 비주얼 스타일 비활성화
            button.FlatAppearance.BorderSize = 0;

            if (isCloseButton)
            {
                button.FlatAppearance.MouseOverBackColor = CloseButtonHover;
                button.FlatAppearance.MouseDownBackColor = CloseButtonPressed;
            }
            else
            {
                button.FlatAppearance.MouseOverBackColor = ButtonHover;
                button.FlatAppearance.MouseDownBackColor = ButtonPressed;
            }
        }

        /// <summary>
        /// UserControl에 다크 테마를 적용합니다.
        /// </summary>
        /// <param name="userControl">테마를 적용할 UserControl</param>
        public static void ApplyDarkTheme(UserControl userControl)
        {
            if (userControl == null) return;

            userControl.BackColor = DarkBackground;
        }

        #endregion

        #region ContextMenu Theme

        /// <summary>
        /// ContextMenuStrip에 다크 테마를 적용합니다.
        /// </summary>
        /// <param name="contextMenu">테마를 적용할 ContextMenuStrip</param>
        public static void ApplyDarkTheme(ContextMenuStrip contextMenu)
        {
            if (contextMenu == null) return;

            contextMenu.BackColor = DarkBackground;
            contextMenu.ForeColor = Color.White;
            contextMenu.Font = new Font("Segoe UI", 9F);
            contextMenu.Renderer = new DarkContextMenuRenderer();
        }

        #endregion

        #region Font Helpers

        /// <summary>
        /// 기본 UI 폰트를 반환합니다.
        /// </summary>
        /// <param name="size">폰트 크기</param>
        /// <param name="style">폰트 스타일</param>
        /// <returns>설정된 폰트</returns>
        public static Font GetDefaultFont(float size = 10F, FontStyle style = FontStyle.Regular)
        {
            return new Font("Segoe UI", size, style);
        }

        /// <summary>
        /// 타이틀바용 폰트를 반환합니다.
        /// </summary>
        /// <returns>타이틀바 폰트</returns>
        public static Font GetTitleBarFont()
        {
            return new Font("Segoe UI", 9F, FontStyle.Regular);
        }

        /// <summary>
        /// 버튼용 폰트를 반환합니다.
        /// </summary>
        /// <returns>버튼 폰트</returns>
        public static Font GetButtonFont()
        {
            return new Font("Segoe UI", 10F, FontStyle.Regular);
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
                    ? ButtonHover           // 선택된 항목
                    : DarkBackground;       // 기본 배경

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
                using (Pen pen = new Pen(DarkBorder))
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
                using (SolidBrush brush = new SolidBrush(DarkImageMargin))
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
                using (Pen pen = new Pen(DarkSeparator))
                {
                    int y = e.Item.Height / 2;
                    e.Graphics.DrawLine(pen, 25, y, e.Item.Width, y);
                }
            }
        }

        #endregion
    }
}