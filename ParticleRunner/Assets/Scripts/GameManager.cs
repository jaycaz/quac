using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public float bunchSpeed = 10.0f; //Speed of the pipe relative to the electron bunch
    private GameObject beamPipe;

    // Use this for initialization
    void Start () {

        //Get objects from the scene
        beamPipe = GameObject.Find("BeamPipe");



        StartBeamPipeMotion(beamPipe);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Set the velocity of the beam pipe to the public variable above
    void StartBeamPipeMotion(GameObject beamPipe){
        Vector3 bunchVel = new Vector3(0f, 0f, -1*bunchSpeed);
        beamPipe.GetComponent<Rigidbody>().velocity = bunchVel;
    }
}
