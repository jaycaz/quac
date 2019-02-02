using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour {

	public GameObject m_pipePrefab;
	public Pipe m_pipeMaterialPrefab;
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

	public void SwitchAllPipes(Pipe newPipePrefab)
	{
		// Find all Pipe objects and replace them
		Transform pipeRoot = m_pipesRoot.transform;
		int numPipes = pipeRoot.childCount;
		Pipe[] originalPipes = pipeRoot.GetComponentsInChildren<Pipe>();

		if(numPipes > 0)
		{
			// Instantiate first new pipe
			Transform firstPipeTransform = pipeRoot.GetChild(0);
			GameObject firstNewPipe = GameObject.Instantiate(newPipePrefab.gameObject);
			firstNewPipe.transform.position = firstPipeTransform.position;
			firstNewPipe.transform.rotation = firstPipeTransform.rotation;
			firstNewPipe.transform.localScale = firstPipeTransform.localScale;

			// Destroy original pipes
			for(int i = originalPipes.Length - 1; i >= 0; i--)
			{
				GameObject.Destroy(originalPipes[i].gameObject);
			}

			// Add first pipe
			firstNewPipe.transform.SetParent(pipeRoot);

			// Instantiate the rest of the f***ing owl
			Pipe prevPipe = firstNewPipe.GetComponent<Pipe>();
			Debug.Assert(prevPipe != null, "PipeSwitcher: cannot switch to object without Pipe component");
			for(int i = 1; i < numPipes; i++)
			{
				// Place each new pipe at the end of the previous pipe
				GameObject newPipeObj = NewPipeInstance(newPipePrefab.gameObject);
				newPipeObj.transform.position = prevPipe.transform.position + 
					(prevPipe.EndTransform.localPosition - prevPipe.StartTransform.localPosition);
				newPipeObj.transform.rotation = prevPipe.transform.rotation;
				newPipeObj.transform.localScale = prevPipe.transform.localScale;
				newPipeObj.transform.SetParent(pipeRoot);
				prevPipe = newPipeObj.GetComponent<Pipe>();
			}
		}

		m_pipePrefab = newPipePrefab.gameObject;
	}

	private GameObject NewPipeInstance(GameObject pipePrefab)
	{
		GameObject newPipeObj = GameObject.Instantiate(pipePrefab.gameObject);
		Pipe newPipe = newPipeObj.GetComponent<Pipe>();
		newPipe.SetMaterialsFromPipe(m_pipeMaterialPrefab);

		return newPipeObj;
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
			GameObject newPipeObj = NewPipeInstance(m_pipePrefab);
			Pipe newPipe = newPipeObj.GetComponent<Pipe>();

			newPipeObj.transform.SetParent(m_pipesRoot.transform);

            //Modify the length of the pipe so that it length contracts as we get faster.
			if(m_gameManager != null)
			{
				Vector3 scaleVect = new Vector3(newPipe.transform.localScale.x, newPipe.transform.localScale.y, newPipe.transform.localScale.z / m_gameManager.GetCurrentGamma());
				newPipe.transform.localScale = scaleVect;
				//Also have to modify the start and end transform positions so that they line up with the ends of the pipe
				newPipe.StartTransform.localPosition = new Vector3(newPipe.StartTransform.localPosition.x, newPipe.StartTransform.localPosition.y, newPipe.StartTransform.localPosition.z / m_gameManager.GetCurrentGamma());
				newPipe.EndTransform.localPosition = new Vector3(newPipe.EndTransform.localPosition.x, newPipe.EndTransform.localPosition.y, newPipe.EndTransform.localPosition.z / m_gameManager.GetCurrentGamma());
			}

			// Move new pipe so it's lined up with last pipe
			newPipe.transform.position = 
				lastPipe.transform.position + (lastPipe.EndTransform.localPosition - newPipe.StartTransform.localPosition);
		}
	}
}
