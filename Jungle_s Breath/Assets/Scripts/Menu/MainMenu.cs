using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    private PlayMusicChange musicChanger;
    private bool start;
    private float time;
    public float timeToStart;

    void Start()
    {
        musicChanger = GameObject.Find("Canvas").GetComponent<PlayMusicChange>();
    }

    void Update()
    {
        if(start && Time.time > time + timeToStart)
        {
            musicChanger.ChangeAudioClip();
            playGame();
        }
    }


    public void startCounter()
    {
        start = true;
        time = Time.time;
    }

    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
