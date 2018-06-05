using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenu;
    public GameObject fades;
    public GameObject guide;

    private RestartButtonSizePause restart;

    void Start()
    {
        restart = this.gameObject.GetComponent<RestartButtonSizePause>();
    }

	void Update () {
        if(Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused && !settingsMenu.activeInHierarchy)
                Resume();
            else if(!GameIsPaused && !settingsMenu.activeInHierarchy)
                Pause();
        }
	}

    public void Resume()
    {
        guide.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        fades.SetActive(true);
    }

    void Pause()
    {
        guide.SetActive(true);
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
