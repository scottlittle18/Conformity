using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson_CameraCollisionHandler : MonoBehaviour
{
    [SerializeField, Tooltip("This is the closest to the player that the camera will ever get.")]
    private float minDistance;

    [SerializeField, Tooltip("This is the furthest from the player that the camera will ever get. (This is also the camera's default position.)")]
    private float maxDistance;

    [SerializeField, Tooltip("Adjusts the speed at which the camera will move towards and away from the player.")]
    private float smooth;

    [SerializeField, Tooltip("Changes the distance of the raycast to help keep the camera from clipping through things such as floors and walls. (Values between 0-1 only since it changes the raycast based on this percentage value.)")]
    private float raycastDistanceModifier;

    private Vector3 camDirection;
    private float currentDistanceFromCamTarget;

    // Start is called before the first frame update
    private void Awake()
    {
        camDirection = transform.localPosition.normalized;
        currentDistanceFromCamTarget = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 desiredCameraPosition = transform.parent.TransformPoint(camDirection * maxDistance);
        RaycastHit cameraLineOfSight;

        if (Physics.Linecast(transform.parent.position, desiredCameraPosition, out cameraLineOfSight))
        {
            currentDistanceFromCamTarget = Mathf.Clamp((cameraLineOfSight.distance * raycastDistanceModifier), minDistance, maxDistance);
        }
        else
        {
            currentDistanceFromCamTarget = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, camDirection * currentDistanceFromCamTarget, Time.deltaTime * smooth);
    }
}
