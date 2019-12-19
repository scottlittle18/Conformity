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
///     2. Set the DisplayText field in the Editor.
///     3. Use and Interact!
///     
/// </summary>
public class InteractableObject : IInteractable
{
    public string DisplayText {get; private set; }

    /// <summary>
    /// Called when an InteractableObject is interacted with by the Player.
    /// </summary>
    public void InteractWithObject()
    {
        Debug.Log($"Player has interacted with {DisplayText}!");
    }
}
