using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Enemy2Controller : MonoBehaviour
{
    GameObject scoreUIText;

    public GameObject Explosion;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2.5f;
        //get the score text UI
        scoreUIText = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //detect colliion with plaeyrship or playerbullet
        if (col.tag == "Player" || col.tag == "PlayerBullet" || col.tag == "PlayerShip2" || col.tag == "PlayerShip2Bullet"
             || col.tag == "PlayerShip3" || col.tag == "PlayerShip3Bullet" || col.tag == "PlayerShip4" || col.tag == "PlayerShip4Bullet")
        {
            PlayExplosion();

            //add 20 point s to score
            scoreUIText.GetComponent<GameScore>().Score += 7;
            //also destroy
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
