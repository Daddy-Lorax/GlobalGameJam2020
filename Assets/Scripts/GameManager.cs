using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Tooltip("Don't touch")]
    public PersistentDataManager persistentManager;

    [Tooltip("Resets all data if true")]
    public bool isBeginning = false;

    private PersistentData persistentData;

    private void OnEnable()
    {
        if (isBeginning)
        {
            persistentManager.StartGame();
        }
    }

    private void Start()
    {
        persistentData = persistentManager.GetCurrentData();
    }

    private void Update()
    {
        persistentData.Update();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
