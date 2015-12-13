﻿using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {

    private bool onSideA;

    private bool flipping;

    private Vector3 sideAPosition;
    private Vector3 sideBPosition;

    private Quaternion sideARotation;
    private Quaternion sideBRotation;

	// Use this for initialization
	void Start () {
        onSideA = true;
        flipping = false;

        sideAPosition = Vector3.zero;
        sideBPosition = new Vector3(0, -12.4f, 0);

        sideARotation = this.transform.rotation;
        this.transform.Rotate(0, 180, 180);
        sideBRotation = this.transform.rotation;
        this.transform.Rotate(0, -180, -180);
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
            if (this.transform.rotation != sideARotation)
            {
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, sideARotation, 1.0f);
            }
            if (this.transform.position == sideAPosition && this.transform.rotation == sideARotation && this.flipping == true)
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
            if (this.transform.rotation != sideBRotation)
            {
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, sideBRotation, 1.0f);
            }
            if (this.transform.position == sideBPosition && this.transform.rotation == sideBRotation && this.flipping == true)
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
