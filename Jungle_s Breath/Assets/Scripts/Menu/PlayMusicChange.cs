using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicChange : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip horrorEffect;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void ChangeAudioClip()
    {
        audioSource.clip = horrorEffect;
        audioSource.Play();
    }
}
