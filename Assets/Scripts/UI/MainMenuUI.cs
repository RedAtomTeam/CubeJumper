using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private RectTransform _uiPanel; // Используем RectTransform для UI
    [SerializeField] private float _animationDuration = 0.5f;
    [SerializeField] private Ease _easeType = Ease.OutBack; // Эффект "пружины"

    private Vector2 _hiddenPosition;
    private Vector2 _shownPosition;


    private void Awake()
    {
        // Запоминаем позиции
        _shownPosition = _uiPanel.anchoredPosition;
        _hiddenPosition = new Vector2(_shownPosition.x, -_uiPanel.rect.height);

        //// Сразу скрываем панель
        //_uiPanel.anchoredPosition = _hiddenPosition;
    }

    public void OpenUI()
    {
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

    public void Play()
    {
        CloseUI();
    }

    private void CloseUI()
    {
        _uiPanel.DOKill();

        // Анимация скрытия вниз + fade-out
        _uiPanel.DOAnchorPos(_hiddenPosition, _animationDuration)
            .SetEase(Ease.InBack)
            .OnComplete(() => {
                SceneManager.LoadSceneAsync("Game");
            });
    }
}
