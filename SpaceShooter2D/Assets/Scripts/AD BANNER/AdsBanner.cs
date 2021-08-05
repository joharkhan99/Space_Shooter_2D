using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine;
using System;

public class AdsBanner : MonoBehaviour
{
    string BannerAd_ID = "ca-app-pub-2978387739751817/9887063619";
    public bool isGameScene = false;

    private BannerView bannerView;

    // Start is called before the first frame update
    void Start()
    {

        MobileAds.Initialize(initStatus => { });

        RequestBanner();
        ShowBannerAD();

    }

    public void RequestBanner()
    {
        AdSize adSize = new AdSize(468,60);
        if (isGameScene)
        {
            this.bannerView = new BannerView(BannerAd_ID, adSize, AdPosition.Bottom);
        }
        else
        {
            this.bannerView = new BannerView(BannerAd_ID, adSize, AdPosition.Top);
        }


    }

    public void ShowBannerAD()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
        this.bannerView.Show();
    }


}
