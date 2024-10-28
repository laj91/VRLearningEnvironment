using System.Collections.Generic;
using UnityEngine;

public class ArrowsObjectPool : MonoBehaviour
{
    // En statisk reference til sig selv, s� andre scripts nemt kan f� adgang til den uden at skulle finde objektet i scenen.
    public static ArrowsObjectPool SharedInstance;

    // En liste til at holde de pr�-instancierede pile.
    public List<GameObject> pooledObjects;

    // En reference til den GameObject (i dette tilf�lde en pil), der skal kopieres.
    public GameObject objectToPool;

    // Antallet af pile der skal oprettes og l�gges i poolen ved start.
    public int amountToPool;

    // Awake kaldes n�r scriptet initialiseres. Her bliver SharedInstance sat til denne instans.
    void Awake()
    {
        SharedInstance = this;
    }

    // Start kaldes f�r det f�rste frame opdateres. Her oprettes og deaktiveres de foruddefinerede pile og tilf�jes til poolen.
    void Start()
    {
        // Initialiserer listen med pile.
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        // Opretter det antal pile som specificeret i amountToPool og tilf�jer dem til listen.
        for (int i = 0; i < amountToPool; i++)
        {
            // Instansierer en ny pil.
            tmp = Instantiate(objectToPool);

            // Deaktiverer pilen, s� den ikke er synlig eller aktiv i spillet.
            tmp.SetActive(false);

            // Tilf�jer pilen til poolen.
            pooledObjects.Add(tmp);
        }
    }

    // En metode til at hente en inaktiv pil fra poolen.
    public GameObject GetPooledObject()
    {
        // Genneml�ber listen over pile for at finde en inaktiv pil.
        for (int i = 0; i < amountToPool; i++)
        {
            // Tjekker om pilen er inaktiv i spillets hierarki.
            if (!pooledObjects[i].activeInHierarchy)
            {
                // Returnerer den fundne inaktive pil.
                return pooledObjects[i];
            }
        }
        // Returnerer null, hvis der ikke er nogen inaktive pile tilg�ngelige.
        return null;
    }
}
