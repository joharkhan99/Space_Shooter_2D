using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class AdUIManager : MonoBehaviour
{
    public Text PlayerShopName;
    public Text PlayerShopScore;
    public Text PlayerShopDiamond;
    public Text PlayerShopGold;
    public GameObject InternetConnPanel;
    public Button guiAlertButton;

    void Start()
    {
        InvokeRepeating("InternetpanelShowHide", 0, 2f);
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
        UnityWebRequest request = new UnityWebRequest("https://www.infologysolutions.com/");
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

    public void ExitShop()
    {
        SceneManager.LoadScene("MenuScene");
    }


}
