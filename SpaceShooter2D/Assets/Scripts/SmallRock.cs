using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRock : MonoBehaviour
{
    public GameObject Explosion;
    float speed;
    GameObject scoreUIText;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        //get the score text UI
        scoreUIText = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        //rock curr pos
        Vector2 position = transform.position;

        //calculate rock new pos
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        //update rock pos
        transform.position = position;

        //bottom left of screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //spin rovk
        float speedRotate = 90.0f;
        transform.Rotate(Vector3.forward * speedRotate * Time.deltaTime);



        //if rock goes out of top screen
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //detect colliion with plaeyrship or playerbullet
        if (col.tag == "Player" || col.tag == "PlayerBullet" || col.tag == "PlayerShip2" || col.tag == "PlayerShip2Bullet"
             || col.tag == "PlayerShip3" || col.tag == "PlayerShip3Bullet" || col.tag == "PlayerShip4" || col.tag == "PlayerShip4Bullet")
        {
            PlayExplosion();

            //add 20 point s to score
            scoreUIText.GetComponent<GameScore>().Score += 4;

            Destroy(gameObject);
        }
    }

    //for instantiatinbg explosion 
    void PlayExplosion()
    {
        GameObject exp = (GameObject)Instantiate(Explosion);
        exp.transform.position = transform.position;
    }

}
