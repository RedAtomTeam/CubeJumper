using UnityEngine;
using YG;

public class AdsService : MonoBehaviour
{
    public static AdsService Instance { get; private set; }
    
    private BalanceManager _balanceManager;
    private int _rewardCoins = 0;


    public void Link(BalanceManager balanceManager)
    {
        _balanceManager = balanceManager;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            YandexGame.RewardVideoEvent += OnReward;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool ShowAd()
    {
        if (YandexGame.timerShowAd > 60)
        {
            SoundtracksService.Instance.StopImmediatelySoundtracks();
            YandexGame.FullscreenShow();
            YandexGame.CloseFullAdEvent += SoundtracksService.Instance.StartSoundtracks;
            return true;
        }
        return false;
    }

    public void ShowRewardAd()
    {
        SoundtracksService.Instance.StopImmediatelySoundtracks();

        YandexGame.CloseVideoEvent += OnAdFinished;
        YandexGame.RewVideoShow(1);
    }

    public void ShowDoubleRewardAd(int coins)
    {
        SoundtracksService.Instance.StopImmediatelySoundtracks();

        YandexGame.CloseVideoEvent += OnAdFinished;
        YandexGame.RewVideoShow(2);
    }

    void OnReward(int rewardId)
    {
        switch (rewardId)
        {
            case 1:
                _balanceManager.AddBalance(10); 
                break;
            case 2:
                _balanceManager.AddBalance(_rewardCoins);
                break;
        }
    }

    private void OnAdFinished()
    {
        YandexGame.CloseVideoEvent -= OnAdFinished;
        SoundtracksService.Instance.StartSoundtracks();

    }

}
