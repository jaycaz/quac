using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronBehavior : MonoBehaviour {

    //Publics
    public float maxR = 5;
    public float addedForce = 1;

    //Variables that will be useful
    private Transform electronTrans;
    private float r = 0.0f;



	// Use this for initialization
	void Start () {
        electronTrans = GetComponent<Transform>();
       

	}
	
	// Update is called once per frame
	void Update () {

        //If this goes out of bounds, destroy it and create a new one at the origin
        float x = electronTrans.position.x;
        float y = electronTrans.position.y;
        r = Mathf.Pow(x * x + y * y, 0.5f);
        if(r > maxR){
            Destroy(gameObject);
        }



        BrownianMotionKick();




	}

    //This gives the ball a little kick in some direction every once in a while
    void BrownianMotionKick()
    {

    
        float x = (0.5f - Random.value) * addedForce / 4;
        float y = (0.5f - Random.value) * addedForce / 4;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(x, y, 0f));

    }

    //Add force in a direction
    public void tuneBeam(float phi)
    {
        float x = addedForce * Mathf.Cos(phi*Mathf.PI/180f);
        float y = addedForce * Mathf.Sin(phi*Mathf.PI/180f);
                                        
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(x,y,0f));






    }



}
