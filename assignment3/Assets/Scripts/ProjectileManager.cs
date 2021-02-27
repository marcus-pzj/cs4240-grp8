using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DeleteInactiveProjectiles", 0.0f, 3.0f);
    }

    private void DeleteInactiveProjectiles()
    {
        foreach (
            Transform child in this.transform
        )
        {
            if (child.gameObject.layer == 8)
            {

                Destroy(child.gameObject);
            }
        }
    }
}
