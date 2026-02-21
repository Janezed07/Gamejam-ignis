using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    public static MenuMusicManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        // Singleton pattern (prevents duplicate music)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps music when scene changes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}

