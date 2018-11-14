using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour {

	public GameObject m_pipePrefab;
	public GameObject m_pipesRoot;

	// Use this for initialization
	void Start () {
		Debug.Assert(m_pipePrefab != null, 
			"PipeSpawner: Missing a pipe prefab to spawn");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			GameObject.Instantiate(m_pipePrefab);
		}
	}

	public void OnTriggerExit(Collider other)
	{
		// Pipe has exited the trigger, spawn a new pipe on the end
		if(other.GetComponent<Pipe>() != null)
		{
			// Debug.Log("Pipe exit");

			// Find last pipe in chain
			Transform t = m_pipesRoot.transform.GetChild(m_pipesRoot.transform.childCount - 1);
			Pipe lastPipe = t.GetComponent<Pipe>();

			// Instantiate new pipe
			GameObject newPipeObj = GameObject.Instantiate(m_pipePrefab);
			Pipe newPipe = newPipeObj.GetComponent<Pipe>();

			newPipeObj.transform.SetParent(m_pipesRoot.transform);

			// Move new pipe so it's lined up with last pipe
			newPipe.transform.position = 
				lastPipe.transform.position + lastPipe.EndTransform.localPosition - newPipe.StartTransform.localPosition;
		}
	}
}
