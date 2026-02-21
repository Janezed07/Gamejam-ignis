using UnityEngine;

public class BeaconEndGame : MonoBehaviour
{
    [Header("End Game Setup")]
    [Tooltip("Drag your deactivated Victory Panel here")]
    public GameObject victoryUI; 

    private void OnTriggerEnter(Collider other)
    {
        // We check for the CharacterController instead of a Tag, just like the vents!
        if (other.GetComponent<CharacterController>() != null)
        {
            Debug.Log("BEACON REACHED! PLAYER WINS!");

            // 1. Turn on the Victory Screen!
            if (victoryUI != null)
            {
                victoryUI.SetActive(true);
            }

            // 2. Freeze all time and movement
            Time.timeScale = 0f;

            // 3. Unlock the mouse so the player/judge can click away or close the game
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}