using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip4Bullet : MonoBehaviour
{
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        //bullet curr pos
        Vector2 position = transform.position;

        //calculate bullet new pos
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        //update bullet pos
        transform.position = position;

        //top-right of screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //if bullet goes out of top screen
        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        //detect colliion with ebemyship or enemybullet
        if (col.tag == "Enemy" || col.tag == "SmallRock" || col.tag == "Level1_BurstingStoneTag" || col.tag == "BossTag" || col.tag == "level1_Enemy2" || col.tag == "Level2Enemy1" || col.tag == "Level2Enemy2" || col.tag == "Level3Enemy1" || col.tag == "Level3Enemy2" || col.tag == "Level3Boss")
        {
            //destroy this player bullet
            Destroy(gameObject);
        }
    }
}
