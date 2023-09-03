using System.Collections;
using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip baby, teen, adult, old;
    [SerializeField] AudioClip overlayBaby, overlayTeen, overlayAdult;
    public static BackgroundSoundManager instance;
    [SerializeField] AudioSource audioSource, overlayAudioSource;
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
        overlayAudioSource.volume = 1;
        switch (i)
        {
            case 0:
                audioSource.clip = baby;
                overlayAudioSource.clip = overlayBaby;
                break;
            case 1:
                audioSource.clip = teen;
                overlayAudioSource.clip = overlayTeen;
                break;
            case 2:
                audioSource.clip = adult;
                overlayAudioSource.clip = overlayAdult;
                break;
            case 3:
                audioSource.clip = old;
                break;
        }

        audioSource.Play();
        if (i != 3)
        {
            overlayAudioSource.Play();
        }
    }


    float fadeDuration = 5f;
    bool fadeTime = false;
    float startTime = 0f;


    void Update()
    {
        if (fadeTime)
        {
            float elapsedTime = Time.time - startTime;
            float fadeFactor = 1f-(elapsedTime / fadeDuration);

            fadeFactor = Mathf.Clamp01(fadeFactor);
            audioSource.volume = fadeFactor;
            overlayAudioSource.volume = fadeFactor;

            if (fadeFactor <= 0)
            {
                fadeTime = false;
                PlayNextSound(currentlyPlaying);
            }
        }
    }
}
