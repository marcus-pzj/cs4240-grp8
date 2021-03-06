using UnityEngine;

public class TargetSpawner: MonoBehaviour
{
    public GameObject targetPrefab;
    public float interval;
    public float spawnRadius;
    public float heightOffset;
    public int maxTargets;
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
                    targetPrefab,
                    new Vector3(0, heightOffset, 0), Quaternion.identity,
                    transform
                );
            }
        }
        else
        {
            if (this.transform.childCount < maxTargets)
            {
                Vector2 randPos = Random.insideUnitCircle * spawnRadius;
                Instantiate(
                    targetPrefab,
                    new Vector3(
                        randPos.x + transform.position.x,
                        heightOffset + transform.position.y,
                        randPos.y + transform.position.z
                    ),
                    Quaternion.identity,
                    transform
                );
            }
        }
    }
}
