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
                //projectileRigibody.useGravity = false;
                //projectileRigibody.velocity = Vector3.zero;
                //projectileRigibody.angularVelocity = Vector3.zero;
                projectileObject.layer = 7; // Set layer to inactive projectile
                projectileObject.transform.parent = this.gameObject.transform;
                //projectileObject.transform.position = offsetPosition;
                Destroy(projectileRigibody);
                projectileObject.transform.position += offsetPosition;
            }

            float forceRate = (m_Force / distance);

            body.AddForce(direction * (forceRate / body.mass) * signal);
        }
    }
}
