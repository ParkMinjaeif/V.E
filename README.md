# IF VisionEngine


## 📜 프로젝트 개요

본 프로젝트는 복잡한 머신 비전 알고리즘을 코딩 없이 시각적인 노드 기반 환경에서 손쉽게 설계하고 실행할 수 있는 통합 비전 솔루션 개발을 목표로 합니다.

-   **(1.1) 프로젝트 목표:** 비전 검사 자동화 프로세스의 개발 장벽을 낮추고, 전문가가 아니더라도 복잡한 검사 로직을 구현할 수 있는 환경을 제공합니다.
-   **(1.2) 기대 효과:** 개발 시간 단축, 유지보수 비용 절감, 비전 시스템 도입의 대중화를 기대합니다.
-   **(1.3) 주요 사용자 타겟:** 머신 비전 엔지니어, 공장 자동화 담당자, 관련 연구 및 교육 분야 종사자.

<br>

## ✨ 핵심 기능

-   **(2.1) Visual Node Editor:** 코딩 없이 노드를 연결하여 비전 알고리즘 파이프라인을 시각적으로 설계합니다.
-   **(2.2) OpenCV Lib:** 강력한 오픈소스 컴퓨터 비전 라이브러리인 OpenCV의 핵심 기능들을 노드 형태로 제공합니다.
-   **(2.3) YOLOv12 레이블링, 학습, 평가 Lib:** 최신 객체 탐지 모델인 YOLO의 데이터 처리, 학습, 평가 과정을 통합 지원합니다.
-   **(2.4) 레시피 출력 및 관리 시스템:** 완성된 알고리즘 파이프라인을 '레시피'로 저장, 관리, 재사용할 수 있습니다.
-   **(2.5) 런타임 엔진:** 설계된 파이프라인을 고속으로 실행하여 실제 검사 환경에 적용합니다.
-   **(2.6) 데이터 및 사용자 관리:** 검사 데이터와 사용자 계정을 체계적으로 관리합니다.
-   **(2.7) Cognex, Mil, Halcon 연동 (후순위):** 주요 상용 비전 라이브러리와의 연동을 지원하여 확장성을 확보합니다.
-   **(2.8) LLM 기반 알고리즘 자동 최적화 (후순위):** LLM을 활용하여 비전 알고리즘의 파라미터를 자동으로 최적화하는 기능을 구현합니다.

---

## 🔀 Git 브랜치 전략 및 협업 규칙

본 프로젝트는 안정적인 협업과 코드 관리를 위해 다음과 같은 Git 브랜치 전략을 사용합니다.

---

### 🧩 브랜치 설명

| 브랜치 이름 | 설명 |
|---|---|
| `main` | 실제 운영 환경에 배포되는 코드가 존재하는 브랜치입니다. 직접 푸시 금지. |
| `dev` | 여러 기능을 통합해 테스트하는 브랜치입니다. QA 테스트는 이 브랜치 기준으로 진행됩니다. |
| `feature/{이름}-{기능}` | 개인 개발 브랜치입니다. 예: `feature/njb-login` |
| `bugfix/{이름}-{내용}` | 버그 수정 브랜치입니다. 예: `bugfix/yjh-validationpage` |

<br>

### ⚙️ UI 관리자 (AppUIManager) 규칙

본 프로젝트는 UI 컨트롤 간의 복잡한 상호작용을 관리하고 코드의 결합도를 낮추기 위해 중앙 관리자인 `AppUIManager`를 사용합니다. `AppUIManager` 사용 시 다음 규칙을 준수합니다.

#### 1. 핵심 원칙

-   **중앙 집중 관리 (Mediator Pattern):** `AppUIManager`는 UI 컨트롤들 사이의 중재자 역할을 합니다. 컨트롤들은 서로를 직접 참조하지 않으며, 모든 상호작용은 `AppUIManager`를 통해 이루어집니다.
-   **전역 접근성 (Singleton Pattern):** `AppUIManager`는 정적(static) 클래스로 구현되어, 애플리케이션의 어느 곳에서나 단일 인스턴스에 접근할 수 있습니다.
-   **단일 책임 원칙 (SRP):** `Form1`과 같은 폼 클래스는 UI 요소들을 배치하고 `AppUIManager`를 초기화하는 책임만 가집니다. 실제 동작 로직과 이벤트 처리는 `AppUIManager`에 위임합니다.

#### 2. 사용 규칙

-   **초기화:**
    -   `AppUIManager`는 반드시 메인 폼(`Form1`)의 생성자 또는 `Load` 이벤트에서 `AppUIManager.Initialize(this);` 와 같이 단 한 번만 초기화되어야 합니다.
    -   이 `Initialize` 메서드 안에서 모든 주요 UserControl(`UcNodeEditor`, `UcLogView` 등)이 생성됩니다.

-   **컨트롤 접근:**
    -   `Form1`에 배치되는 주요 UserControl들은 `AppUIManager`의 정적(static) 프로퍼티를 통해 접근합니다.
    -   예: `pnlBottom.Controls.Add(AppUIManager.ucLogView);`

-   **이벤트 처리:**
    -   노드 엔진(`MyNodesContext`)에서 발생하는 `FeedbackInfo`와 같은 이벤트들은 `AppUIManager` 내부의 `OnNodeFeedbackReceived` 이벤트 핸들러에서 중앙 처리됩니다.
    -   개별 노드나 컨트롤에서 다른 컨트롤의 UI를 직접 업데이트해야 할 경우, 직접 참조하는 대신 `FeedbackInfo` 이벤트를 발생시켜 `AppUIManager`에 작업을 요청하는 것을 원칙으로 합니다.

#### 3. 예시 코드

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
```

**MyNodesContext.cs - 다른 컨트롤에 작업 요청**

```csharp
// "이미지 표시 요청" 노드에서 직접 PictureBox를 건드리는 대신,
// FeedbackInfo 이벤트를 통해 UIManager에게 이미지 표시를 '요청'합니다.

public void RequestDisplayImage(Mat imageToDisplay)
{
    if (imageToDisplay == null || imageToDisplay.Empty()) 
    {
        // ... 오류 처리 ...
        return;
    }

    // AppUIManager가 이 신호를 듣고 ucImageControler의 UI를 업데이트합니다.
    FeedbackInfo?.Invoke("이미지 표시", CurrentProcessingNode, FeedbackType.Information, imageToDisplay, false);
}
```

---
본 문서는 Gemini(LLM모델)을 이용하여 초안을 작성하였으며, 모든 내용은 직접 검토 하였습니다.
