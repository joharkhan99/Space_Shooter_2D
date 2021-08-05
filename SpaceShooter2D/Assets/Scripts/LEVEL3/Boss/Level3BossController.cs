using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3BossController : MonoBehaviour
{
    int BossHealth;
    public GameObject Explosion;
    public GameObject EndingExplosion;

    //healthbar
    public static float healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar <= 0)
        {
            PlayEndingExplosion();
            Destroy(gameObject);

            PlayerPrefs.SetInt("DIAMOND", PlayerPrefs.GetInt("DIAMOND") + 20);
            PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD") + 250);

            //start another level enemies
            /*            GameObject.FindGameObjectWithTag("Level2Enemy1Spawner").GetComponent<Level2Enemy1Spawner>().ScheduleEnemySpawner();
                        GameObject.FindGameObjectWithTag("Level2Enemy2Spawner").GetComponent<Level2Enemy2Spawner>().ScheduleEnemySpawner();
                        GameObject.FindGameObjectWithTag("BurstingStoneSpawner").GetComponent<Level1_BurstingStone_Spawner>().ScheduleRockSpawner();*/
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerBullet")
        {
            healthBar -= 0.001f;
            PlayExplosion();
        } else if (col.tag == "PlayerShip2Bullet")
        {
            healthBar -= 0.002f;
            PlayExplosion();
        } else if (col.tag == "PlayerShip3Bullet")
        {
            healthBar -= 0.0025f;
            PlayExplosion();
        } else if (col.tag == "PlayerShip4Bullet")
        {
            healthBar -= 0.001f;
            PlayExplosion();
        }
    }


    //for instantiatinbg explosion 
    void PlayExplosion()
    {
        GameObject exp = (GameObject)Instantiate(Explosion);
        exp.transform.position = transform.position;
    }

    //for ending explosion 
    void PlayEndingExplosion()
    {
        GameObject exp = (GameObject)Instantiate(EndingExplosion);
        exp.transform.position = transform.position;
    }
}
