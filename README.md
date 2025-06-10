# IFVisionEngine

🔀 Git 브랜치 전략 및 협업 규칙

본 프로젝트는 안정적인 협업과 코드 관리를 위해 다음과 같은 Git 브랜치 전략을 사용합니다.

---

## 🧩 브랜치 설명

| 브랜치 이름 | 설명 |
|---|---|
| `master` | 실제 운영 환경에 배포되는 코드가 존재하는 브랜치입니다. 직접 푸시 금지. |
| `dev` | 여러 기능을 통합해 테스트하는 브랜치입니다. QA 테스트는 이 브랜치 기준으로 진행됩니다. |
| `feature/{이름}-{기능}` | 개인 개발 브랜치입니다. 예: `feature/njb-login` |
| `bugfix/{이름}-{내용}` | 버그 수정 브랜치입니다. 예: `bugfix/yjh-validationpage` |

<br>

## ⚙️ UI 관리자 (AppUIManager) 규칙

본 프로젝트는 UI 컨트롤 간의 복잡한 상호작용을 관리하고 코드의 결합도를 낮추기 위해 중앙 관리자인 `AppUIManager`를 사용합니다. `AppUIManager` 사용 시 다음 규칙을 준수합니다.

### 1. 핵심 원칙

-   **중앙 집중 관리 (Mediator Pattern):** `AppUIManager`는 UI 컨트롤들 사이의 중재자 역할을 합니다. 컨트롤들은 서로를 직접 참조하지 않으며, 모든 상호작용은 `AppUIManager`를 통해 이루어집니다.
-   **전역 접근성 (Singleton Pattern):** `AppUIManager`는 정적(static) 클래스로 구현되어, 애플리케이션의 어느 곳에서나 단일 인스턴스에 접근할 수 있습니다.
-   **단일 책임 원칙 (SRP):** `Form1`과 같은 폼 클래스는 UI 요소들을 배치하고 `AppUIManager`를 초기화하는 책임만 가집니다. 실제 동작 로직과 이벤트 처리는 `AppUIManager`에 위임합니다.

### 2. 사용 규칙

-   **초기화:**
    -   `AppUIManager`는 반드시 메인 폼(`Form1`)의 생성자 또는 `Load` 이벤트에서 `AppUIManager.Initialize(this);` 와 같이 단 한 번만 초기화되어야 합니다.
    -   이 `Initialize` 메서드 안에서 모든 주요 UserControl(`UcNodeEditor`, `UcLogView` 등)이 생성됩니다.

-   **컨트롤 접근:**
    -   `Form1`에 배치되는 주요 UserControl들은 `AppUIManager`의 정적(static) 프로퍼티를 통해 접근합니다.
    -   예: `pnlBottom.Controls.Add(AppUIManager.ucLogView);`

-   **이벤트 처리:**
    -   노드 엔진(`MyNodesContext`)에서 발생하는 `FeedbackInfo`와 같은 이벤트들은 `AppUIManager` 내부의 `OnNodeFeedbackReceived` 이벤트 핸들러에서 중앙 처리됩니다.
    -   개별 노드나 컨트롤에서 다른 컨트롤의 UI를 직접 업데이트해야 할 경우, 직접 참조하는 대신 `FeedbackInfo` 이벤트를 발생시켜 `AppUIManager`에 작업을 요청하는 것을 원칙으로 합니다.

### 3. 예시 코드

**Form1.cs - 초기화**

```csharp
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        
        // AppUIManager를 초기화하고 필요한 컨트롤들을 생성 및 연결합니다.
        AppUIManager.Initialize(this);

        // UIManager가 생성한 컨트롤들을 폼의 패널에 배치합니다.
        this.pnlTop.Controls.Add(AppUIManager.ucNodeEditor);
        this.pnlBottom.Controls.Add(AppUIManager.ucLogView);

        // Dock 속성 설정
        AppUIManager.ucNodeEditor.Dock = DockStyle.Fill;
        AppUIManager.ucLogView.Dock = DockStyle.Fill;
    }
}
