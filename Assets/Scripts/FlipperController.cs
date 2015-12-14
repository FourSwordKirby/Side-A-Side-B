using UnityEngine;
using System.Collections;

public class FlipperController : MonoBehaviour {

    public float pullBackMagnitude;
    public int player;

    public bool controllable;

	private Quaternion baseRotation;
    private float wobbleCharge;
    private bool isWobbling;
    private bool resetting;
    private bool returnedToNeutral;

    private const float FLIPPER_FORCE = 80.0f;
    private const float MAX_CHARGE_TIME = 1.0f;

	// Pulls back the flipper
	void PullBack() {
        //Quaternion curr = transform.rotation;
		transform.rotation = baseRotation;
		transform.RotateAround (transform.position, new Vector3 (0, 1, 0), pullBackMagnitude * FLIPPER_FORCE);
        //Quaternion newrot = transform.rotation;
        //transform.rotation = Quaternion.Lerp(curr, newrot, Time.deltaTime);
	}


	// Use this for initialization
	void Start () {
        pullBackMagnitude = 0;
		baseRotation = transform.rotation;
        this.GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
        this.GetComponent<Rigidbody>().maxAngularVelocity = 30.0f;

        returnedToNeutral = false;
        isWobbling = false;
        resetting = false;
        enabled = controllable;
	}

    public void Reinitialize()
    {
        pullBackMagnitude = 0;
        baseRotation = transform.rotation;
        this.GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
        this.GetComponent<Rigidbody>().maxAngularVelocity = 30.0f;

        returnedToNeutral = false;
        isWobbling = false;
        controllable = !controllable;
        enabled = controllable;
    }

    public void resetPosition()
    {
        returnedToNeutral = true;
        resetting = true;

        this.GetComponent<Rigidbody>().AddTorque(0, -pullBackMagnitude * FLIPPER_FORCE, 0, ForceMode.Impulse);
        isWobbling = true;
    }

	//FixedUpdate is called once per frame, handle's physics
    void FixedUpdate()
    {
        //Reset code (lol giant decision tree)
        if (resetting)
        {
            if ((transform.rotation.eulerAngles.y < baseRotation.eulerAngles.y && pullBackMagnitude > 0)
                || (transform.rotation.eulerAngles.y > baseRotation.eulerAngles.y) && pullBackMagnitude < 0)
            {
                this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                pullBackMagnitude *= -0.75f;
                this.GetComponent<Rigidbody>().AddTorque(0, -pullBackMagnitude * FLIPPER_FORCE, 0, ForceMode.Impulse);
            }

            if (Mathf.Abs(pullBackMagnitude) > 0.0001)
            {
                return;
            }
            else
            {
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                this.GetComponent<Rigidbody>().isKinematic = true;
                resetting = false;
                enabled = false;
                Debug.Log("still here");
                return;
            }
        }

        //Handling the wobbling
        if (isWobbling)
        {
            /*Code for wobble physics*/
            Debug.Log("wobbling");
            //If the stick returned to neutral, we are later allowed to exit the wobble loop)
            if (returnedToNeutral && Controls.getFlipperMagnitude(player) != 0.0f)
            {
                returnedToNeutral = false;
                isWobbling = false;
                pullBackMagnitude = 0;
                transform.rotation = baseRotation;
                this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                return;
            }
            else if (Controls.getFlipperMagnitude(player) == 0.0f)
            {
                returnedToNeutral = true;
            }

            if ((transform.rotation.eulerAngles.y < baseRotation.eulerAngles.y && pullBackMagnitude > 0)
                || (transform.rotation.eulerAngles.y > baseRotation.eulerAngles.y && pullBackMagnitude < 0))
            {
                this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                pullBackMagnitude *= -0.75f;
                this.GetComponent<Rigidbody>().AddTorque(0, -pullBackMagnitude * FLIPPER_FORCE, 0, ForceMode.Impulse);
            }

            wobbleCharge = Mathf.Max(wobbleCharge - Time.deltaTime, 0);
            //reset the flipper when wobbling is done
            if (wobbleCharge == 0.0f)
            {
                isWobbling = false;
                pullBackMagnitude = 0;
                transform.rotation = baseRotation;
                this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
            return;
        }

        //Initial force from releasing the flipper
        if ((Mathf.Abs(pullBackMagnitude) - Mathf.Abs(Controls.getFlipperMagnitude(player)) > 0.4)
            && wobbleCharge > 0)
        {
            this.GetComponent<Rigidbody>().AddTorque(0, -pullBackMagnitude * FLIPPER_FORCE, 0, ForceMode.Impulse);
            isWobbling = true;
            return;
        }

        //Code that happens when you aren't wobbling
        //Moving the flipper if you're not releasing
        pullBackMagnitude = Controls.getFlipperMagnitude(player);
        PullBack();

        //Charging up wobble bar.
        if (Mathf.Abs(pullBackMagnitude) > 0)
        {
            wobbleCharge = Mathf.Min(wobbleCharge + Time.deltaTime, MAX_CHARGE_TIME);
        }
        else
        {
            wobbleCharge = Mathf.Max(wobbleCharge - Time.deltaTime, 0);
        }
    }

    void OnGUI()
    {
        GUILayout.Label("X: " + Input.acceleration.x);
        GUILayout.Label("Y: " + Input.acceleration.y); 	
    }
}
