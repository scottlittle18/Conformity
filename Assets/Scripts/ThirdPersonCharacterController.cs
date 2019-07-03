using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for the Basic Implementation of player input for a Third-Person Character Controller.
///     This specific script is being usedin conjunction with a Cinemachine Free Look Camera.
/// </summary>
public class ThirdPersonCharacterController : MonoBehaviour
{
    [SerializeField]
    private float maxMovementSpeed;

    [SerializeField]
    [Tooltip("This is the speed at which the character will turn when they change direction.")]
    private float rotationSpeed;

    #region Private Non-Serialized Fields
    //Unity Specific Standard Fields-------------//
    private Rigidbody playerRigidBody;
    private Camera mainCamera;
    private Quaternion currentLookDirection;
    private Vector3 camForward;
    private Vector3 desiredMovementDirection;

    //C# Standard Fields-------------------------//
    private float verticalInput;
    private float horizontalInput;
    private float turnAmount;
    private float forwardAmount;
    #endregion

    private void Start()
    {
        mainCamera = Camera.main;
        playerRigidBody = GetComponent<Rigidbody>();
        //currentLookDirection = Quaternion.LookRotation(transform.forward);
    }

    private void Update()
    {
        PlayerMovementAndRotation();
        //PlayerMovement();
        //PlayerRotation();
    }

    private void PlayerMovementAndRotation()
    {
        verticalInput = Input.GetAxisRaw("Horizontal");
        horizontalInput = Input.GetAxisRaw("Vertical");

        if (mainCamera != null)
        {
            // Calculate the camera's relative direction to move
            camForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
            desiredMovementDirection = camForward * verticalInput + mainCamera.transform.right * horizontalInput;
        }
        else
        {
            desiredMovementDirection = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
        }

        // convert the world relative moveInput vector into a local-relative
        // turn amount and forward amount required to head in the desired
        // direction.
        if (desiredMovementDirection.magnitude > 1f)
            desiredMovementDirection.Normalize();

        desiredMovementDirection = transform.InverseTransformDirection(desiredMovementDirection);
        //desiredMovementDirection = Vector3.ProjectOnPlane(desiredMovementDirection, Vector3.up);
        turnAmount = Mathf.Atan2(desiredMovementDirection.x, desiredMovementDirection.z);
        forwardAmount = desiredMovementDirection.z;
        playerRigidBody.velocity = new Vector3(desiredMovementDirection.x, 0f, desiredMovementDirection.z);
    }

    private void PlayerRotation()
    {
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
        Quaternion targetRotationDirection = Quaternion.LookRotation(movementDirection, Vector3.up);

        if (Vector3.SqrMagnitude(movementDirection) > 0)
        {
            transform.rotation = targetRotationDirection;
        }        
    }

    private void PlayerMovement()
    {
        verticalInput = Input.GetAxisRaw("Horizontal");
        horizontalInput = Input.GetAxisRaw("Vertical");

        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0.0f;
        right.y = 0.0f;

        forward.Normalize();
        right.Normalize();


        Vector3 desiredMovementDirection = forward * verticalInput + right * horizontalInput;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMovementDirection), rotationSpeed);
        Vector3 playerMovement = desiredMovementDirection * maxMovementSpeed * Time.deltaTime;
        //transform.Translate(-playerMovement, mainCamera.transform);
    }
}
