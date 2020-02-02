using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapObject : MonoBehaviour
{
    public PersistentDataManager dataManager;

    private PersistentData persistentData;

    private void Start()
    {
        persistentData = dataManager.GetCurrentData();
    }

    private void Keep()
    {
        
    }
}
