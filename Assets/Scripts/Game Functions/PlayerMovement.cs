using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Movement Variables
    [Header("Movement Variables")]
    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private int tempSpeed;
    [SerializeField] private float moveMultiplier = 0.01f;
    [SerializeField] private Vector2 moveVector;
    private Vector2 turnVector;
    //[SerializeField] private bool groundDetected;
    [SerializeField] private LayerMask groundLayer;
    //private RaycastHit hit;
    private RaycastHit2D hit;

    [Header("Component References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] BoxCollider2D bc;


    // Input
    private Input input;
    private InputAction movement;
    private InputAction pauseMenu;
    private bool lockMovement = false;

    [Header("Pause Menu")]
    [SerializeField] GameObject pauseMenuObject;

    // Animation
    [Header("Animation References")]
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        input = new Input();
        movement = input.Movement.Move;
        pauseMenu = input.Movement.PauseMenu;
        tempSpeed = moveSpeed;
        //Physics2D.SetLayerCollisionMask()
    }

    public void LockMovement()
    {
        moveVector = Vector2.zero;
        rb.velocity = Vector2.zero;
        lockMovement = true;
    }

    public void UnlockMovement()
    {
        lockMovement = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // every frame, call Move() to get player input and move the player
        Move();
        // if the player presses the pauseMenu button
        if (pauseMenu.IsPressed())
        {
            //print("Pause button pressed");
            // Enable the pauseMenu UI and freeze the game
            pauseMenuObject.SetActive(true);
            LockMovement();
            //Time.timeScale = 0;
        }
    }

    private void Move()
    {
        //if (lockMovement)
        //{
        //    return;
        //}
        // read input from the player as a 2D vector
        moveVector = movement.ReadValue<Vector2>();

        //adjust animation values based on player's input
        if (moveVector.x != 0 || moveVector.y != 0)
        {
            // if the player is moving, set the animation floats respectively
            animator.SetFloat("x", 1.0f);
            animator.SetFloat("y", moveVector.y);
            animator.SetFloat("speed", 1.0f);
        } 
        else
        {
            animator.SetFloat("speed", 0.0f);
        }
        // if the player moves left/right, flip the sprite accordingly
        if (moveVector.x > 0)
        {
            
            spriteRenderer.flipX = false;
        }
        else if(moveVector.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        //Debug.Log("moveVector: " + moveVector);

        // if the player is moving, set the turn vector, used for detecting valid ground in CheckGround()
        if (moveVector != Vector2.zero)
        {
            turnVector = moveVector;
        }
        // Check if the player is able to move forward
        CheckGround();
        
        if (lockMovement)
        {
            return;
        }
        moveVector = moveVector.normalized * moveSpeed * moveMultiplier;
        
        rb.velocity = moveVector;
    }


    private void CheckGround()
    {


        //  Vector3 origin =    transform.position
        //                  + new Vector3(bc.offset.x, bc.offset.y)
        //                  + new Vector3(turnVector.normalized.x, turnVector.normalized.y, -0.5f);
        // player's position                                = transform.position
        // offset of the player's boxCollider (at the feet) = new Vector3(bc.offset.x, bc.offset.y)
        // offset in front of where the player is facing    = new Vector3(turnVector.normalized.x, turnVector.normalized.y, -0.5f);


        hit = Physics2D.GetRayIntersection(
            new Ray(transform.position + new Vector3(bc.offset.x, bc.offset.y) + new Vector3(turnVector.normalized.x, turnVector.normalized.y, -0.5f),
            new Vector3(0f, 0f, 1.0f)),
            Mathf.Infinity,
            groundLayer
            ); ;
        //Debug.Log(hit.collider);
        if (!hit)
        {
            //Debug.DrawRay(transform.position + new Vector3(bc.offset.x, bc.offset.y) + new Vector3(turnVector.normalized.x, turnVector.normalized.y, -0.5f), new Vector3(0f, 0f, 1.0f), Color.red, 0.5f);
            //groundDetected = false;
            
            // if they detect something that isn't ground, then don't let the player move
            moveSpeed = 0;
        } else
        {
            //Debug.DrawRay(transform.position + new Vector3(bc.offset.x, bc.offset.y) + new Vector3(turnVector.normalized.x, turnVector.normalized.y, -0.5f), new Vector3(0f, 0f, 1.0f), Color.green, 0.5f);
            //groundDetected = true;
         
            // if they detect something that is ground, then let the player move
            moveSpeed = tempSpeed;
        }
        //}
    }

    // used for Unity's InputSystem
    public void OnEnable()
    {
        movement.Enable();
        pauseMenu.Enable();
    }
    public void OnDisable()
    {
        movement.Disable();
        pauseMenu.Disable();
    }
}
