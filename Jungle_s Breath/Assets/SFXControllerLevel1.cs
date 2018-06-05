using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControllerLevel1 : MonoBehaviour {

    public AudioSource footsteps;
    public AudioSource waterSplash;
    public AudioSource sliding;
    public AudioSource playerHit;
    public AudioSource playerDash;
    public AudioSource Boss1;
    public AudioSource outside;
    public AudioSource cave;

    void Start()
    {
        footsteps.Play();
        footsteps.mute = true;

        sliding.Play();
        sliding.mute = true;

        Boss1.Play();
        Boss1.mute = true;

        outside.Play();
        outside.mute = true;
    }

    public void playFootStep()
    {
        footsteps.mute = false;
    }

    public void stopFootStep()
    {

        footsteps.mute = true;
    }

    public void playWaterSplash()
    {
        waterSplash.Play();
    }

    public void playSliding()
    {
        sliding.mute = false;
    }

    public void stopSliding()
    {
        sliding.mute = true;
    }

    public void playPlayerHit()
    {
        playerHit.Play();
    }

    public void playPlayerDash()
    {
        playerDash.Play();
    }

    public void playBoss1()
    {
        Boss1.mute = false;
    }

    public void stopBoss1()
    {
        Boss1.mute = true;
    }

    public void playOutside()
    {
        outside.mute = false;
    }

    public void stopOutside()
    {
        outside.mute = true;
    }

    public void playCave()
    {
        cave.Play();
    }

    public void stopCave()
    {
        cave.Stop();
    }
}
