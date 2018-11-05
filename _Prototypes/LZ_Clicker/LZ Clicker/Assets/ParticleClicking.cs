using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleClicking : MonoBehaviour {
	public enum ParticleType 
	{
		CosmicRay, Contaminant, Neutron
	}
	public ParticleType mode;
	public float particleSpeed=1.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnMouseDown() {
		Destroy(gameObject);
	}

	public void SetVelocity(Vector3 InitialVelocity){
		//Takes in a Normalized velocity and applies it to the particle
		gameObject.GetComponent<Rigidbody2D>().velocity=InitialVelocity*particleSpeed;
	}
}
