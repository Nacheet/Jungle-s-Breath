using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButtonSize : MonoBehaviour {

    public GameObject butt1, butt2, butt3, butt4, butt5, butt6, butt7, butt8, butt9;

    public void RestartSize()
    {
        butt1.GetComponent<Transform>().localScale = new Vector2(1, 1);
        butt2.GetComponent<Transform>().localScale = new Vector2(1, 1);
        butt3.GetComponent<Transform>().localScale = new Vector2(1, 1);
        butt4.GetComponent<Transform>().localScale = new Vector2(1, 1);
        butt5.GetComponent<Transform>().localScale = new Vector2(1, 1);
        butt6.GetComponent<Transform>().localScale = new Vector2(1, 1);
        butt7.GetComponent<Transform>().localScale = new Vector2(1, 1);
        butt8.GetComponent<Transform>().localScale = new Vector2(1, 1);
        butt9.GetComponent<Transform>().localScale = new Vector2(1, 1);
    }
}
