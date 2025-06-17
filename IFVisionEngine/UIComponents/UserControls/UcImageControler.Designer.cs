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
            this.pnlBot = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pBMain = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tLP_StatusBar = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbl_ordinate = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_PixelValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlTitle.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBMain)).BeginInit();
            this.panel1.SuspendLayout();
            this.tLP_StatusBar.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBot
            // 
            this.pnlBot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBot.Location = new System.Drawing.Point(0, 805);
            this.pnlBot.Name = "pnlBot";
            this.pnlBot.Size = new System.Drawing.Size(984, 186);
            this.pnlBot.TabIndex = 0;
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.uiLabel1);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(984, 36);
            this.pnlTitle.TabIndex = 2;
            // 
            // uiLabel1
            // 
            this.uiLabel1.BackColor = System.Drawing.Color.White;
            this.uiLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLabel1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(0, 0);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(984, 36);
            this.uiLabel1.TabIndex = 2;
            this.uiLabel1.Text = "Image Viewer";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.tLP_StatusBar);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 36);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(984, 769);
            this.pnlMain.TabIndex = 3;
            // 
            // pBMain
            // 
            this.pBMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBMain.Location = new System.Drawing.Point(0, 0);
            this.pBMain.Name = "pBMain";
            this.pBMain.Size = new System.Drawing.Size(982, 734);
            this.pBMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBMain.TabIndex = 0;
            this.pBMain.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pBMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(982, 734);
            this.panel1.TabIndex = 2;
            // 
            // tLP_StatusBar
            // 
            this.tLP_StatusBar.ColumnCount = 2;
            this.tLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tLP_StatusBar.Controls.Add(this.statusStrip1, 0, 0);
            this.tLP_StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tLP_StatusBar.Location = new System.Drawing.Point(0, 734);
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
            this.statusStrip1.Size = new System.Drawing.Size(491, 33);
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
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.pnlBot);
            this.Name = "UcImageControler";
            this.Size = new System.Drawing.Size(984, 991);
            this.pnlTitle.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pBMain)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tLP_StatusBar.ResumeLayout(false);
            this.tLP_StatusBar.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBot;
        private System.Windows.Forms.Panel pnlTitle;
        private Sunny.UI.UILabel uiLabel1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.PictureBox pBMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tLP_StatusBar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbl_ordinate;
        private System.Windows.Forms.ToolStripStatusLabel lbl_PixelValue;
    }
}
