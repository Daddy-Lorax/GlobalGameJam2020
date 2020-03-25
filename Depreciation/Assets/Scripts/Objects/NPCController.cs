using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Animator exploAnim;
    [SerializeField] private Transform choicePrompt;
    [SerializeField] private Transform helpBubble;
    public Sprite fixedSprite;
    public Animator animator;

    private PersistentData persistentData;
    public PersistentDataManager dataManager;

    public float fixTime;
    public float fixAmount;
    public float eatTime;
    public float eatAmount;
    private float fixTimeFull;
    private BoxCollider2D bc;
    private bool touchPlayer;
    public int currentState;
    private CharacterMovement cm;

    private GameObject player;

    public float helpDistance = 2;
    public float promptDistance = 1;
    public Vector2 helpOffset;
    //private bool promptsVisible = true;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        touchPlayer = false;
        currentState = 0;
        fixTimeFull = fixTime;

        //button = gameObject.transform.GetChild(0);
        choicePrompt.transform.localScale = new Vector3(0, 0, 1);

        //help = gameObject.transform.GetChild(1);
        helpOffset = helpBubble.transform.localPosition;

        cm = GameObject.Find("Player").GetComponent<CharacterMovement>();
        player = GameObject.Find("Player");
        persistentData = dataManager.GetCurrentData();

    }

    // Update is called once per frame
    void Update()
    {
        bool helpActive = (((Vector2)player.transform.position - (Vector2)transform.position).magnitude > helpDistance);
        //Debug.Log((player.transform.position - transform.position));
        helpBubble.gameObject.SetActive(helpActive);
        helpBubble.transform.localPosition = helpOffset + Vector2.up * Mathf.Sin(Time.time) * 0.2f;

        if (touchPlayer && currentState == 0)
        {
            choicePrompt.transform.localScale = new Vector3(1, 1, 1);
            if (Input.GetKey(KeyCode.E))
            {
                currentState = 1;
                cm.timeFreeze = true;
                Rescue();
            }
            else if (Input.GetKey(KeyCode.F))
            {
                currentState = 3;
                cm.timeFreeze = true;
                Consume();
            }
        }
        else
        {
            choicePrompt.transform.localScale = new Vector3(0, 0, 1);
        }

        if (currentState == 1)
        {
            fixTime -= Time.deltaTime;
            persistentData.currentStamina -= Time.deltaTime * fixAmount;
            if (fixTime < 0)
            {
                cm.timeFreeze = false;
                currentState = 2;
                //gameObject.GetComponent<SpriteRenderer>().sprite = fixedSprite;
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

    public void Rescue()
    {
        //TODO: move full rescue functionality here
        choicePrompt.gameObject.SetActive(false);
        helpBubble.gameObject.SetActive(false);
        animator.Play("Escape");
    }

    public void Consume()
    {
        //TODO: move full consume functionality here
        exploAnim.Play("explo");

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            touchPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        touchPlayer = false;
    }
}
