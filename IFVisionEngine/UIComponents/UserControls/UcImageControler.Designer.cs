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
            this.pnlTitle.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBot
            // 
            this.pnlBot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBot.Location = new System.Drawing.Point(0, 780);
            this.pnlBot.Name = "pnlBot";
            this.pnlBot.Size = new System.Drawing.Size(984, 211);
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
            this.pnlMain.Controls.Add(this.pBMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 36);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(984, 744);
            this.pnlMain.TabIndex = 3;
            // 
            // pBMain
            // 
            this.pBMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBMain.Location = new System.Drawing.Point(0, 0);
            this.pBMain.Name = "pBMain";
            this.pBMain.Size = new System.Drawing.Size(982, 742);
            this.pBMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBMain.TabIndex = 0;
            this.pBMain.TabStop = false;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBot;
        private System.Windows.Forms.Panel pnlTitle;
        private Sunny.UI.UILabel uiLabel1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.PictureBox pBMain;
    }
}
