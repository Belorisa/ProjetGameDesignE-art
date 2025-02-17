using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemy : MonoBehaviour
{

    public GameObject[] waypoints;
    


    public Wave[] waves;
    public int timeBetweenWaves = 5;
    

    private float lastSpawnTime;
    private float lastSpawnTime2;
    
    private int enemiesSpawned = 0;
    private int enemiesSpawned2 = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(testEnnemy).GetComponent<WaypointEnnemy>().waypoints = waypoints;

        lastSpawnTime = Time.time;
        lastSpawnTime2 = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        int currentWave = 0;
        if (currentWave < waves.Length)
        {
            float timeInterval = Time.time - lastSpawnTime;
            float timeInterval2 = Time.time - lastSpawnTime2;

            if (waves[currentWave].prefabEnemy != null && waves[currentWave].prefabEnemy2 != null)
            {
                float spawnInterval = waves[currentWave].interval;
                float spawnInterval2 = waves[currentWave].interval2;
                
                if ((enemiesSpawned == 0 && timeInterval > timeBetweenWaves || timeInterval > spawnInterval) &&
                    enemiesSpawned < waves[currentWave].maxEnemies)
                {
                    lastSpawnTime = Time.time;
                    GameObject newEnemy = Instantiate(waves[currentWave].prefabEnemy);
                    newEnemy.GetComponent<WaypointEnnemy>().waypoints = waypoints;
                    enemiesSpawned++;
                }

                if ((enemiesSpawned2 == 0 && timeInterval2 > timeBetweenWaves || timeInterval2 > spawnInterval2) &&
                    enemiesSpawned2 < waves[currentWave].maxEnemies2)
                {
                    lastSpawnTime2 = Time.time;
                    GameObject newEnemy = Instantiate(waves[currentWave].prefabEnemy2);
                    newEnemy.GetComponent<WaypointEnnemy>().waypoints = waypoints;
                    enemiesSpawned2++;
                }

                if (enemiesSpawned == waves[currentWave].maxEnemies &&
                    GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    enemiesSpawned = 0;
                    enemiesSpawned2 = 0;
                    lastSpawnTime = Time.time;
                    lastSpawnTime2 = Time.time;
                }
            }

            if (waves[currentWave].prefabEnemy != null && waves[currentWave].prefabEnemy2 == null)
            {
                float spawnInterval = waves[currentWave].interval;
                
                if ((enemiesSpawned == 0 && timeInterval > timeBetweenWaves || timeInterval > spawnInterval) &&
                    enemiesSpawned < waves[currentWave].maxEnemies)
                {
                    lastSpawnTime = Time.time;
                    GameObject newEnemy = Instantiate(waves[currentWave].prefabEnemy);
                    newEnemy.GetComponent<WaypointEnnemy>().waypoints = waypoints;
                    enemiesSpawned++;
                }
                
                if (enemiesSpawned == waves[currentWave].maxEnemies &&
                    GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    enemiesSpawned = 0;
                    lastSpawnTime = Time.time;
                }
            }
            
        }
    }
}

[System.Serializable]
public class Wave
{
    public GameObject prefabEnemy;
    public float interval = 2;
    public int maxEnemies = 10;
    
    public GameObject prefabEnemy2;
    public float interval2 = 2;
    public int maxEnemies2 = 10;
}
