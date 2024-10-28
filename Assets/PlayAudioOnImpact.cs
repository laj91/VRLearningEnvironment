using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnImpact : MonoBehaviour
{
    [SerializeField] AudioClip impactSound;
    AudioSource impactSource;
    // Start is called before the first frame update
    void Start()
    {
        impactSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (impactSound != null)
        {
            impactSource.PlayOneShot(impactSound);
        }
    }
}
