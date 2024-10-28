using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlateInteractionController : MonoBehaviour
{
    private Rigidbody wholePlateRb;
    public float sphereSize = 0.1f;
    public LayerMask plateLayer; // Layer til at identificere andre tallerkener.
    public XRGrabInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();

        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnGrab);
            interactable.selectExited.AddListener(OnRelease);  // Tilføj lytter til slip
            Debug.Log("OnGrab and OnRelease added to plate");
        }
    }

    private void Start()
    {
        wholePlateRb = GetComponent<Rigidbody>();
        wholePlateRb.isKinematic = true;
        wholePlateRb.useGravity = false;
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log($"{gameObject.name} has been grabbed!");
        if (wholePlateRb.isKinematic)
        {
            wholePlateRb.isKinematic = false; // Gør tallerkenen dynamisk, når den bliver grebet
            wholePlateRb.useGravity = true;    // Slå tyngdekraft til
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Sørg for at objektet forbliver dynamisk når det slippes
        wholePlateRb.isKinematic = false;
        wholePlateRb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (wholePlateRb.isKinematic)
        {
            wholePlateRb.isKinematic = false; // Fjern kinematic fra tallerkenen
            wholePlateRb.useGravity = true;   // Slå tyngdekraft til
            ActivateStack(transform.position); // Aktivér hele stablen omkring kollisionspunktet
        }
    }

    // Denne metode aktiverer hele stablen af tallerkener omkring kollisionspunktet
    void ActivateStack(Vector3 stackCenter)
    {
        Collider[] entireStack = Physics.OverlapSphere(stackCenter, sphereSize * 2, plateLayer);

        foreach (Collider collider in entireStack)
        {
            Rigidbody nearbyRb = collider.GetComponent<Rigidbody>();
            if (nearbyRb != null && nearbyRb.isKinematic)
            {
                nearbyRb.isKinematic = false; // Gør alle tallerkener i stablen dynamiske
                nearbyRb.useGravity = true;   // Slå tyngdekraften til for hver tallerken
            }
        }
    }

    // Tegn en kugle i editoren for at visualisere sphere-området
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereSize); // Tegn en rød kugle med den angivne radius
    }
}
