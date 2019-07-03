using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("The maximum speed that the player can move at.")]
    private float maxMoveSpeed;

    [SerializeField, Tooltip("The maximum speed that the player will rotate at when changing direction.")]
    private float maxRotationSpeed;

    private Camera mainCamera;
    private Vector3 camForward, camRight;
    private Vector3 desiredMoveDirection;

    private Vector3 appliedMovement;

    private Rigidbody playerRigidBody;

    private float vInput, hInput;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        //Gather Player Components
        playerRigidBody = GetComponent<Rigidbody>();

        //Gather Camera Components
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        //playerRigidBody.AddForce(desiredMoveDirection * maxMoveSpeed, ForceMode.Force);
        //transform.Translate(-appliedMovement, mainCamera.transform);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), maxRotationSpeed);
        //transform.rotation = Quaternion.LookRotation(desiredMoveDirection, Vector3.up);
        //transform.Rotate(new Vector3(hInput, transform.rotation.y, vInput), Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
    }

    private void GetMovementInput()
    {
        //Receive Player Input
        vInput = Input.GetAxisRaw("Vertical");
        hInput = Input.GetAxisRaw("Horizontal");
        
        //Set and Normalize the Camera's Forward facing orientation
        camForward = mainCamera.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        //Set and Normalize the Camera's Right facing orientation
        camRight = mainCamera.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        //NOTE: For some reason the player will only move in the correct direction if the axis' are flipped. (Vertical Input == Left/Right movement && Horizontal == Forward/Backward movement
        desiredMoveDirection = camForward * hInput + camRight * vInput;
        //desiredMoveDirection = transform.InverseTransformDirection(desiredMoveDirection);
        //desiredMoveDirection = Vector3.ProjectOnPlane(desiredMoveDirection, Vector3.up);
        appliedMovement = desiredMoveDirection * maxMoveSpeed * Time.deltaTime;
        transform.Translate(-appliedMovement, mainCamera.transform);

        //TODO: Debugging Camera and Player movement
        Debug.DrawRay(transform.position, camForward, Color.magenta); // Show Camera's forward direction
        Debug.DrawRay(transform.position, transform.forward, Color.cyan); // Show Player's forward direction
    }
}
