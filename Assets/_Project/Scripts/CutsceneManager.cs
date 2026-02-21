using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [Header("Transition Settings")]
    public string nextSceneName;
    public float cutsceneDuration = 5f; // How long before it switches automatically

    void Start()
    {
        // Start the timer to load the next scene
        Invoke("LoadNextScene", cutsceneDuration);
    }

    void Update()
    {
        // Allow player to skip by pressing Space or Escape
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}