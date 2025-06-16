using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IFVisionEngine.UIComponents.UserControls
{
    public partial class UcNodeSelectedView: UserControl
    {
        public UcNodeSelectedView()
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;
        }
        public void DisplayNowData(object dataObject)
        {
            // --- 디버깅 코드 시작 ---
            // Visual Studio의 '출력' 창에서 어떤 데이터가 들어오는지 확인합니다.
            if (dataObject != null)
            {
                // 데이터 객체의 타입 이름을 출력합니다.
                System.Diagnostics.Debug.WriteLine($"[UcNodeDataView] DisplayNowData received: Type = {dataObject.GetType().FullName}");
            }
            else
            {
                // 데이터가 null일 경우를 출력합니다.
                System.Diagnostics.Debug.WriteLine("[UcNodeDataView] DisplayNowData received: null");
            }
            // --- 디버깅 코드 끝 ---

            // 다른 스레드에서 이 메서드를 호출했는지 확인합니다.
            if (this.propertyGrid1.InvokeRequired)
            {
                // UI 스레드가 아니라면, UI 스레드에 작업을 위임(Invoke)합니다.
                this.propertyGrid1.Invoke(new MethodInvoker(() =>
                {
                    propertyGrid1.SelectedObject = dataObject;
                }));
            }
            else
            {
                // 이미 UI 스레드라면 직접 업데이트합니다.
                propertyGrid1.SelectedObject = dataObject;
            }
        }
    }
}
