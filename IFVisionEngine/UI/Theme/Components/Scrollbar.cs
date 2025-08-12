using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/// <summary>
/// 확실한 스크롤바 다크 테마 적용 클래스
/// </summary>
public static class ScrollbarTheme
{
    #region Windows API
    [DllImport("uxtheme.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);
    #endregion

    #region 핵심 메서드
    /// <summary>
    /// 컨트롤에 다크 스크롤바 적용 (핸들 체크 포함)
    /// </summary>
    public static bool ApplyDarkScrollbar(Control control)
    {
        if (control == null) return false;
        
        if (!control.IsHandleCreated)
        {
            // 핸들이 없으면 생성될 때까지 기다림
            control.HandleCreated += (s, e) => ApplyDarkScrollbar(s as Control);
            return false;
        }
        
        try
        {
            int result = SetWindowTheme(control.Handle, "DarkMode_Explorer", null);
            if (result == 0) 
            {
                control.Invalidate();
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// PropertyGrid와 모든 내부 컨트롤에 재귀적으로 테마 적용
    /// </summary>
    public static void ApplyDarkScrollbarRecursive(Control control)
    {
        if (control == null) return;

        // 현재 컨트롤에 테마 적용
        ApplyDarkScrollbar(control);

        // 모든 자식 컨트롤에도 적용
        foreach (Control child in control.Controls)
        {
            ApplyDarkScrollbarRecursive(child);
        }
    }

    /// <summary>
    /// PropertyGrid SelectedObject 설정 + 확실한 스크롤바 테마 유지
    /// </summary>
    public static void SetSelectedObjectWithDarkTheme(this PropertyGrid propertyGrid, object selectedObject)
    {
        if (propertyGrid == null) return;

        // 1. SelectedObject 설정
        propertyGrid.SelectedObject = selectedObject;

        // 2. 적용 시도
        ApplyDarkScrollbarRecursive(propertyGrid);

    }
    #endregion

    #region 퀵 적용
    /// <summary>
    /// PropertyGrid 다크 테마 적용 (확실한 버전)
    /// </summary>
    public static void DarkPropertyGrid(PropertyGrid propertyGrid)
    {
        if (propertyGrid == null) return;

        // 1. 색상 설정
        propertyGrid.BackColor = Color.FromArgb(35, 35, 35);
        propertyGrid.ForeColor = Color.FromArgb(220, 220, 220);
        propertyGrid.LineColor = Color.FromArgb(60, 60, 60);
        propertyGrid.CategoryForeColor = Color.FromArgb(200, 200, 200);
        propertyGrid.HelpBackColor = Color.FromArgb(30, 30, 30);
        propertyGrid.HelpForeColor = Color.FromArgb(220, 220, 220);
        
        // 2. 핸들 생성 확인 후 테마 적용
        if (propertyGrid.IsHandleCreated)
        {
            ApplyDarkScrollbarRecursive(propertyGrid);
        }
        else
        {
            propertyGrid.HandleCreated += (s, e) => {
                ApplyDarkScrollbarRecursive(s as PropertyGrid);
            };
        }

        // 3. 컨트롤이 추가될 때마다 테마 재적용
        propertyGrid.ControlAdded += (s, e) => {
            var timer = new Timer { Interval = 100 };
            timer.Tick += (sender, args) => {
                timer.Stop();
                timer.Dispose();
                ApplyDarkScrollbarRecursive(s as PropertyGrid);
            };
            timer.Start();
        };
    }

    /// <summary>
    /// ListView 다크 테마 적용
    /// </summary>
    public static void DarkListView(ListView listView)
    {
        if (listView == null) return;
        
        listView.BackColor = Color.FromArgb(35, 35, 35);
        listView.ForeColor = Color.FromArgb(220, 220, 220);
        
        ApplyDarkScrollbar(listView);
    }

    /// <summary>
    /// TreeView 다크 테마 적용
    /// </summary>
    public static void DarkTreeView(TreeView treeView)
    {
        if (treeView == null) return;
        
        treeView.BackColor = Color.FromArgb(35, 35, 35);
        treeView.ForeColor = Color.FromArgb(220, 220, 220);
        
        ApplyDarkScrollbar(treeView);
    }
    #endregion

}