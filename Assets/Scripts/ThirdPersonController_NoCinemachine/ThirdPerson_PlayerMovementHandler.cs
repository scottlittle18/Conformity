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
    private Vector3 appliedMoveDirection;
    private Rigidbody playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePlayer(Vector3 moveDirection, Space camSpace)
    {
        appliedMoveDirection = moveDirection * moveSpeed * Time.deltaTime;
        transform.Translate(-appliedMoveDirection, camSpace);
        transform.rotation = Quaternion.Euler(moveDirection.y, moveDirection.x, 0);
    }
}
