using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingRotation : MonoBehaviour
{

  public float speed = 5f;
  public float maxAngle = 45f;  // Maximum swing angle

  private float currentAngle = 0f;  // The current angle of rotation
  private float direction = 1f;  // Direction of rotation


  // Update is called once per frame
  void Update()
  {
    // Calculate the amount to rotate for this frame
    float rotationAmount = Time.deltaTime * speed * direction;

    // Debug messages
    Debug.Log("Rotation Amount: " + rotationAmount);
    Debug.Log("Current Angle before update: " + currentAngle);

    // Update the current angle
    currentAngle += rotationAmount;

    // Debug message
    Debug.Log("Current Angle after update: " + currentAngle);

    // Check if the swing has reached the maximum angle
    if (Mathf.Abs(currentAngle) >= maxAngle)
    {
      // Debug message
      Debug.Log("Reached Max Angle, reversing direction");

      // Reverse direction
      direction *= -1;

      // Ensure the angle doesn't exceed maxAngle
      currentAngle = maxAngle * Mathf.Sign(currentAngle);
    }

    // Perform the rotation by directly setting the z-component of the rotation
    transform.rotation = Quaternion.Euler(0, 0, currentAngle);
  }
}
