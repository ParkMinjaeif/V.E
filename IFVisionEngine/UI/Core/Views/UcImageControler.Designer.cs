namespace IFVisionEngine.UIComponents.UserControls
{
    partial class UcImageControler
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pBMain = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Drag_to_Zoom = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Reset = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_ruler = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Panning = new System.Windows.Forms.ToolStripButton();
            this.tLP_StatusBar = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbl_ordinate = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_PixelValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlMain.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBMain)).BeginInit();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tLP_StatusBar.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.panel2);
            this.pnlMain.Controls.Add(this.tLP_StatusBar);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(984, 991);
            this.pnlMain.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(982, 956);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pBMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(982, 923);
            this.panel1.TabIndex = 2;
            // 
            // pBMain
            // 
            this.pBMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBMain.Location = new System.Drawing.Point(0, 0);
            this.pBMain.Name = "pBMain";
            this.pBMain.Size = new System.Drawing.Size(982, 923);
            this.pBMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBMain.TabIndex = 0;
            this.pBMain.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.toolStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(982, 33);
            this.panel3.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Drag_to_Zoom,
            this.toolStripButton_Reset,
            this.toolStripButton_Delete,
            this.toolStripButton_ruler,
            this.toolStripButton_Panning});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(982, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_Drag_to_Zoom
            // 
            this.toolStripButton_Drag_to_Zoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Drag_to_Zoom.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.toolStripButton_Drag_to_Zoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Drag_to_Zoom.Name = "toolStripButton_Drag_to_Zoom";
            this.toolStripButton_Drag_to_Zoom.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Drag_to_Zoom.Text = "Zoom";
            this.toolStripButton_Drag_to_Zoom.Click += new System.EventHandler(this.toolStripButton_Drag_to_Zoom_Click);
            // 
            // toolStripButton_Reset
            // 
            this.toolStripButton_Reset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Reset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Reset.Name = "toolStripButton_Reset";
            this.toolStripButton_Reset.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Reset.Text = "Reset";
            this.toolStripButton_Reset.Click += new System.EventHandler(this.toolStripButton_Reset_Click);
            // 
            // toolStripButton_Delete
            // 
            this.toolStripButton_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Delete.Name = "toolStripButton_Delete";
            this.toolStripButton_Delete.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Delete.Text = "Delete";
            this.toolStripButton_Delete.Click += new System.EventHandler(this.toolStripButton_Delete_Click);
            // 
            // toolStripButton_ruler
            // 
            this.toolStripButton_ruler.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_ruler.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ruler.Name = "toolStripButton_ruler";
            this.toolStripButton_ruler.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_ruler.Text = "ruler";
            this.toolStripButton_ruler.Click += new System.EventHandler(this.toolStripButton_ruler_Click);
            // 
            // toolStripButton_Panning
            // 
            this.toolStripButton_Panning.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Panning.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Panning.Name = "toolStripButton_Panning";
            this.toolStripButton_Panning.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Panning.Text = "Panning";
            this.toolStripButton_Panning.Click += new System.EventHandler(this.toolStripButton_Panning_Click);
            // 
            // tLP_StatusBar
            // 
            this.tLP_StatusBar.ColumnCount = 1;
            this.tLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tLP_StatusBar.Controls.Add(this.statusStrip1, 0, 0);
            this.tLP_StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tLP_StatusBar.Location = new System.Drawing.Point(0, 956);
            this.tLP_StatusBar.Name = "tLP_StatusBar";
            this.tLP_StatusBar.RowCount = 1;
            this.tLP_StatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLP_StatusBar.Size = new System.Drawing.Size(982, 33);
            this.tLP_StatusBar.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_ordinate,
            this.lbl_PixelValue});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(982, 33);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbl_ordinate
            // 
            this.lbl_ordinate.Name = "lbl_ordinate";
            this.lbl_ordinate.Size = new System.Drawing.Size(52, 28);
            this.lbl_ordinate.Text = "X: -, Y: -";
            // 
            // lbl_PixelValue
            // 
            this.lbl_PixelValue.Name = "lbl_PixelValue";
            this.lbl_PixelValue.Size = new System.Drawing.Size(79, 28);
            this.lbl_PixelValue.Text = "R: -, G: -, B: -";
            // 
            // UcImageControler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "UcImageControler";
            this.Size = new System.Drawing.Size(984, 991);
            this.pnlMain.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pBMain)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tLP_StatusBar.ResumeLayout(false);
            this.tLP_StatusBar.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.PictureBox pBMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tLP_StatusBar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbl_ordinate;
        private System.Windows.Forms.ToolStripStatusLabel lbl_PixelValue;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_Drag_to_Zoom;
        private System.Windows.Forms.ToolStripButton toolStripButton_Reset;
        private System.Windows.Forms.ToolStripButton toolStripButton_Delete;
        private System.Windows.Forms.ToolStripButton toolStripButton_ruler;
        private System.Windows.Forms.ToolStripButton toolStripButton_Panning;
    }
}
