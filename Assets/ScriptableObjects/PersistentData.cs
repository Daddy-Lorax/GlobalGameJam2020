
using RoboRyanTron.Unite2017.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/PersistentData")]
public class PersistentData : ScriptableObject
{
    [Header("Stamina")]
    [Tooltip("Time it takes for the stamina to go down in milliseconds")]
    public float STAMINA_DURATION = 10000;

    public float lastSavedStamina = 0;
    public float currentStamina = 0;

    [Header("Events")]
    public GameEvent playerDies;

    public List<ScrapObject> scrapObjectList;

    [Header("Game data")]
    public int level = 0;
    public List<String> levelNames = new List<String>();
    public bool isPlayerDead = false;

    public void StartGame()
    {
        isPlayerDead = false;
        currentStamina = STAMINA_DURATION;
        lastSavedStamina = STAMINA_DURATION;
        level = 0;
    }

    public void Update()
    {
        currentStamina -= Time.deltaTime;

        if (currentStamina <= 0 && !isPlayerDead)
        {
            playerDies.Raise();
            isPlayerDead = true;
        }
    }

    public void NextLevel()
    {
        lastSavedStamina = currentStamina;
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
