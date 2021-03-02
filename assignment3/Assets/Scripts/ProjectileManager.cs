using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public float delay;
    private void Update()
    {
        if (this.gameObject.layer == 8)
        {
            Invoke("DespawnProjectile", delay);
        }
    }

    private void DespawnProjectile()
    {
        Destroy(this.gameObject);
    }
}
