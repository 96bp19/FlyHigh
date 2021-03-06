﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static List<GameObject> obstacles = new List<GameObject>();
    public int noOfPlatformBeforeGameEnds;
    public float menuShowDelay = 2f;


    [HideInInspector]
    public bool ragdollEnabled = false;
    public static int CurrentPlatformCount = 0;

   
    [SerializeField]
    public int bonuslevelAfter = 5;

    public LaunchPlayer launchPadPrefab;


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

    [HideInInspector]
    public UnityEvent levelCompleteEventListener;
    [HideInInspector]
    public UnityEvent levelFailedEventListener;


    public UnityEvent levelCompleteGUI_event;
    public UnityEvent levelFailedGUI_event;



    public delegate void OnlevelRestarted();
    public static OnlevelRestarted LevelRestartEventListeners;

    public static saveData saveFile;

    GameObject Player;

    static bool levelCompleted;

    private void Awake()
    {
        saveFile = SaveManager.LoadData();
       
    }

    public void OnlevelComplete()
    {
       
        levelCompleted = true;
        saveFile.currentLevel++;
        SaveManager.SaveData(saveFile);

        // Invoke Level complete event
        levelCompleteEventListener.Invoke();
        Invoke("OnLevelCompleteGUI", menuShowDelay);

    }

   

    public void OnPlayerDied()
    {   
        levelCompleted = true;
        //invoke levelFailed event
        levelFailedEventListener.Invoke();
        Invoke("OnLevelFailedGUI", menuShowDelay);
        
   
    }
    void OnLevelCompleteGUI()
    {
        levelCompleteGUI_event.Invoke();
    }

    void OnLevelFailedGUI()
    {
        levelFailedGUI_event.Invoke();
    }

    public static void RemovePreviousObstacles()
    {
        if (obstacles != null)
        {
            foreach (var item in obstacles)
            {
                // old disabled obstacles are re enabled
                item.SetActive(true);
               
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
           
        }
    }

    public bool isBonusStage()
    {
        return saveFile.currentLevel % bonuslevelAfter == 0;
    }

    public int getCurrentLevel()
    {
        return saveFile.currentLevel;
    }

    public void RestartLevel()
    {
        CurrentPlatformCount = 0;
        LevelRestartEventListeners?.Invoke();
        GenerateLevel();
    }

    private void Start()
    {
        Player = FindObjectOfType<PlayerController>().gameObject;
        GenerateLevel();
    }

   

    public void NextLevel()
    {
        //GenerateLevel();
        RestartLevel();
    }

    void GenerateLevel()
    {
        levelCompleted = false;
        LaunchPlayer launcher = FindObjectOfType<LaunchPlayer>();
        
        
       

        if (launcher)
        {
            launcher.transform.parent = null;
            launcher.transform.position = launcher.generateRandomSpawnLocation();
        }
        else
        {
            launcher = Instantiate(launchPadPrefab, transform) as LaunchPlayer;
            launcher.transform.parent = null;
            launcher.transform.position = launcher.generateRandomSpawnLocation();
            
        }

        if (Player)
        {
            Player.GetComponent<Rigidbody>().velocity = new Vector3(0, Player.GetComponent<Rigidbody>().velocity.y, 0);
            
            Player.transform.position = launcher.transform.position + Vector3.up * 10;
        }
     
    }
       
}
