using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Button updateBtn;

    public event Action onClickEvent;

    private void Awake()
    {
        updateBtn.onClick.AddListener(Click);
    }

    private void Click()
    {
        onClickEvent?.Invoke();
    }

    public void UpdateValues(int value, int coins, bool isMax)
    {
        valueText.text = value.ToString();
        coinsText.text = coins.ToString();
        if (isMax)
        {
            valueText.color = Color.red;
            coinsText.gameObject.SetActive(false);
        }
    }
}
