using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{

//#if UNITY_IOS
//#endif
    private string gameId = "3479909";

    private string myPlacementId = Ads.REWARDED_AD;

    public Button Button { get; private set; }

    public event Action OnAdFinished;


    private void Awake()
    {
        Button = GetComponent<Button>();

        //Map the ShowRewardedVideo function to the button’s click listener:
        if (Button)
        {

            Button.onClick.AddListener(ShowRewardedVideo);

            //if (Advertisement.IsReady(myPlacementId))
            //{
            //    Button.onClick.AddListener(ShowRewardedVideo);
            //}
            //else
            //{
            //    Button.onClick.AddListener(() => Debug.Log("Ad is not ready."));
            //}
        }

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, GameManager.Instance.TestMode);
    }

    private void OnDestroy()
    {
        //Advertisement.RemoveListener(this);
    }

    // Implement a function for showing a rewarded video ad:
    private void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            //if (Button)
            //{
            //    Button.onClick.RemoveAllListeners();
            //    Button.onClick.AddListener(ShowRewardedVideo);
            //}
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            OnAdFinished?.Invoke();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}

public static class Ads
{
    public const string REWARDED_AD = "rewardedVideo";
}