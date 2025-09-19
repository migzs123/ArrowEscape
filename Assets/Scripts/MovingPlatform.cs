using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint, endPoint;
    public float speed = 1.5f;

    private Vector2 targetPos;
    private bool canMove = false;  
    private bool goingToEnd = true;

    private void Start()
    {
        transform.position = startPoint.position;
        targetPos = endPoint.position;
    }

    private void Update()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPos) < 0.1f)
            {
                canMove = false; 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);

            if (!canMove)
            {
                canMove = true;

                if (goingToEnd)
                {
                    targetPos = endPoint.position;
                }
                else
                {
                    targetPos = startPoint.position;
                }

                goingToEnd = !goingToEnd; 
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        if (startPoint != null && endPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(startPoint.position, endPoint.position);
        }
    }
}
