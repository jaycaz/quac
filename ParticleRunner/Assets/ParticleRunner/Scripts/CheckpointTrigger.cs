using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.name == "Main Camera")
        {
			Debug.Log("Speed Boost");
            EventManager.TriggerEvent("SpeedBoost");
        }
	}
}
