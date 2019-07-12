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

    [SerializeField, Tooltip("How fast the player will turn to look in the movement direction")]
    private float turningSpeed;

    [SerializeField, Tooltip("The max distance that the Ground Detector will look.")]
    private float groundCheckRadius;

    private Vector3 lastMoveDirection;
    private Vector3 appliedMoveDirection;
    private LayerMask groundLayerMask;

    private bool isGrounded;
    public bool IsGrounded
    {
        get { return isGrounded; }
        private set { isGrounded = value; }
    }

    private void Awake()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
    }
    // Update is called once per frame
    private void Update()
    {
        DetermineLookDirection();

        CheckIfOnPlayerIsGrounded();
    }

    private void CheckIfOnPlayerIsGrounded()
    {
        //Detect if the Player is on the ground

        isGrounded = Physics.CheckSphere(-transform.up, groundCheckRadius, groundLayerMask);

        //TODO: Debug
        Debug.Log($"isGrounded == {isGrounded}");
        Debug.Log($"IsGrounded == {IsGrounded}");
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
