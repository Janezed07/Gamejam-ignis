using System.Collections; // Needed for Coroutines (timers)
using UnityEngine;
using UnityEngine.SceneManagement; 

[RequireComponent(typeof(Collider))]
public class PortalExit : MonoBehaviour
{
    [Tooltip("The exact name of the scene to load (e.g., Level_02)")]
    public string nextSceneName = "MidCutScene";

    [Header("Audio")]
    public AudioClip portalSound; // The audio slot for the Inspector!

    private bool _isLoading = false; // Stops the portal from triggering 50 times in one frame

    private void Start()
    {
        // Force the collider to be a trigger
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if it's the Player, AND make sure we aren't already loading the level
        if (!_isLoading && (other.CompareTag("Player") || other.GetComponent<CharacterController>() != null))
        {
            _isLoading = true; // Lock the portal!
            StartCoroutine(PortalTransitionSequence());
        }
    }

    // This handles the timing so the audio doesn't get cut off!
    private IEnumerator PortalTransitionSequence()
    {
        Debug.Log("<color=magenta>PORTAL ENTERED!</color> Playing sound and loading scene: " + nextSceneName);
        
        // 1. Play the victory/warp sound
        if (portalSound != null)
        {
            AudioSource.PlayClipAtPoint(portalSound, transform.position, 1.0f);
        }

        // 2. WAIT for 1.5 seconds so the sound can actually play!
        // (You can change this number if your sound effect is longer/shorter)
        yield return new WaitForSeconds(1.5f);

        // Optional: Reset the player's heat to zero before the new level starts
        if (HeatManager.instance != null)
        {
            HeatManager.instance.AddHeat(-HeatManager.instance.currentHeat); 
        }

        // 3. Load the next level
        SceneManager.LoadScene(nextSceneName);
    }
}