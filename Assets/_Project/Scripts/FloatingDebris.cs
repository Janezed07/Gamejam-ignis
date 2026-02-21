using UnityEngine;

public class FloatingDebris : MonoBehaviour
{
    public float bobHeight = 0.5f; 
    public float bobSpeed = 1f;
    public Vector3 rotationSpeed = new Vector3(5f, 10f, 2f);

    private Vector3 startPos;
    private float randomOffset;

    void Start()
    {
        startPos = transform.position;
        // This offset ensures they don't all bob at the same time
        randomOffset = Random.Range(0f, 100f);
        
        // Randomize scale slightly so they aren't all identical
        transform.localScale *= Random.Range(0.8f, 1.5f);
    }

    void Update()
    {
        // Smooth bobbing using a Sine wave
        float newY = startPos.y + (Mathf.Sin((Time.time + randomOffset) * bobSpeed) * bobHeight);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Constant slow rotation
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}