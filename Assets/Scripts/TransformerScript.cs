using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformerScript : MonoBehaviour
{
  public Vector2 targetPosition; // Set this to where you want to move the player

  private void OnTriggerEnter2D(Collider2D other)
  {
    // Check if the collider is the player
    if (other.tag == "Player")
    {
      // Get a reference to the PlayerMovement script on the player
      PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

      // Call the StartLerpToTarget method
      playerMovement.StartLerpToTarget(new Vector3(targetPosition.x, targetPosition.y ,0));
    }
  }
}
