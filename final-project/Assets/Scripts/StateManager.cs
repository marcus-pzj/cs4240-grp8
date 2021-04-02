using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private GameObject detector;
    private GameObject target;
    private GameObject[] glassBlocks;

    public float detectorDistance = 5;

    void Update()
    {
        detector = GameObject.FindGameObjectWithTag("Detector");
        glassBlocks = GameObject.FindGameObjectsWithTag("Glass");
        target = GameObject.FindGameObjectWithTag("Target");

        if (detector)
        {
            Material cubeMaterial = detector.GetComponent<MeshRenderer>().material;
            cubeMaterial.SetColor("_Color", Color.white);

            for (int i = 0; i < glassBlocks.Length; i++)
            {
                Transform glassBlockTransform = glassBlocks[i].transform;
                if (Vector3.Distance(glassBlockTransform.position, detector.transform.position) < detectorDistance) {
                    cubeMaterial.SetColor(
                        "_Color", Color.green
                    );
                    break;
                }
            }
        }

    }
}
