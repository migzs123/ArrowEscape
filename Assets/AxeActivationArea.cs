using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeActivationArea : MonoBehaviour
{
    private AxeController controller;
    private void Start()
    {
        controller = GetComponentInParent<AxeController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            controller.StartAttack();
        }
    }
}
