using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // Point A position
    public Transform pointB; // Point B position
    public float speed = 2.0f; // Movement speed
    public float arrivalThreshold = 0.1f; // Distance threshold to consider the platform has reached the target

    private Vector3 targetPosition; // Target position to move towards
    private bool movingToB = true; // Flag to track if the platform is moving towards B

    private void Start()
    {
        // Set initial target position to point B
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
                targetPosition = pointA.position;
            }
            else
            {
                targetPosition = pointB.position;
            }

            // Toggle the flag for next movement
            movingToB = !movingToB;
        }
    }
}
