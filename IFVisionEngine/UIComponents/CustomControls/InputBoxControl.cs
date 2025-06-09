using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

// Form 대신 UserControl을 상속받는 커스텀 입력 컨트롤입니다.
public class InputBoxControl : UserControl
{
    private Label lblPrompt;
    private TextBox txtInput;
    private Button btnOK;
    private Button btnCancel;

    // 외부에서 '확인', '취소' 버튼 클릭을 감지할 수 있도록 이벤트를 정의합니다.
    public event EventHandler OkClicked;
    public event EventHandler CancelClicked;

    // 사용자가 입력한 값을 저장하고 반환하기 위한 속성입니다.
    public string InputValue { get; private set; }

    public string Prompt
    {
        get => lblPrompt.Text;
        set => lblPrompt.Text = value;
    }

    public string DefaultValue
    {
        get => txtInput.Text;
        set
        {
            txtInput.Text = value;
            InputValue = value; // 초기 값 설정
        }
    }

    public InputBoxControl()
    {
        InitializeComponent();
    }

    // UI 컨트롤들을 코드로 직접 초기화합니다.
    private void InitializeComponent()
    {
        this.lblPrompt = new Label();
        this.txtInput = new TextBox();
        this.btnOK = new Button();
        this.btnCancel = new Button();
        this.SuspendLayout();

        this.lblPrompt.Location = new Point(12, 15);
        this.lblPrompt.Size = new Size(356, 23);
        this.lblPrompt.Name = "lblPrompt";

        this.txtInput.Location = new Point(12, 45);
        this.txtInput.Size = new Size(356, 20);
        this.txtInput.Name = "txtInput";
        this.txtInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        this.btnOK.Location = new Point(212, 85);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new Size(75, 23);
        this.btnOK.Text = "확인";

        this.btnCancel.Location = new Point(293, 85);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new Size(75, 23);
        this.btnCancel.Text = "취소";
        this.Controls.Add(this.lblPrompt);
        this.Controls.Add(this.txtInput);
        this.Controls.Add(this.btnOK);
        this.Controls.Add(this.btnCancel);

        this.btnOK.Click += (sender, e) => {
            this.InputValue = this.txtInput.Text;
            OkClicked?.Invoke(this, EventArgs.Empty);
        };

        this.btnCancel.Click += (sender, e) => {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        };
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
