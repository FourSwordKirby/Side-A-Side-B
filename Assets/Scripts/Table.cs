using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {

    private bool onSideA;

    private bool flipping;

    private Vector3 sideAPosition = Vector3.zero;
    private Vector3 sideBPosition = new Vector3(0, -12.4f, 0);


	// Use this for initialization
	void Start () {
        onSideA = true;
        flipping = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flipTable();
        }
	}

    void FixedUpdate()
    {
        if (onSideA)
        {
            if (this.transform.position != sideAPosition)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, sideAPosition, 0.1f);
            }
            if (this.transform.rotation.eulerAngles.y != 0.0f || this.transform.rotation.eulerAngles.z != 0.0f)
            {
                this.transform.Rotate(new Vector3(0, -1f, 1f));

                if (this.transform.rotation.eulerAngles.z > 0 && this.transform.rotation.eulerAngles.z < 180.0f)
                {
                    float yOriginal = this.transform.rotation.eulerAngles.y;
                    float zOriginal = this.transform.rotation.eulerAngles.z;

                    this.transform.Rotate(0, -this.transform.rotation.eulerAngles.y, -this.transform.rotation.eulerAngles.z);
                    this.transform.Rotate(0, yOriginal, 0);
                }
                if (this.transform.rotation.eulerAngles.y < 0)
                {
                    float yOriginal = this.transform.rotation.eulerAngles.y;
                    float zOriginal = this.transform.rotation.eulerAngles.z;

                    this.transform.Rotate(0, -this.transform.rotation.eulerAngles.y, -this.transform.rotation.eulerAngles.z);
                    this.transform.Rotate(0, 0, zOriginal);
                }
            }
            if (this.transform.position == sideAPosition && this.transform.rotation.eulerAngles.z == 0.0f && this.flipping == true)
            {
                foreach (FlipperController flipper in GameObject.FindObjectsOfType<FlipperController>())
                {
                    flipper.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    flipper.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    flipper.enabled = true;
                    flipper.Reinitialize();
                }
                this.flipping = false;
            }
        }
        else
        {
            if (this.transform.position != sideBPosition)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, sideBPosition, 0.1f);
            }
            if (this.transform.rotation.eulerAngles.y != 36.0f || this.transform.rotation.eulerAngles.z != 180.0f)
            {
                this.transform.Rotate(new Vector3(0, 1f, 1f));

                if (this.transform.rotation.eulerAngles.z > 180.0f)
                {
                    float yOriginal = this.transform.rotation.eulerAngles.y;
                    float zOriginal = this.transform.rotation.eulerAngles.z;

                    this.transform.Rotate(0, -this.transform.rotation.eulerAngles.y, -this.transform.rotation.eulerAngles.z);
                    this.transform.Rotate(0, yOriginal, 180);
                }
                if (this.transform.rotation.eulerAngles.y > 36)
                {
                    float yOriginal = this.transform.rotation.eulerAngles.y;
                    float zOriginal = this.transform.rotation.eulerAngles.z;

                    this.transform.Rotate(0, -this.transform.rotation.eulerAngles.y, -this.transform.rotation.eulerAngles.z);
                    this.transform.Rotate(0, 36, zOriginal);
                }
            }
            if (this.transform.position == sideBPosition && this.transform.rotation.eulerAngles.z == 180.0f && this.transform.rotation.eulerAngles.y == 36.0f 
                && this.flipping == true)
            {
                foreach (FlipperController flipper in GameObject.FindObjectsOfType<FlipperController>())
                {
                    flipper.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    flipper.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    flipper.enabled = true;
                    flipper.Reinitialize();
                }
                this.flipping = false;
            }
        }
    }

    public bool IsOnSideA() {
        return onSideA;
    }

	public bool IsFlipping() {
		return flipping;
	}

    void flipTable()
    {
        if (!flipping)
        {
            foreach (FlipperController flipper in GameObject.FindObjectsOfType<FlipperController>())
            {
                flipper.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                flipper.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                flipper.enabled = false;
            }
            onSideA = !onSideA;
            flipping = true;
        }
    }
}
