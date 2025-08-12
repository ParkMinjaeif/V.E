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
            this.comboBox_Method = new Sunny.UI.UIComboBox();
            this.label_Threshold1 = new System.Windows.Forms.Label();
            this.panel_Threshold1 = new System.Windows.Forms.Panel();
            this.trackBar_Threshold1 = new Sunny.UI.UITrackBar();
            this.numericUpDown_Threshold1 = new Sunny.UI.UIIntegerUpDown();
            this.label_Threshold2 = new System.Windows.Forms.Label();
            this.panel_Threshold2 = new System.Windows.Forms.Panel();
            this.trackBar_Threshold2 = new Sunny.UI.UITrackBar();
            this.numericUpDown_Threshold2 = new Sunny.UI.UIIntegerUpDown();
            this.label_KernelSize = new System.Windows.Forms.Label();
            this.comboBox_KernelSize = new Sunny.UI.UIComboBox();
            this.panel_Main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_Threshold1.SuspendLayout();
            this.panel_Threshold2.SuspendLayout();
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
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
            this.comboBox_Method.DataSource = null;
            this.comboBox_Method.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.comboBox_Method.FillColor = System.Drawing.Color.White;
            this.comboBox_Method.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.comboBox_Method.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.comboBox_Method.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.comboBox_Method.Location = new System.Drawing.Point(79, 2);
            this.comboBox_Method.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_Method.MinimumSize = new System.Drawing.Size(63, 0);
            this.comboBox_Method.Name = "comboBox_Method";
            this.comboBox_Method.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.comboBox_Method.Size = new System.Drawing.Size(298, 20);
            this.comboBox_Method.SymbolSize = 24;
            this.comboBox_Method.TabIndex = 1;
            this.comboBox_Method.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBox_Method.Watermark = "";
            // 
            // label_Threshold1
            // 
            this.label_Threshold1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_Threshold1.AutoSize = true;
            this.label_Threshold1.Location = new System.Drawing.Point(3, 24);
            this.label_Threshold1.Name = "label_Threshold1";
            this.label_Threshold1.Size = new System.Drawing.Size(57, 24);
            this.label_Threshold1.TabIndex = 2;
            this.label_Threshold1.Text = "하위 임계값:";
            // 
            // panel_Threshold1
            // 
            this.panel_Threshold1.Controls.Add(this.trackBar_Threshold1);
            this.panel_Threshold1.Controls.Add(this.numericUpDown_Threshold1);
            this.panel_Threshold1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Threshold1.Location = new System.Drawing.Point(79, 26);
            this.panel_Threshold1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Threshold1.Name = "panel_Threshold1";
            this.panel_Threshold1.Size = new System.Drawing.Size(298, 20);
            this.panel_Threshold1.TabIndex = 3;
            // 
            // trackBar_Threshold1
            // 
            this.trackBar_Threshold1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Threshold1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.trackBar_Threshold1.Location = new System.Drawing.Point(0, 0);
            this.trackBar_Threshold1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_Threshold1.Maximum = 500;
            this.trackBar_Threshold1.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_Threshold1.Name = "trackBar_Threshold1";
            this.trackBar_Threshold1.Size = new System.Drawing.Size(201, 20);
            this.trackBar_Threshold1.TabIndex = 0;
            this.trackBar_Threshold1.Text = "uiTrackBar1";
            this.trackBar_Threshold1.Value = 100;
            // 
            // numericUpDown_Threshold1
            // 
            this.numericUpDown_Threshold1.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_Threshold1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numericUpDown_Threshold1.Location = new System.Drawing.Point(201, 0);
            this.numericUpDown_Threshold1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_Threshold1.Maximum = 500;
            this.numericUpDown_Threshold1.Minimum = 0;
            this.numericUpDown_Threshold1.MinimumSize = new System.Drawing.Size(1, 1);
            this.numericUpDown_Threshold1.Name = "numericUpDown_Threshold1";
            this.numericUpDown_Threshold1.ShowText = false;
            this.numericUpDown_Threshold1.Size = new System.Drawing.Size(97, 20);
            this.numericUpDown_Threshold1.TabIndex = 1;
            this.numericUpDown_Threshold1.Text = "uiIntegerUpDown1";
            this.numericUpDown_Threshold1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.numericUpDown_Threshold1.Value = 100;
            // 
            // label_Threshold2
            // 
            this.label_Threshold2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_Threshold2.AutoSize = true;
            this.label_Threshold2.Location = new System.Drawing.Point(3, 48);
            this.label_Threshold2.Name = "label_Threshold2";
            this.label_Threshold2.Size = new System.Drawing.Size(57, 24);
            this.label_Threshold2.TabIndex = 4;
            this.label_Threshold2.Text = "상위 임계값:";
            // 
            // panel_Threshold2
            // 
            this.panel_Threshold2.Controls.Add(this.trackBar_Threshold2);
            this.panel_Threshold2.Controls.Add(this.numericUpDown_Threshold2);
            this.panel_Threshold2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Threshold2.Location = new System.Drawing.Point(79, 50);
            this.panel_Threshold2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Threshold2.Name = "panel_Threshold2";
            this.panel_Threshold2.Size = new System.Drawing.Size(298, 20);
            this.panel_Threshold2.TabIndex = 5;
            // 
            // trackBar_Threshold2
            // 
            this.trackBar_Threshold2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Threshold2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.trackBar_Threshold2.Location = new System.Drawing.Point(0, 0);
            this.trackBar_Threshold2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar_Threshold2.Maximum = 500;
            this.trackBar_Threshold2.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackBar_Threshold2.Name = "trackBar_Threshold2";
            this.trackBar_Threshold2.Size = new System.Drawing.Size(201, 20);
            this.trackBar_Threshold2.TabIndex = 0;
            this.trackBar_Threshold2.Text = "uiTrackBar2";
            this.trackBar_Threshold2.Value = 200;
            // 
            // numericUpDown_Threshold2
            // 
            this.numericUpDown_Threshold2.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDown_Threshold2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numericUpDown_Threshold2.Location = new System.Drawing.Point(201, 0);
            this.numericUpDown_Threshold2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_Threshold2.Maximum = 500;
            this.numericUpDown_Threshold2.Minimum = 0;
            this.numericUpDown_Threshold2.MinimumSize = new System.Drawing.Size(1, 1);
            this.numericUpDown_Threshold2.Name = "numericUpDown_Threshold2";
            this.numericUpDown_Threshold2.ShowText = false;
            this.numericUpDown_Threshold2.Size = new System.Drawing.Size(97, 20);
            this.numericUpDown_Threshold2.TabIndex = 1;
            this.numericUpDown_Threshold2.Text = "uiIntegerUpDown2";
            this.numericUpDown_Threshold2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.numericUpDown_Threshold2.Value = 200;
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
            this.comboBox_KernelSize.DataSource = null;
            this.comboBox_KernelSize.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.comboBox_KernelSize.FillColor = System.Drawing.Color.White;
            this.comboBox_KernelSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.comboBox_KernelSize.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.comboBox_KernelSize.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.comboBox_KernelSize.Location = new System.Drawing.Point(79, 74);
            this.comboBox_KernelSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_KernelSize.MinimumSize = new System.Drawing.Size(63, 0);
            this.comboBox_KernelSize.Name = "comboBox_KernelSize";
            this.comboBox_KernelSize.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.comboBox_KernelSize.Size = new System.Drawing.Size(298, 20);
            this.comboBox_KernelSize.SymbolSize = 24;
            this.comboBox_KernelSize.TabIndex = 7;
            this.comboBox_KernelSize.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBox_KernelSize.Visible = false;
            this.comboBox_KernelSize.Watermark = "";
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
            this.panel_Threshold2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_Method;
        private Sunny.UI.UIComboBox comboBox_Method;
        private System.Windows.Forms.Label label_Threshold1;
        private System.Windows.Forms.Panel panel_Threshold1;
        private Sunny.UI.UITrackBar trackBar_Threshold1;
        private Sunny.UI.UIIntegerUpDown numericUpDown_Threshold1;
        private System.Windows.Forms.Label label_Threshold2;
        private System.Windows.Forms.Panel panel_Threshold2;
        private Sunny.UI.UITrackBar trackBar_Threshold2;
        private Sunny.UI.UIIntegerUpDown numericUpDown_Threshold2;
        private System.Windows.Forms.Label label_KernelSize;
        private Sunny.UI.UIComboBox comboBox_KernelSize;
    }
}