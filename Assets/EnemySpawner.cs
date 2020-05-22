using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject enemyPrefab;
    public float secondsBetweenSpawns;
    private float secondsSinceLastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceLastSpawn = 0;
        
    }

    // fixed update ensures that things happen at exact times for every player, so is important for gameplay critical things
    private void FixedUpdate()
    {
        secondsSinceLastSpawn += Time.deltaTime;

        if (secondsSinceLastSpawn >= secondsBetweenSpawns)
        {
            Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            secondsSinceLastSpawn = 0;
        }
    }
}
