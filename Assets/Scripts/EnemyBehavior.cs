using System.Collections;
using UnityEditor.EditorTools;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBehavior : MonoBehaviour
{
    // public AudioSource enemyAudioSource;
    // public AudioClip enemyHitClip;

    public int enemySpeed;
    public int enemyMinSpeed;
    public int enemyMaxSpeed;

    [Tooltip("If true, the enemy will walk towards the player. Default is false.")]
    public bool walkOn = false;

    private GameObject player;
    private GameObject projectileSpawner;
    private Rigidbody rb;
    private bool isShot = false;

    private Coroutine destroyCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        projectileSpawner = GameObject.FindGameObjectWithTag("ProjectileSpawner");
        rb = GetComponent<Rigidbody>();
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

    void Walk()
    {
        if (walkOn && player != null && !isShot)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, 1, player.transform.position.z), enemySpeed * Time.deltaTime);
        }
    }

    void TurnWalkOff()
    {
        walkOn = false;
    }

    void ShotByBullet()
    {
        // Debug.Log("Enemy killed by bullet");
        // Destroy(gameObject);
        Vector3 forceVelocity = projectileSpawner.GetComponent<spawnBall>().initialVelocity * 2;
        rb.AddForce(forceVelocity, ForceMode.Impulse);
        isShot = true;
        // PlayAudioClip(enemyHitClip);
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null) {
            audioManager.PlayAudioClip(audioManager.enemyHitClip);
        }
        // enemyAudioSource.PlayOneShot(enemyHitClip);
        Debug.Log("Enemy shot back by bullet with force: " + forceVelocity + ". Is shot = " + isShot);

        if (destroyCoroutine == null)
        {
            destroyCoroutine = StartCoroutine(DestroyAfterShot());
        }
    }

    // void PlayAudioClip(AudioClip clip)
    // {   
    //     if (enemyAudioSource == null || clip == null)
    //     {
    //         Debug.LogWarning("EnemyBehavior: Cannot play audio clip - AudioSource or clip is null.");
    //         return;
    //     } else {
    //         Debug.Log("EnemyBehavior: Playing audio clip.");
    //         enemyAudioSource.PlayOneShot(clip);
    //     }
    // }

    IEnumerator DestroyAfterShot()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Enemy destroyed after being shot");
        Destroy(gameObject);
    }
}
