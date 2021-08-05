using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Star : MonoBehaviour
{
    //stars speed
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //star curr pos
        Vector2 position = transform.position;
        //calculate stars new pos
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        //update stars pos
        transform.position = position;

        //bottom-left
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //top-right
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //if star goes outside of screen on bottom, then posiytion star on top of screen and randomly btw left and right
        if (transform.position.y < min.y)
        {
            transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }
    }
}
