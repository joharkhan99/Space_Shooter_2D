using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laser : MonoBehaviour
{
    private LineRenderer line;
    public Transform laserHit;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        line.useWorldSpace = true;
    }

    void update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up);
        Debug.DrawLine(transform.position, hit.point);
        laserHit.position = hit.point;
    }

}
