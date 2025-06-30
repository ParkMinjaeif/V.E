using System.Collections.Generic;
using OpenCvSharp;

namespace IFVisionEngine.Manager
{
    /// <summary>
    /// 노드 실행 중에 생성되는 Mat 객체들을 중앙에서 관리하는 정적 클래스입니다.
    /// </summary>
    public static class ImageDataManager
    {
        // Key: NodeVisual의 고유 ID (GUID), Value: 해당 노드가 생성한 Mat 객체
        private static readonly Dictionary<string, Mat> _imageStore = new Dictionary<string, Mat>();

        /// <summary>
        /// 지정된 노드 ID와 함께 Mat 객체를 등록합니다.
        /// 이미 동일한 ID로 등록된 이미지가 있다면, 이전 이미지는 메모리에서 해제하고 새로 교체합니다.
        /// </summary>
        /// <param name="nodeId">이미지를 생성한 노드의 고유 ID (GUID)</param>
        /// <param name="image">등록할 Mat 객체</param>
        public static void RegisterImage(string nodeId, Mat image)
        {
            if (string.IsNullOrEmpty(nodeId) || image == null) return;

            // 기존에 이미지가 있다면 메모리 누수 방지를 위해 해제
            if (_imageStore.TryGetValue(nodeId, out Mat oldImage))
            {
                oldImage?.Dispose();
            }
            // 복제본을 저장하여 원본과의 참조 문제를 방지합니다.
            _imageStore[nodeId] = image.Clone();
        }

        /// <summary>
        /// 지정된 노드 ID에 해당하는 Mat 객체를 가져옵니다.
        /// </summary>
        /// <param name="nodeId">찾을 이미지의 노드 ID (GUID)</param>
        /// <returns>찾은 Mat 객체, 없으면 null을 반환합니다.</returns>
        public static Mat GetImage(string nodeId)
        {
            if (string.IsNullOrEmpty(nodeId)) return null;

            _imageStore.TryGetValue(nodeId, out Mat image);
            return image;
        }

        /// <summary>
        /// 저장된 모든 이미지 데이터를 메모리에서 해제하고 목록을 비웁니다.
        /// </summary>
        public static void Clear()
        {
            foreach (var mat in _imageStore.Values)
            {
                mat?.Dispose();
            }
            _imageStore.Clear();
        }
    }
}
