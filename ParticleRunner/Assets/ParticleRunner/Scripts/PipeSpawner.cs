using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour {

	public GameObject m_pipePrefab;

	public GameObject m_pipesRoot;
	public List<Pipe> m_pipes;

	// Use this for initialization
	void Start () {
		Debug.Assert(m_pipePrefab != null, 
			"PipeSpawner: Missing a pipe prefab to spawn");

		m_pipes = new List<Pipe>();
		for(int i = 0; i < m_pipesRoot.transform.childCount; i++)
		{
			Pipe pipe = m_pipesRoot.transform.GetChild(i).gameObject.GetComponent<Pipe>();
			if(pipe != null)
			{
				Debug.Log(pipe);
				m_pipes.Add(pipe);
			}
		}
		Debug.LogFormat("Starting with {0} pipes", m_pipes.Count);
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
			Pipe lastPipe = m_pipes[m_pipes.Count - 1];

			// Instantiate new pipe
			GameObject newPipeObj = GameObject.Instantiate(m_pipePrefab);
			Pipe newPipe = newPipeObj.GetComponent<Pipe>();

			// Move new pipe so it's lined up with last pipe
			newPipe.transform.position = 
				lastPipe.transform.position + lastPipe.EndTransform.localPosition - newPipe.StartTransform.localPosition;
			m_pipes.Add(newPipe);
		}


	}
}
