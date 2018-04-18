using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOrHideGameObject : MonoBehaviour {


    public bool show;

    public GameObject go;

    public void hideGo()
    {
        go.SetActive  (false);
    }

    public void  showGo()
    {
        go.SetActive(true);
    }


    void Update()
    {
        if (show)
            showGo();
    }
}
