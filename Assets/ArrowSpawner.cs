using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using static UnityEngine.GraphicsBuffer;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject notch;

    XRGrabInteractable _bow;
    bool _isArrowNotched = false;
    GameObject _currentArrow = null;

    // Called before the first frame update
    void Start()
    {
        _bow = GetComponent<XRGrabInteractable>();
        // Subscribe to the PullActionReleased event to handle arrow notch state
        PullInteraction.PullActionReleased += NotchEmpty;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        PullInteraction.PullActionReleased -= NotchEmpty;
    }

    // Called once per frame
    void Update()
    {
        // If the bow is selected and no arrow is notched, spawn a new arrow after a delay
        if (_bow.isSelected && !_isArrowNotched)
        {
            _isArrowNotched = true;
            StartCoroutine("DelayedSpawn");
        }
        // If the bow is not selected and an arrow exists, deactivate the arrow and reset notch state
        if (!_bow.isSelected && _currentArrow != null)
        {
            _currentArrow.SetActive(false);
            // Optionally destroy the arrow instead of deactivating
            //Destroy(_currentArrow);
            NotchEmpty(1f);
        }
    }

    // Resets the arrow notch state and reference
    private void NotchEmpty(float value)
    {
        _isArrowNotched = false;
        _currentArrow = null;
    }

    // Spawns a new arrow at the notch position after a delay
    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(1f);
        _currentArrow = Instantiate(arrow, notch.transform);
    }
}
