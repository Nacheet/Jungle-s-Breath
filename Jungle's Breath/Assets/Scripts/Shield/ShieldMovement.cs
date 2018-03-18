using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{

    GameObject shield;

    private bool groundedL, groundedR;
    private bool movingRight, movingLeft;
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
        shield = GameObject.Find("Shield");
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
        groundedL = GameObject.Find("Player").GetComponent<PlayerController>().groundedL;
        groundedR = GameObject.Find("Player").GetComponent<PlayerController>().groundedR;
        movingRight = GameObject.Find("Player").GetComponent<PlayerController>().movingRight;
        movingLeft = GameObject.Find("Player").GetComponent<PlayerController>().movingLeft;
        shieldGroundedR = Physics2D.Linecast(shield.transform.position, shieldGroundR.transform.position, whatIsGround);
        shieldGroundedL = Physics2D.Linecast(shield.transform.position, shieldGroundL.transform.position, whatIsGround);

        usingShield = GameObject.Find("Player").GetComponent<PlayerController>().usingShield;

        shieldPositionChange(usingShield, movingRight, movingLeft, groundedR, groundedL, shieldPos);
        shieldPosition(shieldPos);

    }


    void shieldPositionChange(bool usingShield, bool movingRight, bool movingLeft, bool groundedR, bool groundedL, bool[] shieldPos)
    {
        //shieldPos = shieldDefaultR, shieldDefaultL, shieldRight, shieldLeft, shieldUp, shieldDown
        if (usingShield && (groundedR || groundedL))
        {
            shieldPos[0] = false; //shieldDefaultR
            shieldPos[1] = false; //shieldDefaultL

            if (movingRight)
                shieldPos[2] = true; //shieldRight
            else
                shieldPos[2] = false; //shieldRight

            if (movingLeft)
                shieldPos[3] = true; //shieldLeft
            else
                shieldPos[3] = false; //shieldLeft

            if (Input.GetAxisRaw("Vertical") == 1)
            {
                shieldPos[4] = true; //shieldUp
                shieldPos[2] = false; //shieldRight
                shieldPos[3] = false; //shieldLeft
            }
            else
                shieldPos[4] = false; //shieldUp

            if (Input.GetAxisRaw("Vertical") == -1)
            {
                shieldPos[5] = false; //shieldDown
                if(movingLeft)
                    shieldPos[3] = true; //shieldLeft
                else if(movingRight)
                    shieldPos[2] = true; //shieldRight
            }

        }
        else if (usingShield && !groundedL && !groundedR)
        {
            if (movingRight)
                shieldPos[2] = true; //shieldRight
            else
                shieldPos[2] = false; //shieldRight

            if (movingLeft)
                shieldPos[3] = true; //shieldLeft
            else
                shieldPos[3] = false; //shieldLeft

            if (Input.GetAxisRaw("Vertical") == 1)
            {
                if (GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.x == 0)
                    shieldPos[4] = true; //shieldUp
                else
                {
                    if (GameObject.Find("Player").GetComponent <Rigidbody2D>().velocity.x < 0)
                    {
                        shieldPos[1] = true; //shieldDefaultL
                        shieldPos[4] = false; //shieldUp
                    }
                    else if(GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.x > 0)
                    {
                        shieldPos[0] = true; //shieldDefaultR
                        shieldPos[4] = false; //shieldUp
                    }
                }
            }

            if (Input.GetAxisRaw("Vertical") == -1 && !shieldGroundedL && !shieldGroundedR)
            {
              
                shieldPos[5] = true; //shieldDown
            }
            else
            {
                if(movingRight)
                {
                    shieldPos[0] = true; //shieldDefaultR
                    shieldPos[1] = false; //shieldDefaultL
                }
                else if (movingLeft)
                {
                    shieldPos[1] = true; //shieldDefaultL
                    shieldPos[0] = false; //shieldDefaultR
                }
                shieldPos[5] = false;
            }


        }

        else if (!usingShield)
        {
            shieldPos[2] = false; //shieldRight
            shieldPos[3] = false; //shieldLeft
            shieldPos[4] = false; //shieldUp
            shieldPos[5] = false; //shieldDown

            if (movingRight)
            {
                shieldPos[0] = true; //shieldDefaultR
                shieldPos[1] = false; //shieldDefaultL
            }

            else if (movingLeft)
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
        if(shieldPos[1]) //shieldDefaultL
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
