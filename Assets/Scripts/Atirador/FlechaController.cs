using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaController : MonoBehaviour
{
    [SerializeField] private float Velocidade;
   
    void Update()
    {
        transform.transform.position += transform.right * Velocidade * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Confiner")){
            Destroy(this.gameObject);
        }
    }

    ////Ao sair do campo de visão de todas as câmeras
    //private void OnBecameInvisible()
    //{
    //    //Destrói a flecha
    //    Destroy(this.gameObject);
    //}
}
