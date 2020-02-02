
using RoboRyanTron.Unite2017.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void StartGame()
    {
        currentStamina = STAMINA_DURATION;
    }

    public void Update()
    {
        currentStamina -= Time.deltaTime;

        if (currentStamina <= 0)
        {
            playerDies.Raise();
        }
    }


}
