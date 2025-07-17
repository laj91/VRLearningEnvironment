using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootingGameManager : MonoBehaviour
{
    //[SerializeField] private LevelData[] levels;
    [Header("Game Settings")]
    [SerializeField] private List<LevelData> levels = new List<LevelData>();
    [SerializeField] private GameObject player;
    [SerializeField] float time = 0;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI pointCanvasText;
    [SerializeField] private GameObject finishCanvas;
    [SerializeField] private TextMeshProUGUI timeAndPoints;
    //[SerializeField] private TextMeshProUGUI totalScore;
    
    private int numberOfTargets = 0;
    private int totalPoints = 0;
    private int currentLevelIndex = 0;
    private bool isGameRunning = false;

    public void StartLevel()
    {
        isGameRunning = true;
        LevelData currentLevel = levels[currentLevelIndex];

        // Aktivér og deaktiver objekter
        foreach (GameObject obj in currentLevel.objectsToEnable)
        {
            obj.SetActive(true);
            numberOfTargets++;
        }

        if (currentLevel.objectsToDisable != null)
        {
            foreach (GameObject obj in currentLevel.objectsToDisable)
            {
                obj.SetActive(false);
            }
        }
        

        // Flyt objekter til deres positioner
        for (int i = 0; i < currentLevel.objectPositions.Length; i++)
        {
            if (currentLevel.objectsToEnable.Length > i)
            {
                currentLevel.objectsToEnable[i].transform.position = currentLevel.objectPositions[i].position;
                currentLevel.objectsToEnable[i].transform.rotation = currentLevel.objectPositions[i].rotation;
            }
        }

        // Flyt spilleren til startposition
        player.transform.position = currentLevel.playerStartPosition.position;
        player.transform.rotation = currentLevel.playerStartPosition.rotation;

        
    }
    private void Update()
    {
        if (isGameRunning)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
        }
        

    }

    public void NextLevel()
    {
        if (currentLevelIndex < levels.Count - 1)
        {
            currentLevelIndex++;
            //StartLevel();
        }
        else
        {
            Debug.Log("Du har gennemført alle niveauer!");
            // Et eller andet skal ske her
        }
    }

    public void AddPoints()
    {
        totalPoints++;
        Debug.Log("Total points: " + totalPoints);

        pointCanvasText.text = totalPoints.ToString();

        if (totalPoints == numberOfTargets || time == 0)
        {
            NextLevel();
            FinishScreen();
        }
    }

    private void FinishScreen()
    {
        finishCanvas.SetActive(true);
        timeAndPoints.text = $"Total Time: {time}\r\nTotal Points: {totalPoints}";
        isGameRunning = false;
        //Spilleren får vist et canvas med samlede point og tid
        //Spilleren bliver spurgt om de vil fortsætte, eller afslutte
            //Her skal currentLevelIndex++; og StartLevel(); kaldes
    }


}
