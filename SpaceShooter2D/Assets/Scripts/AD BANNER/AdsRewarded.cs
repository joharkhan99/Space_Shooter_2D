using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = System.Random;

public class AdsRewarded : MonoBehaviour
{
    string RewardedAd_ID = "ca-app-pub-2978387739751817/8026831814";
    public RewardedAd rewardedAd;
    public Button guiAlertButton;


    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    public void RequestRewardAD()
    {
        this.rewardedAd = new RewardedAd(RewardedAd_ID);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
    }

    public void ShowRewardAD()
    {
        RequestRewardAD();
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }
    IEnumerator ShowMessage(string message, float delay = 3)
    {
        guiAlertButton.GetComponentInChildren<Text>().text = message;
        guiAlertButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        guiAlertButton.GetComponentInChildren<Text>().text = "";
        guiAlertButton.gameObject.SetActive(false);
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        StartCoroutine(ShowMessage("Reward Ad Failed To Show."));
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        GiveReward();
        GameObject bannerCanvas = GameObject.Find("FULL_BANNER(Clone)");
        bannerCanvas.GetComponent<Canvas>().sortingOrder = 1;
    }

    public void GiveReward()
    {
        int random_number = new Random().Next(1, 3);  //1 for gold , 2 for diamond
        int[] GoldRewardsArray = { 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000 };
        int[] DiamondRewardsArray = { 5, 10, 15, 20, 25, 35, 30, 40, 45, 50, 55 , 60, 70, 75, 80, 85, 90, 100 };
        if (random_number == 1)
        {
            int gold_random_number = new Random().Next(1, 19);
            int PreviousAmount = PlayerPrefs.GetInt("GOLD");
            PlayerPrefs.SetInt("GOLD", PreviousAmount + GoldRewardsArray[gold_random_number]);

            StartCoroutine(ShowMessage("You Have Been Awarded " + GoldRewardsArray[gold_random_number] + " Gold Coins"));
        } else if (random_number == 2)
        {
            int diamond_random_number = new Random().Next(1, 19);
            int PreviousAmount = PlayerPrefs.GetInt("DIAMOND");
            PlayerPrefs.SetInt("DIAMOND", PreviousAmount + GoldRewardsArray[diamond_random_number]);

            StartCoroutine(ShowMessage("You Have Been Awarded " + GoldRewardsArray[diamond_random_number] + " Diamonds"));
        }

    }

}
