using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Feedback;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PullInteraction : XRBaseInteractable
{
    // Event triggered when the pull action is released, passing the pull amount
    public static event Action<float> PullActionReleased;

    public Transform start, end;
    public GameObject notch;
    // Current amount the string is pulled (0 to 1)
    public float pullAmount { get; private set; } = 0.0f;

    private LineRenderer _lineRenderer;
    private IXRSelectInteractor pullingInteractor = null;

    // Called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Sets the interactor that is currently pulling the string
    public void SetPullInteractor(SelectEnterEventArgs args)
    {
        pullingInteractor = args.interactorObject;
    }

    // Called when the string is released
    public void Release()
    {
        PullActionReleased?.Invoke(pullAmount);
        pullingInteractor = null;
        pullAmount = 0f;
        // Reset notch position after release
        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, 0f);
        UpdateString();
    }

    // Handles the interactable's update logic each frame
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Vector3 pullPosition = pullingInteractor.transform.position;
                pullAmount = CalculatePull(pullPosition);
                UpdateString();

                // Optionally send haptic feedback when pulling
                //HapticFeedback();
            }
        }
    }

    // Calculates how far the string is pulled based on the interactor's position
    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
        return Mathf.Clamp(pullValue, 0, 1);
    }

    // Updates the string and notch position visually based on pull amount
    private void UpdateString()
    {
        Vector3 linePosition = Vector3.forward * Mathf.Lerp(start.transform.localPosition.z, end.transform.localPosition.z, pullAmount);
        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, linePosition.z + .2f);
        _lineRenderer.SetPosition(1, linePosition);
    }

    // Sends haptic feedback to the controller when pulling the string
    //void HapticFeedback()
    //{
    //    if(pullingInteractor != null)
    //    {
    //        ActionBasedController currentController = pullingInteractor.transform.gameObject.GetComponent<ActionBasedController>();
    //        currentController.SendHapticImpulse(pullAmount, .1f);
    //    }
    //}
}