using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour {

	public float m_moveAmount = 0.2f;
	public float m_moveTime = 0.2f;
	public float m_rotateTime = 0.2f;

	public Transform m_leftPos;
	public Transform m_rightPos;
	public Transform m_forwardPos;
	public Transform m_backPos;

	private Vector3 m_startPos;
	private Vector3 m_velocity;

	void Start () {
		m_startPos = transform.position;
	}

	void Update () {

		Vector3 targetPos = m_startPos;
		Quaternion targetRot = Quaternion.identity;
		if(Input.GetKey(KeyCode.UpArrow))
		{
			targetPos = m_forwardPos.position;
			targetRot = m_forwardPos.rotation;
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			targetPos = m_backPos.position;
			targetRot = m_backPos.rotation;
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			targetPos = m_leftPos.position;
			targetRot = m_leftPos.rotation;
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			targetPos = m_rightPos.position;
			targetRot = m_rightPos.rotation;
		}

		transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref m_velocity, m_moveTime);
		float rotAngle = Quaternion.Angle(transform.rotation, targetRot);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotAngle * m_rotateTime);
	}
}
