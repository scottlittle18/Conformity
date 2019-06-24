using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    [SerializeField]
    private float maxMovementSpeed;

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float verticalInput = Input.GetAxisRaw("Horizontal");
        float horizontalInput = Input.GetAxisRaw("Vertical");
        Vector3 playerMovement = new Vector3(horizontalInput, 0f, verticalInput) * maxMovementSpeed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }
}
