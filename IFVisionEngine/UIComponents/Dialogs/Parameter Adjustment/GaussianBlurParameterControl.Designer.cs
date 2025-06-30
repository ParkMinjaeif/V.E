namespace IFVisionEngine.UIComponents.Dialogs
{
    partial class GaussianBlurParameterControl
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
            this.kernelMaximum = new System.Windows.Forms.Label();
            this.kernel = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.kernelminimum = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(1110, 673);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Controls.Add(this.kernelMaximum, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.kernel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackBar1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.kernelminimum, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1110, 673);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // kernelMaximum
            // 
            this.kernelMaximum.AutoSize = true;
            this.kernelMaximum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kernelMaximum.Location = new System.Drawing.Point(1055, 0);
            this.kernelMaximum.Name = "kernelMaximum";
            this.kernelMaximum.Size = new System.Drawing.Size(52, 67);
            this.kernelMaximum.TabIndex = 14;
            this.kernelMaximum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kernel
            // 
            this.kernel.AutoSize = true;
            this.kernel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kernel.Location = new System.Drawing.Point(3, 0);
            this.kernel.Name = "kernel";
            this.kernel.Size = new System.Drawing.Size(193, 67);
            this.kernel.TabIndex = 0;
            this.kernel.Text = "kernel";
            this.kernel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trackBar1
            // 
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar1.Location = new System.Drawing.Point(334, 3);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(715, 61);
            this.trackBar1.TabIndex = 6;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown1.Location = new System.Drawing.Point(202, 3);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(71, 21);
            this.numericUpDown1.TabIndex = 9;
            // 
            // kernelminimum
            // 
            this.kernelminimum.AutoSize = true;
            this.kernelminimum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kernelminimum.Location = new System.Drawing.Point(279, 0);
            this.kernelminimum.Name = "kernelminimum";
            this.kernelminimum.Size = new System.Drawing.Size(49, 67);
            this.kernelminimum.TabIndex = 12;
            this.kernelminimum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GaussianBlurParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "GaussianBlurParameterControl";
            this.Size = new System.Drawing.Size(1110, 673);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label kernelMaximum;
        private System.Windows.Forms.Label kernel;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label kernelminimum;
    }
}
