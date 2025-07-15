namespace IFVisionEngine.UIComponents.Dialogs
{
    partial class RadialLinesParameterControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.panel_Main = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_ShowVisualization = new Sunny.UI.UICheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_CenterMethod = new Sunny.UI.UIComboBox();
            this.label_ManualX = new System.Windows.Forms.Label();
            this.numericUpDown_ManualX = new Sunny.UI.UIIntegerUpDown();
            this.label_ManualY = new System.Windows.Forms.Label();
            this.numericUpDown_ManualY = new Sunny.UI.UIIntegerUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_RangeMethod = new Sunny.UI.UIComboBox();
            this.label_FixedLength = new System.Windows.Forms.Label();
            this.panel_FixedLength = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel_LineCount = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.panel_StartAngle = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.button_LineColor = new Sunny.UI.UIButton();
            this.label10 = new System.Windows.Forms.Label();
            this.panel_LineThickness = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox_Style = new Sunny.UI.UIComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBox_ShowCenter = new Sunny.UI.UICheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.checkBox_ShowAngles = new Sunny.UI.UICheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.checkBox_ShowDistances = new Sunny.UI.UICheckBox();
            this.label_BinaryThreshold = new System.Windows.Forms.Label();
            this.panel_BinaryThreshold = new System.Windows.Forms.Panel();
            this.label_BrightnessThreshold = new System.Windows.Forms.Label();
            this.panel_BrightnessThreshold = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.checkBox_OutputLengthData = new Sunny.UI.UICheckBox();
            this.trackBar_FixedLength = new Sunny.UI.UITrackBar();
            this.numericUpDown_FixedLength = new Sunny.UI.UIIntegerUpDown();
            this.trackBar_LineCount = new Sunny.UI.UITrackBar();
            this.numericUpDown_LineCount = new Sunny.UI.UIIntegerUpDown();
            this.trackBar_StartAngle = new Sunny.UI.UITrackBar();
            this.numericUpDown_StartAngle = new Sunny.UI.UIIntegerUpDown();
            this.trackBar_LineThickness = new Sunny.UI.UITrackBar();
            this.numericUpDown_LineThickness = new Sunny.UI.UIIntegerUpDown();
            this.trackBar_BinaryThreshold = new Sunny.UI.UITrackBar();
            this.numericUpDown_BinaryThreshold = new Sunny.UI.UIIntegerUpDown();
            this.trackBar_BrightnessThreshold = new Sunny.UI.UITrackBar();
            this.numericUpDown_BrightnessThreshold = new Sunny.UI.UIIntegerUpDown();
            this.panel_Main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Main
            // 
            this.panel_Main.AutoScroll = true;
            this.panel_Main.Controls.Add(this.tableLayoutPanel1);
            this.panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Main.Location = new System.Drawing.Point(0, 0);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Size = new System.Drawing.Size(400, 600);
            this.panel_Main.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_ShowVisualization, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_CenterMethod, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_ManualX, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown_ManualX, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_ManualY, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown_ManualY, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_RangeMethod, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label_FixedLength, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel_FixedLength, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.panel_LineCount, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.panel_StartAngle, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.button_LineColor, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.panel_LineThickness, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_Style, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_ShowCenter, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_ShowAngles, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_ShowDistances, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.label_BinaryThreshold, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.panel_BinaryThreshold, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.label_BrightnessThreshold, 0, 15);
            this.tableLayoutPanel1.Controls.Add(this.panel_BrightnessThreshold, 1, 15);
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 16);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_OutputLengthData, 1, 16);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 17;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 476);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // Labels 설정
            SetupLabel(this.label1, "시각화:", 6);
            SetupLabel(this.label2, "중심점 방법:", 30);
            SetupLabel(this.label_ManualX, "수동 X좌표:", 54);
            SetupLabel(this.label_ManualY, "수동 Y좌표:", 78);
            SetupLabel(this.label5, "범위 방법:", 102);
            SetupLabel(this.label_FixedLength, "고정 길이:", 126);
            SetupLabel(this.label7, "선 개수:", 150);
            SetupLabel(this.label8, "시작 각도:", 174);
            SetupLabel(this.label9, "선 색상:", 198);
            SetupLabel(this.label10, "선 두께:", 222);
            SetupLabel(this.label11, "선 스타일:", 246);
            SetupLabel(this.label12, "중심점 표시:", 270);
            SetupLabel(this.label13, "각도 표시:", 294);
            SetupLabel(this.label14, "거리 표시:", 318);
            SetupLabel(this.label_BinaryThreshold, "이진화 임계값:", 342);
            SetupLabel(this.label_BrightnessThreshold, "밝기 임계값:", 366);
            SetupLabel(this.label17, "길이 데이터 출력:", 390);

            // CheckBoxes
            SetupCheckBox(this.checkBox_ShowVisualization, "활성화", 4, true);
            SetupCheckBox(this.checkBox_ShowCenter, "활성화", 268, true);
            SetupCheckBox(this.checkBox_ShowAngles, "활성화", 292, false);
            SetupCheckBox(this.checkBox_ShowDistances, "활성화", 316, false);
            SetupCheckBox(this.checkBox_OutputLengthData, "활성화", 388, true);

            // ComboBoxes
            SetupComboBox(this.comboBox_CenterMethod, 26);
            SetupComboBox(this.comboBox_RangeMethod, 98);
            SetupComboBox(this.comboBox_Style, 242);

            // NumericUpDowns (단독)
            SetupNumericUpDown(this.numericUpDown_ManualX, 50, 0, 2000, 0);
            SetupNumericUpDown(this.numericUpDown_ManualY, 74, 0, 2000, 0);

            // TrackBar + NumericUpDown 패널들
            SetupTrackBarPanel(this.panel_FixedLength, this.trackBar_FixedLength, this.numericUpDown_FixedLength, 122);
            SetupTrackBarPanel(this.panel_LineCount, this.trackBar_LineCount, this.numericUpDown_LineCount, 146);
            SetupTrackBarPanel(this.panel_StartAngle, this.trackBar_StartAngle, this.numericUpDown_StartAngle, 170);
            SetupTrackBarPanel(this.panel_LineThickness, this.trackBar_LineThickness, this.numericUpDown_LineThickness, 218);
            SetupTrackBarPanel(this.panel_BinaryThreshold, this.trackBar_BinaryThreshold, this.numericUpDown_BinaryThreshold, 338);
            SetupTrackBarPanel(this.panel_BrightnessThreshold, this.trackBar_BrightnessThreshold, this.numericUpDown_BrightnessThreshold, 362);
            // 
            // checkBox_ShowVisualization
            // 
            this.checkBox_ShowVisualization.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_ShowVisualization.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox_ShowVisualization.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_ShowVisualization.Location = new System.Drawing.Point(163, 3);
            this.checkBox_ShowVisualization.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_ShowVisualization.Name = "checkBox_ShowVisualization";
            this.checkBox_ShowVisualization.Size = new System.Drawing.Size(186, 22);
            this.checkBox_ShowVisualization.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 2;
            // 
            // comboBox_CenterMethod
            // 
            this.comboBox_CenterMethod.DataSource = null;
            this.comboBox_CenterMethod.FillColor = System.Drawing.Color.White;
            this.comboBox_CenterMethod.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox_CenterMethod.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.comboBox_CenterMethod.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.comboBox_CenterMethod.Location = new System.Drawing.Point(164, 33);
            this.comboBox_CenterMethod.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox_CenterMethod.MinimumSize = new System.Drawing.Size(63, 0);
            this.comboBox_CenterMethod.Name = "comboBox_CenterMethod";
            this.comboBox_CenterMethod.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.comboBox_CenterMethod.Size = new System.Drawing.Size(150, 18);
            this.comboBox_CenterMethod.SymbolSize = 24;
            this.comboBox_CenterMethod.TabIndex = 3;
            this.comboBox_CenterMethod.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBox_CenterMethod.Watermark = "";
            // 
            // label_ManualX
            // 
            this.label_ManualX.Location = new System.Drawing.Point(3, 56);
            this.label_ManualX.Name = "label_ManualX";
            this.label_ManualX.Size = new System.Drawing.Size(100, 23);
            this.label_ManualX.TabIndex = 4;
            // 
            // numericUpDown_ManualX
            // 
            this.numericUpDown_ManualX.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.numericUpDown_ManualX.Location = new System.Drawing.Point(164, 61);
            this.numericUpDown_ManualX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown_ManualX.MinimumSize = new System.Drawing.Size(100, 0);
            this.numericUpDown_ManualX.Name = "numericUpDown_ManualX";
            this.numericUpDown_ManualX.ShowText = false;
            this.numericUpDown_ManualX.Size = new System.Drawing.Size(165, 20);
            this.numericUpDown_ManualX.TabIndex = 5;
            this.numericUpDown_ManualX.Text = null;
            this.numericUpDown_ManualX.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_ManualY
            // 
            this.label_ManualY.Location = new System.Drawing.Point(3, 84);
            this.label_ManualY.Name = "label_ManualY";
            this.label_ManualY.Size = new System.Drawing.Size(100, 23);
            this.label_ManualY.TabIndex = 6;
            // 
            // numericUpDown_ManualY
            // 
            this.numericUpDown_ManualY.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.numericUpDown_ManualY.Location = new System.Drawing.Point(164, 89);
            this.numericUpDown_ManualY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown_ManualY.MinimumSize = new System.Drawing.Size(100, 0);
            this.numericUpDown_ManualY.Name = "numericUpDown_ManualY";
            this.numericUpDown_ManualY.ShowText = false;
            this.numericUpDown_ManualY.Size = new System.Drawing.Size(165, 20);
            this.numericUpDown_ManualY.TabIndex = 7;
            this.numericUpDown_ManualY.Text = null;
            this.numericUpDown_ManualY.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 8;
            // 
            // comboBox_RangeMethod
            // 
            this.comboBox_RangeMethod.DataSource = null;
            this.comboBox_RangeMethod.FillColor = System.Drawing.Color.White;
            this.comboBox_RangeMethod.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox_RangeMethod.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.comboBox_RangeMethod.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.comboBox_RangeMethod.Location = new System.Drawing.Point(164, 117);
            this.comboBox_RangeMethod.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox_RangeMethod.MinimumSize = new System.Drawing.Size(63, 0);
            this.comboBox_RangeMethod.Name = "comboBox_RangeMethod";
            this.comboBox_RangeMethod.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.comboBox_RangeMethod.Size = new System.Drawing.Size(150, 18);
            this.comboBox_RangeMethod.SymbolSize = 24;
            this.comboBox_RangeMethod.TabIndex = 9;
            this.comboBox_RangeMethod.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBox_RangeMethod.Watermark = "";
            // 
            // label_FixedLength
            // 
            this.label_FixedLength.Location = new System.Drawing.Point(3, 140);
            this.label_FixedLength.Name = "label_FixedLength";
            this.label_FixedLength.Size = new System.Drawing.Size(100, 23);
            this.label_FixedLength.TabIndex = 10;
            // 
            // panel_FixedLength
            // 
            this.panel_FixedLength.Location = new System.Drawing.Point(163, 143);
            this.panel_FixedLength.Name = "panel_FixedLength";
            this.panel_FixedLength.Size = new System.Drawing.Size(200, 22);
            this.panel_FixedLength.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 12;
            // 
            // panel_LineCount
            // 
            this.panel_LineCount.Location = new System.Drawing.Point(163, 171);
            this.panel_LineCount.Name = "panel_LineCount";
            this.panel_LineCount.Size = new System.Drawing.Size(200, 22);
            this.panel_LineCount.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 14;
            // 
            // panel_StartAngle
            // 
            this.panel_StartAngle.Location = new System.Drawing.Point(163, 199);
            this.panel_StartAngle.Name = "panel_StartAngle";
            this.panel_StartAngle.Size = new System.Drawing.Size(200, 22);
            this.panel_StartAngle.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 224);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 23);
            this.label9.TabIndex = 16;
            // 
            // button_LineColor
            // 
            this.button_LineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LineColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_LineColor.FillColor = System.Drawing.Color.Red;
            this.button_LineColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button_LineColor.Location = new System.Drawing.Point(163, 227);
            this.button_LineColor.MinimumSize = new System.Drawing.Size(1, 1);
            this.button_LineColor.Name = "button_LineColor";
            this.button_LineColor.RectColor = System.Drawing.Color.Red;
            this.button_LineColor.Size = new System.Drawing.Size(234, 22);
            this.button_LineColor.TabIndex = 9;
            this.button_LineColor.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 252);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 17;
            // 
            // panel_LineThickness
            // 
            this.panel_LineThickness.Location = new System.Drawing.Point(163, 255);
            this.panel_LineThickness.Name = "panel_LineThickness";
            this.panel_LineThickness.Size = new System.Drawing.Size(200, 22);
            this.panel_LineThickness.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 280);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 23);
            this.label11.TabIndex = 19;
            // 
            // comboBox_Style
            // 
            this.comboBox_Style.DataSource = null;
            this.comboBox_Style.FillColor = System.Drawing.Color.White;
            this.comboBox_Style.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox_Style.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.comboBox_Style.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.comboBox_Style.Location = new System.Drawing.Point(164, 285);
            this.comboBox_Style.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox_Style.MinimumSize = new System.Drawing.Size(63, 0);
            this.comboBox_Style.Name = "comboBox_Style";
            this.comboBox_Style.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.comboBox_Style.Size = new System.Drawing.Size(150, 18);
            this.comboBox_Style.SymbolSize = 24;
            this.comboBox_Style.TabIndex = 20;
            this.comboBox_Style.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBox_Style.Watermark = "";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(3, 308);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 23);
            this.label12.TabIndex = 21;
            // 
            // checkBox_ShowCenter
            // 
            this.checkBox_ShowCenter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_ShowCenter.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox_ShowCenter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_ShowCenter.Location = new System.Drawing.Point(163, 311);
            this.checkBox_ShowCenter.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_ShowCenter.Name = "checkBox_ShowCenter";
            this.checkBox_ShowCenter.Size = new System.Drawing.Size(186, 22);
            this.checkBox_ShowCenter.TabIndex = 22;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(3, 336);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 23);
            this.label13.TabIndex = 23;
            // 
            // checkBox_ShowAngles
            // 
            this.checkBox_ShowAngles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_ShowAngles.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox_ShowAngles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_ShowAngles.Location = new System.Drawing.Point(163, 339);
            this.checkBox_ShowAngles.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_ShowAngles.Name = "checkBox_ShowAngles";
            this.checkBox_ShowAngles.Size = new System.Drawing.Size(186, 22);
            this.checkBox_ShowAngles.TabIndex = 24;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(3, 364);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 23);
            this.label14.TabIndex = 25;
            // 
            // checkBox_ShowDistances
            // 
            this.checkBox_ShowDistances.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_ShowDistances.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox_ShowDistances.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_ShowDistances.Location = new System.Drawing.Point(163, 367);
            this.checkBox_ShowDistances.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_ShowDistances.Name = "checkBox_ShowDistances";
            this.checkBox_ShowDistances.Size = new System.Drawing.Size(186, 22);
            this.checkBox_ShowDistances.TabIndex = 26;
            // 
            // label_BinaryThreshold
            // 
            this.label_BinaryThreshold.Location = new System.Drawing.Point(3, 392);
            this.label_BinaryThreshold.Name = "label_BinaryThreshold";
            this.label_BinaryThreshold.Size = new System.Drawing.Size(100, 23);
            this.label_BinaryThreshold.TabIndex = 27;
            // 
            // panel_BinaryThreshold
            // 
            this.panel_BinaryThreshold.Location = new System.Drawing.Point(163, 395);
            this.panel_BinaryThreshold.Name = "panel_BinaryThreshold";
            this.panel_BinaryThreshold.Size = new System.Drawing.Size(200, 22);
            this.panel_BinaryThreshold.TabIndex = 28;
            // 
            // label_BrightnessThreshold
            // 
            this.label_BrightnessThreshold.Location = new System.Drawing.Point(3, 420);
            this.label_BrightnessThreshold.Name = "label_BrightnessThreshold";
            this.label_BrightnessThreshold.Size = new System.Drawing.Size(100, 23);
            this.label_BrightnessThreshold.TabIndex = 29;
            // 
            // panel_BrightnessThreshold
            // 
            this.panel_BrightnessThreshold.Location = new System.Drawing.Point(163, 423);
            this.panel_BrightnessThreshold.Name = "panel_BrightnessThreshold";
            this.panel_BrightnessThreshold.Size = new System.Drawing.Size(200, 22);
            this.panel_BrightnessThreshold.TabIndex = 30;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(3, 448);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(100, 23);
            this.label17.TabIndex = 31;
            // 
            // checkBox_OutputLengthData
            // 
            this.checkBox_OutputLengthData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_OutputLengthData.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox_OutputLengthData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_OutputLengthData.Location = new System.Drawing.Point(163, 451);
            this.checkBox_OutputLengthData.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_OutputLengthData.Name = "checkBox_OutputLengthData";
            this.checkBox_OutputLengthData.Size = new System.Drawing.Size(150, 22);
            this.checkBox_OutputLengthData.TabIndex = 32;
            // 
            // trackBar_FixedLength
            // 
            this.trackBar_FixedLength.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.trackBar_FixedLength.Location = new System.Drawing.Point(0, 0);
            this.trackBar_FixedLength.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_FixedLength.Name = "trackBar_FixedLength";
            this.trackBar_FixedLength.Size = new System.Drawing.Size(150, 29);
            this.trackBar_FixedLength.TabIndex = 0;
            // 
            // numericUpDown_FixedLength
            // 
            this.numericUpDown_FixedLength.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.numericUpDown_FixedLength.Location = new System.Drawing.Point(0, 0);
            this.numericUpDown_FixedLength.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown_FixedLength.MinimumSize = new System.Drawing.Size(100, 0);
            this.numericUpDown_FixedLength.Name = "numericUpDown_FixedLength";
            this.numericUpDown_FixedLength.ShowText = false;
            this.numericUpDown_FixedLength.Size = new System.Drawing.Size(116, 29);
            this.numericUpDown_FixedLength.TabIndex = 0;
            this.numericUpDown_FixedLength.Text = null;
            this.numericUpDown_FixedLength.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_LineCount
            // 
            this.trackBar_LineCount.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.trackBar_LineCount.Location = new System.Drawing.Point(0, 0);
            this.trackBar_LineCount.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_LineCount.Name = "trackBar_LineCount";
            this.trackBar_LineCount.Size = new System.Drawing.Size(150, 29);
            this.trackBar_LineCount.TabIndex = 0;
            // 
            // numericUpDown_LineCount
            // 
            this.numericUpDown_LineCount.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.numericUpDown_LineCount.Location = new System.Drawing.Point(0, 0);
            this.numericUpDown_LineCount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown_LineCount.MinimumSize = new System.Drawing.Size(100, 0);
            this.numericUpDown_LineCount.Name = "numericUpDown_LineCount";
            this.numericUpDown_LineCount.ShowText = false;
            this.numericUpDown_LineCount.Size = new System.Drawing.Size(116, 29);
            this.numericUpDown_LineCount.TabIndex = 0;
            this.numericUpDown_LineCount.Text = null;
            this.numericUpDown_LineCount.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_StartAngle
            // 
            this.trackBar_StartAngle.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.trackBar_StartAngle.Location = new System.Drawing.Point(0, 0);
            this.trackBar_StartAngle.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_StartAngle.Name = "trackBar_StartAngle";
            this.trackBar_StartAngle.Size = new System.Drawing.Size(150, 29);
            this.trackBar_StartAngle.TabIndex = 0;
            // 
            // numericUpDown_StartAngle
            // 
            this.numericUpDown_StartAngle.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.numericUpDown_StartAngle.Location = new System.Drawing.Point(0, 0);
            this.numericUpDown_StartAngle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown_StartAngle.MinimumSize = new System.Drawing.Size(100, 0);
            this.numericUpDown_StartAngle.Name = "numericUpDown_StartAngle";
            this.numericUpDown_StartAngle.ShowText = false;
            this.numericUpDown_StartAngle.Size = new System.Drawing.Size(116, 29);
            this.numericUpDown_StartAngle.TabIndex = 0;
            this.numericUpDown_StartAngle.Text = null;
            this.numericUpDown_StartAngle.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_LineThickness
            // 
            this.trackBar_LineThickness.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.trackBar_LineThickness.Location = new System.Drawing.Point(0, 0);
            this.trackBar_LineThickness.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_LineThickness.Name = "trackBar_LineThickness";
            this.trackBar_LineThickness.Size = new System.Drawing.Size(150, 29);
            this.trackBar_LineThickness.TabIndex = 0;
            // 
            // numericUpDown_LineThickness
            // 
            this.numericUpDown_LineThickness.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.numericUpDown_LineThickness.Location = new System.Drawing.Point(0, 0);
            this.numericUpDown_LineThickness.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown_LineThickness.MinimumSize = new System.Drawing.Size(100, 0);
            this.numericUpDown_LineThickness.Name = "numericUpDown_LineThickness";
            this.numericUpDown_LineThickness.ShowText = false;
            this.numericUpDown_LineThickness.Size = new System.Drawing.Size(116, 29);
            this.numericUpDown_LineThickness.TabIndex = 0;
            this.numericUpDown_LineThickness.Text = null;
            this.numericUpDown_LineThickness.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_BinaryThreshold
            // 
            this.trackBar_BinaryThreshold.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.trackBar_BinaryThreshold.Location = new System.Drawing.Point(0, 0);
            this.trackBar_BinaryThreshold.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_BinaryThreshold.Name = "trackBar_BinaryThreshold";
            this.trackBar_BinaryThreshold.Size = new System.Drawing.Size(150, 29);
            this.trackBar_BinaryThreshold.TabIndex = 0;
            // 
            // numericUpDown_BinaryThreshold
            // 
            this.numericUpDown_BinaryThreshold.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.numericUpDown_BinaryThreshold.Location = new System.Drawing.Point(0, 0);
            this.numericUpDown_BinaryThreshold.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown_BinaryThreshold.MinimumSize = new System.Drawing.Size(100, 0);
            this.numericUpDown_BinaryThreshold.Name = "numericUpDown_BinaryThreshold";
            this.numericUpDown_BinaryThreshold.ShowText = false;
            this.numericUpDown_BinaryThreshold.Size = new System.Drawing.Size(116, 29);
            this.numericUpDown_BinaryThreshold.TabIndex = 0;
            this.numericUpDown_BinaryThreshold.Text = null;
            this.numericUpDown_BinaryThreshold.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_BrightnessThreshold
            // 
            this.trackBar_BrightnessThreshold.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.trackBar_BrightnessThreshold.Location = new System.Drawing.Point(0, 0);
            this.trackBar_BrightnessThreshold.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_BrightnessThreshold.Name = "trackBar_BrightnessThreshold";
            this.trackBar_BrightnessThreshold.Size = new System.Drawing.Size(150, 29);
            this.trackBar_BrightnessThreshold.TabIndex = 0;
            // 
            // numericUpDown_BrightnessThreshold
            // 
            this.numericUpDown_BrightnessThreshold.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.numericUpDown_BrightnessThreshold.Location = new System.Drawing.Point(0, 0);
            this.numericUpDown_BrightnessThreshold.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown_BrightnessThreshold.MinimumSize = new System.Drawing.Size(100, 0);
            this.numericUpDown_BrightnessThreshold.Name = "numericUpDown_BrightnessThreshold";
            this.numericUpDown_BrightnessThreshold.ShowText = false;
            this.numericUpDown_BrightnessThreshold.Size = new System.Drawing.Size(116, 29);
            this.numericUpDown_BrightnessThreshold.TabIndex = 0;
            this.numericUpDown_BrightnessThreshold.Text = null;
            this.numericUpDown_BrightnessThreshold.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RadialLinesParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_Main);
            this.Name = "RadialLinesParameterControl";
            this.Size = new System.Drawing.Size(400, 600);
            this.Load += new System.EventHandler(this.RadialLinesParameterControl_Load);
            this.panel_Main.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void SetupLabel(System.Windows.Forms.Label label, string text, int y)
        {
            label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(3, y);
            label.Name = label.Name;
            label.Size = new System.Drawing.Size(61, 12);
            label.TabIndex = 0;
            label.Text = text;
        }


        private void SetupCheckBox(Sunny.UI.UICheckBox checkBox, string text, int y, bool isChecked)
        {
            checkBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            checkBox.Checked = isChecked;
            checkBox.Cursor = System.Windows.Forms.Cursors.Hand;
            checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            checkBox.Location = new System.Drawing.Point(163, y);
            checkBox.MinimumSize = new System.Drawing.Size(1, 1);
            checkBox.Name = checkBox.Name;
            checkBox.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            checkBox.Size = new System.Drawing.Size(60, 16);
            checkBox.TabIndex = 1;
            checkBox.Text = text;
        }

        private void SetupComboBox(Sunny.UI.UIComboBox comboBox, int y)
        {
            comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            comboBox.DataSource = null;
            comboBox.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            comboBox.FillColor = System.Drawing.Color.White;
            comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            comboBox.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            comboBox.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            comboBox.Location = new System.Drawing.Point(163, y);
            comboBox.MinimumSize = new System.Drawing.Size(63, 0);
            comboBox.Name = comboBox.Name;
            comboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            comboBox.Size = new System.Drawing.Size(234, 20);
            comboBox.SymbolSize = 24;
            comboBox.TabIndex = 2;
            comboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            comboBox.Watermark = "";
        }

        private void SetupNumericUpDown(Sunny.UI.UIIntegerUpDown numericUpDown, int y, int min, int max, int value)
        {
            numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            numericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            numericUpDown.Location = new System.Drawing.Point(163, y);
            numericUpDown.Maximum = max;
            numericUpDown.Minimum = min;
            numericUpDown.MinimumSize = new System.Drawing.Size(1, 1);
            numericUpDown.Name = numericUpDown.Name;
            numericUpDown.ShowText = false;
            numericUpDown.Size = new System.Drawing.Size(234, 21);
            numericUpDown.TabIndex = 3;
            numericUpDown.Text = "uiIntegerUpDown";
            numericUpDown.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            numericUpDown.Value = value;
        }

        private void SetupTrackBarPanel(System.Windows.Forms.Panel panel, Sunny.UI.UITrackBar trackBar, Sunny.UI.UIIntegerUpDown numericUpDown, int y)
        {
            panel.Controls.Add(trackBar);
            panel.Controls.Add(numericUpDown);
            panel.Dock = System.Windows.Forms.DockStyle.Fill;
            panel.Location = new System.Drawing.Point(163, y);
            panel.Name = panel.Name;
            panel.Size = new System.Drawing.Size(234, 22);
            panel.TabIndex = 4;

            trackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            trackBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            trackBar.Location = new System.Drawing.Point(0, 0);
            trackBar.Name = trackBar.Name;
            trackBar.Size = new System.Drawing.Size(174, 22);
            trackBar.TabIndex = 0;
            trackBar.Text = "uiTrackBar";

            numericUpDown.Dock = System.Windows.Forms.DockStyle.Right;
            numericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            numericUpDown.Location = new System.Drawing.Point(174, 0);
            numericUpDown.MinimumSize = new System.Drawing.Size(1, 1);
            numericUpDown.Name = numericUpDown.Name;
            numericUpDown.ShowText = false;
            numericUpDown.Size = new System.Drawing.Size(60, 21);
            numericUpDown.TabIndex = 1;
            numericUpDown.Text = "uiIntegerUpDown";
            numericUpDown.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
        }

        // 컨트롤 선언
        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UICheckBox checkBox_ShowVisualization;
        private System.Windows.Forms.Label label2;
        private Sunny.UI.UIComboBox comboBox_CenterMethod;
        private System.Windows.Forms.Label label_ManualX;
        private Sunny.UI.UIIntegerUpDown numericUpDown_ManualX;
        private System.Windows.Forms.Label label_ManualY;
        private Sunny.UI.UIIntegerUpDown numericUpDown_ManualY;
        private System.Windows.Forms.Label label5;
        private Sunny.UI.UIComboBox comboBox_RangeMethod;
        private System.Windows.Forms.Label label_FixedLength;
        private System.Windows.Forms.Panel panel_FixedLength;
        private Sunny.UI.UITrackBar trackBar_FixedLength;
        private Sunny.UI.UIIntegerUpDown numericUpDown_FixedLength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel_LineCount;
        private Sunny.UI.UITrackBar trackBar_LineCount;
        private Sunny.UI.UIIntegerUpDown numericUpDown_LineCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel_StartAngle;
        private Sunny.UI.UITrackBar trackBar_StartAngle;
        private Sunny.UI.UIIntegerUpDown numericUpDown_StartAngle;
        private System.Windows.Forms.Label label9;
        private Sunny.UI.UIButton button_LineColor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel_LineThickness;
        private Sunny.UI.UITrackBar trackBar_LineThickness;
        private Sunny.UI.UIIntegerUpDown numericUpDown_LineThickness;
        private System.Windows.Forms.Label label11;
        private Sunny.UI.UIComboBox comboBox_Style;
        private System.Windows.Forms.Label label12;
        private Sunny.UI.UICheckBox checkBox_ShowCenter;
        private System.Windows.Forms.Label label13;
        private Sunny.UI.UICheckBox checkBox_ShowAngles;
        private System.Windows.Forms.Label label14;
        private Sunny.UI.UICheckBox checkBox_ShowDistances;
        private System.Windows.Forms.Label label_BinaryThreshold;
        private System.Windows.Forms.Panel panel_BinaryThreshold;
        private Sunny.UI.UITrackBar trackBar_BinaryThreshold;
        private Sunny.UI.UIIntegerUpDown numericUpDown_BinaryThreshold;
        private System.Windows.Forms.Label label_BrightnessThreshold;
        private System.Windows.Forms.Panel panel_BrightnessThreshold;
        private Sunny.UI.UITrackBar trackBar_BrightnessThreshold;
        private Sunny.UI.UIIntegerUpDown numericUpDown_BrightnessThreshold;
        private System.Windows.Forms.Label label17;
        private Sunny.UI.UICheckBox checkBox_OutputLengthData;
    }
}