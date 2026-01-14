using UnityEditor.EditorTools;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int enemySpeed;
    public int enemyMinSpeed;
    public int enemyMaxSpeed;

    [Tooltip("If true, the enemy will walk towards the player. Default is false.")]
    public bool walkOn = false;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    public void SetRandomSpeed()
    {
        enemySpeed = Random.Range(enemyMinSpeed, enemyMaxSpeed);
    }
    
    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Projectile"))
    //     {
    //         // scoreManager.SendMessage("updateScore", scoreValue);
    //         Debug.Log("Score!");
    //         Destroy(gameObject);
    //     }
    // }

    void Walk()
    {
        if (walkOn)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, 1, player.transform.position.z), enemySpeed * Time.deltaTime);
        }
    }

    void KilledByBullet()
    {
        Debug.Log("Enemy killed by bullet");
        Destroy(gameObject);
    }
}
