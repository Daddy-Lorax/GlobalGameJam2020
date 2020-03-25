
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

    [Header("Audio File")]
    public GameObject audioPrefab;

    //private GameObject audioGameObject;
    private AudioSource audio;

    public void StartGame()
    {
        currentData.StartGame();
        backupData.StartGame();
        //PlayAudio();
    }

    public PersistentData GetCurrentData()
    {
        return currentData;
    }

    public void NextLevel()
    {
        level++;
        backupData.Backup(currentData);
        if (level < levelNames.Count)
        {
            SceneManager.LoadScene(levelNames[level]);
        }
        else if (level == levelNames.Count)
        {
            SceneManager.LoadScene("Ending");
        }
        else
        {
            Debug.Log("Level names insufficient. Count: " + level.ToString());
        }
    }

    public void ShowEnding()
    {
        SceneManager.LoadScene("Ending");
    }

    [System.Obsolete]
    private void PlayAudio()
    {
        //audioGameObject = Instantiate(audioPrefab);
        //audio = audioGameObject.GetComponent<AudioSource>();
        //audio.Play();
        //audio.volume = 0.2f;
        //DontDestroyOnLoad(audioGameObject);
    }

    private void OnDestroy()
    {
        /*if (audioGameObject != null)
        {
            Destroy(audioGameObject);
        }*/
    }
}
