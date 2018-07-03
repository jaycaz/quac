using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGeneration : MonoBehaviour {
	public enum RadiationDirection 
	{
		Radial, Shower
	}

	public RadiationDirection mode;

	public GameObject particlePrefab;
	
	public float maxTime =5.0f; //Time for generating the next particle
	private float timer; 

	//Coordinates for the particle generation
	private Vector3 spawnAreaCenter;
	private Vector3 spawnAreaRange;
	private GameObject particleInstance;

	//Coordinates for radial movement of particles
	public GameObject innerDetector; // For 'radial' style radiation only
	public Vector3 velocity; // For 'shower' style radiation only

	// Use this for initialization
	void Start () {


		spawnAreaCenter=transform.position;
		spawnAreaRange=transform.localScale/2;

		timer= maxTime;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space")){

			createParticle();
		}
	}

	public void createParticle(){
		//select a random place in the quad for the particle generation and instantiate

		Vector3 randomPosition = new Vector3(Random.Range(-spawnAreaRange.x,spawnAreaRange.x),Random.Range(-spawnAreaRange.y,spawnAreaRange.y),0);
		particleInstance=Instantiate(particlePrefab,randomPosition+spawnAreaCenter, Quaternion.Euler(0,0,0));
		switch(mode){
			case RadiationDirection.Radial:
				velocity=new Vector3(innerDetector.transform.position.x -particleInstance.transform.position.x,innerDetector.transform.position.y -particleInstance.transform.position.y,0);
				break;
			default:
				break;
			}
		particleInstance.GetComponent<ParticleClicking>().SetVelocity(velocity.normalized);
	}
}
