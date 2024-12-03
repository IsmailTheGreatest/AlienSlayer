using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button[] levelButtons; // Array of level buttons (assigned in the Inspector)

    private void Start()
    {
        UpdateButtonColors();
    }

    private void UpdateButtonColors()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (GameManager.Instance.IsLevelComplete(i))
            {
                // Change the button color if the level is completed
                levelButtons[i].GetComponent<Image>().color = GameManager.Instance.completedColor;
            }
            else
            {
                // Default button color
                levelButtons[i].GetComponent<Image>().color = GameManager.Instance.defaultColor;
            }
        }
    }

    public void LoadLevel(int levelIndex)
    {
        GameManager.Instance.LoadLevel(levelIndex);
    }
}
