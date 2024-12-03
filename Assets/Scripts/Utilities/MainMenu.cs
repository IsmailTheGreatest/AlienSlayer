using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button[] levelButtons; // Array of level buttons (assigned in the Inspector)
    public Button helpButton; // Reference to the Help button
    public GameObject helpPanel; // Reference to the Panel GameObject

    private bool isHelpPanelVisible = false; // Tracks the visibility state of the panel

    private void Start()
    {
        UpdateButtonColors();

        // Ensure the help panel is hidden initially
        if (helpPanel != null)
        {
            helpPanel.SetActive(false);
        }

        // Add a listener for the Help button
        if (helpButton != null)
        {
            helpButton.onClick.AddListener(ToggleHelpPanel);
        }
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

    private void ToggleHelpPanel()
    {
        // Toggle the visibility of the panel
        isHelpPanelVisible = !isHelpPanelVisible;

        if (helpPanel != null)
        {
            helpPanel.SetActive(isHelpPanelVisible);
        }

        // Update the button text
        if (helpButton != null)
        {
            Text buttonText = helpButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = isHelpPanelVisible ? "Close" : "Help";
            }
        }
    }
}
