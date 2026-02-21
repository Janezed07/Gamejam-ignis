using UnityEngine;

[RequireComponent(typeof(Collider))] // Forces Unity to ensure a collider exists
public class CoolingVent : MonoBehaviour
{
    [Header("Cooling Settings")]
    [Tooltip("How fast the heat drops while standing here")]
    public float coolingRate = 50f;
    
    [Header("Checkpoint Settings")]
    public bool isCheckpoint = true;
    
    [Header("Visual Feedback")]
    public ParticleSystem steamParticles;
    public Light ventLight;
    public AudioSource healSound;

    private bool _isSaved = false;
    private float _baseIntensity;

    private void Start()
    {
        // FAILSAFE 1: Force the collider to be a trigger so the player doesn't bump into it
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;

        if (ventLight != null)
        {
            _baseIntensity = ventLight.intensity;
        }
    }

    private void Update()
    {
        // Pulse the light smoothly before the checkpoint is claimed
        if (isCheckpoint && ventLight != null && !_isSaved)
        {
            ventLight.intensity = _baseIntensity + Mathf.Sin(Time.time * 3f) * 0.5f;
        }
    }

    // FAILSAFE 2: Bulletproof Player Detection
    private bool IsPlayer(Collider other)
    {
        // It checks the Tag first. If the Tag is missing, it checks if the object has a CharacterController!
        return other.CompareTag("Player") || other.GetComponent<CharacterController>() != null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            Debug.Log("<color=cyan>PLAYER ENTERED VENT!</color> Trigger fired successfully.");

            // 1. Play Feedback
            if (steamParticles != null && !steamParticles.isPlaying) steamParticles.Play();
            if (healSound != null && !healSound.isPlaying) healSound.Play();

            
            // 2. Save Checkpoint
            if (isCheckpoint && !_isSaved)
            {
                FallHandler.currentRespawnPos = transform.position + (Vector3.up * 1.5f);
                _isSaved = true;

                if (ventLight != null) 
                {
                    ventLight.color = Color.green;
                    ventLight.intensity = _baseIntensity + 1f;
                }
                
                Debug.Log("<color=green>CHECKPOINT SAVED!</color> Safe zone locked in.");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsPlayer(other))
        {
            // Safely cool the player down
            if (HeatManager.instance != null)
            {
                HeatManager.instance.AddHeat(-coolingRate * Time.deltaTime);
            }
            else
            {
                Debug.LogWarning("CoolingVent: HeatManager is missing from the scene!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            if (steamParticles != null) steamParticles.Stop();
            if (healSound != null) healSound.Stop();
        }
    }
}