using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip temp;
    public static BackgroundSoundManager instance;
    AudioSource audioSource;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = temp;
        audioSource.Play();
    }


}
