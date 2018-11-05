using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

	public Transform StartTransform { get { return m_startTransform; } }
	[SerializeField] private Transform m_startTransform;

	public Transform EndTransform { get { return m_endTransform; } }
	[SerializeField] private Transform m_endTransform;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
