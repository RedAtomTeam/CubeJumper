using System.Text;
using UnityEngine;

public class BalanceManager : MonoBehaviour
{
    public static BalanceManager Instance { get; private set; }
    public int Coins { get => _currentCoins; private set => _currentCoins = value; }

    [SerializeField] private int _currentCoins;


    public static event System.Action<int> OnCoinsUpdated;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddBalance(int amount)
    {
        _currentCoins += amount;
        OnCoinsUpdated?.Invoke(_currentCoins);
    }

    public bool RemoveBalance(int amount)
    {
        if (_currentCoins < amount)
            return false;
        _currentCoins -= amount;
        OnCoinsUpdated?.Invoke(_currentCoins);
        return true;
    }

   public void LoadBalance(int coins)
    {
        Coins = coins;
    }

    private byte[] SimpleEncrypt(string data)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        for (int i = 0; i < bytes.Length; i++)
            bytes[i] ^= 0x55; 
        return bytes;
    }
}
