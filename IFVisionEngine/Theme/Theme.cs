using System.Drawing;

namespace IFVisionEngine.Themes
{
    /// <summary>
    /// 애플리케이션 전체 테마 색상을 정의하는 클래스
    /// </summary>
    public class Theme
    {
        #region 기본 색상
        /// <summary>기본 배경색</summary>
        public Color BackgroundColor { get; set; }
        /// <summary>기본 전경색</summary>
        public Color ForegroundColor { get; set; }
        /// <summary>기본 테두리색</summary>
        public Color BorderColor { get; set; }
        /// <summary>어두운 테두리색</summary>
        public Color DarkBorderColor { get; set; }
        /// <summary>패널 테두리색</summary>
        public Color PanelBorderColor { get; set; }
        #endregion

        #region 기본 컨트롤 색상
        /// <summary>버튼 배경색</summary>
        public Color ButtonBackColor { get; set; }
        /// <summary>버튼 전경색</summary>
        public Color ButtonForeColor { get; set; }
        /// <summary>버튼 테두리색</summary>
        public Color ButtonBorderColor { get; set; }

        /// <summary>텍스트박스 배경색</summary>
        public Color TextBoxBackColor { get; set; }
        /// <summary>텍스트박스 전경색</summary>
        public Color TextBoxForeColor { get; set; }

        /// <summary>라벨 전경색</summary>
        public Color LabelForeColor { get; set; }
        #endregion

        #region PropertyGrid 색상
        /// <summary>PropertyGrid 메인 배경색</summary>
        public Color PropertyGridBackColor { get; set; }
        /// <summary>PropertyGrid 뷰 배경색</summary>
        public Color PropertyGridViewBackColor { get; set; }
        /// <summary>PropertyGrid 뷰 전경색</summary>
        public Color PropertyGridViewForeColor { get; set; }
        /// <summary>PropertyGrid 카테고리 전경색</summary>
        public Color PropertyGridCategoryForeColor { get; set; }
        /// <summary>PropertyGrid 카테고리 구분선 색상</summary>
        public Color PropertyGridCategorySplitterColor { get; set; }
        /// <summary>PropertyGrid 라인 색상</summary>
        public Color PropertyGridLineColor { get; set; }
        /// <summary>PropertyGrid 선택된 항목 배경색</summary>
        public Color PropertyGridSelectedBackColor { get; set; }
        /// <summary>PropertyGrid 선택된 항목 전경색</summary>
        public Color PropertyGridSelectedForeColor { get; set; }
        /// <summary>PropertyGrid 비활성화 항목 전경색</summary>
        public Color PropertyGridDisabledForeColor { get; set; }
        /// <summary>PropertyGrid 명령 영역 배경색</summary>
        public Color PropertyGridCommandsBackColor { get; set; }
        /// <summary>PropertyGrid 명령 영역 전경색</summary>
        public Color PropertyGridCommandsForeColor { get; set; }
        /// <summary>PropertyGrid 명령 영역 테두리색</summary>
        public Color PropertyGridCommandsBorderColor { get; set; }
        /// <summary>PropertyGrid 도움말 영역 배경색</summary>
        public Color PropertyGridHelpBackColor { get; set; }
        /// <summary>PropertyGrid 도움말 영역 전경색</summary>
        public Color PropertyGridHelpForeColor { get; set; }
        /// <summary>PropertyGrid 도움말 영역 테두리색</summary>
        public Color PropertyGridHelpBorderColor { get; set; }
        /// <summary>PropertyGrid 뷰 테두리색</summary>
        public Color PropertyGridViewBorderColor { get; set; }
        #endregion

        #region ListView 색상
        /// <summary>ListView 배경색</summary>
        public Color ListViewBackColor { get; set; }
        /// <summary>ListView 전경색</summary>
        public Color ListViewForeColor { get; set; }
        /// <summary>ListView 헤더 배경색</summary>
        public Color ListViewHeaderBackColor { get; set; }
        /// <summary>ListView 헤더 전경색</summary>
        public Color ListViewHeaderForeColor { get; set; }
        /// <summary>ListView 헤더 테두리색</summary>
        public Color ListViewHeaderBorderColor { get; set; }
        /// <summary>ListView 선택된 항목 배경색</summary>
        public Color ListViewSelectedBackColor { get; set; }
        /// <summary>ListView 선택된 항목 전경색</summary>
        public Color ListViewSelectedForeColor { get; set; }
        /// <summary>ListView 교대 행 배경색</summary>
        public Color ListViewAlternateRowBackColor { get; set; }
        /// <summary>ListView 행 구분선 색상</summary>
        public Color ListViewRowSeparatorColor { get; set; }
        #endregion

        #region TreeView 색상
        /// <summary>TreeView 배경색</summary>
        public Color TreeViewBackColor { get; set; }
        /// <summary>TreeView 전경색</summary>
        public Color TreeViewForeColor { get; set; }
        /// <summary>TreeView 라인 색상</summary>
        public Color TreeViewLineColor { get; set; }
        /// <summary>TreeView 선택된 노드 배경색</summary>
        public Color TreeViewSelectedBackColor { get; set; }
        /// <summary>TreeView 선택된 노드 전경색</summary>
        public Color TreeViewSelectedForeColor { get; set; }
        /// <summary>TreeView 호버 노드 배경색</summary>
        public Color TreeViewHoverBackColor { get; set; }
        /// <summary>TreeView 호버 노드 전경색</summary>
        public Color TreeViewHoverForeColor { get; set; }
        #endregion

        #region ToolStrip 색상
        /// <summary>ToolStrip 배경색</summary>
        public Color ToolStripBackColor { get; set; }
        /// <summary>ToolStrip 전경색</summary>
        public Color ToolStripForeColor { get; set; }
        /// <summary>ToolStrip 버튼 호버 색상</summary>
        public Color ToolStripButtonHoverColor { get; set; }
        /// <summary>ToolStrip 버튼 눌림 색상</summary>
        public Color ToolStripButtonPressedColor { get; set; }
        /// <summary>ToolStrip 버튼 체크 색상</summary>
        public Color ToolStripButtonCheckedColor { get; set; }
        /// <summary>ToolStrip 구분자 색상</summary>
        public Color ToolStripSeparatorColor { get; set; }
        #endregion

        #region SunnyUI 공통 색상
        /// <summary>SunnyUI 기본 채우기 색상</summary>
        public Color SunnyUIFillColor { get; set; }
        /// <summary>SunnyUI 기본 전경색</summary>
        public Color SunnyUIForeColor { get; set; }
        /// <summary>SunnyUI 기본 테두리색</summary>
        public Color SunnyUIRectColor { get; set; }
        /// <summary>SunnyUI 선택 배경색</summary>
        public Color SunnyUISelectBackColor { get; set; }
        /// <summary>SunnyUI 선택 전경색</summary>
        public Color SunnyUISelectForeColor { get; set; }
        /// <summary>SunnyUI 호버 색상</summary>
        public Color SunnyUIHoverColor { get; set; }
        /// <summary>SunnyUI 비활성화 전경색</summary>
        public Color SunnyUIDisabledForeColor { get; set; }
        /// <summary>SunnyUI 비활성화 테두리색</summary>
        public Color SunnyUIDisabledRectColor { get; set; }
        #endregion

        #region SunnyUI 버튼 색상
        /// <summary>SunnyUI 버튼 호버 색상</summary>
        public Color SunnyUIButtonHoverColor { get; set; }
        /// <summary>SunnyUI 버튼 눌림 색상</summary>
        public Color SunnyUIButtonPressColor { get; set; }
        #endregion

        #region SunnyUI NumericUpDown 색상
        /// <summary>SunnyUI NumericUpDown 버튼 배경색</summary>
        public Color SunnyUINumericButtonFillColor { get; set; }
        /// <summary>SunnyUI NumericUpDown 버튼 전경색</summary>
        public Color SunnyUINumericButtonForeColor { get; set; }
        #endregion

        #region SunnyUI CheckBox 색상
        /// <summary>SunnyUI CheckBox 체크 색상</summary>
        public Color SunnyUICheckBoxColor { get; set; }
        #endregion

        #region SunnyUI TrackBar 색상
        /// <summary>SunnyUI TrackBar 값 색상</summary>
        public Color SunnyUITrackBarValueColor { get; set; }
        #endregion

        #region SunnyUI Label 색상
        /// <summary>SunnyUI Label 배경색</summary>
        public Color SunnyUILabelBackColor { get; set; }
        /// <summary>SunnyUI Label 테두리색</summary>
        public Color SunnyUILabelBorderColor { get; set; }
        #endregion

        #region UILine 색상
        /// <summary>UILine 선 색상</summary>
        public Color UILineColor { get; set; }
        #endregion
        #region PicturxBox 색상
        /// <summary>PicturxBox 선 색상</summary>
        public Color PicturxBoxColor { get; set; }
        #endregion
        #region 다크 테마 정의
        /// <summary>기본 다크 테마</summary>
        public static Theme DarkTheme => new Theme
        {
            // 기본 색상
            BackgroundColor = Color.FromArgb(20, 20, 20),           // 어두운 회색 (메인 배경)
            ForegroundColor = Color.White,                          // 흰색 (기본 텍스트)
            BorderColor = Color.FromArgb(64, 64, 64),              // 중간 회색 (기본 테두리)
            DarkBorderColor = Color.FromArgb(48, 48, 48),          // 어두운 회색 (어두운 테두리)
            PanelBorderColor = Color.FromArgb(55, 55, 55),         // 중간 어두운 회색 (패널 테두리)

            // 기본 컨트롤 색상
            ButtonBackColor = Color.FromArgb(45, 45, 48),          // 어두운 회색 (버튼 배경)
            ButtonForeColor = Color.FromArgb(241, 241, 241),       // 밝은 회색 (버튼 텍스트)
            ButtonBorderColor = Color.FromArgb(80, 80, 80),        // 중간 회색 (버튼 테두리)
            TextBoxBackColor = Color.FromArgb(37, 37, 38),         // 매우 어두운 회색 (텍스트박스 배경)
            TextBoxForeColor = Color.FromArgb(241, 241, 241),      // 밝은 회색 (텍스트박스 텍스트)
            LabelForeColor = Color.FromArgb(241, 241, 241),        // 밝은 회색 (라벨 텍스트)

            // PropertyGrid 색상
            PropertyGridBackColor = Color.FromArgb(45, 45, 45),                // 어두운 회색 (PropertyGrid 배경)
            PropertyGridViewBackColor = Color.FromArgb(32, 32, 32),            // 매우 어두운 회색 (뷰 배경)
            PropertyGridViewForeColor = Color.FromArgb(220, 220, 220),         // 밝은 회색 (뷰 텍스트)
            PropertyGridCategoryForeColor = Color.FromArgb(200, 200, 200),     // 회색 (카테고리 텍스트)
            PropertyGridCategorySplitterColor = Color.FromArgb(60, 60, 60),    // 어두운 회색 (카테고리 구분선)
            PropertyGridLineColor = Color.FromArgb(55, 55, 55),                // 어두운 회색 (라인)
            PropertyGridSelectedBackColor = Color.FromArgb(0, 120, 215),       // 파란색 (선택된 항목 배경)
            PropertyGridSelectedForeColor = Color.White,                       // 흰색 (선택된 항목 텍스트)
            PropertyGridDisabledForeColor = Color.FromArgb(120, 120, 120),     // 중간 회색 (비활성화 텍스트)
            PropertyGridCommandsBackColor = Color.FromArgb(28, 28, 28),        // 매우 어두운 회색 (명령 영역 배경)
            PropertyGridCommandsForeColor = Color.FromArgb(200, 200, 200),     // 회색 (명령 영역 텍스트)
            PropertyGridCommandsBorderColor = Color.FromArgb(200, 200, 200),   // 회색 (명령 영역 테두리)
            PropertyGridHelpBackColor = Color.FromArgb(25, 25, 25),            // 매우 어두운 회색 (도움말 배경)
            PropertyGridHelpForeColor = Color.FromArgb(180, 180, 180),         // 회색 (도움말 텍스트)
            PropertyGridHelpBorderColor = Color.FromArgb(25, 25, 25),          // 매우 어두운 회색 (도움말 테두리)
            PropertyGridViewBorderColor = Color.FromArgb(32, 32, 32),          // 매우 어두운 회색 (뷰 테두리)

            // ListView 색상
            ListViewBackColor = Color.FromArgb(35, 35, 35),                    // 어두운 회색 (ListView 배경)
            ListViewForeColor = Color.FromArgb(220, 220, 220),                 // 밝은 회색 (ListView 텍스트)
            ListViewHeaderBackColor = Color.FromArgb(50, 50, 50),              // 어두운 회색 (헤더 배경)
            ListViewHeaderForeColor = Color.FromArgb(220, 220, 220),           // 밝은 회색 (헤더 텍스트)
            ListViewHeaderBorderColor = Color.FromArgb(80, 80, 80),            // 중간 회색 (헤더 테두리)
            ListViewSelectedBackColor = Color.FromArgb(0, 120, 215),           // 파란색 (선택된 항목 배경)
            ListViewSelectedForeColor = Color.White,                           // 흰색 (선택된 항목 텍스트)
            ListViewAlternateRowBackColor = Color.FromArgb(40, 40, 40),        // 조금 밝은 어두운 회색 (교대 행)
            ListViewRowSeparatorColor = Color.FromArgb(60, 60, 60),            // 어두운 회색 (행 구분선)

            // TreeView 색상
            TreeViewBackColor = Color.FromArgb(35, 35, 35),                    // 어두운 회색 (TreeView 배경)
            TreeViewForeColor = Color.FromArgb(220, 220, 220),                 // 밝은 회색 (TreeView 텍스트)
            TreeViewLineColor = Color.FromArgb(120, 120, 120),                 // 중간 회색 (TreeView 라인)
            TreeViewSelectedBackColor = Color.FromArgb(0, 120, 215),           // 파란색 (선택된 노드 배경)
            TreeViewSelectedForeColor = Color.White,                           // 흰색 (선택된 노드 텍스트)
            TreeViewHoverBackColor = Color.FromArgb(55, 55, 55),               // 밝은 어두운 회색 (호버 배경)
            TreeViewHoverForeColor = Color.FromArgb(230, 230, 230),            // 매우 밝은 회색 (호버 텍스트)

            // ToolStrip 색상
            ToolStripBackColor = Color.FromArgb(40, 40, 40),                   // 어두운 회색 (ToolStrip 배경)
            ToolStripForeColor = Color.FromArgb(220, 220, 220),                // 밝은 회색 (ToolStrip 텍스트)
            ToolStripButtonHoverColor = Color.FromArgb(70, 70, 70),            // 밝은 어두운 회색 (버튼 호버)
            ToolStripButtonPressedColor = Color.FromArgb(0, 100, 180),         // 어두운 파란색 (버튼 눌림)
            ToolStripButtonCheckedColor = Color.FromArgb(0, 120, 215),         // 파란색 (버튼 체크)
            ToolStripSeparatorColor = Color.FromArgb(80, 80, 80),              // 중간 회색 (구분자)

            // SunnyUI 공통 색상
            SunnyUIFillColor = Color.FromArgb(37, 37, 38),                     // 매우 어두운 회색 (SunnyUI 배경)
            SunnyUIForeColor = Color.FromArgb(241, 241, 241),                  // 밝은 회색 (SunnyUI 텍스트)
            SunnyUIRectColor = Color.FromArgb(120, 120, 120),                  // 중간 회색 (SunnyUI 테두리)
            SunnyUISelectBackColor = Color.FromArgb(0, 122, 204),              // 파란색 (SunnyUI 선택 배경)
            SunnyUISelectForeColor = Color.White,                              // 흰색 (SunnyUI 선택 텍스트)
            SunnyUIHoverColor = Color.FromArgb(70, 70, 70),                    // 밝은 어두운 회색 (SunnyUI 호버)
            SunnyUIDisabledForeColor = Color.FromArgb(120, 120, 120),          // 중간 회색 (SunnyUI 비활성화 텍스트)
            SunnyUIDisabledRectColor = Color.FromArgb(80, 80, 80),             // 어두운 회색 (SunnyUI 비활성화 테두리)

            // SunnyUI 버튼 색상
            SunnyUIButtonHoverColor = Color.FromArgb(0, 100, 180),             // 어두운 파란색 (버튼 호버)
            SunnyUIButtonPressColor = Color.FromArgb(0, 80, 160),              // 매우 어두운 파란색 (버튼 눌림)

            // SunnyUI NumericUpDown 색상
            SunnyUINumericButtonFillColor = Color.FromArgb(45, 45, 45),        // 어두운 회색 (NumericUpDown 버튼 배경)
            SunnyUINumericButtonForeColor = Color.FromArgb(200, 200, 200),     // 회색 (NumericUpDown 버튼 텍스트)

            // SunnyUI CheckBox 색상
            SunnyUICheckBoxColor = Color.FromArgb(0, 122, 204),                // 파란색 (CheckBox)

            // SunnyUI TrackBar 색상
            SunnyUITrackBarValueColor = Color.FromArgb(0, 122, 204),           // 파란색 (TrackBar 값)

            // SunnyUI Label 색상
            SunnyUILabelBackColor = Color.FromArgb(37, 37, 38),                // 매우 어두운 회색 (Label 배경)
                                                                      
            UILineColor = Color.FromArgb(50, 50, 50),                          // 중간 회색 (기본 선)
            PicturxBoxColor = Color.FromArgb(0, 0, 0),                         // 검은 배경
        };
        #endregion
    }
}