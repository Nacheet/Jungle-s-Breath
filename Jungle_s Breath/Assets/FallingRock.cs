using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour {

    public GameObject BatBoss;

    public Transform respawnPosition;
    public Transform nextRespawnPosition;

    public Vector2 startPosition;
    public bool hitPlayer = false;
    public float time;
    public float timeToMove = 0.8f;

    void Start()
    {
        startPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        this.GetComponent<Transform>().localScale = new Vector3(Random.Range(this.GetComponent<Transform>().localScale.x * 0.5f, this.GetComponent<Transform>().localScale.x * 1.5f),
            Random.Range(this.GetComponent<Transform>().localScale.y * 0.5f, this.GetComponent<Transform>().localScale.y * 1.5f), Random.Range(this.GetComponent<Transform>().localScale.z * 0.5f, this.GetComponent<Transform>().localScale.z * 1.5f));
    }

    void Update()
    {
        if(BatBoss.GetComponent<BatBoss>().activateCave)
        {
            respawnPosition = nextRespawnPosition;
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        }

        if(hitPlayer && Time.time > time + timeToMove)
        {
            this.transform.position = startPosition;
            hitPlayer = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Death>().dead = true;
            hitPlayer = true;
            time = Time.time;
        }

        if(collision.gameObject.tag == "Bat")
        {
            Destroy(BatBoss.gameObject, 0.5f);
        }

        if(collision.gameObject.tag == "Shot")
        {
            Destroy(collision.gameObject);
        }
    }
}
