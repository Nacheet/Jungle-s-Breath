﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStalactite : MonoBehaviour {

    public GameObject detector;
    public GameObject partsys;
    public GameObject boss;

    public float maxGravity;
    public bool playerUnder;
    public bool hitPlayer;
    public bool hitShield;
    public float destroyTime;

    public float time = 0;
    private float timeToStopPart = 0.5f;

    public bool waterSplashed;
    public Vector2 splashPosition;


    void Start()
    {
        partsys.SetActive(true);
        partsys.SetActive(false);
        boss = GameObject.Find("Boss");
    }

    void Update()
    {
        playerUnder = detector.GetComponent<StalactiteDetector>().playerUnder;

        if (playerUnder && time == 0)
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
        else if(coll.collider.gameObject.tag == "Water")
        {
            waterSplashed = true;
            boss.GetComponent<SecondBoss>().waterSplashed = waterSplashed;

            splashPosition = this.gameObject.transform.position;
            boss.GetComponent<SecondBoss>().waterSplashSpawn.position = splashPosition;

            Destroy(gameObject, destroyTime);
        }
        else //if (coll.collider.gameObject.tag == "Ground")
        {
            Destroy(gameObject, destroyTime);
        }
    }

}
