﻿
using UnityEngine;
using System.Collections;

public static class MyMath
{
    public static float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    public static float Get_Y_Rot_from_Velocity(Vector3 velocity)
    {
        Vector3 vel = velocity.normalized;
        return (Mathf.Atan2(vel.z, vel.x)) * 180 / Mathf.PI;

    }
    public static Vector3 calcNormalVector2(Vector3 startPoint, Vector3 collisionPoint, Vector3 endPoint)
    {

        Vector3 a = collisionPoint - startPoint;
        Vector3 b = endPoint - startPoint;

        float theta = Mathf.Acos(Vector3.Dot(a, b) / (a.magnitude * b.magnitude));

        float distance = a.magnitude * Mathf.Cos(theta);

        Vector3 normalPoint = startPoint + b.normalized * distance;

        return normalPoint;
    }

    public static bool RayCast(Vector3 position, Vector3 direction, float distance, LayerMask layerMask, out RaycastHit hit, Color color)
    {
        Debug.DrawRay(position, direction * distance, color);



        return Physics.Raycast(position, direction, out hit, distance, layerMask);

    }

    public static Vector3 GetSLopeInformaion(Vector3 rightTrans, Vector3 forwardTrans, Vector3 normal)
    {
        Vector3 slopeDirection = Vector3.Cross(rightTrans, normal);
        float angle = Vector3.SignedAngle(forwardTrans, slopeDirection, Vector3.up);

        return slopeDirection;

    }

    public static int GetEnmuLength<T>()
    {
        return System.Enum.GetValues(typeof(T)).Length;
    }

    public static Vector3 GetRandomPointInCollider(BoxCollider collider, bool useRandomY = false)
    {
        Vector3 point;
        if (collider == null)
        {
           
            return Vector3.one *200;
        }
        if (useRandomY)
        {
            point = new Vector3(
                Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                Random.Range(collider.bounds.min.y, collider.bounds.max.y),
                Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );

        }
        else
        {
            point = new Vector3(
               Random.Range(collider.bounds.min.x, collider.bounds.max.x),
               collider.bounds.min.y,
               Random.Range(collider.bounds.min.z, collider.bounds.max.z)
           );
        }


        if (point != collider.ClosestPoint(point))
        {
            Debug.Log("Out of the collider! Looking for other point...");
            point = GetRandomPointInCollider(collider, useRandomY);
        }

        return point;
    }

    public static void GetRelativeCamToWorldPos(out Vector3 min_val, out Vector3 max_val, Camera cam)
    {
        min_val = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        max_val = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }
    public static void GetRelativeCamToWorldPos(Camera cam, out cameraToScreenInfo cameraInfo)
    {
        Vector3 min_val, max_val;

        min_val = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        max_val = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));


        cameraInfo.minX = min_val.x;
        cameraInfo.minY = min_val.y;
        cameraInfo.maxX = max_val.x;
        cameraInfo.maxY = max_val.y;



    }

    public static Vector3 RandomVectorInRange(Vector3 min, Vector3 max)
    {


        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));

    }

    public static bool RandomBool()
    {
        if (Random.Range(0, 2) == 0)
        {
            return false;
        }
        return true;
    }

    public static bool IsValueInsideRange(float value, float min, float max)
    {
        if (value > max || value < min)
        {
            return false;
        }
        return true;
    }

    public static bool IsValueInsideRange(int value, int min, int max)
    {
        if (value > max || value < min)
        {
            return false;
        }
        return true;
    }

    public static bool IsValueInsideRange(Vector3 value, Vector3 min, Vector3 max)
    {
        if (IsValueInsideRange(value.x, min.x, max.x) && IsValueInsideRange(value.y, min.y, max.y) && IsValueInsideRange(value.z, min.z, max.z))
        {
            return true;
        }
        return false;
    }

    public static Vector3 SmoothedNormal(RaycastHit aHit)
    {
        var MC = aHit.collider as MeshCollider;
        if (MC == null)
            return aHit.normal;
        var M = MC.sharedMesh;
        var normals = M.normals;
        var indices = M.triangles;
        var N0 = normals[indices[aHit.triangleIndex * 3 + 0]];
        var N1 = normals[indices[aHit.triangleIndex * 3 + 1]];
        var N2 = normals[indices[aHit.triangleIndex * 3 + 2]];
        var B = aHit.barycentricCoordinate;
        var localNormal = (B[0] * N0 + B[1] * N1 + B[2] * N2).normalized;
        return MC.transform.TransformDirection(localNormal);
    }

    public static bool getSuccessRate(float successChance)
    {
      
        if (Random.Range(0,100)<=successChance)
        {
            return true;
        }
        return false;
    }

    public static void RunFunctionAfter(System.Action functionName ,MonoBehaviour behaviour, float delay)
    {
        //  function call syntax
        // System.Action a = () => AddForce();
        // MyMath.RunFunctionAfter(a, this, delay);
        behaviour.StartCoroutine(DelayRoutine(functionName,delay));
    }

    public static void RunFunctionAfter(System.Action functionName, MonoBehaviour behaviour, float delay, out IEnumerator routine)
    {
        //  function call syntax
        // System.Action a = () => AddForce();
        // MyMath.RunFunctionAfter(a, this, delay);
        routine = DelayRoutine(functionName, delay);
        behaviour.StartCoroutine(routine);
        

    }

    public static IEnumerator DelayRoutine(System.Action functionname , float delay)
    {

        yield return new WaitForSeconds(delay);
        functionname();
    }

    public static void StopCalledFunction(MonoBehaviour behaviour ,IEnumerator routine)
    {
        if (routine != null)
        {
            behaviour.StopCoroutine(routine);

        }
    }

   public static  Vector3 AngleLerp(Vector3 StartAngle, Vector3 FinishAngle, float t)
    {
        float xLerp = Mathf.LerpAngle(StartAngle.x, FinishAngle.x, t);
        float yLerp = Mathf.LerpAngle(StartAngle.y, FinishAngle.y, t);
        float zLerp = Mathf.LerpAngle(StartAngle.z, FinishAngle.z, t);
        Vector3 Lerped = new Vector3(xLerp, yLerp, zLerp);
        return Lerped;
    }

    public static void getDirectionVectorsFromAngle(float angleInDegree, out Vector3 vector1, out Vector3 vector2, Transform relativeTransform)
    {
        vector1 = new Vector3(Mathf.Cos(angleInDegree / 2 * Mathf.Deg2Rad), 0, Mathf.Sin(angleInDegree / 2 * Mathf.Deg2Rad));
        vector1 = relativeTransform.TransformDirection(vector1).normalized;

        vector2 = new Vector3(Mathf.Cos(-angleInDegree / 2 * Mathf.Deg2Rad), 0, Mathf.Sin(-angleInDegree / 2 * Mathf.Deg2Rad));
        vector2 = relativeTransform.TransformDirection(vector2).normalized;


    }

    public static Vector3 getRandomDirectionVectorWithinRange(float angle, Transform relativeTransform)
    {
        angle = (int)angle % 360;
        float randomAngle = Random.Range(-angle / 2, angle / 2);
        Vector3 point = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0, Mathf.Sin(randomAngle * Mathf.Deg2Rad));
        return relativeTransform.TransformDirection(point).normalized;

    }

   
    public static LaunchData CalculateLaunchData(Vector3 targetLoc, Rigidbody LaunchRigidBody,float height,float gravity)
    {

        height += targetLoc.y;
        float displacementY = targetLoc.y - LaunchRigidBody.position.y;
        Vector3 displacementXZ = new Vector3(targetLoc.x - LaunchRigidBody.position.x, 0, targetLoc.z - LaunchRigidBody.position.z);
        float time = Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    public static float LimitAngleFrom_0_To360(float angle)
    {
        float decimalVal = getDecimalFromFloat(angle);
        
        angle = (int)angle % 360;
        if (angle < 0)
        {
            angle = 360 - angle;
        }

        
        return angle+decimalVal;
    }

    public static  float getDecimalFromFloat(float val)
    {
        return val - (int)val;
    }

    public struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;
        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }

    


}
    public struct cameraToScreenInfo
    {
        public float minX, minY, maxX, maxY;
    }

    




// TIps for geting  the class type from the object

/*                                                                    *********
 *                                                                    **********
       typeof(WeatherStation).IsAssignableFrom(observer.GetType()) 
***********                                                           ***************
* *****                                                                 *********************
**/






/* 
 *  used for aligning player to the surface
 * 
     //note, the code here suppose our character use a capsuleCollider and the floors' layerMask is "floor"..
     
    　　　　　//..if yours' is not, you should make some change.
     
     
            RaycastHit hitInfo;
            Vector3 capsuleColliderCenterInWorldSpace=GetComponent<CapsuleCollider> ().transform.TransformPoint (GetComponent<CapsuleCollider>().center);
            bool isHit=Physics.Raycast (capsuleColliderCenterInWorldSpace,new Vector3(0f,-1f,0f),out hitInfo,100f,LayerMask.GetMask("floor"));
     
            Vector3 forward=GetComponent<Rigidbody>().transform.forward;
         
            Vector3 newUp;
            if (isHit) {
                newUp = hitInfo.normal;
            } else {
                newUp = Vector3.up;
            }
            Vector3 left = Vector3.Cross (forward,newUp);//note: unity use left-hand system, and Vector3.Cross obey left-hand rule.
            Vector3 newForward = Vector3.Cross (newUp,left);
            Quaternion oldRotation=GetComponent<Rigidbody>().transform.rotation;
            Quaternion newRotation = Quaternion.LookRotation (newForward, newUp);
     
    　　　　　float kSoftness=0.1f;//if do not want softness, change the value to 1.0f
            GetComponent<Rigidbody> ().MoveRotation (Quaternion.Lerp(oldRotation,newRotation,kSoftness));

     
     */
