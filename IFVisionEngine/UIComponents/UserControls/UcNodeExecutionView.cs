using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NodeEditor;
using IFVisionEngine.Manager;
using OpenCvSharp;
using IFVisionEngine.UIComponents.Data;
using IFVisionEngine.UIComponents.Managers;
using IFVisionEngine.Themes;

namespace IFVisionEngine.UIComponents.UserControls
{
    /// <summary>
    /// 노드 실행 결과를 관리하고 시각화하는 사용자 컨트롤
    /// </summary>
    public partial class UcNodeExecutionView : UserControl
    {
        #region Data Classes and Fields

        /// <summary>
        /// 노드의 키와 이름을 매핑하는 데이터 클래스
        /// </summary>
        public class key_name
        {
            public string key { get; set; }
            public string name { get; set; }
        }
            
        // 노드 이미지 히스토리 관리
        public List<key_name> nodeImageKeyHistory = new List<key_name>();    

        // 이벤트 핸들러 해제를 위한 플래그
        private bool _eventHandlersConnected = false;
        private bool _disposed = false;
        #endregion

        #region Constructor and Initialization
        public UcNodeExecutionView()
        {
            InitializeComponent();
            SetupEventHandlers();
            ThemeManager.ApplyThemeToControl(this);
        }

        private void SetupEventHandlers()
        {
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            treeView1.MouseDown += treeView1_MouseDown;
        }
        #endregion

        #region Node Execution Management
        /// <summary>
        /// 실행된 노드 정보를 TreeView에 추가
        /// </summary>
        public void AddExecutionData(NodeVisual node)
        {
            if (this.treeView1.InvokeRequired)
            {
                this.treeView1.Invoke(new MethodInvoker(() => AddNodeToTreeView(node)));
            }
            else
            {
                AddNodeToTreeView(node);
            }
        }

        private void AddNodeToTreeView(NodeVisual node)
        {
            if (node == null) return;

            TreeNode treeNode = new TreeNode(node.Name);
            treeNode.Tag = node.GetNodeContext();
            this.treeView1.Nodes.Add(treeNode);
        }

        /// <summary>
        /// TreeView의 모든 내용을 삭제
        /// </summary>
        public void ClearData()
        {
            nodeImageKeyHistory.Clear();

            if (this.treeView1.InvokeRequired)
            {
                this.treeView1.Invoke(new MethodInvoker(() => {
                    this.treeView1.Nodes.Clear();
                }));
            }
            else
            {
                this.treeView1.Nodes.Clear();
            }

            Console.WriteLine("[UcNodeExecutionView] 데이터 초기화 완료");
        }

        #endregion

        #region TreeView Event Handlers
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Text == "시작점")
                    return;
                DisplayNodeImage(treeView1.SelectedNode.Text);
            }
        }

        private void DisplayNodeImage(string nodeText)
        {
            var found = nodeImageKeyHistory.FirstOrDefault(x => x.name == nodeText);
            if (found != null)
            {
                Mat image = ImageDataManager.GetImage(found.key);
                if (image != null && !image.Empty())
                {
                    AppUIManager.ucImageControler.DisplayImage(image);
                }
                else
                {
                    MessageBox.Show("이미지를 찾을 수 없습니다.");
                }
            }
            else
            {
                Console.WriteLine($"[UcNodeExecutionView] 노드 이미지 키 '{nodeText}'를 찾을 수 없습니다.");
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            var clickedNode = treeView1.GetNodeAt(e.X, e.Y);
            if (clickedNode != null)
                treeView1.SelectedNode = clickedNode;
        }
        #endregion
    }
}