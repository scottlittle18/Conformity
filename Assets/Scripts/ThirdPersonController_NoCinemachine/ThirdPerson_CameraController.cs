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
    [SerializeField, Tooltip("This is the highest the camera will go around the player." +
        " (This also keeps the camera from looking straight down at the player)")]
    private float yAxisTop;

    [SerializeField, Tooltip("This is the lowest the camera will go around the player." +
        " (This also keeps the camera from looking straight up at the player)")]
    private float yAxisBottom;

    [SerializeField, Tooltip("Controls how fast the camera will rotate around the player when looking around.")]
    private float lookSensitivity;
    #endregion

    private float lookInputX, lookInputY;

    [SerializeField]
    private Transform lookTarget;

    [SerializeField]
    private Transform playerTransform;

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

    // Start is called before the first frame update
    private void Start()
    {
        //Lock Cursor in place and make it Invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SetCameraLookLimits();
    }

    private void SetCameraLookLimits()
    {
        //In case values are not set then these are the default look settings
        if (yAxisTop == 0)
            yAxisTop = -35;
        if (yAxisBottom == 0)
            yAxisBottom = 60;
        if (lookSensitivity == 0)
            lookSensitivity = 1;
    }

    public void ListenForLookInput()
    {
        lookInputX += Input.GetAxisRaw("Mouse X") * lookSensitivity;
        lookInputY -= Input.GetAxisRaw("Mouse Y") * lookSensitivity;
        lookInputY = Mathf.Clamp(lookInputY, yAxisTop, yAxisBottom);

        CameraController();
    }

    private void CameraController()
    {
        transform.LookAt(lookTarget);

        lookTarget.position = playerTransform.position;
        lookTarget.rotation = Quaternion.Euler(lookInputY, lookInputX, 0);
    }
}
