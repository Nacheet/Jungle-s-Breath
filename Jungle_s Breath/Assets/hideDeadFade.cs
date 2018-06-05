using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideDeadFade : MonoBehaviour {

    public GameObject fade;
    public float time;
    float timeToShow = 2f;

	void Start () {
        fade.SetActive(false);
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time > time + timeToShow && !fade.activeInHierarchy)
            fade.SetActive(true);
	}
}
