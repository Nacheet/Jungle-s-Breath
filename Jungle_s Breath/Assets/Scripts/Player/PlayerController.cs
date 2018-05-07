using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    public BoxCollider2D shield;
    private SpriteRenderer rend;
    public LayerMask floor;
    public Animator animator;

    //LateralMovement
    public bool onTheGround = false;
    private float maxSpeed = 14;
    private float jumpSpeed;
    private float normalJumpSpeed = 35;
    private float doubleJumpSpeed;
    public bool falling;
    public bool movingRight;
    private float normalGravity = 10, fallingGravity = 11.5f;
    public float slidingVelocity;

    //Jump
    public bool enableJump;
    public bool enableDoubleJump = true;

    //WallJump
    public bool leftWallHit = false, rightWallHit = false;
    public bool wallHit = false;
    public int wallHitDirection = 0;
    public bool wallJumped = false;
    public float wallJumpTime;
    public float timeToMoveAfterJumping;

    //Shield
    public bool usingShield;
    public bool shieldDefault;
    //ShieldMovement
    public float newSpeed;
    private float maxSpeedCopy;
    //ShieldAttack
    public bool shieldAtt, canAtt;
    public float attSpeed;
    public float attCoolDown = 4.0f;
    public float nextAtt = 0.0f;
    public float attDuration = 1.5f;
    public float lastAttDone;

    //Dash
    public bool canDash = true;
    public bool dash = false;
    public float lastDashDone;
    public float nextDash;
    public float dashCoolDown = 1.0f;
    public float dashDuration = 0.2f;
    public float dashSpeed = 3.0f;

    //GameControl
    public bool dead;


    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        jumpSpeed = normalJumpSpeed;
        doubleJumpSpeed = normalJumpSpeed * 3 / 4;
        maxSpeedCopy = maxSpeed;
    }

    void Update()
    {
        dead = player.GetComponent<Death>().dead;

        if (!dead)
        {
            if (movingRight)
                rend.flipX = false;
            else
                rend.flipX = true;


            float horizontal;
            onTheGround = isOnGround();


            if ((leftWallHit || rightWallHit) && !onTheGround)
                horizontal = 0;
            else
                horizontal = Input.GetAxis("Horizontal");

            if (player.GetComponent<Rigidbody2D>().velocity.x == 0)
            {
                animator.SetBool("moving", false);
            }
            else if (player.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                movingRight = true;
                animator.SetBool("moving", true);
            }
            else if (player.GetComponent<Rigidbody2D>().velocity.x < -0.04)
            {
                movingRight = false;
                animator.SetBool("moving", true);
            }

            if (onTheGround)
            {
                enableJump = true;
                enableDoubleJump = true;
                jumpSpeed = normalJumpSpeed;
            }
            else if (!onTheGround && enableDoubleJump)
            {
                jumpSpeed = doubleJumpSpeed;
            }

            leftWallHit = isOnWallLeft();
            rightWallHit = isOnWallRight();
            if (leftWallHit)
            {
                wallHit = true;
                wallHitDirection = 1;
            }
            else if (rightWallHit)
            {
                wallHit = true;
                wallHitDirection = -1;
            }
            else
            {
                wallHit = false;
                wallHitDirection = 0;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (onTheGround)
                {
                    jump();
                }
                else if (wallHit)
                {
                    wallJumped = true;
                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeed * wallHitDirection, normalJumpSpeed);
                    wallJumpTime = Time.time;
                }
                else if (enableDoubleJump)
                {
                    jump();
                    enableDoubleJump = false;
                }
            }

            if (Time.time > wallJumpTime + timeToMoveAfterJumping && wallJumped)
                wallJumped = false;

            if (!wallJumped)
                move(horizontal);

            if (player.GetComponent<Rigidbody2D>().velocity.y < 0)
                falling = true;
            else
                falling = false;

            if ((leftWallHit || rightWallHit) && falling)
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, slidingVelocity);
            else if (falling)
                player.GetComponent<Rigidbody2D>().gravityScale = fallingGravity;
            else
                player.GetComponent<Rigidbody2D>().gravityScale = normalGravity;



            //Shield
            if (Input.GetButton("Fire1"))
            {
                if (onTheGround)
                {
                    maxSpeed = newSpeed;
                    usingShield = true;
                    animator.SetBool("usingShield", true);
                }
                else
                {
                    usingShield = true;
                    animator.SetBool("usingShield", true);
                }

            }
            else
            {
                usingShield = false;
                maxSpeed = maxSpeedCopy;
                animator.SetBool("usingShield", false);
            }

            //ShieldAttack
            if (Time.time > nextAtt && onTheGround && usingShield)
                canAtt = true;
            else
                canAtt = false;

            if (Input.GetButtonDown("Fire2") && canAtt)
            {
                shieldAtt = true;
                lastAttDone = Time.time;
                nextAtt = Time.time + attCoolDown + attDuration;
            }
            if (lastAttDone + attDuration >= Time.time && shieldAtt)
                GetComponent<Rigidbody2D>().velocity = new Vector2(attSpeed * GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
            else
                shieldAtt = false;

            if (onTheGround)
                animator.SetBool("onTheGround", true);
            else
                animator.SetBool("onTheGround", false);

            if (leftWallHit || rightWallHit)
                animator.SetBool("wallCollision", true);
            else
                animator.SetBool("wallCollision", false);


            //Player dash / slide

            if (Time.time > nextDash && onTheGround)
                canDash = true;
            else
                canDash = false;

            if (Input.GetButtonDown("Fire2") && !usingShield && canDash)
            {
                dash = true;
                lastDashDone = Time.time;
                nextDash = Time.time + dashCoolDown + dashDuration;
            }

            if (dash && Time.time <= lastDashDone + dashDuration)
            {
                if (!onTheGround)
                {
                    dash = false;
                }

                else if (onTheGround)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(dashSpeed * GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
                }

            }
            else
            {
                dash = false;
            }

            if (dash)
            {
                shield.GetComponent<BoxCollider2D>().enabled = false;
                //animator.SetBool("dash", true);
                rend.flipY = true;
            }
            else
            {
                shield.GetComponent<BoxCollider2D>().enabled = true;
                //animator.SetBool("dash", false);
                rend.flipY = false;
            }
        }
        else
        {
            player.transform.position = GetComponent<Death>().deathPosition;
            animator.SetBool("moving", false);
        }


        //Colliding with wall with animation bugs

        if ((leftWallHit && onTheGround && player.GetComponent<Rigidbody2D>().velocity.x < 0) || (rightWallHit && onTheGround && player.GetComponent<Rigidbody2D>().velocity.x > 0))
            animator.SetBool("moving", false);

    }



    private void move(float input)
    {
        if (dead)
            return;
        GetComponent<Rigidbody2D>().velocity = new Vector2(input * maxSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
    }

    private bool isOnGround()
    {
        float lenghtToSearch = 0.1f;
        float colliderLimit = 0.001f;

        Vector2 lineStart = new Vector2(this.transform.position.x, this.transform.position.y - rend.bounds.extents.y - colliderLimit);

        Vector2 vectorToSearch = new Vector2(this.transform.position.x, lineStart.y - lenghtToSearch);
        RaycastHit2D hit = Physics2D.Linecast(lineStart, vectorToSearch, floor);

        return hit;
    }


    private bool isOnWallLeft()
    {
        bool retVal = false;

        float lenghtToSearch = 0.1f;
        float colliderLimit = 0.01f;

        Vector2 lineStart = new Vector2(this.transform.position.x - rend.bounds.extents.x - colliderLimit, this.transform.position.y);

        Vector2 vectorToSearch = new Vector2(lineStart.x - lenghtToSearch, this.transform.position.y);

        RaycastHit2D hitLeft = Physics2D.Linecast(lineStart, vectorToSearch, floor);

        retVal = hitLeft;


        if (retVal)
            if (hitLeft.collider.gameObject.tag == "No Slide Wall")
                return false;
        return retVal;
    }


    private bool isOnWallRight()
    {
        bool retVal = false;

        float lenghtToSearch = 0.1f;
        float colliderLimit = 0.01f;

        Vector2 lineStart = new Vector2(this.transform.position.x + rend.bounds.extents.x + colliderLimit, this.transform.position.y);

        Vector2 vectorToSearch = new Vector2(lineStart.x + lenghtToSearch, this.transform.position.y);

        RaycastHit2D hitRight = Physics2D.Linecast(lineStart, vectorToSearch, floor);

        retVal = hitRight;

        if (retVal)
            if (hitRight.collider.gameObject.tag == "No Slide Wall")
                return false;
        return retVal;
    }


    private void jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, this.jumpSpeed);
        animator.SetTrigger("jump");
    }

}

