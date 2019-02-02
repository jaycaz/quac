using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PipeMaterialSwitcher will "eyedropper" materials from one pipe to another
public class PipeMaterialSwitcher : Switcher<Pipe> {

	public PipeSpawner m_pipeSpawner;

	protected override void SwitchAllObjects(Pipe newPipe)
	{
		m_pipeSpawner.m_pipeMaterialPrefab = newPipe;

		// Find all Pipe objects and replace them
		Transform pipeRoot = m_pipeSpawner.m_pipesRoot.transform;
		Pipe[] originalPipes = pipeRoot.GetComponentsInChildren<Pipe>();
		foreach(Pipe pipe in originalPipes)
		{
			pipe.SetMaterials(newPipe.m_darkMaterial, newPipe.m_lightMaterial);
		}
	}
}
