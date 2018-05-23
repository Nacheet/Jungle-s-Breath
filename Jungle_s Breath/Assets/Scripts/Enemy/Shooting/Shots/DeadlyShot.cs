using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyShot : MonoBehaviour {

    public Vector2 direction;
    public float speed;
    public float initTime;
    public float deathTime = 10;
    public float destroyTime = 0.1f;


    public bool explode;
    public Vector2 explosionPosition;
    private GameObject boss;

	
    void Start()
    {
        boss = GameObject.Find("Boss");
    }

	void Update ()
    {
		if(direction.x != 0)
        {
            this.GetComponent<Rigidbody2D>().velocity = direction;
            this.GetComponent<Rigidbody2D>().velocity *= speed;
        }

        if(Time.time > initTime + deathTime)
        {
            explode = true;
            boss.GetComponent<SecondBoss>().exploded = explode;
            explosionPosition = this.gameObject.transform.position;
            boss.GetComponent<SecondBoss>().explosionSpawn.transform.position = explosionPosition;

            Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground")
        {
            explode = true;
            boss.GetComponent<SecondBoss>().exploded = explode;
            explosionPosition = this.gameObject.transform.position;
            boss.GetComponent<SecondBoss>().explosionSpawn.transform.position = explosionPosition;

            Destroy(this.gameObject, destroyTime);
        }
    }
}
