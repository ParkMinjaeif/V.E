namespace IFVisionEngine.UIComponents.Dialogs
{
    partial class CLAHEParameterControl
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
            this.panel_ClipLimit = new System.Windows.Forms.Panel();
            this.trackBar_ClipLimit = new System.Windows.Forms.TrackBar();
            this.numericUpDown_ClipLimit = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_TileHeight = new System.Windows.Forms.Panel();
            this.trackBar_TileHeight = new System.Windows.Forms.TrackBar();
            this.numericUpDown_TileHeight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_TileWidth = new System.Windows.Forms.Panel();
            this.trackBar_TileWidth = new System.Windows.Forms.TrackBar();
            this.numericUpDown_TileWidth = new System.Windows.Forms.NumericUpDown();
            this.panel_Main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_ClipLimit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_ClipLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ClipLimit)).BeginInit();
            this.panel_TileHeight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_TileHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TileHeight)).BeginInit();
            this.panel_TileWidth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_TileWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TileWidth)).BeginInit();
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
            this.panel_Main.Size = new System.Drawing.Size(350, 160);
            this.panel_Main.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel_ClipLimit, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_TileHeight, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel_TileWidth, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(350, 72);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ClipLimit:";
            // 
            // panel_ClipLimit
            // 
            this.panel_ClipLimit.Controls.Add(this.trackBar_ClipLimit);
            this.panel_ClipLimit.Controls.Add(this.numericUpDown_ClipLimit);
            this.panel_ClipLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ClipLimit.Location = new System.Drawing.Point(143, 2);
            this.panel_ClipLimit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_ClipLimit.Name = "panel_ClipLimit";
            this.panel_ClipLimit.Size = new System.Drawing.Size(204, 20);
            this.panel_ClipLimit.TabIndex = 1;
            // 
            // trackBar_ClipLimit
            // 
            this.trackBar_ClipLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_ClipLimit.Location = new System.Drawing.Point(0, 0);
            this.trackBar_ClipLimit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_ClipLimit.Maximum = 80;
            this.trackBar_ClipLimit.Minimum = 10;
            this.trackBar_ClipLimit.Name = "trackBar_ClipLimit";
            this.trackBar_ClipLimit.Size = new System.Drawing.Size(144, 20);
            this.trackBar_ClipLimit.TabIndex = 0;
            this.trackBar_ClipLimit.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_ClipLimit.Value = 20;
            // 
            // numericUpDown_ClipLimit
            // 
            this.numericUpDown_ClipLimit.DecimalPlaces = 1;
            this.numericUpDown_ClipLimit.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_ClipLimit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_ClipLimit.Location = new System.Drawing.Point(144, 0);
            this.numericUpDown_ClipLimit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_ClipLimit.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown_ClipLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_ClipLimit.Name = "numericUpDown_ClipLimit";
            this.numericUpDown_ClipLimit.Size = new System.Drawing.Size(60, 21);
            this.numericUpDown_ClipLimit.TabIndex = 1;
            this.numericUpDown_ClipLimit.Value = new decimal(new int[] {
            2,
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
            this.label2.Size = new System.Drawing.Size(69, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tile Height:";
            // 
            // panel_TileHeight
            // 
            this.panel_TileHeight.Controls.Add(this.trackBar_TileHeight);
            this.panel_TileHeight.Controls.Add(this.numericUpDown_TileHeight);
            this.panel_TileHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_TileHeight.Location = new System.Drawing.Point(143, 26);
            this.panel_TileHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_TileHeight.Name = "panel_TileHeight";
            this.panel_TileHeight.Size = new System.Drawing.Size(204, 20);
            this.panel_TileHeight.TabIndex = 3;
            // 
            // trackBar_TileHeight
            // 
            this.trackBar_TileHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_TileHeight.Location = new System.Drawing.Point(0, 0);
            this.trackBar_TileHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_TileHeight.Maximum = 32;
            this.trackBar_TileHeight.Minimum = 4;
            this.trackBar_TileHeight.Name = "trackBar_TileHeight";
            this.trackBar_TileHeight.Size = new System.Drawing.Size(144, 20);
            this.trackBar_TileHeight.TabIndex = 0;
            this.trackBar_TileHeight.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_TileHeight.Value = 8;
            // 
            // numericUpDown_TileHeight
            // 
            this.numericUpDown_TileHeight.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_TileHeight.Location = new System.Drawing.Point(144, 0);
            this.numericUpDown_TileHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_TileHeight.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown_TileHeight.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown_TileHeight.Name = "numericUpDown_TileHeight";
            this.numericUpDown_TileHeight.Size = new System.Drawing.Size(60, 21);
            this.numericUpDown_TileHeight.TabIndex = 1;
            this.numericUpDown_TileHeight.Value = new decimal(new int[] {
            8,
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
            this.label3.Size = new System.Drawing.Size(64, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tile Width:";
            // 
            // panel_TileWidth
            // 
            this.panel_TileWidth.Controls.Add(this.trackBar_TileWidth);
            this.panel_TileWidth.Controls.Add(this.numericUpDown_TileWidth);
            this.panel_TileWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_TileWidth.Location = new System.Drawing.Point(143, 50);
            this.panel_TileWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_TileWidth.Name = "panel_TileWidth";
            this.panel_TileWidth.Size = new System.Drawing.Size(204, 20);
            this.panel_TileWidth.TabIndex = 5;
            // 
            // trackBar_TileWidth
            // 
            this.trackBar_TileWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_TileWidth.Location = new System.Drawing.Point(0, 0);
            this.trackBar_TileWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_TileWidth.Maximum = 32;
            this.trackBar_TileWidth.Minimum = 4;
            this.trackBar_TileWidth.Name = "trackBar_TileWidth";
            this.trackBar_TileWidth.Size = new System.Drawing.Size(144, 20);
            this.trackBar_TileWidth.TabIndex = 0;
            this.trackBar_TileWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_TileWidth.Value = 8;
            // 
            // numericUpDown_TileWidth
            // 
            this.numericUpDown_TileWidth.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_TileWidth.Location = new System.Drawing.Point(144, 0);
            this.numericUpDown_TileWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_TileWidth.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown_TileWidth.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown_TileWidth.Name = "numericUpDown_TileWidth";
            this.numericUpDown_TileWidth.Size = new System.Drawing.Size(60, 21);
            this.numericUpDown_TileWidth.TabIndex = 1;
            this.numericUpDown_TileWidth.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // CLAHEParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_Main);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CLAHEParameterControl";
            this.Size = new System.Drawing.Size(350, 160);
            this.Load += new System.EventHandler(this.CLAHEParameterControl_Load);
            this.panel_Main.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel_ClipLimit.ResumeLayout(false);
            this.panel_ClipLimit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_ClipLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ClipLimit)).EndInit();
            this.panel_TileHeight.ResumeLayout(false);
            this.panel_TileHeight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_TileHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TileHeight)).EndInit();
            this.panel_TileWidth.ResumeLayout(false);
            this.panel_TileWidth.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_TileWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TileWidth)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_ClipLimit;
        private System.Windows.Forms.TrackBar trackBar_ClipLimit;
        private System.Windows.Forms.NumericUpDown numericUpDown_ClipLimit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_TileHeight;
        private System.Windows.Forms.TrackBar trackBar_TileHeight;
        private System.Windows.Forms.NumericUpDown numericUpDown_TileHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_TileWidth;
        private System.Windows.Forms.TrackBar trackBar_TileWidth;
        private System.Windows.Forms.NumericUpDown numericUpDown_TileWidth;
    }
}