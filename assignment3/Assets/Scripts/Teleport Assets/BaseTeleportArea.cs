using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BaseTeleportArea : BaseTeleport
{
    protected override bool GenerateTeleportRequest(XRBaseInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
    {
        teleportRequest.destinationPosition = raycastHit.point;
        teleportRequest.destinationRotation = transform.rotation;
        return true;
    }
}
