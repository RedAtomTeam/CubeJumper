using UnityEngine;

public class ShowRewardVideo : MonoBehaviour
{
    private AdsService adsService;

    public void ShowRewardAd()
    {
        if (adsService == null)
        {
            adsService = AdsService.Instance;
        }
        adsService.ShowRewardAd();
    }
}
