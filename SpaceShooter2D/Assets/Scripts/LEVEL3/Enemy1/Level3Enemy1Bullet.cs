using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Enemy1Bullet : MonoBehaviour
{
    float speed;
    Vector2 _direction;
    bool isReady;       //wether tokno if bullet dir ready

    void Awake()
    {
        speed = 10f;
        isReady = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            //bullet curr pos
            Vector2 position = transform.position;

            //calculate bullt new pos
            position += _direction * speed * Time.deltaTime;

            //update bullet pos
            transform.position = position;

            //remove bullt if it goes out of screen
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));  //bottom left
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));  //top right

            //check if bullt goes out-> destroy it
            if (transform.position.x < min.x || transform.position.x > max.x || transform.position.y < min.y || transform.position.y > max.y)
            {
                Destroy(gameObject);
            }

        }

    }

    public void SetDirection(Vector2 direc)
    {
        //set direction normalized to get a unit vector
        _direction = direc.normalized;

        isReady = true; //set flag to true
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //detect enemybullet
        if (col.tag == "Player" || col.tag == "PlayerShip2" || col.tag == "PlayerShip3" || col.tag == "PlayerShip4")
        {
            //destroy enemy bullet
            Destroy(gameObject);
        }
    }

}
