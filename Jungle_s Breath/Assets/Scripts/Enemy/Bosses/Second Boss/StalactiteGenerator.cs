using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteGenerator : MonoBehaviour {

    public GameObject stalactite;
    public Transform stalactiteLeft, stalactiteRight;
    public Transform stalactiteSpawn;

    private float distance;
    public float position;
    public float timeSinceLastStalactite;
    public float nextStalactite;

    void Start ()
    {
        distance = stalactiteRight.position.x - stalactiteLeft.position.x;
        nextStalactite = Random.Range(1.0f, 3.0f);
        position = Random.Range(0.0f, distance) + stalactiteLeft.position.x;
    }
	

	void Update ()
    {

        if (Time.time > timeSinceLastStalactite + nextStalactite)
        {
            
            GameObject newStalactite;
            stalactiteSpawn.transform.position = new Vector2(position, stalactiteSpawn.transform.position.y);
            newStalactite = Instantiate<GameObject>(stalactite, stalactiteSpawn.transform);
            timeSinceLastStalactite = Time.time;
            nextStalactite = Random.Range(2.5f, 3.5f);
            position = Random.Range(0, distance) + stalactiteLeft.position.x;
        }
    }
}
