using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIRStateManager : MonoBehaviour
{
    public GameObject detector;
    public GameObject targetDetector;
    public float detectorDistance = 5;

    private bool isTargetHit = false;
    private bool isGlassDetected = false;
    private bool isTargetDetected = false;
    private GameObject[] glassBlocks;
    private GameObject target;
    private GameObject laserManager;


    void Update()
    {
        glassBlocks = GameObject.FindGameObjectsWithTag("Glass");
        target = GameObject.FindGameObjectWithTag("Target");
        laserManager = GameObject.FindGameObjectWithTag("LaserManager");

        // Game states
        isTargetDetected = false;
        isGlassDetected = false;
        isTargetHit = false;

        if (detector)
        {
            Material cubeMaterial = detector.GetComponent<MeshRenderer>().material;
            cubeMaterial.SetColor("_Color", Color.white);

            for (int i = 0; i < glassBlocks.Length; i++)
            {
                Transform glassBlockTransform = glassBlocks[i].transform;
                if (Vector3.Distance(glassBlockTransform.position, detector.transform.position) < detectorDistance)
                {
                    cubeMaterial.SetColor(
                        "_Color", Color.green
                    );
                    isGlassDetected = true;
                    break;
                }
            }
        }

        if (targetDetector)
        {
            Material cubeMaterial = targetDetector.GetComponent<MeshRenderer>().material;
            cubeMaterial.SetColor("_Color", Color.white);

            Transform targetTransform = target.transform;
            if (Vector3.Distance(targetTransform.position, targetDetector.transform.position) < detectorDistance)
            {
                cubeMaterial.SetColor(
                    "_Color", Color.green
                );
                isTargetDetected = true;
            }
        }

        if (target && laserManager)
        {
            Material cubeMaterial = target.GetComponent<MeshRenderer>().material;
            cubeMaterial.SetColor("_Color", Color.red);
            if (laserManager.GetComponent<LaserManager>().IsTargetHit())
            {
                cubeMaterial.SetColor("_Color", Color.green);
                isTargetHit = true;
            }
        }
    }

	public bool AreObjectsDetected()
	{
		return isTargetDetected && isGlassDetected;
	}

	public bool isObjectiveMet()
    {
        return isTargetDetected && isTargetHit && isGlassDetected;
    }
}
