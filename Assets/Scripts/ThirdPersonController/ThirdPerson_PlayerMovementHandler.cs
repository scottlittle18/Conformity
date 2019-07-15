using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the movement of the player based on the data received from the InputListener
/// </summary>
public class ThirdPerson_PlayerMovementHandler : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField, Tooltip("How fast the player will turn to look in the movement direction")]
    private float turningSpeed;

    [SerializeField, Tooltip("The max distance that the Ground Detector will look.")]
    private float groundCheckRadius;

    private Vector3 lastMoveDirection;
    private Vector3 appliedMoveDirection;
    private LayerMask playerLayerMask;
    private Transform groundCheckPosition;
    private Rigidbody playerRigidBody;

    private bool isGrounded;
    private bool hasNotJumped;
    public bool IsGrounded
    {
        get { return isGrounded; }
        private set
        {
            isGrounded = value;
        }
    }

    public bool HasNotJumped
    {
        get
        {
            if (IsGrounded == true)
            {
                return hasNotJumped = true;
            }
            else if (IsGrounded == false)
            {
                return hasNotJumped = false;
            }
            else
                return hasNotJumped;
        }
        set { hasNotJumped = value; }
    }

    private void Awake()
    {
        playerLayerMask = LayerMask.GetMask("Player");
        groundCheckPosition = GameObject.Find("GroundCheck").transform;
        playerRigidBody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    private void Update()
    {
        DetermineLookDirection();

        Debug.Log($"isGrounded == {isGrounded}");
        Debug.Log($"IsGrounded == {IsGrounded}");
        CheckIfOnPlayerIsGrounded();
    }

    private void CheckIfOnPlayerIsGrounded()
    {
        //Detect if the Player is on the ground
        
        IsGrounded = Physics.CheckSphere(groundCheckPosition.position, groundCheckRadius, playerLayerMask);

        //TODO: Debug
    }

    public void Jump()
    {
        playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void DetermineLookDirection()
    {
        //Determine look direction based on the current move direction and store that direction as the last direction the player moved in
        Quaternion lookDirection = Quaternion.LookRotation(-lastMoveDirection, Vector3.up);

        //Apply rotation
        transform.rotation = lookDirection;
    }

    public void MovePlayer(Vector3 moveDirection, Space camSpace)
    {
        //Store the last move direction for use in the DetermneLookDirection()
        if (moveDirection != Vector3.zero)
        {
            lastMoveDirection = moveDirection;
        }

        // Create movement vector based on received movement input
        appliedMoveDirection = moveDirection * moveSpeed * Time.deltaTime;

        // Translate applied movement to world space && Apply Movement
        transform.Translate(-appliedMoveDirection, Space.World);
    }
}
