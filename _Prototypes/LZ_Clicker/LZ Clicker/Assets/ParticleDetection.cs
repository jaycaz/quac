using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other) {
		print("OH NO!!!");
		Destroy(other.gameObject);
		StartCoroutine(CollideFlash());
	}

	IEnumerator CollideFlash(){
		this.GetComponent<Renderer>().material.color = Color.red;
		yield return new WaitForSeconds(.1f);
		this.GetComponent<Renderer>().material.color = Color.white;
	}
}
