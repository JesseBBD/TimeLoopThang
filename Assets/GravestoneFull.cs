using UnityEngine;

public class GravestoneFull : MonoBehaviour
{
    [SerializeField] GravestoneController parentGravestone;

    void OnCollisionEnter2D(Collision2D collidingObject)
    {
        if (collidingObject.gameObject.tag == "Player")
        {
            parentGravestone.PlayerCollision();
        }
    }
}
