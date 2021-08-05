using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
public class Level2BossBezierFollow : MonoBehaviour
{
    float maxValue; // or whatever you want the max value to be
    float minValue; // or whatever you want the min value to be
    float currentValue = 0; // or wherever you want to start
    float direction = 0.1f;

    void Start()
    {
maxValue = Camera.main.orthographicSize-2f; // or whatever you want the max value to be
minValue = -Camera.main.orthographicSize + 2f; // or whatever you want the min value to be
    }

    void Update()
    {
        StartCoroutine("test");
    }

    IEnumerator test()
    {
        currentValue += Time.deltaTime * direction;
        if (currentValue >= maxValue)
        {
            direction *= -1;
            currentValue = maxValue;
        }
        else if (currentValue <= minValue)
        {
            direction *= -1;
            currentValue = minValue;
        }
        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector3(currentValue, 3.86f, 1);
    }


    /*[SerializeField] private Transform[] routes;        //total routes/paths
    private int routeToGo;      //index of next route/curve
    private float tParam;       //formula variable -> used by formula
    private Vector2 bossPos;    //boss pos and will be calculated by formula
    private float speedModifier;    //move speed of boss
    private bool cotoutineAllowed;  //will help us not run a coroutine when any another coroutine is running

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.0001f;
        cotoutineAllowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        *//*        if (cotoutineAllowed)*//*
        StartCoroutine(GoByTheRoute(routeToGo));
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        cotoutineAllowed = false;       //set to false so that new coroutine will not be started

        //points in route
        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            //formula for posirion
            bossPos = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = bossPos;
            yield return new WaitForSeconds(5f);

            yield return new WaitForEndOfFrame();




        }

        tParam = 0f;    //=0 so that next coroutine starts from zero points (p0)
        routeToGo += 1; //move to next route

        if (routeToGo > routes.Length - 1)  //if boss completes routes then go back to zero
            routeToGo = 0;

        cotoutineAllowed = true;        //make this true so that new coroutine is started

    }*/
}
