using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public Transform attackPoint;
    public float radius;
    public LayerMask playerLayer;

    public float waitTime = 2f;
    public int damage = 2;

    private Animator animator;
    private bool isWaiting = false;
    private bool canAttack = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool("isWaiting", isWaiting);
    }

    public void StartAttack()
    {
        if (!canAttack || isWaiting) return;

        canAttack = false;
        animator.SetTrigger("Attack");
    }


    public void DealDamage()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, radius, playerLayer);

        foreach (Collider2D player in players)
        {
            Vector2 knockDir = (player.transform.position - transform.position).normalized;
            player.GetComponent<Player>().TakeDamageKnockback(damage, knockDir);
        }

        StartCoroutine(WaitTime());
    }

    public IEnumerator WaitTime()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        canAttack = true;
        animator.SetTrigger("Return");
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
