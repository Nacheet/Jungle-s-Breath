using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRangeDetectorExplosion : MonoBehaviour {

    public GameObject enemy;


    public bool isOnRange;
    public bool applyDamage;

    void Update()
    {
        applyDamage = enemy.GetComponent<FollowExplodeEnemy>().applyDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isOnRange = true;

        if (applyDamage)
            enemy.gameObject.tag = "Shot";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isOnRange = false;
    }
}
