namespace IFVisionEngine.UIComponents.Dialogs
{
    partial class MomentsParameterControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_Main = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_Threshold = new System.Windows.Forms.Panel();
            this.trackBar_Threshold = new Sunny.UI.UITrackBar();
            this.numericUpDown_Threshold = new Sunny.UI.UIIntegerUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_ShowCentroid = new Sunny.UI.UICheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_ShowArea = new Sunny.UI.UICheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_ShowOrientation = new Sunny.UI.UICheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox_ShowBoundingBox = new Sunny.UI.UICheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox_ShowEccentricity = new Sunny.UI.UICheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button_DrawColor = new Sunny.UI.UIButton();
            this.label8 = new System.Windows.Forms.Label();
            this.panel_LineThickness = new System.Windows.Forms.Panel();
            this.trackBar_LineThickness = new Sunny.UI.UITrackBar();
            this.numericUpDown_LineThickness = new Sunny.UI.UIIntegerUpDown();
            this.panel_Main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_Threshold.SuspendLayout();
            this.panel_LineThickness.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Main
            // 
            this.panel_Main.AutoScroll = true;
            this.panel_Main.Controls.Add(this.tableLayoutPanel1);
            this.panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Main.Location = new System.Drawing.Point(0, 0);
            this.panel_Main.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Size = new System.Drawing.Size(400, 192);
            this.panel_Main.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel_Threshold, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_ShowCentroid, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_ShowArea, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_ShowOrientation, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_ShowBoundingBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_ShowEccentricity, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.button_DrawColor, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.panel_LineThickness, 1, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 192);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "이진화 임계값:";
            // 
            // panel_Threshold
            // 
            this.panel_Threshold.Controls.Add(this.trackBar_Threshold);
            this.panel_Threshold.Controls.Add(this.numericUpDown_Threshold);
            this.panel_Threshold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Threshold.Location = new System.Drawing.Point(83, 2);
            this.panel_Threshold.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Threshold.Name = "panel_Threshold";
            this.panel_Threshold.Size = new System.Drawing.Size(314, 20);
            this.panel_Threshold.TabIndex = 1;
            // 
            // trackBar_Threshold
            // 
            this.trackBar_Threshold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Threshold.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.trackBar_Threshold.Location = new System.Drawing.Point(0, 0);
            this.trackBar_Threshold.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_Threshold.Maximum = 255;
            this.trackBar_Threshold.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_Threshold.Name = "trackBar_Threshold";
            this.trackBar_Threshold.Size = new System.Drawing.Size(213, 20);
            this.trackBar_Threshold.TabIndex = 0;
            this.trackBar_Threshold.Text = "uiTrackBar1";
            this.trackBar_Threshold.Value = 127;
            // 
            // numericUpDown_Threshold
            // 
            this.numericUpDown_Threshold.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_Threshold.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numericUpDown_Threshold.Location = new System.Drawing.Point(213, 0);
            this.numericUpDown_Threshold.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_Threshold.Maximum = 255;
            this.numericUpDown_Threshold.Minimum = 0;
            this.numericUpDown_Threshold.MinimumSize = new System.Drawing.Size(1, 1);
            this.numericUpDown_Threshold.Name = "numericUpDown_Threshold";
            this.numericUpDown_Threshold.ShowText = false;
            this.numericUpDown_Threshold.Size = new System.Drawing.Size(101, 20);
            this.numericUpDown_Threshold.TabIndex = 1;
            this.numericUpDown_Threshold.Text = "uiIntegerUpDown1";
            this.numericUpDown_Threshold.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.numericUpDown_Threshold.Value = 127;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "중심점 표시:";
            // 
            // checkBox_ShowCentroid
            // 
            this.checkBox_ShowCentroid.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox_ShowCentroid.Checked = true;
            this.checkBox_ShowCentroid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_ShowCentroid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBox_ShowCentroid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_ShowCentroid.Location = new System.Drawing.Point(83, 28);
            this.checkBox_ShowCentroid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_ShowCentroid.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_ShowCentroid.Name = "checkBox_ShowCentroid";
            this.checkBox_ShowCentroid.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.checkBox_ShowCentroid.Size = new System.Drawing.Size(73, 16);
            this.checkBox_ShowCentroid.TabIndex = 3;
            this.checkBox_ShowCentroid.Text = "활성화";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "면적 표시:";
            // 
            // checkBox_ShowArea
            // 
            this.checkBox_ShowArea.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox_ShowArea.Checked = true;
            this.checkBox_ShowArea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_ShowArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBox_ShowArea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_ShowArea.Location = new System.Drawing.Point(83, 52);
            this.checkBox_ShowArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_ShowArea.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_ShowArea.Name = "checkBox_ShowArea";
            this.checkBox_ShowArea.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.checkBox_ShowArea.Size = new System.Drawing.Size(73, 16);
            this.checkBox_ShowArea.TabIndex = 5;
            this.checkBox_ShowArea.Text = "활성화";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "방향각 표시:";
            // 
            // checkBox_ShowOrientation
            // 
            this.checkBox_ShowOrientation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox_ShowOrientation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_ShowOrientation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBox_ShowOrientation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_ShowOrientation.Location = new System.Drawing.Point(83, 76);
            this.checkBox_ShowOrientation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_ShowOrientation.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_ShowOrientation.Name = "checkBox_ShowOrientation";
            this.checkBox_ShowOrientation.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.checkBox_ShowOrientation.Size = new System.Drawing.Size(73, 16);
            this.checkBox_ShowOrientation.TabIndex = 7;
            this.checkBox_ShowOrientation.Text = "활성화";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 24);
            this.label5.TabIndex = 8;
            this.label5.Text = "경계박스 표시:";
            // 
            // checkBox_ShowBoundingBox
            // 
            this.checkBox_ShowBoundingBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox_ShowBoundingBox.Checked = true;
            this.checkBox_ShowBoundingBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_ShowBoundingBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBox_ShowBoundingBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_ShowBoundingBox.Location = new System.Drawing.Point(83, 100);
            this.checkBox_ShowBoundingBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_ShowBoundingBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_ShowBoundingBox.Name = "checkBox_ShowBoundingBox";
            this.checkBox_ShowBoundingBox.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.checkBox_ShowBoundingBox.Size = new System.Drawing.Size(73, 16);
            this.checkBox_ShowBoundingBox.TabIndex = 9;
            this.checkBox_ShowBoundingBox.Text = "활성화";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "편심률 표시:";
            // 
            // checkBox_ShowEccentricity
            // 
            this.checkBox_ShowEccentricity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox_ShowEccentricity.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_ShowEccentricity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBox_ShowEccentricity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_ShowEccentricity.Location = new System.Drawing.Point(83, 124);
            this.checkBox_ShowEccentricity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_ShowEccentricity.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_ShowEccentricity.Name = "checkBox_ShowEccentricity";
            this.checkBox_ShowEccentricity.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.checkBox_ShowEccentricity.Size = new System.Drawing.Size(73, 16);
            this.checkBox_ShowEccentricity.TabIndex = 11;
            this.checkBox_ShowEccentricity.Text = "활성화";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "그리기 색상:";
            // 
            // button_DrawColor
            // 
            this.button_DrawColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button_DrawColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_DrawColor.FillColor = System.Drawing.Color.Red;
            this.button_DrawColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button_DrawColor.Location = new System.Drawing.Point(83, 147);
            this.button_DrawColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_DrawColor.MinimumSize = new System.Drawing.Size(1, 1);
            this.button_DrawColor.Name = "button_DrawColor";
            this.button_DrawColor.RectColor = System.Drawing.Color.Red;
            this.button_DrawColor.Size = new System.Drawing.Size(314, 18);
            this.button_DrawColor.TabIndex = 13;
            this.button_DrawColor.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "선 두께:";
            // 
            // panel_LineThickness
            // 
            this.panel_LineThickness.Controls.Add(this.trackBar_LineThickness);
            this.panel_LineThickness.Controls.Add(this.numericUpDown_LineThickness);
            this.panel_LineThickness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_LineThickness.Location = new System.Drawing.Point(83, 170);
            this.panel_LineThickness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_LineThickness.Name = "panel_LineThickness";
            this.panel_LineThickness.Size = new System.Drawing.Size(314, 20);
            this.panel_LineThickness.TabIndex = 15;
            // 
            // trackBar_LineThickness
            // 
            this.trackBar_LineThickness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_LineThickness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.trackBar_LineThickness.Location = new System.Drawing.Point(0, 0);
            this.trackBar_LineThickness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_LineThickness.Maximum = 10;
            this.trackBar_LineThickness.Minimum = 1;
            this.trackBar_LineThickness.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_LineThickness.Name = "trackBar_LineThickness";
            this.trackBar_LineThickness.Size = new System.Drawing.Size(213, 20);
            this.trackBar_LineThickness.TabIndex = 0;
            this.trackBar_LineThickness.Text = "uiTrackBar2";
            this.trackBar_LineThickness.Value = 2;
            // 
            // numericUpDown_LineThickness
            // 
            this.numericUpDown_LineThickness.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_LineThickness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numericUpDown_LineThickness.Location = new System.Drawing.Point(213, 0);
            this.numericUpDown_LineThickness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_LineThickness.Maximum = 10;
            this.numericUpDown_LineThickness.Minimum = 1;
            this.numericUpDown_LineThickness.MinimumSize = new System.Drawing.Size(1, 1);
            this.numericUpDown_LineThickness.Name = "numericUpDown_LineThickness";
            this.numericUpDown_LineThickness.ShowText = false;
            this.numericUpDown_LineThickness.Size = new System.Drawing.Size(101, 20);
            this.numericUpDown_LineThickness.TabIndex = 1;
            this.numericUpDown_LineThickness.Text = "uiIntegerUpDown2";
            this.numericUpDown_LineThickness.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.numericUpDown_LineThickness.Value = 2;
            // 
            // MomentsParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_Main);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MomentsParameterControl";
            this.Size = new System.Drawing.Size(400, 192);
            this.Load += new System.EventHandler(this.MomentsParameterControl_Load);
            this.panel_Main.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel_Threshold.ResumeLayout(false);
            this.panel_LineThickness.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_Threshold;
        private Sunny.UI.UITrackBar trackBar_Threshold;
        private Sunny.UI.UIIntegerUpDown numericUpDown_Threshold;
        private System.Windows.Forms.Label label2;
        private Sunny.UI.UICheckBox checkBox_ShowCentroid;
        private System.Windows.Forms.Label label3;
        private Sunny.UI.UICheckBox checkBox_ShowArea;
        private System.Windows.Forms.Label label4;
        private Sunny.UI.UICheckBox checkBox_ShowOrientation;
        private System.Windows.Forms.Label label5;
        private Sunny.UI.UICheckBox checkBox_ShowBoundingBox;
        private System.Windows.Forms.Label label6;
        private Sunny.UI.UICheckBox checkBox_ShowEccentricity;
        private System.Windows.Forms.Label label7;
        private Sunny.UI.UIButton button_DrawColor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel_LineThickness;
        private Sunny.UI.UITrackBar trackBar_LineThickness;
        private Sunny.UI.UIIntegerUpDown numericUpDown_LineThickness;
    }
}