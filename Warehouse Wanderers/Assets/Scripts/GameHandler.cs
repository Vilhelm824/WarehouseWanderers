using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public float minSpawnTime = 10f; // Minimum time between package spawns
    public float maxSpawnTime = 30f; // Maximum time between package spawns
    public GameObject cratePrefab; // Prefab of the crate object
    public float minXSpawn = -8f; // Minimum x-coordinate for package spawn
    public float maxXSpawn = 8f; // Maximum x-coordinate for package spawn
    public float minYSpawn = -5f; // Minimum y-coordinate for package spawn
    public float maxYSpawn = 5f; // Maximum y-coordinate for package spawn
    public int maxExplosions = 3;

    private float elapsedTime = 0f; // Time elapsed since last package spawn
    private float nextSpawnTime; // Time when the next package should spawn

    private int consecExploded = 0;

    private void Start()
    {
        CalculateNextSpawnTime();
        consecExploded = 0;
        Debug.Log("Reseting consecExploded to: " + consecExploded);
    }

    private void Update()
    {
        if(consecExploded >= maxExplosions)
        {
            GameEnd();
        }
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= nextSpawnTime)
        {
            SpawnPackage();
            CalculateNextSpawnTime();
        }
    }

    private void CalculateNextSpawnTime()
    {
        nextSpawnTime = Random.Range(elapsedTime + minSpawnTime, elapsedTime + maxSpawnTime);
    }

    private void SpawnPackage()
    {
        float xPos = Random.Range(minXSpawn, maxXSpawn);
        float yPos = Random.Range(minYSpawn, maxYSpawn);
        Vector3 spawnPos = new Vector3(xPos, yPos, 0f);
        Instantiate(cratePrefab, spawnPos, Quaternion.identity);
    }

    public void PackageExploded()
    {
        consecExploded++;
        Debug.Log("num exploded: " + consecExploded);
    }

    public void PackageDelivered()
    {
        consecExploded = 0;
    }

    private void GameEnd()
    {
        // End state
    }
}