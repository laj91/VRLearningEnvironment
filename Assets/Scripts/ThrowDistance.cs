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
        // Nulstil kastedistancen, n�r objektet grebes
        if (isThrown)
        {
            displayThrowDistance.ClearCurrentObject();
            isThrown = false;
        }
    }

    public void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("Object released");
        // Registrer startpositionen n�r objektet slippes
        startPosition = transform.position;
        isThrown = true;

        // Angiv til DisplayThrowDistance, at dette objekt er valgt
        displayThrowDistance.SetCurrentObject(this);

        // Start Coroutine til at m�le kastet
        StartCoroutine(MeasureThrowDistance());
    }

    private IEnumerator MeasureThrowDistance()
    {
        // Vent en lille smule for at give objektet tid til at accelerere
        yield return new WaitForSeconds(0.1f);

        while (isThrown)
        {
            // Opdater kastedistancen, mens objektet bev�ger sig
            throwDistance = Vector3.Distance(startPosition, transform.position);
            Debug.Log($"Current throw distance: {throwDistance}");
            Debug.Log($"Current velocity: {rb.linearVelocity.magnitude}");

            // Tjek om objektet er stoppet med at bev�ge sig
            if (rb.linearVelocity.magnitude <= 0.01f)
            {
                Debug.Log("Object has stopped moving.");
                isThrown = false;
            }

            // Opdater distancen mens objektet bev�ger sig
            displayThrowDistance.UpdateDistanceText(throwDistance);

            // Vent til n�ste frame
            yield return null;
        }

        // Endelig opdatering af distancen
        displayThrowDistance.UpdateDistanceText(throwDistance);
    }
}
