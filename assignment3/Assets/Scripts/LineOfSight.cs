using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public bool hideDebugLaser;

    private void Start()
    {
        if (hideDebugLaser)
        {
            Renderer renderer = this.gameObject.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", Color.clear);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile") && other.gameObject.layer == 7)
        {
            other.gameObject.layer = 6;

            AttractionForce parent = this.transform.parent.GetComponent<AttractionForce>();
            parent.projectileObject = other.gameObject;
            this.gameObject.SetActive(false);
        }
    }
}
