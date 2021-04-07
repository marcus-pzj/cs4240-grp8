using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Range(1.0f, 4.0f)]
    public float RefractiveIndex = 1.5f;

    public bool isMirror = false;

    public bool isOpaque = false;

    // Start is called before the first frame update
    void Start()
    {
    }
 
	public void changeRefractiveIndex(float rindex)
	{
		RefractiveIndex = (float)System.Math.Round(rindex, 2);
		Debug.Log(RefractiveIndex);
	}
}
