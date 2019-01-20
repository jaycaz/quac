using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour {

	public GameObject m_pipePrefab;
	public GameObject m_pipesRoot;
    private GameManager m_gameManager;

	// Use this for initialization
	void Start () {
		Debug.Assert(m_pipePrefab != null, 
			"PipeSpawner: Missing a pipe prefab to spawn");
        m_gameManager = FindObjectOfType<GameManager>();
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

            //Modify the length of the pipe so that it length contracts as we get faster.
            Vector3 scaleVect = new Vector3(newPipe.transform.localScale.x, newPipe.transform.localScale.y, newPipe.transform.localScale.z / m_gameManager.GetCurrentGamma());
            newPipe.transform.localScale = scaleVect;

            //Also have to modify the start and end transform positions so that they line up with the ends of the pipe
            newPipe.StartTransform.localPosition = new Vector3(newPipe.StartTransform.localPosition.x, newPipe.StartTransform.localPosition.y, newPipe.StartTransform.localPosition.z / m_gameManager.GetCurrentGamma());
            newPipe.EndTransform.localPosition = new Vector3(newPipe.EndTransform.localPosition.x, newPipe.EndTransform.localPosition.y, newPipe.EndTransform.localPosition.z / m_gameManager.GetCurrentGamma());

			// Move new pipe so it's lined up with last pipe
			newPipe.transform.position = 
				lastPipe.transform.position + (lastPipe.EndTransform.localPosition - newPipe.StartTransform.localPosition);

          


		}
	}
}
