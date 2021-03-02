using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class LocomotionManager : MonoBehaviour
{
    public event Action<Locomotion> startLocomotion;
    public event Action<Locomotion> endLocomotion;

    public Locomotion m_Locomotion;
    public Locomotion locomotion
    {
        get => m_Locomotion;
        set => m_Locomotion = value;
    }

    protected void Awake()
    {
        if (m_Locomotion == null)
            m_Locomotion = FindObjectOfType<Locomotion>();
    }

    protected bool CanBeginLocomotion()
    {
        if (m_Locomotion == null)
            return false;

        return !m_Locomotion.busy;
    }

    protected bool BeginLocomotion()
    {
        if (m_Locomotion == null)
            return false;

        var success = m_Locomotion.RequestExclusiveOperation(this) == RequestResult.Success;
        if (success)
            startLocomotion?.Invoke(m_Locomotion);

        return success;
    }

    protected bool EndLocomotion()
    {
        if (m_Locomotion == null)
            return false;

        var success = m_Locomotion.FinishExclusiveOperation(this) == RequestResult.Success;
        if (success)
            endLocomotion?.Invoke(m_Locomotion);

        return success;
    }
}
