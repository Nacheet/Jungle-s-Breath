using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBoss : MonoBehaviour {

    //GameObjects and Transforms
    public GameObject detector;
    public GameObject stalactite;
    public GameObject normalProjectile;
    public GameObject deadlyProjectile;
    public GameObject explosion;
    public GameObject waterSplash;


    public Transform stalactiteLeft, stalactiteRight;
    public Transform stalactiteSpawn;

    public Transform lowerShot, upperShot;

    public Transform explosionSpawn;
    public Transform waterSplashSpawn;

    //Player detection
    public bool playerIsInside;

    //Stalactite generation
    private float distance;
    public float position;
    public float timeSinceLastStalactite;
    public float nextStalactite;

    //Projectile generation
    public int side;
    public int type;
    public float timeSinceLastProjectile;
    public float nextProjectile;

    //Boss health
    public int health;

    //Water Splash
    public bool waterSplashed;

    //Explosion
    public bool exploded;

	void Start ()
    {
        distance = stalactiteRight.position.x - stalactiteLeft.position.x;

        nextStalactite = Random.Range(1.0f, 3.0f);
        position = Random.Range(0.0f ,distance) + stalactiteLeft.position.x;

        nextProjectile = Random.Range(0.0f, 3.0f);
        side = Random.Range(0, 2);
        type = Random.Range(0, 2);
    }

    void Update ()
    {
        playerIsInside = detector.GetComponent<OnRangeDetector>().isOnRange;
        stalactiteSpawn.position = new Vector2(position, stalactiteSpawn.position.y);

        if (health >= 0)
        {
            if (playerIsInside)
            {
                if (Time.time > timeSinceLastStalactite + nextStalactite)
                {
                    GameObject newStalactite;
                    newStalactite = Instantiate<GameObject>(stalactite, stalactiteSpawn);
                    timeSinceLastStalactite = Time.time;
                    nextStalactite = Random.Range(2.5f, 3.5f);
                    position = Random.Range(0, distance) + stalactiteLeft.position.x;
                }

                if(Time.time > timeSinceLastProjectile + nextProjectile)
                {
                    GameObject newShot;
                    if (side == 0)
                        if (type == 0)
                            newShot = Instantiate<GameObject>(normalProjectile, lowerShot);
                        else
                            newShot = Instantiate<GameObject>(deadlyProjectile, lowerShot);
                    else
                        if(type == 0)
                            newShot = Instantiate<GameObject>(normalProjectile, upperShot);
                        else
                            newShot = Instantiate<GameObject>(deadlyProjectile, upperShot);

                    if (type == 1)
                    {
                        DeadlyShot shotControl = newShot.GetComponent<DeadlyShot>();
                        shotControl.direction = new Vector2(-1, 0);
                        shotControl.initTime = Time.time;
                    }
                    else
                    {
                        ShotBehaviour shotControl = newShot.GetComponent<ShotBehaviour>();
                        shotControl.vector = new Vector2(-1, 0);
                    }

                    timeSinceLastProjectile = Time.time;
                    nextProjectile = Random.Range(1.0f, 4.0f);
                    side = Random.Range(0, 2);
                    type = Random.Range(0, 2);
                }

                if(waterSplashed)
                {
                    GameObject newSplash;
                    newSplash = Instantiate<GameObject>(waterSplash, waterSplashSpawn);
                    waterSplashed = false;
                    Destroy(newSplash, 0.4f);
                }

                if(exploded)
                {
                    GameObject newExplosion;
                    newExplosion = Instantiate<GameObject>(explosion, explosionSpawn);
                    exploded = false;
                    Destroy(newExplosion, 0.3f);
                }
            }
        }
        else
            Debug.Log("Dead");
	}


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Shot")
        {
            health--;
            Destroy(collision.gameObject);
        }
    }
}
