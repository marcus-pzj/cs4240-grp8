using UnityEngine;

public class AttractionForce : MonoBehaviour
{
    // TODO: Credit or refactor the script
    // https://github.com/kleberandrade/attraction-repulsion-force-unity
    public enum ForceType { Repulsion = -1, None = 0, Attraction = 1 }
    public ForceType m_Type;
    public Transform m_Pivot;
    public float m_Radius;
    public float m_StopRadius;
    public float m_Force;
    public LayerMask m_Layers;

    public GameObject projectileObject;
    public GameObject laser;
    private Vector3 offsetPosition = new Vector3(0.0f, 0.0f, 1.0f);


    private void FixedUpdate()
    {
        OVRInput.FixedUpdate();
        Collider[] colliders = Physics.OverlapSphere(m_Pivot.position, m_Radius, m_Layers);

        float signal = (float)m_Type;

        foreach (var collider in colliders)
        {
            Rigidbody body = collider.GetComponent<Rigidbody>();
            if (body == null)
                continue;

            Vector3 direction = m_Pivot.position - body.position;

            float distance = direction.magnitude;

            direction = direction.normalized;

            if (distance < m_StopRadius)
            {
                Rigidbody projectileRigibody = projectileObject.GetComponent<Rigidbody>();
                projectileObject.layer = 7; // Set layer to inactive projectile
                Destroy(projectileRigibody);
                projectileObject.transform.parent = this.gameObject.transform;
                projectileObject.transform.localPosition = offsetPosition;
                projectileObject.transform.localEulerAngles = Vector3.zero;
            }

            float forceRate = (m_Force / distance);

            body.AddForce(direction * (forceRate / body.mass) * signal);
        }
    }

    private void Update()
    {
        OVRInput.Update();
        if (OVRInput.GetDown(OVRInput.Button.One) && projectileObject != null)
        {
            FireProjectile();
            projectileObject = null;
            laser.SetActive(true);
            // FIXME: Need to make sure it fires properly
        }

        if (projectileObject == null)
        {
            // Set the laser to active here
        }
    }

    public void FireProjectile()
    {
        projectileObject.transform.parent = null;
        projectileObject.AddComponent<Rigidbody>();
        projectileObject.GetComponent<Rigidbody>().AddForce(0, 0, 100);
    }
}
