namespace IFVisionEngine.UIComponents.Dialogs
{
    partial class CLAHEParameterControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TileWidthMaximum = new System.Windows.Forms.Label();
            this.TileWidthminimum = new System.Windows.Forms.Label();
            this.TileHeightMaximum = new System.Windows.Forms.Label();
            this.TileHeightminimum = new System.Windows.Forms.Label();
            this.ClipMaximum = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ClipLimit = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.CLipminimum = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(919, 564);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Controls.Add(this.TileWidthMaximum, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.TileWidthminimum, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.TileHeightMaximum, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.TileHeightminimum, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.ClipMaximum, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.trackBar3, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.trackBar2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ClipLimit, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackBar1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.CLipminimum, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(919, 564);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // TileWidthMaximum
            // 
            this.TileWidthMaximum.AutoSize = true;
            this.TileWidthMaximum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TileWidthMaximum.Location = new System.Drawing.Point(874, 112);
            this.TileWidthMaximum.Name = "TileWidthMaximum";
            this.TileWidthMaximum.Size = new System.Drawing.Size(42, 56);
            this.TileWidthMaximum.TabIndex = 18;
            this.TileWidthMaximum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TileWidthminimum
            // 
            this.TileWidthminimum.AutoSize = true;
            this.TileWidthminimum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TileWidthminimum.Location = new System.Drawing.Point(232, 112);
            this.TileWidthminimum.Name = "TileWidthminimum";
            this.TileWidthminimum.Size = new System.Drawing.Size(39, 56);
            this.TileWidthminimum.TabIndex = 17;
            this.TileWidthminimum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TileHeightMaximum
            // 
            this.TileHeightMaximum.AutoSize = true;
            this.TileHeightMaximum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TileHeightMaximum.Location = new System.Drawing.Point(874, 56);
            this.TileHeightMaximum.Name = "TileHeightMaximum";
            this.TileHeightMaximum.Size = new System.Drawing.Size(42, 56);
            this.TileHeightMaximum.TabIndex = 16;
            this.TileHeightMaximum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TileHeightminimum
            // 
            this.TileHeightminimum.AutoSize = true;
            this.TileHeightminimum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TileHeightminimum.Location = new System.Drawing.Point(232, 56);
            this.TileHeightminimum.Name = "TileHeightminimum";
            this.TileHeightminimum.Size = new System.Drawing.Size(39, 56);
            this.TileHeightminimum.TabIndex = 15;
            this.TileHeightminimum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ClipMaximum
            // 
            this.ClipMaximum.AutoSize = true;
            this.ClipMaximum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClipMaximum.Location = new System.Drawing.Point(874, 0);
            this.ClipMaximum.Name = "ClipMaximum";
            this.ClipMaximum.Size = new System.Drawing.Size(42, 56);
            this.ClipMaximum.TabIndex = 14;
            this.ClipMaximum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown3.Location = new System.Drawing.Point(168, 115);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(58, 21);
            this.numericUpDown3.TabIndex = 11;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown2.Location = new System.Drawing.Point(168, 59);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(58, 21);
            this.numericUpDown2.TabIndex = 10;
            // 
            // trackBar3
            // 
            this.trackBar3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar3.Location = new System.Drawing.Point(277, 115);
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(591, 50);
            this.trackBar3.TabIndex = 8;
            // 
            // trackBar2
            // 
            this.trackBar2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar2.Location = new System.Drawing.Point(277, 59);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(591, 50);
            this.trackBar2.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 56);
            this.label5.TabIndex = 5;
            this.label5.Text = "TileGridSize_Width";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 56);
            this.label3.TabIndex = 2;
            this.label3.Text = "TileGridSize_Height";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ClipLimit
            // 
            this.ClipLimit.AutoSize = true;
            this.ClipLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClipLimit.Location = new System.Drawing.Point(3, 0);
            this.ClipLimit.Name = "ClipLimit";
            this.ClipLimit.Size = new System.Drawing.Size(159, 56);
            this.ClipLimit.TabIndex = 0;
            this.ClipLimit.Text = "ClipLimit";
            this.ClipLimit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trackBar1
            // 
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar1.Location = new System.Drawing.Point(277, 3);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(591, 50);
            this.trackBar1.TabIndex = 6;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown1.Location = new System.Drawing.Point(168, 3);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(58, 21);
            this.numericUpDown1.TabIndex = 9;
            // 
            // CLipminimum
            // 
            this.CLipminimum.AutoSize = true;
            this.CLipminimum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CLipminimum.Location = new System.Drawing.Point(232, 0);
            this.CLipminimum.Name = "CLipminimum";
            this.CLipminimum.Size = new System.Drawing.Size(39, 56);
            this.CLipminimum.TabIndex = 12;
            this.CLipminimum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CLAHEParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "CLAHEParameterControl";
            this.Size = new System.Drawing.Size(919, 564);
            this.Load += new System.EventHandler(this.CLAHE_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ClipLimit;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label TileWidthMaximum;
        private System.Windows.Forms.Label TileWidthminimum;
        private System.Windows.Forms.Label TileHeightMaximum;
        private System.Windows.Forms.Label TileHeightminimum;
        private System.Windows.Forms.Label ClipMaximum;
        private System.Windows.Forms.Label CLipminimum;
    }
}
