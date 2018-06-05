using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControllerLevel2 : MonoBehaviour {

    public AudioSource rockHit;
    public AudioSource batScream;
    public AudioSource batFly;
    public AudioSource rats;
    public AudioSource rockBroken;
    public AudioSource fall;
    public AudioSource boss2;

    void Start()
    {
        batFly.Play();
        batFly.mute = true;

        batScream.Play();
        batScream.mute = true;

        rats.Play();
        rats.mute = true;

        boss2.Play();
        boss2.mute = true;
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

    public void playFall()
    {
        fall.Play();
    }

    public void playBoss2()
    {
        boss2.mute = false;
    }

    public void stopBoss2()
    {
        boss2.mute = true;
    }
}
