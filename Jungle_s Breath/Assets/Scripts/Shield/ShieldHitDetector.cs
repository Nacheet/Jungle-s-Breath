using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHitDetector : MonoBehaviour {


    //public GameObject player;
    //public bool shieldHit = true;
    public float time;

    public bool shieldCol;

    void Update()
    {
    if (shieldCol && Time.time > time + 0.1)
        {
            shieldCol = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Shot" || collision.gameObject.tag == "ShotBoss" || collision.gameObject.tag == "Water")
        {
            shieldCol = true;
            time = Time.time;
        }
    }
}
