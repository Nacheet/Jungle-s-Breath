using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour {

    public GameObject startPoint;
    public GameObject endPoint;

    public float enemySpeed;

    private bool rightDirection;
	
	void Start () {

        if (!rightDirection)
        {
            transform.position = startPoint.transform.position;
        }
        else
        {
            transform.position = endPoint.transform.position;
        }
	}
	

	void Update () {

        if (!rightDirection)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, enemySpeed * Time.deltaTime);

            if(transform.position == endPoint.transform.position)
            {
                rightDirection = true;
                GetComponent<SpriteRenderer>().flipX = true;
            }
                
        }

        if (rightDirection)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.transform.position, enemySpeed * Time.deltaTime);
            if (transform.position == startPoint.transform.position)
            {
                rightDirection = false;
                GetComponent<SpriteRenderer>().flipX = false;
            }
               
        }
	}
}
