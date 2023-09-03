using UnityEngine;

public class GravestoneController : MonoBehaviour
{
    [SerializeField] GameObject fullGravestone, halfGravestone, fallingHalfGravestone;
    [SerializeField] Sprite cracked;
    [SerializeField] int crackCounter = 0;
    [SerializeField] bool lastGravestone = false;

    void Awake()
    {
        fullGravestone.SetActive(true);
        halfGravestone.SetActive(false);
        fallingHalfGravestone.SetActive(false);
    }

    public void PlayerCollision()
    {
        crackCounter++;
        if (lastGravestone)
        {
            halfGravestone.SetActive(true);
            fallingHalfGravestone.SetActive(true);
            fullGravestone.SetActive(false);
        }
        else
        {
            switch (crackCounter)
            {
                case 1:
                    fullGravestone.GetComponent<SpriteRenderer>().sprite = cracked;
                    break;
                case 2:
                    halfGravestone.SetActive(true);
                    fallingHalfGravestone.SetActive(true);
                    fullGravestone.SetActive(false);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
}
