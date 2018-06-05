using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outsideMusic : MonoBehaviour {

    public GameObject SFXManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            SFXManager.GetComponent<SFXControllerLevel1>().playOutside();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            SFXManager.GetComponent<SFXControllerLevel1>().stopOutside();
    }
}
