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
    [SerializeField] bool isLoadingIcon = false;
    [SerializeField] bool rotate = false;

    float elapsedTime = 0f;
    float elapsedTime2 = 0f;
    bool tickTime = false;

    void Awake()
    {
        Restart();
        if (isLoadingIcon)
        {
            currentAge = 100f;
            tickTime = true;
            lerpDuration = 5f;
        }else{
            lerpDuration = 2f;
        }
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
                if (isLoadingIcon)
                {
                    rotate = true;
                }
            }
            age.text = Mathf.Round(lerpedValue * 100f).ToString();
        }

        if (rotate)
        {
            elapsedTime2 += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime2 / lerpDuration);
            Vector3 newRotation = transform.eulerAngles;
            newRotation.z = Mathf.Lerp(0, 185, t);
            transform.eulerAngles = newRotation;

            if (t >= 1f)
            {
                Restart();
            }
        }
    }

    public void addAge(float t, float a)
    {
        currentAge = a;
        tickTime = true;
    }

    void Restart()
    {
        rotate = false;
        topFill.fillAmount = 1f;
        bottomFill.fillAmount = 0f;

        transform.rotation = Quaternion.Euler(Vector3.zero);
        topFill.fillAmount = 1f;
        bottomFill.fillAmount = 0f;

        elapsedTime = 0f;
        elapsedTime2 = 0f;
        oldAge = 0;

        if (isLoadingIcon)
        {
            tickTime = true;
        }
    }
}
