
using UnityEngine;

public class ObstacleEnablingScript : MonoBehaviour
{
    private void OnEnable()
    {

        Invoke("EnableCollider", 0.4f);

    }

    private void OnDisable()
    {

        Collider[] col = GetComponents<Collider>();
        foreach (var item in col)
        {
            item.isTrigger = true;
        }

    }

    void EnableCollider()
    {
        Collider[] col = GetComponents<Collider>();
        foreach (var item in col)
        {
            item.isTrigger = false;
        }
    }
}


