﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour {

    public GameObject detector;
    public GameObject partsys;

    //int minGravity = 0;
    public float maxGravity;
    public bool playerUnder;
    public bool hitPlayer;
    public bool hitShield;
    public float destroyTime;

    public float time = 0;
    private float timeToStopPart = 0.5f;


    void Start()
    {
        partsys.SetActive(true);
        partsys.SetActive(false);
    }

	void Update ()
    {
        playerUnder = detector.GetComponent<StalactiteDetector>().playerUnder;

		if(playerUnder && time == 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = maxGravity;
            time = Time.time;
            partsys.SetActive(true);
        }

        if (Time.time > time + timeToStopPart)
            partsys.SetActive(false);
	}


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.gameObject.tag == "Player")
        {
            hitPlayer = true;
            Destroy(this.gameObject, destroyTime);
        }
        else if (coll.collider.gameObject.tag == "Shield")
        {
            hitShield = true;
            Destroy(gameObject, destroyTime);
        }
        else //if (coll.collider.gameObject.tag == "Ground")
        {
            Destroy(gameObject, destroyTime);
        }
    }

}
