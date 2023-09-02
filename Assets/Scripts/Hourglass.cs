using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hourglass : MonoBehaviour
{
    public TMP_Text age;
    public Image topFill;
    public Image bottomFill;

    public float roundDuration = 10f;
    public int totalRounds = 3;
    int currentRound = 0;

    void Awake()
    {
        SetRoundText(totalRounds);
    }

    public void Begin()
    {
        ++currentRound;

    }

    void SetRoundText(int value)
    {
        age.text = value.ToString();
    }

}
