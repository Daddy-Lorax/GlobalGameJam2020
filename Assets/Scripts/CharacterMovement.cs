using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private int HASH_POS_X = Animator.StringToHash("PosX");
    private int HASH_POS_Y = Animator.StringToHash("PosY");

    public float playerSpeed = 4f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        GetComponent<Rigidbody2D>().velocity=targetVelocity * playerSpeed;

        animator.SetFloat(HASH_POS_X, targetVelocity.x);
        animator.SetFloat(HASH_POS_Y, targetVelocity.y);
    }
}
