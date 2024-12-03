using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool[] levelCompleted = new bool[5]; // Tracks completion status for levels
    public Color completedColor = Color.green; // Color to indicate completed levels
    public Color defaultColor = Color.white; // Default button color

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level_" + levelIndex);
    }

  

    public void MarkLevelComplete(int levelIndex)
    {
        levelCompleted[levelIndex] = true;

        Debug.Log($"Level {levelIndex} completed!");

        LoadMainMenu();
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public bool IsLevelComplete(int levelIndex)
    {
        return levelCompleted[levelIndex];
    }
}
