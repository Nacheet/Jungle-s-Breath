using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowExplodeEnemy : MonoBehaviour {

    private GameObject player;
    public GameObject rangeDetector;
    public GameObject explosionDetector;
    public GameObject explosion;
    public GameObject partSys;
    public GameObject partSys1;

    //Enemy movement
    public Vector2 direction;
    public float maxSpeed;
    public bool isOnRange;

    //Explosion
    public bool isOnRangeExplode;

    public bool collect = false;
    public bool explode = false;
    public bool destroy = false;

    public float time;

    private float timeToCollect = 2.0f;
    private float timeToExplode = 1f;
    private float timeToSprite = 0.5f;
    private float timeToDestroy = 0.1f;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        isOnRange = rangeDetector.GetComponent<OnRangeDetector>().isOnRange;
        isOnRangeExplode = explosionDetector.GetComponent<OnRangeDetectorExplosion>().isOnRange;

        if(isOnRange && !collect)
        {
            direction = new Vector2(player.transform.position.x - this.transform.position.x, 0);
            direction.Normalize();

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * maxSpeed, 0);
        }
        else if(explode || destroy)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        if (isOnRangeExplode && !collect && !explode && !destroy)
        {
            collect = true;
            time = Time.time; 
        }

        if (collect && Time.time < time + timeToCollect)
        {
            partSys.SetActive(true);
        }
        else if(collect && Time.time > time + timeToCollect)
        {
            explode = true;
            partSys.SetActive(false);
            collect = false;
            time = Time.time;
        }

        if(explode && Time.time <= time +  timeToExplode + timeToDestroy && !destroy)
        {
            partSys1.SetActive(true);
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            rangeDetector.SetActive(false);
            explosionDetector.SetActive(false);
        }
        else if(explode && Time.time >= time + timeToExplode + timeToDestroy)
        {
            partSys1.SetActive(false);
            explode = false;
            destroy = true;
        }

        if (explode && Time.time > time + timeToSprite)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            explosion.SetActive(true);
        }


        if(destroy)
            Destroy(this.gameObject);
	}


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(explosion.activeSelf)
        {
            if (coll.gameObject.tag == "Player")
            {
                GameObject.Find("Player").GetComponent<Death>().dead = true;
                GameObject.Find("Player").GetComponent<Death>().deathPosition = new Vector2(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y);
                if (GameObject.Find("Player").GetComponent<Death>().fadeIn)
                    GameObject.Find("Player").GetComponent<Death>().FadeIn();
                else
                    GameObject.Find("Player").GetComponent<Death>().FadeOut();
                GameObject.Find("Player").GetComponent<Death>().teleport = true;
                GameObject.Find("Player").GetComponent<Death>().fadeTime = Time.time;
            }
        }
    }
  }
