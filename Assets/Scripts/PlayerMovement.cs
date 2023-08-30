using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float speed = 10.0f;
  public float jumpForce = 5.0f;
  public float groundRayDistance = 1.0f;
  public LayerMask groundLayer;

  private Rigidbody2D rb;
  private bool isJumping = false;
  private bool isGrounded = false;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    float moveX = Input.GetAxisRaw("Horizontal");

    // Perform raycasting to check for ground beneath player's feet
    RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, groundRayDistance, groundLayer);
    isGrounded = groundHit.collider != null;

    Vector2 moveDirection = new Vector2(moveX, 0);

    // If on a slope, modify the moveDirection
    if (isGrounded && groundHit.normal != Vector2.up)
    {
      moveDirection = RotateByNormal(moveDirection, groundHit.normal);
    }

    moveDirection.Normalize();

    // Only allow jumping if we're on the ground
    if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
    {
      rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
      isJumping = true;
    }

    // Horizontal movement
    rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
    if(moveDirection.x >= 0){
      transform.localScale = new Vector3(1, 1, 1);
    }else{
      transform.localScale = new Vector3(-1, 1, 1);
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (((1 << collision.gameObject.layer) & groundLayer) != 0)
    {
      isJumping = false;
    }
  }

  // Rotate the vector by the normal of the slope
  private Vector2 RotateByNormal(Vector2 moveDirection, Vector2 normal)
  {
    float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg - 90;
    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    return rotation * moveDirection;
  }
}
