using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SceneOpener : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private AdsService _adsService;
    private UpgradeSystem _upgradeSystem;


    private void Start()
    {
        _adsService = AdsService.Instance;
        _upgradeSystem = UpgradeSystem.Instance;
    }

    public void PerformOpenScene()
    {
        if (_upgradeSystem.Progress.currentSpeedLevel > 0 ||
            _upgradeSystem.Progress.currentSlideLevel > 0 ||
            _upgradeSystem.Progress.currentJumpLevel > 0)
        {
            YandexGame.CloseFullAdEvent += OpenScene;
            if (!_adsService.ShowAd())
            {
                YandexGame.CloseFullAdEvent -= OpenScene;
                OpenScene();
            }
        }
        else
        {
            OpenScene();
        }
    }

    private void OpenScene()
    {
        YandexGame.CloseFullAdEvent -= OpenScene;
        YandexGame.CloseFullAdEvent -= SoundtracksService.Instance.StartSoundtracks;
        SceneManager.LoadSceneAsync(_sceneName);
    }
}
