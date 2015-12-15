using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BumperController : MonoBehaviour {

    private Light bumperLight;
    private List<Color> colorList;


	// Use this for initialization
	void Start () {
        bumperLight = this.gameObject.GetComponentInChildren<Light>();
        colorList.Add(Color.red);
        colorList.Add(Color.white);
        colorList.Add(Color.blue);
        colorList.Add(Color.green);
        colorList.Add(Color.yellow);
	}
	
	// Update is called once per frame
	void Update () {
        if (bumperLight.intensity > 0)
            bumperLight.intensity -= 0.5F;
	}

    void OnCollisionEnter(Collision col)
    {
        bumperLight.intensity = 8.0F;
        bumperLight.color = colorList[Random.Range(0, colorList.Count)];
    }
}
