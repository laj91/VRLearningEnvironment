using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FireBulletOnActivate : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private AudioClip gunFiring;
    [SerializeField] private InputActionReference shootingTrigger;
    private GameObject startGame;

    private AudioSource gunSource;
    private StartShootingGame startShootingGameScript;
    private XRGrabInteractable grabbable;

    void Start()
    {
        startGame = GameObject.Find("! Manager !");
        // Find and validate required references
        if (startGame == null)
        {
            Debug.LogError("StartGame reference is missing!");
            return;
        }

        startShootingGameScript = startGame.GetComponent<StartShootingGame>();
        Debug.Log("StartShootingGame = " + startShootingGameScript.gameHasStarted);
        if (startShootingGameScript == null)
        {
            Debug.LogError("StartShootingGame script is missing on startGame object!");
            return;
        }

        gunSource = GetComponent<AudioSource>();
        if (gunSource == null)
        {
            Debug.LogWarning("AudioSource is missing on " + gameObject.name);
        }

        grabbable = GetComponent<XRGrabInteractable>();

        if (!startShootingGameScript.gameHasStarted)
        {
            // If the game has not started, add listener to grabbable
            if (grabbable == null)
            {
                Debug.LogError("XRGrabInteractable is missing on " + gameObject.name);
                return;
            }

            grabbable.activated.AddListener(FireBullet);
        }
        else
        {
            Debug.Log("StartShootingGame = " + startShootingGameScript.gameHasStarted);
            Destroy(gameObject.GetComponent<XRGrabInteractable>());
            // If the game has started, enable trigger input
            if (shootingTrigger == null)
            {
                Debug.LogError("ShootingTrigger is not assigned!");
                return;
            }

            shootingTrigger.action.Enable();
            shootingTrigger.action.performed += FireBulletInShootingGame;
        }
    }

    private void OnDestroy()
    {
        // Remove event listeners to avoid memory leaks
        if (grabbable != null)
        {
            grabbable.activated.RemoveListener(FireBullet);
        }

        if (shootingTrigger != null)
        {
            shootingTrigger.action.performed -= FireBulletInShootingGame;
            shootingTrigger.action.Disable();
        }
    }

    // Fires a bullet when the interactable is activated
    private void FireBullet(ActivateEventArgs arg)
    {
        FireBulletCommon();
    }

    // Fires a bullet when the shooting trigger is pressed during the game
    private void FireBulletInShootingGame(InputAction.CallbackContext context)
    {
        Debug.Log("Button press registered in game");
        FireBulletCommon();
    }

    // Common logic for spawning and firing a bullet
    private void FireBulletCommon()
    {
        if (bullet == null || spawnPoint == null)
        {
            Debug.LogError("Bullet or spawn point is not set!");
            return;
        }

        GameObject spawnedBullet = Instantiate(bullet);
        if (gunSource != null && gunFiring != null)
        {
            gunSource.PlayOneShot(gunFiring);
        }
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().linearVelocity = spawnPoint.forward * bulletSpeed;
        Destroy(spawnedBullet, 5);
    }
}
