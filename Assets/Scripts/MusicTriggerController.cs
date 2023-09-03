using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTriggerController : MonoBehaviour
{
    [SerializeField] GameObject babyTrigger, teenTrigger, adultTrigger, oldTrigger;

    void Awake()
    {
        RestartTriggers();
    }

    public void RestartTriggers()
    {
        babyTrigger.SetActive(true);
        teenTrigger.SetActive(true);
        adultTrigger.SetActive(true);
        oldTrigger.SetActive(true);
    }

}
