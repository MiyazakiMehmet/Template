using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int enemyNumber;
    //Will be added in the inspector
    public GameObject[] typeOfEnemies;
    public float spawnCooldown;
}

public class EnemySpawner : MonoBehaviour
{
    //To access class above in the inspector
    [SerializeField] private Wave[] waves; //List of every waves
    [SerializeField] private Transform[] spawnPoints;

    private Wave currentWave;
    private int currentWaveNumber;

    private float timeCounter = 0f;
    private bool canSpawn = true;

    void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
    }

    void SpawnWave()
    {
        if (canSpawn && Time.time > timeCounter + currentWave.spawnCooldown)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpawnPoint.position, Quaternion.identity);
            currentWave.enemyNumber--; //Spawn the next enemy
            if(currentWave.enemyNumber <= 0) //if indicated number of enemies has spawned
            {
                canSpawn = false;
            }
            timeCounter = Time.time;
        }
    }
}
