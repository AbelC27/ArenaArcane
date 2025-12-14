using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI; 
    public static bool isPaused = false; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f; 
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame()
    {
        UnityEngine.Debug.Log("Ieșire din joc...");
        Application.Quit(); 

    }
}