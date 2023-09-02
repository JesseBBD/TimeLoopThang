using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float playerAge;
    [SerializeField] const float playerMaxAge = 100;
    [SerializeField] Hourglass hourglass;

    void Awake()
    {
        playerAge = 0;
    }

    public void TakeDamage(float damage){
        playerAge += damage;
        if(playerAge >= playerMaxAge){
            playerAge = 100;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        hourglass.addAge(damage, playerAge);
    }
}
