using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleSoul1 : MonoBehaviour {

    bool pickedUp = false;

    void Update()
    {
        if(pickedUp)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().dashDuration = 0.4f;
            Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            pickedUp = true;
        }
    }
}
