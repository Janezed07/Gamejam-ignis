using UnityEngine;

public class FallHandler : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip respawnSound;

    // Static means this variable is remembered globally, even across scripts
    public static Vector3 currentRespawnPos;

    void Start()
    {
        // When the level loads, set the safe spot to exactly where the player is standing
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            currentRespawnPos = player.transform.position;
        }
        else
        {
            Debug.LogError("FallHandler: Could not find player! Make sure your robot has the 'Player' tag.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Did the player touch the kill floor?
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player fell! Teleporting to safety...");

            CharacterController cc = other.GetComponent<CharacterController>();
            
            // 1. TURN OFF CONTROLLER (Crucial for teleporting)
            if (cc != null) cc.enabled = false;

            // 2. TELEPORT
            other.transform.position = currentRespawnPos;

            // 3. TURN CONTROLLER BACK ON
            if (cc != null) cc.enabled = true;

            // 4. PLAY THE AUDIO (Playing it at the respawn point so the player actually hears it!)
            if (respawnSound != null)
            {
                AudioSource.PlayClipAtPoint(respawnSound, currentRespawnPos, 1.0f);
            }

            // 5. APPLY PENALTY
            if (HeatManager.instance != null)
            {
                HeatManager.instance.AddHeat(20f); // Punish them with heat for falling
            }
        }
    }
}   