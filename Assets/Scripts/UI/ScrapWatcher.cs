using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrapWatcher : MonoBehaviour
{
    public PersistentDataManager dataManager;

    private PersistentData persistentData;
    private TextMeshProUGUI textScrapSize;

    private void Start()
    {
        persistentData = dataManager.GetCurrentData();
        textScrapSize = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textScrapSize.SetText(persistentData.GetScrapSize().ToString());
    }
}
