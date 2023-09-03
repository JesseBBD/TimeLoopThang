using System.Collections;
using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip baby, teen, adult, old;
    public static BackgroundSoundManager instance;
    [SerializeField] AudioSource audioSource;
    int currentlyPlaying = 0;

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

        audioSource.clip = baby;
        audioSource.Play();
    }

    public void PlaySound(int i)
    {
        if (i != currentlyPlaying)
        {
            currentlyPlaying = i;
            startTime = Time.deltaTime;
            fadeTime = true;
        }
    }

    void PlayNextSound(int i)
    {
        audioSource.volume = 1;
        switch (i)
        {
            case 0:
                audioSource.clip = baby;
                break;
            case 1:
                audioSource.clip = teen;
                break;
            case 2:
                audioSource.clip = adult;
                break;
            case 3:
                audioSource.clip = old;
                break;
        }

        audioSource.Play();
    }


    float fadeDuration = 5f;
    bool fadeTime = false;
    float startTime = 0f;


    void Update()
    {
        if (fadeTime)
        {
            float elapsedTime = Time.time - startTime;
            float fadeFactor = 1f - (elapsedTime / fadeDuration);

            if (fadeFactor < 0)
            {
                fadeTime = false;
                PlayNextSound(currentlyPlaying);
            }
            else
            {
                audioSource.volume = fadeFactor;
            }
        }
    }
}
