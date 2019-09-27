using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    // Name of Object to be displayed or Action to be taken
    string DisplayText { get; }

    // Interact with the Object in range
    void InteractWithObject();
}
