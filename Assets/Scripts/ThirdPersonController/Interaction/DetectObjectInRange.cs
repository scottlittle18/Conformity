using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Purpose:
///     This script is used to detect which objects are within the InteractableObjectDetector
///   and make the closest interactable the active target
///   
/// </summary>
public class DetectObjectInRange : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField]
    [Tooltip ("This is the Capsule Collider that will " +
        "act as a detection zone for any interactable objects within range")]
    private CapsuleCollider detectionZone;
    #endregion

    // Used to store the interactable objects that are inside the detector
    private List<GameObject> interactablesInRange = new List<GameObject>();

    // Used to store the closest interactable
    private GameObject closestInteractable;

    // Stores the distance between the player and the current target interactable
    private float distanceToCurrentInteractiveTarget;

    private void Update()
    {
        // Finds the distance between the player and the Interable target
        distanceToCurrentInteractiveTarget = Vector3.Distance(this.transform.position, interactablesInRange[0].transform.position);

        // Iterates through the interactablesInRange[] to check if any are closer than the current target
        foreach (GameObject interactable in interactablesInRange)
        {
            // Sets the closest interactable object to be the active one (meaning the one the player would interact with upon hitting the interaction button
            if (Vector3.Distance(this.transform.position, interactable.transform.position) < distanceToCurrentInteractiveTarget)
            {
                closestInteractable = interactable;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Interactable")
        {
            interactablesInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Interactable")
        {
            interactablesInRange.Remove(collision.gameObject);
        }
    }
}
