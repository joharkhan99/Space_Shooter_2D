using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRockSpawner : MonoBehaviour
{
    public GameObject SmallRock;        //referene small rokc to game object
    float maxSpawnrateInSecs = 10f;

    // Start is called before the first frame update
    void Start()
    {
/*        Invoke("SpawnRock", maxSpawnrateInSecs);
        //increase spawn rate every 20 secs
        InvokeRepeating("IncreaseSpawnRate", 0f, 20f);*/
    }

    // Update is called once per frame
    void Update()
    {
/*        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);*/
    }

    void SpawnRock()
    {
        //bottom left
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //top right
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //instantiate small rock
        GameObject single_rock = (GameObject)Instantiate(SmallRock);
        single_rock.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        //when to spawn nect small rock
        ScheduleNextRockSpawn();

    }

    void ScheduleNextRockSpawn()
    {
        //reset max spawn rate
        maxSpawnrateInSecs = 3f;

        float spawnInSecs;
        if (maxSpawnrateInSecs > 1f)
        {
            //pick num btw 1 and maxSpawnrateInSecs
            spawnInSecs = Random.Range(1f, maxSpawnrateInSecs);
        }
        else
        {
            spawnInSecs = 1f;
        }

        Invoke("SpawnRock", spawnInSecs);
    }

    //for increasing difficulty of game
    void IncreaseSpawnRate()
    {
        if (maxSpawnrateInSecs > 1f)
            maxSpawnrateInSecs-=2f;
        if (maxSpawnrateInSecs == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    //fucn to start enemy spawner
    public void ScheduleSmallRockSpawner()
    {
        Invoke("SpawnRock", maxSpawnrateInSecs);

        //increase spawn rate every 20 secs
        InvokeRepeating("IncreaseSpawnRate", 0f, 20f);
    }

    //func to stop spawning enemise
    public void UnscheduleSmallRockSpawner()
    {
        CancelInvoke("SpawnRock");
        CancelInvoke("IncreaseSpawnRate");
    }


}
