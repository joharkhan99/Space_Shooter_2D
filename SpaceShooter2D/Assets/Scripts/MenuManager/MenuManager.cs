using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MenuManager : MonoBehaviour
{
    public Text PlayerProfileName;
    public Text PlayerProfileScore;
    public Text PlayerProfileDiamond;
    public Text PlayerProfileGold;
    public GameObject InternetConnPanel;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InternetpanelShowHide", 0, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        string Player_prefs_USERNAME = PlayerPrefs.GetString("USERNAME");
        int Player_prefs_SCORE = PlayerPrefs.GetInt("HIGHSCORE"),
            Player_prefs_DIAMONDS = PlayerPrefs.GetInt("DIAMOND"),
            Player_prefs_GOLDS = PlayerPrefs.GetInt("GOLD");

        PlayerProfileName.GetComponent<Text>().text = Player_prefs_USERNAME;
        PlayerProfileScore.GetComponent<Text>().text = Player_prefs_SCORE.ToString();
        PlayerProfileDiamond.GetComponent<Text>().text = Player_prefs_DIAMONDS.ToString();
        PlayerProfileGold.GetComponent<Text>().text = Player_prefs_GOLDS.ToString();
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


    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene("RankingScene");
    }

    public void LoadShop()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void LoadSpinner()
    {
        SceneManager.LoadScene("SpinnerScene");
    }

    public void LoadAdScene()
    {
        SceneManager.LoadScene("AdScene");
    }

    public void ExitGamePlay()
    {
        Application.Quit();
    }

}
