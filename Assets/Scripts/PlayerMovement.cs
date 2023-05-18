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

    // Animation
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        input = new Input();
        movement = input.Movement.Move;
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        moveVector = movement.ReadValue<Vector2>();
        if (moveVector.x != 0 || moveVector.y != 0)
        {
            // do stuff for animations?
            animator.SetInteger("x", (int)moveVector.x);
            animator.SetInteger("y", (int)moveVector.y);
            spriteRenderer.flipX = moveVector.x > 0 ? true : false;
        }
        moveVector = moveVector.normalized * moveSpeed * moveMultiplier;
        rb.velocity = moveVector;
        //transform.Translate(new Vector3(moveVector.x, moveVector.y, 0f), Space.World);
        //Debug.Log("moveVector: " + moveVector);
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
