using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Hourglass : MonoBehaviour
{
    [SerializeField] TMP_Text age;
    [SerializeField] Image topFill, bottomFill, sand;
    [SerializeField] PlayerManager playerManager;
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
        }
        else
        {
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

        if (!isLoadingIcon)
        {
            currentAge += Time.deltaTime / 10f;
            if (currentAge > 100)
            {
                currentAge = 100;
                oldAge = 100;
                age.text = Mathf.Round(currentAge).ToString();
                playerManager.SetPlayerAge(oldAge);
            }
            else if (currentAge * 100 % 10 < 1)
            {
                oldAge = currentAge;
                bottomFill.fillAmount = currentAge / 100f;
                topFill.fillAmount = 1f - bottomFill.fillAmount;
                age.text = Mathf.Round(currentAge).ToString();
                playerManager.SetPlayerAge(oldAge);
            }
        }

    }

    public void addAge(float t, float a)
    {
        currentAge = a;
        tickTime = true;
    }

    public void Restart()
    {
        rotate = false;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        topFill.fillAmount = 1f;
        bottomFill.fillAmount = 0f;

        elapsedTime = 0f;
        elapsedTime2 = 0f;
        oldAge = 0;
        currentAge = 0;
        age.text = currentAge.ToString();

        if (isLoadingIcon)
        {
            tickTime = true;
        }
    }
}
