using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Purpose:
///     This script is meant to be attached to objects that the Player can interact with.
///     
/// Implementation Procedure:
///     1. Attach to the object you want to make interactable.
///     2. Ensure the name of the GameObject is the typed in-editor the same way you'd like it displayed in-game.
///     3. Use and Interact!
///     
/// </summary>
public class InteractableObject : MonoBehaviour, IInteractable
{
    public string DisplayText {get; private set; }

    private void Awake()
    {
        // Set the DisplayText that appears to be the name of the object
        DisplayText = gameObject.name;
    }
    /// <summary>
    /// Called when an InteractableObject is interacted with by the Player.
    /// </summary>
    public void InteractWithObject()
    {
        Debug.Log($"Player has interacted with {DisplayText}!");
    }
}
