using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour {

    public GameObject music1;
    public GameObject music2;
    public float maxVolume= 1;


    private void Start()
    {
        music2.GetComponent<AudioSource>().volume = 0;
    }

    private void Update()
    {
        if (music2.GetComponent<AudioSource>().volume < maxVolume)
        {
            music2.GetComponent<AudioSource>().volume = music2.GetComponent<AudioSource>().volume+0.01f;
            music1.GetComponent<AudioSource>().volume = music1.GetComponent<AudioSource>().volume-0.01f;
        }
        if (music2.GetComponent<AudioSource>().volume == maxVolume)
        {
            this.enabled = false;

        }
    }

}
