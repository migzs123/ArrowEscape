using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretPassage : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField] private float opacidadeNormal = 1f;
    [SerializeField] private float opacidadeTransparente = 0.3f;
    [SerializeField] private float velocidadeTransicao = 2f;

    private bool playerDentro = false;
    private Color corAtual;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        corAtual = tilemap.color;
    }

    private void Update()
    {
        float opacidadeAlvo = playerDentro ? opacidadeTransparente : opacidadeNormal;
        corAtual.a = Mathf.Lerp(corAtual.a, opacidadeAlvo, velocidadeTransicao * Time.deltaTime);
        tilemap.color = corAtual;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = false;
        }
    }
}
