using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControllerLevel1 : MonoBehaviour {

    public AudioSource footsteps;
    public AudioSource waterSplash;
    public AudioSource sliding;
    public AudioSource playerHit;
    public AudioSource playerDash;

    void Start()
    {
        footsteps.Play();
        footsteps.mute = true;

        sliding.Play();
        sliding.mute = true;
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
}
