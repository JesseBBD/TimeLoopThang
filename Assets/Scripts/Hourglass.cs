using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hourglass : MonoBehaviour
{
    [SerializeField] TMP_Text age;
    [SerializeField] Image topFill, bottomFill, sand;
    [SerializeField] float maxAge = 100f;
    [SerializeField] float currentAge = 0f;
    [SerializeField] float oldAge = 0f;
    [SerializeField] float lerpDuration = 2f;

    float elapsedTime = 0f;
    bool tickTime = false;

    void Awake()
    {
        topFill.fillAmount = 1f;
        bottomFill.fillAmount = 0f;
    }

    void Update()
    {
        if (tickTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / lerpDuration);
            float lerpedValue = Mathf.Lerp(oldAge / 100f, currentAge / 100f, t);

            bottomFill.fillAmount = lerpedValue;
            topFill.fillAmount = 1f - bottomFill.fillAmount;

            sand.fillAmount = 1f;
            sand.material.mainTextureOffset = new Vector2(0, t);

            if (t >= 1f)
            {
                oldAge = currentAge;
                tickTime = false;
            }
            age.text = Mathf.Round(lerpedValue*100f).ToString();
        }
    }

    public void addAge(float t, float a)
    {
        currentAge = a;
        tickTime = true;
    }
}
