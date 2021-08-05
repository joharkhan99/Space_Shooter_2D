using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_Enemy2Bullet : MonoBehaviour
{
    float speed;
    Vector2 _direction; //dir of fire/bullet
    bool isReady;       //to know when bulet dir is set

    void Awake()
    {
        speed = 5f;
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
            Vector2 pos = transform.position;
            //compute new pos
            pos += _direction * speed * Time.deltaTime;
            //update bullet pos
            transform.position = pos;

            //if bullet goes out of screen, destroy it
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));  //bottom left
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));  //top right

            if (transform.position.x < min.x || transform.position.x > max.x || transform.position.y < min.y || transform.position.y > max.y)
            {
                Destroy(gameObject);
            }

        }
    }

    public void SetDirection(Vector2 dir)
    {
        _direction = dir.normalized;    //normalized for getting a unit vector
        isReady = true;
    }

}
