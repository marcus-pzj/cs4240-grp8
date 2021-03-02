using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

/// Responsible for moving the XR Rig to the desired location
public class Teleporter : LocomotionManager
{
    protected TeleportRequest currentRequest { get; set; }
    protected bool validRequest { get; set; }
    
    Locomotion m_locomotion;

    public Locomotion locomotion
    {
        get => m_Locomotion;
        set => m_Locomotion = value;
    }

    void Awake()
    {
        if (m_Locomotion == null)
            m_Locomotion = FindObjectOfType<Locomotion>();
    }

    /// Returns "true" if successfully queued. "false" otherwise.
    public virtual bool QueueTeleportRequest(TeleportRequest teleportRequest)
    {
        currentRequest = teleportRequest;
        validRequest = true;
        return true;
    }

    protected virtual void Update()
    {
        if (!validRequest || !BeginLocomotion())
            return;

        var xrRig = locomotion.xrRig;
        if (xrRig != null) {
            Debug.Log("teleporting");
            switch (currentRequest.matchOrientation) {
                case MatchOrientation.WorldSpaceUp:
                    xrRig.MatchRigUp(Vector3.up);
                    break;
                case MatchOrientation.TargetUp:
                    xrRig.MatchRigUp(currentRequest.destinationRotation * Vector3.up);
                    break;
                case MatchOrientation.TargetUpAndForward:
                    xrRig.MatchRigUpCameraForward(currentRequest.destinationRotation * Vector3.up, currentRequest.destinationRotation * Vector3.forward);
                    break;
                case MatchOrientation.None:
                    // Maintain current rig rotation.
                    break;
                default:
                    Assert.IsTrue(false, $"Unhandled {nameof(MatchOrientation)}={currentRequest.matchOrientation}.");
                    break;
            }
            var heightAdjustment = xrRig.rig.transform.up * xrRig.cameraInRigSpaceHeight;
            var cameraDestination = currentRequest.destinationPosition + heightAdjustment;
            xrRig.MoveCameraToWorldLocation(cameraDestination);
        }

        EndLocomotion();
        validRequest = false;
    }
}
