using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {



    //Parameters associated with speeding up the tunnel
    public float dTPerSpeedup = 5f; //in s
    public float currentTime = 0;
    private float currentGamma = 1f;
    private WorldMover worldmover;
    

    //These are the game objects (electrons) created by this game manager
    //and the associated parameters.
    private List<GameObject> electronsInBunch;
    private GameObject electron;
    public GameObject prefabElectron;
    public float initialBunchSize = 10;
    public float electronZPosition = -7f;
    public float startingBunchSpanTuningParam = 0.1f;

    //These deal with the end-conditions of the game
    public float gameTime = 30f; //seconds





    // Subscribe to events as soon as this object is enabled
        void OnEnable () {
       // EventManager.StartListening("SpeedBoost",IncrementPipeVelocity);
    }

    void OnDisable () {
       // EventManager.StopListening("SpeedBoost",IncrementPipeVelocity);
    }
    

    // Use this for initialization
    void Start () {


        //Create a bunch of electrons and start the beam pipe motion.
        electronsInBunch = new List<GameObject>();
        Respawn();

        //Get the world mover object
        worldmover = FindObjectOfType<WorldMover>();
    

	}
	
	// Update is called once per frame
	void Update () {

        //Update the length contraction factor only when the klystrons are fired.


        /*
        //update length contraction factor continuously
        if( currentGamma < 5f ){
            if( Time.time - currentTime > dTPerSpeedup ){
                currentTime = Time.time;
                currentGamma += 0.5f;
            }
        }
        */
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
            electron = Instantiate(prefabElectron, new Vector3(startingBunchSpanTuningParam * (Random.value - 0.5f), startingBunchSpanTuningParam * (Random.value - 0.5f), electronZPosition), Quaternion.identity);
            electronsInBunch.Add(electron);
        }
    }

    //Returns gamma
    public float GetCurrentGamma()
    {
        return currentGamma;
    }

    //Sets gamma
    public void SetGamma(float value)
    {
        currentGamma = value;
    }

}





