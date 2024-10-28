using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class ThrowDistance : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;
    private bool isThrown = false;
    private float throwDistance = 0f;

    // Reference til DisplayThrowDistance-scriptet
    public ThrowDistanceToCanvas displayThrowDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Object grabbed");
        // Nulstil kastedistancen, når objektet grebes
        if (isThrown)
        {
            displayThrowDistance.ClearCurrentObject();
            isThrown = false;
        }
    }

    public void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("Object released");
        // Registrer startpositionen når objektet slippes
        startPosition = transform.position;
        isThrown = true;

        // Angiv til DisplayThrowDistance, at dette objekt er valgt
        displayThrowDistance.SetCurrentObject(this);

        // Start Coroutine til at måle kastet
        StartCoroutine(MeasureThrowDistance());
    }

    private IEnumerator MeasureThrowDistance()
    {
        // Vent en lille smule for at give objektet tid til at accelerere
        yield return new WaitForSeconds(0.1f);

        while (isThrown)
        {
            // Opdater kastedistancen, mens objektet bevæger sig
            throwDistance = Vector3.Distance(startPosition, transform.position);
            Debug.Log($"Current throw distance: {throwDistance}");
            Debug.Log($"Current velocity: {rb.velocity.magnitude}");

            // Tjek om objektet er stoppet med at bevæge sig
            if (rb.velocity.magnitude <= 0.01f)
            {
                Debug.Log("Object has stopped moving.");
                isThrown = false;
            }

            // Opdater distancen mens objektet bevæger sig
            displayThrowDistance.UpdateDistanceText(throwDistance);

            // Vent til næste frame
            yield return null;
        }

        // Endelig opdatering af distancen
        displayThrowDistance.UpdateDistanceText(throwDistance);
    }
}
