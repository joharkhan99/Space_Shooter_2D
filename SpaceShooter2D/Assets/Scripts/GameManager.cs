using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    private const string highscoreURL = "https://infologysolutions.com/spaceshooter_api/index.php";

    //reference to game objects
    public GameObject playerShip, playerShip2, playerShip3, playerShip4;
    public GameObject playerHealthBar;
    public GameObject enemySpawner;
    public GameObject Level1_Enemy2Spawner;
    public GameObject BossSpawner;
    public GameObject SmallRockSpawner;
    public GameObject BurstingStoneSpawner;
    public GameObject gameOver;
    public GameObject scoreUIText;
    bool onetime = false;
    public GameObject InternetConnPanel;


    // Player GOld/Diamond
    public Text Diamonds;
    public Text GoldCoin;

    // LEVEL2
    public GameObject Level2Enemy1Spawner;
    public GameObject Level2Enemy2Spawner;

    // LEVEL3
    public GameObject Level3Enemy1Spawner;
    public GameObject Level3Enemy2Spawner;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        Gameover
    }

    GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InternetpanelShowHide", 0, 2f);

        GMState = GameManagerState.Gameplay;
        SetGameManagerState(GMState);

        // player coins/diamonds
        int Player_prefs_DIAMONDS = PlayerPrefs.GetInt("DIAMOND"),
            Player_prefs_GOLDS = PlayerPrefs.GetInt("GOLD");

        Diamonds.GetComponent<Text>().text = Player_prefs_DIAMONDS.ToString();
        GoldCoin.GetComponent<Text>().text = Player_prefs_GOLDS.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("BossTag") != null)
        {
            enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
            Level1_Enemy2Spawner.GetComponent<Level1_Enemy2Spawner>().UnscheduleEnemySpawner();
            SmallRockSpawner.GetComponent<SmallRockSpawner>().UnscheduleSmallRockSpawner();
            BurstingStoneSpawner.GetComponent<Level1_BurstingStone_Spawner>().UnscheduleRockSpawner();
        }

        if (GameObject.FindGameObjectWithTag("Level2Boss") != null)
        {
            Level2Enemy1Spawner.GetComponent<Level2Enemy1Spawner>().UnscheduleEnemySpawner();
        }

        if (GameObject.FindGameObjectWithTag("Level3Boss") != null)
        {
            Level3Enemy1Spawner.GetComponent<Level3Enemy1Spawner>().UnscheduleEnemySpawner();
            Level3Enemy2Spawner.GetComponent<Level3Enemy2Spawner>().UnscheduleEnemySpawner();
        }
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


    //for updating game manager state
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Gameplay:
                //hide gameover
                gameOver.SetActive(false);
                // hide player bar
                playerHealthBar.SetActive(false);
                //reset score
                scoreUIText.GetComponent<GameScore>().Score = 0;
                //set player visible and init player lives
                if (PlayerPrefs.GetString("SELECTSHIP") == "ship1")
                {
                    playerShip.GetComponent<PlayerController>().Init();
                }
                else if (PlayerPrefs.GetString("SELECTSHIP") == "ship2")
                {
                    playerShip2.GetComponent<PlayerShip2Controller>().Init();
                }
                else if (PlayerPrefs.GetString("SELECTSHIP") == "ship3")
                {
                    playerShip3.GetComponent<PlayerShip3Controller>().Init();
                }
                else if (PlayerPrefs.GetString("SELECTSHIP") == "ship4")
                {
                    playerShip4.GetComponent<PlayerShip4Controller>().Init();
                }
                else
                {
                    playerShip.GetComponent<PlayerController>().Init();
                }

                //start eenmy spawner
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                Level1_Enemy2Spawner.GetComponent<Level1_Enemy2Spawner>().ScheduleEnemySpawner();
                //stop rock spawning
                SmallRockSpawner.GetComponent<SmallRockSpawner>().ScheduleSmallRockSpawner();
                // burst stone spawn
                BurstingStoneSpawner.GetComponent<Level1_BurstingStone_Spawner>().ScheduleRockSpawner();
                //hide gameover
                gameOver.SetActive(false);
                // show player bar
                playerHealthBar.SetActive(true);

                // fill healthbar
                PlayerHealthBar.SetHealthBarValue(1f);

                BossSpawner.GetComponent<BossSpawner>().Invoke("SpawnEnemy", 60);

                break;

            case GameManagerState.Gameover:
                //reset score
                scoreUIText.GetComponent<GameScore>().Score = 0;

                //update score in db
                PostScores(PlayerPrefs.GetString("USERNAME"), PlayerPrefs.GetInt("HIGHSCORE"));

                //stop enemy spawnig
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                Level1_Enemy2Spawner.GetComponent<Level1_Enemy2Spawner>().UnscheduleEnemySpawner();
                //stop rock spawning
                SmallRockSpawner.GetComponent<SmallRockSpawner>().UnscheduleSmallRockSpawner();
                //stop BurstingStoneSpawner
                BurstingStoneSpawner.GetComponent<Level1_BurstingStone_Spawner>().UnscheduleRockSpawner();
                //display gameover
                gameOver.SetActive(true);
                //change gamenamager state to opening after 8 secs
                Invoke("LoadMenuScene", 4f);
                // hide player bar after some time
                playerHealthBar.GetComponent<PlayerHealthBar>().Invoke("HideBarAfterTime", 2);

                //empty player healthbar
                PlayerHealthBar.SetHealthBarValue(PlayerHealthBar.GetHealthBarValue() - 0f);

                RemoveGameObjects("BossTag");


                //------------------LEVEL2------------------
                Level2Enemy1Spawner.GetComponent<Level2Enemy1Spawner>().UnscheduleEnemySpawner();
                Level2Enemy2Spawner.GetComponent<Level2Enemy2Spawner>().UnscheduleEnemySpawner();
                RemoveGameObjects("Level2Boss");
                RemoveGameObjects("Level2Enemy2");

                //------------------LEVEL3------------------
                Level3Enemy1Spawner.GetComponent<Level3Enemy1Spawner>().UnscheduleEnemySpawner();
                Level3Enemy2Spawner.GetComponent<Level3Enemy2Spawner>().UnscheduleEnemySpawner();
                RemoveGameObjects("Level3Boss");
                break;

        }
    }

    //for setting game state
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    //func to change game opening state
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void ResumeExitClickGame()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        GMState = GameManagerState.Opening;
        Invoke("UpdateGameManagerState", 1f);
    }


    public void RemoveGameObjects(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }


    private void PostScores(string name, int score)
    {
        StartCoroutine(DoPostScores(name, score));
    }
    IEnumerator DoPostScores(string name, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("update_leaderboard_ZErXPnRu6PI1ka2kjes2EvGUat8Q7fhN", "true");
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

}
