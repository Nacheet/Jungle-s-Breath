using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToLevel3 : MonoBehaviour {

    private PlayerController playerController;
    private float gravity;

    public bool teleport;
    public bool fadeIn = false;
    private GameObject player;
    public float fadeTime;
    private float timeTeleport = 0.8f;


    public CanvasGroup uiElement;

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
    }


    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
    }



    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 0.5f)
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


    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player" && fadeIn)
        {
            FadeIn();
            teleport = true;
            fadeTime = Time.time;

            playerController.enabled = false;
        }
        else if(coll.gameObject.tag == "Player" && !fadeIn)
        {
            FadeOut();
            teleport = true;
            fadeTime = Time.time;

            playerController.enabled = false;
        }
    }

    void Start()
    {
        FadeOut();
        player = GameObject.Find("Player");

        playerController = player.GetComponent<PlayerController>();

    }

    void Update()
    {
        if (teleport && Time.time > fadeTime + timeTeleport)
        {
            teleport = false;

            playerController.enabled = true;

            SceneManager.LoadScene("Final");

        }

    }
}
