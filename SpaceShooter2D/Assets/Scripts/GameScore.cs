using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameScore : MonoBehaviour
{
    Text scoreUIText;
    int score;
    GameObject Level1_Enemy2Spawner;
    GameObject HighScoreTextUI;


    // Start is called before the first frame update
    void Start()
    {
        scoreUIText = GetComponent<Text>();
        Level1_Enemy2Spawner = GameObject.FindGameObjectWithTag("Level1_enemy2Spawner");
        HighScoreTextUI = GameObject.Find("HighScoreText");
    }

    // Update is called once per frame
    void Update()
    {
    }

    //short way of setter/getter
    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            UpdateScoretextUI();
        }
    }

    //for score updating
    void UpdateScoretextUI()
    {
        string scoreStr = score.ToString();
        scoreUIText.text = scoreStr;
        UpdateHighScore();
    }

    void UpdateHighScore()
    {
        if (!PlayerPrefs.HasKey("HIGHSCORE"))
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
            UpdateHighScoreTextUI(score);
        }
        else if(score > PlayerPrefs.GetInt("HIGHSCORE"))
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
            UpdateHighScoreTextUI(score);
        }
        else
        {
            UpdateHighScoreTextUI(PlayerPrefs.GetInt("HIGHSCORE"));
        }
    }

    void UpdateHighScoreTextUI(int hs)
    {
        HighScoreTextUI.GetComponent<Text>().text = hs.ToString();
    }



}
