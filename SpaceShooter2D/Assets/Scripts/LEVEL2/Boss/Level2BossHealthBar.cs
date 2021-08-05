using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2BossHealthBar : MonoBehaviour
{
    Quaternion rotation;
    Vector3 localScale;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = Level2BossController.healthBar;
        transform.localScale = localScale;
    }
    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
    }
}
