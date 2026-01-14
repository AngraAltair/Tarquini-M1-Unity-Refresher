using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public int enemyTotalWaves;
    public int enemiesPerWave;
    public GameObject enemyPrefab;
    public Transform enemyParent;
    private List<GameObject> enemies;
    private int enemyWave;

    public float minX;
    public float maxX;
    public float maxZ;

    // public float minY;
    private Vector3 spawnerPosition;

    private Coroutine spawnCoroutine;
    // Spawn enemies in waves. A wave will have a certain amount of enemies; when all enemies in a wave are defeated, move to the next wave
    // Start is called before the first frame update
    void Start()
    {
        enemyWave = 1;
        enemies = new List<GameObject>();
        spawnerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        spawnCoroutine = StartCoroutine(SpawnEnemyWaves());
        // SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        // if (enemies.Count == 0 && enemyWave != enemyTotalWaves)
        // {
        //     Debug.Log("Wave finished. Spawning more enemies.");
        // }
        
        if (enemyWave > enemyTotalWaves && enemies.Count == 0)
        {
            StopCoroutine(spawnCoroutine);
            Debug.Log("All enemy waves completed. Spawn coroutine stopped.");
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(Random.Range(minX, maxX),1,Random.Range(spawnerPosition.z, maxZ)),transform.rotation, enemyParent);
            newEnemy.GetComponent<EnemyBehavior>().SetRandomSpeed();
            enemies.Add(newEnemy);
            Debug.Log("Spawned enemy");
        }
    }

    IEnumerator SpawnEnemyWaves()
    {
        while (enemyWave <= enemyTotalWaves)
        {
            // SpawnEnemies();
            // enemyWave++;
            // if (enemies.Count == 0) {
            //     Debug.Log("Wave finished. Spawning more enemies in 5 seconds.");
            //     yield return new WaitForSeconds(5);
            // }
            for (int i = 0; i < enemyTotalWaves; i++)
            {
                SpawnEnemies();
                Debug.Log("Wave " + enemyWave + " spawned. Waiting for all enemies to be defeated.");
                enemyWave++;

                while (enemyParent.childCount > 0)
                {
                    yield return null;
                }
            }
        }
    }
}
