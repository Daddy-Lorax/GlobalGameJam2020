using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public PersistentDataManager dataManager;

    //public SaveData saveData;
    public AudioSource audioClip;

    private PersistentData persistentData;

    private void Start()
    {
        persistentData = dataManager.GetCurrentData();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("We win");
        //audioClip.Play();
        gameObject.SetActive(false);
        dataManager.NextLevel();
        //saveData.IncreaseLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
