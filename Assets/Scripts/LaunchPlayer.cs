
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlayer : MonoBehaviour
{
    public Rigidbody ball;
    public Transform target;

    public float height = 1;
    public float gravity = -10;
    public bool debugPath;
    public static bool itStoped = false;
    bool jumpckeck = true, colisionCheck = false;
    public Vector3 checkTransform;


    public GameObject spherePrefab;

    void Start()
    {
        //ball.useGravity = false;
    }

    void Update()
    {


        height = Mathf.Clamp(height, 0.5f, 8);
       
    }

 
    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateLaunchData().initialVelocity;
    }
    LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
        float time = Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }
    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = ball.position;

        int resolution = 30;
        Gizmos.color = Color.green;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ball.position + displacement;
            //Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            Gizmos.DrawLine(previousDrawPoint, drawPoint);
            //GameObject  obj =Instantiate(spherePrefab, transform);
            //obj.transform.position = previousDrawPoint;
            previousDrawPoint = drawPoint;
        }
    }
    void OnDrawGizmos()
    {
        if (debugPath)
        {
            DrawPath();
        }
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



}
