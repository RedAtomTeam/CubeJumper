using TMPro;
using UnityEngine;

public class BalanceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI balanceText;


    private void Start()
    {
        BalanceManager.Instance.OnCoinsUpdated += UpdateBalance;   
        balanceText.text = BalanceManager.Instance.Coins.ToString();        
    }

    private void UpdateBalance(int balance)
    {
        balanceText.text = balance.ToString();
    }
}
