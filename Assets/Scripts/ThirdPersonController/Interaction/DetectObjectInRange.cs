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
}
