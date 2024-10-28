using UnityEngine;

public class SurfaceMaterial : MonoBehaviour
{
    [Tooltip("Hvor hård denne overflade er. 1 = Hård (f.eks. beton), 0.1 = Blød (f.eks. pude)")]
    [Range(0.1f, 1f)]  // Begræns hårdheden mellem 0.1 og 1
    public float hardness = 1f;  // Standardværdi er hård (1 = hårdt materiale)

    // Du kan tilføje yderligere logik eller egenskaber for materialet, hvis det bliver nødvendigt
}

