using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

[RequireComponent(typeof(Collider))]
public class MonolithCore : MonoBehaviour
{
    [Header("End Game Settings")]
    [Tooltip("The massive sound that plays when they touch the core")]
    public AudioClip victorySound; 
    
    [Tooltip("Check this to completely close the .exe when they win. Uncheck to load a menu instead.")]
    public bool quitGameInstead = true; 
    public string mainMenuSceneName = "Menu"; 

    private bool _isEnding = false;

    private void Start()
    {
        // Force the core's collider to be a trigger so they pass into it
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Did the player touch the core?
        if (!_isEnding && (other.CompareTag("Player") || other.GetComponent<CharacterController>() != null))
        {
            _isEnding = true; // Lock it so it only triggers once!
            StartCoroutine(EndGameSequence());
        }
    }

    private IEnumerator EndGameSequence()
    {
        // ... (your existing sound code)

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            
            // Check if the controller is already off before trying to move or disable it
            if (cc != null && cc.enabled) 
            {
                cc.enabled = false; 
            }
        }

        yield return new WaitForSeconds(3.0f);

        // Load Main Menu
        SceneManager.LoadScene("OutroCutScene"); 
    }
}