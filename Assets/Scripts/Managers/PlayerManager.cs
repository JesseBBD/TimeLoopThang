using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float playerHealth;
    [SerializeField] const float playerMaxHealth = 10;
    [SerializeField] Hourglass hourglass;

    void Awake()
    {
        playerHealth = playerMaxHealth;
    }

    public void TakeDamage(float damage){
        hourglass.addAge(damage);
        playerHealth -= damage;
        if(playerHealth <= 0){
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
