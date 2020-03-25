using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class _UnityEventString : UnityEvent<string> { }

public class DoorTrigger : MonoBehaviour
{

    public enum Direction { UP, DOWN, LEFT, RIGHT   }
    public static Direction opposite(Direction o) {
        switch (o)
        {
            case Direction.UP: return Direction.DOWN;
            case Direction.DOWN: return Direction.UP;
            case Direction.LEFT: return Direction.RIGHT;
            case Direction.RIGHT: return Direction.LEFT;
        }
        throw new System.Exception("Invalid Direction value.");
    }


    public Direction exitDirection;

    public _UnityEventString onEnter;

    BoxCollider2D box;

    public float border = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Move" + exitDirection.ToString());
        //onEnter.Invoke(exitDirection.ToString());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Determine which side we left from
        //if()
        //Debug.Log(collision.gameObject.name);
        Vector2 dists = box.bounds.extents;

        Vector2 exitPt = collision.bounds.center - transform.position;

        if (exitPt.x > dists.x-border)
        {
            exitDirection = Direction.RIGHT;
        }
        else if (exitPt.x <-dists.x+border)
        {
            exitDirection = Direction.LEFT;
        }
        else if (exitPt.y > dists.y-border)
        {
            exitDirection = Direction.UP;
        }
        else if (exitPt.y < -dists.y+border)
        {
            exitDirection = Direction.DOWN;
        }
        //Debug.Log(exitPt+", "+dists+"\n"+exitDirection);
        onEnter.Invoke(exitDirection.ToString());
    }
}
