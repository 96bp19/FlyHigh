using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager 
{
    public static string levelCount = "LevelCount";
    
    public static void SaveData(saveData data)
    {
        PlayerPrefs.SetInt(levelCount, data.currentLevel);
    }
    public static saveData LoadData()
    {
        saveData data;
        data.currentLevel = PlayerPrefs.GetInt(levelCount, 1); 
        return data;
    }



}
public struct saveData
{
    public int currentLevel;

};
