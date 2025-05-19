using System;
using UnityEngine;
using YG;

public class SaveService : MonoBehaviour
{
    public static SaveService Instance { get; private set; }

    private static BalanceManager _balanceManager;
    private static UpgradeSystem _upgradeSystem;

    public static event Action<int> OnLoadBalance;
    public static event Action<PlayerProgressConfig> OnLoadProgress;

    public void Link(BalanceManager balanceManager, UpgradeSystem upgradeSystem)
    {
        _balanceManager = balanceManager;
        _upgradeSystem = upgradeSystem;

        Init();
    }

    private void Init()
    {
        _balanceManager.OnCoinsUpdated += Save;
        _upgradeSystem.OnUpgradesChanged += Save;

        Load();
    }

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

    static void Save(int coin)
    {
        Save();
    }

    static void Save()
    {
        SavesYG save = new SavesYG()
        {
            progress = _upgradeSystem.Progress,
            balance = _balanceManager.Coins,
        };

        YandexGame.savesData = save;
        YandexGame.SaveProgress();
    }

    static void Load()
    {
        OnLoadBalance?.Invoke(YandexGame.savesData.balance);
        OnLoadProgress?.Invoke(YandexGame.savesData.progress);
    }

}
