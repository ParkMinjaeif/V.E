using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NodeEditor;

public partial class MyNodesContext
{
    /// <summary>
    /// 이 노드는 실행되면 대화상자를 띄워 사용자에게 문자열을 입력받고,
    /// 그 결과를 'outputValue' 출력 핀으로 내보냅니다.
    /// </summary>
    /// <param name="outputValue">사용자가 대화상자에 입력한 문자열이 이 출력 매개변수를 통해 전달됩니다.</param>
    [Node(
        name: "문자열 입력 대화상자",
        menu: "입력",
        description: "대화상자를 열어 사용자에게 직접 문자열을 입력받습니다."
    )]
    public void GetStringFromDialog(out string outputValue)
    {
        // 1. 대화상자 역할을 할 임시 폼(Form)을 생성합니다.
        using (var dialogForm = new Form())
        {
            // 2. 1단계에서 만든 InputBoxControl의 인스턴스를 생성합니다.
            var inputBox = new InputBoxControl();

            // 3. UserControl과 Form의 속성을 설정합니다.
            inputBox.Prompt = "값을 입력하고 '확인'을 누르세요:";
            inputBox.Dock = DockStyle.Fill;
            dialogForm.Text = "사용자 입력";
            dialogForm.ClientSize = new Size(380, 130);
            dialogForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialogForm.StartPosition = FormStartPosition.CenterParent;
            dialogForm.MaximizeBox = false;
            dialogForm.MinimizeBox = false;

            // 4. UserControl의 이벤트가 발생하면 Form의 DialogResult를 설정해 폼이 닫히도록 연결합니다.
            inputBox.OkClicked += (s, ev) => dialogForm.DialogResult = DialogResult.OK;
            inputBox.CancelClicked += (s, ev) => dialogForm.DialogResult = DialogResult.Cancel;

            // 5. Form에 UserControl을 추가하고 Enter/ESC 키를 연결합니다.
            dialogForm.Controls.Add(inputBox);
            Button okButton = inputBox.Controls.Find("btnOK", true).FirstOrDefault() as Button;
            if (okButton != null)
            {
                dialogForm.AcceptButton = okButton;
            }

            // 6. 대화상자를 띄우고 결과를 확인합니다.
            if (dialogForm.ShowDialog() == DialogResult.OK)
            {
                // '확인'을 눌렀다면, 입력된 값을 출력 매개변수에 할당합니다.
                outputValue = inputBox.InputValue;
            }
            else
            {
                // '취소'를 눌렀다면, 빈 문자열을 할당합니다.
                outputValue = "";
            }
        }
    }

    /// <summary>
    /// *** 새로 추가된 노드 ***
    /// 이 노드는 실행되면 파일 열기 대화상자를 띄워 사용자에게 이미지 파일을 선택받고,
    /// 그 파일의 전체 경로를 'selectedFilePath' 출력 핀으로 내보냅니다.
    /// </summary>
    /// <param name="selectedFilePath">사용자가 선택한 이미지 파일의 전체 경로가 이 출력 매개변수를 통해 전달됩니다.</param>
    [Node(
        name: "이미지 파일 선택",
        menu: "입력",
        description: "파일 대화상자를 열어 이미지 파일(*.jpg, *.png, *.bmp)을 선택합니다."
    )]
    public void GetImageFilePathFromDialog(out string selectedFilePath)
    {
        using (var openFileDialog = new OpenFileDialog())
        {
            // 파일 필터를 설정하여 특정 이미지 파일 형식만 표시합니다.
            openFileDialog.Filter = "Image Files(*.jpg; *.png; *.bmp)|*.jpg;*.png;*.bmp|All files (*.*)|*.*";
            openFileDialog.Title = "이미지 파일 선택";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            // 대화상자를 띄우고 사용자가 '열기'를 눌렀는지 확인합니다.
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 선택된 파일의 전체 경로를 출력 매개변수에 할당합니다.
                selectedFilePath = openFileDialog.FileName;
            }
            else
            {
                // '취소'를 눌렀다면, 빈 문자열을 할당합니다.
                selectedFilePath = "";
            }
        }
        Console.WriteLine($"FilePath : {selectedFilePath}");
    }
}
