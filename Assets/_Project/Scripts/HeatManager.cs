using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeatManager : MonoBehaviour
{
    public static HeatManager instance;

    [Header("Heat Settings")]
    public float maxHeat = 100f;
    public float currentHeat = 0f;
    public float coolingRate = 10f;
    private bool isDead = false;

    [Header("UI Polish")]
    public Slider heatSlider;
    public Image fillImage;

    [Header("Visuals")]
    public Renderer playerRenderer;
    
    // THIS IS THE NEW VARIABLE YOU WERE MISSING
    public Gradient heatGradient; 
    
    public float glowIntensity = 2f;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        if (isDead) return;

        // 1. Logic
        if (currentHeat > 0)
        {
            currentHeat -= coolingRate * Time.deltaTime;
        }
        currentHeat = Mathf.Clamp(currentHeat, 0, maxHeat);

        // 2. Visuals
        UpdateVisuals();

        // 3. Death
        if (currentHeat >= maxHeat)
        {
            Die();
        }
    }

    public void AddHeat(float amount)
    {
        currentHeat += amount;
    }

    void UpdateVisuals()
    {
        // Calculate percentage (0.0 to 1.0)
        float heatRatio = currentHeat / maxHeat;

        // Get the color from the Gradient at this percentage
        Color targetColor = heatGradient.Evaluate(heatRatio);

        // Update UI Bar Color
        if (fillImage != null)
        {
            fillImage.color = targetColor;
        }

        // Update UI Slider Value
        if (heatSlider != null)
        {
            heatSlider.value = heatRatio;
        }

        // Update Robot Emission (Multiply by intensity to make it glow)
        if (playerRenderer != null)
        {
            playerRenderer.material.SetColor("_EmissionColor", targetColor * glowIntensity);
        }
    }

    void Die()
    {
        Debug.Log("<color=red>SYSTEM OVERHEAT: Respawning...</color>");
        
        // Use the FallHandler's global respawn position instead of reloading the scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;
            
            player.transform.position = FallHandler.currentRespawnPos;
            
            if (cc != null) cc.enabled = true;
        }

        // Reset heat so they don't immediately die again
        currentHeat = 0f;
        isDead = false;
    }
}