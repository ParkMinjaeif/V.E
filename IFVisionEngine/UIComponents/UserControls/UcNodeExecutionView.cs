using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NodeEditor;

namespace IFVisionEngine.UIComponents.UserControls
{
    public partial class UcNodeExecutionView : UserControl
    {
        public UcNodeExecutionView()
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// 실행된 노드 정보를 TreeView에 추가합니다.
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
            treeNode.Tag = node.GetNodeContext(); // Tag 속성에 실제 노드의 속성 정보를 저장
            this.treeView1.Nodes.Add(treeNode);
        }

        /// <summary>
        /// TreeView에서 다른 노드를 선택했을 때 호출됩니다.
        /// </summary>
        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.propertyGrid1.SelectedObject = e.Node?.Tag;
        }

        /// <summary>
        /// TreeView의 모든 내용을 지웁니다.
        /// </summary>
        public void ClearData()
        {
            if (this.treeView1.InvokeRequired)
            {
                this.treeView1.Invoke(new MethodInvoker(() => {
                    this.treeView1.Nodes.Clear();
                    this.propertyGrid1.SelectedObject = null;
                }));
            }
            else
            {
                this.treeView1.Nodes.Clear();
                this.propertyGrid1.SelectedObject = null;
            }
        }
    }
}
