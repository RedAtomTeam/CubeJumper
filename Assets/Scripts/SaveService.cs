using System;
using UnityEngine;
using YG;

public class SaveService : MonoBehaviour
{
    public static SaveService Instance { get; private set; }

    private static BalanceManager _balanceManager;
    private static UpgradeSystem _upgradeSystem;

    public static event Action<int> OnLoadBalance;
    public static event Action<int, int, int> OnLoadProgress;

    public void Link(BalanceManager balanceManager, UpgradeSystem upgradeSystem)
    {
        _balanceManager = balanceManager;
        _upgradeSystem = upgradeSystem;

        Init();
    }

    private void Init()
    {


        BalanceManager.OnCoinsUpdated += Save;
        UpgradeSystem.OnUpgradesChanged += Save;

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

    public static void SaveHeight(int height)
    {
        YandexGame.savesData.maxHeight = height;
    }

    public static void SaveTime(int time)
    {
        YandexGame.savesData.maxTimeInSeconds = time;
    }


    static void Save(int coin)
    {
        Save();
    }

    static void Save()
    {
        YandexGame.savesData.balance = _balanceManager.Coins;

        YandexGame.savesData.currentSpeedLevel = _upgradeSystem.Progress.currentSpeedLevel;
        YandexGame.savesData.currentJumpLevel = _upgradeSystem.Progress.currentJumpLevel;
        YandexGame.savesData.currentSlideLevel = _upgradeSystem.Progress.currentSlideLevel;

        YandexGame.SaveProgress();
    }

    static void Load()
    {
        OnLoadBalance?.Invoke(YandexGame.savesData.balance);
        OnLoadProgress?.Invoke(
                                YandexGame.savesData.currentSpeedLevel,
                                YandexGame.savesData.currentJumpLevel,
                                YandexGame.savesData.currentSlideLevel
            );
    }
}


public partial class SavesYG
{
    // Ваши данные для сохранения
    public int coins = 5; // Пример
}