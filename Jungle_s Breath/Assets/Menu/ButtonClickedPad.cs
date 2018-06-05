using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonClickedPad : MonoBehaviour, ISelectHandler, IDeselectHandler{
    public bool active;
    public Button button;


	void Start () {
        active = false;
	}
	
	void Update () {
		if(Input.GetButtonDown("Jump") && active)
        {
            button.onClick.Invoke();
        }
	}

    public void OnSelect(BaseEventData eventData)
    {
        active = true;
    }

    public void OnDeselect(BaseEventData data)
    {
        active = false;
    }

    public void setFalse()
    {
        active = false;
    }
}
