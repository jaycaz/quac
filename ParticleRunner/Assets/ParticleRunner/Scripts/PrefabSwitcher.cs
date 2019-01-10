using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSwitcher : MonoBehaviour {

	public List<GameObject> m_pipePrefabList;
	public List<string> m_names;
	public PipeSpawner m_pipeSpawner;

	private int m_currentIndex = 0;

	void OnGUI()
	{
		// GUILayout.Label(
		// "Enter - Next Pipe,\n" + 
		// "Backspace - Prev Pipe\n" + 
		// "Number Key - Select Pipe (1-9)\n");

		GUILayout.Label("Number Key - Select Pipe (1-9)\n");

		for(int i = 0; i < m_pipePrefabList.Count; i++)
		{
			string entryName = (i < m_names.Count) ? m_names[i] : m_pipePrefabList[i].name;
			GUILayout.Label(string.Format(
				"{0} - {1}",
				i+1,
				entryName
			));
		}
	}

	// Update is called once per frame
	void Update () {

		// if(Input.GetKeyDown(KeyCode.Return))
		// {
		// 	m_currentIndex = (m_currentIndex + 1) % m_pipePrefabList.Count;
		// 	SwitchAllPipes(m_currentIndex);
		// }
		// if(Input.GetKeyDown(KeyCode.Backspace))
		// {
		// 	m_currentIndex = (m_currentIndex - 1 + m_pipePrefabList.Count) % m_pipePrefabList.Count;
		// 	SwitchAllPipes(m_currentIndex);
		// }
		if(Input.GetKeyDown(KeyCode.Alpha1))
			SwitchAllPipes(0);
		if(Input.GetKeyDown(KeyCode.Alpha2))
			SwitchAllPipes(1);
		if(Input.GetKeyDown(KeyCode.Alpha3))
			SwitchAllPipes(2);
		if(Input.GetKeyDown(KeyCode.Alpha4))
			SwitchAllPipes(3);
		if(Input.GetKeyDown(KeyCode.Alpha5))
			SwitchAllPipes(4);
		if(Input.GetKeyDown(KeyCode.Alpha6))
			SwitchAllPipes(5);
		if(Input.GetKeyDown(KeyCode.Alpha7))
			SwitchAllPipes(6);
		if(Input.GetKeyDown(KeyCode.Alpha8))
			SwitchAllPipes(7);
		if(Input.GetKeyDown(KeyCode.Alpha9))
			SwitchAllPipes(8);

	}

	public void SwitchAllPipes(int index)
	{
		if(index < m_pipePrefabList.Count)
		{
			m_currentIndex = index;
			SwitchAllPipes(m_pipePrefabList[index]);
		}
	}

	public void SwitchAllPipes(GameObject newPipePrefab)
	{
		// Find all Pipe objects and replace them
		Transform pipeRoot = m_pipeSpawner.m_pipesRoot.transform;
		int numPipes = pipeRoot.childCount;
		Pipe[] originalPipes = pipeRoot.GetComponentsInChildren<Pipe>();

		if(numPipes > 0)
		{
			// Instantiate first new pipe
			Transform firstPipeTransform = pipeRoot.GetChild(0);
			GameObject firstNewPipe = GameObject.Instantiate(newPipePrefab);
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
				GameObject newPipe = GameObject.Instantiate(newPipePrefab);
				newPipe.transform.position = prevPipe.transform.position + 
					(prevPipe.EndTransform.localPosition - prevPipe.StartTransform.localPosition);
				newPipe.transform.rotation = prevPipe.transform.rotation;
				newPipe.transform.localScale = prevPipe.transform.localScale;
				newPipe.transform.SetParent(pipeRoot);
				prevPipe = newPipe.GetComponent<Pipe>();
			}
		}

		if(m_pipeSpawner != null)
		{
			m_pipeSpawner.m_pipePrefab = newPipePrefab;
		}
	}
}
