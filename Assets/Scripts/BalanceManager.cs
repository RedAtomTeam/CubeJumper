using DG.Tweening.Plugins.Core.PathCore;
using System.IO;
using System.Text;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BalanceManager : MonoBehaviour
{
    public static BalanceManager Instance { get; private set; }

    [SerializeField] private int _currentCoins;
    private const string SAVE_KEY = "PlayerCoins";

    public int Coins { get => _currentCoins; private set => _currentCoins = value; }

    public event System.Action<int> OnCoinsUpdated;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadBalance();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddBalance(int amount)
    {
        _currentCoins += amount;
        SaveBalance();
        OnCoinsUpdated?.Invoke(_currentCoins);
    }

    public bool RemoveBalance(int amount)
    {
        if (_currentCoins < amount)
            return false;
        _currentCoins -= amount;
        OnCoinsUpdated?.Invoke(_currentCoins);
        SaveBalance();
        return true;
    }

    private void SaveBalance()
    {
        string savePath = System.IO.Path.Combine(Application.persistentDataPath, "Balance");

        try
        {
            File.WriteAllText(savePath, Coins.ToString());
            Debug.Log($"Успешно сохранено значение {Coins} в файл: {savePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка сохранения: {e.Message}");
        }
    }

    private void LoadBalance()
    {
        string loadPath = System.IO.Path.Combine(Application.persistentDataPath, "Balance");

        if (!File.Exists(loadPath))
        {
            SaveBalance();
            return;
        }

        try
        {
            if (int.TryParse(File.ReadAllText(loadPath), out _currentCoins))
                Debug.Log($"Успешно загружено значение {Coins} в файл: {loadPath}");
            else
                Debug.Log($"Не удалось спарсить числовое значение из строки '{File.ReadAllText(loadPath)}'");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка сохранения: {e.Message}");
        }
    }

    private byte[] SimpleEncrypt(string data)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        for (int i = 0; i < bytes.Length; i++)
            bytes[i] ^= 0x55; 
        return bytes;
    }
}
