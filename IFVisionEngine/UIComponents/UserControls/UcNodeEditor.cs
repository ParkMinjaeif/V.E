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
using System.Drawing.Drawing2D;

namespace IFVisionEngine.UIComponents.UserControls
{
    public partial class UcNodeEditor: UserControl
    {
        public static MyNodesContext _nodesContext; // 컨텍스트 멤버 변수
        private Form1 _formMainInstance;

        public UcNodeEditor(Form1 mainForm)
        {
            InitializeComponent();
            _formMainInstance = mainForm;

            // 컨텍스트 초기화 및 할당
            _nodesContext = new MyNodesContext();
            _nodesContext.Invoker = this; // 컨텍스트에 UI 컨트롤 참조를 전달합니다.
            nodesControl1.Context = _nodesContext;
        }

        private void UcNodeEditor_Load(object sender, EventArgs e)
        {
            nodesControl1.Execute();
        }

        public MyNodesContext GetContext()
        {
            return _nodesContext;
        }

        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON 파일 (*.json)|*.json|모든 파일 (*.*)|*.*",
                Title = "노드 레이아웃 저장"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveNodeLayoutToJson(nodesControl1, saveFileDialog.FileName);
            }
        }

        private void toolStripButton_Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON 파일 (*.json)|*.json|모든 파일 (*.*)|*.*",
                Title = "노드 레이아웃 불러오기",
                CheckFileExists = true,
                CheckPathExists = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadNodeLayoutFromJson(nodesControl1, openFileDialog.FileName);
                // 로드 후, 그래프를 다시 실행하거나 UI를 갱신해야 할 수 있습니다.
                // 예를 들어, 로드된 그래프를 바로 실행하려면:
                // nodesControl1.Execute(); 
                // 또는 화면을 강제로 새로고침:
                // nodesControl1.Invalidate(); 
            }
        }

        // 이전에 제공된 JSON 저장/로드 메서드 (SaveNodeLayoutToJson, LoadNodeLayoutFromJson, JsonNodeGraphWrapper 클래스)
        // 여기에 해당 메서드들을 배치하거나, 별도의 유틸리티 클래스로 분리할 수 있습니다.
        public void SaveNodeLayoutToJson(NodesControl nodesCtrl, string filePath)
        {
            try
            {
                byte[] serializedData = nodesCtrl.Serialize();
                if (serializedData == null) { Console.WriteLine("직렬화 실패"); return; }
                string base64Data = Convert.ToBase64String(serializedData);
                var jsonData = new { NodeGraphData = base64Data };
                string jsonString = System.Text.Json.JsonSerializer.Serialize(jsonData, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(filePath, jsonString, Encoding.UTF8);
                MessageBox.Show("레이아웃이 저장되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"저장 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class JsonNodeGraphWrapper
        {
            public string NodeGraphData { get; set; }
        }

        public void LoadNodeLayoutFromJson(NodesControl nodesCtrl, string filePath)
        {
            try
            {
                if (!System.IO.File.Exists(filePath)) { MessageBox.Show("파일 없음"); return; }
                string jsonString = System.IO.File.ReadAllText(filePath, Encoding.UTF8);
                var jsonData = System.Text.Json.JsonSerializer.Deserialize<JsonNodeGraphWrapper>(jsonString);
                if (jsonData == null || string.IsNullOrEmpty(jsonData.NodeGraphData)) { MessageBox.Show("데이터 읽기 실패"); return; }
                byte[] serializedData = Convert.FromBase64String(jsonData.NodeGraphData);
                nodesCtrl.Deserialize(serializedData);
                MessageBox.Show("레이아웃을 불러왔습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"로드 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton_toggleSize_Click(object sender, EventArgs e)
        {
            _formMainInstance.togglePnlLeft();
        }

        private void nodesControl1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void toolStripButton_Run_Click(object sender, EventArgs e)
        {
            // '실행' 버튼을 누르면 전체 워크플로우를 실행합니다.
            nodesControl1.Execute();
        }

    }
}
