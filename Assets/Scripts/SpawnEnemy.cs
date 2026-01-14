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
    // Spawn enemies in waves. A wave will have a certain amount of enemies; when all enemies in a wave are defeated, move to the next wave
    // Start is called before the first frame update
    void Start()
    {
        enemyWave = 1;
        enemies = new List<GameObject>();
        spawnerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count == 0 && enemyWave != enemyTotalWaves)
        {
            Debug.Log("Wave finished. Spawning more enemies.");
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
}
