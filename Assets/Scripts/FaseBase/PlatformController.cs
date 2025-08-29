using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    public Transform startPoint, endPoint;
    public float speed = 1.5f;
    Vector2 targetPos;

    private bool colisao = false;

    private void Start()
    {
       targetPos = endPoint.position;
    }

    private void Update()
    {
        if (colisao) {
            if (Vector2.Distance(transform.position, startPoint.position) < .1f)
                targetPos = endPoint.position;
            if (Vector2.Distance(transform.position, endPoint.position) < .1f)
                targetPos = startPoint.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startPoint.position, endPoint.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Jogador")
        {
            colisao = true;
            collision.transform.SetParent(rb.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Jogador")
        {
            collision.transform.SetParent(this.transform);
        }
    }
}
