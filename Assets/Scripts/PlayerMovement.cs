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

    // Input
    private Input input;
    private InputAction movement;

    private void Awake()
    {
        input = new Input();
        movement = input.Movement.Move;
        
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = movement.ReadValue<Vector2>();
        if(moveVector.x != 0 || moveVector.y != 0)
        {
            // do stuff for animations?
        }
        moveVector = moveVector.normalized * moveSpeed * moveMultiplier;
        transform.Translate(new Vector3(moveVector.x, moveVector.y, 0f), Space.World);
        Debug.Log("moveVector: " + moveVector);

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
