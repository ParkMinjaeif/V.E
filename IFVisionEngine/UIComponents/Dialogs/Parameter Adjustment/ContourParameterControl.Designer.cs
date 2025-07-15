using Sunny.UI;

namespace IFVisionEngine.UIComponents.Dialogs
{
    partial class ContourParameterControl
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
            this.comboBox_RetrievalMode = new Sunny.UI.UIComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_ApproximationMethod = new Sunny.UI.UIComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_MinArea = new System.Windows.Forms.Panel();
            this.trackBar_MinArea = new Sunny.UI.UITrackBar();
            this.numericUpDown_MinArea = new Sunny.UI.UIIntegerUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panel_MaxArea = new System.Windows.Forms.Panel();
            this.trackBar_MaxArea = new Sunny.UI.UITrackBar();
            this.numericUpDown_MaxArea = new Sunny.UI.UIIntegerUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox_DrawOnOriginal = new Sunny.UI.UICheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel_Thickness = new System.Windows.Forms.Panel();
            this.trackBar_Thickness = new Sunny.UI.UITrackBar();
            this.numericUpDown_Thickness = new Sunny.UI.UIIntegerUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_ColorMode = new Sunny.UI.UIComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button_FixedColor = new Sunny.UI.UIButton();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox_ShowNumbers = new Sunny.UI.UICheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBox_ShowVisualization = new Sunny.UI.UICheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBox_OutputData = new Sunny.UI.UICheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBox_OutputAsJson = new Sunny.UI.UICheckBox();
            this.panel_Main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_MinArea.SuspendLayout();
            this.panel_MaxArea.SuspendLayout();
            this.panel_Thickness.SuspendLayout();
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
            this.panel_Main.Size = new System.Drawing.Size(400, 240);
            this.panel_Main.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_RetrievalMode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_ApproximationMethod, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel_MinArea, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel_MaxArea, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_DrawOnOriginal, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel_Thickness, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_ColorMode, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.button_FixedColor, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_ShowNumbers, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_ShowVisualization, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_OutputData, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_OutputAsJson, 1, 11);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(383, 288);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "검출 모드:";
            // 
            // comboBox_RetrievalMode
            // 
            this.comboBox_RetrievalMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_RetrievalMode.DataSource = null;
            this.comboBox_RetrievalMode.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.comboBox_RetrievalMode.FillColor = System.Drawing.Color.White;
            this.comboBox_RetrievalMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.comboBox_RetrievalMode.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.comboBox_RetrievalMode.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.comboBox_RetrievalMode.Location = new System.Drawing.Point(79, 2);
            this.comboBox_RetrievalMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_RetrievalMode.MinimumSize = new System.Drawing.Size(63, 0);
            this.comboBox_RetrievalMode.Name = "comboBox_RetrievalMode";
            this.comboBox_RetrievalMode.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.comboBox_RetrievalMode.Size = new System.Drawing.Size(301, 20);
            this.comboBox_RetrievalMode.SymbolSize = 24;
            this.comboBox_RetrievalMode.TabIndex = 1;
            this.comboBox_RetrievalMode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBox_RetrievalMode.Watermark = "";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "근사화 방법:";
            // 
            // comboBox_ApproximationMethod
            // 
            this.comboBox_ApproximationMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_ApproximationMethod.DataSource = null;
            this.comboBox_ApproximationMethod.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.comboBox_ApproximationMethod.FillColor = System.Drawing.Color.White;
            this.comboBox_ApproximationMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.comboBox_ApproximationMethod.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.comboBox_ApproximationMethod.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.comboBox_ApproximationMethod.Location = new System.Drawing.Point(79, 26);
            this.comboBox_ApproximationMethod.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_ApproximationMethod.MinimumSize = new System.Drawing.Size(63, 0);
            this.comboBox_ApproximationMethod.Name = "comboBox_ApproximationMethod";
            this.comboBox_ApproximationMethod.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.comboBox_ApproximationMethod.Size = new System.Drawing.Size(301, 20);
            this.comboBox_ApproximationMethod.SymbolSize = 24;
            this.comboBox_ApproximationMethod.TabIndex = 3;
            this.comboBox_ApproximationMethod.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBox_ApproximationMethod.Watermark = "";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "최소 면적:";
            // 
            // panel_MinArea
            // 
            this.panel_MinArea.Controls.Add(this.trackBar_MinArea);
            this.panel_MinArea.Controls.Add(this.numericUpDown_MinArea);
            this.panel_MinArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_MinArea.Location = new System.Drawing.Point(79, 50);
            this.panel_MinArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_MinArea.Name = "panel_MinArea";
            this.panel_MinArea.Size = new System.Drawing.Size(301, 20);
            this.panel_MinArea.TabIndex = 5;
            // 
            // trackBar_MinArea
            // 
            this.trackBar_MinArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_MinArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.trackBar_MinArea.Location = new System.Drawing.Point(0, 0);
            this.trackBar_MinArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_MinArea.Maximum = 10000;
            this.trackBar_MinArea.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_MinArea.Name = "trackBar_MinArea";
            this.trackBar_MinArea.Size = new System.Drawing.Size(160, 20);
            this.trackBar_MinArea.TabIndex = 0;
            this.trackBar_MinArea.Text = "uiTrackBar1";
            this.trackBar_MinArea.Value = 100;
            // 
            // numericUpDown_MinArea
            // 
            this.numericUpDown_MinArea.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_MinArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numericUpDown_MinArea.Location = new System.Drawing.Point(160, 0);
            this.numericUpDown_MinArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_MinArea.Maximum = 10000;
            this.numericUpDown_MinArea.Minimum = 0;
            this.numericUpDown_MinArea.MinimumSize = new System.Drawing.Size(100, 0);
            this.numericUpDown_MinArea.Name = "numericUpDown_MinArea";
            this.numericUpDown_MinArea.ShowText = false;
            this.numericUpDown_MinArea.Size = new System.Drawing.Size(141, 20);
            this.numericUpDown_MinArea.TabIndex = 1;
            this.numericUpDown_MinArea.Text = "uiIntegerUpDown1";
            this.numericUpDown_MinArea.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.numericUpDown_MinArea.Value = 100;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "최대 면적:";
            // 
            // panel_MaxArea
            // 
            this.panel_MaxArea.Controls.Add(this.trackBar_MaxArea);
            this.panel_MaxArea.Controls.Add(this.numericUpDown_MaxArea);
            this.panel_MaxArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_MaxArea.Location = new System.Drawing.Point(79, 74);
            this.panel_MaxArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_MaxArea.Name = "panel_MaxArea";
            this.panel_MaxArea.Size = new System.Drawing.Size(301, 20);
            this.panel_MaxArea.TabIndex = 7;
            // 
            // trackBar_MaxArea
            // 
            this.trackBar_MaxArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_MaxArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.trackBar_MaxArea.Location = new System.Drawing.Point(0, 0);
            this.trackBar_MaxArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_MaxArea.Maximum = 10000;
            this.trackBar_MaxArea.Minimum = 1;
            this.trackBar_MaxArea.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_MaxArea.Name = "trackBar_MaxArea";
            this.trackBar_MaxArea.Size = new System.Drawing.Size(160, 20);
            this.trackBar_MaxArea.TabIndex = 0;
            this.trackBar_MaxArea.Text = "uiTrackBar2";
            this.trackBar_MaxArea.Value = 100;
            // 
            // numericUpDown_MaxArea
            // 
            this.numericUpDown_MaxArea.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_MaxArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numericUpDown_MaxArea.Location = new System.Drawing.Point(160, 0);
            this.numericUpDown_MaxArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_MaxArea.Maximum = 10000000;
            this.numericUpDown_MaxArea.Minimum = 1000;
            this.numericUpDown_MaxArea.MinimumSize = new System.Drawing.Size(100, 0);
            this.numericUpDown_MaxArea.Name = "numericUpDown_MaxArea";
            this.numericUpDown_MaxArea.ShowText = false;
            this.numericUpDown_MaxArea.Size = new System.Drawing.Size(141, 20);
            this.numericUpDown_MaxArea.TabIndex = 1;
            this.numericUpDown_MaxArea.Text = "uiIntegerUpDown2";
            this.numericUpDown_MaxArea.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.numericUpDown_MaxArea.Value = 100000;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 24);
            this.label5.TabIndex = 8;
            this.label5.Text = "원본에 그리기:";
            // 
            // checkBox_DrawOnOriginal
            // 
            this.checkBox_DrawOnOriginal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox_DrawOnOriginal.Checked = true;
            this.checkBox_DrawOnOriginal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_DrawOnOriginal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBox_DrawOnOriginal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_DrawOnOriginal.Location = new System.Drawing.Point(79, 100);
            this.checkBox_DrawOnOriginal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_DrawOnOriginal.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_DrawOnOriginal.Name = "checkBox_DrawOnOriginal";
            this.checkBox_DrawOnOriginal.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.checkBox_DrawOnOriginal.Size = new System.Drawing.Size(72, 16);
            this.checkBox_DrawOnOriginal.TabIndex = 9;
            this.checkBox_DrawOnOriginal.Text = "활성화";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "선 두께:";
            // 
            // panel_Thickness
            // 
            this.panel_Thickness.Controls.Add(this.trackBar_Thickness);
            this.panel_Thickness.Controls.Add(this.numericUpDown_Thickness);
            this.panel_Thickness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Thickness.Location = new System.Drawing.Point(79, 122);
            this.panel_Thickness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Thickness.Name = "panel_Thickness";
            this.panel_Thickness.Size = new System.Drawing.Size(301, 20);
            this.panel_Thickness.TabIndex = 11;
            // 
            // trackBar_Thickness
            // 
            this.trackBar_Thickness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Thickness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.trackBar_Thickness.Location = new System.Drawing.Point(0, 0);
            this.trackBar_Thickness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_Thickness.Maximum = 10;
            this.trackBar_Thickness.Minimum = 1;
            this.trackBar_Thickness.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_Thickness.Name = "trackBar_Thickness";
            this.trackBar_Thickness.Size = new System.Drawing.Size(160, 20);
            this.trackBar_Thickness.TabIndex = 0;
            this.trackBar_Thickness.Text = "uiTrackBar3";
            this.trackBar_Thickness.Value = 2;
            // 
            // numericUpDown_Thickness
            // 
            this.numericUpDown_Thickness.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_Thickness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numericUpDown_Thickness.Location = new System.Drawing.Point(160, 0);
            this.numericUpDown_Thickness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_Thickness.Maximum = 10;
            this.numericUpDown_Thickness.Minimum = 1;
            this.numericUpDown_Thickness.MinimumSize = new System.Drawing.Size(100, 0);
            this.numericUpDown_Thickness.Name = "numericUpDown_Thickness";
            this.numericUpDown_Thickness.ShowText = false;
            this.numericUpDown_Thickness.Size = new System.Drawing.Size(141, 20);
            this.numericUpDown_Thickness.TabIndex = 1;
            this.numericUpDown_Thickness.Text = "uiIntegerUpDown3";
            this.numericUpDown_Thickness.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.numericUpDown_Thickness.Value = 2;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "색상 모드:";
            // 
            // comboBox_ColorMode
            // 
            this.comboBox_ColorMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_ColorMode.DataSource = null;
            this.comboBox_ColorMode.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.comboBox_ColorMode.FillColor = System.Drawing.Color.White;
            this.comboBox_ColorMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.comboBox_ColorMode.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.comboBox_ColorMode.Items.AddRange(new object[] {
            ""});
            this.comboBox_ColorMode.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.comboBox_ColorMode.Location = new System.Drawing.Point(79, 146);
            this.comboBox_ColorMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_ColorMode.MinimumSize = new System.Drawing.Size(63, 0);
            this.comboBox_ColorMode.Name = "comboBox_ColorMode";
            this.comboBox_ColorMode.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.comboBox_ColorMode.Size = new System.Drawing.Size(301, 20);
            this.comboBox_ColorMode.SymbolSize = 24;
            this.comboBox_ColorMode.TabIndex = 13;
            this.comboBox_ColorMode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBox_ColorMode.Watermark = "";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "고정 색상:";
            // 
            // button_FixedColor
            // 
            this.button_FixedColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button_FixedColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_FixedColor.FillColor = System.Drawing.Color.Green;
            this.button_FixedColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button_FixedColor.Location = new System.Drawing.Point(79, 171);
            this.button_FixedColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_FixedColor.MinimumSize = new System.Drawing.Size(1, 1);
            this.button_FixedColor.Name = "button_FixedColor";
            this.button_FixedColor.RectColor = System.Drawing.Color.Green;
            this.button_FixedColor.Size = new System.Drawing.Size(301, 18);
            this.button_FixedColor.TabIndex = 15;
            this.button_FixedColor.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 198);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "번호 표시:";
            // 
            // checkBox_ShowNumbers
            // 
            this.checkBox_ShowNumbers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox_ShowNumbers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_ShowNumbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBox_ShowNumbers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_ShowNumbers.Location = new System.Drawing.Point(79, 196);
            this.checkBox_ShowNumbers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_ShowNumbers.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_ShowNumbers.Name = "checkBox_ShowNumbers";
            this.checkBox_ShowNumbers.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.checkBox_ShowNumbers.Size = new System.Drawing.Size(72, 16);
            this.checkBox_ShowNumbers.TabIndex = 17;
            this.checkBox_ShowNumbers.Text = "활성화";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 216);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 24);
            this.label10.TabIndex = 18;
            this.label10.Text = "시각화 표시:";
            // 
            // checkBox_ShowVisualization
            // 
            this.checkBox_ShowVisualization.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox_ShowVisualization.Checked = true;
            this.checkBox_ShowVisualization.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_ShowVisualization.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBox_ShowVisualization.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_ShowVisualization.Location = new System.Drawing.Point(79, 220);
            this.checkBox_ShowVisualization.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_ShowVisualization.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_ShowVisualization.Name = "checkBox_ShowVisualization";
            this.checkBox_ShowVisualization.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.checkBox_ShowVisualization.Size = new System.Drawing.Size(72, 16);
            this.checkBox_ShowVisualization.TabIndex = 19;
            this.checkBox_ShowVisualization.Text = "활성화";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 240);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 24);
            this.label11.TabIndex = 20;
            this.label11.Text = "데이터 출력:";
            // 
            // checkBox_OutputData
            // 
            this.checkBox_OutputData.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox_OutputData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_OutputData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBox_OutputData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_OutputData.Location = new System.Drawing.Point(79, 244);
            this.checkBox_OutputData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_OutputData.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_OutputData.Name = "checkBox_OutputData";
            this.checkBox_OutputData.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.checkBox_OutputData.Size = new System.Drawing.Size(72, 16);
            this.checkBox_OutputData.TabIndex = 21;
            this.checkBox_OutputData.Text = "활성화";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 270);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 12);
            this.label12.TabIndex = 22;
            this.label12.Text = "JSON 형태:";
            // 
            // checkBox_OutputAsJson
            // 
            this.checkBox_OutputAsJson.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox_OutputAsJson.Checked = true;
            this.checkBox_OutputAsJson.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_OutputAsJson.Enabled = false;
            this.checkBox_OutputAsJson.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBox_OutputAsJson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.checkBox_OutputAsJson.Location = new System.Drawing.Point(79, 268);
            this.checkBox_OutputAsJson.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_OutputAsJson.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBox_OutputAsJson.Name = "checkBox_OutputAsJson";
            this.checkBox_OutputAsJson.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.checkBox_OutputAsJson.Size = new System.Drawing.Size(72, 16);
            this.checkBox_OutputAsJson.TabIndex = 23;
            this.checkBox_OutputAsJson.Text = "활성화";
            // 
            // ContourParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_Main);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ContourParameterControl";
            this.Size = new System.Drawing.Size(400, 240);
            this.Load += new System.EventHandler(this.ContourParameterControl_Load);
            this.panel_Main.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel_MinArea.ResumeLayout(false);
            this.panel_MaxArea.ResumeLayout(false);
            this.panel_Thickness.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UIComboBox comboBox_RetrievalMode;
        private System.Windows.Forms.Label label2;
        private Sunny.UI.UIComboBox comboBox_ApproximationMethod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_MinArea;
        private Sunny.UI.UITrackBar trackBar_MinArea;
        private Sunny.UI.UIIntegerUpDown numericUpDown_MinArea;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel_MaxArea;
        private Sunny.UI.UITrackBar trackBar_MaxArea;
        private Sunny.UI.UIIntegerUpDown numericUpDown_MaxArea;
        private System.Windows.Forms.Label label5;
        private Sunny.UI.UICheckBox checkBox_DrawOnOriginal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel_Thickness;
        private Sunny.UI.UITrackBar trackBar_Thickness;
        private Sunny.UI.UIIntegerUpDown numericUpDown_Thickness;
        private System.Windows.Forms.Label label7;
        private Sunny.UI.UIComboBox comboBox_ColorMode;
        private System.Windows.Forms.Label label8;
        private Sunny.UI.UIButton button_FixedColor;
        private System.Windows.Forms.Label label9;
        private Sunny.UI.UICheckBox checkBox_ShowNumbers;
        private System.Windows.Forms.Label label10;
        private Sunny.UI.UICheckBox checkBox_ShowVisualization;
        private System.Windows.Forms.Label label11;
        private Sunny.UI.UICheckBox checkBox_OutputData;
        private System.Windows.Forms.Label label12;
        private Sunny.UI.UICheckBox checkBox_OutputAsJson;
    }
}