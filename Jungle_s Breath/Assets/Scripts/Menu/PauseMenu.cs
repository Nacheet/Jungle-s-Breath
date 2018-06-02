using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject fades;

    private RestartButtonSizePause restart;

    void Start()
    {
        restart = this.gameObject.GetComponent<RestartButtonSizePause>();
    }

	void Update () {
		
        if(Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
	}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        fades.SetActive(true);
    }

    void Pause()
    {
        fades.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        restart.RestartSize();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
