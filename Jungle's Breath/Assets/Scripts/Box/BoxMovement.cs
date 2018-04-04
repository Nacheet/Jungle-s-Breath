using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour {

    GameObject shield;
    public float minMass;
    private float maxMass = 1000000.0f;
	// Use this for initialization
	void Start () {
        shield = GameObject.Find("Shield");
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == shield.GetComponent<Collider2D>())
        {
            GetComponent<Rigidbody2D>().mass = minMass;
        }
        else
            GetComponent<Rigidbody2D>().mass = maxMass;
    }
}
