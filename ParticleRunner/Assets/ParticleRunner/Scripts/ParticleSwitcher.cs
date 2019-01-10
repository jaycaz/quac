using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSwitcher : MonoBehaviour {

	public List<GameObject> m_particlePrefabList;
	public GameObject m_particleRoot;

	void OnGUI()
	{
		// // GUILayout.Label(
		// // "Enter - Next Pipe,\n" + 
		// // "Backspace - Prev Pipe\n" + 
		// // "Number Key - Select Pipe (1-9)\n");

		// GUILayout.Label("Number Key - Select Pipe (1-9)\n");

		// for(int i = 0; i < m_pipePrefabList.Count; i++)
		// {
		// 	string entryName = (i < m_names.Count) ? m_names[i] : m_pipePrefabList[i].name;
		// 	GUILayout.Label(string.Format(
		// 		"{0} - {1}",
		// 		i+1,
		// 		entryName
		// 	));
		// }
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
			SwitchAllParticles(0);
		if(Input.GetKeyDown(KeyCode.Alpha2))
			SwitchAllParticles(1);
		if(Input.GetKeyDown(KeyCode.Alpha3))
			SwitchAllParticles(2);
		if(Input.GetKeyDown(KeyCode.Alpha4))
			SwitchAllParticles(3);
		if(Input.GetKeyDown(KeyCode.Alpha5))
			SwitchAllParticles(4);
		if(Input.GetKeyDown(KeyCode.Alpha6))
			SwitchAllParticles(5);
		if(Input.GetKeyDown(KeyCode.Alpha7))
			SwitchAllParticles(6);
		if(Input.GetKeyDown(KeyCode.Alpha8))
			SwitchAllParticles(7);
		if(Input.GetKeyDown(KeyCode.Alpha9))
			SwitchAllParticles(8);

	}

	public void SwitchAllParticles(int index)
	{
		if(index < m_particlePrefabList.Count)
		{
			SwitchAllParticles(m_particlePrefabList[index]);
		}
	}

	public void SwitchAllParticles(GameObject newPipePrefab)
	{
		// Find all particle objects and replace them
		int numParticles = m_particleRoot.transform.childCount;
		Electron[] originalPipes = m_particleRoot.GetComponentsInChildren<Electron>();

		for(int i = originalPipes.Length - 1; i >= 0; i--)
		{
			GameObject.Destroy(originalPipes[i].gameObject);
		}
	}
}
