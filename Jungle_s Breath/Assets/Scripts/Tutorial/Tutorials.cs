using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour {

    public GameObject player;
    public GameObject message;
   
	// Use this for initialization
	void Start () {

   
        message.SetActive(false);
     
    }

   


    void printTutorial1()
    {
        message.SetActive(true);
    }

    void deleteTutorial()
    {
        message.SetActive(false);
    }
     




    private void OnTriggerEnter2D(Collider2D collision)
     {        

        printTutorial1();
              
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        deleteTutorial();
    }


}
