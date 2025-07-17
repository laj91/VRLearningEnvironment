using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using TMPro;

public class StartShootingGame : MonoBehaviour
{
    [Header("Player related")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] objectsToEnable;
    [SerializeField] GameObject[] objectsToDisable;
    [SerializeField] GameObject dynamicMoveProviderObject;
    [SerializeField] Transform playerStartPosition;
    [SerializeField] ShootingGameManager shootingGameManager;

    [Header("Gun related")]
    [SerializeField] Transform gunPositionOnPlayer;
    [SerializeField] Transform gunParentObject;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject playerRightHand;

    Vector3 instantiatedGunPosition;
    Quaternion instantiatedGunRotation;

    public bool gameHasStarted = false;
    
    public void StartGame()
    {
        SetPlayerPosition();
        SetGunPositionAndDisableHands();
        DisableObjects();
        EnableObjects();
        gameHasStarted = true;
    }

    public void StartLevel()
    {
        // vent med denne
        shootingGameManager.StartLevel();
        
    }

    private void EnableObjects()
    {
        foreach (GameObject item in objectsToEnable)
        {
            item.SetActive(true);
        }
    }

    private void DisableObjects()
    {
        foreach (GameObject item in objectsToDisable)
        {
            item.SetActive(false);
        }
    }

    private void SetPlayerPosition()
    {
        dynamicMoveProviderObject.SetActive(false);
        player.transform.position = playerStartPosition.position;
        player.transform.rotation = playerStartPosition.rotation;
    }

    private void SetGunPositionAndDisableHands()
    {
        // Position og rotation fra handens position
        instantiatedGunPosition = gunPositionOnPlayer.position;
        instantiatedGunRotation = gunPositionOnPlayer.rotation;

        // Instansiering af pistolen
        GameObject instantiatedGun = Instantiate(gun, instantiatedGunPosition, instantiatedGunRotation);
        Rigidbody instantiatedGunRb = instantiatedGun.GetComponent<Rigidbody>();

        instantiatedGunRb.useGravity = false;
        instantiatedGunRb.isKinematic = true;
        

        // Sæt forælderen korrekt for at følge hånden
        instantiatedGun.transform.SetParent(gunParentObject, true); // Brug true, så pistolen følger parentens transform.

        // Deaktiver hånden
        playerRightHand.SetActive(false);

        // Hvis du har problemer med synligheden, kan du tilføje debug checks her for at bekræfte
        Debug.Log("Pistol instansieret og sat som child af gunParentObject.");
    }



}
