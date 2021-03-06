using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterLaser : MonoBehaviour
{
    private GameObject teleporterPad;

    public AudioClip teleportSound;

    private void Update()
    {
        if (
            OVRInput.Get(OVRInput.RawButton.LIndexTrigger) &&
            teleporterPad != null
        )
        {
            Teleport();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Teleporter"))
        {
            teleporterPad = other.gameObject;

            Renderer teleporterRenderer =
                other.gameObject.GetComponent<Renderer>();
            teleporterRenderer.material.SetColor("_Color", Color.red);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (teleporterPad.gameObject.CompareTag("Teleporter"))
        {
            Renderer teleporterRenderer =
                teleporterPad.gameObject.GetComponent<Renderer>();
            teleporterRenderer.material.SetColor("_Color", Color.white);
        }
        teleporterPad = null;
    }

    private void Teleport()
    {
        GameObject player = GameObject.FindWithTag("Player");
        OVRPlayerController OVRC = player.GetComponent<OVRPlayerController>();
        OVRC.enabled = false;
        player.transform.position =
            new Vector3(teleporterPad.transform.position.x,
                teleporterPad.transform.position.y + 1f,
                teleporterPad.transform.position.z);
        OVRC.enabled = true;

        if (teleportSound != null)
        {
            GetComponent<AudioSource>().clip = teleportSound;
            GetComponent<AudioSource>().Play();
        }
    }
}
