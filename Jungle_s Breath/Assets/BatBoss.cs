using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBoss : MonoBehaviour {

    public GameObject enemies;

    public GameObject stalactites;
    public Transform state1position;

    public Transform batSpawnLeft, batSpawnRight, batSpawn;
    public Transform newBat;

    GameObject enemyCopy;
    public Transform state0Position;

    public int state = 0;
    public int health = 100;
    public int maxHealth;

    float maxSpeed = 10f;
    int speedDir = -1;

    float time;
    public float timeToSpawn = 0.7f;

    float randomSpeedDirState0;
    int state0SpeedDir;

    //State 1
    int maxEnemies = 1;
    public int counter = 0;

    float distance;
    float position;

    bool spawned = false;
    bool collided = false;


    void Start()
    {
        maxHealth = health;
        distance = batSpawnRight.position.x - batSpawnLeft.position.x;
    }

	void Update ()
    {
        if ((health < maxHealth * 0.9 && state == 0))
            state = 1;

        if (state == 0)
        {
            float dist = this.GetComponent<Transform>().position.y - state0Position.position.y;
            Debug.Log(dist);
            Mathf.Abs(dist);
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            if (dist > 0.01)
            {
                if (randomSpeedDirState0 == 0)
                    randomSpeedDirState0 = Random.Range(50, 150);

                if (randomSpeedDirState0 > 100)
                    state0SpeedDir = 1;
                else
                    state0SpeedDir = -1;

                Vector2 moveToPos = new Vector2(1, state0Position.position.y - this.gameObject.transform.position.y);
                moveToPos.Normalize();

                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveToPos.x * state0SpeedDir * 5, moveToPos.y * 5);
            }
            else
            {
                stalactites.SetActive(true);
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeed * speedDir, 0);
            }

        }
        if(state == 1)
        {
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            stalactites.SetActive(false);

            float dist = this.GetComponent<Transform>().position.x - state1position.transform.position.x;
            Mathf.Abs(dist);

            if (dist > 0.1)
            {
               Vector2 moveToPos = new Vector2(state1position.position.x - this.gameObject.transform.position.x, state1position.position.y - this.gameObject.transform.position.y);
                moveToPos.Normalize();

                this.gameObject.GetComponent<Rigidbody2D>().velocity = moveToPos * 5;
            }
            else
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                this.gameObject.GetComponent<Transform>().position = state1position.position;
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

                //Spawn enemies
                
                if(counter < maxEnemies && Time.time > time + timeToSpawn)
                {
                    if (!spawned)
                    {
                        GameObject newEnemy;

                        position = Random.Range(0.0f, distance) + batSpawnLeft.position.x;
                        batSpawn.position = new Vector2(position, batSpawn.transform.position.y);

                        Transform newPosition;
                        newPosition = Instantiate<Transform>(newBat, batSpawn);

                        newEnemy = Instantiate<GameObject>(enemies, newPosition);
                        newEnemy.GetComponent<Transform>().localScale = new Vector3(0.4f, 0.4f, 1);
                        newEnemy.GetComponent<bat>().maxSpeed = Random.Range(1.3f, 2f);
                        enemyCopy = newEnemy;
                        spawned = true;
                    }
                    else
                    {
                        collided = enemyCopy.GetComponent<bat>().collided;
                        if(collided)
                        {
                            counter++;
                            time = Time.time;
                            spawned = false;
                        }
                        
                    }
                }
                else if(counter >= maxEnemies)
                {
                    counter = 0;
                    state = 0;
                }
                
            }

        }


	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Ground")
        {
            speedDir *= -1;
        }

        if(collision.collider.gameObject.tag == "Shot")
        {
            health--;
        }
    }
}
