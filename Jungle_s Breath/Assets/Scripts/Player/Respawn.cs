using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    public Transform respawnPlayer;
    public Transform newRespawn;


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            respawnPlayer.position = newRespawn.position;
        }
    }
}
