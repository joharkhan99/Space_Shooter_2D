using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using GoogleMobileAds.Api;


public class SpinnerManager : MonoBehaviour
{
    private bool _isStarted;
    private float[] _sectorsAngles;
    private float _finalAngle;
    private float _startAngle = 0;
    private float _currentLerpRotationTime;
    public Button TurnButton;
    public GameObject Circle; 			// Rotatable Object with rewards
    private string AwardType = "";

    public Text PlayerShopName;
    public Text PlayerShopScore;
    public Text PlayerShopDiamond;
    public Text PlayerShopGold;
    public GameObject InternetConnPanel;
    public Button guiAlertButton;

    string RewardedAd_ID = "ca-app-pub-2978387739751817/8026831814";
    public RewardedAd rewardedAd;


    void Start()
    {
        InvokeRepeating("InternetpanelShowHide", 0, 2f);
        MobileAds.Initialize(initStatus => { });
    }
    public void RequestRewardAD()
    {
        this.rewardedAd = new RewardedAd(RewardedAd_ID);
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);

        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
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
    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        StartCoroutine(ShowMessage("Reward Ad Failed To Show."));
    }
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        TurnWheel();

        GameObject bannerCanvas = GameObject.Find("FULL_BANNER(Clone)");
        bannerCanvas.GetComponent<Canvas>().sortingOrder = 1;
    }


    public void TurnWheel()
    {
        _currentLerpRotationTime = 0f;

        // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
        _sectorsAngles = new float[] { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 360 };

        int fullCircles = 5;
        float randomFinalAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];

        // Here we set up how many circles our wheel should rotate before stop
        _finalAngle = -(fullCircles * 360 + randomFinalAngle);
        _isStarted = true;
    }

    private void GiveAwardByAngle()
    {
        // Here you can set up rewards for every sector of wheel
        switch ((int)_startAngle)
        {
            case 0:
                AwardType = "diamond";
                RewardCoins(70);
                break;
            case -330:
                AwardType = "gold";
                RewardCoins(1000);
                break;
            case -300:
                AwardType = "gold";
                RewardCoins(5);
                break;
            case -270:
                AwardType = "diamond";
                RewardCoins(500);
                break;
            case -240:
                AwardType = "gold";
                RewardCoins(500);
                break;
            case -210:
                AwardType = "gold";
                RewardCoins(15);
                break;
            case -180:
                AwardType = "diamond";
                RewardCoins(400);
                break;
            case -150:
                AwardType = "gold";
                RewardCoins(1000);
                break;
            case -120:
                AwardType = "gold";
                RewardCoins(100);
                break;
            case -90:
                AwardType = "diamond";
                RewardCoins(500);
                break;
            case -60:
                AwardType = "gold";
                RewardCoins(1000);
                break;
            case -30:
                AwardType = "gold";
                RewardCoins(800);
                break;
            default:
                AwardType = "gold";
                RewardCoins(5);
                break;
        }
    }

    void Update()
    {
        string Player_prefs_USERNAME = PlayerPrefs.GetString("USERNAME");
        int Player_prefs_SCORE = PlayerPrefs.GetInt("HIGHSCORE"),
            Player_prefs_DIAMONDS = PlayerPrefs.GetInt("DIAMOND"),
            Player_prefs_GOLDS = PlayerPrefs.GetInt("GOLD");

        PlayerShopName.GetComponent<Text>().text = Player_prefs_USERNAME;
        PlayerShopScore.GetComponent<Text>().text = Player_prefs_SCORE.ToString();
        PlayerShopDiamond.GetComponent<Text>().text = Player_prefs_DIAMONDS.ToString();
        PlayerShopGold.GetComponent<Text>().text = Player_prefs_GOLDS.ToString();

        // Make turn button non interactable if user has not enough money for the turn
        if (DateTime.Now.Ticks - TimeSpan.TicksPerDay > long.Parse(PlayerPrefs.GetString("LASTDATESPUN", "0")))
        {
            TurnButton.interactable = true;
            TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
        else
        {
            TurnButton.interactable = false;
            TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        }

        if (!_isStarted)
            return;

        float maxLerpRotationTime = 4f;

        // increment timer once per frame
        _currentLerpRotationTime += Time.deltaTime;
        if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle)
        {
            _currentLerpRotationTime = maxLerpRotationTime;
            _isStarted = false;
            _startAngle = _finalAngle % 360;

            GiveAwardByAngle();
        }

        // Calculate current position using linear interpolation
        float t = _currentLerpRotationTime / maxLerpRotationTime;

        // This formulae allows to speed up at start and speed down at the end of rotation.
        // Try to change this values to customize the speed
        t = t * t * t * (t * (6f * t - 15f) + 10f);

        float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
        Circle.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void InternetpanelShowHide()
    {
        StartCoroutine(CheckInternetConnection(isConnected =>
        {
            if (isConnected)
            {
                InternetConnPanel.SetActive(false);
            }
            else
            {
                InternetConnPanel.SetActive(true);
            }
        }));
    }

    IEnumerator CheckInternetConnection(System.Action<bool> action)
    {
        UnityWebRequest request = new UnityWebRequest("https://infologysolutions.com");
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }



    private void RewardCoins(int awardCoins)
    {
        StartCoroutine(ShowMessage("You Recieved " + awardCoins.ToString() +" "+ AwardType+"s"));

        int PreviousAmount = PlayerPrefs.GetInt(AwardType.ToUpper());
        PlayerPrefs.SetInt(AwardType.ToUpper(), PreviousAmount + awardCoins);

        PlayerPrefs.SetString("LASTDATESPUN", DateTime.Now.Ticks.ToString());
    }

    public void ExitShop()
    {
        SceneManager.LoadScene("MenuScene");
    }

    IEnumerator ShowMessage(string message, float delay=3)
    {
        guiAlertButton.GetComponentInChildren<Text>().text = message;
        guiAlertButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        guiAlertButton.GetComponentInChildren<Text>().text = "";
        guiAlertButton.gameObject.SetActive(false);
    }

    public void OnSpinAdButtonClick()
    {

    }


}
