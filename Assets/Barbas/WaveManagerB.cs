using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveManagerB : MonoBehaviour
{
    public GameObject gears;
    
    public GameObject[] waypoints;

    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private float lastSpawnTime;
    private float lastSpawnTime2;


    private int enemiesSpawned;
    private int enemiesSpawned2;

    public bool isRunning;
    public bool firstWaveDone = false;
    public bool waveDone = false;

    public  int currentWave = 0;
    public int nbrOfLoops;
    public float scalingPerLoops;

    void Awake()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        gears.GetComponent<Gears>().pathManager = gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(testEnnemy).GetComponent<WaypointEnnemy>().waypoints = waypoints;

        lastSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            Wave();

            if (gears.GetComponent<Gears>().tileManager.tilemapTrail.GetComponent<TilemapCollider2D>().isTrigger)
            {
                gears.GetComponent<Gears>().tileManager.tilemapTrail.GetComponent<TilemapCollider2D>().isTrigger =
                    false;
            }
        }
        else
        {
            if (!gears.GetComponent<Gears>().tileManager.tilemapTrail.GetComponent<TilemapCollider2D>().isTrigger)
            {
                gears.GetComponent<Gears>().tileManager.tilemapTrail.GetComponent<TilemapCollider2D>().isTrigger =
                    true;
            }
        }
    }

    public void Loop()
    {
        Time.timeScale = 1;
        
        Gears.gears.uIManager.GetComponent<UIManager>().loopPanel.SetActive(false);
        
        currentWave = 0;
            
        nbrOfLoops++;

        foreach (var wave in waves)
        {
            wave.maxEnemies = (int) (wave.maxEnemies * scalingPerLoops);

            wave.maxEnemies2 = (int) (wave.maxEnemies2 * scalingPerLoops);
        }
    }

    void Wave()
    {
        if (currentWave == waves.Length)
        {
            Gears.gears.uIManager.GetComponent<UIManager>().loopPanel.SetActive(true);
        }
        
        if (currentWave < waves.Length && isRunning) // vérification de la wave actuel et que la partie n'est pas en pause
        {
            float timeInterval = Time.time - lastSpawnTime;
            float timeInterval2 = Time.time - lastSpawnTime2;

            if (waves[currentWave].prefabEnemy != null && waves[currentWave].prefabEnemy2 != null ) //spawn de 2 ennemi
            {
                float spawnInterval = waves[currentWave].interval;
                float spawnInterval2 = waves[currentWave].interval2;

                if ((enemiesSpawned == 0 && timeInterval > timeBetweenWaves || timeInterval > spawnInterval) &&
                    enemiesSpawned < waves[currentWave].maxEnemies) // code a répéter pour chaque ennemi
                {
                    lastSpawnTime = Time.time;

                    Instantiate(waves[currentWave].prefabEnemy,new Vector3(waypoints[0].transform.position.x, waypoints[0].transform.position.y, 
                        waves[currentWave].prefabEnemy.transform.position.z), waves[currentWave].prefabEnemy.transform.rotation);
                    
                    enemiesSpawned++;
                }

                if ((enemiesSpawned2 == 0 && timeInterval2 > timeBetweenWaves || timeInterval2 > spawnInterval2) &&
                    enemiesSpawned2 < waves[currentWave].maxEnemies2)
                {
                    lastSpawnTime2 = Time.time;
                    Instantiate(waves[currentWave].prefabEnemy2,new Vector3(waypoints[0].transform.position.x, waypoints[0].transform.position.y, 
                        waves[currentWave].prefabEnemy2.transform.position.z), waves[currentWave].prefabEnemy2.transform.rotation);
                    
                    enemiesSpawned2++;
                }

                if (enemiesSpawned == waves[currentWave].maxEnemies &&
                    GameObject.FindGameObjectWithTag("Enemy") == null) // remise des valeur a zero pour la vague suivante et activation du trigger firstWavedone la première fois
                {
                    enemiesSpawned = 0;
                    enemiesSpawned2 = 0;
                    lastSpawnTime = Time.time;
                    lastSpawnTime2 = Time.time;
                    isRunning = false;

                    firstWaveDone = true;
                    waveDone = true;
                }
                
            }

            if (waves[currentWave].prefabEnemy != null && waves[currentWave].prefabEnemy2 == null ) // spawn de 1 ennemi
            {
                float spawnInterval = waves[currentWave].interval;

                if ((enemiesSpawned == 0 && timeInterval > timeBetweenWaves || timeInterval > spawnInterval) &&
                    enemiesSpawned < waves[currentWave].maxEnemies)
                {
                    lastSpawnTime = Time.time;
                    Instantiate(waves[currentWave].prefabEnemy,new Vector3(waypoints[0].transform.position.x, waypoints[0].transform.position.y, 
                        waves[currentWave].prefabEnemy.transform.position.z), waves[currentWave].prefabEnemy.transform.rotation);

                    enemiesSpawned++;
                }

                if (enemiesSpawned == waves[currentWave].maxEnemies &&
                    GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    enemiesSpawned = 0;
                    lastSpawnTime = Time.time;
                    isRunning = false;
                    
                    firstWaveDone = true;
                    waveDone = true;

                }

                
            }
        }
    }
}