﻿
using RoboRyanTron.Unite2017.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/PersistentDataManager")]
public class PersistentDataManager : ScriptableObject
{
    [Header("Group Data")]
    public PersistentData currentData;
    public PersistentData backupData;

    [Header("Game data")]
    public int level = 0;
    public List<String> levelNames = new List<String>();

    public void StartGame()
    {
        currentData.StartGame();
        backupData.StartGame();
    }

    public PersistentData GetCurrentData()
    {
        return currentData;
    }

    public void NextLevel()
    {
        backupData.Backup(currentData);
        if (level < levelNames.Count)
        {
            SceneManager.LoadScene(levelNames[level]);
        }
        else
        {
            Debug.Log("Level names insufficient. Count: " + level.ToString());
        }
    }

}
