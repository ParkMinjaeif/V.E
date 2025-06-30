namespace IFVisionEngine.UIComponents.Dialogs
{
    partial class GaussianBlurParameterControl
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
            this.panel_KernelWidth = new System.Windows.Forms.Panel();
            this.trackBar_KernelWidth = new System.Windows.Forms.TrackBar();
            this.numericUpDown_KernelWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_KernelHeight = new System.Windows.Forms.Panel();
            this.trackBar_KernelHeight = new System.Windows.Forms.TrackBar();
            this.numericUpDown_KernelHeight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_SigmaX = new System.Windows.Forms.Panel();
            this.trackBar_SigmaX = new System.Windows.Forms.TrackBar();
            this.numericUpDown_SigmaX = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panel_SigmaY = new System.Windows.Forms.Panel();
            this.trackBar_SigmaY = new System.Windows.Forms.TrackBar();
            this.numericUpDown_SigmaY = new System.Windows.Forms.NumericUpDown();
            this.panel_Main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_KernelWidth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_KernelWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_KernelWidth)).BeginInit();
            this.panel_KernelHeight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_KernelHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_KernelHeight)).BeginInit();
            this.panel_SigmaX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_SigmaX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SigmaX)).BeginInit();
            this.panel_SigmaY.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_SigmaY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SigmaY)).BeginInit();
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
            this.panel_Main.Size = new System.Drawing.Size(380, 160);
            this.panel_Main.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel_KernelWidth, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_KernelHeight, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel_SigmaX, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel_SigmaY, 1, 3);
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
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "커널 너비:";
            // 
            // panel_KernelWidth
            // 
            this.panel_KernelWidth.Controls.Add(this.trackBar_KernelWidth);
            this.panel_KernelWidth.Controls.Add(this.numericUpDown_KernelWidth);
            this.panel_KernelWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_KernelWidth.Location = new System.Drawing.Point(155, 2);
            this.panel_KernelWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_KernelWidth.Name = "panel_KernelWidth";
            this.panel_KernelWidth.Size = new System.Drawing.Size(222, 20);
            this.panel_KernelWidth.TabIndex = 1;
            // 
            // trackBar_KernelWidth
            // 
            this.trackBar_KernelWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_KernelWidth.Location = new System.Drawing.Point(0, 0);
            this.trackBar_KernelWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_KernelWidth.Maximum = 51;
            this.trackBar_KernelWidth.Minimum = 1;
            this.trackBar_KernelWidth.Name = "trackBar_KernelWidth";
            this.trackBar_KernelWidth.Size = new System.Drawing.Size(162, 20);
            this.trackBar_KernelWidth.TabIndex = 0;
            this.trackBar_KernelWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_KernelWidth.Value = 5;
            // 
            // numericUpDown_KernelWidth
            // 
            this.numericUpDown_KernelWidth.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_KernelWidth.Location = new System.Drawing.Point(162, 0);
            this.numericUpDown_KernelWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_KernelWidth.Maximum = new decimal(new int[] {
            51,
            0,
            0,
            0});
            this.numericUpDown_KernelWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_KernelWidth.Name = "numericUpDown_KernelWidth";
            this.numericUpDown_KernelWidth.Size = new System.Drawing.Size(60, 21);
            this.numericUpDown_KernelWidth.TabIndex = 1;
            this.numericUpDown_KernelWidth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "커널 높이:";
            // 
            // panel_KernelHeight
            // 
            this.panel_KernelHeight.Controls.Add(this.trackBar_KernelHeight);
            this.panel_KernelHeight.Controls.Add(this.numericUpDown_KernelHeight);
            this.panel_KernelHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_KernelHeight.Location = new System.Drawing.Point(155, 26);
            this.panel_KernelHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_KernelHeight.Name = "panel_KernelHeight";
            this.panel_KernelHeight.Size = new System.Drawing.Size(222, 20);
            this.panel_KernelHeight.TabIndex = 3;
            // 
            // trackBar_KernelHeight
            // 
            this.trackBar_KernelHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_KernelHeight.Location = new System.Drawing.Point(0, 0);
            this.trackBar_KernelHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_KernelHeight.Maximum = 51;
            this.trackBar_KernelHeight.Minimum = 1;
            this.trackBar_KernelHeight.Name = "trackBar_KernelHeight";
            this.trackBar_KernelHeight.Size = new System.Drawing.Size(162, 20);
            this.trackBar_KernelHeight.TabIndex = 0;
            this.trackBar_KernelHeight.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_KernelHeight.Value = 5;
            // 
            // numericUpDown_KernelHeight
            // 
            this.numericUpDown_KernelHeight.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_KernelHeight.Location = new System.Drawing.Point(162, 0);
            this.numericUpDown_KernelHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_KernelHeight.Maximum = new decimal(new int[] {
            51,
            0,
            0,
            0});
            this.numericUpDown_KernelHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_KernelHeight.Name = "numericUpDown_KernelHeight";
            this.numericUpDown_KernelHeight.Size = new System.Drawing.Size(60, 21);
            this.numericUpDown_KernelHeight.TabIndex = 1;
            this.numericUpDown_KernelHeight.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "SigmaX:";
            // 
            // panel_SigmaX
            // 
            this.panel_SigmaX.Controls.Add(this.trackBar_SigmaX);
            this.panel_SigmaX.Controls.Add(this.numericUpDown_SigmaX);
            this.panel_SigmaX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_SigmaX.Location = new System.Drawing.Point(155, 50);
            this.panel_SigmaX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_SigmaX.Name = "panel_SigmaX";
            this.panel_SigmaX.Size = new System.Drawing.Size(222, 20);
            this.panel_SigmaX.TabIndex = 5;
            // 
            // trackBar_SigmaX
            // 
            this.trackBar_SigmaX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_SigmaX.Location = new System.Drawing.Point(0, 0);
            this.trackBar_SigmaX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_SigmaX.Maximum = 100;
            this.trackBar_SigmaX.Name = "trackBar_SigmaX";
            this.trackBar_SigmaX.Size = new System.Drawing.Size(162, 20);
            this.trackBar_SigmaX.TabIndex = 0;
            this.trackBar_SigmaX.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // numericUpDown_SigmaX
            // 
            this.numericUpDown_SigmaX.DecimalPlaces = 1;
            this.numericUpDown_SigmaX.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_SigmaX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_SigmaX.Location = new System.Drawing.Point(162, 0);
            this.numericUpDown_SigmaX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_SigmaX.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_SigmaX.Name = "numericUpDown_SigmaX";
            this.numericUpDown_SigmaX.Size = new System.Drawing.Size(60, 21);
            this.numericUpDown_SigmaX.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "SigmaY:";
            // 
            // panel_SigmaY
            // 
            this.panel_SigmaY.Controls.Add(this.trackBar_SigmaY);
            this.panel_SigmaY.Controls.Add(this.numericUpDown_SigmaY);
            this.panel_SigmaY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_SigmaY.Location = new System.Drawing.Point(155, 74);
            this.panel_SigmaY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_SigmaY.Name = "panel_SigmaY";
            this.panel_SigmaY.Size = new System.Drawing.Size(222, 20);
            this.panel_SigmaY.TabIndex = 7;
            // 
            // trackBar_SigmaY
            // 
            this.trackBar_SigmaY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_SigmaY.Location = new System.Drawing.Point(0, 0);
            this.trackBar_SigmaY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_SigmaY.Maximum = 100;
            this.trackBar_SigmaY.Name = "trackBar_SigmaY";
            this.trackBar_SigmaY.Size = new System.Drawing.Size(162, 20);
            this.trackBar_SigmaY.TabIndex = 0;
            this.trackBar_SigmaY.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // numericUpDown_SigmaY
            // 
            this.numericUpDown_SigmaY.DecimalPlaces = 1;
            this.numericUpDown_SigmaY.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_SigmaY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_SigmaY.Location = new System.Drawing.Point(162, 0);
            this.numericUpDown_SigmaY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_SigmaY.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_SigmaY.Name = "numericUpDown_SigmaY";
            this.numericUpDown_SigmaY.Size = new System.Drawing.Size(60, 21);
            this.numericUpDown_SigmaY.TabIndex = 1;
            // 
            // GaussianBlurParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_Main);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "GaussianBlurParameterControl";
            this.Size = new System.Drawing.Size(380, 160);
            this.Load += new System.EventHandler(this.GaussianBlurParameterControl_Load);
            this.panel_Main.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel_KernelWidth.ResumeLayout(false);
            this.panel_KernelWidth.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_KernelWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_KernelWidth)).EndInit();
            this.panel_KernelHeight.ResumeLayout(false);
            this.panel_KernelHeight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_KernelHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_KernelHeight)).EndInit();
            this.panel_SigmaX.ResumeLayout(false);
            this.panel_SigmaX.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_SigmaX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SigmaX)).EndInit();
            this.panel_SigmaY.ResumeLayout(false);
            this.panel_SigmaY.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_SigmaY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SigmaY)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_KernelWidth;
        private System.Windows.Forms.TrackBar trackBar_KernelWidth;
        private System.Windows.Forms.NumericUpDown numericUpDown_KernelWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_KernelHeight;
        private System.Windows.Forms.TrackBar trackBar_KernelHeight;
        private System.Windows.Forms.NumericUpDown numericUpDown_KernelHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_SigmaX;
        private System.Windows.Forms.TrackBar trackBar_SigmaX;
        private System.Windows.Forms.NumericUpDown numericUpDown_SigmaX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel_SigmaY;
        private System.Windows.Forms.TrackBar trackBar_SigmaY;
        private System.Windows.Forms.NumericUpDown numericUpDown_SigmaY;
    }
}