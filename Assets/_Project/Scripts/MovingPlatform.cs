using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(0, 10, 0); // Moves UP 10 units
    public float speed = 3f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Smoothly move back and forth (PingPong)
        float cycle = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPos, startPos + moveOffset, cycle);
    }

    // Stick the player to the platform (Crucial for moving platforms)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) other.transform.SetParent(null);
    }
}