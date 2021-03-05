using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterLaser : MonoBehaviour
{
    public bool hideDebugLaser;
    private GameObject teleporterPad;

    private void Start()
    {
        if (hideDebugLaser)
        {
            Renderer renderer = this.gameObject.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", Color.clear);
        }
    }

    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) && teleporterPad != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = new Vector3(
                teleporterPad.transform.position.x,
                teleporterPad.transform.position.y + 0.1f,
                teleporterPad.transform.position.z
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Teleporter"))
        {
            teleporterPad = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        teleporterPad = null;
    }
}
