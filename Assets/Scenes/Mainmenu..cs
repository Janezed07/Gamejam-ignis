using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject introPanel;
    public GameObject mainButtons;
    public GameObject levelsPanel;

    void Start()
    {
        introPanel.SetActive(true);
        mainButtons.SetActive(false);
        levelsPanel.SetActive(false);
    }

    public void ContinueToMenu()
    {
        introPanel.SetActive(false);
        mainButtons.SetActive(true);
    }

    // public void PlayGame()
    // {
    //     SceneManager.LoadScene(1); // Loads Gamescene
    // }

    public void OpenLevels()
    {
        mainButtons.SetActive(false);
        levelsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        levelsPanel.SetActive(false);
        mainButtons.SetActive(true);
    }

    // public void LoadLevel1()
    // {
    //     SceneManager.LoadScene(1); // Loads Gamescene
    // }

    public void QuitGame()
    {
        Application.Quit();
    }
}



