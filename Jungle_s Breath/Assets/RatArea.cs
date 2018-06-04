using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatArea : MonoBehaviour {

    public bool playerInside = false;
    public GameObject SFXController;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            playerInside = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            playerInside = false;
    }


    void Update()
    {
        if (playerInside)
            SFXController.GetComponent<SFXControllerLevel2>().playRats();
        else
            SFXController.GetComponent<SFXControllerLevel2>().stopRats();
    }
}
