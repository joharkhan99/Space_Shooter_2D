using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject Star; //reference to star
    public int MaxStars;        //max stars
    //colors arrray
    Color[] StarColors = {
        new Color(0.5f, 0.5f, 0.8f),  //blue
        new Color(0f, 0.6f, 0.5f),  //green
        new Color(1f, 1f, 0f),  //yellow
        new Color(0.7f, 0f, 0f),  //reed    
        new Color(0.91f, 0.3f, 0.21f),
        new Color(0.2f, 0.2f, 0.2f),
    };

    // Start is called before the first frame update
    void Start()
    {
        //bottom-left
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //top-right
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        for(int i = 0; i < MaxStars; ++i)
        {
            GameObject star = (GameObject)Instantiate(Star);
            //set star color
            star.GetComponent<SpriteRenderer>().color = StarColors[i % StarColors.Length];
            //set star random pos
            star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            //set star random speed
            star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);
            //make star a child of Star (GameObject) at top
            star.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
