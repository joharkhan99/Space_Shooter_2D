using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2BossGun : MonoBehaviour
{
    public GameObject EnemyBullet;  //enemy's gun bullet in prefab

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("fire", 0f, 5f);
        InvokeRepeating("stopFire", 1f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FireEnemyBullet()
    {
        GameObject PlayerShip = GameObject.Find("Player");
        GameObject PlayerShip2 = GameObject.Find("PlayerShip2");
        GameObject PlayerShip3 = GameObject.Find("PlayerShip3");
        GameObject PlayerShip4 = GameObject.Find("PlayerShip4");

        if (PlayerShip != null || PlayerShip2 != null || PlayerShip3 != null || PlayerShip4 != null)  //if player not dead
        {
            //instantiate enemy bullet
            GameObject bullet = (GameObject)Instantiate(EnemyBullet);
            //set bullet's initial position
            bullet.transform.position = transform.position;
            //compute bullt direct towards playership
            Vector2 direction = -transform.up;
            //set bullet direction
            bullet.GetComponent<Level2BossBullet>().SetDirection(direction);
        }
    }

    IEnumerator test()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            InvokeRepeating("FireEnemyBullet", 0f, 0.05f);
            CancelInvoke("FireEnemyBullet");
        }
    }

    void fire()
    {
        InvokeRepeating("FireEnemyBullet", 0f, 0.05f);
    }

    void stopFire()
    {
        CancelInvoke("FireEnemyBullet");
    }

}
