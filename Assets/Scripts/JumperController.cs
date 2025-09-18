using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour
{
    private Animator animator;
    public Player player;

    public float jumpForce = 40f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            animator.SetTrigger("Jump");
            
        }
    }

    public void Jump()
    {
        if (player == null) return;

        player.rb.AddForce(new Vector2(0f , jumpForce) , ForceMode2D.Impulse);
        player.stateMachine.ChangeState(player.fallState);
    }

}
