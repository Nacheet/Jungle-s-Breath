using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControllerLevel2 : MonoBehaviour {

    public AudioSource rockHit;
    public AudioSource batScream;
    public AudioSource batFly;
    public AudioSource rats;
    public AudioSource rockBroken;

    void Start()
    {
        batFly.Play();
        batFly.mute = true;

        batScream.Play();
        batScream.mute = true;

        rats.Play();
        rats.mute = true;
    }

    public void playRockHit()
    {
        rockHit.Play();
    }

    public void playBatScream()
    {
        batScream.mute = false;
    }

    public void stopBatScream()
    {
        batScream.mute = true;
    }

    public void playBatFly()
    {
        batFly.mute = false;
    }

    public void stopBatFly()
    {
        batFly.mute = true;
    }

    public void playRats()
    {
        rats.mute = false;
    }

    public void stopRats()
    {
        rats.mute = true;
    }

    public void playRockBroken()
    {
        rockBroken.Play();
    }
}
