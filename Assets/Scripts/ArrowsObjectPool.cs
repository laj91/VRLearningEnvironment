using System.Collections.Generic;
using UnityEngine;

public class ArrowsObjectPool : MonoBehaviour
{
    // En statisk reference til sig selv, så andre scripts nemt kan få adgang til den uden at skulle finde objektet i scenen.
    public static ArrowsObjectPool SharedInstance;

    // En liste til at holde de præ-instancierede pile.
    public List<GameObject> pooledObjects;

    // En reference til den GameObject (i dette tilfælde en pil), der skal kopieres.
    public GameObject objectToPool;

    // Antallet af pile der skal oprettes og lægges i poolen ved start.
    public int amountToPool;

    // Awake kaldes når scriptet initialiseres. Her bliver SharedInstance sat til denne instans.
    void Awake()
    {
        SharedInstance = this;
    }

    // Start kaldes før det første frame opdateres. Her oprettes og deaktiveres de foruddefinerede pile og tilføjes til poolen.
    void Start()
    {
        // Initialiserer listen med pile.
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        // Opretter det antal pile som specificeret i amountToPool og tilføjer dem til listen.
        for (int i = 0; i < amountToPool; i++)
        {
            // Instansierer en ny pil.
            tmp = Instantiate(objectToPool);

            // Deaktiverer pilen, så den ikke er synlig eller aktiv i spillet.
            tmp.SetActive(false);

            // Tilføjer pilen til poolen.
            pooledObjects.Add(tmp);
        }
    }

    // En metode til at hente en inaktiv pil fra poolen.
    public GameObject GetPooledObject()
    {
        // Gennemløber listen over pile for at finde en inaktiv pil.
        for (int i = 0; i < amountToPool; i++)
        {
            // Tjekker om pilen er inaktiv i spillets hierarki.
            if (!pooledObjects[i].activeInHierarchy)
            {
                // Returnerer den fundne inaktive pil.
                return pooledObjects[i];
            }
        }
        // Returnerer null, hvis der ikke er nogen inaktive pile tilgængelige.
        return null;
    }
}
