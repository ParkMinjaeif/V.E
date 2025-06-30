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
using IFVisionEngine.Manager;
using OpenCvSharp;

namespace IFVisionEngine.UIComponents.UserControls
{
    /// <summary>
    /// 노드 실행 결과를 관리하고 시각화하는 사용자 컨트롤입니다.
    /// TreeView를 통해 실행된 노드 목록을 표시하고, PropertyGrid로 노드 속성을 확인할 수 있습니다.
    /// </summary>
    public partial class UcNodeExecutionView : UserControl
    {
        #region Data Classes and Fields
        /// <summary>
        /// 노드의 키(식별자)와 이름을 매핑하는 데이터 클래스입니다.
        /// 노드 실행 히스토리에서 이미지 검색을 위해 사용됩니다.
        /// </summary>
        public class key_name
        {
            public string key { get; set; }     // 노드의 고유 식별자 (ImageDataManager에서 사용)
            public string name { get; set; }    // 노드의 표시 이름 (TreeView에서 표시)
        }

        // 1. 노드 이미지 히스토리 관리
        public List<key_name> nodeImageKeyHistory = new List<key_name>();  // 실행된 노드의 키-이름 매핑 히스토리
        #endregion

        #region Constructor and Initialization
        /// <summary>
        /// 생성자 - 컨트롤 초기화 및 이벤트 핸들러 연결
        /// </summary>
        public UcNodeExecutionView()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            // 2. TreeView 이벤트 핸들러 연결
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;  // 노드 클릭 시 이미지 표시
        }
        #endregion

        #region Node Execution Management
        /// <summary>
        /// 실행된 노드 정보를 TreeView에 추가합니다.
        /// 스레드 안전성을 위해 UI 스레드에서 실행되도록 보장합니다.
        /// </summary>
        /// <param name="node">추가할 노드 객체</param>
        public void AddExecutionData(NodeVisual node)
        {
            if (this.treeView1.InvokeRequired)
            {
                // 다른 스레드에서 호출된 경우 UI 스레드로 마샬링
                this.treeView1.Invoke(new MethodInvoker(() => AddNodeToTreeView(node)));
            }
            else
            {
                // 이미 UI 스레드인 경우 직접 실행
                AddNodeToTreeView(node);
            }
        }

        /// <summary>
        /// 노드를 TreeView에 실제로 추가하는 내부 메서드입니다.
        /// </summary>
        /// <param name="node">추가할 노드 객체</param>
        private void AddNodeToTreeView(NodeVisual node)
        {
            if (node == null) return;

            // 1. TreeNode 생성 및 노드 컨텍스트 저장
            TreeNode treeNode = new TreeNode(node.Name);
            treeNode.Tag = node.GetNodeContext(); // Tag 속성에 실제 노드의 속성 정보를 저장

            // 2. TreeView에 노드 추가
            this.treeView1.Nodes.Add(treeNode);
        }

        /// <summary>
        /// TreeView의 모든 내용을 지웁니다.
        /// 노드 히스토리와 PropertyGrid도 함께 초기화됩니다.
        /// </summary>
        public void ClearData()
        {
            // 1. 노드 히스토리 초기화
            nodeImageKeyHistory.Clear();

            if (this.treeView1.InvokeRequired)
            {
                // 다른 스레드에서 호출된 경우 UI 스레드로 마샬링
                this.treeView1.Invoke(new MethodInvoker(() => {
                    this.treeView1.Nodes.Clear();
                    this.propertyGrid1.SelectedObject = null;
                }));
            }
            else
            {
                // 이미 UI 스레드인 경우 직접 실행
                this.treeView1.Nodes.Clear();
                this.propertyGrid1.SelectedObject = null;
            }
        }
        #endregion

        #region TreeView Event Handlers
        /// <summary>
        /// TreeView에서 다른 노드를 선택했을 때 호출됩니다.
        /// 선택된 노드의 속성을 PropertyGrid에 표시합니다.
        /// </summary>
        /// <param name="sender">이벤트 발생 객체</param>
        /// <param name="e">TreeView 이벤트 인수</param>
        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 선택된 노드의 Tag (노드 컨텍스트)를 PropertyGrid에 표시
            this.propertyGrid1.SelectedObject = e.Node?.Tag;
        }

        /// <summary>
        /// TreeView 노드 클릭 시 호출 - 해당 노드의 이미지를 이미지 컨트롤러에 표시
        /// </summary>
        /// <param name="sender">이벤트 발생 객체</param>
        /// <param name="e">마우스 클릭 이벤트 인수</param>
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                string nodeText = treeView1.SelectedNode.Text;

                // 1. 시작점 노드는 이미지가 없으므로 처리하지 않음
                if (nodeText == "시작점")
                    return;

                // 2. 노드 히스토리에서 해당 이름의 노드 검색
                var found = nodeImageKeyHistory.FirstOrDefault(x => x.name == nodeText);
                if (found != null)
                {
                    // 3. ImageDataManager에서 이미지 가져오기
                    Mat image = ImageDataManager.GetImage(found.key);

                    // 4. 이미지가 존재할 때만 이미지 컨트롤러에 표시
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
                    MessageBox.Show("리스트에 해당 노드 이름이 없습니다.");
                }
            }
            else
            {
                MessageBox.Show("선택된 노드가 없습니다.");
            }
        }

        /// <summary>
        /// TreeView 마우스 다운 이벤트 - 클릭된 노드를 즉시 선택 상태로 만듦
        /// (기본 TreeView 동작보다 빠른 선택을 위해)
        /// </summary>
        /// <param name="sender">이벤트 발생 객체</param>
        /// <param name="e">마우스 이벤트 인수</param>
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            // 1. 클릭된 위치의 노드 찾기
            var clickedNode = treeView1.GetNodeAt(e.X, e.Y);
            if (clickedNode != null)
                treeView1.SelectedNode = clickedNode; // 2. 해당 노드를 즉시 선택 상태로 설정
        }
        #endregion
    }
}