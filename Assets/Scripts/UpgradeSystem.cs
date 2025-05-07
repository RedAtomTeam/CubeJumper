using System;
using System.IO;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private PlayerMovementSystemConfig _playerConfig;
    [SerializeField] private UpgradesConfig _upgradesConfig;
    private readonly PlayerProgressConfig _progress = new PlayerProgressConfig();

    public static UpgradeSystem Instance { get; private set; }

    public int SpeedLevel { get => _progress.currentSpeedLevel; }
    public int JumpLevel { get => _progress.currentJumpLevel; }
    public int SlideLevel { get => _progress.currentSlideLevel; }

    public int SpeedMaxLevel { get => _upgradesConfig.speedUpgrade.levels.Length - 1; }
    public int JumpMaxLevel { get => _upgradesConfig.jumpUpgrade.levels.Length - 1; }
    public int SlideMaxLevel { get => _upgradesConfig.slideUpgrade.levels.Length - 1; }

    public int SpeedValue
    {
        get
        {
            int res = 0;
            for (int i = 0; i <= _progress.currentSpeedLevel; i++)
                res += _upgradesConfig.speedUpgrade.levels[i].valueModifier;
            return res;
        }
    }

    public int SlideValue
    {
        get
        {
            int res = 0;
            for (int i = 0; i <= _progress.currentSlideLevel; i++)
                res += _upgradesConfig.slideUpgrade.levels[i].valueModifier;
            return res;
        }
    }

    public int JumpValue
    {
        get
        {
            int res = 0;
            for (int i = 0; i <= _progress.currentJumpLevel; i++)
                res += _upgradesConfig.jumpUpgrade.levels[i].valueModifier;
            return res;
        }
    }

    public int SpeedNextCost { 
        get {
            return _upgradesConfig.speedUpgrade.levels[_progress.currentSpeedLevel+1].cost;
        }
    }
    public int JumpNextCost {
        get
        {
            return _upgradesConfig.jumpUpgrade.levels[_progress.currentJumpLevel+1].cost;
        }
    }
    public int SlideNextCost {
        get
        {
            return _upgradesConfig.slideUpgrade.levels[_progress.currentSlideLevel+1].cost;
        }
    }


    public event Action OnUpgradesChanged;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadProgress()
    {
        string loadPath = Path.Combine(Application.persistentDataPath, "Progress");

        if (!File.Exists(loadPath))
        {
            SaveProgress();
            return;
        }

        try
        {
            string json = File.ReadAllText(loadPath);
            JsonUtility.FromJsonOverwrite(json, _progress);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка загрузки: {e.Message}");
        }
    }

    private void SaveProgress()
    {
        string savePath = Path.Combine(Application.persistentDataPath, "Progress");
        string json = JsonUtility.ToJson(_progress);

        try
        {
            File.WriteAllText(savePath, json);
            Debug.Log($"Успешно сохранено значение {json} в файл: {savePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка сохранения: {e.Message}");
        }
    }

    public bool TryUpgradeSpeed()
    {
        UpgradeTrack track = _upgradesConfig.speedUpgrade;
        return TryUpgrade(ref _progress.currentSpeedLevel, track);
    }

    public bool TryUpgradeJump()
    {
        UpgradeTrack track = _upgradesConfig.jumpUpgrade;
        return TryUpgrade(ref _progress.currentJumpLevel, track);
    }

    public bool TryUpgradeSlide()
    {
        UpgradeTrack track = _upgradesConfig.slideUpgrade;
        return TryUpgrade(ref _progress.currentSlideLevel, track);
    }

    private bool TryUpgrade(ref int currentLevel, UpgradeTrack track)
    {
        int nextLevel = currentLevel + 1;
        if (nextLevel >= track.levels.Length)
        {
            return false;
        }


        UpgradeLevel nextUpgrade = track.levels[nextLevel];
        if (!BalanceManager.Instance.RemoveBalance(nextUpgrade.cost))
            return false;

        currentLevel++;
        OnUpgradesChanged?.Invoke();
        SaveProgress();
        return true;
    }
}
