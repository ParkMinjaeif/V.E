using NodeEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IFVisionEngine.Utils
{
    public class MyNodesContext
    {
        // INodesContext 인터페이스 구현
        public NodeVisual CurrentProcessingNode { get; set; }
        public event Action<string, NodeVisual, FeedbackType, object, bool> FeedbackInfo;

        // 프로젝트별 이벤트나 속성을 여기에 추가할 수 있습니다.
        // 예: public event Action SomeCustomEvent;

        // 노드 정의 예시 (README 참고)

        [Node(name: "Starter", menu: "General", isExecutionInitiator: true, description: "Node from which processing begins.")]
        public void Starter()
        {
            // FeedbackInfo?.Invoke("Starter node executed.", CurrentProcessingNode, FeedbackType.Info, null, false);
            Console.WriteLine("Starter node executed."); // 간단한 로그 출력
            // Clear(); // README 예시의 Clear()와 같은 메서드가 있다면 호출
        }

        [Node(name: "Display Object", menu: "Debug", description: "Allows to show any output in popup message box.")]
        public void ShowMessage(object obj)
        {
            if (obj != null)
            {
                MessageBox.Show(obj.ToString(), "Nodes Debug: " + obj.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Object is null.", "Nodes Debug", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 추가적인 노드 메서드들을 여기에 정의합니다.
        // 예시: LoadModel 등 (필요한 경우 사용자 정의 타입과 로직을 포함)
        /*
        [Node(name: "Load Model", menu:"General", customEditor:typeof(NELoadModel3D), description:"Node that loads 3D model from disk and also textures together.")]
        public void LoadModel(string path, bool useCage, out Model3D model)
        {
            // model = new Model3D();
            // ... 로딩 로직 ...
            // 이 예시를 사용하려면 Model3D 타입과 NELoadModel3D 컨트롤이 정의되어 있어야 합니다.
            // 지금은 주석 처리합니다.
            model = null; // 임시
            MessageBox.Show("LoadModel (Not Implemented): " + path);
        }
        */

        // 만약 README의 Clear()와 같은 메서드를 사용한다면 여기에 구현합니다.
        public void Clear()
        {
            // FeedbackInfo?.Invoke("Graph cleared.", null, FeedbackType.Info, null, false);
            Console.WriteLine("Graph clear requested.");
        }
    }
}
