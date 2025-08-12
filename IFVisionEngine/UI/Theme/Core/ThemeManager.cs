using IFVisionEngine.Manager;
using Sunny.UI;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static MyNodesContext.RadialLinesParameters;

namespace IFVisionEngine.Themes
{
    /// <summary>
    /// 애플리케이션 전체의 다크 테마를 관리하는 정적 클래스
    /// 모든 색상은 Theme 클래스에서 중앙 관리됨
    /// </summary>
    public static class ThemeManager
    {
        #region 속성 및 이벤트

        /// <summary>현재 적용된 테마</summary>
        public static Theme CurrentTheme { get; private set; } = Theme.DarkTheme;

        /// <summary>테마 변경 시 발생하는 이벤트</summary>
        public static event Action ThemeChanged;

        #endregion

        #region 공개 메서드

        /// <summary>
        /// 새로운 테마를 설정하고 모든 열린 폼에 적용
        /// </summary>
        /// <param name="theme">적용할 테마</param>
        public static void SetTheme(Theme theme)
        {
            CurrentTheme = theme;

            // 모든 열린 폼에 테마 적용
            foreach (Form form in Application.OpenForms)
            {
                ApplyThemeToControl(form);
                form.Refresh();
            }

            ThemeChanged?.Invoke();
        }

        /// <summary>
        /// 특정 컨트롤과 그 자식 컨트롤들에 테마 적용
        /// </summary>
        /// <param name="control">테마를 적용할 컨트롤</param>
        public static void ApplyThemeToControl(Control control)
        {
            if (control == null)
            {
                Debug.WriteLine("❌ ApplyThemeToControl: control이 null입니다.");
                return;
            }

            string controlInfo = $"{control.GetType().Name}";
            if (!string.IsNullOrEmpty(control.Name))
                controlInfo += $" ({control.Name})";

            Debug.WriteLine($"🔍 처리 중: {controlInfo}");

            // IThemable 인터페이스 구현 확인 (최우선)
            if (control is IThemable themableControl)
            {
                themableControl.ApplyTheme(CurrentTheme);
                Debug.WriteLine($"✅ {controlInfo} → IThemable 인터페이스로 처리됨");
                return;
            }

            // 컨트롤 타입별 테마 적용
            ApplyThemeByControlType(control, controlInfo);

            // 자식 컨트롤들에 재귀적으로 적용
            foreach (Control childControl in control.Controls)
            {
                ApplyThemeToControl(childControl);
            }
        }

        #endregion

        #region 컨트롤별 테마 적용 메서드

        /// <summary>
        /// 컨트롤 타입에 따른 테마 적용
        /// SunnyUI 컨트롤은 우선 처리되며, 일반 WinForms 컨트롤들은 개별 처리
        /// </summary>
        /// <param name="control">테마를 적용할 컨트롤</param>
        /// <param name="controlInfo">디버그용 컨트롤 정보</param>
        private static void ApplyThemeByControlType(Control control, string controlInfo)
        {
            // SunnyUI 컨트롤 우선 처리
            if (control.GetType().Namespace?.Contains("Sunny.UI") == true)
            {
                ApplySunnyUITheme(control);
                Debug.WriteLine($"✅ {control.GetType().Name} → SunnyUI 다크 테마 적용");
                return;
            }

            // 일반 WinForms 컨트롤 처리
            switch (control)
            {

                case ToolStrip toolStrip:
                    ApplyToolStripTheme(toolStrip, controlInfo);
                    break;
                case PropertyGrid propertyGrid:
                    ApplyPropertyGridTheme(propertyGrid, controlInfo);
                    break;
                case PictureBox pictureBox:
                    ApplyPictureBoxTheme(pictureBox, controlInfo);
                    break;

                case ListView listView:
                    ApplyListViewTheme(listView, controlInfo);
                    break;

                case TreeView treeView:
                    ApplyTreeViewTheme(treeView, controlInfo);
                    break;

                case Panel panel:
                    ApplyPanelTheme(panel, controlInfo);
                    ScrollbarTheme.ApplyDarkScrollbar(panel);
                    break;
                case Form form:
                    ApplyFormTheme(form, controlInfo);
                    break;
                case Label label:
                    ApplyLabelTheme(label, controlInfo);
                    break;

                case RichTextBox richTextBox:
                    ApplyRichTextBoxTheme(richTextBox, controlInfo);
                    break;

                case Button button:
                    ApplyButtonTheme(button, controlInfo);
                    break;
            }
        }
        private static void ApplyPictureBoxTheme(PictureBox pictureBox, string controlInfo)
        {
            // 배경색 설정
            pictureBox.BackColor = CurrentTheme.PicturxBoxColor;

        }
        /// <summary>PropertyGrid 완전 다크 테마 적용</summary>
        private static void ApplyPropertyGridTheme(PropertyGrid propertyGrid, string controlInfo)
        {
            // 기본 색상 설정
            propertyGrid.BackColor = CurrentTheme.PropertyGridBackColor;
            propertyGrid.ViewBackColor = CurrentTheme.PropertyGridViewBackColor;
            propertyGrid.ViewForeColor = CurrentTheme.PropertyGridViewForeColor;
            propertyGrid.CategoryForeColor = CurrentTheme.PropertyGridCategoryForeColor;
            propertyGrid.CategorySplitterColor = CurrentTheme.PropertyGridCategorySplitterColor;
            propertyGrid.LineColor = CurrentTheme.PropertyGridLineColor;

            // 선택 및 포커스 색상
            propertyGrid.SelectedItemWithFocusBackColor = CurrentTheme.PropertyGridSelectedBackColor;
            propertyGrid.SelectedItemWithFocusForeColor = CurrentTheme.PropertyGridSelectedForeColor;
            propertyGrid.DisabledItemForeColor = CurrentTheme.PropertyGridDisabledForeColor;

            // 하단 영역 색상
            propertyGrid.CommandsBackColor = CurrentTheme.PropertyGridCommandsBackColor;
            propertyGrid.CommandsForeColor = CurrentTheme.PropertyGridCommandsForeColor;
            propertyGrid.CommandsBorderColor = CurrentTheme.PropertyGridCommandsBorderColor;
            propertyGrid.HelpBackColor = CurrentTheme.PropertyGridHelpBackColor;
            propertyGrid.HelpForeColor = CurrentTheme.PropertyGridHelpForeColor;
            propertyGrid.HelpBorderColor = CurrentTheme.PropertyGridHelpBorderColor;
            propertyGrid.ViewBorderColor = CurrentTheme.PropertyGridViewBorderColor;

            Debug.WriteLine($"✅ {controlInfo} → PropertyGrid 완전 다크 테마 적용");
        }

        /// <summary>ListView 다크 테마 적용</summary>
        private static void ApplyListViewTheme(ListView listView, string controlInfo)
        {
            listView.BackColor = CurrentTheme.ListViewBackColor;
            listView.ForeColor = CurrentTheme.ListViewForeColor;
            listView.BorderStyle = BorderStyle.None;
            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.GridLines = false;
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.OwnerDraw = true;

            // 커스텀 그리기 이벤트 등록
            listView.DrawColumnHeader += OnListViewDrawColumnHeader;
            listView.DrawItem += OnListViewDrawItem;
            listView.DrawSubItem += OnListViewDrawSubItem;
            listView.Paint += (sender, e) => DrawControlBorder(sender as Control, e, CurrentTheme.BorderColor);
            ScrollbarTheme.DarkListView(listView);

            Debug.WriteLine($"✅ {controlInfo} → ListView 테마 적용");
        }

        /// <summary>TreeView 다크 테마 적용</summary>
        private static void ApplyTreeViewTheme(TreeView treeView, string controlInfo)
        {
            treeView.BackColor = CurrentTheme.TreeViewBackColor;
            treeView.ForeColor = CurrentTheme.TreeViewForeColor;
            treeView.LineColor = CurrentTheme.TreeViewLineColor;
            treeView.BorderStyle = BorderStyle.None;

            // 커스텀 그리기 모드 활성화 (점선 테두리 제거를 위해)
            treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
            treeView.DrawNode += OnTreeViewDrawNode;
            treeView.Paint += (sender, e) => DrawControlBorder(sender as Control, e, CurrentTheme.BorderColor);

            Debug.WriteLine($"✅ {controlInfo} → TreeView 테마 적용");
        }

        /// <summary>ToolStrip 다크 테마 적용</summary>
        private static void ApplyToolStripTheme(ToolStrip toolStrip, string controlInfo)
        {
            toolStrip.BackColor = CurrentTheme.ToolStripBackColor;
            toolStrip.ForeColor = CurrentTheme.ToolStripForeColor;
            toolStrip.Renderer = new DarkToolStripRenderer();
            // ToolStrip 내부 아이템들 처리
            foreach (ToolStripItem item in toolStrip.Items)
            {
                switch (item)
                {
                    case ToolStripLabel toolStripLabel:
                        ApplyToolStripLabelTheme(toolStripLabel, controlInfo);
                        break;
                }
            }
            Debug.WriteLine($"✅ {controlInfo} → ToolStrip 테마 적용");
        }
        private static void ApplyToolStripLabelTheme(ToolStripLabel toolStripLabel, string controlInfo)
        {
            try
            {
                // 기본 색상 적용
                toolStripLabel.BackColor = CurrentTheme.ToolStripBackColor;
                toolStripLabel.ForeColor = CurrentTheme.ToolStripForeColor;
            }
            catch (Exception ex)
            {
                // 로깅 또는 예외 처리
                Console.WriteLine($"ToolStripLabel 테마 적용 중 오류: {ex.Message}");
            }
        }
        /// <summary>Panel 다크 테마 적용</summary>
        private static void ApplyPanelTheme(Panel panel, string controlInfo)
        {
            panel.BackColor = CurrentTheme.BackgroundColor;

            if (panel.BorderStyle != BorderStyle.None)
            {
                panel.BorderStyle = BorderStyle.None;
                panel.Paint += (sender, e) => DrawControlBorder(sender as Control, e, CurrentTheme.PanelBorderColor);
                Debug.WriteLine($"✅ {controlInfo} → Panel 테마 적용 (커스텀 테두리)");
            }
            else
            {
                Debug.WriteLine($"✅ {controlInfo} → Panel 테마 적용");
            }
        }
        /// <summary>Form 다크 테마 적용</summary>
        private static void ApplyFormTheme(Form panel, string controlInfo)
        {
            panel.BackColor = CurrentTheme.BackgroundColor;
        }

        /// <summary>Label 다크 테마 적용</summary>
        private static void ApplyLabelTheme(Label label, string controlInfo)
        {
            label.ForeColor = CurrentTheme.LabelForeColor;
            label.BackColor = Color.Transparent;

            Debug.WriteLine($"✅ {controlInfo} → Label 테마 적용");
        }

        /// <summary>RichTextBox 다크 테마 적용</summary>
        private static void ApplyRichTextBoxTheme(RichTextBox richTextBox, string controlInfo)
        {
            richTextBox.BackColor = CurrentTheme.TextBoxBackColor;
            richTextBox.ForeColor = CurrentTheme.TextBoxForeColor;
            ScrollbarTheme.ApplyDarkScrollbar(richTextBox);

            Debug.WriteLine($"✅ {controlInfo} → RichTextBox 테마 적용");
        }

        /// <summary>Button 다크 테마 적용</summary>
        private static void ApplyButtonTheme(Button button, string controlInfo)
        {
            button.BackColor = CurrentTheme.ButtonBackColor;
            button.ForeColor = CurrentTheme.ButtonForeColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = CurrentTheme.ButtonBorderColor;
            button.FlatAppearance.BorderSize = 1;

            Debug.WriteLine($"✅ {controlInfo} → Button 테마 적용");
        }

        /// <summary>
        /// Sunny.UI 컨트롤 다크 테마 적용
        /// 각 컨트롤 타입별로 세부 색상 설정을 다르게 적용
        /// </summary>
        /// <param name="sunnyControl">SunnyUI 컨트롤</param>
        private static void ApplySunnyUITheme(Control sunnyControl)
        {
            try
            {
                var type = sunnyControl.GetType();

                // 컨트롤별 세부 설정
                switch (type.Name)
                {
                    case "UIComboBox":
                        ApplySunnyUIComboBoxTheme(type, sunnyControl);
                        break;

                    case "UIIntegerUpDown":
                    case "UIDoubleUpDown":
                        ApplySunnyUINumericUpDownTheme(type, sunnyControl);
                        break;

                    case "UICheckBox":
                        ApplySunnyUICheckBoxTheme(type, sunnyControl);
                        break;

                    case "UIButton":
                        ApplySunnyUIButtonTheme(type, sunnyControl);
                        break;

                    case "UITrackBar":
                        ApplySunnyUITrackBarTheme(type, sunnyControl);
                        break;

                    case "UITextBox":
                        ApplySunnyUITextBoxTheme(type, sunnyControl);
                        break;

                    case "UILabel":
                        ApplySunnyUILabelTheme(type, sunnyControl);
                        break;
                    case "UILine":
                        ApplySunnyUILineTheme(type, sunnyControl);
                        break;

                    default:
                        ApplySunnyUIDefaultTheme(type, sunnyControl);
                        break;
                }

                Debug.WriteLine($"✅ {type.Name} SunnyUI 다크 테마 적용완료");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ SunnyUI 테마 적용 실패: {ex.Message}");
                // 실패 시 기본 색상만 적용
                try
                {
                    sunnyControl.BackColor = CurrentTheme.BackgroundColor;
                    sunnyControl.ForeColor = CurrentTheme.ForegroundColor;
                }
                catch { }
            }
        }

        #endregion

        #region SunnyUI 개별 컨트롤 테마 적용 메서드
        /// <summary>UILine 다크 테마 적용 (점선, 수직선, 테두리 제거)</summary>
        private static void ApplySunnyUILineTheme(Type type, Control sunnyControl)
        {
            // 점선 세로 회색 구분선
            type.GetProperty("LineColor")?.SetValue(sunnyControl, CurrentTheme.UILineColor);
            type.GetProperty("Direction")?.SetValue(sunnyControl, 1);  // 1=Vertical (현재 0=Horizontal)
            type.GetProperty("LineDashStyle")?.SetValue(sunnyControl, 1);  // 1=Dash (현재 0=None)
            type.GetProperty("LineSize")?.SetValue(sunnyControl, 4);

            // 테두리 제거
            type.GetProperty("FillColor")?.SetValue(sunnyControl, Color.Transparent);
            type.GetProperty("RectSize")?.SetValue(sunnyControl, 0);  // 현재 1 → 0으로
            type.GetProperty("Radius")?.SetValue(sunnyControl, 0);    // 현재 5 → 0으로

            // 끝까지 뻗어나가게
            sunnyControl.Dock = DockStyle.Fill;
        }
        /// <summary>UIComboBox 테마 적용</summary>
        private static void ApplySunnyUIComboBoxTheme(Type type, Control sunnyControl)
        {
            type.GetProperty("FillColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIFillColor);
            type.GetProperty("ForeColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIForeColor);
            type.GetProperty("RectColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIRectColor);

            // 드롭다운 창 배경색과 항목 색상 설정
            type.GetProperty("ItemFillColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIFillColor);
            type.GetProperty("ItemForeColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIForeColor);
            type.GetProperty("ItemSelectBackColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUISelectBackColor);
            type.GetProperty("ItemSelectForeColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUISelectForeColor);
            type.GetProperty("ItemHoverColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIHoverColor);
        }

        /// <summary>UIIntegerUpDown/UIDoubleUpDown 테마 적용</summary>
        private static void ApplySunnyUINumericUpDownTheme(Type type, Control sunnyControl)
        {
            type.GetProperty("FillColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIFillColor);
            type.GetProperty("ForeColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIForeColor);
            type.GetProperty("RectColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIRectColor);
            type.GetProperty("ButtonFillColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUINumericButtonFillColor);
            type.GetProperty("ButtonForeColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUINumericButtonForeColor);

            // 비활성화 상태 색상
            type.GetProperty("ForeDisableColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIDisabledForeColor);
            type.GetProperty("RectDisableColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIDisabledRectColor);
        }

        /// <summary>UICheckBox 테마 적용</summary>
        private static void ApplySunnyUICheckBoxTheme(Type type, Control sunnyControl)
        {
            type.GetProperty("ForeColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIForeColor);
            type.GetProperty("CheckBoxColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUICheckBoxColor);
            type.GetProperty("FillColor")?.SetValue(sunnyControl, Color.Transparent);
        }

        /// <summary>UIButton 테마 적용</summary>
        private static void ApplySunnyUIButtonTheme(Type type, Control sunnyControl)
        {
            type.GetProperty("ForeColor")?.SetValue(sunnyControl, Color.White);
            type.GetProperty("RectColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUISelectBackColor);
            type.GetProperty("FillHoverColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIButtonHoverColor);
            type.GetProperty("FillPressColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIButtonPressColor);
        }

        /// <summary>UITrackBar 테마 적용</summary>
        private static void ApplySunnyUITrackBarTheme(Type type, Control sunnyControl)
        {
            type.GetProperty("FillColor")?.SetValue(sunnyControl, CurrentTheme.ButtonBackColor);
            type.GetProperty("ForeColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUITrackBarValueColor);
            type.GetProperty("RectColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIRectColor);
            type.GetProperty("ValueColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUITrackBarValueColor);
        }

        /// <summary>UITextBox 테마 적용</summary>
        private static void ApplySunnyUITextBoxTheme(Type type, Control sunnyControl)
        {
            type.GetProperty("FillColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIFillColor);
            type.GetProperty("ForeColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIForeColor);
            type.GetProperty("RectColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIRectColor);
        }

        /// <summary>UILabel 테마 적용</summary>
        private static void ApplySunnyUILabelTheme(Type type, Control sunnyControl)
        {
            var uiLabel = sunnyControl as UILabel;
            if (uiLabel != null)
            {
                uiLabel.ForeColor = CurrentTheme.SunnyUIForeColor;
                uiLabel.BackColor = CurrentTheme.SunnyUILabelBackColor;
            }
        }

        /// <summary>기본 SunnyUI 컨트롤 테마 적용</summary>
        private static void ApplySunnyUIDefaultTheme(Type type, Control sunnyControl)
        {
            type.GetProperty("FillColor")?.SetValue(sunnyControl, CurrentTheme.ButtonBackColor);
            type.GetProperty("ForeColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIForeColor);
            type.GetProperty("RectColor")?.SetValue(sunnyControl, CurrentTheme.SunnyUIRectColor);
        }

        #endregion

        #region 그리기 이벤트 핸들러

        /// <summary>일반 컨트롤 테두리 그리기</summary>
        private static void DrawControlBorder(Control control, PaintEventArgs e, Color borderColor)
        {
            if (control == null || e == null) return;

            using (var pen = new Pen(borderColor, 1))
            {
                var rect = new Rectangle(0, 0, control.Width - 1, control.Height - 1);
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        /// <summary>ListView 헤더 그리기</summary>
        private static void OnListViewDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (var brush = new SolidBrush(CurrentTheme.ListViewHeaderBackColor))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            using (var textBrush = new SolidBrush(CurrentTheme.ListViewHeaderForeColor))
            {
                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center
                };
                e.Graphics.DrawString(e.Header.Text, e.Font, textBrush, e.Bounds, sf);
            }

            using (var pen = new Pen(CurrentTheme.ListViewHeaderBorderColor))
            {
                e.Graphics.DrawLine(pen, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom);
            }
        }

        /// <summary>ListView 아이템 그리기</summary>
        private static void OnListViewDrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // 커스텀 그리기 모드에서는 DrawDefault를 사용하지 않음
        }

        /// <summary>ListView 서브 아이템 그리기</summary>
        private static void OnListViewDrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Color backColor;
            Color textColor;

            if (e.Item.Selected)
            {
                backColor = CurrentTheme.ListViewSelectedBackColor;
                textColor = CurrentTheme.ListViewSelectedForeColor;
            }
            else if (e.ItemIndex % 2 == 0)
            {
                backColor = CurrentTheme.ListViewBackColor;
                textColor = CurrentTheme.ListViewForeColor;
            }
            else
            {
                backColor = CurrentTheme.ListViewAlternateRowBackColor;
                textColor = CurrentTheme.ListViewForeColor;
            }

            using (var brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            using (var textBrush = new SolidBrush(textColor))
            {
                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };
                e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, textBrush, e.Bounds, sf);
            }

            using (var pen = new Pen(CurrentTheme.ListViewRowSeparatorColor))
            {
                e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
            }
        }

        /// <summary>TreeView 노드 커스텀 그리기 (점선 테두리 제거)</summary>
        private static void OnTreeViewDrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            var treeView = sender as TreeView;
            if (treeView == null) return;

            // 배경색과 텍스트 색상 결정
            Color backColor;
            Color textColor;

            if (e.Node.IsSelected)
            {
                backColor = CurrentTheme.TreeViewSelectedBackColor;
                textColor = CurrentTheme.TreeViewSelectedForeColor;
            }
            else if ((e.State & TreeNodeStates.Hot) != 0)
            {
                backColor = CurrentTheme.TreeViewHoverBackColor;
                textColor = CurrentTheme.TreeViewHoverForeColor;
            }
            else
            {
                backColor = Color.Transparent;
                textColor = CurrentTheme.TreeViewForeColor;
            }

            // 배경 그리기 (선택된 경우에만)
            if (e.Node.IsSelected || (e.State & TreeNodeStates.Hot) != 0)
            {
                using (var brush = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }

            // 텍스트 그리기
            using (var textBrush = new SolidBrush(textColor))
            {
                var textRect = e.Bounds;
                textRect.X += 2; // 약간의 여백
                textRect.Width = 200;

                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                e.Graphics.DrawString(e.Node.Text, treeView.Font, textBrush, textRect, sf);
            }
        }

        #endregion
    }

    #region 커스텀 렌더러

    /// <summary>ToolStrip 및 StatusStrip 다크 테마 렌더러</summary>
    public class DarkToolStripRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item is ToolStripButton button)
            {
                Color color = Color.Transparent;

                if (button.Pressed)
                    color = ThemeManager.CurrentTheme.ToolStripButtonPressedColor;
                else if (button.Selected)
                    color = ThemeManager.CurrentTheme.ToolStripButtonHoverColor;
                else if (button.Checked)
                    color = ThemeManager.CurrentTheme.ToolStripButtonCheckedColor;
                if (color != Color.Transparent)
                {
                    // 완전히 각진 네모로 그리기 (둥근 모서리 제거)
                    using (SolidBrush brush = new SolidBrush(color))
                    {
                        e.Graphics.FillRectangle(brush, e.Item.Bounds);
                    }
                }
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = ThemeManager.CurrentTheme.ToolStripForeColor;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(ThemeManager.CurrentTheme.ToolStripSeparatorColor),
                e.Item.Bounds.Left + 2, e.Item.Bounds.Top + 2,
                e.Item.Bounds.Left + 2, e.Item.Bounds.Bottom - 2);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(ThemeManager.CurrentTheme.ToolStripBackColor), e.AffectedBounds);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            // 테두리 제거
        }
    }

    #endregion
}