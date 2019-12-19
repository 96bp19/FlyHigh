
using UnityEngine;

public class ObstacleEnablingScript : MonoBehaviour
{
    private void OnEnable()
    {

        Invoke("EnableCollider", 0.4f);

    }

    private void OnDisable()
    {
      
        GetComponent<BoxCollider>().enabled = false;
    }

    void EnableCollider()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
}


