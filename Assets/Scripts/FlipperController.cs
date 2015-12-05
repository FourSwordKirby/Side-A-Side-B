using UnityEngine;
using System.Collections;

public class FlipperController : MonoBehaviour {

    public float pullBackMagnitude;

	// Use this for initialization
	void Start () {
        pullBackMagnitude = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //Just moves the flipper up and down lol
        this.transform.position += new Vector3(0, Controls.getFlipperMagnitude(), 0);
        //Code that might be used later for accelerometers
        //transform.Translate(Input.acceleration.x, 0, -Input.acceleration.z);
	}
}
