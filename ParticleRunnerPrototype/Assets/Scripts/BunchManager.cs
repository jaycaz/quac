﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunchManager : MonoBehaviour {




    public int initialBunchSize = 3;
    private List<GameObject> electrons;
    public float addedForce = 10;
    public GameObject prefabElectron;
    private GameObject electron;
    private GameObject electronParent;
    public float quadForceTuningParam = 1;



	// Use this for initialization
	void Start () {
        electrons = new List<GameObject>();
        Respawn();

        // electronParent = new GameObject("ElectronParent");

        DebugMenu debugMenu = GameObject.FindObjectOfType<DebugMenu>();
        if(debugMenu != null)
        {
            debugMenu.OnDriftForceChange.AddListener(SetParticleDriftForce);
            debugMenu.OnTuningForceChange.AddListener(SetTuningForce);
            debugMenu.OnNumParticlesChange.AddListener(SetNumParticles);
            debugMenu.OnResetParticles.AddListener(Respawn);
        }
	}
	
	// Update is called once per frame
	void Update () {

        // Debug.Log("Fuck");

	}

    public void Respawn()
    {
        foreach(GameObject e in electrons)
        {
            if(e != null)
            {
                Destroy(e.gameObject);
            }
        }

        //Create a bunch of electrons
        electrons = new List<GameObject>();
        for (int iEl = 0; iEl < initialBunchSize; ++iEl ){
            electron = Instantiate(prefabElectron, new Vector3(0f,0f,0f), Quaternion.identity );
            electrons.Add(electron);
        }
    }

    public void SetTuningForce(float force)
    {
        addedForce = force;
    }

    public void SetParticleDriftForce(float force)
    {
        for(int i = 0; i < electrons.Count; i++)
        {
            if (electrons[i] == null) continue;
            electrons[i].GetComponent<ElectronBehavior>().addedForce = force;
        }
    }

    public void SetNumParticles(int numParticles)
    {
        initialBunchSize = numParticles;
    }
    public void SetNumParticles(float numParticles)
    {
        SetNumParticles((int) numParticles);
    }

    //This tunes the beam of particles by acting on it with a quadrupole force
    public void tuneBeamQuadrupole(float phi)
    {
        //If you press the top or bottom buttons, focus in one direction
        if (phi == 180 || phi == 0)
        {
            int numElectrons = electrons.Count;
            for (int iE = 0; iE < numElectrons; ++iE)
            {
                //Loop over all electrons and identify their x and y positions
                if (electrons[iE] == null) continue;
                Transform tf = electrons[iE].gameObject.transform;
                float xi = tf.position.x;
                float yi = tf.position.y;

                //Calculate the magnetic focusing force based on this position
                float fx = 1 * quadForceTuningParam * xi;
                float fy = -1 *quadForceTuningParam * yi;

                Rigidbody rb = electrons[iE].gameObject.GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(fx, fy, 0f));

            }   
        }
        else if (phi == 90 || phi == 270)
        {
            int numElectrons = electrons.Count;
            for (int iE = 0; iE < numElectrons; ++iE)
            {
                //Loop over all electrons and identify their x and y positions
                if (electrons[iE] == null) continue;
                Transform tf = electrons[iE].gameObject.transform;
                float xi = tf.position.x;
                float yi = tf.position.y;

                //Calculate the magnetic focusing force based on this position
                float fx = -1* quadForceTuningParam * xi;
                float fy = 1 * quadForceTuningParam * yi;

                Rigidbody rb = electrons[iE].gameObject.GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(fx, fy, 0f));

            }   
        }
        else{
            Debug.Log("This button is currently inactive.");
        }
        
    }

    public void tuneBeamAll( float phi )
    {
        
        float x = addedForce * Mathf.Cos(phi * Mathf.PI / 180f);
        float y = addedForce * Mathf.Sin(phi * Mathf.PI / 180f);


        //Loop over all objects with the electron tag. Only bump them if they are near the edge
        // GameObject[] electrons = GameObject.FindGameObjectsWithTag("electron");
        int numElectrons = electrons.Count;
        for (int iE = 0; iE < numElectrons; ++iE ){
            if(electrons[iE] == null) continue;
            Transform tf = electrons[iE].gameObject.transform;
            float xi = tf.position.x;
            float yi = tf.position.y;
            float ri = Mathf.Pow(xi * xi + yi * yi, 0.5f);
            float phii = 180f/Mathf.PI*Mathf.Atan2(yi, xi);

            if (Mathf.Abs(phii - phi + 180) < 22.5f && ri > 2f)
            {
                Rigidbody rb = electrons[iE].gameObject.GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(x, y, 0f));
            }
        }
    }


}
