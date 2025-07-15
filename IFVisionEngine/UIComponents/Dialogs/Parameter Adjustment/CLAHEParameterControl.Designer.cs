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
            this.trackBar_ClipLimit = new Sunny.UI.UITrackBar();
            this.numericUpDown_ClipLimit = new Sunny.UI.UIDoubleUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_TileHeight = new System.Windows.Forms.Panel();
            this.trackBar_TileHeight = new Sunny.UI.UITrackBar();
            this.numericUpDown_TileHeight = new Sunny.UI.UIIntegerUpDown();
            this.uiComboBox1 = new Sunny.UI.UIComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_TileWidth = new System.Windows.Forms.Panel();
            this.trackBar_TileWidth = new Sunny.UI.UITrackBar();
            this.numericUpDown_TileWidth = new Sunny.UI.UIIntegerUpDown();
            this.panel_Main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_ClipLimit.SuspendLayout();
            this.panel_TileHeight.SuspendLayout();
            this.numericUpDown_TileHeight.SuspendLayout();
            this.panel_TileWidth.SuspendLayout();
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
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
            this.panel_ClipLimit.Location = new System.Drawing.Point(73, 2);
            this.panel_ClipLimit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_ClipLimit.Name = "panel_ClipLimit";
            this.panel_ClipLimit.Size = new System.Drawing.Size(274, 20);
            this.panel_ClipLimit.TabIndex = 1;
            // 
            // trackBar_ClipLimit
            // 
            this.trackBar_ClipLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_ClipLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.trackBar_ClipLimit.Location = new System.Drawing.Point(0, 0);
            this.trackBar_ClipLimit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_ClipLimit.Maximum = 80;
            this.trackBar_ClipLimit.Minimum = 10;
            this.trackBar_ClipLimit.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_ClipLimit.Name = "trackBar_ClipLimit";
            this.trackBar_ClipLimit.Size = new System.Drawing.Size(181, 20);
            this.trackBar_ClipLimit.TabIndex = 0;
            this.trackBar_ClipLimit.Text = "uiTrackBar1";
            this.trackBar_ClipLimit.Value = 20;
            // 
            // numericUpDown_ClipLimit
            // 
            this.numericUpDown_ClipLimit.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_ClipLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numericUpDown_ClipLimit.Location = new System.Drawing.Point(181, 0);
            this.numericUpDown_ClipLimit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_ClipLimit.Maximum = 8D;
            this.numericUpDown_ClipLimit.Minimum = 1D;
            this.numericUpDown_ClipLimit.MinimumSize = new System.Drawing.Size(1, 1);
            this.numericUpDown_ClipLimit.Name = "numericUpDown_ClipLimit";
            this.numericUpDown_ClipLimit.ShowText = false;
            this.numericUpDown_ClipLimit.Size = new System.Drawing.Size(93, 20);
            this.numericUpDown_ClipLimit.TabIndex = 1;
            this.numericUpDown_ClipLimit.Text = "uiDoubleUpDown1";
            this.numericUpDown_ClipLimit.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.numericUpDown_ClipLimit.Value = 2D;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tile Height:";
            // 
            // panel_TileHeight
            // 
            this.panel_TileHeight.Controls.Add(this.trackBar_TileHeight);
            this.panel_TileHeight.Controls.Add(this.numericUpDown_TileHeight);
            this.panel_TileHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_TileHeight.Location = new System.Drawing.Point(73, 26);
            this.panel_TileHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_TileHeight.Name = "panel_TileHeight";
            this.panel_TileHeight.Size = new System.Drawing.Size(274, 20);
            this.panel_TileHeight.TabIndex = 3;
            // 
            // trackBar_TileHeight
            // 
            this.trackBar_TileHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_TileHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.trackBar_TileHeight.Location = new System.Drawing.Point(0, 0);
            this.trackBar_TileHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_TileHeight.Maximum = 32;
            this.trackBar_TileHeight.Minimum = 4;
            this.trackBar_TileHeight.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_TileHeight.Name = "trackBar_TileHeight";
            this.trackBar_TileHeight.Size = new System.Drawing.Size(181, 20);
            this.trackBar_TileHeight.TabIndex = 0;
            this.trackBar_TileHeight.Text = "uiTrackBar2";
            this.trackBar_TileHeight.Value = 8;
            // 
            // numericUpDown_TileHeight
            // 
            this.numericUpDown_TileHeight.Controls.Add(this.uiComboBox1);
            this.numericUpDown_TileHeight.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_TileHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numericUpDown_TileHeight.Location = new System.Drawing.Point(181, 0);
            this.numericUpDown_TileHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_TileHeight.Maximum = 32;
            this.numericUpDown_TileHeight.Minimum = 4;
            this.numericUpDown_TileHeight.MinimumSize = new System.Drawing.Size(1, 1);
            this.numericUpDown_TileHeight.Name = "numericUpDown_TileHeight";
            this.numericUpDown_TileHeight.ShowText = false;
            this.numericUpDown_TileHeight.Size = new System.Drawing.Size(93, 20);
            this.numericUpDown_TileHeight.TabIndex = 1;
            this.numericUpDown_TileHeight.Text = "uiIntegerUpDown1";
            this.numericUpDown_TileHeight.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.numericUpDown_TileHeight.Value = 8;
            // 
            // uiComboBox1
            // 
            this.uiComboBox1.DataSource = null;
            this.uiComboBox1.FillColor = System.Drawing.Color.White;
            this.uiComboBox1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.uiComboBox1.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.uiComboBox1.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiComboBox1.Location = new System.Drawing.Point(13, 17);
            this.uiComboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiComboBox1.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiComboBox1.Name = "uiComboBox1";
            this.uiComboBox1.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiComboBox1.Size = new System.Drawing.Size(150, 29);
            this.uiComboBox1.SymbolSize = 24;
            this.uiComboBox1.TabIndex = 3;
            this.uiComboBox1.Text = "uiComboBox1";
            this.uiComboBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiComboBox1.Watermark = "";
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
            this.panel_TileWidth.Location = new System.Drawing.Point(73, 50);
            this.panel_TileWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_TileWidth.Name = "panel_TileWidth";
            this.panel_TileWidth.Size = new System.Drawing.Size(274, 20);
            this.panel_TileWidth.TabIndex = 5;
            // 
            // trackBar_TileWidth
            // 
            this.trackBar_TileWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_TileWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.trackBar_TileWidth.Location = new System.Drawing.Point(0, 0);
            this.trackBar_TileWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_TileWidth.Maximum = 32;
            this.trackBar_TileWidth.Minimum = 4;
            this.trackBar_TileWidth.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_TileWidth.Name = "trackBar_TileWidth";
            this.trackBar_TileWidth.Size = new System.Drawing.Size(181, 20);
            this.trackBar_TileWidth.TabIndex = 0;
            this.trackBar_TileWidth.Text = "uiTrackBar3";
            this.trackBar_TileWidth.Value = 8;
            // 
            // numericUpDown_TileWidth
            // 
            this.numericUpDown_TileWidth.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_TileWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numericUpDown_TileWidth.Location = new System.Drawing.Point(181, 0);
            this.numericUpDown_TileWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_TileWidth.Maximum = 32;
            this.numericUpDown_TileWidth.Minimum = 4;
            this.numericUpDown_TileWidth.MinimumSize = new System.Drawing.Size(1, 1);
            this.numericUpDown_TileWidth.Name = "numericUpDown_TileWidth";
            this.numericUpDown_TileWidth.ShowText = false;
            this.numericUpDown_TileWidth.Size = new System.Drawing.Size(93, 20);
            this.numericUpDown_TileWidth.TabIndex = 1;
            this.numericUpDown_TileWidth.Text = "uiIntegerUpDown2";
            this.numericUpDown_TileWidth.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.numericUpDown_TileWidth.Value = 8;
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
            this.panel_TileHeight.ResumeLayout(false);
            this.numericUpDown_TileHeight.ResumeLayout(false);
            this.panel_TileWidth.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_ClipLimit;
        private Sunny.UI.UITrackBar trackBar_ClipLimit;
        private Sunny.UI.UIDoubleUpDown numericUpDown_ClipLimit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_TileHeight;
        private Sunny.UI.UITrackBar trackBar_TileHeight;
        private Sunny.UI.UIIntegerUpDown numericUpDown_TileHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_TileWidth;
        private Sunny.UI.UITrackBar trackBar_TileWidth;
        private Sunny.UI.UIIntegerUpDown numericUpDown_TileWidth;
        private Sunny.UI.UIComboBox uiComboBox1;
    }
}