using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_Enemy2Spawner : MonoBehaviour
{
    public GameObject enemy;
    float maxSpawnrateInSecs = 30f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        //bottom left
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //top right
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //instantiate enemy
        GameObject single_enemy = (GameObject)Instantiate(enemy);
        single_enemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        //when to spawn nect enemy
        SchedulenextEnemySpawn();

    }
    void SchedulenextEnemySpawn()
    {
        //reset max spawn rate
        maxSpawnrateInSecs = 5f;

        float spawnInSecs;
        if (maxSpawnrateInSecs > 1f)
        {
            //pick num btw 1 and maxSpawnrateInSecs
            spawnInSecs = Random.Range(4f, maxSpawnrateInSecs);
        }
        else
        {
            spawnInSecs = 5f;
        }

        Invoke("SpawnEnemy", spawnInSecs);
    }

    //for increasing difficulty of game
    void IncreaseSpawnRate()
    {
        if (maxSpawnrateInSecs > 1f)
            maxSpawnrateInSecs--;
        if (maxSpawnrateInSecs == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    //fucn to start enemy spawner
    public void ScheduleEnemySpawner()
    {
        Invoke("SpawnEnemy", maxSpawnrateInSecs);

        //increase spawn rate every 20 secs
        InvokeRepeating("IncreaseSpawnRate", 0f, 10f);
    }

    //func to stop spawning enemise
    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
