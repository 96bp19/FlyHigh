
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlayer : MonoBehaviour
{
    // rigidbody component of any object that needs to be launched
    [HideInInspector]
    public Rigidbody LaunchRigidBody;
  
    [HideInInspector]
    public LaunchPlayer previousLauncher;

    public float height = 2;
    public float gravity = -10;
    public bool debugPath;
    public static bool itStoped = false;
    bool jumpckeck = true, colisionCheck = false;

    private Vector3 randomLocation;
    public GameObject launchPadPrefab;
    public GameObject RingItemPrefab;
    public GameObject bonusLevelItemPrefab;

    GameObject Player;
    

    List<GameObject> allpickableItems = new List<GameObject>();


    public GameObject ImpactParticle;

  
    public levelSpawnableItems[] objectToSpawnBasedOnLevel;

    
    
    
    [System.Serializable]
    public struct levelSpawnableItems
    {
        public int MaxLevelRange;
        public GameObject[] itemToSpawn;

        
    }

    void Start()
    {
        Player = FindObjectOfType<PlayerController>().gameObject;
        

    }

   
 
    void Launch(Vector3 targetLoc)
    {
        Physics.gravity = Vector3.up * gravity;
        LaunchRigidBody.useGravity = true;
        LaunchRigidBody.velocity = CalculateLaunchData(targetLoc).initialVelocity;
    }
    LaunchData CalculateLaunchData(Vector3 targetLoc)
    {
        float newHeight = targetLoc.y + height;
        Debug.Log("launch loc : " + targetLoc);
        float displacementY =targetLoc.y - LaunchRigidBody.position.y;
        Vector3 displacementXZ = new Vector3(targetLoc.x - LaunchRigidBody.position.x, 0, targetLoc.z - LaunchRigidBody.position.z);
        float time = Mathf.Sqrt(-2 * newHeight / gravity) + Mathf.Sqrt(2 * (displacementY - newHeight) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * newHeight);
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
            Gizmos.DrawLine(previousDrawPoint, drawPoint);
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
            if (i >2)
            {
                float simulationTime = i / (float)resolution * launchData.timeToTarget;
                Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
                Vector3 drawPoint = LaunchRigidBody.position + displacement;


                if (LevelManager.Instance.isBonusStage())
                {
                    GameObject obj = Instantiate(bonusLevelItemPrefab, drawPoint, Quaternion.LookRotation(previousDrawPoint - drawPoint));
                    allpickableItems.Add(obj);
                }
                else
                {
                    if (i == 7)
                    {
                        GameObject obj = Instantiate(getPickableItembasedOnlevel(), drawPoint, Quaternion.LookRotation(previousDrawPoint- drawPoint));
                        allpickableItems.Add(obj);

                    }
                    else
                    {
                        GameObject obj = Instantiate(RingItemPrefab, drawPoint, Quaternion.LookRotation(previousDrawPoint - drawPoint));
                        allpickableItems.Add(obj);
                    }

                }
                

                
                previousDrawPoint = drawPoint;

                if (i >resolution -2)
                {
                    break;
                }

            }
            
        }
    }    

    void OnDrawGizmos()
    {
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


    GameObject getPickableItembasedOnlevel()
    {
        GameObject obj = null;
        foreach (var item in objectToSpawnBasedOnLevel)
        {
            if (LevelManager.Instance.getCurrentLevel() <= item.MaxLevelRange)
            {
                obj = item.itemToSpawn[Random.Range(0, item.itemToSpawn.Length)];
                break;
            }
        }

        return obj;
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CheckLevelCompleteStatus())
            {
                // level complete no need to call this method again;
                return;
            }
            other.GetComponent<PlayerParticleController>().OnPlayerLaunched();
               
            LaunchRigidBody = other.GetComponent<Rigidbody>();
            Vector3 spawnedPos = generateRandomSpawnLocation();
            Vector3 newLookRot = Quaternion.LookRotation(transform.position- spawnedPos, Vector3.up).eulerAngles;
            newLookRot.x = newLookRot.z = 0;
            GameObject newSpawnPlace = Instantiate(launchPadPrefab, spawnedPos,Quaternion.Euler(newLookRot));
            LaunchPlayer launcher = newSpawnPlace.GetComponent<LaunchPlayer>();
      
            // previous launchPad is removed from the scene
            if (launcher)                                           
            {
                launcher.previousLauncher = this;
            }
            if (previousLauncher)
            {
                LevelManager.RemovePreviousObstacles();
                previousLauncher.DestroyObjectAnditsItems();
            }
            newSpawnPlace.name = "PlayerFallPlace";
         
            randomLocation = newSpawnPlace.GetComponentInChildren<LandLocationCollider>().generateRandomPoint();

            Launch(randomLocation);
            SpawnPickableItems(newSpawnPlace.transform.position);

            UI_Manager.Instance.UpdateLandingText(Vector3.Distance(other.transform.position, transform.position));
            OnLanded(other.gameObject);
          
        }
    }

     bool CheckLevelCompleteStatus()
    {
        if (LevelManager.CurrentPlatformCount >=LevelManager.Instance.noOfPlatformBeforeGameEnds)
        {
            LevelManager.Instance.OnlevelComplete();
            return true;
        }
        LevelManager.CurrentPlatformCount++;
        return false;
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


    public float Randomgenerationrange =30;
    public float minHeightForSpawn= -5, maxHeightForSpawn=5;
    public float minDistance = 30, maxDistance = 60;
    public float spawnangle =70f;
    public float maxAllowedHeight = 100f;
    public Vector3 generateRandomSpawnLocation()
    {
        float randomDis = Random.Range(minDistance, maxDistance);
        
        Vector3 newRandomPoint;
        newRandomPoint = MyMath.getRandomDirectionVectorWithinRange(spawnangle, transform);
        newRandomPoint = newRandomPoint * randomDis + transform.position;
        newRandomPoint.y = transform.position.y;
        newRandomPoint.y += Random.Range(minHeightForSpawn , maxHeightForSpawn);
        newRandomPoint.y = Mathf.Clamp(newRandomPoint.y,2, maxAllowedHeight);
      
        return newRandomPoint;
        
    }

    // this function is used to spawn particle only
    public void OnLanded(GameObject player)
    {
        PlayerController player_controller = player.GetComponent<PlayerController>();
        if (player)
        {
            ImpactParticle.transform.position = player.transform.position;
            ImpactParticle.SetActive(true);
        }
    }

    


   



}
