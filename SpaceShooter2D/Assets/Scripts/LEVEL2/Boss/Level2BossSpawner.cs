using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2BossSpawner : MonoBehaviour
{
    public GameObject enemy;
    float maxSpawnrateInSecs = 20f;
/*    public GameObject scoreUIText;*/
    public GameObject healthField;

    bool onetime = false;

    // Start is called before the first frame update
    void Start()
    {
/*        Invoke("SpawnEnemy",5f);*/
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));

        GameObject e = (GameObject)Instantiate(enemy);
        e.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }

    void ShowBossHealth()
    {
        healthField.SetActive(true);
    }
    public void HideBossHealth()
    {
        healthField.SetActive(false);
    }



}
