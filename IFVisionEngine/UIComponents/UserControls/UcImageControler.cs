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
using IFVisionEngine.Manager;
using System.Drawing.Drawing2D;

namespace IFVisionEngine.UIComponents.UserControls
{
    public partial class UcImageControler: UserControl
    {
        private Form1 _formMainInstance;

        // --- 줌/패닝 기능을 위한 멤버 변수 선언 ---
        private Bitmap _currentBitmap;      // 원본 비트맵 이미지
        private float _zoomFactor = 1.0f;   // 현재 확대/축소 비율
        private PointF _imageOffset;        // 이미지의 시작 위치 (패닝 오프셋)
        private bool _isPanning = false;    // 현재 패닝 중인지 여부 (마우스 오른쪽 버튼)
        private System.Drawing.Point _lastMousePosition;   // 마지막 마우스 위치 (패닝 계산용)

        public UcImageControler(Form1 mainForm)
        {
            InitializeComponent();
            _formMainInstance = mainForm;
            this.Dock = DockStyle.Fill;

            // pnlBot에 ucLogView를 추가하는 로직은 Form1이나 AppUIManager에서 처리하는 것이 더 적합할 수 있습니다.
            this.pnlBot.Controls.Add(AppUIManager.ucLogView);

            // 부드러운 이미지 리사이징과 드래그를 위한 설정
            this.DoubleBuffered = true;

            // PictureBox 이벤트 핸들러 연결
            this.pBMain.Paint += pBMain_Paint;
            this.pBMain.MouseWheel += pBMain_MouseWheel;
            this.pBMain.MouseDown += pBMain_MouseDown;
            this.pBMain.MouseMove += pBMain_MouseMove;
            this.pBMain.MouseUp += pBMain_MouseUp;
        }

        /// <summary>
        /// Mat 객체를 받아 비트맵으로 변환하고, 뷰를 초기화하며, 화면을 갱신합니다.
        /// </summary>
        public void DisplayImage(Mat image)
        {
            Console.WriteLine("Activate Display Image");
            if (image == null || image.Empty())
            {
                _currentBitmap?.Dispose();
                _currentBitmap = null;
                pBMain.Invalidate();
                return;
            }

            _currentBitmap?.Dispose();
            _currentBitmap = BitmapConverter.ToBitmap(image);

            // PictureBox의 Image 속성을 사용하지 않으므로 null로 설정하여 오동작을 방지합니다.
            if (pBMain.Image != null)
            {
                pBMain.Image.Dispose();
                pBMain.Image = null;
            }

            // 새 이미지가 로드되면 뷰를 초기화합니다.
            ResetImageView();
        }

        /// <summary>
        /// 줌과 위치를 기본값으로 리셋하고 화면을 다시 그립니다.
        /// </summary>
        private void ResetImageView()
        {
            if (_currentBitmap == null) return;

            // 1. 이미지를 컨트롤에 꽉 채우는 'Best Fit' 줌 비율을 계산합니다.
            float zoomX = (float)pBMain.ClientSize.Width / _currentBitmap.Width;
            float zoomY = (float)pBMain.ClientSize.Height / _currentBitmap.Height;
            _zoomFactor = Math.Min(zoomX, zoomY);

            // 2. 이미지가 중앙에 위치하도록 오프셋을 계산합니다.
            float newWidth = _currentBitmap.Width * _zoomFactor;
            float newHeight = _currentBitmap.Height * _zoomFactor;
            _imageOffset = new PointF(
                (pBMain.ClientSize.Width - newWidth) / 2f,
                (pBMain.ClientSize.Height - newHeight) / 2f
            );

            pBMain.Invalidate(); // PictureBox를 다시 그리도록 요청합니다.
        }

        #region 줌/패닝 이벤트 핸들러

        /// <summary>
        /// PictureBox를 다시 그려야 할 때마다 호출됩니다. 줌/패닝의 핵심 로직입니다.
        /// </summary>
        private void pBMain_Paint(object sender, PaintEventArgs e)
        {
            if (_currentBitmap == null)
            {
                // 이미지가 없으면 배경을 깨끗하게 지웁니다.
                e.Graphics.Clear(pBMain.BackColor);
                return;
            }

            // 고품질 이미지 리사이징을 위한 보간 모드 설정
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // 현재 패닝 위치(Offset)와 줌 비율을 그래픽스 객체에 적용합니다.
            e.Graphics.TranslateTransform(_imageOffset.X, _imageOffset.Y);
            e.Graphics.ScaleTransform(_zoomFactor, _zoomFactor);

            // 최종적으로 변환이 적용된 이미지를 그립니다.
            e.Graphics.DrawImage(_currentBitmap, 0, 0);
        }

        /// <summary>
        /// 마우스 휠을 스크롤하여 이미지를 확대/축소합니다.
        /// </summary>
        private void pBMain_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_currentBitmap == null) return;

            float oldZoom = _zoomFactor;
            if (e.Delta > 0)
            {
                _zoomFactor *= 1.2f;
            }
            else
            {
                _zoomFactor /= 1.2f;
            }

            // 줌 비율 제한 (너무 작거나 커지지 않도록)
            _zoomFactor = Math.Max(0.1f, Math.Min(_zoomFactor, 20.0f));

            // 마우스 커서 위치를 중심으로 확대/축소되도록 오프셋을 조정합니다.
            PointF mousePos = e.Location;
            float newX = mousePos.X - (mousePos.X - _imageOffset.X) * (_zoomFactor / oldZoom);
            float newY = mousePos.Y - (mousePos.Y - _imageOffset.Y) * (_zoomFactor / oldZoom);
            _imageOffset = new PointF(newX, newY);

            pBMain.Invalidate();
        }

        /// <summary>
        /// 마우스 오른쪽 버튼을 누르면 패닝을 시작합니다.
        /// </summary>
        private void pBMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _isPanning = true;
                _lastMousePosition = e.Location;
                this.Cursor = Cursors.Hand; // 커서 모양을 손 모양으로 변경
            }
        }

        /// <summary>
        /// 마우스를 움직이는 동안 패닝을 수행합니다.
        /// </summary>
        private void pBMain_MouseMove(object sender, MouseEventArgs e)
        {
            // 패닝 로직 (마우스 오른쪽 버튼)
            if (_isPanning)
            {
                int dx = e.Location.X - _lastMousePosition.X;
                int dy = e.Location.Y - _lastMousePosition.Y;
                _imageOffset.X += dx;
                _imageOffset.Y += dy;
                _lastMousePosition = e.Location;
                pBMain.Invalidate();
            }

            // *** 새로 추가된 부분: 좌표 및 픽셀 값 업데이트 로직 ***
            if (_currentBitmap == null) return;

            // 1. 역변환: 화면 좌표(e.Location) -> 실제 이미지 좌표
            int imgX = (int)((e.X - _imageOffset.X) / _zoomFactor);
            int imgY = (int)((e.Y - _imageOffset.Y) / _zoomFactor);

            // 2. 좌표가 이미지 경계 내에 있는지 확인
            if (imgX >= 0 && imgX < _currentBitmap.Width && imgY >= 0 && imgY < _currentBitmap.Height)
            {
                // 3. 픽셀 값 가져오기
                Color pixelColor = _currentBitmap.GetPixel(imgX, imgY);

                // 4. 상태 표시줄 레이블 업데이트
                lbl_ordinate.Text = $"X: {imgX}, Y: {imgY}";
                lbl_PixelValue.Text = $"R: {pixelColor.R}, G: {pixelColor.G}, B: {pixelColor.B}";   
            }
            else
            {
                // 마우스가 이미지 밖에 있을 경우 레이블 비우기
                lbl_ordinate.Text = "X: -, Y: -";
                lbl_PixelValue.Text = "R: -, G: -, B: -";
            }
        }

        /// <summary>
        /// 마우스 오른쪽 버튼을 떼면 패닝을 중지합니다.
        /// </summary>
        private void pBMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _isPanning = false;
                this.Cursor = Cursors.Default; // 커서 모양을 원래대로 복원
            }
        }

        #endregion
    }
}
