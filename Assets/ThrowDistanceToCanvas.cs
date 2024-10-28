using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThrowDistanceToCanvas : MonoBehaviour
{
    private ThrowDistance currentObject; // Holder det nuværende objekt
    [SerializeField] private TextMeshProUGUI distanceText; // Reference til TMP-elementet

    public void SetCurrentObject(ThrowDistance obj)
    {
        currentObject = obj;
    }

    public void ClearCurrentObject()
    {
        currentObject = null;
        distanceText.text = ""; // Nulstil teksten
    }

    public void UpdateDistanceText(float distance)
    {
        if (currentObject != null)
        {
            distanceText.text = $"{distance:F2} meters";
        }
    }
}
