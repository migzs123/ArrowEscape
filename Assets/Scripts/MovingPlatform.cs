using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // A propriedade Rigidbody2D foi removida porque não é necessária para este tipo de movimento.
    public Transform startPoint, endPoint;
    public float speed = 1.5f;
    private Vector2 targetPos;

    private void Start()
    {
        // Define o ponto inicial como o primeiro alvo.
        targetPos = endPoint.position;
    }

    private void Update()
    {
        // Se a posição da plataforma estiver muito próxima do ponto de destino...
        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            // ... inverte o destino para o outro ponto.
            if (targetPos == (Vector2)endPoint.position)
            {
                targetPos = startPoint.position;
            }
            else
            {
                targetPos = endPoint.position;
            }
        }

        // Move a plataforma em direção ao destino.
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        // Desenha uma linha para visualizar o caminho da plataforma no editor.
        if (startPoint != null && endPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(startPoint.position, endPoint.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ao colidir, torna o jogador filho da plataforma para que ele se mova junto.
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Ao sair, remove o jogador do "parenting".
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}