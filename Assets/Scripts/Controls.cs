using UnityEngine;
using System.Collections;

public class Controls {

    public static float getFlipperMagnitude()
    {
        float flipperMagnitude = Input.GetAxis("Vertical");
        return flipperMagnitude;
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
}
