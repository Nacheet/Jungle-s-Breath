using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Area : MonoBehaviour {

    public bool playerIsInside = false;

    public GameObject actualCamera;
    public GameObject mainCamera;
    public GameObject bossCamera;

    public GameObject SFXManager;
    public GameObject bat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerIsInside = true;
            GameObject.Find("Player").GetComponent<CameraAproach>().camera1 = actualCamera;
            GameObject.Find("Player").GetComponent<CameraAproach>().camera2 = bossCamera;

            if (!bat.GetComponent<BatBoss>().activateCave)
            {
                SFXManager.GetComponent<SFXControllerLevel2>().playBoss2();
                SFXManager.GetComponent<SFXControllerLevel1>().stopCave();
            }
            else
            {
                SFXManager.GetComponent<SFXControllerLevel2>().stopBoss2();
                SFXManager.GetComponent<SFXControllerLevel1>().playCave();
            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SFXManager.GetComponent<SFXControllerLevel2>().stopBoss2();
            SFXManager.GetComponent<SFXControllerLevel1>().playCave();

            playerIsInside = false;
            GameObject.Find("Player").GetComponent<CameraAproach>().camera1 = actualCamera;
            GameObject.Find("Player").GetComponent<CameraAproach>().camera2 = mainCamera;
        }
    }
}
