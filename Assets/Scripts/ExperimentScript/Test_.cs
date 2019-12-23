using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ : MonoBehaviour
{

 

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
        //GetComponent<Rigidbody>().velocity = launchData.initialVelocity;
    }

    MyMath.LaunchData launchData;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            float randomPointdistance = Random.Range(minDis, maxDis);
           
            point = MyMath.getRandomDirectionVectorWithinRange(angle, transform);
            point = transform.position +point * randomPointdistance;
        }
    }


    public int circularResolution;
    public int radiusResolution=1;
    public float currentAngle;

    private void OnDrawGizmos()
    {
       
        maxDis = Mathf.Max(maxDis, minDis);
        MyMath.getDirectionVectorsFromAngle(angle, out point_01,out point_02, transform);
        Debug.DrawRay(transform.position, point_01 * maxDis, Color.blue);
        Debug.DrawRay(transform.position, point_02 * maxDis, Color.red);



        circularResolution = Mathf.Clamp(circularResolution, 4, 100);
        radiusResolution = Mathf.Clamp(radiusResolution, 1, 100);
        
        float angleDistribution = 360 / circularResolution;
        float radiusDistribution = maxDis / radiusResolution;


        

        for (int i = 0; i < radiusResolution; i++)
        {
            for (int x = 0;x <=360 ; x += (int)angleDistribution)
            {
                Vector3 circlePoint = new Vector3(Mathf.Cos((x) * Mathf.Deg2Rad), 0f, Mathf.Sin((x) * Mathf.Deg2Rad))*radiusDistribution*i;
                circlePoint += transform.position;

                float actualAngle = MyMath.LimitAngleFrom_0_To360(transform.localEulerAngles.y) +x;
                actualAngle = MyMath.LimitAngleFrom_0_To360(actualAngle);
                                
                if (MyMath.IsValueInsideRange(actualAngle,0,angle/2) || MyMath.IsValueInsideRange(actualAngle,360-angle/2,360))
                {
                 Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(circlePoint, 0.3f);

                }
                else
                {
                    Gizmos.color = Color.red;
                }
            }

        }
      
        

        Gizmos.DrawSphere(point, 2f);

       

       
       




    }

  
}

