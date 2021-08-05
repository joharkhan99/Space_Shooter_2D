using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //explosion animation will call this func at the end to destroy Explosion
    void DestroyGameObject() {
        Destroy(gameObject);
    }

}
