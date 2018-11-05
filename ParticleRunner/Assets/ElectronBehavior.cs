using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronBehavior : MonoBehaviour {

    //Parameters for tuning beam
    public float quadForceTuningParam = 1;
    public float coulombForceTuningParam = 1;

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
        DoSpaceChargeForces();






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

        //Resultant force vector
        Vector3 CoulombForceTotal = new Vector3(0,0,0);

        //Find the transform of this electron
        Vector3 thisElectronPosition = gameObject.transform.position;

        //Loop through all electrons
        foreach (GameObject e in otherElectrons)
        {
            if (e.gameObject == this.gameObject) continue;
            else{

                //Find the position of this electron
                Vector3 otherElectronPosition = e.transform.position;

                //Find the direction of the force, determine the distance, and then normalize the vector
                Vector3 forceDirection = thisElectronPosition - otherElectronPosition;
                float distance2 = forceDirection.sqrMagnitude;
                forceDirection.Normalize();

                //Now find the magnitude of the force between these two particles
                float forceMag = coulombForceTuningParam / distance2;
                Vector3 magVector = new Vector3(forceMag, forceMag, 0); //Don't want to push it in Z...
                Vector3 forceTotal = Vector3.Scale(forceDirection, magVector);
                CoulombForceTotal += forceTotal;
            }
        }

        //Now add a force
        gameObject.GetComponent<Rigidbody>().AddForce(CoulombForceTotal);

    }


    //Check to see if this collision kills the electron.
	private void OnCollisionEnter(Collision collision)
	{
		
	}

}
