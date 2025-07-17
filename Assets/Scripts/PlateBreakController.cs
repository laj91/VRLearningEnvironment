using UnityEngine;

public class PlateBreakController : MonoBehaviour
{
    public float maxHealth = 200f;  // Liv på tallerkenen
    private float currentHealth;
    public float baseDamage = 10f;  // Grundskade ved hver kollision
    public GameObject brokenPlate;  // Reference til den brudte tallerken
    public LayerMask plateLayer;    // Lag til at identificere tallerkener
    public float sphereSize = 0.1f; // Størrelse på overlap-sfæren for skade

    public float maxForce = 0.3f;   // Maksimal kraft på brudte tallerkens dele
    public float minForce = 0.1f;   // Minimal kraft på brudte tallerkens dele
    public bool addForceToPlate = false;

    private Rigidbody rb;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();

        // Sørg for at den hele tallerken starter aktiv, og den brudte er deaktiveret
        gameObject.SetActive(true);
        if (brokenPlate != null)
        {
            brokenPlate.SetActive(false);
        }
    }

    private void Update()
    {
        // Sørg for, at den brudte tallerken følger position og rotation af den hele, indtil bruddet sker
        if (gameObject.activeInHierarchy && brokenPlate != null)
        {
            brokenPlate.transform.position = gameObject.transform.position;
            brokenPlate.transform.rotation = gameObject.transform.rotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Hent kollisionens relative hastighed (velocity)
        float velocity = collision.relativeVelocity.magnitude;
        //Debug.Log(collision.gameObject.name + "collided with " + gameObject.name);

        // Tjek om det objekt, tallerkenen rammer, har et "SurfaceMaterial" script (eller lignende)
        SurfaceMaterial surface = collision.gameObject.GetComponent<SurfaceMaterial>();
        if (surface != null)
        {
            // Hent overfladens hårdhed fra surface-scriptet
            float surfaceHardness = surface.hardness;

            // Beregn skade baseret på overfladens hårdhed og hastigheden
            float damage = baseDamage * surfaceHardness * velocity;

            // Påfør skade på tallerkenen
            TakeDamage(damage);
        }
        else
        {
            TakeDamage(baseDamage * 0.5f * velocity);
        }
    }

    // Metode til at tage skade
    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //Debug.Log($"Plate took {damage} damage, current health: {currentHealth}");

        // Hvis tallerkenens liv når 0, destrueres den og erstattes med den brudte version
        if (currentHealth <= 0)
        {
            BreakPlate(); // Ødelæg tallerkenen
        }
    }

    // Metode til at ødelægge tallerkenen og aktivere den brudte version
    void BreakPlate()
    {
        if (brokenPlate != null)
        {
            // Deaktiver den hele tallerken og aktiver den brudte
            brokenPlate.SetActive(true);

            if (addForceToPlate)
            {
                // Sørg for, at den brudte tallerken frigives
                foreach (Rigidbody rb in brokenPlate.GetComponentsInChildren<Rigidbody>())
                {
                    // Generer en tilfældig kraftretning
                    Vector3 randomDirection = Random.onUnitSphere; // En tilfældig retning i alle dimensioner
                    float forceMagnitude = Random.Range(minForce, maxForce); // Juster kraftens størrelse

                    // Tilføj kraft i den tilfældige retning
                    rb.AddForce(randomDirection * forceMagnitude, ForceMode.Impulse);

                    rb.constraints = RigidbodyConstraints.None; // Fjern begrænsninger
                    rb.isKinematic = false; // Gør dem bevægelige
                    rb.useGravity = true;
                }
            }

            // Destruer den hele tallerken
            Destroy(gameObject); // Dette kan erstattes med en ødelæggelsesanimering eller effekt
        }
    }

    // Tegn en kugle i editoren for at visualisere sphere-området
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereSize); // Tegn en rød kugle med den angivne radius
    }
}
