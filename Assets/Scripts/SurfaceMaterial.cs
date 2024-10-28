using UnityEngine;

public class SurfaceMaterial : MonoBehaviour
{
    [Tooltip("Hvor h�rd denne overflade er. 1 = H�rd (f.eks. beton), 0.1 = Bl�d (f.eks. pude)")]
    [Range(0.1f, 1f)]  // Begr�ns h�rdheden mellem 0.1 og 1
    public float hardness = 1f;  // Standardv�rdi er h�rd (1 = h�rdt materiale)

    // Du kan tilf�je yderligere logik eller egenskaber for materialet, hvis det bliver n�dvendigt
}

