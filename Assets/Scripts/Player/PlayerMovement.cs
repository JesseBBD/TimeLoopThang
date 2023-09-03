using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float speed = 10.0f;
  public float jumpForce = 5.0f;
  public float groundRayDistance = 1.0f;
  public LayerMask groundLayer;

  private Rigidbody2D rb;
  private Animator animator;
  private bool isJumping = false;
  private bool isGrounded = false;

  // Lerp related
  private bool isLerping = false;
  private Vector2 startPosition;
  private Vector2 targetPosition;
  private float lerpStartTime;
  private float lerpTime = 2.0f;
  bool isDead = false;
  public void setIsDead(bool d) => isDead = d;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    if (isDead)
    {
        rb.velocity = new Vector2(0, 0);
    }
    else
    {

      if (isLerping)
      {
        float lerpValue = (Time.time - lerpStartTime) / lerpTime;
        transform.position = Vector2.Lerp(startPosition, targetPosition, lerpValue);

        if (lerpValue >= 1.0f)
        {
          isLerping = false;
          rb.isKinematic = false;
        }
        return;
      }

      float moveX = Input.GetAxisRaw("Horizontal");
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
        rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
        animator.SetTrigger("jump");
      }


      if (moveDirection.x != 0)
      {
        rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
        animator.SetBool("walking", true);
        transform.localScale = new Vector3(Mathf.Sign(moveDirection.x), 1, 1); // Flip character if needed
      }
      else
      {
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetBool("walking", false);
      }
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

  public void StartLerpToTarget(Vector2 target)
  {
    isLerping = true;
    lerpStartTime = Time.time;
    startPosition = transform.position;
    targetPosition = target;
    rb.isKinematic = true;
  }
}
