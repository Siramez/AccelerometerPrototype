using UnityEngine;

public class MovingPlatformUpDown : MonoBehaviour
{
    public Transform pointA; // Bottom point position
    public Transform pointB; // Top point position
    public float speed = 2.0f; // Movement speed
    public float arrivalThreshold = 0.1f; // Distance threshold to consider the platform has reached the target

    private Vector3 targetPosition; // Target position to move towards
    private bool movingToB = true; // Flag to track if the platform is moving upwards

    private void Start()
    {
        // Set initial target position to point B (top point)
        targetPosition = pointB.position;
    }

    private void Update()
    {
        // Move platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // If platform is close enough to the target position, switch target to the other point
        if (Vector3.Distance(transform.position, targetPosition) < arrivalThreshold)
        {
            if (movingToB)
            {
                targetPosition = pointA.position; // Move towards point A (bottom point)
            }
            else
            {
                targetPosition = pointB.position; // Move towards point B (top point)
            }

            // Toggle the flag for next movement
            movingToB = !movingToB;
        }
    }
}
