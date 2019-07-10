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
    private Vector3 appliedTurningDirection;
    private Rigidbody playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        DetermineLookDirection();

        //TODO: Debugging Camera and Player movement
        Debug.DrawRay(transform.position, transform.forward, Color.cyan); // Show Player's forward direction
    }

    private void DetermineLookDirection()
    {
        Debug.Log("Determining Look Direction...");
        //Determine look direction based on the current move direction and store that direction as the last direction the player moved in

        //TODO: trying multiple methods of rotating the player

        //-----Attempt 1-----Using Quaternion.LookDirection
        //Vector3 inverseWorldUp;
        //inverseWorldUp = transform.InverseTransformDirection(Vector3.up);

            //-----Attempt 2----- Using Quaternion.Euler
        //Quaternion lookDirection = Quaternion.Euler(0, Mathf.Atan2(lastMoveDirection.x, lastMoveDirection.z), 0);

            //-----Attempt 3----- Using Quaternion.Slerp
        //Quaternion lookDirection = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-lastMoveDirection), turningSpeed);
        
        //Determine Look Direction
        Quaternion lookDirection = Quaternion.LookRotation(-lastMoveDirection, Vector3.up);
        lookDirection.eulerAngles = new Vector3(0, lookDirection.eulerAngles.y, 0);
        Debug.Log($"Y Vector of Rotation is: {lookDirection.eulerAngles.y}");
        //Apply rotation
        transform.rotation = lookDirection;

        Debug.Log("Determined Look Direction!!!");
    }

    public void MovePlayer(Vector3 moveDirection, Space camSpace)
    {
        //Store the last move direction
        if (moveDirection != Vector3.zero)
        {
            lastMoveDirection = moveDirection;
        }

        appliedMoveDirection = moveDirection * moveSpeed * Time.deltaTime;
        transform.Translate(-appliedMoveDirection, Space.World);
    }
}
