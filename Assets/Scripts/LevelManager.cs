using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static List<GameObject> obstacles = new List<GameObject>();

    public int noOfPlatformBeforeGameEnds;
    public static int CurrentPlatformCount = 0;

    [SerializeField]
    private int currentLevel =10;
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

    public delegate void LevelCompleteEvent();
    public static LevelCompleteEvent levelCompleteEventListeners;

    public delegate void LevelFailedEvent();
    public static LevelFailedEvent levelFailedEventListeners;

    public delegate void OnlevelRestarted();
    public static OnlevelRestarted LevelRestartEventListeners;

    GameObject Player;

    static bool levelCompleted;

    public static void OnlevelComplete()
    {
        levelCompleteEventListeners?.Invoke();
        levelCompleted = true;
    }

    public static void OnPlayerDied()
    {
        levelFailedEventListeners?.Invoke();
        levelCompleted = true;
        
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

    public bool isBonusStage()
    {
        return currentLevel % bonuslevelAfter == 0;
    }

    public int getCurrentLevel()
    {
        return currentLevel;
    }

    public void setCurrentLevel(int levelcount)
    {
        currentLevel = levelcount;
    }

    public void ResetLevelStats()
    {
        LevelRestartEventListeners?.Invoke();
        GenerateLevel();
    }

    private void Start()
    {
        Player = FindObjectOfType<PlayerController>().gameObject;
        GenerateLevel();
    }

    public bool ragdollEnabled = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && levelCompleted)
        {
            // GenerateLevel();
            ResetLevelStats();
           
        }
    }

    public void NextLevel()
    {
        GenerateLevel();
    }

   public void GenerateLevel()
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
