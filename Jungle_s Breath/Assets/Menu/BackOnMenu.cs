using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackOnMenu : MonoBehaviour
{

    public Button button;

	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetButtonDown("Back"))
        {
            button.onClick.Invoke();
        }

	}
}




