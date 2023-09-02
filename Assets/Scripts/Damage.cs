using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] float damageAmount;

    void OnCollisionEnter2D(Collision2D collidingObject)
    {
        if(collidingObject.gameObject.tag == "Player"){
            collidingObject.gameObject.GetComponent<PlayerManager>().TakeDamage(damageAmount);
        }else{
            Destroy(gameObject);
        }
    }
}
