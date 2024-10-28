using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class StartShootingGame : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] objectsToEnable;
    [SerializeField] GameObject dynamicMoveProviderObject;
    [SerializeField] Transform playerStartPosition;

    bool gameHasStarted = false;

    public void StartGame()
    {
        EnableObjects();
        SetPlayerPosition();
    }

    private void EnableObjects()
    {
        foreach (GameObject item in objectsToEnable)
        {
            item.SetActive(true);
        }
    }

    private void SetPlayerPosition()
    {
        dynamicMoveProviderObject.SetActive(false);
        player.transform.position = playerStartPosition.position;
        player.transform.rotation = playerStartPosition.rotation;
    }
}
