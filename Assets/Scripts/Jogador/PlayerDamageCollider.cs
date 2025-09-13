using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageCollider : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spikes"))
        {
            player.Die();
        }
        if (collision.CompareTag("Arrow"))
        {
            collision.enabled = false;

            player.Damage(1);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Potion"))
        {
            Debug.Log("Pegou");
            player.Cure(2);
            Destroy(collision.gameObject);
        }
    }
}
