using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalText : MonoBehaviour {


    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public Color colorWhiteFaded;
    public float fadein1;
    public float staytime1;
    public float fadeout1;
    public float intermediateTime;
    public float fadein2;
    public float staytime2;
    public float fadeout2;
    public float intermediateTime2;
    public float fadein3;
    public float staytime3;
    public float fadeout3;
    public float endTime;

    private float initTime;
    public int state;
    public Color auxColor;

    // Use this for initialization
    void Start () {
        state = 0;
        initTime = Time.time;
        text1.SetActive(true);
        text2.SetActive(false);
        text3.SetActive(false);
        text1.GetComponent<Text>().color = colorWhiteFaded;
        auxColor = colorWhiteFaded;

    }
	
	// Update is called once per frame
	void Update () {

        if (state == 0)
        {
            if (text1.GetComponent<Text>().color.a < 1)
            {
                auxColor.a += fadein1;
                text1.GetComponent<Text>().color = auxColor;
            }
            else {
                initTime = Time.time;
                state++;
            }
        }
        else if (state == 1) {
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
            if (Time.time - initTime >= intermediateTime)
            {
                text1.SetActive(false);
                text2.SetActive(true);
                text3.SetActive(false);
                text2.GetComponent<Text>().color = colorWhiteFaded;
                state++;
            }
        }

        else if (state == 4)
        {

            if (text2.GetComponent<Text>().color.a < 1)
            {
                auxColor.a += fadein2;
                text2.GetComponent<Text>().color = auxColor;
            }
            else
            {
                initTime = Time.time;
                state++;
            }
        }
        else if (state == 5)
        {
            if (Time.time - initTime >= staytime2)
            {
                state++;
            }
        }
        else if (state == 6)
        {
            if (text2.GetComponent<Text>().color.a > 0)
            {
                auxColor.a -= fadeout2;
                text2.GetComponent<Text>().color = auxColor;
            }
            else
            {
                initTime = Time.time;
                state++;
            }
        }
        else if (state == 7)
        {
            if (Time.time - initTime >= intermediateTime2)
            {
                text1.SetActive(false);
                text2.SetActive(false);
                text3.SetActive(true);
                text3.GetComponent<Text>().color = colorWhiteFaded;
                state++;
            }
        }

        else if (state == 8)
        {

            if (text3.GetComponent<Text>().color.a < 1)
            {
                auxColor.a += fadein3;
                text3.GetComponent<Text>().color = auxColor;
            }
            else
            {
                initTime = Time.time;
                state++;
            }
        }
        else if (state == 9)
        {
            if (Time.time - initTime >= staytime3)
            {
                state++;
            }
        }
        else if (state == 10)
        {
            if (text3.GetComponent<Text>().color.a > 0)
            {
                auxColor.a -= fadeout3;
                text3.GetComponent<Text>().color = auxColor;
            }
            else
            {
                initTime = Time.time;
                state++;
            }
        }
        else if (state == 11)
        {
            if (Time.time - initTime >= endTime)
            {
                text1.SetActive(false);
                text2.SetActive(false);
                text3.SetActive(false);
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
