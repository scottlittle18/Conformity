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

    private Vector3 lastMoveDirection;
    private Vector3 appliedMoveDirection;

    // Update is called once per frame
    private void Update()
    {
        DetermineLookDirection();
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
