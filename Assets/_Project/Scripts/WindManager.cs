using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Optional: For a UI warning flash

public class WindManager : MonoBehaviour
{
    [Header("Wind Settings")]
    public Vector3 windDirection = new Vector3(0, 0, 1); // Pushes player along the Z axis
    public float windForce = 15f;
    public float calmDuration = 6f; // Time between gusts
    public float warningDuration = 2f; // Time the indicator flashes before wind hits
    public float gustDuration = 3f; // How long the wind blows

    [Header("References")]
    public Transform player;
    public CharacterController playerController;
    public ParticleSystem windParticles; // Drag a particle system here later

    private bool isWindBlowing = false;

    void Start()
    {
        windDirection = windDirection.normalized;
        StartCoroutine(WindCycle());
    }

    IEnumerator WindCycle()
    {
        while (true)
        {
            // 1. Calm Phase
            isWindBlowing = false;
            if (windParticles != null) windParticles.Stop();
            yield return new WaitForSeconds(calmDuration);

            // 2. Warning Phase (You can trigger UI flashes or Audio here)
            Debug.Log("WIND WARNING!");
            // Optional: Play howling wind sound here
            yield return new WaitForSeconds(warningDuration);

            // 3. Gust Phase
            Debug.Log("WIND HITTING!");
            isWindBlowing = true;
            if (windParticles != null) windParticles.Play();
            yield return new WaitForSeconds(gustDuration);
        }
    }

    void Update()
    {
        if (isWindBlowing && player != null)
        {
            // Cast a ray backwards from the player towards the wind direction
            // If it hits something (like the Monolith), the player is shielded!
            if (!Physics.Raycast(player.position, -windDirection, 20f))
            {
                // Not shielded! Push the player
                // We use transform.position instead of Move to bypass CharacterController grounding conflicts
                player.position += windDirection * (windForce * Time.deltaTime);
            }
        }
    }
}