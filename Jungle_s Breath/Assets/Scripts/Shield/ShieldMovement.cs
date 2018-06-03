using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{

    public GameObject shield;
    public Animator animator;

    private bool onTheGround;
    private bool movingRight;
    public LayerMask whatIsGround;
    public bool shieldGroundedR, shieldGroundedL;
    public Transform shieldGroundR, shieldGroundL;

    ////Moviment del escut
    private bool usingShield;
    private int numberOfPositions = 6;
    public bool[] shieldPos;
    //shieldPos = shieldDefaultR, shieldDefaultL, shieldRight, shieldLeft, shieldUp, shieldDown

    public Transform shieldDefaultR, shieldDefaultL, shieldRight, shieldLeft, shieldUp, shieldDown;


    //Mides del escut
    float XHor, YHor, XVer, YVer;


    // Use this for initialization
    void Start()
    {
        shieldPos = new bool[numberOfPositions];
        shield.transform.position = shieldDefaultR.position;

        XHor = shield.transform.localScale.x;
        YHor = shield.transform.localScale.y;

        YVer = XHor * GameObject.Find("Player").GetComponent<PlayerController>().transform.localScale.x;
        YVer /= GameObject.Find("Player").GetComponent<PlayerController>().transform.localScale.y;

        XVer = YHor * GameObject.Find("Player").GetComponent<PlayerController>().transform.localScale.y;
        XVer /= GameObject.Find("Player").GetComponent<PlayerController>().transform.localScale.x;

    }

    // Update is called once per frame
    void Update()
    {
        onTheGround = GameObject.Find("Player").GetComponent<PlayerController>().onTheGround;
        movingRight = GameObject.Find("Player").GetComponent<PlayerController>().movingRight;
        shieldGroundedR = Physics2D.Linecast(shield.transform.position, shieldGroundR.transform.position, whatIsGround);
        shieldGroundedL = Physics2D.Linecast(shield.transform.position, shieldGroundL.transform.position, whatIsGround);

        usingShield = GameObject.Find("Player").GetComponent<PlayerController>().usingShield;

        shieldPositionChange(usingShield, movingRight, onTheGround, shieldPos);
        shieldPosition(shieldPos);

    }


    void shieldPositionChange(bool usingShield, bool movingRight, bool _onTheGround, bool[] shieldPos)
    {
        //shieldPos = shieldDefaultR, shieldDefaultL, shieldRight, shieldLeft, shieldUp, shieldDown
        if (usingShield && _onTheGround)
        {
            shieldPos[0] = false; //shieldDefaultR
            shieldPos[1] = false; //shieldDefaultL

            if (movingRight)
                shieldPos[2] = true; //shieldRight
            else
                shieldPos[2] = false; //shieldRight

            if (!movingRight)
                shieldPos[3] = true; //shieldLeft
            else
                shieldPos[3] = false; //shieldLeft

            if (Input.GetAxisRaw("Vertical") == 1)
            {
                shieldPos[4] = true; //shieldUp
                shieldPos[2] = false; //shieldRight
                shieldPos[3] = false; //shieldLeft
                animator.SetBool("shieldUp", true);
            }
            else
            {
                shieldPos[4] = false; //shieldUp
                animator.SetBool("shieldUp", false);
            }

            if (Input.GetAxisRaw("Vertical") == -1)
            {
                animator.SetBool("shieldDown", false);

                shieldPos[5] = false; //shieldDown
                if (!movingRight)
                    shieldPos[3] = true; //shieldLeft
                else if (movingRight)
                    shieldPos[2] = true; //shieldRight
            }

        }
        else if (usingShield && !onTheGround)
        {
            if (movingRight)
                shieldPos[2] = true; //shieldRight
            else
                shieldPos[2] = false; //shieldRight

            if (!movingRight)
                shieldPos[3] = true; //shieldLeft
            else
                shieldPos[3] = false; //shieldLeft

            if (Input.GetAxisRaw("Vertical") == 1)
            {
                if (GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.x == 0)
                {
                    shieldPos[4] = true; //shieldUp
                    animator.SetBool("shieldUp", true);
                }
                else
                {
                    if (GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.x < 0)
                    {
                        shieldPos[1] = true; //shieldDefaultL
                        shieldPos[4] = false; //shieldUp
                        animator.SetBool("shieldUp", false);
                    }
                    else if (GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.x > 0)
                    {
                        shieldPos[0] = true; //shieldDefaultR
                        shieldPos[4] = false; //shieldUp
                        animator.SetBool("shieldUp", false);
                    }
                }
            }

            if (Input.GetAxisRaw("Vertical") == -1)//&& !shieldGroundedL && !shieldGroundedR)
            {

                shieldPos[5] = true; //shieldDown
                animator.SetBool("shieldDown", true);
            }
            else
                animator.SetBool("shieldDown", false);
            /*else
            {
                if (movingRight)
                {
                    shieldPos[0] = true; //shieldDefaultR
                    shieldPos[1] = false; //shieldDefaultL
                }
                else if (!movingRight)
                {
                    shieldPos[1] = true; //shieldDefaultL
                    shieldPos[0] = false; //shieldDefaultR
                }
                shieldPos[5] = false;
            }*/


        }

        else if (!usingShield)
        {
            shieldPos[2] = false; //shieldRight
            shieldPos[3] = false; //shieldLeft
            shieldPos[4] = false; //shieldUp
            shieldPos[5] = false; //shieldDown
            animator.SetBool("shieldDown", false);
            animator.SetBool("shieldUp", false);

            if (movingRight)
            {
                shieldPos[0] = true; //shieldDefaultR
                shieldPos[1] = false; //shieldDefaultL
            }

            else if (!movingRight)
            {
                shieldPos[1] = true; //shieldDefaultL
                shieldPos[0] = false; //shieldDefaultR
            }

        }

    }

    void shieldPosition(bool[] shieldPos)
    {
        //shieldPos = shieldDefaultR, shieldDefaultL, shieldRight, shieldLeft, shieldUp, shieldDown
        if (shieldPos[0]) //shieldDefaultR
        {
            shield.transform.position = shieldDefaultR.position;
            shield.transform.localScale = new Vector2(XHor, YHor);
        }
        if (shieldPos[1]) //shieldDefaultL
        {
            shield.transform.position = shieldDefaultL.position;
            shield.transform.localScale = new Vector2(XHor, YHor);
        }
        if (shieldPos[2]) //shieldRight
        {
            shield.transform.position = shieldRight.position;
            shield.transform.localScale = new Vector2(XHor, YHor);
        }
        if (shieldPos[3]) //shieldLeft
        {
            shield.transform.position = shieldLeft.position;
            shield.transform.localScale = new Vector2(XHor, YHor);
        }
        if (shieldPos[4]) //ShieldUp
        {
            shield.transform.position = shieldUp.position;
            shield.transform.localScale = new Vector2(XVer, YVer);
        }
        if (shieldPos[5]) //ShieldDown
        {
            shield.transform.position = shieldDown.position;
            shield.transform.localScale = new Vector2(XVer, YVer);
        }
    }
}

