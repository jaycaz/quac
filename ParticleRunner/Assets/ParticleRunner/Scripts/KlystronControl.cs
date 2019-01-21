using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class is the class that deals with klystron behavior and control
public class KlystronControl : MonoBehaviour {

    //Tunability
    public float klystronAdditionalSpeed = 0.2f;

    //WorldMover info
    private GameManager gm;
    private float gamma;
    

	// Use this for initialization
    void Start () {
        gm = FindObjectOfType<GameManager>();


    }
	

    // Listen to events
    void OnEnable () {
        EventManager.StartListening("FireKlystron",FireKlystron);        
    }

    void OnDisable () {
        EventManager.StopListening("FireKlystron",FireKlystron);
    }



	// Update is called once per frame
	void Update () {

        //Get the worldspeed
        gamma = gm.GetCurrentGamma();

        //Get the slider
        Slider slider = GetComponentInChildren<Slider>();

        //Set the slider value
        slider.value = Mathf.Sin(2*gamma * Time.time);



	}

    //This function fires the klystron to accelerate the particle
    //when the fire klystron button is clicked
    public void FireKlystron(){



        //First, get the slider value. We'll use this to weight
        //the velocity change.
        Slider slider = GetComponentInChildren<Slider>();
        float val = slider.value;

        //Clip to make sure that no one can break the game and go backward
        if (gamma + val * klystronAdditionalSpeed >= 0.5f)
        {
            gm.SetGamma(gamma + val * klystronAdditionalSpeed);
        }



    }


}
