using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleSwitcher : MonoBehaviour {

	public List<GameObject> m_particlePrefabList;
	public List<string> m_names;
	public GameObject m_particleRoot;
	public SwitcherUtil.SwitcherType m_switcherDebugInputType;

	private int m_currentIndex = 0;

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(10 + 100 * (int) m_switcherDebugInputType, 10, 110, 500));
		GUILayout.BeginVertical();
		GUILayout.Label("Select Particle");

		for(int i = 0; i < m_particlePrefabList.Count; i++)
		{
			bool current = m_currentIndex == i;
			string entryName = (i < m_names.Count) ? m_names[i] : m_particlePrefabList[i].name;
			GUILayout.Label(string.Format(
				"{0}{1} - {2}{3}",
				current ? "[" : "",
				SwitcherUtil.GetKeyStr(m_switcherDebugInputType, i),
				entryName,
				current ? "]" : ""
			));
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	// Update is called once per frame
	void Update () {

		int input = SwitcherUtil.GetInput(m_switcherDebugInputType);

		if(input > 0)
		{
			SwitchAllParticles(input);
		}
	}

	public void SwitchAllParticles(int index)
	{
		m_currentIndex = index;
		if(index < m_particlePrefabList.Count)
		{
			SwitchAllParticles(m_particlePrefabList[index - 1]);
		}
	}

	public void SwitchAllParticles(GameObject newParticlePrefab)
	{
		// Find all particle objects and replace them
		int numParticles = m_particleRoot.transform.childCount;
		Electron[] originalParticles = m_particleRoot.GetComponentsInChildren<Electron>();

		for(int i = originalParticles.Length - 1; i >= 0; i--)
		{
			Electron p = originalParticles[i];
			GameObject newParticle = GameObject.Instantiate(newParticlePrefab, p.transform.position, p.transform.rotation);
			newParticle.transform.parent = m_particleRoot.transform;
			GameObject.Destroy(originalParticles[i].gameObject);
		}
	}
}
