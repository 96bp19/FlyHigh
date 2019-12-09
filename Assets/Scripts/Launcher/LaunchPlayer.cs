﻿
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlayer : MonoBehaviour
{
    // rigidbody component of any object that needs to be launched
    public Rigidbody LaunchRigidBody;
  

    public float height = 10;
    public float gravity = -10;
    public bool debugPath;
    public static bool itStoped = false;
    bool jumpckeck = true, colisionCheck = false;

    Vector3 randomLocation;

    public BoxCollider boxCollier;

    public GameObject ObjectToSpawnPrefab;
    public GameObject PickableItemPrefab;

    List<GameObject> allpickableItems = new List<GameObject>();

    public LaunchPlayer previousLauncher;


    

    void Start()
    {
       


    }

   
 
    void Launch(Vector3 targetLoc)
    {
        Physics.gravity = Vector3.up * gravity;
        LaunchRigidBody.useGravity = true;
        LaunchRigidBody.velocity = CalculateLaunchData(targetLoc).initialVelocity;
    }
    LaunchData CalculateLaunchData(Vector3 targetLoc)
    {
        float displacementY =targetLoc.y - LaunchRigidBody.position.y;
        Vector3 displacementXZ = new Vector3(targetLoc.x - LaunchRigidBody.position.x, 0, targetLoc.z - LaunchRigidBody.position.z);
        float time = Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }
    void DrawPath(Vector3 targetLoc)
    {
        LaunchData launchData = CalculateLaunchData( targetLoc);
        Vector3 previousDrawPoint = LaunchRigidBody.position;

        int resolution = 30;
        Gizmos.color = Color.green;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = LaunchRigidBody.position + displacement;
            //Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            Gizmos.DrawLine(previousDrawPoint, drawPoint);
            //GameObject  obj =Instantiate(spherePrefab, transform);
            //obj.transform.position = previousDrawPoint;
            previousDrawPoint = drawPoint;
        }
    }


    void SpawnPickableItems(Vector3 targetLoc)
    {
        LaunchData launchData = CalculateLaunchData(targetLoc);
        Vector3 previousDrawPoint = LaunchRigidBody.position;

        int resolution = 10;

        Vector3 randVector = MyMath.RandomVectorInRange(Vector3.one* 0.3f, Vector3.one);
        Color itemColor = new Color(randVector.x,randVector.y,randVector.z);
        Gizmos.color = Color.green;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = LaunchRigidBody.position + displacement;
            if (MyMath.RandomBool())
            {
               GameObject obj = Instantiate(PickableItemPrefab, drawPoint, Quaternion.LookRotation(previousDrawPoint- drawPoint));
                allpickableItems.Add(obj);
              //  obj.GetComponentInChildren<Renderer>().material.color = itemColor;

            }
            previousDrawPoint = drawPoint;

            if (i >resolution -2)
            {
                break;
            }
            
        }
    }    

    void OnDrawGizmos()
    {
//         if (debugPath)
//         {
//             DrawPath();
//         }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(randomLocation, 1);
    }
    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;
        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LaunchRigidBody = other.GetComponent<Rigidbody>();
            Vector3 spawnedPos = generateRandomSpawnLocation();
            GameObject newSpawnPlace = Instantiate(ObjectToSpawnPrefab, spawnedPos,Quaternion.identity);
            LaunchPlayer launcher = newSpawnPlace.GetComponent<LaunchPlayer>();
            if (launcher)
            {
                launcher.previousLauncher = this;
            }
            if (previousLauncher)
            {
                previousLauncher.DestroyObjectAnditsItems();
            }
            newSpawnPlace.name = "PlayerFallPlace";
            Debug.Log(spawnedPos);
         
            boxCollier = newSpawnPlace.GetComponent<BoxCollider>();
            randomLocation = MyMath.GetRandomPointInCollider(boxCollier);

            
            Launch(randomLocation);
            SpawnPickableItems(newSpawnPlace.transform.position);
          

           
        

        }
    }

    // removes previous launcher and its spawned items
    public void DestroyObjectAnditsItems()
    {
            foreach (var item in allpickableItems)
            {
                if (item != null)
                {
                    Destroy(item);
                }

            }

        allpickableItems.Clear();
        Destroy(this.gameObject);

    }


    public float Randomgenerationrange =100;
    float minHeightForSpawn= 0, maxHeightForSpawn=200;
    Vector3 generateRandomSpawnLocation()
    {
        Vector3 newRandomPoint = MyMath.RandomVectorInRange(transform.position - new Vector3(Randomgenerationrange, 10, Randomgenerationrange), transform.position + new Vector3(Randomgenerationrange, 10, Randomgenerationrange));
        newRandomPoint.y = Mathf.Clamp(newRandomPoint.y,minHeightForSpawn, maxHeightForSpawn);
        return newRandomPoint;
        
    }


   



}
