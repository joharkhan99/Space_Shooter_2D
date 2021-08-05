using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float SpawnRandomFactor = 0.5f;
    [SerializeField] int numberOfEnemies = 2;
    [SerializeField] float moveSpeed = 0.02f;

    public GameObject getEnemyPrefab() { return enemyPrefab; }

    public List<Transform> getWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWayPoints.Add(child);
        }

        return waveWayPoints;
    }

    public float getTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float getSpawnRandomFactor() { return SpawnRandomFactor; }
    public int getNumberOfEnemies() { return numberOfEnemies; }
    public float getMoveSpeed() { return moveSpeed; }

}
