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
        //Being revised to do stuff with HingeJointss
        //Guide is here: http://forum.unity3d.com/threads/pinball-flippers-swatting-objects-using-physics.220281/

        if (Mathf.Abs(pullBackMagnitude) <= Mathf.Abs(Controls.getFlipperMagnitude(player)))
        {
            //Initially pulls back /forward the flipper
            pullBackMagnitude = Controls.getFlipperMagnitude(player);
            PullBack();
        }
        else
        {
            pullBackMagnitude = 0;
            Debug.Log("here");
            this.GetComponent<Rigidbody>().angularVelocity += new Vector3(0, pullBackMagnitude, 0);
        }

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
