using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatArea : MonoBehaviour {

    public bool playerInside = false;
    public GameObject SFXController;
    public GameObject ratCamera;
    public GameObject actualCamera;
    public GameObject mainCamera;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInside = true;
            GameObject.Find("Player").GetComponent<CameraAproach>().camera1 = actualCamera;
            GameObject.Find("Player").GetComponent<CameraAproach>().camera2 = ratCamera;
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInside = false;
            GameObject.Find("Player").GetComponent<CameraAproach>().camera1 = actualCamera;
            GameObject.Find("Player").GetComponent<CameraAproach>().camera2 = mainCamera;
        }

    }


    void Update()
    {
        if (playerInside)
            SFXController.GetComponent<SFXControllerLevel2>().playRats();
        else
            SFXController.GetComponent<SFXControllerLevel2>().stopRats();
    }
}
