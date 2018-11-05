﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronBehavior : MonoBehaviour {

    //Parameters for tuning beam
    public float quadForceTuningParam = 1;

    private float electronWiggleParameter = 0;
    private float electronWiggleFrequency; //The frequency of sinusoidal oscillation is defined here.
    private float initialElectronPositionZ;


	// Use this for initialization
	void Start () {

        //Define the frequency once here, with a random value
        electronWiggleFrequency = Random.value;

        //Identify initial parameters of the electron
        initialElectronPositionZ = gameObject.transform.position.z;

	}
	
	// Update is called once per frame
	void Update () {

        WiggleElectronInZ();


	}

    //Move the electrons somehow in z
    private void WiggleElectronInZ()
    {
        electronWiggleParameter += 0.03f*electronWiggleFrequency;
        float electronPositionZ = initialElectronPositionZ + 1f*Mathf.Sin(electronWiggleParameter);
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, electronPositionZ);
        gameObject.transform.position = pos;
    }


    //Magnetic field focusing function: exert the appropriate tuning force on
    //this electron. //if direction == 1, then we're at 0 or 180
    public void DoQuadrupoleFocusingGeneral(int direction)
    {

        //Identify X and Y position of this electron
        Transform tf = gameObject.transform;
        float xi = tf.position.x;
        float yi = tf.position.y;

        //Calculate the magnetic focusing force based on this position
        float fx = direction * quadForceTuningParam * xi;
        float fy = -1*direction * quadForceTuningParam * yi;

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(fx, fy, 0f));
    }
        
    //Specific Focusing in X
    public void DoQuadrupoleFocusingX()
    {
        DoQuadrupoleFocusingGeneral(-1);
    }

    //Specific focusing in Y
    public void DoQuadrupoleFocusingY()
    {
        DoQuadrupoleFocusingGeneral(1);
    }


    //Coulomb forces
    private void DoSpaceChargeForces()
    {
        //Find all of the other electrons that have been created
        GameObject[] otherElectrons = GameObject.FindGameObjectsWithTag("Electron");

        //Loop through all electrons
        //foreach( GameObject e in otherElectrons )



    }



}
