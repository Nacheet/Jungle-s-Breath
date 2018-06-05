using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePadMenu : MonoBehaviour {

    public GameObject gamePad;

    public float fadein1;
    public float staytime1;
    public float fadeout1;
    public float endTime;

    private float initTime;
    public int state;

    public Color auxColor;

    void Start()
    {
        state = 0;
        initTime = Time.time;
        gamePad.SetActive(true);
        gamePad.GetComponent<SpriteRenderer>().color = auxColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            if (gamePad.GetComponent<SpriteRenderer>().color.a < 1)
            {
                auxColor.a += fadein1;
                gamePad.GetComponent<SpriteRenderer>().color = auxColor;
            }
            else
            {
                initTime = Time.time;
                state++;
            }
        }
        else if (state == 1)
        {
            if (Time.time - initTime >= staytime1)
            {
                state++;
            }
        }
        else if (state == 2)
        {
            if (gamePad.GetComponent<SpriteRenderer>().color.a > 0)
            {
                auxColor.a -= fadeout1;
                gamePad.GetComponent<SpriteRenderer>().color = auxColor;
            }
            else
            {
                initTime = Time.time;
                state++;
            }
        }
        else if (state == 3)
        {
            if (Time.time - initTime >= endTime)
            {
                gamePad.SetActive(false);
                SceneManager.LoadScene("Nivell 1");
            }
        }
    }
}
