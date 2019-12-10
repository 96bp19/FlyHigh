using UnityEngine;

public class LandLocationCollider : MonoBehaviour
{
    // might need to add something here later

    public Vector3 point = Vector3.zero;
    Vector3 zeroVector = Vector3.zero;
    float minRandomRage =-30, maxRandomRange =30;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
         
            generateRandomPoint();
        }
  
    }

    public Vector3 generateRandomPoint()
    {
        point = zeroVector;
        point.x = Random.Range(minRandomRage, maxRandomRange);
        point = transform.root.TransformPoint(point);
        return point;
               
    }

   

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(point, 1.5f);
     
    }


}
