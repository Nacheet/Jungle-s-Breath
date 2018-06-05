using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateLimitColliders : MonoBehaviour {

    public GameObject limit1;
    public GameObject limit2;

    public GameObject batBoss;

	void Start () {
        limit1.SetActive(false);
        limit2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(batBoss.GetComponent<BatBoss>().activateCave)
        {
            limit1.SetActive(true);
            limit2.SetActive(true);
        }
	}
}
