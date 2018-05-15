using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowExplodeEnemy : MonoBehaviour {

    private GameObject player;
    public GameObject rangeDetector;
    public GameObject explosionDetector;
    public GameObject explosion;
    public GameObject partSys;

    //Enemy movement
    public Vector2 direction;
    public float maxSpeed;
    public bool isOnRange;

    //Explosion
    public bool isOnRangeExplode;
    public bool explode;
    public float time;
    public float time2;
    public float timeToDamage;
    public float timeToDestroy;
    public bool applyDamage;
    public bool destroyEnemy;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        isOnRange = rangeDetector.GetComponent<OnRangeDetector>().isOnRange;
        isOnRangeExplode = explosionDetector.GetComponent<OnRangeDetectorExplosion>().isOnRange;

        if(isOnRange && !explode)
        {
            direction = new Vector2(player.transform.position.x - this.transform.position.x, 0);
            direction.Normalize();

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * maxSpeed, 0);
        }
        else if(!isOnRange || explode)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        if (isOnRangeExplode && !explode)
        {
            explode = true;
            time = Time.time; 
        }

        if (explode && Time.time > time + timeToDamage)
        {
            explode = false;
            this.gameObject.tag = "Shot";
            this.gameObject.layer = 16;

            explosionDetector.SetActive(false);
            explosion.SetActive(true);

            explosionDetector.GetComponent<CircleCollider2D>().isTrigger = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.GetComponent<BoxCollider2D>().enabled = false;
            time2 = Time.time;
            destroyEnemy = true;
        }

        if(destroyEnemy && Time.time > time2 + timeToDestroy)
        {
            Destroy(this.gameObject);
        }

        if (explode)
            partSys.SetActive(true);
        else
            partSys.SetActive(false);
	}
}
