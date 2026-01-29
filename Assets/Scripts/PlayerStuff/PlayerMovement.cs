using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private PlayerPhysics playerPhysics;

    private Vector2 currentInput;

    [Tooltip("The player's speed")]
    [SerializeField] private float speed;

    [Tooltip("Applied gravity per fixed frame")]
    [SerializeField] private float gravityStrength;
    
    [FormerlySerializedAs("dragFactor")]
    [Tooltip("The amount of the squared velocity that is subtracted per fixed frame")]
    [SerializeField, Range(0f,1f)] private float dragCoefficient;
    
    [Tooltip("The fraction that the speed will move to the goal amount per fixed frame")]
    [SerializeField, Range(0f,1f)] private float accelerationFactor;
    [Tooltip("The fraction that the speed will slow per frame if an input is NOT being held")]
    [SerializeField, Range(0f,1f)] private float decelerationFactor;
    
    [Tooltip("The jump velocity applied when jumping, applied instantly")]
    [SerializeField] private float jumpVelocity;

    private bool jumpInputHeld;

    [Tooltip("The maximum jump height achievable")]
    [SerializeField] private float maxJumpHeight;

    public bool onGround
    {
        get;
        private set;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (currentInput.x == 0)
        {
            playerRB.linearVelocityX = Mathf.MoveTowards(playerRB.linearVelocityX, 0, decelerationFactor);
        }
        else
        {
            playerRB.linearVelocityX = Mathf.MoveTowards(playerRB.linearVelocityX, currentInput.x*speed, accelerationFactor);
        }
        
        // Apply gravity
        playerRB.linearVelocityY -= gravityStrength * Time.fixedDeltaTime;
        
        // Apply drag
        playerRB.linearVelocity = playerRB.linearVelocity.normalized * (playerRB.linearVelocity.magnitude - (playerRB.linearVelocity.sqrMagnitude * dragCoefficient));

        if (jumpInputHeld)
        {
            UnityEngine.Debug.Log("Send zucchini");
            playerRB.linearVelocityY = MathF.Min(playerRB.linearVelocityY + jumpVelocity, maxJumpHeight);
            if (playerRB.linearVelocityY == maxJumpHeight) jumpInputHeld = false;
        }
    }

    public void OnPlayerMovement(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        
        currentInput = input;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && playerPhysics.OnGround)
        {
            jumpInputHeld = context.performed;
        }
        if (!playerPhysics.OnGround && context.canceled)
        {
            jumpInputHeld = false;
        }
    }
}
