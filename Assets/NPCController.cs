using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float fixTime;
    public float fixAmount;
    public float eatTime;
    public float eatAmount;
    private float fixTimeFull;
    private BoxCollider2D bc;
    private bool touch;
    public int currentState;
    public PersistentDataManager dataManager;
    private Transform button;
    private Transform help;
    private CharacterMovement cm;
    private PersistentData persistentData;
    public Sprite fixedSprite;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        touch = false;
        currentState = 0;
        fixTimeFull = fixTime;

        button = gameObject.transform.GetChild(0);
        button.transform.localScale = new Vector3 (0,0,1);
    
        help = gameObject.transform.GetChild(1);

        cm = GameObject.Find("Player").GetComponent<CharacterMovement>();
        persistentData = dataManager.GetCurrentData();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == 0) {
            if (Mathf.Sin(Time.time * 4) > 0) {
                help.transform.localScale = new Vector3 (1,1,1);
            }
            else
            {
                 help.transform.localScale = new Vector3 (0,0,1);
            }
            //help.GetComponent<SpriteRenderer>().color = new Color (1,1,1, / 2 + 0.75f);
        }
        else
        {
            help.transform.localScale = new Vector3 (0,0,1);
        }

        if (touch && currentState == 0) {
            button.transform.localScale = new Vector3 (1,1,1);
            if (Input.GetKey(KeyCode.E)) 
            {
                currentState = 1;
                cm.timeFreeze = true;
            }
            else if (Input.GetKey(KeyCode.F))
            {
                currentState = 3;
                cm.timeFreeze = true;
            }
        }
        else
        {
            button.transform.localScale = new Vector3 (0,0,1);
        }

        if (currentState == 1) 
        {
            fixTime -= Time.deltaTime;
            persistentData.currentStamina -= Time.deltaTime * fixAmount;
            if (fixTime < 0)
            {
                cm.timeFreeze = false;
                currentState = 2;
                gameObject.GetComponent<SpriteRenderer>().sprite = fixedSprite;
            }
        }
        else if (currentState == 3)
        {
            eatTime -= Time.deltaTime;
            persistentData.currentStamina += Time.deltaTime * eatAmount;
            if (eatTime < 0)
            {

                cm.timeFreeze = false;
                currentState = 4;
                gameObject.SetActive(false);
            }
        }
        

    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.name == "Player") {
            touch = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        touch = false;
    }
}
