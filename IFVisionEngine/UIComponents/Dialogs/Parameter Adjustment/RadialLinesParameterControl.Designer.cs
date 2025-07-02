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

            // 시각화
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_ShowVisualization = new System.Windows.Forms.CheckBox();

            // 중심점 방법
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_CenterMethod = new System.Windows.Forms.ComboBox();

            // 수동 좌표
            this.label_ManualX = new System.Windows.Forms.Label();
            this.numericUpDown_ManualX = new System.Windows.Forms.NumericUpDown();
            this.label_ManualY = new System.Windows.Forms.Label();
            this.numericUpDown_ManualY = new System.Windows.Forms.NumericUpDown();

            // 범위 방법
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_RangeMethod = new System.Windows.Forms.ComboBox();

            // 고정 길이
            this.label_FixedLength = new System.Windows.Forms.Label();
            this.panel_FixedLength = new System.Windows.Forms.Panel();
            this.trackBar_FixedLength = new System.Windows.Forms.TrackBar();
            this.numericUpDown_FixedLength = new System.Windows.Forms.NumericUpDown();

            // 선 개수
            this.label7 = new System.Windows.Forms.Label();
            this.panel_LineCount = new System.Windows.Forms.Panel();
            this.trackBar_LineCount = new System.Windows.Forms.TrackBar();
            this.numericUpDown_LineCount = new System.Windows.Forms.NumericUpDown();

            // 시작 각도
            this.label8 = new System.Windows.Forms.Label();
            this.panel_StartAngle = new System.Windows.Forms.Panel();
            this.trackBar_StartAngle = new System.Windows.Forms.TrackBar();
            this.numericUpDown_StartAngle = new System.Windows.Forms.NumericUpDown();

            // 선 색상
            this.label9 = new System.Windows.Forms.Label();
            this.button_LineColor = new System.Windows.Forms.Button();

            // 선 두께
            this.label10 = new System.Windows.Forms.Label();
            this.panel_LineThickness = new System.Windows.Forms.Panel();
            this.trackBar_LineThickness = new System.Windows.Forms.TrackBar();
            this.numericUpDown_LineThickness = new System.Windows.Forms.NumericUpDown();

            // 선 스타일
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox_Style = new System.Windows.Forms.ComboBox();

            // 표시 옵션들
            this.label12 = new System.Windows.Forms.Label();
            this.checkBox_ShowCenter = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.checkBox_ShowAngles = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.checkBox_ShowDistances = new System.Windows.Forms.CheckBox();

            // 이진화 임계값
            this.label_BinaryThreshold = new System.Windows.Forms.Label();
            this.panel_BinaryThreshold = new System.Windows.Forms.Panel();
            this.trackBar_BinaryThreshold = new System.Windows.Forms.TrackBar();
            this.numericUpDown_BinaryThreshold = new System.Windows.Forms.NumericUpDown();

            // 밝기 임계값
            this.label_BrightnessThreshold = new System.Windows.Forms.Label();
            this.panel_BrightnessThreshold = new System.Windows.Forms.Panel();
            this.trackBar_BrightnessThreshold = new System.Windows.Forms.TrackBar();
            this.numericUpDown_BrightnessThreshold = new System.Windows.Forms.NumericUpDown();

            // 길이 데이터 출력
            this.label17 = new System.Windows.Forms.Label();
            this.checkBox_OutputLengthData = new System.Windows.Forms.CheckBox();

            this.panel_Main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_FixedLength.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_FixedLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FixedLength)).BeginInit();
            this.panel_LineCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_LineCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LineCount)).BeginInit();
            this.panel_StartAngle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_StartAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_StartAngle)).BeginInit();
            this.panel_LineThickness.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_LineThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LineThickness)).BeginInit();
            this.panel_BinaryThreshold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_BinaryThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BinaryThreshold)).BeginInit();
            this.panel_BrightnessThreshold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_BrightnessThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BrightnessThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ManualX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ManualY)).BeginInit();
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
            for (int i = 0; i < 17; i++)
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            }
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 476);
            this.tableLayoutPanel1.TabIndex = 0;

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

            // Color Button
            this.button_LineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LineColor.BackColor = System.Drawing.Color.Red;
            this.button_LineColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_LineColor.Location = new System.Drawing.Point(163, 195);
            this.button_LineColor.Name = "button_LineColor";
            this.button_LineColor.Size = new System.Drawing.Size(234, 23);
            this.button_LineColor.TabIndex = 9;
            this.button_LineColor.UseVisualStyleBackColor = false;

            // 전체 컨트롤 설정
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_Main);
            this.Name = "RadialLinesParameterControl";
            this.Size = new System.Drawing.Size(400, 600);
            this.Load += new System.EventHandler(this.RadialLinesParameterControl_Load);

            // Suspend 해제
            this.panel_Main.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel_FixedLength.ResumeLayout(false);
            this.panel_FixedLength.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_FixedLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FixedLength)).EndInit();
            this.panel_LineCount.ResumeLayout(false);
            this.panel_LineCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_LineCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LineCount)).EndInit();
            this.panel_StartAngle.ResumeLayout(false);
            this.panel_StartAngle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_StartAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_StartAngle)).EndInit();
            this.panel_LineThickness.ResumeLayout(false);
            this.panel_LineThickness.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_LineThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LineThickness)).EndInit();
            this.panel_BinaryThreshold.ResumeLayout(false);
            this.panel_BinaryThreshold.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_BinaryThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BinaryThreshold)).EndInit();
            this.panel_BrightnessThreshold.ResumeLayout(false);
            this.panel_BrightnessThreshold.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_BrightnessThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BrightnessThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ManualX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ManualY)).EndInit();
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

        private void SetupCheckBox(System.Windows.Forms.CheckBox checkBox, string text, int y, bool isChecked)
        {
            checkBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            checkBox.AutoSize = true;
            checkBox.Checked = isChecked;
            checkBox.CheckState = isChecked ? System.Windows.Forms.CheckState.Checked : System.Windows.Forms.CheckState.Unchecked;
            checkBox.Location = new System.Drawing.Point(163, y);
            checkBox.Name = checkBox.Name;
            checkBox.Size = new System.Drawing.Size(60, 16);
            checkBox.TabIndex = 1;
            checkBox.Text = text;
            checkBox.UseVisualStyleBackColor = true;
        }

        private void SetupComboBox(System.Windows.Forms.ComboBox comboBox, int y)
        {
            comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox.FormattingEnabled = true;
            comboBox.Location = new System.Drawing.Point(163, y);
            comboBox.Name = comboBox.Name;
            comboBox.Size = new System.Drawing.Size(234, 20);
            comboBox.TabIndex = 2;
        }

        private void SetupNumericUpDown(System.Windows.Forms.NumericUpDown numericUpDown, int y, int min, int max, int value)
        {
            numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            numericUpDown.Location = new System.Drawing.Point(163, y);
            numericUpDown.Maximum = new decimal(new int[] { max, 0, 0, 0 });
            numericUpDown.Minimum = new decimal(new int[] { min, 0, 0, 0 });
            numericUpDown.Name = numericUpDown.Name;
            numericUpDown.Size = new System.Drawing.Size(234, 21);
            numericUpDown.TabIndex = 3;
            numericUpDown.Value = new decimal(new int[] { value, 0, 0, 0 });
        }

        private void SetupTrackBarPanel(System.Windows.Forms.Panel panel, System.Windows.Forms.TrackBar trackBar, System.Windows.Forms.NumericUpDown numericUpDown, int y)
        {
            panel.Controls.Add(trackBar);
            panel.Controls.Add(numericUpDown);
            panel.Dock = System.Windows.Forms.DockStyle.Fill;
            panel.Location = new System.Drawing.Point(163, y);
            panel.Name = panel.Name;
            panel.Size = new System.Drawing.Size(234, 22);
            panel.TabIndex = 4;

            trackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            trackBar.Location = new System.Drawing.Point(0, 0);
            trackBar.Name = trackBar.Name;
            trackBar.Size = new System.Drawing.Size(174, 22);
            trackBar.TabIndex = 0;
            trackBar.TickStyle = System.Windows.Forms.TickStyle.None;

            numericUpDown.Dock = System.Windows.Forms.DockStyle.Right;
            numericUpDown.Location = new System.Drawing.Point(174, 0);
            numericUpDown.Name = numericUpDown.Name;
            numericUpDown.Size = new System.Drawing.Size(60, 21);
            numericUpDown.TabIndex = 1;
        }

        // 컨트롤 선언
        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_ShowVisualization;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_CenterMethod;
        private System.Windows.Forms.Label label_ManualX;
        private System.Windows.Forms.NumericUpDown numericUpDown_ManualX;
        private System.Windows.Forms.Label label_ManualY;
        private System.Windows.Forms.NumericUpDown numericUpDown_ManualY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_RangeMethod;
        private System.Windows.Forms.Label label_FixedLength;
        private System.Windows.Forms.Panel panel_FixedLength;
        private System.Windows.Forms.TrackBar trackBar_FixedLength;
        private System.Windows.Forms.NumericUpDown numericUpDown_FixedLength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel_LineCount;
        private System.Windows.Forms.TrackBar trackBar_LineCount;
        private System.Windows.Forms.NumericUpDown numericUpDown_LineCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel_StartAngle;
        private System.Windows.Forms.TrackBar trackBar_StartAngle;
        private System.Windows.Forms.NumericUpDown numericUpDown_StartAngle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button_LineColor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel_LineThickness;
        private System.Windows.Forms.TrackBar trackBar_LineThickness;
        private System.Windows.Forms.NumericUpDown numericUpDown_LineThickness;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox_Style;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkBox_ShowCenter;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox checkBox_ShowAngles;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBox_ShowDistances;
        private System.Windows.Forms.Label label_BinaryThreshold;
        private System.Windows.Forms.Panel panel_BinaryThreshold;
        private System.Windows.Forms.TrackBar trackBar_BinaryThreshold;
        private System.Windows.Forms.NumericUpDown numericUpDown_BinaryThreshold;
        private System.Windows.Forms.Label label_BrightnessThreshold;
        private System.Windows.Forms.Panel panel_BrightnessThreshold;
        private System.Windows.Forms.TrackBar trackBar_BrightnessThreshold;
        private System.Windows.Forms.NumericUpDown numericUpDown_BrightnessThreshold;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox checkBox_OutputLengthData;
    }
}