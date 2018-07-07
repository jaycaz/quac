using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunchManager : MonoBehaviour {




    public int initialBunchSize = 3;
    private GameObject[] electrons;
    public float addedForce = 10;
    public GameObject prefabElectron;
    private GameObject electron;



	// Use this for initialization
	void Start () {


        //Create a bunch of electrons
        electrons = new GameObject[initialBunchSize];
        for (int iEl = 0; iEl < initialBunchSize; ++iEl ){
            electron = Instantiate(prefabElectron, new Vector3(0f,0f,0f), Quaternion.identity );
            electrons[iEl] = prefabElectron;
        }
            



	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log("Fuck");
             

	}

    public void tuneBeamAll( float phi )
    {
        
        float x = addedForce * Mathf.Cos(phi * Mathf.PI / 180f);
        float y = addedForce * Mathf.Sin(phi * Mathf.PI / 180f);


        //Loop over all objects with the electron tag. Only bump them if they are near the edge
        electrons = GameObject.FindGameObjectsWithTag("electron");
        int numElectrons = electrons.Length;
        for (int iE = 0; iE < numElectrons; ++iE ){
            Transform tf = electrons[iE].gameObject.transform;
            float xi = tf.position.x;
            float yi = tf.position.y;
            float ri = Mathf.Pow(xi * xi + yi * yi, 0.5f);
            float phii = 180f/Mathf.PI*Mathf.Atan2(yi, xi);
            if (Mathf.Abs(phii - phi + 180) < 45f && ri > 2f)
            {
                Rigidbody rb = electrons[iE].gameObject.GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(x, y, 0f));
            }
        }
    }


}
