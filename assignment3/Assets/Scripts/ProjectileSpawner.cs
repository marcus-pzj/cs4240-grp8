using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float interval;
    public float spawnRadius;
    public float heightOffset;
    public int maxProjectiles;
    public bool isFixedPoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnProjectile", 0.0f, interval);
    }

    private void SpawnProjectile()
    {
        if (isFixedPoint)
        {
            if (this.transform.childCount < 1)
            {
                Instantiate(
                    projectilePrefab,
                    new Vector3(0, heightOffset, 0), Quaternion.identity,
                    transform
                );
            }
        } else {
            if (this.transform.childCount < maxProjectiles)
            {
                Vector2 randPos = Random.insideUnitCircle * spawnRadius;
                Instantiate(
                    projectilePrefab,
                    new Vector3(
                        randPos.x + transform.position.x,
                        heightOffset,
                        randPos.y + transform.position.z
                    ),
                    Quaternion.identity,
                    transform
                );
            }
        }
    }
}
