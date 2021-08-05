using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System;

public class LeaderBoardScore : MonoBehaviour
{

    private const string highscoreURL = "https://infologysolutions.com/spaceshooter_api/index.php";

    public Text MenuPlayerName;
    public Text MenuPlayerRanking;
    public Text Diamonds;
    public Text GoldCoin;
    public GameObject InternetConnPanel;

    public void Start()
    {
        StartCoroutine(GetUserRank(PlayerPrefs.GetString("USERNAME")));
        RetrieveScores();

        string Player_prefs_USERNAME = PlayerPrefs.GetString("USERNAME");
        int Player_prefs_SCORE = PlayerPrefs.GetInt("HIGHSCORE"),
            Player_prefs_DIAMONDS = PlayerPrefs.GetInt("DIAMOND"),
            Player_prefs_GOLDS = PlayerPrefs.GetInt("GOLD");

        MenuPlayerName.GetComponent<Text>().text = Player_prefs_USERNAME;
        MenuPlayerRanking.GetComponent<Text>().text = Player_prefs_SCORE.ToString();
        Diamonds.GetComponent<Text>().text = Player_prefs_DIAMONDS.ToString();
        GoldCoin.GetComponent<Text>().text = Player_prefs_GOLDS.ToString();

/*        StartCoroutine(CheckInternetConnection(isConnected =>
        {
            if (isConnected)
            {
                InternetConnPanel.SetActive(false);
            }
            else
            {
                InternetConnPanel.SetActive(true);
            }
        }));*/
    }

    // Update is called once per frame
    void Update()
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
        yield return new WaitForSeconds(3);
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

    IEnumerator GetUserRank(string name)
    {
        WWWForm form = new WWWForm();
        form.AddField("GetUserRank_ZErXPnRu6PI1ka2kjes2EvGUat8Q7fhN", "true");
        form.AddField("name", name);

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string contents = www.downloadHandler.text;
                using (StringReader reader = new StringReader(contents))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        GameObject.Find("PersonalNumber").GetComponent<Text>().text = line;
                        GameObject.Find("PersonalName").GetComponent<Text>().text = PlayerPrefs.GetString("USERNAME");
                        GameObject.Find("PersonalScore").GetComponent<Text>().text = PlayerPrefs.GetInt("HIGHSCORE").ToString();
                    }
                }
            }
        }
    }




    public List<Score> RetrieveScores()
    {
        List<Score> scores = new List<Score>();
        StartCoroutine(DoRetrieveScores(scores));
        return scores;
    }

    public void PostScores(string name, int score)
    {
        StartCoroutine(DoPostScores(name, score));
    }

    IEnumerator DoRetrieveScores(List<Score> scores)
    {
        WWWForm form = new WWWForm();
        form.AddField("get_leaderboard_ZErXPnRu6PI1ka2kjes2EvGUat8Q7fhN", "true");

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string contents = www.downloadHandler.text;
                using (StringReader reader = new StringReader(contents))
                {
                    string line;
                    int S_No = 1;
                    int user_number = 1;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Score entry = new Score();
                        entry.name = line;
                        try
                        {
                            entry.score = Int32.Parse(reader.ReadLine());
                            GameObject.Find("Number"+ S_No).GetComponent<Text>().text = S_No.ToString();
                            GameObject.Find("Name"+ user_number).GetComponent<Text>().text = entry.name;
                            GameObject.Find("Score"+ user_number).GetComponent<Text>().text = entry.score.ToString();

                            S_No++;
                            user_number++;
                        }
                        catch (Exception e)
                        {
                            Debug.Log("Invalid score: " + e);
                            continue;
                        }

                        scores.Add(entry);
                    }
                }
            }
        }
    }

    IEnumerator DoPostScores(string name, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("post_leaderboard_ZErXPnRu6PI1ka2kjes2EvGUat8Q7fhN", "true");
        form.AddField("name", name);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Successfully posted score!");
            }
        }
    }
    
    public void Exit()
    {
        SceneManager.LoadScene("MenuScene");
    }

}

public struct Score
{
    public string name;
    public int score;
}
