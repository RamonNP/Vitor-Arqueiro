using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdmobScript : MonoBehaviour
{
    private BannerView bannerView;
    public Player player;
    public static AdmobScript instance;
    private RewardedAd rewardedAd;

    InterstitialAd interstitial;

    public static AdmobScript getInstance() {
        if(instance == null)
        {
            instance = GameObject.FindObjectOfType<AdmobScript>();
        }
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        player = GetPlayer();
        //Request Ads
        //RequestBanner();
        RequestInterstitial();
        RequestRewardedAd();
        
    }

    public void showInterstitialAd()
    {
        //Show Ad
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }

    }

    public void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
			string adUnitId = "ca-app-pub-3940256099942544/6300978111";//TEDSTE
			//string adUnitId = "ca-app-pub-2409485950941966/7026271335"; //REAL
#elif UNITY_IPHONE
			string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
#else
			string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the bottom of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
        Invoke("DestroyBanner", 15f);
    }
    public void DestroyBanner() {
        bannerView.Destroy();
    }
    public void RequestInterstitial()
    {
        #if UNITY_ANDROID

                string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //TESTE
                //string adUnitId = "ca-app-pub-2409485950941966/8543829220"; //REAL
        #elif UNITY_IPHONE
                    string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
        #else
                    string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void RequestRewardedAd() {
        string adUnitId;
        #if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917";//teste
            //adUnitId = "ca-app-pub-2409485950941966/8472388403";//Real
        #elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
            adUnitId = "unexpected_platform";
        #endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        //player.LOG.text = player.LOG.text + "HandleRewardedAdLoaded event received";
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        //player.LOG.text = player.LOG.text + "HandleRewardedAdFailedToLoad event received with message: "                             + args.Message;
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        //player.LOG.text = player.LOG.text + "HandleRewardedAdOpening event received";
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {

       //player.LOG.text = player.LOG.text + "HandleRewardedAdFailedToShow event received";
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
 ///player.LOG.text = player.LOG.text +  "HandleRewardedAdClosed event received";
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        GetPlayer().CallBackmoreArrows(10);
    }
    public void ShowRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Show();
        }
        
    }

    private Player GetPlayer() {
        if(player == null){
            player = FindObjectOfType(typeof(Player)) as Player;
        }
        return player;
    }
}
