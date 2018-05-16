using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour {

    private GameObject player;
    public GameObject rangeDetector;
    public Animator animator;
    private SpriteRenderer rend;

    //Enemy movement
    public Vector2 direction;
    public float maxSpeed;
    public bool isOnRange;

    private bool collided;
    private float time;
    private float timeToMove = 2;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        rend = GetComponent<SpriteRenderer>();
        this.GetComponent<Rigidbody2D>().gravityScale = GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale;
    }
	
	// Update is called once per frame
	void Update ()
    {
        isOnRange = rangeDetector.GetComponent<OnRangeDetector>().isOnRange;
        if(isOnRange && !collided)
        {
            direction = new Vector2(player.transform.position.x - this.transform.position.x, 0);
            direction.Normalize();

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * maxSpeed, 0);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        if (this.GetComponent<Rigidbody2D>().velocity.x != 0)
            animator.SetBool("moving", true);
        else
            animator.SetBool("moving", false);

        if (this.GetComponent<Rigidbody2D>().velocity.x < 0)
            rend.flipX = true;
        else if (this.GetComponent<Rigidbody2D>().velocity.x > 0)
            rend.flipX = false;

        if(collided && Time.time < time + timeToMove)
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (collided && Time.time > time + timeToMove)
        {
            collided = false;
            this.GetComponent<BoxCollider2D>().enabled = true;
            this.GetComponent<Rigidbody2D>().gravityScale = GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale;
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.gameObject.tag == "Player")
        {
            collided = true;
            time = Time.time;
        }
    }
}
