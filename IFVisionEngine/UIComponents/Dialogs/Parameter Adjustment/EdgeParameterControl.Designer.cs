namespace IFVisionEngine.UIComponents.Dialogs
{
    partial class EdgeParameterControl
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
            this.label_Method = new System.Windows.Forms.Label();
            this.comboBox_Method = new System.Windows.Forms.ComboBox();
            this.label_Threshold1 = new System.Windows.Forms.Label();
            this.panel_Threshold1 = new System.Windows.Forms.Panel();
            this.trackBar_Threshold1 = new System.Windows.Forms.TrackBar();
            this.numericUpDown_Threshold1 = new System.Windows.Forms.NumericUpDown();
            this.label_Threshold2 = new System.Windows.Forms.Label();
            this.panel_Threshold2 = new System.Windows.Forms.Panel();
            this.trackBar_Threshold2 = new System.Windows.Forms.TrackBar();
            this.numericUpDown_Threshold2 = new System.Windows.Forms.NumericUpDown();
            this.label_KernelSize = new System.Windows.Forms.Label();
            this.comboBox_KernelSize = new System.Windows.Forms.ComboBox();
            this.panel_Main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_Threshold1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Threshold1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Threshold1)).BeginInit();
            this.panel_Threshold2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Threshold2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Threshold2)).BeginInit();
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
            this.panel_Main.Size = new System.Drawing.Size(380, 200);
            this.panel_Main.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.label_Method, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_Method, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_Threshold1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_Threshold1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_Threshold2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel_Threshold2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_KernelSize, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_KernelSize, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(380, 96);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label_Method
            // 
            this.label_Method.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_Method.AutoSize = true;
            this.label_Method.Location = new System.Drawing.Point(3, 6);
            this.label_Method.Name = "label_Method";
            this.label_Method.Size = new System.Drawing.Size(61, 12);
            this.label_Method.TabIndex = 0;
            this.label_Method.Text = "검출 방법:";
            // 
            // comboBox_Method
            // 
            this.comboBox_Method.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Method.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Method.FormattingEnabled = true;
            this.comboBox_Method.Location = new System.Drawing.Point(155, 2);
            this.comboBox_Method.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_Method.Name = "comboBox_Method";
            this.comboBox_Method.Size = new System.Drawing.Size(222, 20);
            this.comboBox_Method.TabIndex = 1;
            // 
            // label_Threshold1
            // 
            this.label_Threshold1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_Threshold1.AutoSize = true;
            this.label_Threshold1.Location = new System.Drawing.Point(3, 30);
            this.label_Threshold1.Name = "label_Threshold1";
            this.label_Threshold1.Size = new System.Drawing.Size(73, 12);
            this.label_Threshold1.TabIndex = 2;
            this.label_Threshold1.Text = "하위 임계값:";
            // 
            // panel_Threshold1
            // 
            this.panel_Threshold1.Controls.Add(this.trackBar_Threshold1);
            this.panel_Threshold1.Controls.Add(this.numericUpDown_Threshold1);
            this.panel_Threshold1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Threshold1.Location = new System.Drawing.Point(155, 26);
            this.panel_Threshold1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Threshold1.Name = "panel_Threshold1";
            this.panel_Threshold1.Size = new System.Drawing.Size(222, 20);
            this.panel_Threshold1.TabIndex = 3;
            // 
            // trackBar_Threshold1
            // 
            this.trackBar_Threshold1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Threshold1.Location = new System.Drawing.Point(0, 0);
            this.trackBar_Threshold1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_Threshold1.Maximum = 500;
            this.trackBar_Threshold1.Name = "trackBar_Threshold1";
            this.trackBar_Threshold1.Size = new System.Drawing.Size(162, 20);
            this.trackBar_Threshold1.TabIndex = 0;
            this.trackBar_Threshold1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Threshold1.Value = 100;
            // 
            // numericUpDown_Threshold1
            // 
            this.numericUpDown_Threshold1.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_Threshold1.Location = new System.Drawing.Point(162, 0);
            this.numericUpDown_Threshold1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_Threshold1.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown_Threshold1.Name = "numericUpDown_Threshold1";
            this.numericUpDown_Threshold1.Size = new System.Drawing.Size(60, 21);
            this.numericUpDown_Threshold1.TabIndex = 1;
            this.numericUpDown_Threshold1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label_Threshold2
            // 
            this.label_Threshold2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_Threshold2.AutoSize = true;
            this.label_Threshold2.Location = new System.Drawing.Point(3, 54);
            this.label_Threshold2.Name = "label_Threshold2";
            this.label_Threshold2.Size = new System.Drawing.Size(73, 12);
            this.label_Threshold2.TabIndex = 4;
            this.label_Threshold2.Text = "상위 임계값:";
            // 
            // panel_Threshold2
            // 
            this.panel_Threshold2.Controls.Add(this.trackBar_Threshold2);
            this.panel_Threshold2.Controls.Add(this.numericUpDown_Threshold2);
            this.panel_Threshold2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Threshold2.Location = new System.Drawing.Point(155, 50);
            this.panel_Threshold2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Threshold2.Name = "panel_Threshold2";
            this.panel_Threshold2.Size = new System.Drawing.Size(222, 20);
            this.panel_Threshold2.TabIndex = 5;
            // 
            // trackBar_Threshold2
            // 
            this.trackBar_Threshold2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Threshold2.Location = new System.Drawing.Point(0, 0);
            this.trackBar_Threshold2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_Threshold2.Maximum = 500;
            this.trackBar_Threshold2.Name = "trackBar_Threshold2";
            this.trackBar_Threshold2.Size = new System.Drawing.Size(162, 20);
            this.trackBar_Threshold2.TabIndex = 0;
            this.trackBar_Threshold2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Threshold2.Value = 200;
            // 
            // numericUpDown_Threshold2
            // 
            this.numericUpDown_Threshold2.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_Threshold2.Location = new System.Drawing.Point(162, 0);
            this.numericUpDown_Threshold2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_Threshold2.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown_Threshold2.Name = "numericUpDown_Threshold2";
            this.numericUpDown_Threshold2.Size = new System.Drawing.Size(60, 21);
            this.numericUpDown_Threshold2.TabIndex = 1;
            this.numericUpDown_Threshold2.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label_KernelSize
            // 
            this.label_KernelSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_KernelSize.AutoSize = true;
            this.label_KernelSize.Location = new System.Drawing.Point(3, 78);
            this.label_KernelSize.Name = "label_KernelSize";
            this.label_KernelSize.Size = new System.Drawing.Size(61, 12);
            this.label_KernelSize.TabIndex = 6;
            this.label_KernelSize.Text = "커널 크기:";
            this.label_KernelSize.Visible = false;
            // 
            // comboBox_KernelSize
            // 
            this.comboBox_KernelSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_KernelSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_KernelSize.FormattingEnabled = true;
            this.comboBox_KernelSize.Location = new System.Drawing.Point(155, 74);
            this.comboBox_KernelSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_KernelSize.Name = "comboBox_KernelSize";
            this.comboBox_KernelSize.Size = new System.Drawing.Size(222, 20);
            this.comboBox_KernelSize.TabIndex = 7;
            this.comboBox_KernelSize.Visible = false;
            // 
            // EdgeParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_Main);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "EdgeParameterControl";
            this.Size = new System.Drawing.Size(380, 200);
            this.Load += new System.EventHandler(this.EdgeParameterControl_Load);
            this.panel_Main.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel_Threshold1.ResumeLayout(false);
            this.panel_Threshold1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Threshold1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Threshold1)).EndInit();
            this.panel_Threshold2.ResumeLayout(false);
            this.panel_Threshold2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Threshold2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Threshold2)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_Method;
        private System.Windows.Forms.ComboBox comboBox_Method;
        private System.Windows.Forms.Label label_Threshold1;
        private System.Windows.Forms.Panel panel_Threshold1;
        private System.Windows.Forms.TrackBar trackBar_Threshold1;
        private System.Windows.Forms.NumericUpDown numericUpDown_Threshold1;
        private System.Windows.Forms.Label label_Threshold2;
        private System.Windows.Forms.Panel panel_Threshold2;
        private System.Windows.Forms.TrackBar trackBar_Threshold2;
        private System.Windows.Forms.NumericUpDown numericUpDown_Threshold2;
        private System.Windows.Forms.Label label_KernelSize;
        private System.Windows.Forms.ComboBox comboBox_KernelSize;
    }
}