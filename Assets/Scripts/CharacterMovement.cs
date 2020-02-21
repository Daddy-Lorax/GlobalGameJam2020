using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private int HASH_POS_X = Animator.StringToHash("VelX");
    private int HASH_POS_Y = Animator.StringToHash("VelY");
    private int HASH_IS_MOVING = Animator.StringToHash("IsMoving");

    public float SPEED_THRESHOLD = 0.01f;

    public float playerSpeed = 4f;
    public bool timeFreeze;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timeFreeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeFreeze)
        {
            Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            GetComponent<Rigidbody2D>().velocity = targetVelocity * playerSpeed;

            bool isMoving = Mathf.Abs(targetVelocity.x) > SPEED_THRESHOLD || Mathf.Abs(targetVelocity.y) > SPEED_THRESHOLD;
            //bool isMoving = targetVelocity != Vector2.zero;
            if (isMoving)
            {
                animator.SetFloat(HASH_POS_X, targetVelocity.x);
                animator.SetFloat(HASH_POS_Y, targetVelocity.y);
            }
            animator.SetBool(HASH_IS_MOVING, isMoving);
        }
    }
}
