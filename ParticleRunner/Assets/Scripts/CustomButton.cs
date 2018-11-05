using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomButton : MonoBehaviour {

	public string EventName;  // Dirty workaround to make the button do a custom non-existent thing
	//private Button thisButton;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonClick () {
            EventManager.TriggerEvent (EventName); //Trigger the corresponding event, the exceptions are handled by the Event Manager.
	}
}
