using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FalseGround : MonoBehaviour
{
    public float destroyTime = 2f;
    private bool isBreaking = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBreaking) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitToBreak());
        }
    }

    private IEnumerator WaitToBreak()
    {
        isBreaking = true;
        // aqui voc� pode tocar anima��o/som antes de destruir
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject); // destr�i o objeto todo (sprite, collider, etc)
    }
}
