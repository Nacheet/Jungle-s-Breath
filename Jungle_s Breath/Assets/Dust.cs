using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour {


    public GameObject dust, player;
    public Transform rightHand, leftHand;

    //Player variables
    public bool wallCollLeft, wallCollRight;
    public bool falling;

    void Update()
    {
        wallCollLeft = player.GetComponent<PlayerController>().leftWallHit;
        wallCollRight = player.GetComponent<PlayerController>().rightWallHit;
        falling = player.GetComponent<PlayerController>().falling;

        if(falling)
        {
            Debug.Log("falling");
            if (wallCollRight)
            {
                dust.gameObject.SetActive(true);
                dust.transform.position = rightHand.position;
                Debug.Log("active");
            }
            else if (wallCollLeft)
            {
                dust.gameObject.SetActive(true);
                dust.transform.position = leftHand.position;
                Debug.Log("active");
            }
            else
                gameObject.SetActive(false);
        }
        else
        {
            dust.gameObject.SetActive(false);
            Debug.Log("notactive");
        }
    }
}
