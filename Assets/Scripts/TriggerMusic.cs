using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMusic : MonoBehaviour
{
    [SerializeField] int musicToTrigger;
    [SerializeField] MusicTriggerController musicTriggerController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (musicToTrigger == 0)
            {
                musicTriggerController.RestartTriggers();
            }
            GameObject.FindGameObjectWithTag("MusicManager").GetComponent<BackgroundSoundManager>().PlaySound(musicToTrigger);
            gameObject.SetActive(false);
        }
    }
}
