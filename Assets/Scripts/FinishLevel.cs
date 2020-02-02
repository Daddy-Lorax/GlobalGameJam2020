using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public PersistentData persistentData;

    //public SaveData saveData;
    public AudioSource audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("We win");
        //audioClip.Play();
        gameObject.SetActive(false);
        persistentData.NextLevel();
        //saveData.IncreaseLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
