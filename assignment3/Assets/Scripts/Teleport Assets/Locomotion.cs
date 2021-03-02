using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public enum RequestResult
{
    Success,
    Busy,
    Error,
}

// Locomotion is used to control access to the XR Rig. Only one Locomotion can move the XR Rig at a time
// Having multiple instances of Locomotion drive an XR Rig is not recommended.
public class Locomotion : MonoBehaviour
{
    LocomotionManager m_CurrentExclusiveManager;
    float m_TimeMadeExclusive;

    
    // The timeout (in seconds) for exclusive access to the XR Rig
    [SerializeField]
    float m_Timeout = 10f;
    public float timeout
    {
        get => m_Timeout;
        set => m_Timeout = value;
    }

    [SerializeField]
    // The XR Rig that Locomotion is controlling
    XRRig m_XRRig;
    public XRRig xrRig
    {
        get => m_XRRig;
        set => m_XRRig = value;
    }

    // if the XR object is blocked
    public bool busy => m_CurrentExclusiveManager != null;
    public bool Busy => busy;

    protected void Awake()
    {
        if (m_XRRig == null)
            m_XRRig = FindObjectOfType<XRRig>();
    }

    protected void Update()
    {
        if (m_CurrentExclusiveManager != null && Time.time > m_TimeMadeExclusive + m_Timeout)
        {
            ResetExclusivity();
        }
    }

    // Block the XR rig for the LocomotionManager
    public RequestResult RequestExclusiveOperation(LocomotionManager manager)
    {
        if (manager == null)
            return RequestResult.Error;

        if (m_CurrentExclusiveManager == null) {
            m_CurrentExclusiveManager = manager;
            m_TimeMadeExclusive = Time.time;
            return RequestResult.Success;
        }
        return m_CurrentExclusiveManager != manager ? RequestResult.Busy : RequestResult.Error;
    }

    internal void ResetExclusivity()
    {
        m_CurrentExclusiveManager = null;
        m_TimeMadeExclusive = 0f;
    }

    // Relinquish control for the LocomotionManager
    public RequestResult FinishExclusiveOperation(LocomotionManager manager)
    {
        if(manager == null || m_CurrentExclusiveManager == null)
            return RequestResult.Error;

        if (m_CurrentExclusiveManager == manager) {
            ResetExclusivity();
            return RequestResult.Success;
        }
        return RequestResult.Error;
    }
}
