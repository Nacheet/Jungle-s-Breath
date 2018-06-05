using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteBat : MonoBehaviour {

    const int numberOfPositions = 4;
    public Transform[] positions;


	void Start ()
    {
        positions = new Transform[numberOfPositions];
	}
	

	void Update ()
    {
		for(int i = 0; i < numberOfPositions; i++)
        {
            //if(positions[i].GetComponent<>().)
        }
	}
}
