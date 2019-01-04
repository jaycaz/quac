using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour {

	private WorldMover m_worldMover;

	// Use this for initialization
	void Start () {
		// TODO(jaycaz): Make this a singleton
		m_worldMover = GameObject.FindObjectOfType<WorldMover>();
	}
	
	// Update is called once per frame
	void Update () {
		if(m_worldMover != null && m_worldMover.gameObject.activeSelf)
		{
			transform.position += 
				Time.deltaTime * m_worldMover.m_moveSpeed * m_worldMover.m_moveAxis;
		}
	}
}
