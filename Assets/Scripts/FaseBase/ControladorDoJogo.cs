using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDoJogo : MonoBehaviour
{
    //Pega o Canvas da UI de morte
    [SerializeField] GameObject Canvas;

    public void Passou(int scene)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(scene);
    }

    public void Morreu()
    {
        //Para o tempo e ativa o Canvas
        Time.timeScale = 0.0f;
        Canvas.gameObject.SetActive(true);
    }
}
