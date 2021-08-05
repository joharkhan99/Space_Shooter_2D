using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    public WaveConfig WaveConfig;
    List<Transform> wayPoints;
    int wayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        wayPoints = WaveConfig.getWayPoints();
        transform.position = wayPoints[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.WaveConfig = waveConfig;
    }

    private void Move()
    {
        if (wayPointIndex <= wayPoints.Count - 1)
        {
            var targetPos = wayPoints[wayPointIndex].transform.position;
            var movementThisFrame = WaveConfig.getMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movementThisFrame);
            if (transform.position == targetPos)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
