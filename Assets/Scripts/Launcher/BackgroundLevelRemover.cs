using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLevelRemover : MonoBehaviour
{
    private void Start()
    {
        LevelManager.RemovePreviousObstacles();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            LevelManager.AddObstaclesToList(other.gameObject);
        }
    }
}
