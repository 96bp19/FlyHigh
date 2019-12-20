using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ : MonoBehaviour
{

    
    public enum Transformval
    {
        transform_right,transformLeft, transform_up,transform_down,transform_forward,transform_back,vector_left,vector_right,vector_up,vector_down,vector_forward,vector_back
    }


    // Update is called once per frame
    

    public float minDis=5, maxDis=10;

    Vector3 point;
    Vector3 point_01;
    Vector3 point_02;

    public float angle;

    public GameObject target;
    public float height;
    private void Start()
    {
        point = transform.position;
        launchData = MyMath.CalculateLaunchData(target.transform.position, GetComponent<Rigidbody>(), height, Physics.gravity.y);
        GetComponent<Rigidbody>().velocity = launchData.initialVelocity;
    }

    MyMath.LaunchData launchData;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            float randomPointdistance = Random.Range(minDis, maxDis);
            float randomAngle = Random.Range(-angle/2, angle/2);
            point = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0, Mathf.Sin(randomAngle * Mathf.Deg2Rad));
            point = transform.TransformDirection(point);
            point = transform.position + point.normalized * randomPointdistance;
        }
    }

    private void OnDrawGizmos()
    {

        maxDis = Mathf.Max(maxDis, minDis);
        point_01 = new Vector3(Mathf.Cos(angle/2 * Mathf.Deg2Rad), 0, Mathf.Sin(angle/2 * Mathf.Deg2Rad));
        point_01 = transform.TransformDirection(point_01);
        point_02 = new Vector3(Mathf.Cos(-angle / 2 * Mathf.Deg2Rad), 0, Mathf.Sin(-angle / 2 * Mathf.Deg2Rad));
        point_02 = transform.TransformDirection(point_02);
        Debug.DrawRay(transform.position, point_01 * maxDis, Color.blue);
        Debug.DrawRay(transform.position, point_02 * maxDis, Color.red);
      


        

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(point, 2f);

       
       




    }

  
}

