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
            if (this.transform.position != Vector3.zero)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, sideAPosition, 0.1f);
            }
            else
            {
                this.flipping = false;
            }

            if (this.transform.rotation.eulerAngles.z != 0.0f)
            {
                this.transform.Rotate(new Vector3(0, 0, 1f));
                if (this.transform.rotation.eulerAngles.z > 0 && this.transform.rotation.eulerAngles.z < 180.0f)
                {
                    this.transform.Rotate(0, 0, - this.transform.rotation.eulerAngles.z);
                }
            }
        }
        else
        {
            if (this.transform.position != sideBPosition)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, sideBPosition, 0.1f);
            }
            else
            {
                this.flipping = false;
            }
            if (this.transform.rotation.eulerAngles.z != 180.0f)
            {
                Debug.Log(this.transform.rotation.eulerAngles.z);
                this.transform.Rotate(new Vector3(0, 0, 1f));
                if (this.transform.rotation.eulerAngles.z > 180)
                {
                    this.transform.Rotate(0, 0, - this.transform.rotation.eulerAngles.z);
                    this.transform.Rotate(0, 0, this.transform.rotation.eulerAngles.z + 180);

                }
            }
        }
    }

    public bool IsOnSideA() {
        return onSideA;
    }

    void flipTable()
    {
        if (!flipping)
        {
            onSideA = !onSideA;
            flipping = true;
        }
    }
}
