
using RoboRyanTron.Unite2017.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/PersistentData")]
public class PersistentData : ScriptableObject
{
    [NonSerializedAttribute]
    public static int SAVE_NUM = 3;

    [Header("Player Status")]
    //[Tooltip("Time it takes for the stamina to go down in milliseconds")]// not anymore
    public float PLAYER_MAXHEALTH = 100;
    public float lastSavedStamina = 0;
    public float currentStamina = 100;

    public bool isPlayerDead = false;

    [Header("Progression Data")]
    ////
    // Note: These are draft values and may be changed. Much of this data should be used for room generation.
    ///
    //NPC Data
    public int numNpcEncountered = 0;
    public int numNpcRescues = 0;
    public int numNpcKills = 0;
    public int numNpcAbandon = 0;

    //Scrap Data
    public int numScrapEncountered = 0;
    public int numScrapGet = 0;
    public int numScrapMissed = 0;
    //public List<ScrapObject> scrapObjectList;

    //Exploration Data
    public int numRoomsClearedTotal = 0;
    public int numFloorsBeenTo = 0;
    public int roomsClearedThisFloor = 0;
    public int timesLeftExit = 0;
    public int timesRightExit = 0;
    public int timesUpExit = 0;
    public int timesDownExit = 0;
    public float timeInCurrentRoom = 0;
    public float totaltimeInRooms = 0;//Note: only count regular rooms for this.
    public float averageTimeInRooms = 0;
    public float timeOnCurrentFloor = 0;


    [Header("Game data")]
    public int level = 0;

    public List<String> levelNames = new List<String>();


    [Header("Events")]
    public GameEvent playerDies;


    public void StartGame()
    {
        isPlayerDead = false;
        currentStamina = PLAYER_MAXHEALTH;
        lastSavedStamina = PLAYER_MAXHEALTH;
        level = 0;
        //scrapObjectList.Clear();
    }

    public void Update()
    {
        //currentStamina -= Time.deltaTime;

        if (currentStamina <= 0 && !isPlayerDead)
        {
            playerDies.Raise();
            isPlayerDead = true;
        }
    }

    internal void AddScrap(ScrapObject self)
    {
        currentStamina += (float) 0.2 * PLAYER_MAXHEALTH;
        if (currentStamina >= PLAYER_MAXHEALTH) currentStamina = PLAYER_MAXHEALTH;
        self.gameObject.SetActive(false);
    }

    /*public int GetScrapSize()
    {
        return scrapObjectList.Count;
    }*/



    internal void Backup(PersistentData other)
    {
        this.PLAYER_MAXHEALTH = other.PLAYER_MAXHEALTH;
        this.lastSavedStamina = other.lastSavedStamina;
        this.currentStamina = other.currentStamina;
        this.playerDies = other.playerDies;
        //this.scrapObjectList = other.scrapObjectList;
        this.level = other.level;
    }
}
