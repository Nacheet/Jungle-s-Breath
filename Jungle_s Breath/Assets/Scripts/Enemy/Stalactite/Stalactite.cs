using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour {

    public GameObject detector;
    public GameObject partsys;

    public float maxGravity;
    public bool playerUnder;
    public bool hitPlayer;
    public bool hitShield;
    public float destroyTime;

    public float time = 0;
    private float timeToStopPart = 0.5f;

    public bool destroy = false;
    public bool falling = false;


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
            falling = true;
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
            destroy = true;
            Destroy(this.gameObject, destroyTime);
        }
        else if (coll.collider.gameObject.tag == "Shield")
        {
            hitShield = true;
            destroy = true;
            Destroy(gameObject, destroyTime);
        }
        else if(coll.collider.gameObject.tag == "Bat")
        {
            destroy = true;
            Destroy(gameObject);
        }
        else if (coll.collider.gameObject.tag == "Ground")
        {
            destroy = true;
            Destroy(gameObject, destroyTime);
        }
    }

}
