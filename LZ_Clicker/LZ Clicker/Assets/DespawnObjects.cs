﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnObjects : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit2D(Collider2D other) {
		Destroy(other.gameObject);
	}
}
