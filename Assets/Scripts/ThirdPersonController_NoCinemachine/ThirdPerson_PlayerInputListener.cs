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
    
    //Special Fields (e.g. Vector3, RigidBody, etc.)
    private Vector3 intentedMoveDirection;
    private Vector3 camForward, camRight;
    private Camera mainCam;
    private ThirdPerson_CameraController camController;
    private ThirdPerson_PlayerMovementHandler playerMovementHandler;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        camController = Camera.main.GetComponent<ThirdPerson_CameraController>();
        playerMovementHandler = GetComponent<ThirdPerson_PlayerMovementHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        ListenForMoveInput();
    }

    private void LateUpdate()
    {
        camController.ListenForLookInput();
    }

    private void ListenForMoveInput()
    {
        verticalMoveInput = Input.GetAxisRaw("Vertical");
        horizontalMoveInput = Input.GetAxisRaw("Horizontal");

        camForward = mainCam.transform.forward;
        camRight = mainCam.transform.right;

        //Calculate Move Input Based On Camera's Relative Position
        intentedMoveDirection = verticalMoveInput * camForward + horizontalMoveInput * camRight;
        
        playerMovementHandler.MovePlayer(intentedMoveDirection, camController.MainCameraSpace);
    }
}
