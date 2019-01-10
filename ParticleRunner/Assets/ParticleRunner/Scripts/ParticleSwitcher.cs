using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSwitcher : MonoBehaviour {

	public List<GameObject> m_particlePrefabList;
	public GameObject m_particleRoot;

	// Update is called once per frame
	void Update () {

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
