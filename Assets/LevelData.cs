using UnityEngine;

[System.Serializable]
public class LevelData
{
    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;
    public Transform[] objectPositions;
    public Transform playerStartPosition;
}
