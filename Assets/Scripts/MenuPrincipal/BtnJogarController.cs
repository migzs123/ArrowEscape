using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnJogarController : MonoBehaviour
{
    public void Jogar()
    {
        //Avan�a pra pr�xima cena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
