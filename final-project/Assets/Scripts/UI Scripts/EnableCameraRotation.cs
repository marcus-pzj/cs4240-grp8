using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCameraRotation : MonoBehaviour
{
    void Start()
    {
		Screen.orientation = ScreenOrientation.AutoRotation;
	}
}
