using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public bool teleport;
    public bool fadeIn = false;
    public GameObject shield;

    public Transform teleportPos;
    private float fadeTime;
    private float timeTeleport = 0.8f;


    private bool playerCol;
    private bool shieldCol;
    private float time;
    public bool dead;
    public Vector2 deathPosition;

    public CanvasGroup uiElement;

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
    }


    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
    }



    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 0.4f)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1)
                break;

            yield return new WaitForEndOfFrame();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        shieldCol = shield.GetComponent<ShieldHitDetector>().shieldCol;

        if (collision.gameObject.tag == "Shot" || collision.gameObject.tag == "ShotBoss" || collision.gameObject.tag == "Water")
        {
            playerCol = true;
            time = Time.time;
        }
    }

    void Start()
    {
        FadeOut();
        dead = false;
    }


    void Update()
    {

        if (playerCol && Time.time > time + 0.1)
        {
            playerCol = false;
        }

        if (playerCol && !shieldCol)
        {
            dead = true;
            deathPosition = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            if (fadeIn)
                FadeIn();
            else
                FadeOut();

            teleport = true;
            fadeTime = Time.time;
        }

        if (teleport && Time.time > fadeTime + timeTeleport)
        {
            transform.position = teleportPos.position;
            teleport = false;

            if (fadeIn)
                FadeOut();
            else
                FadeIn();

            dead = false;
        }
    }


}

