using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstMenu : MonoBehaviour {

    private bool goToMainMenu;
    private bool firstTimePressed;
    private float time;

    public bool fadeIn = false;
    private float fadeTime;
    public float timeFade = 0.8f;
    public float lerpTime;

    public GameObject mainMenu;
    public GameObject menu;
    public GameObject firstMenuInside;
    public CanvasGroup uiElement;

    public void Start()
    {
        goToMainMenu = false;
        firstTimePressed = false;
        FadeOut();
    }


    void Update()
    {
        if(menu.activeSelf)
        {
            if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Pause") || Input.GetButtonDown("Fire1") || Input.anyKeyDown) && firstTimePressed)
            {
                goToMainMenu = true;
                if (fadeIn)
                    FadeIn();
                else
                    FadeOut();

                fadeTime = Time.time;
            }

            if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Pause") || Input.GetButtonDown("Fire1") || Input.anyKeyDown) && !firstTimePressed)
            {
                firstTimePressed = true;
            }

            if (goToMainMenu && Time.time > fadeTime + timeFade)
            {
                mainMenu.SetActive(true);
                firstMenuInside.SetActive(false);
                firstTimePressed = false;
                goToMainMenu = false;

                if (fadeIn)
                    FadeOut();
                else
                    FadeIn();


                menu.gameObject.SetActive(false);
                uiElement.gameObject.SetActive(false);
            }
        }
        
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, lerpTime));
    }


    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, lerpTime));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime)
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
}
