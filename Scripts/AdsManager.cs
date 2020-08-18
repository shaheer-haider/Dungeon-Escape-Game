using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{
    string androidGameID = "3657909";
    bool testMode = true;
    string myplacementId = "rewardedVideo";

    Button myButton;

    private void Start()
    {
        myButton = GetComponent<Button>();
        myButton.interactable = Advertisement.IsReady(myplacementId);
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(androidGameID, testMode);
    }
    void ShowRewardedVideo()
    {
        Advertisement.Show(myplacementId);
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            PlayerPrefs.SetInt("TotalDiamonds", PlayerPrefs.GetInt("TotalDiamonds") + 10);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == myplacementId)
        {
            myButton.interactable = true;
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
