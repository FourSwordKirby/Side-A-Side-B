using UnityEngine;
using System.Collections;

public class Controls {

    public static float getFlipperMagnitude(int player)
    {
        return Input.GetAxis("P"+(player+1)+"_Vertical");
    }

    public static bool interactInputDown()
    {
        return (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Interact"));
    }

    public static bool lockonInputDown()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public static bool attackInputDown()
    {
        return Input.GetMouseButtonDown(1);
    }

    void Awake() {
    }
}
