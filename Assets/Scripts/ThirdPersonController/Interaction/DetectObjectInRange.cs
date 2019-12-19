using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameObject closestInteractable;

    private float distanceToCurrentInteractiveTarget;

    private void Update()
    {
        distanceToCurrentInteractiveTarget = Vector3.Distance(this.transform.position, interactablesInRange[0].transform.position);

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
}
