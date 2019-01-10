using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleChildObjects : MonoBehaviour {

	public float m_angle = 0f;
	public float m_minAmplitude = 0.5f;
	public float m_maxAmplitude = 1f;
	public float m_minSpeed = 0.5f;
	public float m_maxSpeed = 1f;

	private struct MoveData
	{
		public Vector3 startPos;
		public Vector3 moveAxis;
		public float amplitude;
		public float speed;
		public float time;
	}

	private List<MoveData> m_movement;

	void Start () {
		m_movement = new List<MoveData>();
		
		for(int i = 0; i < transform.childCount; i++)
		{
			Transform t = transform.GetChild(i);

			MoveData m = new MoveData();
			m.time = 0f;
			m.speed = Random.Range(m_minSpeed, m_maxSpeed);
			m.amplitude = Random.Range(m_minAmplitude, m_maxAmplitude);
			Vector2 r = Random.insideUnitCircle;
			m.startPos = t.position;
			m.moveAxis = new Vector3(r.x, r.y, 0f);

			m_movement.Add(m);
		}
	}
	
	void Update () {

		for(int i = 0; i < m_movement.Count; i++)
		{
			MoveData m = m_movement[i];

			if(i < transform.childCount)
			{
				GameObject obj = transform.GetChild(i).gameObject;

				if(obj != null)
				{
					// Update position
					m.time += (m.speed * 2.0f * Mathf.PI * Time.deltaTime);
					obj.transform.position = m.startPos + 
						(m.moveAxis * m.amplitude * Mathf.Cos(m.time));
				}
			}

			m_movement[i] = m;
		}
	}
}
