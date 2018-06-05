using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButtonSizePause : MonoBehaviour {

    public GameObject butt1, butt2, butt3, butt4, butt5;

    public void RestartSize()
    {
        butt1.GetComponent<Transform>().localScale = new Vector2(1, 1);
        butt2.GetComponent<Transform>().localScale = new Vector2(1, 1);
        butt3.GetComponent<Transform>().localScale = new Vector2(1, 1);
        butt4.GetComponent<Transform>().localScale = new Vector2(1, 1);
        butt5.GetComponent<Transform>().localScale = new Vector2(1, 1);
    }
}
