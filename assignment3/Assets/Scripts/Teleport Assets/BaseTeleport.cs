using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class BaseTeleport : XRBaseInteractable
{
    public enum TeleportTrigger
    {
        OnSelectExited,
        OnSelectEntered,
        OnActivate,
        OnDeactivate,
        OnSelectExit = OnSelectExited,
        OnSelectEnter = OnSelectEntered,
    }

    // Teleporter that this interactable will communicate teleport requests to
    // If no teleporter is configured, will attempt to find one on Awake
    Teleporter m_Teleporter;
    public Teleporter teleporter
    {
        get => m_Teleporter;
        set => m_Teleporter = value;
    }

    // [SerializeField]
    // Orient the rig after teleportation:
    // World Space Up - stay oriented according to the world space up vector
    // Target Up - orient according to the target BaseTeleportationInteractable Transform's up vector." +
    // Target Up And Forward - orient according to the target BaseTeleportationInteractable Transform's rotation." +
    // None -  maintain the same orientation before and after teleporting.")
    MatchOrientation m_MatchOrientation = MatchOrientation.WorldSpaceUp;
    public MatchOrientation matchOrientation
    {
        get => m_MatchOrientation;
        set => m_MatchOrientation = value;
    }

    [SerializeField]
    // Specify when the teleportation will be triggered
    TeleportTrigger m_TeleportTrigger = TeleportTrigger.OnSelectExited;

    // Specifies when teleportation will trigger.
    public TeleportTrigger teleportTrigger
    {
        get => m_TeleportTrigger;
        set => m_TeleportTrigger = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if (m_Teleporter == null)
        {
            m_Teleporter = FindObjectOfType<Teleporter>();
        }
    }

    protected virtual bool GenerateTeleportRequest(XRBaseInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
    {
        return false;
    }

    void SendTeleportRequest(XRBaseInteractor interactor)
    {
        if (!interactor || m_Teleporter == null)
            return;

        var rayInt = interactor as XRRayInteractor;
        if (rayInt != null) {
            if (rayInt.GetCurrentRaycastHit(out var raycastHit)) {
                var found = false;
                foreach (var interactionCollider in colliders) {
                    if (interactionCollider == raycastHit.collider) {
                        found = true;
                        break;
                    }
                }
                if (found) {
                    var tr = new TeleportRequest {
                        matchOrientation = m_MatchOrientation,
                        requestTime = Time.time,
                    };
                    if (GenerateTeleportRequest(interactor, raycastHit, ref tr)) {
                        m_Teleporter.QueueTeleportRequest(tr);
                    }
                }
            }
        }
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        if (m_TeleportTrigger == TeleportTrigger.OnSelectEntered)
            SendTeleportRequest(interactor);

        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        if (m_TeleportTrigger == TeleportTrigger.OnSelectExited)
            SendTeleportRequest(interactor);

        base.OnSelectExited(interactor);
    }

    protected override void OnActivate(XRBaseInteractor interactor)
    {
        if (m_TeleportTrigger == TeleportTrigger.OnActivate)
            SendTeleportRequest(interactor);

        base.OnActivate(interactor);
    }

    protected override void OnDeactivate(XRBaseInteractor interactor)
    {
        if (m_TeleportTrigger == TeleportTrigger.OnDeactivate)
            SendTeleportRequest(interactor);

        base.OnDeactivate(interactor);
    }
}
