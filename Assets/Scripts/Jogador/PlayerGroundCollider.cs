using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    private Player player;

    [Header("Configuração do Ground Check")]
    [SerializeField] private Vector2 boxSize = new Vector2(0.5f, 0.1f);
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkOffsetY = -0.1f; // Offset para ajustar a posição do box

    private Vector2 boxCenter;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        if (player == null)
        {
            Debug.LogError("Player não encontrado no parent!", this);
        }
    }

    private void Update()
    {
        PerformGroundCheck();
    }

    private void PerformGroundCheck()
    {
        // Calcula o centro do box com offset
        boxCenter = new Vector2(transform.position.x, transform.position.y + checkOffsetY);

        // Checa se há algum colisor na área do box
        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer);

        // Atualiza o estado do player
        if (player != null)
        {
            player.isGrounded = hit != null;
        }
    }

    private void OnDrawGizmos()
    {
        // Calcula o centro para o gizmo também
        Vector2 gizmoCenter = new Vector2(transform.position.x, transform.position.y + checkOffsetY);

        Gizmos.color = Color.red;

#if UNITY_EDITOR
        // Verifica se está no editor e se há um player com grounded
        if (Application.isPlaying)
        {
            Player parentPlayer = GetComponentInParent<Player>();
            if (parentPlayer != null && parentPlayer.isGrounded)
                Gizmos.color = Color.green;
        }
        else
        {
            // Visualização no modo edição
            Player parentPlayer = GetComponentInParent<Player>();
            if (parentPlayer != null && parentPlayer.isGrounded)
                Gizmos.color = Color.green;
        }
#endif

        Gizmos.DrawWireCube(gizmoCenter, boxSize);
    }
}