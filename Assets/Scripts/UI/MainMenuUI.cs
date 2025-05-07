using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private RectTransform _uiPanel; // ���������� RectTransform ��� UI
    [SerializeField] private float _animationDuration = 0.5f;
    [SerializeField] private Ease _easeType = Ease.OutBack; // ������ "�������"

    private Vector2 _hiddenPosition;
    private Vector2 _shownPosition;


    private void Awake()
    {
        // ���������� �������
        _shownPosition = _uiPanel.anchoredPosition;
        _hiddenPosition = new Vector2(_shownPosition.x, -_uiPanel.rect.height);

        //// ����� �������� ������
        //_uiPanel.anchoredPosition = _hiddenPosition;
    }

    public void OpenUI()
    {
        // ������������� ���������� �������� ����� �������� ����������
        _uiPanel.DOKill();

        // �������� ������ ����� + fade-in
        _uiPanel.gameObject.SetActive(true);
        _uiPanel.DOAnchorPos(_shownPosition, _animationDuration)
            .SetEase(_easeType)
            .OnComplete(() => {
                // �������������� �������� ����� ���������� ��������
            });
    }

    public void Play()
    {
        CloseUI();
    }

    private void CloseUI()
    {
        _uiPanel.DOKill();

        // �������� ������� ���� + fade-out
        _uiPanel.DOAnchorPos(_hiddenPosition, _animationDuration)
            .SetEase(Ease.InBack)
            .OnComplete(() => {
                SceneManager.LoadSceneAsync("Game");
            });
    }
}
