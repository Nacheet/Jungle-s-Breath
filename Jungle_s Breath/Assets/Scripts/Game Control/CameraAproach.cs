using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAproach : MonoBehaviour {


    public GameObject camera1;
    public GameObject camera2;

    public float speedPos = 10;
    public float speedSize = 0.086f;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (camera1.GetComponent<Transform>().localPosition.x != camera2.GetComponent<Transform>().localPosition.x)
        {
            camera1.GetComponent<Transform>().localPosition = Vector3.MoveTowards(camera1.GetComponent<Transform>().localPosition, camera2.GetComponent<Transform>().localPosition, speedPos * Time.deltaTime);
        }

        if (camera1.GetComponent<Camera>().orthographicSize < camera2.GetComponent<Camera>().orthographicSize)
        {
            camera1.GetComponent<Camera>().orthographicSize = camera1.GetComponent<Camera>().orthographicSize + speedSize;
        }

        if (camera1.GetComponent<Camera>().orthographicSize > camera2.GetComponent<Camera>().orthographicSize)
        {
            camera1.GetComponent<Camera>().orthographicSize = camera1.GetComponent<Camera>().orthographicSize - speedSize;
        }
    }
}
