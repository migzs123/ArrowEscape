using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public float playerHealth = -1;
    [HideInInspector] public float playerHealthAtLevelStart = -1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // registrar callback para troca de cena
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica se a cena atual é o menu
        if (scene.buildIndex == 0) // coloque exatamente o nome da cena
        {
            playerHealth = -1;
            playerHealthAtLevelStart = -1;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
