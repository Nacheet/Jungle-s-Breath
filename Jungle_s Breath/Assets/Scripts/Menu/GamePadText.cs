using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePadText : MonoBehaviour
{

    public GameObject text1;
    public Color colorWhiteFaded;
    public float fadein1;
    public float staytime1;
    public float fadeout1;
    public float endTime;

    private float initTime;
    public int state;
    public Color auxColor;

    // Use this for initialization
    void Start()
    {
        state = 0;
        initTime = Time.time;
        text1.SetActive(true);
        text1.GetComponent<Text>().color = colorWhiteFaded;
        auxColor = colorWhiteFaded;

    }

    // Update is called once per frame
    void Update()
    {

        if (state == 0)
        {
            if (text1.GetComponent<Text>().color.a < 1)
            {
                auxColor.a += fadein1;
                text1.GetComponent<Text>().color = auxColor;
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
            if (text1.GetComponent<Text>().color.a > 0)
            {
                auxColor.a -= fadeout1;
                text1.GetComponent<Text>().color = auxColor;
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
                text1.SetActive(false);
                SceneManager.LoadScene("Nivell 1");
            }
        }
    }

}