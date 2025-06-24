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
    public partial class UcImageControler : UserControl
    {
        // 1. 메인 폼 참조
        private Form1 _formMainInstance;

        // 2. 이미지 및 뷰포트 상태 변수
        private Bitmap _currentBitmap;      // 원본 비트맵 이미지
        private float _zoomFactor = 1.0f;   // 현재 확대/축소 비율
        private PointF _imageOffset;        // 이미지 시작 위치(패닝 오프셋)
        private bool _isPanning = false;    // 현재 패닝 중인지 여부 (마우스 오른쪽 버튼)
        private System.Drawing.Point _lastMousePosition;   // 마지막 마우스 위치 (패닝 계산용)

        // 3. 드래그 확대/모드 상태
        private MouseMode _mouseMode = MouseMode.None;
        private PointF _dragStart;
        private PointF _dragEnd;
        private bool _isDragging = false;

        // 4. 임시 사각형/오버레이 변수 (추후 확장 가능)
        private Rectangle _zoomRect;

        public enum MouseMode { None, DragZoom }

        /// <summary>
        /// 생성자 - 메인폼 참조, 컨트롤 Dock, 이벤트 핸들러 및 부드러운 화면 설정
        /// </summary>
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

        #region 이미지 표시 및 뷰 상태 초기화

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

            // 고품질 리사이즈
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // 1. 패닝/확대 적용
            e.Graphics.TranslateTransform(_imageOffset.X, _imageOffset.Y);
            e.Graphics.ScaleTransform(_zoomFactor, _zoomFactor);
            e.Graphics.DrawImage(_currentBitmap, 0, 0);

            // 2. 변환 해제 후 오버레이(드래그 박스 등) 원래 좌표계로 시각화
            e.Graphics.ResetTransform();
            if (_mouseMode == MouseMode.DragZoom && _isDragging)
            {
                Rectangle rect = GetRectangle(_dragStart, _dragEnd);
                using (Pen pen = new Pen(Color.Red, 2))
                    e.Graphics.DrawRectangle(pen, rect);
            }
        }

        /// <summary>
        /// 마우스 휠로 확대/축소 (커서 기준으로 줌)
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
            _zoomFactor = Math.Max(0.1f, Math.Min(_zoomFactor, 20.0f)); // 제한

            // 커서 위치를 기준으로 오프셋 재계산
            PointF mousePos = e.Location;
            float newX = mousePos.X - (mousePos.X - _imageOffset.X) * (_zoomFactor / oldZoom);
            float newY = mousePos.Y - (mousePos.Y - _imageOffset.Y) * (_zoomFactor / oldZoom);
            _imageOffset = new PointF(newX, newY);

            pBMain.Invalidate();
        }

        /// <summary>
        /// 마우스 버튼 누름 - 드래그 확대/패닝 시작
        /// </summary>
        private void pBMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (_mouseMode == MouseMode.DragZoom && e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStart = e.Location;
                _dragEnd = e.Location;
            }
            else if (_mouseMode == MouseMode.None && e.Button == MouseButtons.Right)
            {
                _isPanning = true;
                _lastMousePosition = e.Location;
                this.Cursor = Cursors.Hand; // 손 모양 커서
            }
        }

        /// <summary>
        /// 마우스 이동 - 드래그 확대 영역 지정, 패닝 이동
        /// </summary>
        private void pBMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseMode == MouseMode.DragZoom && _isDragging)
            {
                _dragEnd = e.Location;
                pBMain.Invalidate(); // 사각형 시각화
            }
            else if (_mouseMode == MouseMode.None && e.Button == MouseButtons.Right && _isPanning)
            {
                int dx = e.Location.X - _lastMousePosition.X;
                int dy = e.Location.Y - _lastMousePosition.Y;
                _imageOffset.X += dx;
                _imageOffset.Y += dy;
                _lastMousePosition = e.Location;
                pBMain.Invalidate();
            }

            // 상태바 표시: 마우스 위치, 픽셀값
            if (_currentBitmap == null) return;
            int imgX = (int)((e.X - _imageOffset.X) / _zoomFactor);
            int imgY = (int)((e.Y - _imageOffset.Y) / _zoomFactor);
            if (imgX >= 0 && imgX < _currentBitmap.Width && imgY >= 0 && imgY < _currentBitmap.Height)
            {
                Color pixelColor = _currentBitmap.GetPixel(imgX, imgY);
                lbl_ordinate.Text = $"X: {imgX}, Y: {imgY}";
                lbl_PixelValue.Text = $"R: {pixelColor.R}, G: {pixelColor.G}, B: {pixelColor.B}";
            }
            else
            {
                lbl_ordinate.Text = "X: -, Y: -";
                lbl_PixelValue.Text = "R: -, G: -, B: -";
            }
        }

        /// <summary>
        /// 마우스 버튼 뗌 - 드래그 확대 적용, 패닝 종료
        /// </summary>
        private void pBMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (_mouseMode == MouseMode.DragZoom && _isDragging)
            {
                _isDragging = false;
                _dragEnd = e.Location;
                Rectangle dragRect = GetRectangle(_dragStart, _dragEnd);

                // 1. 드래그 영역(PictureBox좌표→원본 이미지좌표 변환)
                float imgX = (dragRect.X - _imageOffset.X) / _zoomFactor;
                float imgY = (dragRect.Y - _imageOffset.Y) / _zoomFactor;
                float imgW = dragRect.Width / _zoomFactor;
                float imgH = dragRect.Height / _zoomFactor;
                RectangleF imgRect = new RectangleF(imgX, imgY, imgW, imgH);

                // 2. 픽쳐박스 비율에 맞춰 최소 포함 사각형으로 확장
                float boxAspect = (float)pBMain.ClientSize.Width / pBMain.ClientSize.Height;
                RectangleF fitRect = ExpandToFitAspect(imgRect, boxAspect);

                // 3. 확대비율 및 오프셋 재계산 (뷰포트 방식)
                float zoom = (float)pBMain.ClientSize.Width / fitRect.Width;
                if (fitRect.Height * zoom > pBMain.ClientSize.Height)
                    zoom = (float)pBMain.ClientSize.Height / fitRect.Height;
                _zoomFactor = zoom;

                float newWidth = fitRect.Width * _zoomFactor;
                float newHeight = fitRect.Height * _zoomFactor;
                _imageOffset = new PointF(
                    (pBMain.ClientSize.Width - newWidth) / 2f - fitRect.X * _zoomFactor,
                    (pBMain.ClientSize.Height - newHeight) / 2f - fitRect.Y * _zoomFactor
                );

                _mouseMode = MouseMode.None;
                Cursor = Cursors.Default;
                pBMain.Invalidate();
            }
            else if (_mouseMode == MouseMode.None && e.Button == MouseButtons.Right)
            {
                _isPanning = false;
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region 기능/툴버튼 이벤트

        /// <summary>
        /// '드래그 확대' 버튼 클릭 - 드래그 확대 모드로 진입
        /// </summary>
        private void toolStripButton_Drag_to_Zoom_Click(object sender, EventArgs e)
        {
            _mouseMode = MouseMode.DragZoom;
            Cursor = Cursors.Cross; // 드래그용 커서
        }

        /// <summary>
        /// '확대/축소 리셋' 버튼 클릭 - 줌/오프셋을 초기화
        /// </summary>
        private void toolStripButton_Reset_Click(object sender, EventArgs e)
        {
            ResetImageView();
            pBMain.Invalidate();
        }

        /// <summary>
        /// '오버레이 초기화' 버튼 클릭 - 현재 이미지를 삭제(초기화)
        /// </summary>
        private void toolStripButton_Delete_Click(object sender, EventArgs e)
        {
            ResetOverlay();
        }

        #endregion

        #region 유틸리티 함수(사각형, 확대, 비율계산 등)

        /// <summary>
        /// 두 좌표로 사각형(Rectangle) 생성(PictureBox 좌표계)
        /// </summary>
        private Rectangle GetRectangle(PointF p1, PointF p2)
        {
            return new Rectangle(
                (int)Math.Min(p1.X, p2.X),
                (int)Math.Min(p1.Y, p2.Y),
                (int)Math.Abs(p1.X - p2.X),
                (int)Math.Abs(p1.Y - p2.Y));
        }

        /// <summary>
        /// 주어진 사각형을 지정한 비율(targetAspect)에 맞게 '포함하는' 최소 사각형으로 확장
        /// </summary>
        private RectangleF ExpandToFitAspect(RectangleF rect, float targetAspect)
        {
            float cx = rect.X + rect.Width / 2f;
            float cy = rect.Y + rect.Height / 2f;
            float rectAspect = rect.Width / rect.Height;

            float newW, newH;
            if (rectAspect > targetAspect)
            {
                // 더 넓을 때: 세로를 늘려 비율 맞춤
                newW = rect.Width;
                newH = rect.Width / targetAspect;
            }
            else
            {
                // 더 높을 때: 가로를 늘려 비율 맞춤
                newH = rect.Height;
                newW = rect.Height * targetAspect;
            }
            return new RectangleF(
                cx - newW / 2f,
                cy - newH / 2f,
                newW,
                newH
            );
        }

        /// <summary>
        /// 오버레이/이미지 모두 초기화(삭제) 및 상태바 리셋
        /// </summary>
        private void ResetOverlay()
        {
            if (_currentBitmap != null)
            {
                _currentBitmap.Dispose();
                _currentBitmap = null;
            }
            lbl_ordinate.Text = "X: -, Y: -";
            lbl_PixelValue.Text = "R: -, G: -, B: -";
            pBMain.Invalidate();
        }

        #endregion
    }
}
