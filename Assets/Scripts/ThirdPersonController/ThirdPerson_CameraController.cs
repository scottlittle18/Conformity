using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles Camera movement to change look direction via Mouse Input
/// </summary>
public class ThirdPerson_CameraController : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField, Tooltip("This is transform of the object that will be the camera's focal point.")]
    private Transform cameraTarget;

    [SerializeField]
    private Transform playerTransform;


    [SerializeField, Tooltip("Controls how fast the camera will rotate around the player when looking around.")]
    private float lookSensitivity;
    #endregion

    #region Classes & Associated Declarations
    [Serializable]
    public class AxisSettings
    {
        [Tooltip("This is the highest the camera will go around the player." +
        " (This also keeps the camera from looking straight down at the player)")]
        public float yAxisTop;

        [Tooltip("This is the lowest the camera will go around the player." +
            " (This also keeps the camera from looking straight up at the player)")]
        public float yAxisBottom;

        [Tooltip("The Y Axis (Up/Down) offset of the camera from the player.")]
        public float yAxisOffset;

        [Tooltip("The X Axis (Left/Right) offset of the camera from the player.")]
        public float xAxisOffset;        
    }

    [SerializeField]
    private AxisSettings axisSettings = new AxisSettings();
    #endregion

    private float lookInputX, lookInputY;
    private Space mainCameraSpace;

    //Meant to be Read-Only
    public Space MainCameraSpace
    {
        get
        {
            mainCameraSpace = Space.Self;
            return mainCameraSpace;
        }
    }

    #region MonoBehaviour Functions
    // Start is called before the first frame update
    private void Start()
    {
        //Lock Cursor in place and make it Invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        SetCameraLookLimits();
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void LateUpdate()
    {
        ListenForLookInput();
    }
    #endregion

    private void FollowPlayer()
    {
        //TODO: Add code for DeadZone Here
    }

    private void SetCameraLookLimits()
    {        
        //In case values are not set in the editor then these are the default look settings
        if (axisSettings.yAxisTop == 0)
            axisSettings.yAxisTop = -35;
        if (axisSettings.yAxisBottom == 0)
            axisSettings.yAxisBottom = 60;
        if (lookSensitivity == 0)
            lookSensitivity = 1;
    }

    private void ListenForLookInput()
    {
        //Retrieve Look Input
        lookInputX += Input.GetAxisRaw("Mouse X") * lookSensitivity;
        lookInputY -= Input.GetAxisRaw("Mouse Y") * lookSensitivity;

        //Clamps the look rotation to keep it from going too high or low
        lookInputY = Mathf.Clamp(lookInputY, axisSettings.yAxisTop, axisSettings.yAxisBottom);
        
        CameraController();
    }

    private void CameraController()
    {
        transform.LookAt(cameraTarget);

        // Follow the player's position and offset the camera as needed (or wanted)
        cameraTarget.position = playerTransform.position + new Vector3(axisSettings.xAxisOffset, axisSettings.yAxisOffset, 0);

        // Rotate based on player's look input
        cameraTarget.rotation = Quaternion.Euler(lookInputY, lookInputX, 0);
    }
}
