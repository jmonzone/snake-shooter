using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public static class Ads
{
    public const string APPLE_GAME_ID = "3479909";
    public const string REWARDED_AD = "rewardedVideo";
    public const string BANNER_AD = "bannerPlacement";
}

public class BannerAdScript : MonoBehaviour
{

#if UNITY_IOS
    private string gameId = "3479909";
#endif

    public bool testMode = true;

    private void Start()
    {
        Advertisement.Initialize(gameId, testMode);

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(Ads.BANNER_AD))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(Ads.BANNER_AD);
    }
}