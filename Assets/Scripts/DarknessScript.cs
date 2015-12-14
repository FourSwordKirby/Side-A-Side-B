using UnityEngine;
using System.Collections;

public class DarknessScript : MonoBehaviour {

    public float transparency = 1.0f;
    private bool isDark;

	// Use this for initialization
	void Start () {
        isDark = false;
        transparency = 0.0f;
        Color oldColor = GetComponent<Renderer>().material.color;
        Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, transparency);
        GetComponent<Renderer>().material.SetColor("_Color", newColor);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDark)
        {
            if (transparency > 0.0f)
            {
                transparency -= 0.02f;
                Color oldColor = GetComponent<Renderer>().material.color;
                Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, transparency);
                GetComponent<Renderer>().material.SetColor("_Color", newColor);
            }
        }
        else{
            if (transparency < 1.0f)
            {
                transparency += 0.02f;
                Color oldColor = GetComponent<Renderer>().material.color;
                Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, transparency);
                GetComponent<Renderer>().material.SetColor("_Color", newColor);
            }
        }
	}

    public void turnDark()
    {
        isDark = true;
    }

    public void turnBright()
    {
        isDark = false;
    }
}
