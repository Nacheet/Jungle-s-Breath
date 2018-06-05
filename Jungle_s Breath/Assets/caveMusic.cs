using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caveMusic : MonoBehaviour {

    public GameObject SFXManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            SFXManager.GetComponent<SFXControllerLevel1>().playCave();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            SFXManager.GetComponent<SFXControllerLevel1>().stopCave();
    }
}
