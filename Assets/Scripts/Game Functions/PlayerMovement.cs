using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Movement Variables
    [Header("Movement Variables")]
    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private int tempSpeed;
    [SerializeField] private float moveMultiplier = 0.01f;
    private Vector2 moveVector;
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
    //private bool lockMovement = false;

    // Animation
    [Header("Animation References")]
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        input = new Input();
        movement = input.Movement.Move;
        tempSpeed = moveSpeed;
        //Physics2D.SetLayerCollisionMask()
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
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        
        moveVector = movement.ReadValue<Vector2>();

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
        if (moveVector != Vector2.zero)
        {
            turnVector = moveVector;
        }
        CheckGround();

        moveVector = moveVector.normalized * moveSpeed * moveMultiplier;
        
        rb.velocity = moveVector;
    }


    private void CheckGround()
    {

        Vector3 origin = transform.position + new Vector3(bc.offset.x, bc.offset.y);
        origin += new Vector3(turnVector.normalized.x, turnVector.normalized.y, -0.5f);
        hit = Physics2D.GetRayIntersection(
            new Ray(origin, new Vector3(0f, 0f, 1.0f)),
            //new Ray(new Vector3(bc.offset.x, bc.offset.y, 0f)
            //    //bc.transform.position 
            //    + new Vector3(turnVector.normalized.x, turnVector.normalized.y, -0.5f), new Vector3(0f, 0f, 1.0f)),
            Mathf.Infinity,
            groundLayer
            ); ;
        //Debug.Log(hit.collider);
        if (!hit)
        {
            Debug.DrawRay(origin,
                //new Vector3(bc.offset.x, bc.offset.y, 0f)
                //   //bc.transform.position 
                //   + new Vector3(turnVector.normalized.x, turnVector.normalized.y, -0.5f)
                new Vector3(0f, 0f, 1.0f), Color.red, 0.5f);
            moveSpeed = 0;
            //groundDetected = false;
        } else
        {
            Debug.DrawRay(origin,
                 //new Vector3(bc.offset.x, bc.offset.y, 0f)
                 ////bc.transform.position 
                 //+ new Vector3(turnVector.normalized.x, turnVector.normalized.y, -0.5f)
                 new Vector3(0f, 0f, 1.0f), Color.green, 0.5f);
            moveSpeed = tempSpeed;
            //groundDetected = true;
        }
        //}
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
