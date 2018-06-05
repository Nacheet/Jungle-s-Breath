using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour {

    public GameObject tutorials;
    public GameObject message;


	void Start () {
        tutorials.SetActive(false);
        message.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
     {        
        if(collision.gameObject.tag == "Player")
        {
            tutorials.SetActive(true);
            message.SetActive(true);
        }
              
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tutorials.SetActive(false);
            message.SetActive(false);
        }
    }


}
