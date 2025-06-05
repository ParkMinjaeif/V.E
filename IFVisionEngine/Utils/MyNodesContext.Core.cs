// MyNodesContext.Core.cs
using System;
using System.Windows.Forms; // MessageBox 등
using System.Numerics;      // Vector3
using System.Runtime.Serialization; // ISerializable
using System.ComponentModel;  // TypeConverter, ExpandableObjectConverter
using NodeEditor;           // 사용하시는 노드 에디터 라이브러리

// OpenCvSharp 관련 using은 이 파일에서 직접적으로 필요하지 않다면 제거 가능
// using OpenCvSharp;
// using OpenCvSharp.Extensions;

public partial class MyNodesContext : INodesContext
{
    public NodeVisual CurrentProcessingNode { get; set; }
    public event Action<string, NodeVisual, FeedbackType, object, bool> FeedbackInfo;

    // --- 기존 Vector3W 클래스 ---
    [Serializable]
    [System.ComponentModel.TypeConverter(typeof(ExpandableObjectConverter))]
    public class Vector3W : ISerializable
    {
        public Vector3 Value;

        public float X { get { return Value.X; } set { Value.X = value; } }
        public float Y { get { return Value.Y; } set { Value.Y = value; } }
        public float Z { get { return Value.Z; } set { Value.Z = value; } }

        public Vector3W() { Value = new Vector3(); }
        public Vector3W(Vector3 vec) { Value = vec; }

        protected Vector3W(SerializationInfo info, StreamingContext context)
        {
            Value.X = info.GetSingle("X");
            Value.Y = info.GetSingle("Y");
            Value.Z = info.GetSingle("Z");
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("X", Value.X);
            info.AddValue("Y", Value.Y);
            info.AddValue("Z", Value.Z);
        }

        public override string ToString()
        {
            return $"X: {X:F2}, Y: {Y:F2}, Z: {Z:F2}";
        }
    }
    // --- 끝: 기존 Vector3W 클래스 ---


    // 예시: StarterNode (OpenCV와 직접 관련 없음)
    [Node(name: "시작점", menu: "흐름 제어", isExecutionInitiator: true, description: "워크플로우 처리 시작점입니다.")]
    public void StarterNode()
    {
        Console.WriteLine("스타터 노드 실행됨!"); // 디버그 콘솔 출력
        FeedbackInfo?.Invoke("시작 노드가 실행되었습니다.", CurrentProcessingNode, FeedbackType.Information, null, false);
    }

    // 수정된 ShowMessageNode
    [Node(name: "메시지 표시 요청", menu: "디버그", description: "입력된 메시지를 UI에 표시하도록 요청합니다.")]
    public void ShowMessageNode(string message) // [NodeInput(...)] 제거
    {
        // FeedbackInfo를 통해 MainForm에 메시지 표시를 요청합니다.
        // data 매개변수를 사용하여 MainForm에서 이 요청을 식별할 수 있도록 합니다.
        // "SHOW_MESSAGE_BOX_REQUEST"는 임의의 식별자(태그)입니다.
        // 실제 메시지 내용은 FeedbackInfo의 첫 번째 string 매개변수(message)로 전달됩니다.
        FeedbackInfo?.Invoke(message, CurrentProcessingNode, FeedbackType.Information, "SHOW_MESSAGE_BOX_REQUEST", false);
    }
}
