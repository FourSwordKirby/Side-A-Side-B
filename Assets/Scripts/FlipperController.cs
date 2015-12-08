using UnityEngine;
using System.Collections;

public class FlipperController : MonoBehaviour {

    public float pullBackMagnitude;
    public int player;

	private Quaternion baseState;

	// Pulls back the flipper
	void PullBack() {
		transform.rotation = baseState;
		transform.RotateAround (transform.position, new Vector3 (0, 1, 0), pullBackMagnitude * 50);
	}


	// Use this for initialization
	void Start () {
        pullBackMagnitude = 0;
		baseState = transform.localRotation;
	}

	// Update is called once per frame
	void Update () {
		pullBackMagnitude = Controls.getFlipperMagnitude (player);
		//if (pullBackMagnitude != 0) Debug.Log (pullBackMagnitude);
		PullBack ();

    /*
        //Code that might be used later for accelerometers
        transform.Translate(Input.acceleration.x, 0, -Input.acceleration.z);
     */
    }

    void OnGUI()
    {
        GUILayout.Label("X: " + Input.acceleration.x);
        GUILayout.Label("Y: " + Input.acceleration.y); 	
    }
}
