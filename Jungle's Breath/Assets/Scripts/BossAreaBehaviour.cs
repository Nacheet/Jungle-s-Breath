using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaBehaviour : MonoBehaviour {

    public GameObject actualCamera;
    public GameObject mainCamera;
    public GameObject bossCamera;
//    public GameObject mainCamera1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<MusicChanger>().music1 = GameObject.Find("MainMusic");
            GameObject.Find("Player").GetComponent<MusicChanger>().music2 = GameObject.Find("Boss1Music");

            GameObject.Find("Player").GetComponent<MusicChanger>().enabled = true;


            GameObject.Find("Player").GetComponent<CameraAproach>().camera1 = actualCamera;
            GameObject.Find("Player").GetComponent<CameraAproach>().camera2 = bossCamera;


            //bossCamera.SetActive(true);
            //mainCamera.SetActive(false);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<MusicChanger>().music1 = GameObject.Find("Boss1Music");
            GameObject.Find("Player").GetComponent<MusicChanger>().music2 = GameObject.Find("MainMusic");

            GameObject.Find("Player").GetComponent<MusicChanger>().enabled = true;


            GameObject.Find("Player").GetComponent<CameraAproach>().camera1 = actualCamera;
            GameObject.Find("Player").GetComponent<CameraAproach>().camera2 = mainCamera;


            //mainCamera.SetActive(true);
            //bossCamera.SetActive(false);
        }
    }

}
