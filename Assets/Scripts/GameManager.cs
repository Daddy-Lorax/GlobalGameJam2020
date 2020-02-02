using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("Don't touch")]
    public PersistentData persistentData;

    [Tooltip("Resets all data if true")]
    public bool isBeginning = false;

    private void Start()
    {
        if (isBeginning)
        {
            persistentData.StartGame();
        }
    }

    private void Update()
    {
        persistentData.Update();
    }
}
