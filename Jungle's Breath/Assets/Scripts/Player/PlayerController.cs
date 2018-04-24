using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private GameObject player;
    public Animator animator;

    //Variables moviment
    public bool grounded;
    public LayerMask whatIsFloor, whatIsGround;
    public Transform groundCheckL, groundCheckR;
    public float maxSpeed = 17;
    public bool groundedL = false, groundedR = false;
    public bool movingRight, movingLeft;
    public bool notMoving;

    //Variables salt
    public float jumpVelocity = 19.0f;
    public float maxGravity = 7.0f, minGravity = 3.5f;
    private bool falling;
    //Variables salt paret
    public Transform playerRight, playerLeft;
    public bool slidingL, slidingR;
    private bool wallJumping, jumpR, jumpL;
    private const float jumpXCap = 0.8f;
    public float slidingGravity = 2.0f;

    ////Moviment del escut
    public bool usingShield;
    public bool shieldDefault;
    //Variables del moviment del escut
    public float newSpeed;
    private float maxSpeedCopy;

    //Variables del atac
    public bool shieldAtt, canAtt;
    public float attSpeed;
    public float attCoolDown = 4.0f;
    public float nextAtt = 0.0f;
    public float attDuration = 1.5f;
    public float lastAttDone;

    void Start()
    {
        player = GameObject.Find("Player");
        GetComponent<Rigidbody2D>().gravityScale = minGravity;
        maxSpeedCopy = maxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(grounded)
        {
            if(notMoving)
            {
                animator.SetBool("notMov", true);
            }
            else
            {
                animator.SetBool("notMov", false);
            }
        }


        //Comprova si esta al terra
        groundedL = Physics2D.Linecast(player.transform.position, groundCheckL.position, whatIsGround);
        groundedR = Physics2D.Linecast(player.transform.position, groundCheckR.position, whatIsGround);
        //Comprova si esta tocant la paret dreta o esquerra
        slidingR = Physics2D.Linecast(player.transform.position, playerRight.position, whatIsFloor);
        slidingL = Physics2D.Linecast(player.transform.position, playerLeft.position, whatIsFloor);

        //Grounded
        if (groundedL || groundedR)
        {
            grounded = true;
            animator.SetBool("grounded", true);
        }
        else
        {
            animator.SetBool("grounded", false);
            grounded = false;
        }


        //Moviment lateral
        move(Input.GetAxis("Horizontal"));

        //Per saber si el jugador es mou cap a la dreta o cap a la esquerra
        if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            movingLeft = true;
            movingRight = false;
            notMoving = false;
        }
        else if (GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            movingRight = true;
            movingLeft = false;
            notMoving = false;
        }
        else
            notMoving = true;

        //Salt
        if (Input.GetButtonDown("Jump"))
        {
            jump(groundedL, groundedR);
            animator.SetTrigger("jump");
        }
        if (GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = maxGravity;
            falling = true;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = minGravity;
            falling = false;
        }

        //Salt desde la paret
        if (slidingL && !groundedL && !groundedR && Input.GetButtonDown("Jump"))
        {
            float xSpeed = maxSpeed * jumpXCap;
            wallJumping = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, jumpVelocity);
            jumpR = true;

        }

        if (slidingR && !groundedL && !groundedR && Input.GetButtonDown("Jump"))
        {
            float xSpeed = -maxSpeed * jumpXCap;
            wallJumping = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, jumpVelocity);
            jumpL = true;

        }

        if ((wallJumping) && GetComponent<Rigidbody2D>().velocity.y < 0)
            wallJumping = false;
        if (groundedR || groundedL || slidingR)
            jumpR = false;
        if (groundedR || groundedL || slidingL)
            jumpL = false;

        if ((slidingL || slidingR) && falling)
            GetComponent<Rigidbody2D>().gravityScale = slidingGravity;

        //Posicio del escut
        if (Input.GetButton("Fire1"))
        {
            if (groundedR || groundedL)
            {
                GetComponent<PlayerController>().maxSpeed = newSpeed;
                usingShield = true;
            }
            else
                usingShield = true;
            animator.SetBool("usingShield", true);
        }
        else
        {
            usingShield = false;
            GetComponent<PlayerController>().maxSpeed = maxSpeedCopy;
            animator.SetBool("usingShield", false);
        }

        //Atac amb l'escut

        if (Time.time > nextAtt && grounded && usingShield)
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
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(attSpeed * GetComponent<Rigidbody2D>().velocity.x, 0);
        }
        else
            shieldAtt = false;


    }

    private void move(float input)
    {
        if (wallJumping)
            return;


        Vector2 movement = GetComponent<Rigidbody2D>().velocity;
        movement.x = input * maxSpeed; //Es modifica la velocitat de movement en el eix de les x tal que moviment rebut (input) * maxSpeed = M
        GetComponent<Rigidbody2D>().velocity = movement; //La velocitat modificada s'aplica al GameObject, en aquest cas només modifica el eix de les x [(M,0)]

        if (falling && jumpL && GetComponent<Rigidbody2D>().velocity.x == 0)
        {
            movement.x += maxSpeed / -3;
            GetComponent<Rigidbody2D>().velocity = movement;
        }
        if (falling && jumpR && GetComponent<Rigidbody2D>().velocity.x == 0)
        {
            movement.x += maxSpeed / 3;
            GetComponent<Rigidbody2D>().velocity = movement;
        }
    }

    private void jump(bool groundedL, bool groundedR)
    {
        if (grounded)
        {
            GetComponent<Rigidbody2D>().velocity += jumpVelocity * Vector2.up; //Vector2.up = (0,1) Per tant, x*0 = 0 i x*1 = x [(0,x)]
        }
    }
}




