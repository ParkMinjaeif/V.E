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

namespace IFVisionEngine.UIComponents.UserControls
{
    public partial class UcLogView: UserControl
    {
        private Form1 _formMainInstance;

        public UcLogView(Form1 mainForm)
        {
            InitializeComponent();
            _formMainInstance = mainForm;

            this.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// 외부에서 로그를 추가하기 위한 공개 메서드입니다.
        /// </summary>
        /// <param name="type">로그의 종류 (Information, Warning, Error 등)</param>
        /// <param name="message">로그 메시지 내용</param>
        public void AddLog(FeedbackType type, string message)
        {
            // UI 스레드에서 실행되도록 보장하여 스레드 충돌을 방지합니다.
            if (this.lvwLog.InvokeRequired)
            {
                this.lvwLog.Invoke(new MethodInvoker(() => AddListViewItem(type, message)));
            }
            else
            {
                AddListViewItem(type, message);
            }
        }

        // 실제 ListView에 아이템을 추가하는 내부 메서드입니다.
        private void AddListViewItem(FeedbackType type, string message)
        {
            // ListViewItem 객체를 생성합니다. 첫 번째 컬럼(발생시간)의 내용입니다.
            ListViewItem item = new ListViewItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            // 나머지 컬럼(SubItems)을 추가합니다.
            item.SubItems.Add(type.ToString()); // 종류
            item.SubItems.Add(message);         // 내용

            // 종류(Type)에 따라 글자색을 변경합니다.
            switch (type)
            {
                case FeedbackType.Error:
                    item.ForeColor = Color.Red;
                    break;
                case FeedbackType.Warning:
                    item.ForeColor = Color.Orange;
                    break;
            }

            // ListView의 맨 위에 새 로그를 추가합니다.
            this.lvwLog.Items.Insert(0, item);
        }

        private void uiSymbolButton_toggle_Click(object sender, EventArgs e)
        {
            _formMainInstance.togglePnlBottom();
        }
    }
}
