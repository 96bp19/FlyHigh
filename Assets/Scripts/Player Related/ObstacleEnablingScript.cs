
using UnityEngine;

public class ObstacleEnablingScript : MonoBehaviour
{
    private void OnEnable()
    {

        Invoke("EnableCollider", 0.4f);

    }

    private void OnDisable()
    {
        Debug.Log(name + " disabled");
        GetComponent<BoxCollider>().enabled = false;
    }

    void EnableCollider()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
}


