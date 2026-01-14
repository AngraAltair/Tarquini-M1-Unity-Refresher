using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource enemyAudioSource;
    public AudioClip enemyHitClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioClip(AudioClip clip)
    {
        enemyAudioSource.PlayOneShot(clip);
    }
}
