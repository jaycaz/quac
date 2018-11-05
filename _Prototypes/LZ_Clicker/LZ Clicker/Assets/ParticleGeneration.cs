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
	
	public DebugMenu debugMenu;

	public float maxTime = 0.1f;

	private float instantiationTimer; 

	// Generation parameters
	private float spawnRate = 1f; // Rate at which particles should be generated
	private float spawnSpeed = 1f; // Initial velocity multiplier for particles
	private float spawnSize = 1f; // Size at which to spawn particles

	//Coordinates for the particle generation
	private Vector3 spawnAreaCenter;
	private Vector3 spawnAreaRange;
	private GameObject particleInstance;

	//Coordinates for radial movement of particles
	public GameObject innerDetector; // For 'radial' style radiation only
	private Vector3 innerDetectorCenter;
	private Vector3 innerDetectorAreaRange;
	private Vector3 innerDetectorTarget;

	public Vector3 velocity; // For 'shower' style radiation only

	// Use this for initialization
	void Start () {


		spawnAreaCenter=transform.position;
		spawnAreaRange=transform.localScale/2;

		innerDetectorCenter=innerDetector.transform.position;
		innerDetectorAreaRange=innerDetector.transform.localScale/2;

		instantiationTimer = 0f;

		// var debugMenu = GameObject.FindObjectOfType<DebugMenu>();
		if(debugMenu != null)
		{
			debugMenu.OnSpawnRateChange.AddListener(SetSpawnRate);
			debugMenu.OnSpawnSpeedChange.AddListener(SetSpawnSpeed);
			debugMenu.OnSpawnSizeChange.AddListener(SetSpawnSize);
		}
	}

	void OnDestroy() {
		if(debugMenu != null)
		{
			debugMenu.OnSpawnRateChange.RemoveListener(SetSpawnRate);
			debugMenu.OnSpawnSpeedChange.RemoveListener(SetSpawnSpeed);
			debugMenu.OnSpawnSizeChange.RemoveListener(SetSpawnSize);
		}
	}
	
	// Update is called once per frame
	void Update () {
		SpawnParticles();
	}

	public void SetSpawnRate(float spawnRate) {
		this.spawnRate = spawnRate;
	}

	public void SetSpawnSpeed(float spawnSpeed) {
		this.spawnSpeed = spawnSpeed;
	}

	public void SetSpawnSize(float spawnSize) {
		this.spawnSize = spawnSize;
	}

	//Creates a single particle instance
	public void CreateParticle(){
		//select a random place in the quad for the particle generation and instantiate

		Vector3 randomPosition = new Vector3(Random.Range(-spawnAreaRange.x,spawnAreaRange.x),Random.Range(-spawnAreaRange.y,spawnAreaRange.y),0);
		particleInstance=Instantiate(particlePrefab,randomPosition+spawnAreaCenter, Quaternion.Euler(0,0,0));
		switch(mode){
			//Find a random point in the inner detector and aim at it
			case RadiationDirection.Radial:
				innerDetectorTarget=new Vector3(Random.Range(-innerDetectorAreaRange.x,innerDetectorAreaRange.x),Random.Range(-innerDetectorAreaRange.y,innerDetectorAreaRange.y),0);
				velocity=new Vector3(innerDetector.transform.position.x+innerDetectorTarget.x -particleInstance.transform.position.x,innerDetector.transform.position.y +innerDetectorTarget.y-particleInstance.transform.position.y,0);
				break;
			default:
				break;
			}
		particleInstance.GetComponent<ParticleClicking>().SetVelocity(velocity.normalized * spawnSpeed);
		particleInstance.transform.localScale *= spawnSize;
	}
	//Keeps track of time to spaw particles at a fixed rate, determined by spawnRate
	private void SpawnParticles(){
		instantiationTimer += Time.deltaTime;
		float scaledMaxTime = maxTime / spawnRate;
		if (instantiationTimer >= scaledMaxTime)
		{
			CreateParticle();
			instantiationTimer = 0f;
			}
		}
}
