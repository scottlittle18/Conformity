using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    [SerializeField]
    private float maxMovementSpeed;

    private Rigidbody playerRigidBody;

    private Transform mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerMovement();
        //PlayerRotation();
    }

    private void PlayerRotation()
    {
        Quaternion playerDirection = playerRigidBody.transform.localRotation;
            playerDirection = mainCamera.localRotation;
    }

    private void PlayerMovement()
    {
        float verticalInput = Input.GetAxisRaw("Horizontal");
        float horizontalInput = Input.GetAxisRaw("Vertical");
        Vector3 playerMovement = new Vector3(horizontalInput, 0f, verticalInput) * maxMovementSpeed * Time.deltaTime;
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
        transform.Translate(-playerMovement, Space.World);
        Quaternion targetRotationDirection = Quaternion.LookRotation(movementDirection, Vector3.up);
        if (Vector3.SqrMagnitude(movementDirection) > 0)
        {
            transform.rotation = targetRotationDirection;
        }
    }
}
