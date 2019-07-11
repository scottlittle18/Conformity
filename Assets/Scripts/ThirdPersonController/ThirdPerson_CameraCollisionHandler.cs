using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the distance between the mainCamera and it's parent. This causes the camera to move closer to the camTarget if 
///     something (like a wall) gets between the mainCamera and it's parent to keep from losing sight of the camTarget.
/// </summary>
public class ThirdPerson_CameraCollisionHandler : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField, Tooltip("This is the closest to the player that the camera will ever get.")]
    private float minDistance;

    [SerializeField, Tooltip("This is the furthest from the player that the camera will ever get. (This is also the camera's default position.)")]
    private float maxDistance;

    [SerializeField, Tooltip("Adjusts the speed at which the camera will move towards and away from the player.")]
    private float smooth;

    [SerializeField, Tooltip("Changes the distance of the raycast to help keep the camera from clipping through things such as floors and walls. (Values between 0-1 only since it changes the raycast based on this percentage value.)")]
    private float raycastDistanceModifier;
    #endregion

    private Vector3 camDirection;
    private float currentDistanceFromCamTarget;

    // Start is called before the first frame update
    private void Awake()
    {
        //Get camera's current position
        camDirection = transform.localPosition.normalized;
        currentDistanceFromCamTarget = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckCameraLineOfSight();
    }

    private void CheckCameraLineOfSight()
    {
        //Store the distance of the camera from the camTarget's local position
        Vector3 desiredCameraPosition = transform.parent.TransformPoint(camDirection * maxDistance);
        RaycastHit cameraLineOfSight;

        if (Physics.Linecast(transform.parent.position, desiredCameraPosition, out cameraLineOfSight))
        {
            //If an object causes the camera to break Line Of Sight with the camTarget, make the camera move closer to the camTarget to regain visual
            currentDistanceFromCamTarget = Mathf.Clamp((cameraLineOfSight.distance * raycastDistanceModifier), minDistance, maxDistance);
        }
        else
        {
            //Default camera distance from camTarget
            currentDistanceFromCamTarget = maxDistance;
        }

        //Apply changes to camera's distance from camTarget
        transform.localPosition = Vector3.Lerp(transform.localPosition, camDirection * currentDistanceFromCamTarget, Time.deltaTime * smooth);
    }
}
