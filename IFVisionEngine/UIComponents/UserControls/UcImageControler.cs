using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using OpenCvSharp;

namespace IFVisionEngine.UIComponents.UserControls
{
    public partial class UcImageControler: UserControl
    {
        public UcImageControler()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Mat 객체를 받아 PictureBox에 이미지를 표시하는 공개 메서드입니다.
        /// </summary>
        /// <param name="image">표시할 이미지의 Mat 객체입니다.</param>
        public void DisplayImage(Mat image)
        {
            // 이미지가 null이거나 비어있으면 아무것도 하지 않습니다.
            if (image == null || image.Empty())
            {
                // 필요하다면 PictureBox를 비웁니다.
                // pBMain.Image = null; 
                return;
            }

            // 다른 스레드에서 UI 컨트롤을 업데이트하려고 할 경우를 대비한 안전장치입니다.
            if (pBMain.InvokeRequired)
            {
                pBMain.Invoke(new MethodInvoker(delegate {
                    // 이전에 표시된 이미지가 있다면 메모리에서 해제합니다.
                    pBMain.Image?.Dispose();
                    // Mat을 Bitmap으로 변환하여 PictureBox에 할당합니다.
                    pBMain.Image = BitmapConverter.ToBitmap(image);
                }));
            }
            else
            {
                pBMain.Image?.Dispose();
                pBMain.Image = BitmapConverter.ToBitmap(image);
            }
        }
    }
}
