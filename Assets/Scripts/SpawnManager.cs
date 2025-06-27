using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

   [SerializeField] List<Transform> spawnPoints;
   
    public void SetSpawnPoints(List<Transform> points)
    {
        spawnPoints = points;
    }
    public Transform GetRandomSpawnPoint()
    {
        if (spawnPoints == null || spawnPoints.Count == 0) { return null; }
        Transform a = spawnPoints[Random.Range(0, spawnPoints.Count)];
        spawnPoints.Remove(a);
        return a;
    }
}
