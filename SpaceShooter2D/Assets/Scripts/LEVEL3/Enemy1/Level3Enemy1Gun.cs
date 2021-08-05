using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Enemy1Gun : MonoBehaviour
{
    public GameObject EnemyBullet;  //enemy's gun bullet in prefab

    // Start is called before the first frame update
    void Start()
    {
        //fire an enemy bulet after 1 sec
        Invoke("FireEnemyBullet", 1f);
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
            Vector2 direction = bullet.transform.position;
            if (PlayerShip)
            {
                direction = PlayerShip.transform.position - bullet.transform.position;
            }
            else if (PlayerShip2)
            {
                direction = PlayerShip2.transform.position - bullet.transform.position;
            }
            else if (PlayerShip3)
            {
                direction = PlayerShip3.transform.position - bullet.transform.position;
            }
            else if (PlayerShip4)
            {
                direction = PlayerShip4.transform.position - bullet.transform.position;
            }

            //set bullet direction
            bullet.GetComponent<Level3Enemy1Bullet>().SetDirection(direction);
        }

    }
}
