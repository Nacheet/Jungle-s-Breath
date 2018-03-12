using UnityEngine;

public class PlayerController: MonoBehaviour {

    private GameObject player;

    //Variables moviment
    public LayerMask whatIsFloor;
    public Transform groundCheck;
    public float maxSpeed = 17;
    public bool grounded = false;
    public bool movingRight, movingLeft;

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

    void Start ()
    {
        player = GameObject.Find("Player");
        GetComponent<Rigidbody2D>().gravityScale = minGravity;
        maxSpeedCopy = maxSpeed;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Comprova si esta al terra
        grounded = Physics2D.Linecast(player.transform.position, groundCheck.position, whatIsFloor);
        //Comprova si esta tocant la paret dreta o esquerra
        slidingR = Physics2D.Linecast(player.transform.position, playerRight.position, whatIsFloor);
        slidingL = Physics2D.Linecast(player.transform.position, playerLeft.position, whatIsFloor);

        //Moviment lateral
        move(Input.GetAxis("Horizontal"));

        //Per saber si el jugador es mou cap a la dreta o cap a la esquerra
        if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            movingLeft = true;
            movingRight = false;
        }
        else if (GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            movingRight = true;
            movingLeft = false;
        }

        //Salt
        if (Input.GetButtonDown("Jump"))
            jump(grounded);
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
        if (slidingL && !grounded && Input.GetButtonDown("Jump"))
        {
            float xSpeed = maxSpeed * jumpXCap;
            wallJumping = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, jumpVelocity);
            jumpR = true;

        }

        if (slidingR && !grounded && Input.GetButtonDown("Jump"))
        {
            float xSpeed = -maxSpeed * jumpXCap;
            wallJumping = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, jumpVelocity);
            jumpL = true;

        }

        if ((wallJumping) && GetComponent<Rigidbody2D>().velocity.y < 0)
            wallJumping = false;
        if (grounded || slidingR)
            jumpR = false;
        if (grounded || slidingL)
            jumpL = false;

        if ((slidingL || slidingR) && falling)
            GetComponent<Rigidbody2D>().gravityScale = slidingGravity;

        //Posicio del escut
        if (Input.GetButton("Fire1"))
        {
            if (grounded)
            {
                GetComponent<PlayerController>().maxSpeed = newSpeed;
                usingShield = true;
            }
            else
                usingShield = true;
        }
        else
        {
            usingShield = false;
            GetComponent<PlayerController>().maxSpeed = maxSpeedCopy;
        }
    }

    private void move(float input)
    {
        if (wallJumping)
            return;

        Vector2 movement = GetComponent<Rigidbody2D>().velocity;
        movement.x = input * maxSpeed; //Es modifica la velocitat de movement en el eix de les x tal que moviment rebut (input) * maxSpeed = M
        GetComponent<Rigidbody2D>().velocity = movement; //La velocitat modificada s'aplica al GameObject, en aquest cas només modifica el eix de les x [(M,0)]

        if(falling && jumpL && GetComponent<Rigidbody2D>().velocity.x == 0)
        {
            movement.x += maxSpeed / -3;
            GetComponent<Rigidbody2D>().velocity = movement;
        }
        if(falling && jumpR && GetComponent<Rigidbody2D>().velocity.x == 0)
        {
            movement.x += maxSpeed / 3;
            GetComponent<Rigidbody2D>().velocity = movement;
        }
    }

    private void jump(bool grounded)
    {
        if (grounded)
        {
            GetComponent<Rigidbody2D>().velocity += jumpVelocity * Vector2.up; //Vector2.up = (0,1) Per tant, x*0 = 0 i x*1 = x [(0,x)]
        }
    } 
}

   


