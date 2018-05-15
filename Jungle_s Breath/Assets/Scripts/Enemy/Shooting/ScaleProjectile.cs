using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleProjectile : MonoBehaviour {

    private float scaleX, scaleY;

	// Use this for initialization
	void Start () {
        scaleX = 0.9f;
        scaleY = 0.9f;
    }
	
	// Update is called once per frame
	void Update () {

        if (this.GetComponent<Rigidbody2D>().velocity.x < 0)
            this.GetComponent<Transform>().localScale = new Vector2(-scaleX, scaleY);
        else
            this.GetComponent<Transform>().localScale = new Vector2(scaleX, scaleY);
    }
}
