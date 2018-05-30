using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : MonoBehaviour {

    GameObject player;
    public GameObject batBoss;

    public float maxSpeed = 0;
    Vector2 direction;

    public bool collided = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        direction = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
    }

    void Update ()
    {
        this.GetComponent<Rigidbody2D>().velocity = direction * maxSpeed;
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag != "Bat")
        {
            collided = true;
            direction = new Vector2(0, 0);
            Destroy(this.gameObject, 0.2f);
        }
    }
}
