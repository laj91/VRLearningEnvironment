using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Attachment;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class NearFarSwitcher : MonoBehaviour
{
    public InputActionReference aButtonReference;
    private XRGrabInteractable[] interactables;
    private bool isNearMode = false; // Start i Near Mode

    void Awake()
    {
        // Find alle XRGrabInteractables i scenen og gem dem i et array
        interactables = FindObjectsOfType<XRGrabInteractable>();
    }

    void OnEnable()
    {
        aButtonReference.action.performed += _ => ToggleFarAttachMode();
        aButtonReference.action.Enable();
    }

    void OnDisable()
    {
        aButtonReference.action.performed -= _ => ToggleFarAttachMode();
        aButtonReference.action.Disable();
    }

    private void ToggleFarAttachMode()
    {
        // Skift tilstand
        isNearMode = !isNearMode;

        // Sæt til den nye tilstand for alle interactables
        var mode = isNearMode ? InteractableFarAttachMode.Near : InteractableFarAttachMode.Far;
        foreach (var interactable in interactables)
        {
            interactable.farAttachMode = mode;
        }

        Debug.Log("Switched to " + (isNearMode ? "Near" : "Far") + " Mode");
    }
}
