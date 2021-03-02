using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// Teleportable area that teleports user to a pre-determined
/// specific position
public class BaseTeleportAnchor : BaseTeleport
{
    [SerializeField]
    Transform m_TeleportAnchorTransform;
    public Transform teleportAnchorTransform
    {
        get => m_TeleportAnchorTransform;
        set => m_TeleportAnchorTransform = value;
    }

    protected void OnValidate()
    {
        if (m_TeleportAnchorTransform == null)
            m_TeleportAnchorTransform = transform;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        GizmoHelpers.DrawWireCubeOriented(m_TeleportAnchorTransform.position, m_TeleportAnchorTransform.rotation, 1f);
        GizmoHelpers.DrawAxisArrows(m_TeleportAnchorTransform, 1f);
    }

    protected override bool GenerateTeleportRequest(XRBaseInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
    {
        if (m_TeleportAnchorTransform == null)
            return false;

        teleportRequest.destinationPosition = m_TeleportAnchorTransform.position;
        teleportRequest.destinationRotation = m_TeleportAnchorTransform.rotation;
        return true;
    }
}

