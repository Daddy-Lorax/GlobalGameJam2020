using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour
{
    private int HASH_ENDING_CONTROLLER = Animator.StringToHash("EndingCode");
    Animator animator;

    public PersistentDataManager dataManager;
    public float duration;

    private PersistentData persistentData;

    public string[] endingStateNames = new string[] { "GoodEnd", "BadEnd" };

    public int endingCode = 0;

    private void Start()
    {
        persistentData = dataManager.GetCurrentData();
        //endingCode = (persistentData.currentStamina >= PersistentData.SAVE_NUM) ? 1 : 0;
        animator = GetComponent<Animator>();

        StartCutscene();
    }

    private void Update()
    {

    }

    public void StartCutscene()
    {

        animator.Play(endingStateNames[endingCode]);
    }
}
