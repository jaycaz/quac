using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class valueDisplay : MonoBehaviour {
	//These values mimic a particular component of a script, but not take the actual values
	public float value=1.0f;
	public float delta=0.05f;
	public float minimum=0f;
	public float maximum=10f;

	// The display text
	private Text displayText;
	// Use this for initialization
	void Start () {
		displayText= GetComponent<Text>();
		displayText.text=value.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		displayText.text=value.ToString();
	}
	public void add(){
		value=Mathf.Clamp(value+delta, minimum, maximum);
	}

		public void subtract(){
		value=Mathf.Clamp(value-delta, minimum, maximum);
	}
}
