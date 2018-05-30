using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteSpawn : MonoBehaviour {

    public GameObject stalactite;

    float time;
    float timeToSpawn = 4f;

    void Update()
    {
        if (Time.time > time + timeToSpawn)
        {
            GameObject newStalactite;
            newStalactite = Instantiate<GameObject>(stalactite, this.transform);

            time = Time.time;
        }
    }
}
