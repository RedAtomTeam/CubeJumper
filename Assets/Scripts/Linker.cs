using UnityEngine;

public class Linker : MonoBehaviour
{
    public static Linker Instance { get; private set; }

    [SerializeField] private SaveService _saveService;
    [SerializeField] private BalanceManager _balanceManager;
    [SerializeField] private UpgradeSystem _upgradeSystem;
    [SerializeField] private AdsService _adsService;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SaveService.OnLoadProgress += _upgradeSystem.LoadProgress;
            SaveService.OnLoadBalance += _balanceManager.LoadBalance;

            _saveService.Link(_balanceManager, _upgradeSystem);
            _adsService.Link(_balanceManager);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
