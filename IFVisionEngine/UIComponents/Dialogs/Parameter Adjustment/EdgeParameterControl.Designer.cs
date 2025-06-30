namespace IFVisionEngine.UIComponents.Dialogs
{
    partial class EdgeParameterControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Threshold2Maximum = new System.Windows.Forms.Label();
            this.Threshold2minimum = new System.Windows.Forms.Label();
            this.Threshold1Maximum = new System.Windows.Forms.Label();
            this.numericUpDown_threshold2 = new System.Windows.Forms.NumericUpDown();
            this.trackBar_threshold2 = new System.Windows.Forms.TrackBar();
            this.Threshold2 = new System.Windows.Forms.Label();
            this.Threshold1 = new System.Windows.Forms.Label();
            this.trackBar_threshold1 = new System.Windows.Forms.TrackBar();
            this.numericUpDown_threshold1 = new System.Windows.Forms.NumericUpDown();
            this.Threshold1minimum = new System.Windows.Forms.Label();
            this.comboBox_edgeMethod = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_threshold2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_threshold2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_threshold1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_threshold1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Controls.Add(this.Threshold2Maximum, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.Threshold2minimum, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.Threshold1Maximum, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown_threshold2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.trackBar_threshold2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.Threshold2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Threshold1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackBar_threshold1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown_threshold1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Threshold1minimum, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_edgeMethod, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1379, 724);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Threshold2Maximum
            // 
            this.Threshold2Maximum.AutoSize = true;
            this.Threshold2Maximum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Threshold2Maximum.Location = new System.Drawing.Point(1311, 72);
            this.Threshold2Maximum.Name = "Threshold2Maximum";
            this.Threshold2Maximum.Size = new System.Drawing.Size(65, 72);
            this.Threshold2Maximum.TabIndex = 16;
            this.Threshold2Maximum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Threshold2minimum
            // 
            this.Threshold2minimum.AutoSize = true;
            this.Threshold2minimum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Threshold2minimum.Location = new System.Drawing.Point(347, 72);
            this.Threshold2minimum.Name = "Threshold2minimum";
            this.Threshold2minimum.Size = new System.Drawing.Size(62, 72);
            this.Threshold2minimum.TabIndex = 15;
            this.Threshold2minimum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Threshold1Maximum
            // 
            this.Threshold1Maximum.AutoSize = true;
            this.Threshold1Maximum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Threshold1Maximum.Location = new System.Drawing.Point(1311, 0);
            this.Threshold1Maximum.Name = "Threshold1Maximum";
            this.Threshold1Maximum.Size = new System.Drawing.Size(65, 72);
            this.Threshold1Maximum.TabIndex = 14;
            this.Threshold1Maximum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_threshold2
            // 
            this.numericUpDown_threshold2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown_threshold2.Location = new System.Drawing.Point(251, 75);
            this.numericUpDown_threshold2.Name = "numericUpDown_threshold2";
            this.numericUpDown_threshold2.Size = new System.Drawing.Size(90, 21);
            this.numericUpDown_threshold2.TabIndex = 10;
            // 
            // trackBar_threshold2
            // 
            this.trackBar_threshold2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_threshold2.Location = new System.Drawing.Point(415, 75);
            this.trackBar_threshold2.Name = "trackBar_threshold2";
            this.trackBar_threshold2.Size = new System.Drawing.Size(890, 66);
            this.trackBar_threshold2.TabIndex = 7;
            // 
            // Threshold2
            // 
            this.Threshold2.AutoSize = true;
            this.Threshold2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Threshold2.Location = new System.Drawing.Point(3, 72);
            this.Threshold2.Name = "Threshold2";
            this.Threshold2.Size = new System.Drawing.Size(242, 72);
            this.Threshold2.TabIndex = 2;
            this.Threshold2.Text = "Threshold2";
            this.Threshold2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Threshold1
            // 
            this.Threshold1.AutoSize = true;
            this.Threshold1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Threshold1.Location = new System.Drawing.Point(3, 0);
            this.Threshold1.Name = "Threshold1";
            this.Threshold1.Size = new System.Drawing.Size(242, 72);
            this.Threshold1.TabIndex = 0;
            this.Threshold1.Text = "Threshold1";
            this.Threshold1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trackBar_threshold1
            // 
            this.trackBar_threshold1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_threshold1.Location = new System.Drawing.Point(415, 3);
            this.trackBar_threshold1.Name = "trackBar_threshold1";
            this.trackBar_threshold1.Size = new System.Drawing.Size(890, 66);
            this.trackBar_threshold1.TabIndex = 6;
            // 
            // numericUpDown_threshold1
            // 
            this.numericUpDown_threshold1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown_threshold1.Location = new System.Drawing.Point(251, 3);
            this.numericUpDown_threshold1.Name = "numericUpDown_threshold1";
            this.numericUpDown_threshold1.Size = new System.Drawing.Size(90, 21);
            this.numericUpDown_threshold1.TabIndex = 9;
            // 
            // Threshold1minimum
            // 
            this.Threshold1minimum.AutoSize = true;
            this.Threshold1minimum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Threshold1minimum.Location = new System.Drawing.Point(347, 0);
            this.Threshold1minimum.Name = "Threshold1minimum";
            this.Threshold1minimum.Size = new System.Drawing.Size(62, 72);
            this.Threshold1minimum.TabIndex = 12;
            this.Threshold1minimum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_edgeMethod
            // 
            this.comboBox_edgeMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_edgeMethod.FormattingEnabled = true;
            this.comboBox_edgeMethod.Location = new System.Drawing.Point(3, 147);
            this.comboBox_edgeMethod.Name = "comboBox_edgeMethod";
            this.comboBox_edgeMethod.Size = new System.Drawing.Size(242, 20);
            this.comboBox_edgeMethod.TabIndex = 17;
            // 
            // EdgeParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EdgeParameterControl";
            this.Size = new System.Drawing.Size(1379, 724);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_threshold2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_threshold2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_threshold1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_threshold1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label Threshold2Maximum;
        private System.Windows.Forms.Label Threshold2minimum;
        private System.Windows.Forms.Label Threshold1Maximum;
        private System.Windows.Forms.NumericUpDown numericUpDown_threshold2;
        private System.Windows.Forms.TrackBar trackBar_threshold2;
        private System.Windows.Forms.Label Threshold2;
        private System.Windows.Forms.Label Threshold1;
        private System.Windows.Forms.TrackBar trackBar_threshold1;
        private System.Windows.Forms.NumericUpDown numericUpDown_threshold1;
        private System.Windows.Forms.Label Threshold1minimum;
        private System.Windows.Forms.ComboBox comboBox_edgeMethod;
    }
}
