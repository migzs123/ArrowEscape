using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject heartUI;
    private bool isPaused = false;

    private void Start()
    {
        Resume();
    }

    void Update()
    {
        // tecla Esc para pausar/despausar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        heartUI.SetActive(false);
        Time.timeScale = 0f; // pausa o jogo
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        heartUI.SetActive(true);
        Time.timeScale = 1f; // retoma o jogo
        isPaused = false;
    }

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
