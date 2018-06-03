using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBoss : MonoBehaviour {

    public GameObject enemies;

    public GameObject stalactites1, stalactites2, stalactites3;
    public Transform state1position;

    public Transform batSpawnLeft, batSpawnRight, batSpawn;
    public Transform newBat;

    GameObject enemyCopy;
    public Transform state0Position;

    public Animator animator;

    public int state = 0;
    public int health;
    public int maxHealth;

    float maxSpeed = 10f;
    int speedDir = -1;

    float time;
    public float timeToSpawn = 0.7f;

    float randomSpeedDirState0;
    int state0SpeedDir;

    //State 0
    public bool state0Phase1 = false;
    public bool state0Phase2 = false;
    public bool state0Phase3 = false;

    //State 1
    float distanceToSpawn = 1;
    float maxEnemies = 1;
    float maxEnemiesCopy;
    public int counter = 0;

    float distance;
    float position;

    bool spawned = false;
    bool collided = false;

    public bool lastState1 = false;

    //State 2
    float timeForShake = 0;
    float timeShaking = 0.8f;
    float shakeMag = 0.5f;

    //State 3
    float nextFall;
    float fallTime = 0;
    public bool falling;
    public bool startFall;
    float distanceToPlayerX;
    float fallSpeed = 20f;
    Vector2 moveToPlayer;
    bool groundCol;
    float initFallPos;
    public float dist;
    public int falls = 0;
    public bool stayOnGround;
    public bool batHit;
    public bool attackOnceAfterState2 = true;
    public float timeOnGround = 3f;
    public float timeGround;

    int resistance;

    void Start()
    {
        maxHealth = health;
        maxEnemiesCopy = maxEnemies;
        distance = batSpawnRight.position.x - batSpawnLeft.position.x;

        stalactites2.SetActive(false);
        stalactites3.SetActive(false);

        nextFall = Random.Range(2f, 4f);
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        resistance = 3;
    }

	void Update ()
    {
        if (this.gameObject.GetComponent<Rigidbody2D>().velocity.x == 0)
        {
            animator.SetBool("FrontFly", true);
            animator.SetBool("LeftFly", false);
            animator.SetBool("RightFly", false);
        }
        else if (this.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            animator.SetBool("FrontFly", false);
            animator.SetBool("LeftFly", false);
            animator.SetBool("RightFly", true);
        }
        else if (this.gameObject.GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            animator.SetBool("FrontFly", false);
            animator.SetBool("LeftFly", true);
            animator.SetBool("RightFly", false);
        }

        if (state == 0)
        {
            float dist = this.GetComponent<Transform>().position.y - state0Position.transform.position.y;
            dist = Mathf.Abs(dist);

            if (dist > 0.01)
            {
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

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
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeed * speedDir, 0);

                if (health <= 0 && !state0Phase3)
                {
                    state0Phase3 = true;
                    Destroy(stalactites3.gameObject);
                    state = 1;
                }

                if( health <= maxHealth * 1/3 && !state0Phase2)
                {
                    state0Phase2 = true;
                    Destroy(stalactites2.gameObject);
                    state = 1;
                }

                if(health <= maxHealth * 2/3 && !state0Phase1)
                {
                    state0Phase1 = true;
                    Destroy(stalactites1.gameObject);
                    state = 1;
                }
            }

        }
        if(state == 1)
        {
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            distanceToSpawn = this.GetComponent<Transform>().position.x - state1position.transform.position.x;
            distanceToSpawn = Mathf.Abs(distanceToSpawn);
            //Debug.Log(distanceToSpawn);
            if (distanceToSpawn >= 0.1)
            {
                //Debug.Log("MOVING TO POSITION");
               Vector2 moveToPos = new Vector2(state1position.position.x - this.gameObject.transform.position.x, state1position.position.y - this.gameObject.transform.position.y);
                moveToPos.Normalize();

                this.gameObject.GetComponent<Rigidbody2D>().velocity = moveToPos * 5;
            }
            else
            {
                Debug.Log("ON POSITION");
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

                //Spawn enemies
                if (state0Phase1 && !state0Phase2 && !state0Phase3)
                    maxEnemies = maxEnemiesCopy;
                else if (state0Phase1 && state0Phase2 && !state0Phase3)
                    maxEnemies = maxEnemiesCopy * 1.5f;
                else if(state0Phase1 && state0Phase2 && state0Phase3)
                {
                    maxEnemies = maxEnemiesCopy * 2f;
                    lastState1 = true;
                }


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
                    if (!lastState1)
                    {
                        state = 0;
                        randomSpeedDirState0 = 0;

                        if(state0Phase2)
                            stalactites3.SetActive(true);
                        else if (state0Phase1)
                            stalactites2.SetActive(true);
                    }
                    else
                        state = 2;
                }  
            }

        }
        else if(state == 2)
        {
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            animator.SetBool("Knocked", false);
            float distThis = this.GetComponent<Transform>().position.x - state1position.transform.position.x;
            distThis = Mathf.Abs(distThis);

            if (distThis > 0.1)
            {
                Debug.Log("MOVING TO SHAKE POS");
                Vector2 moveToPos = new Vector2(state1position.position.x - this.gameObject.transform.position.x, state1position.position.y - this.gameObject.transform.position.y);
                moveToPos.Normalize();

                this.gameObject.GetComponent<Rigidbody2D>().velocity = moveToPos * 5;
            }
            else
            {
                Debug.Log("SHAKING");
                if (timeForShake == 0)
                    timeForShake = Time.time;

                else if (timeForShake + timeShaking > Time.time)
                {
                    this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    CameraShake.Shake(timeShaking, shakeMag);
                    GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
                    GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
                else
                {
                    state = 3;
                    timeForShake = 0;
                    fallTime = Time.time;
                    attackOnceAfterState2 = true;
                    GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
                }

            }
        }
        else if(state == 3)
        {
            if (fallTime == 0)
                fallTime = Time.time;

            dist = this.GetComponent<Transform>().position.y - state1position.transform.position.y;
            dist = Mathf.Abs(dist);

            if(Time.time > fallTime + nextFall)
                startFall = true;

            if (startFall)
            {
                moveToPlayer = new Vector2(GameObject.Find("Player").GetComponent<Transform>().position.x - this.GetComponent<Transform>().position.x, GameObject.Find("Player").GetComponent<Transform>().position.y - this.GetComponent<Transform>().position.y);
                moveToPlayer.Normalize();

                this.gameObject.GetComponent<Rigidbody2D>().velocity = moveToPlayer * fallSpeed;

                falling = true;

                initFallPos = this.GetComponent<Transform>().position.x;

                distanceToPlayerX = GameObject.Find("Player").GetComponent<Transform>().position.x - this.GetComponent<Transform>().position.x;
                distanceToPlayerX = Mathf.Abs(distanceToPlayerX);
                startFall = false;

                fallTime = 50 * Time.time;
                falls++;
                Debug.Log("FALL START");
            }


            if (falling)
            {
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                this.gameObject.GetComponent<Rigidbody2D>().velocity = moveToPlayer * fallSpeed;

                if (groundCol)
                {
                    if (falls == 4)
                    {
                        if (Time.time < timeOnGround + timeGround)
                        {
                            animator.SetBool("Knocked", true);
                            Debug.Log("STAY ON GROUND");
                            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                        }
                        else
                        {
                            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                            falls = 0;
                            falling = false;
                            Debug.Log("FLY");
                            fallTime = Time.time;
                            nextFall = Random.Range(2f, 3f);
                        }
                    }
                    else
                    {
                        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        falling = false;
                        Debug.Log("GROUND COL");
                        groundCol = false;
                        fallTime = Time.time;
                        nextFall = Random.Range(2f, 3f);
                    }
                }
            }
            else if(dist > 0.01f && !falling)
            {
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                animator.SetBool("Knocked", false);
                Debug.Log("MOVING");
                if(initFallPos == 0)
                { 
                    Vector2 moveToPos = new Vector2(state1position.position.x - this.gameObject.transform.position.x, state1position.position.y - this.gameObject.transform.position.y);
                    moveToPos.Normalize();

                    this.gameObject.GetComponent<Rigidbody2D>().velocity = moveToPos * 10;
                }
                else
                {
                    Vector2 moveToPos = new Vector2(initFallPos + 2 * speedDir * distanceToPlayerX - this.gameObject.transform.position.x, state1position.position.y - this.gameObject.transform.position.y);
                    moveToPos.Normalize();

                    this.gameObject.GetComponent<Rigidbody2D>().velocity = moveToPos * 10;
                }
            }
            else if(dist <= 0.01f && !falling)
            {
                Debug.Log("MOVING AROUND");
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeed * speedDir, 0); 
            }

            if (batHit)
            {
                state = 2;
                batHit = false;
                resistance--;
                timeForShake = 0;
            }
        }

        if (resistance == 2)
            Debug.Log("PEDRA POC TRENCADA");
        else if (resistance == 1)
            Debug.Log("PEDRA SEMITRENCADA");
        else if (resistance == 0)
            Debug.Log("PEDRA TRENCADA");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Ground")
        {
            speedDir *= -1;
            groundCol = true;
            timeGround = Time.time;
        }

        if(collision.collider.gameObject.tag == "Shot")
        {
            health--;
            animator.SetTrigger("Hitted");
        }

        if(collision.collider.gameObject.tag == "Player" && GameObject.Find("Player").GetComponent<PlayerController>().shieldAtt && state == 3)
        {
            batHit = true;
        }
    }
}
