using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleSwitcher : Switcher<Electron> {

	public GameObject m_particleRoot;

	protected override void SwitchAllObjects(GameObject newParticlePrefab)
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
