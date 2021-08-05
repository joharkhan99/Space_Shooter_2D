using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject GameManagerObject;      //reference t game manager

    public GameObject PlayerBullet;
    public GameObject bulletPosition;
    public GameObject Explosion;
    float LazerFiringPeriod = 0.08f;
    Coroutine FiringCoroutine;

    //player move
    private Vector3 mousePos;
    private Rigidbody2D rb;
    private float moveSpeed = 100f;

    public float speed;

    GameObject ScoreText;


    public void Init()
    {
        //reset plyaer pos to cenetre when game starts
        transform.position = new Vector2(0, 0);
        //set this player game object to active
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //fire bullets on muse click
        if (Input.GetMouseButtonDown(0))
        {
           FiringCoroutine = StartCoroutine(FireContinuous());
            GetComponent<AudioSource>().Play();
        }
        //fire bullets on muse click
        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(FiringCoroutine);
        }

        float x = Input.GetAxis("Horizontal");   //val will be -1,0 or 1 for (left,no input, right)
        float y = Input.GetAxis("Vertical");     //val will be -1,0 or 1 for (down,no input, up)

        Vector2 direction = new Vector2(x, y).normalized;

        // call func to set player position
        Move(direction);
    }

    IEnumerator FireContinuous()
    {
        while (true)
        {
        GetComponent<AudioSource>().Play();
        //instantiate first bullet
        GameObject bullet = (GameObject)Instantiate(PlayerBullet);
        bullet.transform.position = bulletPosition.transform.position;  //set bullet initial pos
        yield return new WaitForSeconds(LazerFiringPeriod);
        }
    }


    void Move(Vector2 direction)
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //detect colliion with ebemyship or enemybullet
        switch (col.tag)
        {
            case "SmallRock":
                PlayExplosion();
                PlayerHealthBar.SetHealthBarValue(PlayerHealthBar.GetHealthBarValue() - 0.05f);
                break;

            case "Level1_BurstingStoneTag":
                PlayExplosion();
                PlayerHealthBar.SetHealthBarValue(PlayerHealthBar.GetHealthBarValue() - 0.07f);
                break;

            case "Enemy":
            case "EnemyBullet":
                PlayExplosion();
                PlayerHealthBar.SetHealthBarValue(PlayerHealthBar.GetHealthBarValue() - 0.1f);
                break;

            case "level1_Enemy2":
            case "Level1_Enemy2Bullet":
                PlayExplosion();
                PlayerHealthBar.SetHealthBarValue(PlayerHealthBar.GetHealthBarValue() - 0.15f);
                break;
            
            case "BossTag":
            case "BossBullet":
                PlayExplosion();
                PlayerHealthBar.SetHealthBarValue(PlayerHealthBar.GetHealthBarValue() - 0.2f);
                break;
                
                //---------------------------LEVEL2
            case "Level2Enemy1":
            case "Level2Enemy1Bullet":
                PlayExplosion();
                PlayerHealthBar.SetHealthBarValue(PlayerHealthBar.GetHealthBarValue() - 0.06f);
                break;

            case "Level2Enemy2":
            case "Level2Enemy2Bullet":
                PlayExplosion();
                PlayerHealthBar.SetHealthBarValue(PlayerHealthBar.GetHealthBarValue() - 0.09f);
                break;

            case "Level2Boss":
            case "Level2BossBullet":
                PlayExplosion();
                PlayerHealthBar.SetHealthBarValue(PlayerHealthBar.GetHealthBarValue() - 0.04f);
                break;

            //---------------------------LEVEL3
            case "Level3Enemy1":
            case "Level3Enemy1Bullet":
                PlayExplosion();
                PlayerHealthBar.SetHealthBarValue(PlayerHealthBar.GetHealthBarValue() - 0.08f);
                break;

            case "Level3Enemy2":
            case "Level3Enemy2Bullet":
                PlayExplosion();
                PlayerHealthBar.SetHealthBarValue(PlayerHealthBar.GetHealthBarValue() - 0.095f);
                break;

            case "Level3Boss":
            case "Level3BossBullet":
                PlayExplosion();
                PlayerHealthBar.SetHealthBarValue(PlayerHealthBar.GetHealthBarValue() - 0.06f);
                break;
        }

        if (PlayerHealthBar.GetHealthBarValue() <= 0)
        {
             PlayerHealthBar.SetHealthBarValue(PlayerHealthBar.GetHealthBarValue() - 0f);
            //change game maanger state
            GameManagerObject.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameover);
            //hide the players ship
            gameObject.SetActive(false);
        }
        else
        {
        }

    }
    

    //for instantiatinbg explosion 
    void PlayExplosion()
    {
        GameObject exp = (GameObject)Instantiate(Explosion);
        exp.transform.position = transform.position;
    }


}
