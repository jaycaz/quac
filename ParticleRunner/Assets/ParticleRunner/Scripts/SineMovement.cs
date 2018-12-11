using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour {

	public Vector3 m_moveAxis = Vector3.up;
	public float m_angle = 0f;
	public bool m_randomAxis = false;
	public float m_amplitude = 1f;
	public float m_speed = 1f;

	private Vector3 m_startPos;
	private float m_t;

	void Start () {
		m_startPos = transform.position;

		if(m_randomAxis)
		{
			Vector2 r = Random.insideUnitCircle;
			m_moveAxis = new Vector3(r.x, r.y, 0f);
		}
	}
	
	void Update () {
		m_t += m_speed * 2.0f * Mathf.PI * Time.deltaTime;
		transform.position = m_startPos + (m_moveAxis * m_amplitude * Mathf.Cos(m_t));
	}
}
