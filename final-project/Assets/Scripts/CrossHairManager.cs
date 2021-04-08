using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairManager : MonoBehaviour
{
    // GAME CAMERA
    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private LayerMask glassLayer;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        GameObject slider;
        if (
            Physics
                .Raycast(playerCamera
                    .ScreenPointToRay(new Vector3(Screen.width / 2,
                        Screen.height / 2,
                        0)),
                out hit,
                100,
                glassLayer)
        )
        {
            Transform transformHit = hit.transform;
            string tag = transformHit.tag;
            string name = transformHit.name;
            Debug.Log("HIT HIT HIT " + tag + " " + name);
            slider = transformHit.parent.Find("Glass Block Slider").gameObject;
            if (slider)
            {
                slider.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject
                obj
                in
                GameObject.FindGameObjectsWithTag("Glass Block Slider")
            )
            {
                obj.SetActive(false);
            }
        }
    }
}
