using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour
{
    private int HASH_ENDING_CONTROLLER = Animator.StringToHash("EndingCode");

    public PersistentDataManager dataManager;
    public float duration;

    private PersistentData persistentData;

    private void Start()
    {
        persistentData = dataManager.GetCurrentData();
        int endingCode = (persistentData.currentStamina >= PersistentData.SAVE_NUM) ?
            2 : 1;
        Animator animator = GetComponent<Animator>();
        animator.SetInteger(HASH_ENDING_CONTROLLER, endingCode);
    }

    private void Update()
    {
        
    }
}
