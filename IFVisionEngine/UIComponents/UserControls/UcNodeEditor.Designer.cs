namespace IFVisionEngine.UIComponents.UserControls
{
    partial class UcNodeEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcNodeEditor));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton_Load = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton_Run = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.nodesControl1 = new NodeEditor.NodesControl();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Save,
            this.toolStripLabel1,
            this.toolStripButton_Load,
            this.toolStripLabel2,
            this.toolStripButton_Run,
            this.toolStripLabel4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1904, 31);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // 
            this.toolStripButton_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Save.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.toolStripButton_Save.Image = global::IFVisionEngine.Properties.Resources.Zoom;
            this.toolStripButton_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Save.Name = "toolStripButton_Save";
            this.toolStripButton_Save.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton_Save.Text = "toolStripButton1";
            this.toolStripButton_Save.Click += new System.EventHandler(this.toolStripButton_Save_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(31, 28);
            this.toolStripLabel1.Text = "저장";
            // 
            // toolStripButton_Load
            // 
            this.toolStripButton_Load.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Load.Image = global::IFVisionEngine.Properties.Resources.Zoom;
            this.toolStripButton_Load.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Load.Name = "toolStripButton_Load";
            this.toolStripButton_Load.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton_Load.Text = "toolStripButton2";
            this.toolStripButton_Load.Click += new System.EventHandler(this.toolStripButton_Load_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(31, 28);
            this.toolStripLabel2.Text = "로드";
            // 
            // toolStripButton_Run
            // 
            this.toolStripButton_Run.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Run.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Run.Image")));
            this.toolStripButton_Run.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Run.Name = "toolStripButton_Run";
            this.toolStripButton_Run.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton_Run.Text = "toolStripButton1";
            this.toolStripButton_Run.Click += new System.EventHandler(this.toolStripButton_Run_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(31, 28);
            this.toolStripLabel4.Text = "실행";
            // 
            // nodesControl1
            // 
            this.nodesControl1.BackColor = System.Drawing.Color.Black;
            this.nodesControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nodesControl1.BackgroundImage")));
            this.nodesControl1.Context = null;
            this.nodesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodesControl1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nodesControl1.Location = new System.Drawing.Point(0, 0);
            this.nodesControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nodesControl1.Name = "nodesControl1";
            this.nodesControl1.Size = new System.Drawing.Size(1904, 991);
            this.nodesControl1.TabIndex = 0;
            this.nodesControl1.OnNodeContextSelected += new System.Action<object>(this.nodesControl1_OnNodeContextSelected);
            this.nodesControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.nodesControl1_MouseDown);
            this.nodesControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.nodesControl1_MouseMove);
            this.nodesControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.nodesControl1_MouseUp);
            // 
            // UcNodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.nodesControl1);
            this.Name = "UcNodeEditor";
            this.Size = new System.Drawing.Size(1904, 991);
            this.Load += new System.EventHandler(this.UcNodeEditor_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NodeEditor.NodesControl nodesControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_Save;
        private System.Windows.Forms.ToolStripButton toolStripButton_Load;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolStripButton_Run;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
    }
}
