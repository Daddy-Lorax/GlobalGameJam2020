using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownDepth : MonoBehaviour
{
    //[SerializeField]
    private int sortOrderOrigin = 8192;
    private Renderer myRenderer;
    //[SerializeField]
    private static float positionScaling = 32f;
    [SerializeField]
    private float offset = 0;

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        myRenderer.sortingOrder = Mathf.FloorToInt(sortOrderOrigin - (transform.position.y * positionScaling - offset));
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 orig = (transform.position - Vector3.up * offset / positionScaling) ;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(orig + Vector3.left, orig + Vector3.right);
    }
}
