using UnityEngine;

public class SwingRotation : MonoBehaviour
{

  [SerializeField] float speed = 5f;
  [SerializeField] float maxAngle = 45f;  // Maximum swing angle
  private float currentAngle = 0f;  // The current angle of rotation
  private float direction = 1f;  // Direction of rotation


  void Update()
  {
    float rotationAmount = Time.deltaTime * speed * direction;
    currentAngle += rotationAmount;

    if (Mathf.Abs(currentAngle) >= maxAngle)
    {
      direction *= -1;
      currentAngle = maxAngle * Mathf.Sign(currentAngle);
    }

    transform.rotation = Quaternion.Euler(0, 0, currentAngle);
  }
}
