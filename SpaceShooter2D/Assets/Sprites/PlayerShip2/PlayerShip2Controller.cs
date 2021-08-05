using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip2Controller : MonoBehaviour
{
    public GameObject GameManagerObject;
    public GameObject PlayerBullet;
    public GameObject bulletPosition;
    public GameObject Explosion;
    float LazerFiringPeriod = 0.2f;
    Coroutine Ship2FiringCoroutine;

    //player move
    private Vector3 mousePos;
    private Rigidbody2D rb;
    private float moveSpeed = 100f;
    public float speed;

    public void Init()
    {
        transform.position = new Vector2(0, 0);
        gameObject.SetActive(true);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ship2FiringCoroutine = StartCoroutine(Ship2FireContinuous());
            GetComponent<AudioSource>().Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(Ship2FiringCoroutine);
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);
    }

    IEnumerator Ship2FireContinuous()
    {
        while (true)
        {
            GetComponent<AudioSource>().Play();
            GameObject bullet = (GameObject)Instantiate(PlayerBullet);
            bullet.transform.position = bulletPosition.transform.position;
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
            GameManagerObject.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameover);
            gameObject.SetActive(false);
        }
    }


    void PlayExplosion()
    {
        GameObject exp = (GameObject)Instantiate(Explosion);
        exp.transform.position = transform.position;
    }
}
