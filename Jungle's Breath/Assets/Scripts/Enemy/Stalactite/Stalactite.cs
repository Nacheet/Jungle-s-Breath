using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour {

    public GameObject detector;

    //int minGravity = 0;
    public float maxGravity;
    public bool playerUnder;
    public bool hitPlayer;
    public bool hitShield;
    public float destroyTime;


	void Update ()
    {
        playerUnder = detector.GetComponent<StalactiteDetector>().playerUnder;

		if(playerUnder)
        {
            GetComponent<Rigidbody2D>().gravityScale = maxGravity;
        }
	}


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.gameObject.tag == "Player")
        {
            hitPlayer = true;
        }
        else if (coll.collider.gameObject.tag == "Shield")
        {
            hitShield = true;
            Destroy(gameObject, destroyTime);
        }
        else if (coll.collider.gameObject.tag == "Ground")
        {
            Destroy(gameObject, destroyTime);
        }
    }

}
