using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Listens for the Player input and sends that info to both the 
/// CameraController and PlayerMovementHandler scripts
/// </summary>
public class ThirdPerson_PlayerInputListener : MonoBehaviour
{
    

    #region Non-Serialized Fields
    //Input Fields
    private float verticalMoveInput, horizontalMoveInput;
    private bool jumpInput = false;

    
    //Special Fields (e.g. Vector3, RigidBody, etc.)
    private Vector3 intentedMoveDirection;
    private Vector3 camForward, camRight;
    private Camera mainCam;
    private ThirdPerson_CameraController camController;
    private ThirdPerson_PlayerMovementHandler playerMovementHandler;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        mainCam = Camera.main;
        camController = GetComponent<ThirdPerson_CameraController>();
        playerMovementHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPerson_PlayerMovementHandler>();
    }

    // Update is called once per frame
    private void Update()
    {
        ListenForMoveInput();

        //Allow Player to Jump if they are on the Ground
        if (playerMovementHandler.IsGrounded && playerMovementHandler.HasNotJumped)
        {
            ListenForJumpInput();
        }
    }
    
    private void ListenForJumpInput()
    {
        jumpInput = Input.GetButtonDown("Jump");

        //TODO: Debug
        Debug.Log($"Jump Input Received; JumpInput == {jumpInput}");

        if (jumpInput && playerMovementHandler.IsGrounded && playerMovementHandler.HasNotJumped)
        {
            playerMovementHandler.Jump();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            playerMovementHandler.HasNotJumped = false;
        }
    }

    private void ListenForMoveInput()
    {
        //Retrieve Move Input
        verticalMoveInput = Input.GetAxisRaw("Vertical");
        horizontalMoveInput = Input.GetAxisRaw("Horizontal");

        // Get the MainCamera's forward and rightward directions
        camForward = mainCam.transform.forward;
        camForward.y = 0f;

        camRight = mainCam.transform.right;
        camRight.y = 0f;

        //Calculate Move Input Based On Camera's Relative Position
        intentedMoveDirection = (camForward.normalized * verticalMoveInput + camRight.normalized * horizontalMoveInput);

        playerMovementHandler.MovePlayer(intentedMoveDirection, camController.MainCameraSpace);
    }
}
