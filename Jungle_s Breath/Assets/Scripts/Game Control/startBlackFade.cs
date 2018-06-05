using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startBlackFade : MonoBehaviour {

    public GameObject Image;
    public float time = 0;
    private float timeToDestroy = 1.5f;
    public bool enableDeathSound = false;

    void Start()
    {
        time = Time.time;
        Image.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
    }

    void Update()
    {
        if (Time.time > time + timeToDestroy)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
            Destroy(Image.gameObject);
            enableDeathSound = true;
        }
    }
}
