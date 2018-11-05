using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    




    public float bunchSpeed = 10.0f; //Speed of the pipe relative to the electron bunch
    private GameObject beamPipe;
    private float beamPipeInitialZ; //The initial Z location of the beamPipe.

    //These are the game objects (electrons) created by this game manager
    //and the associated parameters.
    private List<GameObject> electronsInBunch;
    private GameObject electron;
    public GameObject prefabElectron;
    public float initialBunchSize = 10;
    public float electronZPosition = -7f;


    // Use this for initialization
    void Start () {

        //Get the beam pipe and find its position in z
        beamPipe = GameObject.Find("BeamPipe");
        beamPipeInitialZ = beamPipe.transform.position.z;

        //Create a bunch of electrons and start the beam pipe motion.
        electronsInBunch = new List<GameObject>();
        Respawn();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //This sets the speed at which the beampipe moves.
    void SetBeamPipeVelocity(GameObject beamPipe, float velocity){
        Vector3 bunchVel = new Vector3(0f, 0f, -1 * velocity);
        beamPipe.GetComponent<Rigidbody>().velocity = bunchVel;  
    }
  


    //This kills all electrons if there are any, and re-spawns them. It also sets the beam pipe position back to
    //its original position
    public void Respawn()
    {
        
        foreach (GameObject e in electronsInBunch){
            if (e != null){
                Destroy(e.gameObject);
            }
        }


        //Create a bunch of electrons with a random distribution in X, Y
        for (int iEl = 0; iEl < initialBunchSize; ++iEl)
        {
            electron = Instantiate(prefabElectron, new Vector3((Random.value-0.5f), (Random.value-0.5f), electronZPosition), Quaternion.identity);
            electronsInBunch.Add(electron);
        }

        //Move the beamPipe back to the start and start it moving again
        SetBeamPipeVelocity(beamPipe,0f);
        Vector3 pos = new Vector3(beamPipe.transform.position.x, beamPipe.transform.position.y, beamPipeInitialZ);
        beamPipe.transform.position = pos;
        SetBeamPipeVelocity(beamPipe, bunchSpeed); 
    }
}





