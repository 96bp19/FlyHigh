using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static List<GameObject> obstacles = new List<GameObject>();

    public int noOfPlatformBeforeGameEnds;
    public static int CurrentPlatformCount = 0;


    private static LevelManager _Instance;
    public static LevelManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<LevelManager>();
            }
            return _Instance;
        }
        
    }

    public delegate void LevelCompleteEvent();
    public static LevelCompleteEvent levelCompleteEventListeners;

    public delegate void LevelFailedEvent();
    public static LevelFailedEvent levelFailedEventListeners;



    public static void OnlevelComplete()
    {
        levelCompleteEventListeners?.Invoke();
    }

    public static void OnPlayerDied()
    {
        levelFailedEventListeners?.Invoke();
        
    }

    public static void RemovePreviousObstacles()
    {
        if (obstacles != null)
        {
            foreach (var item in obstacles)
            {
                // old disabled obstacles are re enabled
                item.SetActive(true);
                Debug.Log("removed from list");
            }
        }
        obstacles.Clear();
    }

    public static void AddObstaclesToList(GameObject objToAdd)
    {
        if (!obstacles.Contains(objToAdd))
        {
            objToAdd.SetActive(false);
            obstacles.Add(objToAdd);
            Debug.Log("added to list");
        }
    }
       
}
