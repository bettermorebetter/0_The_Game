using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class The_Game : MonoBehaviour
{
    [Header("UI 연결")]
    public TextMeshProUGUI countText;  // 감소할 숫자를 표시할 텍스트
    public Button clickButton;         // 클릭할 버튼
    public RectTransform eggRect;
    private Vector3 originalScale;
    private Tween scaleTween;
    public Image eggImage;

    [Header("Scale Settings")]
    public float maxScaleFactor = 1.1f;   // 최대 커지는 배율 (예: 1.2 = 120%)
    public float duration = 0.1f;

    [Header("게임 설정")]
    public long count = 100;            // 시작 카운트

    [Header("단계별 달걀 스프라이트 (1~10)")]
    public Sprite[] eggSprites = new Sprite[10];

    [Header("단계 설정")]
    public int maxStage = 10;
    private int currentStage = 1;

    private long requiredClicks;    // 해당 단계에 필요한 클릭 수

    void Start()
    {
        originalScale = eggRect.localScale;
        // 버튼 클릭 이벤트 연결
        clickButton.onClick.AddListener(OnClickButton);
        // 초기 텍스트 설정
        UpdateCountText(count);
        // 초기 단계 세팅
        InitStage();
    }

    void InitStage()
    {
        // 1) 필요 클릭 수 계산: 10^(stage+1)
        //    Mathf.Pow은 float이므로 long으로 캐스트
        requiredClicks = CalcRequiredClicks(currentStage);

        count = requiredClicks; // 카운트 초기화

        UpdateCountText(requiredClicks);

        // 3) 달걀 스프라이트 변경
        if (eggSprites.Length >= currentStage)
            eggImage.sprite = eggSprites[currentStage - 1];
    }

    long CalcRequiredClicks(int stage)
    {
        long v = 1;
        for (int i = 0; i < stage + 1; i++)
            v *= 10;
        return v;
    }

    void OnClickButton()
    {
        // 1. 달걀이 커졌다가 줄어드는 효과
        // 1) 이전 트윈이 남아 있으면 종료
        if (scaleTween != null && scaleTween.IsActive())
            scaleTween.Kill();

        // 2) 스케일 리셋
        eggRect.localScale = originalScale;

        // 3) 최대 스케일 지정 → 원래로 복귀 (Yoyo)
        scaleTween = eggRect
            .DOScale(originalScale * maxScaleFactor, duration)
            .SetEase(Ease.OutBack)                   // 탄력감 ↑
            .SetLoops(2, LoopType.Yoyo);

        count--;                    // 카운트 1 감소

        if (count <= 0)
        {
            NextStage();  // 카운트가 0이 되면 다음 단계로 넘어감
        }                 // 0 이하일 땐 더 감소하지 않음
        else
        {
            UpdateCountText(count);
        }
    }

    void UpdateCountText(long count)
    {
        countText.text = count.ToString();
    }

    void NextStage()
    {
        if (currentStage < maxStage)
        {
            currentStage++;
            InitStage();
            // (선택) 레벨업 효과(애니메이션, 사운드) 추가
        }
        else
        {
            // 최고 단계 도달: 게임 종료 혹은 리셋
            countText.text = "All Clear!";
            clickButton.interactable = false;
        }
    }
}
