using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Movement Variables
    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private float moveMultiplier = 0.01f;
    private Vector2 moveVector;
    [SerializeField] Rigidbody2D rb;


    // Input
    private Input input;
    private InputAction movement;
    //private bool lockMovement = false;

    // Animation
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        input = new Input();
        movement = input.Movement.Move;
        
    }

    //public void LockMovement()
    //{
    //    lockMovement = true;
    //}

    //public void UnlockMovement()
    //{
    //    lockMovement = false;
    //}

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //if (lockMovement)
        //{
        //    moveVector = new Vector2(0f, 0f);
        //} 
        //else
        //{
            moveVector = movement.ReadValue<Vector2>();
        //}

        if (moveVector.x != 0 || moveVector.y != 0)
        {
            animator.SetFloat("x", 1.0f);
            animator.SetFloat("y", moveVector.y);
            animator.SetFloat("speed", 1.0f);
        } 
        else
        {
            animator.SetFloat("speed", 0.0f);
        }
        if (moveVector.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(moveVector.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        //Debug.Log("moveVector: " + moveVector);
        moveVector = moveVector.normalized * moveSpeed * moveMultiplier;
        rb.velocity = moveVector;
    }
    public void OnEnable()
    {
        movement.Enable();
    }
    public void OnDisable()
    {
        movement.Disable();
    }
}
