using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour {

    private PlayMusicChange musicChanger;
    public AudioMixer main;
    private bool start;
    private float time;
    public float timeToStart;

    void Start()
    {
        musicChanger = GameObject.Find("Canvas").GetComponent<PlayMusicChange>();
    }

    void Update()
    {
        if(start && Time.time < time + timeToStart)
        {
            main.SetFloat("volume", -20);
        }
        else if(start && Time.time > time + timeToStart)
        {
            main.SetFloat("volume", 0);
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
