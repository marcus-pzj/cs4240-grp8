using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            other.gameObject.layer = 6;

            AttractionForce parent = this.transform.parent.GetComponent<AttractionForce>();
            parent.projectileObject = other.gameObject;
            this.gameObject.SetActive(false);
        }
    }
}
