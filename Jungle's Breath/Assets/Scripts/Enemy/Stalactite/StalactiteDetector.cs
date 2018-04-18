using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteDetector : MonoBehaviour {

    public bool playerUnder;



    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            playerUnder = true;
        }
    }
}
