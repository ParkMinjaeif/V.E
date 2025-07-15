using System;
using System.Drawing;
using System.Windows.Forms;
using IFVisionEngine.UIComponents.Enums;

namespace IFVisionEngine.UIComponents.Common
{
    /// <summary>
    /// 크기 조절 관련 공통 유틸리티 클래스
    /// CustomTitleBar와 WindowWrapper에서 공통으로 사용되는 크기 조절 로직을 제공합니다.
    /// </summary>
    public static class ResizeHelper
    {
        #region Constants

        /// <summary>크기 조절 감지 영역의 픽셀 두께</summary>
        public const int RESIZE_BORDER_WIDTH = 8;

        #endregion

        #region Cursor Management

        /// <summary>
        /// 크기 조절 방향에 맞는 커서를 반환합니다.
        /// </summary>
        /// <param name="direction">크기 조절 방향</param>
        /// <returns>해당 방향의 커서</returns>
        public static Cursor GetCursorForDirection(ResizeDirection direction)
        {
            switch (direction)
            {
                case ResizeDirection.Top:
                case ResizeDirection.Bottom:
                    return Cursors.SizeNS;
                case ResizeDirection.Left:
                case ResizeDirection.Right:
                    return Cursors.SizeWE;
                case ResizeDirection.TopLeft:
                case ResizeDirection.BottomRight:
                    return Cursors.SizeNWSE;
                case ResizeDirection.TopRight:
                case ResizeDirection.BottomLeft:
                    return Cursors.SizeNESW;
                default:
                    return Cursors.Default;
            }
        }

        #endregion

        #region Direction Detection

        /// <summary>
        /// 마우스 위치에 따른 크기 조절 방향을 결정합니다. (일반 컨트롤용)
        /// </summary>
        /// <param name="mousePos">마우스 위치 (컨트롤 상대 좌표)</param>
        /// <param name="controlSize">컨트롤 크기</param>
        /// <param name="titleBarHeight">타이틀바 높이 (타이틀바 영역 제외용, 0이면 제외 안함)</param>
        /// <returns>크기 조절 방향</returns>
        public static ResizeDirection GetResizeDirection(Point mousePos, Size controlSize, int titleBarHeight = 0)
        {
            bool left = mousePos.X <= RESIZE_BORDER_WIDTH;
            bool right = mousePos.X >= controlSize.Width - RESIZE_BORDER_WIDTH;
            bool top = mousePos.Y <= RESIZE_BORDER_WIDTH;
            bool bottom = mousePos.Y >= controlSize.Height - RESIZE_BORDER_WIDTH;

            // 타이틀바 영역은 제외 (드래그 이동과 충돌 방지)
            if (titleBarHeight > 0 && mousePos.Y <= titleBarHeight && !top)
            {
                return ResizeDirection.None;
            }

            // 코너 우선 체크 (대각선 크기 조절)
            if (top && left) return ResizeDirection.TopLeft;
            if (top && right) return ResizeDirection.TopRight;
            if (bottom && left) return ResizeDirection.BottomLeft;
            if (bottom && right) return ResizeDirection.BottomRight;

            // 가장자리 체크 (단방향 크기 조절)
            if (top) return ResizeDirection.Top;
            if (bottom) return ResizeDirection.Bottom;
            if (left) return ResizeDirection.Left;
            if (right) return ResizeDirection.Right;

            return ResizeDirection.None;
        }

        /// <summary>
        /// 폼용 크기 조절 방향을 결정합니다. (타이틀바 영역 제외 로직 포함)
        /// </summary>
        /// <param name="mousePos">마우스 위치 (폼 상대 좌표)</param>
        /// <param name="form">대상 폼</param>
        /// <param name="titleBarHeight">타이틀바 높이</param>
        /// <returns>크기 조절 방향</returns>
        public static ResizeDirection GetResizeDirectionForForm(Point mousePos, Form form, int titleBarHeight = 40)
        {
            // 최대화 상태에서는 크기 조절 불가
            if (form.WindowState != FormWindowState.Normal)
                return ResizeDirection.None;

            // 타이틀바 영역은 제외 (드래그 이동과 충돌 방지)
            if (mousePos.Y > 0 && mousePos.Y <= titleBarHeight && mousePos.Y > RESIZE_BORDER_WIDTH)
                return ResizeDirection.None;

            return GetResizeDirection(mousePos, form.Size, titleBarHeight);
        }

        #endregion

        #region Bounds Calculation

        /// <summary>
        /// 델타 값을 기반으로 새로운 경계를 계산합니다.
        /// </summary>
        /// <param name="direction">크기 조절 방향</param>
        /// <param name="deltaX">X축 이동 거리</param>
        /// <param name="deltaY">Y축 이동 거리</param>
        /// <param name="startLocation">크기 조절 시작 위치</param>
        /// <param name="startSize">크기 조절 시작 크기</param>
        /// <param name="minWidth">최소 너비</param>
        /// <param name="minHeight">최소 높이</param>
        /// <returns>새로운 경계 Rectangle</returns>
        public static Rectangle CalculateNewBounds(
            ResizeDirection direction,
            int deltaX, int deltaY,
            Point startLocation, Size startSize,
            int minWidth, int minHeight)
        {
            int newX = startLocation.X;
            int newY = startLocation.Y;
            int newWidth = startSize.Width;
            int newHeight = startSize.Height;

            switch (direction)
            {
                case ResizeDirection.Right:
                    newWidth = Math.Max(minWidth, startSize.Width + deltaX);
                    break;

                case ResizeDirection.Left:
                    var leftResize = CalculateLeftResize(deltaX, startLocation, startSize, minWidth);
                    newWidth = leftResize.newWidth;
                    newX = leftResize.newX;
                    break;

                case ResizeDirection.Bottom:
                    newHeight = Math.Max(minHeight, startSize.Height + deltaY);
                    break;

                case ResizeDirection.Top:
                    var topResize = CalculateTopResize(deltaY, startLocation, startSize, minHeight);
                    newHeight = topResize.newHeight;
                    newY = topResize.newY;
                    break;

                case ResizeDirection.BottomRight:
                    newWidth = Math.Max(minWidth, startSize.Width + deltaX);
                    newHeight = Math.Max(minHeight, startSize.Height + deltaY);
                    break;

                case ResizeDirection.BottomLeft:
                    var bottomLeftWidth = CalculateLeftResize(deltaX, startLocation, startSize, minWidth);
                    newWidth = bottomLeftWidth.newWidth;
                    newX = bottomLeftWidth.newX;
                    newHeight = Math.Max(minHeight, startSize.Height + deltaY);
                    break;

                case ResizeDirection.TopRight:
                    newWidth = Math.Max(minWidth, startSize.Width + deltaX);
                    var topRightHeight = CalculateTopResize(deltaY, startLocation, startSize, minHeight);
                    newHeight = topRightHeight.newHeight;
                    newY = topRightHeight.newY;
                    break;

                case ResizeDirection.TopLeft:
                    var topLeftWidth = CalculateLeftResize(deltaX, startLocation, startSize, minWidth);
                    var topLeftHeight = CalculateTopResize(deltaY, startLocation, startSize, minHeight);
                    newWidth = topLeftWidth.newWidth;
                    newHeight = topLeftHeight.newHeight;
                    newX = topLeftWidth.newX;
                    newY = topLeftHeight.newY;
                    break;
            }

            return new Rectangle(newX, newY, newWidth, newHeight);
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// 왼쪽 크기 조절 계산을 수행합니다.
        /// </summary>
        /// <param name="deltaX">X축 이동 거리</param>
        /// <param name="startLocation">시작 위치</param>
        /// <param name="startSize">시작 크기</param>
        /// <param name="minWidth">최소 너비</param>
        /// <returns>새로운 너비와 X 위치</returns>
        private static (int newWidth, int newX) CalculateLeftResize(int deltaX, Point startLocation, Size startSize, int minWidth)
        {
            int widthChange = Math.Max(minWidth - startSize.Width, -deltaX);
            int newWidth = startSize.Width + widthChange;
            int newX = startLocation.X - widthChange;
            return (newWidth, newX);
        }

        /// <summary>
        /// 상단 크기 조절 계산을 수행합니다.
        /// </summary>
        /// <param name="deltaY">Y축 이동 거리</param>
        /// <param name="startLocation">시작 위치</param>
        /// <param name="startSize">시작 크기</param>
        /// <param name="minHeight">최소 높이</param>
        /// <returns>새로운 높이와 Y 위치</returns>
        private static (int newHeight, int newY) CalculateTopResize(int deltaY, Point startLocation, Size startSize, int minHeight)
        {
            int heightChange = Math.Max(minHeight - startSize.Height, -deltaY);
            int newHeight = startSize.Height + heightChange;
            int newY = startLocation.Y - heightChange;
            return (newHeight, newY);
        }

        #endregion
    }
}