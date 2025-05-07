using DG.Tweening;
using UnityEngine;

public class UpgradesUI : MonoBehaviour
{
    [SerializeField] private UpgradeUI JumpUI;
    [SerializeField] private UpgradeUI SpeedUI;
    [SerializeField] private UpgradeUI SlideUI;

    [SerializeField] private UpgradeSystem upgradeSystem;

    [SerializeField] private PlayerMovementSystemConfig playerMovementSystemConfig;


    [SerializeField] private RectTransform _uiPanel; // Используем RectTransform для UI
    [SerializeField] private float _animationDuration = 0.5f;
    [SerializeField] private Ease _easeType = Ease.OutBack; // Эффект "пружины"

    private Vector2 _hiddenPosition;
    private Vector2 _shownPosition;




    public void OpenUI()
    {
        UpdateUI();

        // Останавливаем предыдущие анимации чтобы избежать конфликтов
        _uiPanel.DOKill();

        // Анимация выезда снизу + fade-in
        _uiPanel.gameObject.SetActive(true);
        _uiPanel.DOAnchorPos(_shownPosition, _animationDuration)
            .SetEase(_easeType)
            .OnComplete(() => {
                // Дополнительные действия после завершения анимации
            });
    }

    public void CloseUI()
    {
        _uiPanel.DOKill();

        // Анимация скрытия вниз + fade-out
        _uiPanel.DOAnchorPos(_hiddenPosition, _animationDuration)
            .SetEase(Ease.InBack)
            .OnComplete(() => {
                _uiPanel.gameObject.SetActive(false);
            });
    }

    private void Awake()
    {
        // Запоминаем позиции
        _shownPosition = _uiPanel.anchoredPosition;
        _hiddenPosition = new Vector2(_shownPosition.x, -_uiPanel.rect.height);

        // Сразу скрываем панель
        _uiPanel.anchoredPosition = _hiddenPosition;
    }

    private void UpdateUI()
    {
        if (upgradeSystem == null)
            upgradeSystem = UpgradeSystem.Instance;
        JumpUI.onClickEvent += UpgradeJump;
        SpeedUI.onClickEvent += UpgradeSpeed;
        SlideUI.onClickEvent += UpgradeSlide;

        UpdateJumpUI();
        UpdateSpeedUI();
        UpdateSlideUI();
    }

    private void UpgradeJump()
    {
        upgradeSystem.TryUpgradeJump();
        UpdateJumpUI();
    }

    private void UpgradeSpeed()
    {
        upgradeSystem.TryUpgradeSpeed();
        UpdateSpeedUI();
    }
    private void UpgradeSlide()
    {
        upgradeSystem.TryUpgradeSlide();
        UpdateSlideUI();
    }

    private void UpdateJumpUI()
    {
        var value = (int)playerMovementSystemConfig.jumpForce + upgradeSystem.JumpValue;
        var isMax = upgradeSystem.JumpLevel == upgradeSystem.JumpMaxLevel;
        var jumpNextCost = isMax ? 0 : upgradeSystem.JumpNextCost;


        JumpUI.UpdateValues(value, jumpNextCost, isMax);
    }

    private void UpdateSpeedUI()
    {
        var speedNextCost = upgradeSystem.SpeedLevel < upgradeSystem.SpeedMaxLevel ? upgradeSystem.SpeedNextCost : 0;
        SpeedUI.UpdateValues((int)playerMovementSystemConfig.horizontalMoveSpeed + upgradeSystem.SpeedValue, speedNextCost, upgradeSystem.SpeedLevel == upgradeSystem.SpeedMaxLevel);
    }

    private void UpdateSlideUI()
    {
        var slideNextCost = upgradeSystem.SlideLevel < upgradeSystem.SlideMaxLevel ? upgradeSystem.SlideNextCost : 0;
        SlideUI.UpdateValues((int)playerMovementSystemConfig.wallSlideSpeed + upgradeSystem.SlideValue, slideNextCost, upgradeSystem.SlideLevel == upgradeSystem.SlideMaxLevel);
    }

}
