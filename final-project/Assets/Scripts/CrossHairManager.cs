using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairManager : MonoBehaviour
{
    // GAME CAMERA
    [SerializeField]
    private Camera playerCamera;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (
            Physics
                .Raycast(playerCamera
                    .ScreenPointToRay(new Vector3(Screen.width / 2,
                        Screen.height / 2,
                        0)),
                out hit,
                100)
        )
        {
            Transform transformHit = hit.transform;
            string tag = transformHit.tag;

            if (tag == "Glass")
            {
                Debug.Log("hit hit hit");
            }
        }
    }
}
