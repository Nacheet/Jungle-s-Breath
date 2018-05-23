using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratSpawn : MonoBehaviour {

    public GameObject rats;

    private void Start()
    {
        rats.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rats.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rats.SetActive(false);
        }
    }
}
