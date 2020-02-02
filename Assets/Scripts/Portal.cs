using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal nextPortal;

    private bool justEntered;

    private void Start()
    {
        justEntered = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!justEntered)
        {
            nextPortal.ReceivePlayer(other);
        }
    }

    public void ReceivePlayer(Collider2D other)
    {
        justEntered = true;
        other.transform.position = this.transform.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        justEntered = false;
    }
}
