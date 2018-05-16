using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRangeDetectorExplosion : MonoBehaviour {

    public GameObject enemy;
    public bool isOnRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isOnRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isOnRange = false;
    }
}
