using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapObject : MonoBehaviour
{
    public PersistentDataManager dataManager;

    private PersistentData persistentData;

    public SpriteRenderer prompt;

    private float promptVisibleTimer = 0;
    private bool touchingPlayer = false;

    private void Start()
    {
        persistentData = dataManager.GetCurrentData();
    }

    private void Update()
    {
        if (!touchingPlayer)
        {
            promptVisibleTimer = Mathf.Max(0, promptVisibleTimer - Time.unscaledDeltaTime);
        }
        prompt.color = new Color(1f, 1f, 1f, promptVisibleTimer);

        if (touchingPlayer && Input.GetKey(KeyCode.F))
        {
            Collect();
        }
    }

    private void Collect()
    {
        persistentData.AddScrap(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collect();
        if (collision.name == "Player")
        {
            promptVisibleTimer = 1f;
            touchingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            promptVisibleTimer = 1f;
            touchingPlayer = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
