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
            if(transform.childCount >= 1)
            {
                ClearChildren();
            }
            GameObject newStalactite;
            newStalactite = Instantiate<GameObject>(stalactite, this.transform);

            time = Time.time;
        }
    }

    public void ClearChildren()
    {
        int i = 0;
        GameObject[] allChildren = new GameObject[transform.childCount];

        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        foreach (GameObject child in allChildren)
        {
            if(!child.GetComponent<Stalactite>().falling)
                DestroyImmediate(child.gameObject);
        }
    }
}
