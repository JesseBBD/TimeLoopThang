using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector2 boxSize;
    [SerializeField] float cameraVerticalOffset;
    BoxArea boxArea;
    struct BoxArea
    {
        public Vector2 center;
        Vector2 velocity;
        float left, right, top, bottom;

        public BoxArea(Bounds playerBounds, Vector2 size)
        {
            left = playerBounds.center.x - size.x / 2;
            right = playerBounds.center.x + size.x / 2;
            bottom = playerBounds.min.y;
            top = playerBounds.min.y + size.y;

            velocity = Vector2.zero;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds playerBounds)
        {
            float shiftX = 0;
            if (playerBounds.min.x < left)
            {
                shiftX = playerBounds.min.x - left;
            }
            else if (playerBounds.max.x > right)
            {
                shiftX = playerBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (playerBounds.min.y < bottom)
            {
                shiftY = playerBounds.min.y - bottom;
            }
            else if (playerBounds.max.y > top)
            {
                shiftY = playerBounds.max.y - top;
            }
            bottom += shiftY;
            top += shiftY;

            center = new Vector2((left+right)/2, (top+bottom)/2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }

    void Start()
    {
        boxArea = new BoxArea(player.GetComponent<Collider2D>().bounds, boxSize);
    }

    void LateUpdate()
    {
        boxArea.Update(player.GetComponent<Collider2D>().bounds);
        Vector2 boxPosition = boxArea.center + Vector2.up*cameraVerticalOffset;
        transform.position = new Vector3(Mathf.Max(boxPosition.x+5f, -1f), boxPosition.y, -10);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color (1, 0, 1, 0.5f);
        Gizmos.DrawCube(boxArea.center, boxSize);
    }
}
