using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour {
	public void OnTriggerEnter(Collider other)
	{
		// Pipe has exited the trigger, spawn a new pipe on the end
		if(other.GetComponent<Pipe>() != null)
		{
			GameObject.Destroy(other.gameObject);
            Debug.Log("Kill pipe");
		}
	}
}
