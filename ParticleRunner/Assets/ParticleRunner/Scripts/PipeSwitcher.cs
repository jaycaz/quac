using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSwitcher : MonoBehaviour {

	public List<GameObject> m_pipePrefabList;
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
			GUILayout.Label(string.Format(
				"{0} - {1}",
				i+1,
				m_pipePrefabList[i].name
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
		// Find all Pipe objects
		var scenePipes = GameObject.FindObjectsOfType<Pipe>();

		for(int i = scenePipes.Length - 1; i >= 0; i--)
		{
			Pipe pipe = scenePipes[i];
			GameObject newPipe = GameObject.Instantiate(newPipePrefab);
			newPipe.transform.position = pipe.transform.position;
			newPipe.transform.rotation = pipe.transform.rotation;
			newPipe.transform.localScale = pipe.transform.localScale;

			newPipe.transform.SetParent(pipe.transform.parent);

			GameObject.Destroy(pipe.gameObject);
		}

		if(m_pipeSpawner != null)
		{
			m_pipeSpawner.m_pipePrefab = newPipePrefab;
		}
	}
}
