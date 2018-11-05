using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Just in case

public class ElectronBehavior : MonoBehaviour {

    //Parameters for tuning beam
    public float quadForceTuningParam = 1;

    private float electronWiggleParameter = 0;

    // Listen to events
    void OnEnable () {
        EventManager.StartListening("Xfocus",DoQuadrupoleFocusingX);
        EventManager.StartListening("Yfocus",DoQuadrupoleFocusingY);
        
    }

    void OnDisable () {
        EventManager.StopListening("Xfocus",DoQuadrupoleFocusingX);
        EventManager.StopListening("Yfocus",DoQuadrupoleFocusingY);
    }
    
	// Use this for initialization
	void Start () {
		
       

	}
	
	// Update is called once per frame
	void Update () {

        WiggleElectronInZ();


	}

    //Move the electrons somehow in z
    private void WiggleElectronInZ()
    {
        float wigglefrequency = Random.value;
        electronWiggleParameter += 0.1f*wigglefrequency;
        float electronPositionZ = 1f*Mathf.Sin(electronWiggleParameter);
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
        Debug.Log("XForce!");
        DoQuadrupoleFocusingGeneral(-1);
    }

    //Specific focusing in Y
    public void DoQuadrupoleFocusingY()
    {   
        Debug.Log("YForce!");
        DoQuadrupoleFocusingGeneral(1);
    }




}
