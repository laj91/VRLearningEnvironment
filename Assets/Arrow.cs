using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] Transform tip;
    [SerializeField] AudioSource hitSound;

    Rigidbody _rigidBody;
    bool _inAir = false;
    Vector3 _lastPosition = Vector3.zero;

    [SerializeField] ParticleSystem _particleSystem;
    //[SerializeField] TrailRenderer _trailRenderer;
    // Start is called before the first frame update
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        //_trailRenderer = GetComponent<TrailRenderer>();
        //_particleSystem = GetComponent<ParticleSystem>();
        PullInteraction.PullActionReleased += Release;

        Stop();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        PullInteraction.PullActionReleased -= Release;
        gameObject.transform.parent = null;
        _inAir = true;
        SetPhysics(true);

        Vector3 force = transform.forward * value * speed;
        _rigidBody.AddForce(force, ForceMode.Impulse);

        StartCoroutine(RotateWithVelocity());

        _lastPosition = tip.position;

        _particleSystem.Play();
        //_trailRenderer.emitting = true;
    }

    IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();

        while (_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidBody.linearVelocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (_inAir)
        {
            CheckCollision();
            _lastPosition = tip.position;
        }
    }

    void CheckCollision()
    {
        if (Physics.Linecast(_lastPosition, tip.position, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.gameObject.layer != 8)
            {
                if (hitInfo.transform.TryGetComponent(out Rigidbody body))
                {
                    _rigidBody.interpolation = RigidbodyInterpolation.None;
                    transform.parent = hitInfo.transform;
                    body.AddForce(_rigidBody.linearVelocity, ForceMode.Impulse);
                }
                PlaySound();
                Stop();

            }
        }
    }

    private void PlaySound()
    {
        hitSound.Play();
    }

    void Stop()
    {
        _inAir = false;
        SetPhysics(false);

        _particleSystem.Stop();
        //_trailRenderer.emitting = false;
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidBody.useGravity = usePhysics;
        _rigidBody.isKinematic = !usePhysics;
    }
}
