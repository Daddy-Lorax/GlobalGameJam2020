using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float fixTime;
    private float fixTimeFull;
    private BoxCollider2D bc;
    private bool touch;
    public bool beenFixed;
    public Transform child;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        touch = false;
        beenFixed = false;
        fixTimeFull = fixTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (touch && Input.GetKey(KeyCode.E) && !beenFixed) {
            fixTime -= Time.deltaTime;
            child.transform.localScale = new Vector3(fixTime / fixTimeFull, fixTime / fixTimeFull, fixTime / fixTimeFull);
            if (fixTime <= 0) {
                beenFixed = true;
            }
            
        }
        else {
            child.transform.localScale = new Vector3(0,0,0);
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
