using UnityEngine;

public class HeatTrap : MonoBehaviour
{
    [Tooltip("How much heat is added per second while inside the trap")]
    public float heatDamageRate = 30f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (HeatManager.instance != null)
            {
                // Rapidly increase heat while they stand in the fire
                HeatManager.instance.AddHeat(heatDamageRate * Time.deltaTime);
            }
        }
    }
}