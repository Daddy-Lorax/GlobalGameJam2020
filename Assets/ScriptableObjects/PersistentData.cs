
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

    internal void Backup(PersistentData other)
    {
        this.STAMINA_DURATION = other.STAMINA_DURATION;
        this.lastSavedStamina = other.lastSavedStamina;
        this.currentStamina = other.currentStamina;
        this.playerDies = other.playerDies;
        this.scrapObjectList = other.scrapObjectList;
        this.level = other.level;
    }

    public List<String> levelNames = new List<String>();
    public bool isPlayerDead = false;

    public void StartGame()
    {
        isPlayerDead = false;
        currentStamina = STAMINA_DURATION;
        lastSavedStamina = STAMINA_DURATION;
        level = 0;
        scrapObjectList.Clear();
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

    internal void AddScrap(ScrapObject self)
    {
        scrapObjectList.Add(self);
        self.gameObject.SetActive(false);
    }

    public int GetScrapSize()
    {
        return scrapObjectList.Count;
    }
}
