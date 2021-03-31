using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private GameObject detector;
    private GameObject[] glassBlocks;

    public float detectorDistance = 5;
    // Start is called before the first frame update
    void Start()
    {
        detector = GameObject.FindGameObjectWithTag("Detector");
        glassBlocks = GameObject.FindGameObjectsWithTag("Glass");

        Renderer cubeRenderer = detector.GetComponent<Renderer>();
        cubeRenderer.material.SetColor(
            "_Color", new Color(Color.white.r, Color.white.g, Color.white.b, 0.3f)
        );

    }


    void FixedUpdate()
    {
        Renderer cubeRenderer = detector.GetComponent<Renderer>();

        if (detector)
        {
            for (int i = 0; i < glassBlocks.Length; i++)
            {
                Transform glassBlockTransform = glassBlocks[i].transform;
                if (Vector3.Distance(glassBlockTransform.position, detector.transform.position) < detectorDistance) {
                    cubeRenderer.material.SetColor(
                        "_Color", new Color(Color.green.r, Color.green.g, Color.green.b, 0.3f)
                    );
                    break;
                } else
                {
                    cubeRenderer.material.SetColor(
                        "_Color", new Color(Color.white.r, Color.white.g, Color.white.b, 0.3f)
                    );
                }
            }
        }
    }
}
