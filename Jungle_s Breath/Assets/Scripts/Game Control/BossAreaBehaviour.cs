using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaBehaviour : MonoBehaviour {

    public GameObject actualCamera;
    public GameObject mainCamera;
    public GameObject bossCamera;
    public GameObject boss1;
    public bool bossActivated= false;

    public GameObject SFXManager;
    GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<MusicChanger>().music1 = GameObject.Find("MainMusic");
            GameObject.Find("Player").GetComponent<MusicChanger>().music2 = GameObject.Find("Boss1Music");

            GameObject.Find("Player").GetComponent<MusicChanger>().enabled = true;

            GameObject.Find("Player").GetComponent<CameraAproach>().camera1 = actualCamera;
            GameObject.Find("Player").GetComponent<CameraAproach>().camera2 = bossCamera;

            if (!bossActivated)
            {
                boss = Instantiate<GameObject>(boss1);
                bossActivated = true;
            }

            if (boss.GetComponent<Boss1Behaviour>().bossHP <= 0)
                SFXManager.GetComponent<SFXControllerLevel1>().stopBoss1();
            else
                SFXManager.GetComponent<SFXControllerLevel1>().playBoss1();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {

            SFXManager.GetComponent<SFXControllerLevel1>().stopBoss1();

            GameObject.Find("Player").GetComponent<MusicChanger>().music1 = GameObject.Find("Boss1Music");
            GameObject.Find("Player").GetComponent<MusicChanger>().music2 = GameObject.Find("MainMusic");

            GameObject.Find("Player").GetComponent<MusicChanger>().enabled = true;


            GameObject.Find("Player").GetComponent<CameraAproach>().camera1 = actualCamera;
            GameObject.Find("Player").GetComponent<CameraAproach>().camera2 = mainCamera;
        }
    }

}
