using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    public AudioSource audioSource;
    public AudioClip enemyScreamClip;
    public AudioClip lazerShootClip;
    public AudioClip jumpClip;
    public AudioClip timeWarpClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void PlayEnemyScream()
    {
        audioSource.pitch = Random.Range(0.6f, 1.4f);
        audioSource.volume = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(enemyScreamClip);
        audioSource.pitch = 1f;
        audioSource.volume = 1f;
    }

    public void PlayLazerShoot()
    {
        audioSource.pitch = Random.Range(0.6f, 1.4f);
        audioSource.volume = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(lazerShootClip);
        audioSource.pitch = 1f;
        audioSource.volume = 1f;
    }

    public void PlayJumpSound()
    {
        audioSource.pitch = Random.Range(0.6f, 1.4f); 
        audioSource.volume = Random.Range(0.8f, 1f); // Slight variation
        audioSource.PlayOneShot(jumpClip);
        audioSource.pitch = 1f;
        audioSource.volume = 1f; // Reset to default volume
    }

    public void TimeWarpSound()
    {
        audioSource.pitch = Random.Range(0.6f, 1.4f);
        audioSource.volume = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(timeWarpClip);
        audioSource.pitch = 1f;
        audioSource.volume = 1f;
    }

}

