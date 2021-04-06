using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionStateManager : MonoBehaviour
{
    public GameObject detector;
    public GameObject targetDetector;
    public float detectorDistance = 5;

    private bool isTargetHit = false;
    private bool isMirrorDetected = false;
    private bool isTargetDetected = false;
    private GameObject mirror;
    private GameObject target;
    private GameObject laserManager;


    void Update()
    {
        mirror = GameObject.FindGameObjectWithTag("Mirror");
        target = GameObject.FindGameObjectWithTag("Target");
        laserManager = GameObject.FindGameObjectWithTag("LaserManager");

        // Game states
        isTargetDetected = false;
        isMirrorDetected = false;
        isTargetHit = false;

        if (detector && mirror)
        {
            Material cubeMaterial = detector.GetComponent<MeshRenderer>().material;
            cubeMaterial.SetColor("_Color", Color.white);
            Transform mirrorTransform = mirror.transform;
            if (Vector3.Distance(mirrorTransform.position, detector.transform.position) < detectorDistance)
            {
                cubeMaterial.SetColor(
                    "_Color", Color.green
                );
                isMirrorDetected = true;
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
            cubeMaterial.SetColor("_Color", Color.white);
            if (laserManager.GetComponent<LaserManager>().IsTargetHit())
            {
                cubeMaterial.SetColor("_Color", Color.red);
                isTargetHit = true;
            }
        }
    }

    public bool isObjectiveMet()
    {
        return isTargetDetected && isTargetHit && isMirrorDetected;
    }
}
