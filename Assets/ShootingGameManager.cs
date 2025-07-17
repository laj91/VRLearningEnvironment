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
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI timePassed;
    
    private int numberOfTargets = 0;
    private int totalPoints = 0;
    private int currentLevelIndex = 0;
    private bool isGameRunning = false;

    // Starts the current level and sets up objects and player position
    public void StartLevel()
    {
        isGameRunning = true;
        LevelData currentLevel = levels[currentLevelIndex];

        // Enable and count target objects
        foreach (GameObject obj in currentLevel.objectsToEnable)
        {
            obj.SetActive(true);
            numberOfTargets++;
        }

        // Disable objects if any are specified
        if (currentLevel.objectsToDisable != null)
        {
            foreach (GameObject obj in currentLevel.objectsToDisable)
            {
                obj.SetActive(false);
            }
        }
        
        // Move enabled objects to their designated positions
        for (int i = 0; i < currentLevel.objectPositions.Length; i++)
        {
            if (currentLevel.objectsToEnable.Length > i)
            {
                currentLevel.objectsToEnable[i].transform.position = currentLevel.objectPositions[i].position;
                currentLevel.objectsToEnable[i].transform.rotation = currentLevel.objectPositions[i].rotation;
            }
        }

        // Move player to the starting position
        player.transform.position = currentLevel.playerStartPosition.position;
        player.transform.rotation = currentLevel.playerStartPosition.rotation;
    }

    // Updates the timer and UI while the game is running
    private void Update()
    {
        if (isGameRunning)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                timePassed.text = $"Time: {Mathf.Round(time)}";
            }
        }
    }

    // Advances to the next level or ends the game if all levels are completed
    public void NextLevel()
    {
        if (currentLevelIndex < levels.Count - 1)
        {
            currentLevelIndex++;
            //StartLevel();
        }
        else
        {
            Debug.Log("You have completed all levels!");
            // Something should happen here
        }
    }

    // Adds points when a target is hit and checks for level completion
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

    // Displays the finish screen and stops the game
    private void FinishScreen()
    {
        finishCanvas.SetActive(true);
        score.text = $"Total Time: {time}\r\nTotal Points: {totalPoints}";
        isGameRunning = false;
    }
}
