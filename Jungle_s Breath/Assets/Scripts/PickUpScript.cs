using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {

    public float blinkRate=0.5f;
    public float nextBlink;
    private GameObject player;
    private int counter = 0;
    private float nextDieTime = 0.0f, dieTimeRate = 0.02f;
    private bool touched = false;

	// Use this for initialization
	void Start () {
        nextBlink = Time.time + blinkRate;
        player = GameObject.Find("Player");
        nextDieTime = Time.time;

    }
	
	// Update is called once per frame
	void Update () {

        if (touched)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            if (Time.time > nextDieTime && counter < 50)
            {
                gameObject.GetComponent<Transform>().localScale = gameObject.GetComponent<Transform>().localScale - new Vector3(0.1f, 0.1f, 0);
                counter++;
                nextDieTime = Time.time + dieTimeRate;
            }
            if (counter == 50)
            {
                Destroy(gameObject);
            }
        }
        else if (Time.time > nextBlink)
        {
            nextBlink = Time.time + blinkRate;
            this.GetComponent<SpriteRenderer>().enabled = !this.GetComponent<SpriteRenderer>().enabled;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>() == player.GetComponent<Collider2D>())
        {
            touched = true;

        }
    }
}
